<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PickOrEditTableCTRL.ascx.cs" Inherits="ConSol.Controls.PickOrEditTableCTRL" %>
<h2>Pick/Edit a Table</h2>
<div id="message" class="message" runat="server" visible="false" enableviewstate="false">
</div>
<hr />
<asp:RadioButtonList ID="rlbTable" DataValueField="Name" DataTextField="Name" RepeatColumns="1" RepeatLayout="Flow" runat="server" OnSelectedIndexChanged="rlbTable_SelectedIndexChanged" AutoPostBack="True"></asp:RadioButtonList>
<br />
<br />
<hr />
<h2>Top 10 rows</h2>
<hr />
<asp:GridView ID="GridView1" Width="100%" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None">
    <AlternatingRowStyle BackColor="White" />
    <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
    <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
    <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
    <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
    <SortedAscendingCellStyle BackColor="#FDF5AC" />
    <SortedAscendingHeaderStyle BackColor="#4D0000" />
    <SortedDescendingCellStyle BackColor="#FCF6C0" />
    <SortedDescendingHeaderStyle BackColor="#820000" />
</asp:GridView>
<br />
<br />
<asp:Button ID="BtnSave" runat="server" Text="Save" OnClick="BtnSave_Click" />
&nbsp;
<asp:Button ID="BtnCancel" runat="server" Text="Cancel" OnClick="BtnCancel_Click" />
&nbsp;<table style="width: 100%">
    <tr>
        <td></td>
        <td></td>
        <td style="text-align: right">
            <asp:Button ID="btnStart" runat="server" Text="Next" Width="56px" OnClick="btnStart_Click" /></td>
    </tr>
</table>
