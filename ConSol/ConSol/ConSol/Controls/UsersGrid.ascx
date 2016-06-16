<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UsersGrid.ascx.cs" Inherits="ConSol.Controls.UsersGrid" %>
<asp:GridView ID="UsersList" runat="server" AutoGenerateColumns="False" AllowPaging="True"
    EmptyDataText="No record yet in this list" CellPadding="5" CellSpacing="5" CaptionAlign="Left"
    HorizontalAlign="Left" OnPageIndexChanging="UsersList_PageIndexChanging" ForeColor="#333333" GridLines="None" BorderWidth="5" Width="100%">
    <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
    <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
    <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
    <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
    <AlternatingRowStyle BackColor="White" />
    <Columns>
        <asp:BoundField DataField="UserName" HeaderText="UserName" ReadOnly="True" SortExpression="UserName" />
        <asp:TemplateField>
            <ItemTemplate>
                <asp:Label ID="lblFullName" runat="server" Text='<%#  Eval("Title") + " "  + Eval("FirstName") + " " + Eval("LastName") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <%--        <asp:BoundField DataField="UserRole" HeaderText="User Role" SortExpression="UserRole" />
        <asp:BoundField DataField="WebTheme" HeaderText="WebTheme" SortExpression="WebTheme" />--%>
        <asp:TemplateField>
            <ItemTemplate>
                <asp:HyperLink ID="HyperLink1" runat="server" Text="Edit User" NavigateUrl='<%# "~/p.aspx?p=edit-user&username=" + Eval("UserName") %>' />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemTemplate>
                <asp:HyperLink ID="HyperLink1" runat="server" Text="Edit User In Roles" NavigateUrl='<%# "~/p.aspx?p=set-user-in-role&username=" + Eval("UserName") %>' />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
<br />
<asp:HyperLink ID="HyperLink2" runat="server" Text="Add New User" NavigateUrl="~/p.aspx?p=add-new-user" />