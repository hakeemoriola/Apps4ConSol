<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SettingsControl.ascx.cs" Inherits="ConSol.Controls.SettingsControl" %>
<style type="text/css">
    .auto-style1 {
        height: 61px;
    }
</style>
<table style="width: 100%">
    <tr>
        <td>
            <asp:HyperLink ID="HyperLink3" NavigateUrl="~/p.aspx?p=users" runat="server"><h4>Users</h4></asp:HyperLink>
            <p>
                Allows you to set roles for Users in the system.
            </p>
        </td>
        <td>
            <asp:HyperLink ID="HyperLink1" NavigateUrl="~/p.aspx?p=userrole" runat="server"><h4>Permissions</h4></asp:HyperLink>
            <p>
                Allows you to set permissions for TMS_SelfModule and TMS_SelfModule actions in the system.
            </p>
        </td>
    </tr>
    <tr>
        <td class="auto-style1">
            <asp:HyperLink ID="HyperLink13" runat="server" NavigateUrl="~/p.aspx?p=dm-wizard-start"><h4>Database Management Wizard</h4></asp:HyperLink>
            <p>
                This wizard allows you to add or modify database setting in the system.
            </p>
        </td>
        <td class="auto-style1">&nbsp;</td>
    </tr>
    <tr>
        <td class="auto-style1">&nbsp;</td>
        <td class="auto-style1">&nbsp;</td>
    </tr>
    <tr>
        <td class="auto-style1">
            <asp:HyperLink ID="HyperLink10" NavigateUrl="~/p.aspx?p=db-man" runat="server"><h4>Database Management</h4></asp:HyperLink>
            <p>
                Allows you to add and modify employee detail in the system.
            </p>
        </td>
        <td class="auto-style1">
            <asp:HyperLink ID="HyperLink2" NavigateUrl="~/p.aspx?p=base-column-man" runat="server"><h4>Base Columns Management</h4></asp:HyperLink>
            <p>
                Allows you to add and modify employee detail in the system.
            </p>
        </td>
    </tr>
    <tr>
        <td>
            <asp:HyperLink ID="HyperLink11" runat="server" NavigateUrl="~/p.aspx?p=db-meta-tables"><h4>Tables Management</h4></asp:HyperLink>
            <p>
                Allows you to add and modify employee detail in the system.
            </p>
        </td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td>
            <asp:HyperLink ID="HyperLink12" runat="server" NavigateUrl="~/p.aspx?p=db-meta-columns"><h4>Column Management</h4></asp:HyperLink>
            <p>
                Allows you to add and modify employee detail in the system.
            </p>
        </td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
    </tr>
</table>