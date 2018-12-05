<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Informe.aspx.cs" Inherits="Presentacion.Supervisor.Informe" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Informe</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <h2>Informe Empresa</h2>
        <p>&nbsp;</p>
        <p>
            <asp:Label ID="lblNombreEmpresa" runat="server" Text="Nombre Empresa:" Font-Bold="True"></asp:Label> &nbsp;<asp:Label ID="txtNombreEmpresa" runat="server"></asp:Label>
&nbsp;</p>
        <p>
            <asp:Label ID="lblFecha" runat="server" Text="Fecha: " Font-Bold="True"></asp:Label> <asp:Label ID="txtFecha" runat="server"></asp:Label>
        </p>
        <p>
            &nbsp;</p>
        <p>
            <asp:GridView ID="gvEmpresaInfo"  runat="server" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" Height="230px" Width="689px">
                <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F7F7F7" />
                <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                <SortedDescendingCellStyle BackColor="#E5E5E5" />
                <SortedDescendingHeaderStyle BackColor="#242121" />
            </asp:GridView>
        </p>
    </div>
    </form>
</body>
</html>
