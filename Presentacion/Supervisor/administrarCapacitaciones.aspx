<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="administrarCapacitaciones.aspx.cs" Inherits="Presentacion.Supervisor.administrarCapacitaciones" %>

<html>
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
     <!-- jQuery Js -->
    <script src="../assets/js/jquery-1.10.2.js"></script>
    <!-- Bootstrap Js -->
    <script src="../assets/js/bootstrap.min.js"></script>
    <!-- DataTable -->
    <script src="../assets/js/dataTables/dataTables.bootstrap.js"></script>

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
                            <a class="active-menu" href="#"><i class="fa fa-calendar"></i> Capacitaciones<span class="fa arrow"></span></a>
                            <ul class="nav nav-second-level collapse in">
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
                                    Capacitaciones
                                </div>
                                <div class="panel-body">   
                                    <form id="form2" runat="server">
                                           <!-- Alert -->
                                 <div class="" id="alerta" runat="server">
                                     <asp:Label ID="lblAlertMsge" runat="server" Text=""></asp:Label>
                                 </div> 
                                       <!-- /Alert --> 
                                        <br />
                                        <div class="col-lg-12">                          
                                            <div class="form-group">
                                                <asp:GridView ID="gvCapacitaciones" class="table table-striped table-bordered table-hover" EmptyDataText="No se han encontrado resultados." runat="server" AutoGenerateColumns="False" OnRowCommand="gvCapacitaciones_RowCommand">
                                            <Columns>
                                                <asp:BoundField DataField="ID" HeaderText="Id" SortExpression="ID" Visible="false" />
                                                <asp:BoundField DataField="EMPRESA" HeaderText="Empresa" SortExpression="EMPRESA" />
                                                <asp:BoundField DataField="FECHA" HeaderText="Fecha" SortExpression="FECHA" />
                                                <asp:BoundField DataField="LUGAR" HeaderText="Lugar" SortExpression="LUGAR"/>
                                                <asp:BoundField DataField="OBJETIVO" HeaderText="Objetivo" SortExpression="OBJETIVO"/>  
                                                <asp:BoundField DataField="EXPOSITOR" HeaderText="Expositor" SortExpression="EXPOSITOR" />
                                                 <asp:TemplateField headertext="Opciones" >
                                                            <ItemTemplate>
                                                            <asp:LinkButton CommandName="Detalle" CommandArgument='<%# Eval("ID") %>' class="btn btn-primary" runat="server"><i class="fa fa-info"></i> Detalle</asp:LinkButton>
                                                            <asp:LinkButton CommandName="Modificar" CommandArgument='<%# Eval("ID") %>' class="btn btn-info" runat="server"><i class="fa fa-edit"></i> Editar</asp:LinkButton>
                                                            <asp:LinkButton CommandName="Eliminar" CommandArgument='<%# Eval("ID") %>' class="btn btn-danger" runat="server"><i class="fa fa-trash"></i> Anular</asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>                                           
                                            </Columns>
                                               </asp:GridView>
                                            </div>
                                        </div>
                                                                                
   <!-- Modal -->
  <div class="modal fade" id="myModal" role="dialog">
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="modal-header">
          <h4 id="tituloModal" runat="server" class="modal-title"></h4>
        </div>
        <div id="modificarDatos" runat="server">
        <div class="modal-body">
          <div class="form-group">
              <asp:HiddenField ID="hdnIdCapacitacion" runat="server" />
             </div> 
             <div class="form-group">
                <label for="">Expositor</label>
              <asp:DropDownList ID="ddlExpositor" class="form-control" runat="server"></asp:DropDownList>
            </div>         
            <div class="form-group">
                <label for="">Fecha</label>
                <input id="datetimepicker" type="date" class="form-control" runat="server" autocomplete="off" />
            </div>
            <div class="form-group">
                <label for="">Hora</label>
               <asp:DropDownList ID="ddlHora" class="form-control" runat="server"></asp:DropDownList>
            </div>
              <div class="form-group">
                <label for="">Objetivo</label>
                <textarea id="txtObjetivo" cols="20" rows="2" runat="server" class="form-control"></textarea>
            </div>
            <div class="form-group">
                <label for="">Lugar</label>
                <input id="txtLugar" type="text" class="form-control" runat="server" />
            </div>
        </div>
        <div class="modal-footer">
             <button type="button" class="btn btn-default" data-dismiss="modal">Cancelar</button>
           <button type="button" class="btn btn-success" runat="server" onserverclick="btnModificar_Click"><i class="fa fa-floppy-o"></i> Guardar</button>
         </div>
            </div><!-- /modificar-datos -->

          <div id="listarDetalle" runat="server">
           <div class="modal-body">
               <asp:GridView ID="gvDetalleCap" runat="server" class="table table-striped table-bordered table-hover" AutoGenerateColumns="false" OnRowDataBound="gvDetalleCap_RowDataBound">
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
          </div><!-- /listar-datos -->
          </div>
      </div>
  </div><!-- close modal -->

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
    <!-- Metis Menu Js -->
    <script src="../assets/js/jquery.metisMenu.js"></script>
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

</body>

</html>

