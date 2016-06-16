<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SearchByDatabaseCTRL.ascx.cs" Inherits="ConSol.Controls.SearchByDatabaseCTRL" %>
<cc2:msgBox ID="msgBox1" runat="server" Style="text-align: center" />

<table style="width: 100%">
    <tr>
        <td style="width: 20%">&nbsp;</td>
        <td>
            <asp:CheckBox ID="chkPickColumns" Text="Select Column(s)" runat="server" OnCheckedChanged="chkPickColumns_CheckedChanged" AutoPostBack="True" Checked="true" />
        </td>
        <td>
            <asp:CheckBox ID="chkMustHaveValue" Text="Must Have Value" runat="server" />
        </td>
    </tr>
</table>
<hr />
<table style="width: 100%">
    <tr>
        <td style="width: 20%"><strong>Columns</strong></td>
        <td>
            <asp:Panel ID="plnColumn" runat="server" Visible="true">
                <asp:CheckBoxList ID="chkDbColumns" BorderWidth="200" RepeatLayout="Table" runat="server" DataTextField="Name" DataValueField="Name" BorderStyle="None" AutoPostBack="True" Width="101px" OnSelectedIndexChanged="chkDbColumns_SelectedIndexChanged"></asp:CheckBoxList>
                <asp:LinkButton ID="lnkBtnchkAll" runat="server" OnClick="lnkBtnchkAll_Click">Check All</asp:LinkButton>
                &nbsp;<asp:LinkButton ID="lnkBtnUnchkAll" runat="server" OnClick="lnkBtnUnchkAll_Click">Uncheck All</asp:LinkButton>
            </asp:Panel>
        </td>
    </tr>
</table>
<hr />
<table style="width: 100%">
    <tr>
        <td style="width: 20%">
            <asp:HiddenField ID="hfSearchCriteria" runat="server" />
            <strong>Database: &nbsp;</strong></td>
        <td>
            <asp:CheckBoxList ID="chkDatabase" RepeatColumns="3" runat="server" DataTextField="Name" DataValueField="Id" BorderStyle="None" AutoPostBack="True" OnSelectedIndexChanged="chkDatabase_SelectedIndexChanged"></asp:CheckBoxList>
        </td>
    </tr>
</table>
<hr />
<table style="width: 100%">
    <tr>
        <td style="width: 20%">&nbsp;</td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td></td>
        <td>
            <asp:Button ID="BtnCommand" runat="server" Text="Search" OnClick="BtnCommand_Click" />
            <asp:Label ID="Label1" runat="server"></asp:Label>
            <asp:Label ID="Label2" runat="server"></asp:Label>
        </td>
    </tr>

</table>
<hr />
