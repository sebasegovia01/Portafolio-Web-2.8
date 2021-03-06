﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CitasMedicas.aspx.cs" Inherits="Presentacion.Medico.CitasMedicas" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Sistema - Citas Médicas</title>
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
                        <asp:Label ID="lblNombreUs" runat="server" Text="Label"></asp:Label> <i class="fa fa-user fa-fw"></i> <i class="fa fa-caret-down"></i>
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
                            <a class="active-menu" href="#"><i class="glyphicon glyphicon-list-alt"></i>Citas Médicas<span class="fa arrow"></span></a>
                            <ul class="nav nav-second-level collapse in">
                                <li>
                                    <a href="CitasMedicas.aspx">Mis Citas</a>
                                </li>
                            </ul>
                        </li>
                    <li>
                            <a href="#"><i class="fa fa-medkit"></i>Atenciones Médicas<span class="fa arrow"></span></a>
                            <ul class="nav nav-second-level">
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
                                    Visitas Médicas
                                </div>
                                <div class="panel-body">   
                                    <form id="form1" runat="server">
                                    
                                        <div class="col-lg-12">                          
                                            <div class="form-group">
                                               <!-- Alert -->
                                              <div class="" id="alerta" runat="server">
                                                 <asp:Label ID="lblAlertMsge" runat="server" Text=""></asp:Label>
                                              </div> 
                                              <!-- /Alert --> 
                                                <br />
                                                <br />
                                                <asp:GridView ID="gvVisitasMe" class="table table-striped table-bordered table-hover" EmptyDataText="No se han encontrado resultados." runat="server" AutoGenerateColumns="False" DataKeyNames="ID" OnRowCommand="gvVisitasMe_RowCommand" OnRowDataBound="gvVisitasMe_RowDataBound">
                                                    <Columns>
                                                        <asp:BoundField DataField="ID" SortExpression="ID" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn"/>
                                                        <asp:BoundField DataField="RUT" HeaderText="Rut" SortExpression="RUT"/>
                                                        <asp:BoundField DataField="NOMBRE" HeaderText="Nombre" SortExpression="NOMBRE"/>
                                                        <asp:BoundField DataField="FECHA" HeaderText="Fecha" SortExpression="FECHA"/>
                                                        <asp:BoundField DataField="HORA" HeaderText="Hora" SortExpression="HORA"/>
                                                        <asp:BoundField DataField="ASISTENCIA" HeaderText="Asistencia" SortExpression="ASISTENCIA" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn"/>
                                                        <asp:TemplateField headertext="">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnHabilitar" runat="server" text="" CssClass="" CommandName="" CommandArgument='<%# Eval("ID") %>' OnClientClick="return Confirmar();"></asp:LinkButton>
                                                        </ItemTemplate>
                                                       </asp:TemplateField> 
                                                   </Columns>
                                                </asp:GridView>                                
                                            </div>
                                       </div>
<!-- Modal -->
  <div id="myModal" class="modal fade" role="dialog">
  <div class="modal-dialog">
      <div class="modal-content">
        <div class="modal-header">
          <h4 class="modal-title">Modificar Cita</h4>
        </div>
        <div class="modal-body">
            <asp:Label ID="lblAlertModal" runat="server" Visible="False" ForeColor="Red"></asp:Label>
           <div class="form-group">
               <asp:HiddenField ID="hdnId" runat="server" />
               <asp:HiddenField ID="hdnRut" runat="server" />
                <label for="">Fecha</label>
                 
              </div>
               <div class="form-group">
                <label for="">Horas Disponibles</label>
              </div>
            <div class="form-group">
                <label for="">Asistencia</label>
            </div>
          </div>
        <div class="modal-footer">
             <button type="button" class="btn btn-default" data-dismiss="modal">Cancelar</button>
            <asp:Button ID="btnModificar" CssClass="btn btn-success" runat="server" Text="Modificar" />
        </div>
      </div>
    </div>
  </div><!-- close modal -->
                                        </form> 
                                </div>                          
                        </div>                                
                 </div><!--/ROW -->
                 <!-- Trigger the modal with a button -->
                   
                                                           
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
     <script>
         $(document).ready(function () {
             $('#dataTables-example').dataTable();
         });

         function Confirmar() {
             return confirm('¿Modificar esta cita?') == true;
         }
 </script>
</body>

</html>
