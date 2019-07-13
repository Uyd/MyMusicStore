<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductList.aspx.cs" Inherits="aspWeb.ProductList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Caption="学院列表" Width="100%" CaptionAlign="Right" CellPadding="1">
            <Columns>
                <asp:HyperLinkField DataNavigateUrlFields="SortCode" DataNavigateUrlFormatString="pageResponse/ProductdeDetail.aspx?id={0}" DataTextField="SortCode" DataTextFormatString="{0}" HeaderText="序号" Text="明细">
                <ItemStyle Width="100px" />
                </asp:HyperLinkField>
                <asp:BoundField DataField="Name" HeaderText="学院名称" >
                <ItemStyle Width="100px" />
                </asp:BoundField>
                <asp:CommandField ShowEditButton="True" >
                <ItemStyle Width="50px" />
                </asp:CommandField>
                <asp:CommandField ShowDeleteButton="True" >
                <ItemStyle Width="50px" />
                </asp:CommandField>
            </Columns>
        </asp:GridView>
    </form>
</body>
</html>
