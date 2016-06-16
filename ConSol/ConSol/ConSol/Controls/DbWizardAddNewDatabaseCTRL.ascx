<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DbWizardAddNewDatabaseCTRL.ascx.cs" Inherits="ConSol.Controls.DbWizardAddNewDatabaseCTRL" %>
<h1>Add New Database</h1>
<div id="message" class="message" runat="server" visible="false" enableviewstate="false">
</div>
<p>
    <style type="text/css">
        .auto-style1 {
            font-weight: bold;
            text-align: right;
        }

        .auto-style2 {
            width: 21px;
        }
    </style>
    <table style="width: 50%;">
        <tr>
            <td class="auto-style1">Project Name</td>
            <td>
                <asp:TextBox Width="300" ID="txtProjectName" runat="server"></asp:TextBox>
                <%--</td>--%>
            <td class="auto-style2"></td>
        </tr>
        <tr>
            <td class="auto-style1">Server IP</td>
            <td>
                <asp:TextBox Width="300" ID="txtServerIP" runat="server"></asp:TextBox>
            </td>
            <td class="auto-style2">&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style1">Username</td>
            <td>
                <asp:TextBox Width="300" ID="txtUsername" runat="server"></asp:TextBox>
            </td>
            <td class="auto-style2">&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style1">Password</td>
            <td>
                <asp:TextBox Width="300" ID="txtPassword" TextMode="Password" runat="server"></asp:TextBox>
            </td>
            <td class="auto-style2">&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style1">Database</td>
            <td>
                <asp:TextBox Width="300" ID="txtDatabase" runat="server"></asp:TextBox>
            </td>
            <td class="auto-style2">&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style1">Port</td>
            <td>
                <asp:TextBox Width="300" ID="txtPort" runat="server"></asp:TextBox>
            </td>
            <td class="auto-style2">&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style1">&nbsp;</td>
            <td>
                <asp:CheckBox Width="300" ID="chkActive" Text="Is Active?" runat="server" />
            </td>
            <td class="auto-style2">&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>
                <asp:Button ID="BtnCommand" runat="server" Text="Save" OnClick="BtnCommand_Click" />
                &nbsp;
            <asp:Button ID="BtnCancel" runat="server" Text="Cancel" OnClick="BtnCancel_Click" />
            </td>
            <td class="auto-style2">&nbsp;</td>
        </tr>
    </table>
</p>
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