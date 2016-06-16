<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HarmonizedViewCTRL.ascx.cs" Inherits="ConSol.Controls.HarmonizedViewCTRL" %>
<link href="../App_Themes/css/Site.css" rel="stylesheet" type="text/css" />
<h1>Harmonized View</h1>
<hr />

<div>
    <asp:UpdatePanel ID="upGrdCustomers" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <asp:Panel ID="pnlSearch" runat="server">
                <table style="background-color: #C7CFF7; border-width: 3px; border-style: solid; margin: 0px 20px 0px 0px;">
                    <tr>
                        <td colspan="5">
                            <asp:Label ID="lblSearchError" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Search Column</td>
                        <td align="left">Search Criteria</td>
                        <td align="left">Search Value</td>
                        <td></td>
                        <td></td>
                    </tr>

                    <tr>
                        <td>
                            <asp:HiddenField ID="hfSearchCriteria" runat="server" />
                            <asp:DropDownList ID="ddSearchField" Width="150px" runat="server">
                                <asp:ListItem Text="Customer Key" Value="customerkey"></asp:ListItem>
                                <asp:ListItem Text="City" Value="City"></asp:ListItem>
                                <asp:ListItem Text="Customer Alternate Key" Value="CustomerAlternateKey"></asp:ListItem>
                                <asp:ListItem Text="Title" Value="Title"></asp:ListItem>
                                <asp:ListItem Text="First Name" Value="FirstName"></asp:ListItem>
                                <asp:ListItem Text="Last Name" Value="LastName"></asp:ListItem>
                                <asp:ListItem Text="Birth Date" Value="BirthDate"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddSearchCriteria" runat="server">
                                <asp:ListItem Text="Contains"></asp:ListItem>
                                <asp:ListItem Text="DoesNotContain"></asp:ListItem>
                                <asp:ListItem Text="EqualTo"></asp:ListItem>
                                <asp:ListItem Text="NotEqualTo"></asp:ListItem>
                                <asp:ListItem Text="GreaterThan"></asp:ListItem>
                                <asp:ListItem Text="LessThan"></asp:ListItem>
                                <asp:ListItem Text="GreaterThanOrEqualTo"></asp:ListItem>
                                <asp:ListItem Text="LessThanOrEqualTo"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSearchValue" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button ID="btnSearch" CssClass="searchbtn" runat="server" OnClick="btnSearch_Click" Text="Search" />
                        </td>
                        <td>
                            <asp:Button ID="btnReload" CssClass="reloadbtn" runat="server" OnClick="btnReload_Click" Text="Reload" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="pnlGrdCustomers" runat="server" ScrollBars="Auto" Width="100%" Height="352px" CssClass="srcColor">
                <cc1:GridView ID="gvCustomers" runat="server" AutoGenerateColumns="False" AllowSorting="True"
                    CssClass="tablestyle" OnDataBound="gvCustomers_DataBound" DataSourceID="odsCustomers" AllowPaging="True">
                    <AlternatingRowStyle CssClass="altrowstyle" />
                    <HeaderStyle CssClass="headerstyle" />
                    <RowStyle CssClass="rowstyle" Wrap="false" />

                    <EmptyDataRowStyle BackColor="#edf5ff" Height="300px" VerticalAlign="Middle" HorizontalAlign="Center" />
                    <EmptyDataTemplate>
                        No Records Found
                    </EmptyDataTemplate>

                    <Columns>
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
                SelectMethod="GetCustomersSortedPage" TypeName="ConSol.DAL.AdvWorksDB"
                EnablePaging="True" SelectCountMethod="GetCustomersCount"
                SortParameterName="sortExpression">
                <SelectParameters>
                    <asp:ControlParameter ControlID="hfSearchCriteria" Name="searchCriteria" Direction="Input" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>