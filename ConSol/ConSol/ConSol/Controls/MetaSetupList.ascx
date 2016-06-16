<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MetaSetupList.ascx.cs" Inherits="ConSol.Controls.MetaSetupList" %>
<asp:GridView ID="grdMetaSetup" runat="server" CssClass="table" Width="100%" AutoGenerateColumns="False" DataKeyNames="Id" DataSourceID="SqlDataSource1">
    <Columns>
        <asp:BoundField DataField="Id" HeaderText="Id" InsertVisible="False" ReadOnly="True" SortExpression="Id" />
        <asp:BoundField DataField="ProjectName" HeaderText="ProjectName" SortExpression="ProjectName" />
        <asp:BoundField DataField="Database" HeaderText="Database" SortExpression="Database" />
        <asp:TemplateField>
            <ItemTemplate>
                <asp:HyperLink ID="HyperLink1" runat="server" Text="Edit Setup" NavigateUrl='<%# "~/p.aspx?p=configure-meta&action=1&Id=" + Eval("Id") %>' />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ApplicationServices %>" SelectCommand="SELECT [Id], [ProjectName], [Database] FROM [ConSettings]"></asp:SqlDataSource>
<br />
<%--<asp:HyperLink ID="HyperLink1" NavigateUrl="~/p.aspx?p=add-metadata-setup" runat="server">Add New DB Setup</asp:HyperLink>--%>