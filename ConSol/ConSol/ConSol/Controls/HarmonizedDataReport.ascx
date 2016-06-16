<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HarmonizedDataReport.ascx.cs" Inherits="ConSol.Controls.HarmonizedDataReport" %>
<h1>Harmonized Data Report</h1>

<table style="width: 100%;" class="table">
    <tr>
        <th>&nbsp;</th>
        <th>Unique Record Total</th>
        <th></th>
        <th></th>
        <th></th>
    </tr>
    <asp:PlaceHolder ID="plhRecordCount" runat="server"></asp:PlaceHolder>
    <tr>
        <td></td>
        <td style="text-align: center">
            <asp:Label ID="Label3" runat="server" Style="text-align: center; font-size: x-large; font-weight: 700"></asp:Label>
        </td>
        <td></td>
        <td></td>
        <td style="text-align: center"></td>
    </tr>
</table>
<%--<hr />--%>
<br />
<asp:Button ID="BtnRefresh" runat="server" Text="Harmonized Data Counts" OnClick="BtnRefresh_Click" />