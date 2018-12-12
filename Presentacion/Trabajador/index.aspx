<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="Presentacion.Trabajador.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Sistema - Trabajador</title>
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
    <!-- hide column table -->
    <link href="../assets/css/hidecolumn.css" rel="stylesheet" />
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
                        <li><a href="../Consulta.aspx?close=1"><i class="fa fa-sign-out fa-fw"></i> Cerrar sesión</a>
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
               </ul>
            </div>

        </nav>
        <!-- /. NAV SIDE  -->
        <form method="POST" runat="server">
        <div id="page-wrapper">
            <div id="page-inner">


                <div class="row">
                    <div class="col-md-12">
                        <h1 class="page-header">
                            Bienvenido <small><asp:Label ID="lblMsgeBienvenida" runat="server" Text=""></asp:Label></small>
                        </h1>
                    </div>
                </div>
                <!-- Metricas -->
                 <div class="row">
                     <!-- ultima conexión -->
                    <div class="col-md-3 col-sm-12 col-xs-12">
                        <div class="panel panel-primary text-center no-boder bg-color-green">
                            <div class="panel-body">
                                <i class="fa fa-calendar fa-5x"></i>
                                <h3 id="lblUltimaConexion" runat="server"></h3>
                            </div>
                            <div class="panel-footer back-footer-green">
                                Ultima Conexión
                            </div>
                        </div>
                    </div>
                     <!-- empresa -->
                    <div class="col-md-3 col-sm-12 col-xs-12">
                        <div class="panel panel-primary text-center no-boder bg-color-blue">
                            <div class="panel-body">
                                <i class="fa fa-building fa-5x"></i>
                                <h3 id="lblEmpresa" runat="server"></h3>
                            </div>
                            <div class="panel-footer back-footer-blue">
                                Empresa
                            </div>
                        </div>
                    </div>
                     <!-- proxima capacitación -->
                    <div class="col-md-3 col-sm-12 col-xs-12">
                        <div class="panel panel-primary text-center no-boder bg-color-red">
                            <div class="panel-body">
                                <i class="fa fa-briefcase fa-5x"></i>
                                <h3 id="lblFechaCapacitacion" runat="server"></h3>
                            </div>
                            <div class="panel-footer back-footer-red">
                                Proxima Capacitación<a id="lblConfirmacion" runat="server" style="color:white;font-style:italic"> Confirmar</a>
                            </div>
                        </div>
                    </div>
                     <!-- proxima cita médica -->
                    <div class="col-md-3 col-sm-12 col-xs-12">
                        <div class="panel panel-primary text-center no-boder bg-color-brown">
                            <div class="panel-body">
                                <i class="fa fa-stethoscope fa-5x"></i>
                                <h3 id="lblCitaMedica" runat="server"></h3>
                            </div>
                            <div class="panel-footer back-footer-brown">
                                Proxima Cita Médica
                            </div>
                        </div>
                    </div>
                </div><!-- Metricas -->
                 <div class="row">
                    <div class="col-md-12 col-sm-12 col-xs-12">

                        <div class="panel panel-default">
                            <div class="panel-heading">
                                Examenes Médicos
                            </div> 
                            <div class="panel-body">
                                <div class="table-responsive">
                                        <asp:GridView ID="gvExamenes" runat="server" class="table table-striped table-bordered table-hover" EmptyDataText="No existen examenes disponibles." AutoGenerateColumns="False" OnRowDataBound="gvExamenes_RowDataBound" OnRowCommand="gvExamenes_RowCommand">
                                             <Columns>
                                             <asp:BoundField DataField="ID" HeaderText="Código Cita" SortExpression="ID" />
                                             <asp:BoundField AccessibleHeaderText="RUT" DataField="RUT" HeaderText="Rut" SortExpression="RUT" />
                                             <asp:BoundField AccessibleHeaderText="NOMBRE" DataField="NOMBRE" HeaderText="Paciente" SortExpression="Paciente" />
                                             <asp:BoundField AccessibleHeaderText="FECHA" DataField="FECHA" HeaderText="Fecha Examen" SortExpression="FECHA" />
                                             <asp:BoundField AccessibleHeaderText="DESCRIPCION" DataField="DESCRIPCION" HeaderText="Descripción" SortExpression="DESCRIPCION" />
                                             <asp:BoundField AccessibleHeaderText="DOCUMENTO" DataField="DOCUMENTO" HeaderText="Documento" SortExpression="DOCUMENTO" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn" >
                                            <HeaderStyle CssClass="hideGridColumn"></HeaderStyle>

                                                <ItemStyle CssClass="hideGridColumn"></ItemStyle>
                                             </asp:BoundField>
                                            <asp:BoundField AccessibleHeaderText="HABILITADO" DataField="HABILITADO" HeaderText="Habilitado" SortExpression="HABILITADO" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn" >
                                                    <HeaderStyle CssClass="hideGridColumn"></HeaderStyle>

                                                <ItemStyle CssClass="hideGridColumn"></ItemStyle>
                                             </asp:BoundField>
                                             <asp:TemplateField headertext="Opciones">
                                                            <ItemTemplate>
                                                                <asp:LinkButton CommandName="DescargarExm" CommandArgument='<%# Eval("ID") %>' class="btn btn-primary" runat="server"><i class="fa fa-file-pdf-o"></i> Descargar Examen</asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField> 

                                        </Columns>
                                        </asp:GridView>
                                </div>
                            </div>
                        </div>

                    </div>
                     <div class="col-md-12 col-sm-12 col-xs-12">

                        <div class="panel panel-default">
                            <div class="panel-heading">
                                Certificados Capacitaciones
                            </div> 
                            <div class="panel-body">
                                <div class="table-responsive">
                                        <asp:GridView ID="gvCertificados" runat="server" class="table table-striped table-bordered table-hover" AutoGenerateColumns="False" EmptyDataText="No se han encontrado certificados" OnRowCommand="gvCertificados_RowCommand" OnRowDataBound="gvCertificados_RowDataBound">
                                             <Columns>
                                             <asp:BoundField DataField="CODIGO" HeaderText="Código" SortExpression="CODIGO" />
                                             <asp:BoundField AccessibleHeaderText="ID" DataField="ID" HeaderText="Id" SortExpression="ID" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn" />
                                             <asp:BoundField AccessibleHeaderText="NOMBRE" DataField="NOMBRE" HeaderText="Nombre" SortExpression="NOMBRE" />
                                             <asp:BoundField AccessibleHeaderText="DOCUMENTO" DataField="DOCUMENTO" HeaderText="Documento" SortExpression="DOCUMENTO" />
                                             <asp:BoundField AccessibleHeaderText="FECHA" DataField ="FECHA" HeaderText="Fecha Emisión" SortExpression="FECHA" />
                                             <asp:TemplateField headertext="Opciones">
                                                            <ItemTemplate>
                                                                <asp:LinkButton CommandName="DescargarCer" CommandArgument='<%# Eval("DOCUMENTO") %>' class="btn btn-primary" runat="server"><i class="fa fa-file-pdf-o"></i> Descargar Certificado</asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField> 

                                        </Columns>
                                        </asp:GridView>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
                <!-- /. ROW  -->

				<footer><p>Portafolio 2018</p></footer>
            </div>
            <!-- /. PAGE INNER  -->
        </div>
        <!-- /. PAGE WRAPPER  -->
      </form>
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
