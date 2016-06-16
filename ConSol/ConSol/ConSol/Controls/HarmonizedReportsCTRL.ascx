<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HarmonizedReportsCTRL.ascx.cs" Inherits="ConSol.Controls.HarmonizedReportsCTRL" %>
<h1>Unique Data Report</h1>
<hr />
<div style="width: 100%; text-align: right">
    <strong>
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/p.aspx?p=harmonized-data-report">Click Here For Harmonized Data Report</asp:HyperLink></strong>
</div>
<hr />
<table style="width: 100%;" class="table">
    <tr>
        <th>Id</th>
        <th>IP Address</th>
        <th>Database Name</th>
        <th>Table Name</th>
        <th>Total Record Count</th>
    </tr>
    <asp:PlaceHolder ID="plhRecordCount" runat="server"></asp:PlaceHolder>
    <tr>
        <td></td>
        <td></td>
        <td></td>
        <td></td>
        <td style="text-align: center">
            <asp:Label ID="Label1" runat="server" Style="text-align: center; font-size: x-large; font-weight: 700"></asp:Label>
        </td>
    </tr>
</table>
<%--<hr />--%>
<br />
<asp:Button ID="BtnRefresh" runat="server" Text="Get Raw Data Counts" OnClick="BtnRefresh_Click" />