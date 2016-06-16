<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SetUserInRoleCTRL.ascx.cs" Inherits="ConSol.Controls.SetUserInRoleCTRL" %>
<div id="message" class="message" runat="server" visible="false" enableviewstate="false">
</div>
<table width="100%">
    <tr>
        <td style="width: 211px">
            <b>User Role Name</b></td>
        <td>
            <asp:Label skinid="hiLabel" ID="lblUsername" runat="server" Width="100%" /></td>
        <td style="width: 57px">
        </td>
    </tr>
    <tr>
        <td style="width: 211px">
        </td>
        <td>
        </td>
        <td style="width: 57px">
        </td>
    </tr>
    <tr>
        <td style="width: 211px">
            <b>Role Name</b></td>
        <td>
            <asp:DropDownList ID="ddlRoleName" Width="60%" runat="server" DataSourceID="SqlDataSource1"
                DataTextField="UserRoleName" DataValueField="UserRoleName">
            </asp:DropDownList><asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ApplicationServices %>"
                SelectCommand="getAllUserRoles" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
        </td>
        <td style="width: 57px">
        </td>
    </tr>
    <tr>
        <td style="width: 211px; height: 21px">
        </td>
        <td style="height: 21px">
            <asp:LinkButton ID="BtnCommand" runat="server" OnClick="BtnCommand_Click" Text="Set User To Role"></asp:LinkButton>
            <asp:PlaceHolder ID="plcModulesinRole" runat="server" />
        </td>
        <td style="width: 57px; height: 21px">
        </td>
    </tr>
</table>
