<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Capacitaciones.aspx.cs" Inherits="Presentacion.Ingeniero.Capacitaciones" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Sistema - Capacitaciones</title>
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
                    <a href="#"><i class="glyphicon glyphicon-list-alt"></i> Evaluaciones<span class="fa arrow"></span></a>
                    <ul class="nav nav-second-level">
                        <li>
                            <a href="AdministrarEvaluacion.aspx">Administrar evaluaciones</a>
                            <a href="AdministrarRecomendacion.aspx">Administrar recomendación</a>
                        </li>
                    </ul>
                </li>
                  <li>
                    <a class="active-menu" href="#"><i class="fa fa-calendar"></i> Capacitaciones<span class="fa arrow"></span></a>
                    <ul class="nav nav-second-level collapse in">
                        <li>
                            <a href="Capacitaciones.aspx">Mis capacitaciones</a>
                        </li>
                    </ul>
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
                                     Mis capacitaciones
                                </div>
                                <div class="panel-body">
                                    <div class="table-responsive">  
                                       <!-- Alert -->
                                              <div class="" id="alerta" runat="server">
                                                 <asp:Label ID="lblAlertMsge" runat="server" Text=""></asp:Label>
                                              </div> 
                                              <!-- /Alert --> 
                                           <br />
                                        <br />
                                        <asp:GridView class="table table-striped table-bordered table-hover" ID="gvCapataciones" runat="server" EmptyDataText="No se han encontrado resultados." Visible="False" AutoGenerateColumns="False" DataKeyNames="ID" OnRowCommand="gvCapataciones_RowCommand">
                                        <Columns>
                                             <asp:BoundField DataField="ID" HeaderText="Id" SortExpression="ID" visible="false"/>
                                             <asp:BoundField AccessibleHeaderText="OBJETIVO" DataField="OBJETIVO" HeaderText="Objetivo" SortExpression="OBJETIVO" />
                                             <asp:BoundField AccessibleHeaderText="FECHA" DataField="FECHA" HeaderText="Fecha" SortExpression="FECHA" />
                                             <asp:BoundField AccessibleHeaderText="LUGAR" DataField="LUGAR" HeaderText="Lugar" SortExpression="LUGAR" />
                                             <asp:TemplateField headertext="">
                                                            <ItemTemplate>
                                                                <asp:LinkButton CommandName="Iniciar" CommandArgument='<%# Eval("ID") %>' class="btn btn-success" runat="server"><i class="fa fa-play"></i> Iniciar</asp:LinkButton>
                                                                <asp:LinkButton CommandName="Detalle" CommandArgument='<%# Eval("ID") %>' class="btn btn-primary" runat="server"><i class="fa fa-info"></i> Detalle</asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                        </Columns>
                                        </asp:GridView>
                                    </div>

                                         <!-- Modal -->
  <div class="modal fade" id="myModal" role="dialog">
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="modal-header">
          <h4 class="modal-title">Detalle Asistencia</h4>
        </div>
           <div class="modal-body">
               <asp:GridView ID="gvDetalleCap" runat="server" class="table table-striped table-bordered table-hover" DataKeyNames="ID" AutoGenerateColumns="false" OnRowDataBound="gvDetalleCap_RowDataBound">
                     <Columns>
                        <asp:BoundField DataField="ID" HeaderText="Id" SortExpression="ID" Visible="false" />
                        <asp:BoundField DataField="EMPLEADO" HeaderText="Empleado" SortExpression="EMPLEADO" />
                        <asp:BoundField DataField="ASISTENCIA" HeaderText="Asistencia" SortExpression="Asistencia" />                                  
                     </Columns>
               </asp:GridView>
           </div>
          <div class="modal-footer">
             <button type="button" class="btn btn-default" data-dismiss="modal">Cancelar</button>
         </div>
          </div>
      </div>
  </div><!-- close modal -->
                                </div>
                            </div>
                            <!--End Advanced Tables -->
                        </div>
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
