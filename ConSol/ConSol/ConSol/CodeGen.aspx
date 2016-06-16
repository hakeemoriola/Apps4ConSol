<%@ Page Title="" Language="C#" EnableEventValidation="false" ValidateRequest="false" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CodeGen.aspx.cs" Inherits="CodeGen" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">
    <style type="text/css">
        .auto-style1 {
            height: 26px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CBody" runat="Server">
    <table style="width: 100%">
        <tr>
            <td>GridView Name</td>
            <td>
                <asp:TextBox ID="txtGridViewName" runat="server"></asp:TextBox>
            </td>
            <td></td>
            <td>Object Name</td>
            <td>
                <asp:DropDownList ID="ddlType" AppendDataBoundItems="true" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
                    <asp:ListItem>-Select-</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>

        <tr>
            <td class="auto-style1">Data Namespace</td>
            <td class="auto-style1">
                <asp:TextBox ID="txtDataNamespace" runat="server" Enabled="False"></asp:TextBox>
            </td>
            <td class="auto-style1"></td>
            <td class="auto-style1">Data Provider ()</td>
            <td class="auto-style1">
                <asp:TextBox ID="txtObjectName" runat="server" Width="80%" Enabled="False"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td>&nbsp;</td>
            <td colspan="3">
                <asp:RadioButtonList RepeatLayout="Flow" RepeatDirection="Horizontal" ID="chkControlType" runat="server">
                    <asp:ListItem>View</asp:ListItem>
                    <asp:ListItem>Add</asp:ListItem>
                    <asp:ListItem>Edit</asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td>&nbsp;</td>
        </tr>

        <tr>
            <td>URL</td>
            <td>List</td>
            <td>&nbsp;</td>
            <td>Add</td>
            <td>Edit</td>
        </tr>

        <tr>
            <td>&nbsp;</td>
            <td>
                <asp:TextBox ID="txtListUrl" runat="server"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
            <td>
                <asp:TextBox ID="txtAddUrl" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="txtEditUrl" runat="server"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>Param</td>
        </tr>

        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>
                <asp:TextBox ID="txtParams" runat="server"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td>&nbsp;</td>
            <td>
                <asp:Button ID="BtnCommand" runat="server" Text="Generate" OnClick="BtnCommand_Click" />
            </td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>

        <tr>
            <td colspan="5">
                <asp:TextBox ID="txtOutput" TextMode="MultiLine" Width="100%" runat="server" Height="346px"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
    </table>
</asp:Content>