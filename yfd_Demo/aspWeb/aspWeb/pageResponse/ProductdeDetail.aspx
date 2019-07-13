<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductdeDetail.aspx.cs" Inherits="aspWeb.pageResponse.ProductdeDetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h4 style="height: 25px">
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/ProductList.aspx">←</asp:HyperLink>
&nbsp;查看明细&nbsp;&nbsp; </h4>
        <p>学院编号：<asp:Label ID="lbSn" runat="server" Text="Label"></asp:Label>
        </p>
        <p>学院名称：<asp:Label ID="lbName" runat="server" Text="Label"></asp:Label>
        </p>
        <p>学院说明：<asp:Label ID="lb3" runat="server" Text="Label"></asp:Label>
        </p>
        <p>
            <asp:Image ID="Image1" runat="server" Height="203px" ImageUrl="https://timgsa.baidu.com/timg?image&amp;quality=80&amp;size=b9999_10000&amp;sec=1542252204051&amp;di=fe50f503dd3ce255381b8f2350369820&amp;imgtype=0&amp;src=http%3A%2F%2F5b0988e595225.cdn.sohucs.com%2Fimages%2F20171217%2F637985d80e2642e3b136339b52f78558.jpeg" Width="249px" />
        </p>
        <p>
            <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/ProductList.aspx">返回</asp:HyperLink>
        </p>
    </div>
    </form>
</body>
</html>
