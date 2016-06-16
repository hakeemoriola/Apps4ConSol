<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OSearchCTRL.ascx.cs" Inherits="ConSol.Controls.OSearchCTRL" %>
<asp:Panel ID="pnlSearch" runat="server" >
            <table style="background-color:#C7CFF7; border-width:3px; border-style:solid; margin: 0px 20px 0px 0px;">
                <tr>
                    <td colspan="5">
                        <asp:Label ID="lblSearchError" runat="server" ForeColor="Red" ></asp:Label>
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
                    <td><asp:HiddenField ID="hfSearchCriteria" runat="server" />
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
                            <asp:ListItem Text= "Contains" ></asp:ListItem>                            
                            <asp:ListItem Text= "DoesNotContain" ></asp:ListItem>                            
                            <asp:ListItem Text="EqualTo" ></asp:ListItem>
                            <asp:ListItem Text= "NotEqualTo" ></asp:ListItem>                            
                            <asp:ListItem Text= "GreaterThan"></asp:ListItem>                            
                            <asp:ListItem Text= "LessThan" ></asp:ListItem>                            
                            <asp:ListItem Text= "GreaterThanOrEqualTo" ></asp:ListItem>                            
                            <asp:ListItem Text= "LessThanOrEqualTo"></asp:ListItem>                            
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:TextBox ID="txtSearchValue" runat="server" ></asp:TextBox> 
                    </td>
                    <td>
                        <asp:Button ID="btnSearch" CssClass="searchbtn" runat="server"  OnClick="btnSearch_Click" Text="Search" />
                    </td>
                    <td>
                        <asp:Button ID="btnReload" CssClass="reloadbtn" runat="server"  OnClick="btnReload_Click" Text="Reload" />
                    </td>
                </tr>
                </table>
            </asp:Panel>