<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdministrarEmpresas.aspx.cs" Inherits="Presentacion.Administrador.AdministrarEmpresas" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Sistema - Empresa</title>
    <!-- Bootstrap Styles-->
    <link href="../assets/css/bootstrap.css" rel="stylesheet" />
    <!-- FontAwesome Styles-->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />
    <!-- Morris Chart Styles-->
    <link href="../assets/js/morris/morris-0.4.3.min.css" rel="stylesheet" />
    <!-- Custom Styles-->
    <link href="../assets/css/custom-styles.css" rel="stylesheet" />
    <!-- Google Fonts-->
    <link href='http://fonts.googleapis.com/css?family=Open+Sans' rel='stylesheet' type='text/css' />
    <!-- TABLE STYLES-->
    <link href="../assets/js/dataTables/dataTables.bootstrap.css" rel="stylesheet" />
        <!-- jQuery Js -->
    <script src="../assets/js/jquery-1.10.2.js"></script>
    <!-- Bootstrap Js -->
    <script src="../assets/js/bootstrap.min.js"></script>
    <!-- hide column table -->
    <link href="../assets/css/hidecolumn.css" rel="stylesheet" />

</head>

<body>
    <form id="form1" runat="server">
    <div id="wrapper">
        <nav class="navbar navbar-default top-navbar" role="navigation">
            <div class="navbar-header">
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".sidebar-collapse">
                    <span class="sr-only">Cambiar menú</span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>
                <a class="navbar-brand" href="index.aspx">SAFE</a>
            </div>

            <ul class="nav navbar-top-links navbar-right">

                <!-- /.dropdown -->
                <li class="dropdown">
                    <a class="dropdown-toggle" data-toggle="dropdown" href="#" aria-expanded="false">
                        <asp:Label ID="lblNombreUs" runat="server" Text="Label"></asp:Label>  <i class="fa fa-user fa-fw"></i> <i class="fa fa-caret-down"></i>
                    </a>
                    <ul class="dropdown-menu dropdown-user">
                        <li><a href="#"><i class="fa fa-user fa-fw"></i> Perfil</a>
                        </li>
                        <li><a href="#"><i class="fa fa-gear fa-fw"></i> Configuración</a>
                        </li>
                        <li class="divider"></li>
                        <li><a href="../Login.aspx?close=1"><i class="fa fa-sign-out fa-fw"></i> Cerrar sesión</a>
                        </li>
                    </ul>
                    <!-- /.dropdown-user -->
                </li>
                <!-- /.dropdown -->
            </ul>
        </nav>
        <!--/. NAV TOP  -->
        <nav class="navbar-default navbar-side" role="navigation">
            <div class="sidebar-collapse">
                <ul class="nav" id="main-menu">

                    <li>
                        <a href="index.aspx"><i class="fa fa-home"></i> Principal</a>
                    </li>
                    <li>
                            <a href="#"><i class="fa fa-group"></i> Usuarios<span class="fa arrow"></span></a>
                            <ul class="nav nav-second-level">
                                <li>
                                    <a href="agregarUsuario.aspx">Añadir usuario</a>
                                </li>
                                <li>
                                    <a href="administrarUsuarios.aspx">Lista usuarios</a>
                                </li>
                            </ul>
                        </li>
                    <li>
                            <a class="active-menu" href="#"><i class="fa fa-building-o"></i> Empresas<span class="fa arrow"></span></a>
                            <ul class="nav nav-second-level collapse in">
                                <li>
                                    <a href="agregarEmpresa.aspx">Añadir empresa</a>
                                </li>
                                <li>
                                     <a href="administrarEmpresas.aspx">Lista empresas</a>
                                </li>
                            </ul>
                    </li>
                      <li>
                            <a href="tiposdeEvaluacion.aspx"><i class="fa fa-list-alt"></i> Tipos de Evaluación</a>
                    </li>
                </ul>

            </div>

        </nav>
        <!-- /. NAV SIDE  -->
        <div id="page-wrapper">
            <div id="page-inner">


                 <div class="row">
                    <div class="col-md-12">
                        <h1 class="page-header">
                            <small><a id="btnVolver" href="#">Volver</a></small>
                        </h1>
                    </div>
                </div>
                <div class="row">
                        <div class="col-md-12">
                            <!-- Advanced Tables -->
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                     Empresas
                                </div>
                                <div class="panel-body">
                                    <div class="table-responsive">  
                                     <asp:Label ID="lblAlerta" runat="server" Visible="False" Font-Bold="True" ForeColor="Red"></asp:Label>
                                        <br />
                                        <br />
                                        <asp:GridView class="table table-striped table-bordered table-hover" ID="gvEmpresas" runat="server" EmptyDataText="No se han encontrado resultados." Visible="False" AutoGenerateColumns="False" DataKeyNames="RUT" OnRowCommand="gvEmpresas_RowCommand" OnRowDataBound="gvEmpresas_RowDataBound">
                                        <Columns>
                                             <asp:BoundField DataField="RUT" HeaderText="Rut" SortExpression="RUT" />
                                             <asp:BoundField AccessibleHeaderText="NOMBRE" DataField="NOMBRE" HeaderText="Nombre" SortExpression="NOMBRE" />
                                             <asp:BoundField AccessibleHeaderText="CORREO" DataField="CORREO" HeaderText="Correo" SortExpression="CORREO" />
                                             <asp:BoundField AccessibleHeaderText="NUMERO" DataField="NUMERO" HeaderText="Telefono" SortExpression="NUMERO" />
                                             <asp:BoundField AccessibleHeaderText="COMUNA" DataField="COMUNA" HeaderText="Comuna" SortExpression="COMUNA" />
                                            <asp:BoundField AccessibleHeaderText="HABILITADA" DataField="HABILITADA" HeaderText="Estado" SortExpression="HABILITADA" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn" />
                                             <asp:TemplateField headertext="">
                                                            <ItemTemplate>
                                                                <asp:LinkButton CommandName="Modificar" CommandArgument='<%# Eval("RUT") %>' class="btn btn-info" runat="server"><i class="fa fa-edit"></i> Modificar</asp:LinkButton>
                                                                <asp:Button ID="btnHabilitar" runat="server" text="" CssClass="" CommandName="Deshabilitar" CommandArgument='<%# Eval("RUT") %>' OnClientClick="return Confirmar();" />
                                                                 </ItemTemplate>
                                                        </asp:TemplateField> </Columns>
                                        </asp:GridView>
                                    </div>
                                    
                                </div>
                            </div>
                            <!--End Advanced Tables -->
                        </div>
                                    <!-- Modal -->
  <div class="modal fade" id="myModal" role="dialog">
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="modal-header">
          <h4 class="modal-title"> Modificar Datos</h4>
        </div>
        <div class="modal-body">
          <div class="form-group">
              <asp:HiddenField ID="hdnRut" runat="server" />
             </div>
          <div class="form-group">
                <label for="">Nombre</label>
              <asp:TextBox ID="txtNombre" class="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="">Dirección</label>
              <asp:TextBox ID="txtDireccion" class="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="">Comuna</label>
                <asp:DropDownList ID="cmbComuna" class="form-control" runat="server"></asp:DropDownList>
            </div>
            <div class="form-group">
                <label for="">Correo</label>
              <asp:TextBox ID="txtCorreo" class="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="">Telefono</label>
                <asp:TextBox ID="txtFono" class="form-control" runat="server" MaxLength="9"></asp:TextBox>
                 </div>
        </div>
        <div class="modal-footer">
             <button type="button" class="btn btn-default" data-dismiss="modal">Cancelar</button>
           <button type="button" class="btn btn-success" runat="server" onserverclick="btnModificar_Click"><i class="fa fa-floppy-o"></i> Guardar</button>
         </div>
      </div>
    </div>
  </div><!-- close modal -->
                    </div>
                        <!-- /. ROW  -->
                

				<footer><p>Portafolio 2018</p></footer>
            </div>
            <!-- /. PAGE INNER  -->
        </div>
        <!-- /. PAGE WRAPPER  -->
    </div>
    <!-- /. WRAPPER  -->
    </form>

    <!-- Metis Menu Js -->
    <script src="../assets/js/jquery.metisMenu.js"></script>
 <!-- DATA TABLE SCRIPTS -->
 <script src="../assets/js/dataTables/jquery.dataTables.js"></script>
 <script src="../assets/js/dataTables/dataTables.bootstrap.js"></script>
     <script>
         $(document).ready(function () {
             $('#dataTables-example').dataTable();
         });

         function Confirmar() {
             return confirm('¿Desea modificar este usuario?') == true;
         }
 </script>
      <!-- Custom Js -->
 <script src="../assets/js/custom-scripts.js"></script>
      <!-- user Script -->
 <script src="../assets/js/userScript.js"></script>

</body>

</html>
