<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ApplicationDataReportedCTRL.ascx.cs" Inherits="ConSol.Controls.ApplicationDataReportedCTRL" %>
<style type="text/css">
    .auto-style1 {
        text-align: center;
    }
</style>
<h1>Raw Data Report (<em>Logged not Real-time</em>)</h1>
<hr />
<asp:Label ID="lblMessage" runat="server"></asp:Label>
<div style="width: 100%; text-align: right">
</div>
<hr />
<table style="width: 100%;" class="table">
    <tr>
        <th>Id</th>
        <th>IP Address</th>
        <th>Database Name</th>
        <th>Table Name</th>
        <th>Total Record Count</th>
        <th>Total Unique Count</th>
        <th></th>
    </tr>
    <asp:PlaceHolder ID="plhRecordCount" runat="server"></asp:PlaceHolder>
    <tr>
        <td></td>
        <td></td>
        <td></td>
        <td></td>
        <td style="text-align: center">
            <strong>
                <asp:Label ID="lblTotalRecord" runat="server"></asp:Label>
            </strong></td>
        <td class="auto-style1"><strong>
            <asp:Label ID="lblTotalUnique" runat="server"></asp:Label>
        </strong></td>
        <td></td>
    </tr>
</table>
<%--<hr />--%>
<br />

<script src="/scripts/app/app.js" type="text/javascript"></script>