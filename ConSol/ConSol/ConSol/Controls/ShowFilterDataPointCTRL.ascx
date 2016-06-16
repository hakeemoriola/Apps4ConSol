<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ShowFilterDataPointCTRL.ascx.cs" Inherits="ConSol.Controls.ShowFilterDataPointCTRL" %>
<h2>Filtered Search</h2>
<hr />
<asp:UpdatePanel ID="upGrdCustomers" runat="server" UpdateMode="Always">
    <ContentTemplate>
        <asp:HiddenField ID="hfSearchCriteria" runat="server" />

        <asp:Panel ID="pnlGrdCustomers" runat="server" ScrollBars="Auto" Width="100%" Height="352px" CssClass="srcColor">
            <cc1:GridView ID="gvCustomers" runat="server" AutoGenerateColumns="true" AllowSorting="True" Width="100%"
                CssClass="tablestyle" OnDataBound="gvCustomers_DataBound" DataSourceID="odsCustomers" AllowPaging="True">
                <AlternatingRowStyle CssClass="table table-striped" />
                <HeaderStyle CssClass="headerstyle" />
                <RowStyle CssClass="rowstyle" Wrap="false" />

                <EmptyDataRowStyle BackColor="#edf5ff" Height="300px" VerticalAlign="Middle" HorizontalAlign="Center" />
                <EmptyDataTemplate>
                    No Records Found
                </EmptyDataTemplate>

                <Columns>
                   <%-- <asp:BoundField HeaderText="NAME" DataField="NAME" />
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
                    <asp:BoundField HeaderText="Source" DataField="Source" />--%>
                </Columns>
                <PagerTemplate>
                    <table width="100%">
                        <tr>
                            <td style="text-align: left">Page Size: 
                                    <asp:DropDownList ID="ddPageSize" runat="server" EnableViewState="true" OnSelectedIndexChanged="ddPageSize_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Text="100"></asp:ListItem>
                                        <asp:ListItem Text="250"></asp:ListItem>
                                        <asp:ListItem Text="500"></asp:ListItem>
                                    </asp:DropDownList>
                            </td>
                            <td style="text-align: right">
                                <asp:Label ID="lblPageCount" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </PagerTemplate>
            </cc1:GridView>
        </asp:Panel>


        <div style="margin-top: 5px">
            <asp:DataPager ID="pager" runat="server" PagedControlID="gvCustomers">
                <Fields>
                    <asp:NextPreviousPagerField FirstPageText="&lt;&lt;" LastPageText="&gt;&gt;"
                        NextPageText="&gt;" PreviousPageText="&lt;" ShowFirstPageButton="True"
                        ShowNextPageButton="False" ButtonCssClass="datapager" />
                    <asp:NumericPagerField ButtonCount="10" NumericButtonCssClass="datapager" CurrentPageLabelCssClass="datapager" />
                    <asp:NextPreviousPagerField LastPageText="&gt;&gt;" NextPageText="&gt;"
                        ShowLastPageButton="True" ShowPreviousPageButton="False" ButtonCssClass="datapager" />
                </Fields>
            </asp:DataPager>
        </div>
        <br />
        <asp:ObjectDataSource ID="odsCustomers" runat="server"
            SelectMethod="GetFilteredData" TypeName="ConSol.DAL.AdvWorksDB"
            EnablePaging="True" SelectCountMethod="GetFilteredCount"
            SortParameterName="sortExpression">
            <SelectParameters>
                <asp:ControlParameter ControlID="hfSearchCriteria" Name="searchCriteria" Direction="Input" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </ContentTemplate>
</asp:UpdatePanel>

<asp:Button ID="BtnRefine" runat="server" Text="Refine Search" OnClick="BtnRefine_Click" />&nbsp;
<asp:Button ID="BtnExportToExcel" runat="server" Text="Export to Excel" OnClick="BtnExportToExcel_Click" />
