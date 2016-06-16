<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DbWizardSetDColumnCTRL.ascx.cs" Inherits="ConSol.Controls.DbWizardSetDColumnCTRL" %>
<h2>Distinct Column</h2>
<hr />
<asp:RadioButtonList ID="RadioButtonList1" DataTextField="BaseDbColumn" DataValueField="BaseDbColumn" runat="server"></asp:RadioButtonList>
<hr />
<asp:Button ID="BtnSave" runat="server" Text="Save" OnClick="BtnSave_Click" />
&nbsp;
<asp:Button ID="BtnCancel" runat="server" Text="Cancel" OnClick="BtnCancel_Click" />
<hr />
<table style="width: 100%">
    <tr>
        <td></td>
        <td></td>
        <td style="text-align: right">
            <asp:Button ID="btnStart" runat="server" Text="Finish" Width="56px" OnClick="btnStart_Click" /></td>
    </tr>
</table>


