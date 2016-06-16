<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DataCountCTRL.ascx.cs" Inherits="ConSol.Controls.DataCountCTRL" %>
<h1>Data Point Count</h1>
<table id="myTable" class="table table-striped table-bordered" cellspacing="0" width="100%">
    <thead>
        <tr>
            <th><b>NAME</b></th>
            <th><b>GENDER</b></th>
            <th><b>AGE</b></th>
            <th><b>TOWN</b></th>
            <th><b>MOBILE NUMBER</b></th>
            <th><b>OCCUPATION</b></th>
            <th><b>JOB STATUS</b></th>
            <th><b>EMAIL</b></th>
            <th><b>INDUSTRY</b></th>
            <th><b>STATE</b></th>
            <th><b>LGA</b></th>
        </tr>
    </thead>
    <asp:PlaceHolder ID="plhResultofSearch" runat="server"></asp:PlaceHolder>
</table>
<p>
    &nbsp;
</p>
<p>
    &nbsp;
</p>
<asp:Button ID="BtnCommand" runat="server" Text="Get Data Count" OnClick="BtnCommand_Click" />