<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SessionReport.aspx.cs" Inherits="Black21.SessionReport1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="width: 746px; height: 443px">
            &nbsp;&nbsp;&nbsp;
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" CellSpacing="2" DataKeyNames="GameID" DataSourceID="SqlDataSource1" ForeColor="Black" HorizontalAlign="Center">
                <Columns>
                    <asp:BoundField DataField="GameID" HeaderText="GameID" InsertVisible="False" ReadOnly="True" SortExpression="GameID" />
                    <asp:BoundField DataField="PlayerTotal" HeaderText="PlayerTotal" SortExpression="PlayerTotal" />
                    <asp:BoundField DataField="DealerTotal" HeaderText="DealerTotal" SortExpression="DealerTotal" />
                    <asp:BoundField DataField="GameResult" HeaderText="GameResult" SortExpression="GameResult" />
                </Columns>
                <FooterStyle BackColor="#CCCCCC" />
                <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
                <RowStyle BackColor="White" />
                <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#808080" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#383838" />
            </asp:GridView>
            <br />
            <br />
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:BlackJackConnectionString5 %>" SelectCommand="SELECT * FROM [SessionReport]"></asp:SqlDataSource>
            <br />
            <br />
            <br />
            <br />
        </div>
    </form>
</body>
</html>
