<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DbWizardChooseActionCTRL.ascx.cs" Inherits="ConSol.Controls.DbWizardChooseActionCTRL" %>
<div id="message" class="message" runat="server" visible="false" enableviewstate="false">
</div>
<h1>Choose Database Action</h1>
<p>
    <asp:RadioButtonList ID="rblAction" AutoPostBack="True" runat="server" OnSelectedIndexChanged="rblAction_SelectedIndexChanged">
        <asp:ListItem Value="0">Add new database</asp:ListItem>
        <asp:ListItem Value="1">Modify an existing database</asp:ListItem>
    </asp:RadioButtonList>
</p>

<asp:Panel Visible="false" ID="pnlDatabases" runat="server">
    <table>
        <tr>
            <td>Database</td>
            <td>
                <asp:DropDownList ID="ddlDatabase" runat="server" AutoPostBack="True" DataSourceID="SqlDataSource1" DataTextField="ProjectName" DataValueField="Id"></asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ApplicationServices %>" SelectCommand="SELECT [Id], [ProjectName] FROM [ConSettings]"></asp:SqlDataSource>
            </td>
        </tr>
    </table>
</asp:Panel>

<p>
    <table style="width: 100%">
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td style="text-align: right">&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td style="text-align: right">&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td style="text-align: right">&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td style="text-align: right">&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td style="text-align: right">&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td style="text-align: right">&nbsp;</td>
        </tr>
        <tr>
            <td></td>
            <td></td>
            <td style="text-align: right">
                <asp:Button ID="btnStart" runat="server" Text="Next" Width="56px" OnClick="btnStart_Click" /></td>
        </tr>
    </table>
</p>