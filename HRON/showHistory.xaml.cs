using HRONLib;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HRON
{
    /// <summary>
    /// Interaktionslogik für showHistory.xaml
    /// </summary>
    public partial class showHistory : UserControl
    {
        Type entity;
        HRONEntities entities;
        MainWindow mainWindow;
        String PK;

        public showHistory(MainWindow main, HRONEntities _entities, Type entityType, String PrimaryKey)
        {
            InitializeComponent();
            mainWindow = main;
            entities = _entities;
            entity = entityType;
            PK = PrimaryKey;
        }

        public static string GetPrimaryKeyValuesOf(string[] properties)
        {
            if (properties.Length == 1)
            {
                return properties[0];
            }
            if (properties.Length > 1)
            {
                string output = "[";

                output += string.Join(",", properties);

                output += "]";
                return output;
            }
            return null;
        }

        protected void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

            // Laden Sie Ihre Daten nicht zur Entwurfszeit.
            if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                //Laden Sie hier Ihre Daten, und weisen Sie das Ergebnis der CollectionViewSource zu.
                System.Windows.Data.CollectionViewSource myCollectionViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["auditLogViewSource"];
                String entityTypeName = entity.ToString();
                entities.AuditLog.Where(w => entityTypeName.Contains(w.TypeFullName) && w.RecordId == PK).Load();
                myCollectionViewSource.Source = entities.AuditLog.Local.Where(w => entityTypeName.Contains(w.TypeFullName) && w.RecordId == PK);
            }
        }
    }
}
