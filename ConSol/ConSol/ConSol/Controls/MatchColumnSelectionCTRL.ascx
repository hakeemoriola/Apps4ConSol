<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MatchColumnSelectionCTRL.ascx.cs" Inherits="ConSol.Controls.MatchColumnSelectionCTRL" %>
<h2>Match Column(s) Selected</h2>
<hr />
<asp:GridView ID="GridView1" Width="100%" runat="server" AutoGenerateColumns="false" OnRowCreated="GridView1_RowCreated">
    <Columns>
        <asp:TemplateField>
            <ItemTemplate>
                <asp:Label ID="lblDbId" runat="server" Text='<%# Eval("DbId") %>'></asp:Label>

            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemTemplate>
                <asp:Label ID="lblTableId" runat="server" Text='<%# Eval("TableId") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField>
            <ItemTemplate>
                <asp:Label ID="lblDBcolumn" runat="server" Text='<%# Eval("DBColumn") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemTemplate>
                <asp:DropDownList ID="ddlBaseDBColumn" DataValueField="BaseColumnName" DataTextField="BaseColumnName" runat="server"></asp:DropDownList>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
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


