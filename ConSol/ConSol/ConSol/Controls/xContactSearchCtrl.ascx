<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="xContactSearchCtrl.ascx.cs" Inherits="ConSol.Controls.xContactSearchCtrl" %>
<h1>x-Search</h1>

<table style="width: 100%;" class="table">
    <tr>
        <td style="width: 40%;"><b>Name</b></td>
        <td style="width: 60%;">
            <asp:TextBox ID="txtName" runat="server" Width="80%"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td><strong>Gender</strong></td>
        <td>
            <asp:DropDownList ID="ddlGender" runat="server" Width="80%">
                <asp:ListItem>-Select-</asp:ListItem>
                <asp:ListItem>Female</asp:ListItem>
                <asp:ListItem>Male</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td style="font-weight: 700"><strong>Phone No.</strong></td>
        <td>
            <asp:TextBox ID="txtPhoneNo" runat="server" Width="80%" OnTextChanged="txtPhoneNo_TextChanged"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td><strong>Age</strong></td>
        <td>
            <asp:TextBox ID="txtAge" runat="server" Width="80%"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td><strong>Occupation</strong></td>
        <td>
            <asp:TextBox ID="txtOccupation" runat="server" Width="80%"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td><strong>Job Status</strong></td>
        <td>
            <asp:DropDownList ID="ddlJobStatus" runat="server" Width="80%">
                <asp:ListItem>-Select-</asp:ListItem>
                <asp:ListItem>Employed</asp:ListItem>
                <asp:ListItem>UnEmployed</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td><b>Email</b></td>
        <td>
            <asp:TextBox ID="txtEmail" runat="server" Width="80%" Style="text-align: left"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td><strong>Town</strong></td>
        <td>
            <asp:TextBox ID="txtTown" runat="server" Width="80%"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td><b>State</b></td>
        <td>
            <asp:DropDownList ID="ddlState" runat="server" Width="80%" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" DataSourceID="SqlDataSource2" DataTextField="Name" DataValueField="Code" AutoPostBack="True">
                <asp:ListItem>-Select-</asp:ListItem>
            </asp:DropDownList>
            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ApplicationServices %>" SelectCommand="SELECT top 37 [Code]
      ,[Name]
  FROM [StateOptions]
"></asp:SqlDataSource>
        </td>
    </tr>
    <tr>
        <td><b>LGA</b></td>
        <td>
            <asp:DropDownList ID="ddlLGA" runat="server" Width="80%" DataSourceID="SqlDataSource1" DataTextField="LGA_Name" DataValueField="LGA_Name" OnSelectedIndexChanged="ddlLGA_SelectedIndexChanged">
                <asp:ListItem>-Select-</asp:ListItem>
            </asp:DropDownList>

            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ApplicationServices %>" SelectCommand="SELECT [ID], [LGA_Name] FROM [VxLGAS] WHERE ([StateId] = @StateId)">
                <SelectParameters>
                    <asp:ControlParameter ControlID="ddlState" Name="StateId" PropertyName="SelectedValue" Type="Int32" />
                </SelectParameters>
            </asp:SqlDataSource>
        </td>
    </tr>
</table>
