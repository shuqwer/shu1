<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MenuManage.aspx.cs" Inherits="用户权限角色管理.MenuManage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <h3>菜单管理</h3>
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="UserManage.aspx" Target="_self">用户管理</asp:HyperLink>
        <br />
        <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="RoleManage.aspx" Target="_self">角色管理</asp:HyperLink>
        <panel>
            <h5>菜单列表</h5>
            <asp:GridView ID="gvMenu" runat="server" AutoGenerateDeleteButton="True" OnRowDeleting="gvMenu_RowDeleting" OnSelectedIndexChanged="gvMenu_SelectedIndexChanged" AutoGenerateSelectButton="True">
            </asp:GridView>
        </panel>
        <panel>
            <h5>编辑菜单</h5>
            <asp:Label ID="Label1" runat="server" Text="ID:"></asp:Label>
            <asp:Label ID="lblID" runat="server" Text=""></asp:Label>
            <br />
            <asp:Label ID="Label2" runat="server" Text="菜单名："></asp:Label>
            <asp:TextBox ID="tbModMenuName" runat="server"></asp:TextBox>
            <br />
             <asp:Label ID="Label4" runat="server" Text="状态："></asp:Label>
            <asp:RadioButton ID="rbModMenuActive" runat="server" GroupName="ModMenuStatus" Text="启用" />
            <asp:RadioButton ID="rbModMenuDisActive" runat="server" GroupName="ModMenuStatus" Text="停用" />
            <br />
            
            <asp:Button ID="btMod" runat="server" Text="修改" OnClick="btMod_Click" />
        </panel>
        <panel>
            <h5>新增菜单</h5>
            <asp:Label ID="Label3" runat="server" Text="角色名："></asp:Label>
            <asp:TextBox ID="tbMenuName" runat="server"></asp:TextBox>
            <br />
            <asp:Label ID="Label5" runat="server" Text="状态："></asp:Label>
            <asp:RadioButton ID="rbMenuActive" runat="server" Checked="True" GroupName="RoleStatus" Text="启用" />
            <asp:RadioButton ID="rbMenuDisActive" runat="server" GroupName="RoleStatus" Text="停用" />
            <br />
            <asp:Button ID="btAdd" runat="server" Text="新增" onclick="btAdd_Click"/>
        </panel>
    </form>
</body>
</html>
