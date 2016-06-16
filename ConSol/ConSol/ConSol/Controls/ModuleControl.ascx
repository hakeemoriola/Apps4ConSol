<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ModuleControl.ascx.cs" Inherits="ConSol.Controls.ModuleControl" %>
<div id="message" class="message" runat="server" visible="false" enableviewstate="false">
</div>
<table width="100%">
    <tbody>
        <tr>
            <td style="width: 20%; height: 26px;">
                <strong>TMS_SelfModule ID</strong></td>
            <td style="width: 80%">
                <asp:TextBox ID="txtModuleID" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 20%">
                <strong>TMS_SelfModule Name</strong></td>
            <td style="width: 80%">
                <asp:TextBox ID="txtModuleName" runat="server" Width="70%"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 20%"></td>
            <td style="width: 80%">
                <asp:Image ID="imagepath" runat="server" Height="100" Width="150" /></td>
        </tr>
        <tr>
            <td style="width: 20%">
                <strong>TMS_SelfModule Image</strong></td>
            <td style="width: 80%">
                <asp:FileUpload ID="moduleImage" runat="server" Width="396px" /></td>
        </tr>
        <tr>
            <td style="width: 20%">
                <strong>TMS_SelfModule Description</strong></td>
            <td style="width: 80%">
                <asp:TextBox ID="txtModuleDescription" runat="server" TextMode="MultiLine" Width="80%" />
            </td>
        </tr>
        <tr>
            <td style="height: 21px; width: 20%;"></td>
            <td style="width: 80%">
                <asp:LinkButton ID="BtnCommand" runat="server" Text="Add TMS_SelfModule Info" OnClick="BtnCommand_Click" />
            </td>
        </tr>
    </tbody>
</table>