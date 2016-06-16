<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FilterByDataPointCTRL.ascx.cs" Inherits="ConSol.Controls.FilterByDataPointCTRL" %>
<cc2:msgBox ID="msgBox1" runat="server" Style="text-align: center" />
<table style="width:100%">
    <tr>
        <td style="width:20%">
            <asp:HiddenField ID="hfSearchCriteria" runat="server" />
            <strong>Database: &nbsp;</strong></td>
        <td>
            <asp:CheckBoxList ID="chkDatabase" RepeatColumns="3" runat="server" DataTextField="Name" DataValueField="Name" BorderStyle="None" AutoPostBack="True" OnSelectedIndexChanged="chkDatabase_SelectedIndexChanged"></asp:CheckBoxList>
        </td>
    </tr>   
    <tr>
        <td></td>
        <td>
            <asp:Button ID="BtnCommand" runat="server" Text="Search" OnClick="BtnCommand_Click" /></td>
    </tr>

</table>
<hr />
