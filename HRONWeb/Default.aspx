<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HRONWeb.Default" %>

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
    <style>
        .alignmiddle {
            margin: 14px 0;
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
    <form id="form1" runat="server">
    <h1 class="mui-appbar"><span class="mui-container"><asp:Literal runat="server" id="txtTitle" EnableViewState="false" /></span></h1>
    <div id="contx" class="mui-container">    
        <div class="mui-container-fluid">
            <asp:Repeater runat="server" id="repeater">
                <ItemTemplate>
                      <div class="mui-row">
                        <div class="mui-col-md-2 alignmiddle"><%# Eval("starttime") %></div>
                        <div class="mui-col-md-5 alignmiddle"><%# Eval("subject") %></div>
                        <div class="mui-col-md-3"><a href="Approve.aspx?WFID=<%# Eval("approverWFID").ToString() %>&AID=<%# Eval("approverActivityID") %>" class="mui-btn mui-btn--primary">Aprove/Reject</a></div>
                      </div>                    
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
    </form>
</body>
</html>
