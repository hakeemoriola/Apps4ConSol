<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewDataForCountCTRL.ascx.cs" Inherits="ConSol.Controls.ViewDataForCountCTRL" %>
<h1>Data Point Harmonized</h1>
<hr />
<div>
    PageSize:
        <asp:DropDownList ID="ddlPageSize" runat="server" AutoPostBack="true" OnSelectedIndexChanged="PageSize_Changed">
            <asp:ListItem Text="500" Value="500" />
            <asp:ListItem Text="1000" Value="1000" />
            <asp:ListItem Text="5000" Value="5000" />
        </asp:DropDownList>
    <hr />
    <asp:GridView ID="GridView1" CssClass="table table-striped table-bordered" runat="server" AutoGenerateColumns="false" Width="100%">
        <Columns>
            <asp:BoundField HeaderText="RowNumber" DataField="RowNumber" />
            <asp:BoundField HeaderText="NAME" DataField="NAME" />
            <asp:BoundField HeaderText="GENDER" DataField="GENDER" />
            <asp:BoundField HeaderText="AGE" DataField="AGE" />
            <asp:BoundField HeaderText="ADDRESS" DataField="ADDRESS" />
            <asp:BoundField HeaderText="TOWN" DataField="TOWN" />
            <asp:BoundField HeaderText="MOBILENUMBER1" DataField="MOBILENUMBER1" />
            <asp:BoundField HeaderText="MOBILENUMBER2" DataField="MOBILENUMBER2" />
            <asp:BoundField HeaderText="OCCUPATION" DataField="OCCUPATION" />
            <asp:BoundField HeaderText="JOBSTATUS" DataField="JOBSTATUS" />
            <asp:BoundField HeaderText="EMAIL" DataField="EMAIL" />
            <asp:BoundField HeaderText="INDUSTRY" DataField="INDUSTRY" />
            <asp:BoundField HeaderText="STATE" DataField="STATE" />
            <asp:BoundField HeaderText="Source" DataField="Source" />
        </Columns>
    </asp:GridView>
    <asp:Label ID="Label1" runat="server"></asp:Label>
    <asp:Repeater Visible="false" ID="rptPager" runat="server" OnItemCommand="rptPager_ItemCommand">
        <ItemTemplate>
            <asp:LinkButton ID="lnkPage" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>' Enabled='<%# Eval("Enabled") %>' OnClick="Page_Changed"></asp:LinkButton>
        </ItemTemplate>
    </asp:Repeater>
    <div style="text-align: right">
        <asp:Button ID="btnFirst" runat="server" Text="First" OnClick="btnFirst_Click" />
        &nbsp;
        <asp:Button ID="btnPrevious" runat="server" Text="Previous" OnClick="btnPrevious_Click" />
        &nbsp;
        <asp:Button ID="btnNext" runat="server" Text="Next" OnClick="btnNext_Click" />
        &nbsp;
        <asp:Button ID="btnLast" runat="server" Text="Last" OnClick="btnLast_Click" />
        &nbsp;
    </div>
</div>