<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ModuleGrid.ascx.cs" Inherits="ConSol.Controls.ModuleGrid" %>
<asp:GridView ID="VesselsList" runat="server" OnPageIndexChanging="VesselsList_PageIndexChanging"
    AutoGenerateColumns="False" AllowPaging="True" EmptyDataText="No record yet in this list"
    CellPadding="5" CellSpacing="5" CaptionAlign="Left" HorizontalAlign="Left" ForeColor="#333333"
    GridLines="None" BorderWidth="5" Width="100%">
    <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
    <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
    <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
    <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
    <AlternatingRowStyle BackColor="White" />
    <Columns>
        <asp:TemplateField>
            <ItemTemplate>
                <h1>
                    <%# Eval("ModuleName") %>
                </h1>
                <div style="text-align: right">
                    <asp:HyperLink ID="HyperLink3" runat="server" Text="Edit" NavigateUrl='<%# "~/p.aspx?p=TMS_SelfModule&action=1&moduleid=" + Eval("ModuleId") %>' />
                    <asp:HyperLink ID="HyperLink1" runat="server" SkinID="editlink" NavigateUrl='<%# "~/p.aspx?p=TMS_SelfModule&action=1&moduleid=" + Eval("ModuleId") %>' />
                </div>
                <asp:Image ID="image1" runat="server" ImageUrl='<%# "~/Resources/" +  Eval("ModuleImage") %>'
                    CssClass="photosmall" Width="150" Height="100" AlternateText="Write a short description of the image here. It will show if the image is not loaded. Non visual browsers and search engines will also read this text."
                    ToolTip="Users will see this text when they roll over this image. Non-visual browsers will read this text to blind users." />
                <p>
                    <%# Eval("ModuleDescription") %>
                </p>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
<br />
<asp:HyperLink ID="HyperLink2" runat="server" Text="Add New TMS_SelfModule Info" NavigateUrl="~/p.aspx?p=TMS_SelfModule&action=0" />