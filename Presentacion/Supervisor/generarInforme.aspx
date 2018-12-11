<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="generarInforme.aspx.cs" Inherits="Presentacion.Supervisor.generarInforme" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Sistema - Informe</title>
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
                            <a class="active-menu"  href="#"><i class="fa fa-building-o"></i> Empresas<span class="fa arrow"></span></a>
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
                            <a href="#"><i class="fa fa-medkit"></i> Visitas médicas<span class="fa arrow"></span></a>
                            <ul class="nav nav-second-level">
                                <li>
                                    <a href="agregarVisitaMedica.aspx">Agregar visita médica</a>
                                </li>
                                <li>
                                     <a href="administrarVisitas.aspx">Administrar visitas médicas</a>
                                </li>
                            </ul>
                    </li>
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
                                    Informes
                                </div>
                                <div class="panel-body">   
                                    <form id="form2" runat="server">
                                          <!-- Alert -->
                                              <div class="" id="alerta" runat="server">
                                                 <asp:Label ID="lblAlertMsge" runat="server" Text=""></asp:Label>
                                              </div> 
                                              <!-- /Alert --> 
                                        <div class="form-group">
                                             <div class="col-lg-6">
                                                 <label for="">Empresa</label><br />
                                                <asp:DropDownList ID="ddlEmpresa" runat="server" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlEmpresa_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>   
                                             <div class="col-lg-6">
                                                 <label for="">Informe General</label><br />
                                                 <asp:LinkButton ID="btnInformeGral" CssClass="btn btn-primary" runat="server" OnClick="btnInformeGral_Click"><i class="fa fa-file-pdf-o"></i> Descargar</asp:LinkButton>
                                                 <br />
                                            </div> 
                                              </div>  
                                       
                                <br />
                                        <div class="form-group">    
                                            <br /> 
                                        <div class="col-lg-12">  
                                               <br />                                                   
                                                <asp:GridView ID="gvEmpresas" class="table table-striped table-bordered table-hover" EmptyDataText="No se han encontrado resultados." runat="server" AutoGenerateColumns="False" OnRowCommand="gvEmpresas_RowCommand">
                                               <Columns>
                                             <asp:BoundField DataField="RUT" HeaderText="Rut" SortExpression="RUT" />
                                             <asp:BoundField AccessibleHeaderText="NOMBRE" DataField="NOMBRE" HeaderText="Nombre" SortExpression="NOMBRE" />
                                             <asp:BoundField AccessibleHeaderText="CORREO" DataField="CORREO" HeaderText="Correo" SortExpression="CORREO" />
                                             <asp:BoundField AccessibleHeaderText="NUMERO" DataField="NUMERO" HeaderText="Telefono" SortExpression="NUMERO" />
                                             <asp:BoundField AccessibleHeaderText="COMUNA" DataField="COMUNA" HeaderText="Comuna" SortExpression="COMUNA" />
                                             <asp:TemplateField headertext="">
                                                            <ItemTemplate>
                                                                <asp:LinkButton CommandName="Descargar" CommandArgument='<%# Eval("RUT") %>' class="btn btn-primary" runat="server"><i class="fa fa-file-pdf-o"></i> Descargar Informe</asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                        </Columns>
                                               </asp:GridView>
                                            </div>
                                        </div>
                                    </form>

                                </div>                       
                        </div>       </div>           
                 </div><!--/ROW -->


				<footer><p>Portafolio 2018</p></footer>
            </div>
            <!-- /. PAGE INNER  -->
        </div>
        <!-- /. PAGE WRAPPER  -->
    </div>
    <!-- /. WRAPPER  -->
    <!-- JS Scripts-->
    <!-- jQuery Js -->
    <script src="../assets/js/jquery-1.10.2.js"></script>
    <!-- Bootstrap Js -->
    <script src="../assets/js/bootstrap.min.js"></script>
    <!-- Metis Menu Js -->
    <script src="../assets/js/jquery.metisMenu.js"></script>
    <!-- Morris Chart Js -->
    <script src="../assets/js/morris/raphael-2.1.0.min.js"></script>
    <script src="../assets/js/morris/morris.js"></script>
    <!-- Custom Js -->
    <script src="../assets/js/custom-scripts.js"></script>


</body>

</html>
