<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="agregarVisitaMedica.aspx.cs" Inherits="Presentacion.Supervisor.agregarVisita" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Sistema - Médico</title>
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
       <!-- Step Style -->
    <link href="../assets/css/step.css" rel="stylesheet" />
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
                        <asp:Label ID="lblNombreUs" runat="server" Text=""></asp:Label>  <i class="fa fa-user fa-fw"></i> <i class="fa fa-caret-down"></i>
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
                            <a href="#"><i class="glyphicon glyphicon-list-alt"></i> Evaluaciones<span class="fa arrow"></span></a>
                            <ul class="nav nav-second-level">
                                <li>
                                    <a href="agregarEvaluacion.aspx">Generar evaluación</a>
                                </li>
                                <li>
                                    <a href="administrarEvaluacion.aspx">Administrar evaluaciones</a>
                                </li>
                            </ul>
                        </li>
                    <li>
                            <a href="#"><i class="fa fa-building-o"></i> Empresas<span class="fa arrow"></span></a>
                            <ul class="nav nav-second-level">
                                <li>
                                    <a href="generarInforme.aspx"> Generar informe</a>
                                </li>
                            </ul>
                    </li>
                    <li>
                            <a href="#"><i class="fa fa-calendar"></i> Capacitaciones<span class="fa arrow"></span></a>
                            <ul class="nav nav-second-level">
                                <li>
                                    <a href="agregarCapacitacion.aspx">Generar plan de capacitación</a>
                                </li>
                                <li>
                                     <a href="administrarCapacitaciones.aspx">Administrar capacitaciones</a>
                                </li>
                            </ul>
                    </li>
                    <li>
                            <a class="active-menu" href="#"><i class="fa fa-medkit"></i> Visitas médicas<span class="fa arrow"></span></a>
                            <ul class="nav nav-second-level collapse in">
                                <li>
                                    <a href="agregarVisitaMedica.aspx">Agregar visita médica</a>
                                </li>
                                <li>
                                     <a href="administrarVisitas.aspx">Administrar visitas médicas</a>
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
                                    Ingresar Visita Médica
                                </div>
                                <div class="panel-body">   
                                   <form id="form1" runat="server">
                                       
                        <div class="col-lg-6">    
                                        <div class="form-group">
                                            <label for="">Fecha</label>
                                            <asp:Calendar ID="MyCalendario" runat="server" BackColor="White" BorderColor="White" BorderWidth="1px" Font-Names="Verdana" Font-Size="10pt" ForeColor="Black" Height="190px" NextPrevFormat="FullMonth" Width="350px" OnDayRender="MyCalendario_DayRender" DayNameFormat="FirstTwoLetters" FirstDayOfWeek="Monday" Font-Strikeout="False" OnSelectionChanged="MyCalendario_SelectionChanged" SelectorStyle-BackColor="Red">
                                                <DayHeaderStyle Font-Bold="True" Font-Size="8pt" />
                                                <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" VerticalAlign="Bottom" />
                                                <OtherMonthDayStyle ForeColor="#999999" />
                                                <SelectedDayStyle BackColor="Red" ForeColor="White" />
                                                <TitleStyle BackColor="White" BorderColor="Black" BorderWidth="4px" Font-Bold="True" Font-Size="12pt" ForeColor="#333399" />
                                                <TodayDayStyle BackColor="#CCCCCC" />
                                            </asp:Calendar>
                                        </div>              
                                </div>
                                <div class="col-lg-6">
                                    <div class="form-group">
                                        <label for="">Empresa*</label>
                                        <asp:DropDownList ID="cmbEmpresa" class="form-control" runat="server" OnSelectedIndexChanged="cmbEmpresa_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                        </div>
                                    <div class="form-group">
                                        <label for="">Doctor(a)*</label>
                                        <div class="row">
                                            <div class="col-lg-6">
                                                <asp:DropDownList ID="cmbDoctor" class="form-control" runat="server" OnSelectedIndexChanged="cmbDoctor_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                            </div>
                                            <div class="col-lg-6">
                                                <div class="col-lg-6">
                                                    <a href="agregarMedico.aspx" class="btn btn-primary"><i class="fa fa-user-md"></i> Añadir Nuevo</a>
                                                </div>
                                            </div>
                                        </div>
                                        </div>


                                        <div class="form-group">
                                            <label for="">Horas Disponibles*</label>
                                                    <asp:DropDownList ID="cmbHora" CssClass="form-control" runat="server" AutoPostBack="True">                                            
                                                    </asp:DropDownList>
                                        </div>  
                                </div>
                                       <div class="row">
                                <div class="col-lg-6">
                                    <button type="button" class="btn btn-primary" runat="server" onserverclick="btnAgendar_Click"><i class="fa fa-calendar"></i> Agendar</button>
                                          <br />
                                        </div>
                                         <br />
                                           <br />
                                    <br />
                                      &nbsp;&nbsp;&nbsp;
                                      <asp:Label ID="lblAlerta" runat="server" Visible="False" Font-Bold="True" ForeColor="Red"></asp:Label>
   
                                           <br />
                                    
                                           <br>
                                <div class="col-lg-12">    
                                     <p class="help-block">Los campos con (*) son obligatorios.</p>  
                                    </div>
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
    <!-- JS Scripts-->

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
