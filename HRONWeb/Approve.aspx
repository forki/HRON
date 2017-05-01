<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Approve.aspx.cs" Inherits="HRONWeb.Approve" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1"/>    
    <link href="//cdn.muicss.com/mui-0.9.15/css/mui.min.css" rel="stylesheet" type="text/css" />
    <script src="//cdn.muicss.com/mui-0.9.15/js/mui.min.js"></script>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        #txtNotes {
            height: 58px;
            width: 100%;
            margin-top: 30px;
            margin-bottom: 30px;
        }
        body {
            font-family: Calibri;
            font-size: 18px;
        }
        #cont {
            width: 80vw;
            margin: 10px auto;
        }
        h1 {
            margin: 0;
            font-size: 1.6em;
        }
        h1 span  {
            display: block;
            line-height: 64px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" class="mui-form">
    <h1 class="mui-appbar"><span class="mui-container"><asp:Literal runat="server" id="txtTitle" EnableViewState="false" /></span></h1>
    <div id="contx" class="mui-container">
        <br />
        <asp:Literal runat="server" id="txtBody" EnableViewState="false" />
        <br />
        <div class="mui-textfield">
            <textarea runat="server" id="txtNotes" placeholder="Your Notes"></textarea>
        </div>
        <br />
        <asp:Literal runat="server" ID="message"></asp:Literal>
        <button runat="server" id="btnApprove" class="mui-btn mui-btn--primary" onserverclick="btnApprove_ServerClick">Approve</button>
        <button runat="server" id="btnReject" class="mui-btn mui-btn--danger" onserverclick="btnReject_ServerClick">Reject</button>
    </div>
    </form>
</body>
</html>
