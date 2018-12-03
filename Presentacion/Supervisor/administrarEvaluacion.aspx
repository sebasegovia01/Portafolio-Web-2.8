<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="administrarEvaluacion.aspx.cs" Inherits="Presentacion.Supervisor.administrarEvaluacion" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Sistema - Evaluaciones</title>
    <!-- Bootstrap Styles-->
    <link href="../assets/css/bootstrap.css" rel="stylesheet" />
    <!-- FontAwesome Styles-->
    <link href="../assets/css/font-awesome.css" rel="stylesheet" />
    <!-- Morris Chart Styles-->
    <link href="../assets/js/morris/morris-0.4.3.min.css" rel="stylesheet" />
    <!-- Custom Styles-->
    <link href="../assets/css/custom-styles.css" rel="stylesheet" />
    <!-- Google Fonts-->
    <link href='http://fonts.googleapis.com/css?family=Open+Sans' rel='stylesheet' type='text/css' />

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
                        <a href="index.aspx"><i class="fa fa-home"></i> Principal</a>
                    </li>
                    <li>
                            <a class="active-menu" href="#"><i class="glyphicon glyphicon-list-alt"></i> Evaluaciones<span class="fa arrow"></span></a>
                            <ul class="nav nav-second-level collapse in">
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
                            <a href="#"><i class="fa fa-medkit"></i> Visitas médicas<span class="fa arrow"></span></a>
                            <ul class="nav nav-second-level">
                                <li>
                                    <a href="agregarVisitaMedica.aspx">Generar visita médica</a>
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
                                    Evaluaciones
                                </div>
                                <div class="panel-body">   
                                    <form id="form1" runat="server">
                                    
                                        <div class="col-lg-12">                          
                                            <div class="form-group">
                                                <label for="">Tipo*</label><br />
                                                <asp:DropDownList ID="ddlTipo" runat="server" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlTipo_SelectedIndexChanged">
                                                    <asp:ListItem Selected="True" Value="0">Selecciona tipo</asp:ListItem>
                                                    <asp:ListItem Value="1">Persona</asp:ListItem>
                                                    <asp:ListItem Value="2">Empresa</asp:ListItem>
                                                </asp:DropDownList>

                                                <label for="">Estado*</label><br />
                                                <asp:DropDownList ID="ddlTipoEvaluacion" runat="server" class="form-control" OnSelectedIndexChanged="ddlTipoEvaluacion_SelectedIndexChanged" AutoPostBack="True">
                                                    <asp:ListItem Selected="True" Value="falso">Selecciona estado</asp:ListItem>
                                                    <asp:ListItem Value="1">Derivada</asp:ListItem>
                                                    <asp:ListItem Value="0">No Derivada</asp:ListItem>
                                                </asp:DropDownList>

                                                <br />

                                                <asp:GridView ID="gvEvaluaciones" class="table table-striped table-bordered table-hover" EmptyDataText="No se han encontrado resultados." runat="server" AutoGenerateColumns="False" OnRowCommand="gvEvaluaciones_RowCommand">
                                                    <Columns>
                                                        <asp:BoundField DataField="FECHA" HeaderText="FECHA" SortExpression="FECHA" />
                                                        <asp:BoundField DataField="OBSERVACION" HeaderText="OBSERVACIÓN" SortExpression="OBSERVACIÓN" />
                                                        <asp:BoundField DataField="DERIVADA" HeaderText="DERIVADA" SortExpression="DERIVADA" />
                                                        <asp:BoundField DataField="EMPLEADO" HeaderText="EMPLEADO" SortExpression="EMPLEADO" /> 
                                                        <asp:BoundField DataField="EMPRESA" HeaderText="EMPRESA" SortExpression="EMPRESA" />
                                                        <asp:BoundField DataField="CLIENTE" HeaderText="CLIENTE" SortExpression="CLIENTE" />
                                                        <asp:TemplateField headertext="DERIVACIÓN">
                                                            <ItemTemplate>
                                                                <asp:LinkButton CommandName="Enviar" CommandArgument='<%# Eval("CLAVE") %>' class="btn btn-primary" OnClientClick="return Confirmar();" runat="server">Derivar</asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    </asp:GridView>

                                                <asp:GridView ID="gvEmpresa" class="table table-striped table-bordered table-hover" EmptyDataText="No se han encontrado resultados." runat="server" AutoGenerateColumns="False" OnRowCommand="gvEmpresa_RowCommand">
                                                    <Columns>
                                                        <asp:BoundField DataField="FECHA" HeaderText="FECHA" SortExpression="FECHA" />
                                                        <asp:BoundField DataField="OBSERVACION" HeaderText="OBSERVACION" SortExpression="OBSERVACION" />
                                                        <asp:BoundField DataField="DERIVADA" HeaderText="DERIVADA" SortExpression="DERIVADA" />
                                                        <asp:BoundField DataField="EMPLEADO" HeaderText="EMPLEADO" SortExpression="EMPLEADO" /> 
                                                        <asp:BoundField DataField="EMPRESA" HeaderText="EMPRESA" SortExpression="EMPRESA" />
                                                        <asp:TemplateField headertext="DERIVACIÓN">
                                                            <ItemTemplate>
                                                                <asp:LinkButton CommandName="Enviar" CommandArgument='<%# Eval("CLAVE") %>' class="btn btn-primary" OnClientClick="return Confirmar();" runat="server">Derivar</asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    </asp:GridView>

                                            </div>
                                        </div>
                                        <asp:Label ID="lblAlerta" runat="server" Visible="true" Font-Bold="True" ForeColor="Red"></asp:Label>
                                        
                                    </form>

                                </div>                       
                        </div>                 
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
    <script src="../assets/js/dataTables/dataTables.bootstrap.js"></script>
     <script>
         function Confirmar(){
             return confirm('¿Desea enviar esta evaluación para su posterior revisión?') == true;
         }
    </script>
    <!-- Morris Chart Js -->
    <script src="../assets/js/morris/raphael-2.1.0.min.js"></script>
    <script src="../assets/js/morris/morris.js"></script>
    <!-- Custom Js -->
    <script src="../assets/js/custom-scripts.js"></script>
    <!-- user Script -->
    <script src="../assets/js/userScript.js"></script>

</body>

</html>

