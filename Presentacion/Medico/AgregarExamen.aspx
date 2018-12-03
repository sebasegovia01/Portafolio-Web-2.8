<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AgregarExamen.aspx.cs" Inherits="Presentacion.Medico.AgregarExamen" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Sistema - Agregar Examen</title>
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
        <!-- jQuery Js -->
    <script src="../assets/js/jquery-1.10.2.js"></script>
    <!-- Bootstrap Js -->
    <script src="../assets/js/bootstrap.min.js"></script>
</head>

<body>
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
                        <asp:Label ID="lblNombreUs" runat="server" Text="Label"></asp:Label> <i class="fa fa-user fa-fw"></i> <i class="fa fa-caret-down"></i>
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
                        <a class="active-menu" href="index.aspx"><i class="fa fa-home"></i> Principal</a>
                    </li>
                    <li>
                            <a href="#"><i class="glyphicon glyphicon-list-alt"></i>Citas Médicas<span class="fa arrow"></span></a>
                            <ul class="nav nav-second-level">
                                <li>
                                    <a href="CitasMedicas.aspx">Mis Citas</a>
                                </li>
                            </ul>
                        </li>
                    <li>
                            <a href="#"><i class="fa fa-medkit"></i>Atenciones Médicas<span class="fa arrow"></span></a>
                            <ul class="nav nav-second-level collapse in">
                                 <li>
                                     <a href="AgregarDiagnostico.aspx">Agregar Diagnostico</a>
                                </li>
                                <li>
                                     <a href="AdministrarDiagnosticos.aspx">Administrar Diagnosticos</a>
                                </li>
                                <li>
                                     <a href="AgregarExamen.aspx">Agregar Examen</a>
                                </li>
                                <li>
                                     <a href="AdministrarExamenes.aspx">Administrar Examenes Médicos</a>
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
                 <!-- /. ROW  -->     
                 <div class="row">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    Añadir Examen
                                </div>
                                <div class="panel-body">   
                                    <form id="form1" runat="server">
                        <div class="col-lg-6">                          
                                        <div class="form-group">
                                          <div class="form-group">
                                            <label for="">Empleado</label>
                                        <asp:DropDownList CssClass="form-control" ID="cmbEmpleado" runat="server"></asp:DropDownList>
                                    </div>  
                                        </div>
                                        <div class="form-group">
                                            <label for="">Información Examen</label>
                                            <textarea class="form-control" id="txtNombreEx" cols="20" rows="4" runat="server"></textarea>
                                        </div>
                                </div>
                                <div class="col-lg-6">
                                    <div class="form-group">
                                            <label for="">Examen</label>
                                        <asp:FileUpload class="form-control" ID="flExamen" runat="server" />
                                    </div>  
                                </div>

                                <div class="col-lg-12">
                                      <button type="button" class="btn btn-primary" runat="server" onserverclick="btnAgregar_Click"><i class="fa fa-floppy-o"></i> Guardar</button>
                                           <br />
                                    <br />
                                           <asp:Label ID="lblAlerta" runat="server" Visible="False" Font-Bold="True" ForeColor="Red"></asp:Label>
   
                                     <p class="help-block">Los campos con (*) son obligatorios.</p>                                   
                                    <br>
                         
                                </div>
                                        </form>
                            </div>                       
                        </div>                 
                 </div><!--/ROW -->
                 
                  <!-- Modal -->
  <div class="modal fade" id="myModal" role="dialog">
    <div class="modal-dialog modal-sm">
      <div class="modal-content">
        <div class="modal-header">
          <h4 class="modal-title" id="modalTitle" runat="server"></h4>
        </div>
        <div class="modal-body">
          <p runat="server" id="modalText"></p>
        </div>
        <div class="modal-footer">
            <a href="#" runat="server" id="modalBtn" class="btn btn-default"></a>
        </div>
      </div>
    </div>
  </div><!-- close modal -->
				<footer><p>Portafolio 2018</p></footer>
            </div>
            <!-- /. PAGE INNER  -->
        </div>
        <!-- /. PAGE WRAPPER  -->
    </div>
    <!-- /. WRAPPER  -->
    <!-- Metis Menu Js -->
    <script src="../assets/js/jquery.metisMenu.js"></script>
    <!-- Morris Chart Js -->
    <script src="../assets/js/morris/raphael-2.1.0.min.js"></script>
    <script src="../assets/js/morris/morris.js"></script>
    <!-- Custom Js -->
    <script src="../assets/js/custom-scripts.js"></script>
    <!-- user Script -->
    <script src="../assets/js/userScript.js"></script>

</body>

</html>

