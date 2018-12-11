<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdministrarExamenes.aspx.cs" Inherits="Presentacion.Medico.AdministrarExamenes" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Sistema - Administrar Examenes</title>
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
                            <a href="#"><i class="glyphicon glyphicon-list-alt"></i>Citas Médicas<span class="fa arrow"></span></a>
                            <ul class="nav nav-second-level">
                                <li>
                                    <a href="CitasMedicas.aspx">Mis Citas</a>
                                </li>
                            </ul>
                        </li>
                    <li>
                            <a class="active-menu" href="#"><i class="fa fa-medkit"></i>Atenciones Médicas<span class="fa arrow"></span></a>
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
                <div class="row">
                        <div class="col-md-12">
                            <!-- Advanced Tables -->
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                     Examenes
                                </div>
                                <div class="panel-body">
                                    <div class="table-responsive">  
                                                      <!-- Alert -->
                                 <div class="" id="alerta" runat="server">
                                     <asp:Label ID="lblAlertMsge" runat="server" Text=""></asp:Label>
                                 </div> 
                                       <br />
                                        <br />
                                        <asp:GridView class="table table-striped table-bordered table-hover" ID="gvExamenes" runat="server" EmptyDataText="No se han encontrado resultados." Visible="False" AutoGenerateColumns="False" DataKeyNames="ID" OnRowDataBound="gvExamenes_RowDataBound" OnRowCommand="gvExamenes_RowCommand">
                                        <Columns>
                                             <asp:BoundField DataField="ID" HeaderText="Código Cita" SortExpression="ID" />
                                             <asp:BoundField AccessibleHeaderText="RUT" DataField="RUT" HeaderText="Rut" SortExpression="RUT" />
                                             <asp:BoundField AccessibleHeaderText="NOMBRE" DataField="NOMBRE" HeaderText="Paciente" SortExpression="Paciente" />
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
                                                                <asp:LinkButton CommandName="Modificar" CommandArgument='<%# Eval("ID") %>' class="btn btn-info" runat="server"><i class="fa fa-edit"></i> Modificar</asp:LinkButton>
                                                                <asp:LinkButton CommandName="Eliminar"  CommandArgument='<%# Eval("ID") %>' class="btn btn-danger" runat="server" OnClientClick="return Confirmar();"><i class="fa fa-trash"></i> Eliminar</asp:LinkButton>
                                                                <asp:LinkButton CommandName="Descargar" CommandArgument='<%# Eval("ID") %>' class="btn btn-primary" runat="server"><i class="fa fa-file-pdf-o"></i> Descargar Examen</asp:LinkButton>
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
              <!-- Datos no visibles -->
              <asp:HiddenField ID="hdnId" runat="server" />
             </div>
            <div class="form-group">
                <label for="">Anotaciones</label>
                <textarea class="form-control" id="txtDescripcion" cols="20" rows="4" runat="server"></textarea>
            </div>
                 <div class="form-group">
                <label for="">Examen actual: </label>
                     <asp:Label ID="inputTxtExamen" Text="text" runat="server" />
            </div>
             <div class="form-group">
                <label for="">Subir nuevo</label>
                 <asp:FileUpload class="form-control" ID="flDocumento" runat="server" />
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
             return confirm('¿Desea eliminar este examen?') == true;
         }
 </script>
      <!-- Custom Js -->
 <script src="../assets/js/custom-scripts.js"></script>
      <!-- user Script -->
 <script src="../assets/js/userScript.js"></script>

</body>

</html>

