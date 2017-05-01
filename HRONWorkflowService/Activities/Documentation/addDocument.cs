using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using HRONLib;
using System.Reflection;
using Microsoft.Office.Interop.Word;
using System.IO;

namespace HRONWorkflowService.Activities.Documentation
{

    public sealed class addDocument : CodeActivity
    {
        // Aktivitätseingabeargument vom Typ "string" definieren
        public InArgument<int> employeeId { get; set; }

        public InArgument<int> documentId { get; set; }

        public OutArgument<EmplFiles> emplFiles { get; set; }

        // Wenn durch die Aktivität ein Wert zurückgegeben wird, erfolgt eine Ableitung von CodeActivity<TResult>
        // und der Wert von der Ausführmethode zurückgegeben.
        protected override void Execute(CodeActivityContext context)
        {
            int eId = employeeId.Get(context);
            int dId = documentId.Get(context);

            HRONEntities dbContext = new HRONEntities();
            Employee empl = dbContext.Employee.Where(e => e.emplID == eId).First();
            EmplDocumentation doc = empl.EmplDocumentation.Where(d => d.documentationID == dId).First();
            if (doc != null)
            {
                byte[] bDoc = doc.baDocumentation.documentationDocument;
                string nDoc = doc.baDocumentation.documentationDocumentName;
                if (nDoc.EndsWith(".dotx") || nDoc.EndsWith(".dot"))
                    convertDocument(empl, ref bDoc, ref nDoc);
                EmplFiles fil = new EmplFiles();
                fil.EmplDocumentation = doc;
                fil.Employee = empl;
                fil.FileContent = bDoc;
                fil.FileName = nDoc;

                doc.EmplFiles.Add(fil);

                dbContext.SaveChanges();

                emplFiles.Set(context, fil.UnProxy<EmplFiles>(dbContext));

                cleanup();
            }
        }

        public static void convertDocument(Employee empl, ref byte[] bDoc, ref string nDoc)
        {
            if (nDoc.EndsWith(".dotx") || nDoc.EndsWith(".dot"))
            {
                try
                {
                    // Create word document from Template
                    string basePath = System.IO.Path.GetTempPath() + "\\HR";
                    if (!Directory.Exists(basePath))
                        Directory.CreateDirectory(basePath);

                    String path = basePath + "\\" + Guid.NewGuid().ToString() + "_" + nDoc;
                    File.WriteAllBytes(path, bDoc);

                    Object oMissing = System.Reflection.Missing.Value;
                    Object oTrue = true;
                    Object oFalse = false;
                    Application oWord = new Application();
                    oWord.Visible = false;
                    Document oWordDoc = new Document();
                    Object oTemplatePath = path;
                    Object oTemplatePathOut;
                    if (path.EndsWith(".dot"))
                        oTemplatePathOut = path + ".doc";
                    else
                        oTemplatePathOut = path + ".docx";

                    oWordDoc = oWord.Documents.Add(ref oTemplatePath, ref oMissing, ref oMissing, ref oMissing);
                    foreach (Field myMergeField in oWordDoc.Fields)
                    {
                        Range rngFieldCode = myMergeField.Code;
                        String fieldText = rngFieldCode.Text;
                        if (fieldText.StartsWith(" MERGEFIELD"))
                        {
                            String fieldName = fieldText.Substring(11);
                            fieldName = fieldName.Trim();

                            EmplSalary eSal = empl.EmplSalary.OrderByDescending(o => o.salStartingFrom).First();

                            var properties = empl.GetType().GetProperty(fieldName);
                            if (properties == null && eSal != null)
                                properties = eSal.GetType().GetProperty(fieldName);

                            if (properties != null)
                            {
                                myMergeField.Select();
                                object val = properties.GetValue(empl);
                                if (val == null)
                                    oWord.Selection.TypeText("");
                                else
                                {
                                    if (properties.PropertyType == typeof(DateTime))
                                        oWord.Selection.TypeText(((DateTime)val).ToString("dd/MM/yyyy"));
                                    else
                                        oWord.Selection.TypeText(properties.GetValue(empl).ToString());
                                }
                            }
                        }
                    }
                    object saveChanges = WdSaveOptions.wdDoNotSaveChanges;
                    oWordDoc.SaveAs2(ref oTemplatePathOut, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing);
                    oWordDoc.Close(ref saveChanges, ref oMissing, ref oMissing);
                    oWord.Quit(ref oMissing, ref oMissing, ref oMissing);

                    bDoc = File.ReadAllBytes(oTemplatePathOut.ToString());
                    if (nDoc.EndsWith(".dot"))
                        nDoc = nDoc.Substring(0, nDoc.Length - 3) + "doc";
                    if (nDoc.EndsWith(".dotx"))
                        nDoc = nDoc.Substring(0, nDoc.Length - 4) + "docx";
                }
                catch (Exception)
                {
                    int a = 0;
                }
            }
        }

        private void cleanup()
        {

            try
            {
                string basePath = System.IO.Path.GetTempPath() + "\\HR";
                if (Directory.Exists(basePath))
                    Directory.Delete(basePath, true);
            }
            catch (Exception) { }
        }
    }
}
