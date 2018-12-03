<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="administrarVisitas.aspx.cs" EnableEventValidation="True" Inherits="Presentacion.Supervisor.administrarVisitas" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Sistema - Visitas Médicas</title>
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
                                    Visitas Médicas
                                </div>
                                <div class="panel-body">   
                                    <form id="form1" runat="server">
                                    
                                        <div class="col-lg-12">                          
                                            <div class="form-group">
                                                <asp:Label ID="lblAlert" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
                                                <br />
                                                <br />
                                                <asp:GridView ID="gvVisitasMe" class="table table-striped table-bordered table-hover" EmptyDataText="No se han encontrado resultados." runat="server" AutoGenerateColumns="False" DataKeyNames="ID" OnRowCommand="gvVisitasMe_RowCommand" OnRowDataBound="gvVisitasMe_RowDataBound">
                                                    <Columns>
                                                        <asp:BoundField DataField="ID" SortExpression="ID" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn"/>
                                                        <asp:BoundField DataField="RUT" HeaderText="Rut" SortExpression="RUT"/>
                                                        <asp:BoundField DataField="NOMBRE" HeaderText="Nombre" SortExpression="NOMBRE"/>
                                                        <asp:BoundField DataField="FECHA" HeaderText="Fecha" SortExpression="FECHA"/>
                                                        <asp:BoundField DataField="HORA" HeaderText="Hora" SortExpression="HORA"/>
                                                        <asp:BoundField DataField="ASISTENCIA" HeaderText="Asistencia" SortExpression="ASISTENCIA"/>
                                                        <asp:BoundField DataField="ACTIVA" HeaderText="Activa" SortExpression="ACTIVA" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn"/>
                                                        <asp:TemplateField headertext="">
                                                        <ItemTemplate>
                                                          <asp:LinkButton CommandName="Modificar" CommandArgument='<%# Eval("ID") %>' class="btn btn-info" runat="server"><i class="fa fa-edit"></i> Modificar</asp:LinkButton>
                                                          <asp:Button ID="btnHabilitar" runat="server" text="" CssClass="" CommandName="Deshabilitar" CommandArgument='<%# Eval("ID") %>' OnClientClick="return Confirmar();" />
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
              <!--<input id="dtFecha" type="date" class="form-control" runat="server" value="" />-->
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
               <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                     <asp:Calendar ID="MyCalendario" runat="server" BackColor="White" BorderColor="White" BorderWidth="1px" Font-Names="Verdana" Font-Size="10pt" ForeColor="Black" Height="190px" NextPrevFormat="FullMonth" Width="350px" OnDayRender="MyCalendario_DayRender" DayNameFormat="FirstTwoLetters" FirstDayOfWeek="Monday" Font-Strikeout="False" OnSelectionChanged="MyCalendario_SelectionChanged" SelectorStyle-BackColor="Red">
                                                <DayHeaderStyle Font-Bold="True" Font-Size="8pt" />
                                                <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" VerticalAlign="Bottom" />
                                                <OtherMonthDayStyle ForeColor="#999999" />
                                                <SelectedDayStyle BackColor="Red" ForeColor="White" />
                                                <TitleStyle BackColor="White" BorderColor="Black" BorderWidth="4px" Font-Bold="True" Font-Size="12pt" ForeColor="#333399" />
                                                <TodayDayStyle BackColor="#CCCCCC" />
                                            </asp:Calendar>
                        </ContentTemplate>
                </asp:UpdatePanel>
                 
              </div>
               <div class="form-group">
                <label for="">Horas Disponibles</label>
                      <asp:DropDownList ID="cmbHora" CssClass="form-control" runat="server">                                            
                      </asp:DropDownList>
              </div>
            <div class="form-group">
                <label for="">Asistencia</label>
                <asp:DropDownList ID="dpAsistencia" CssClass="form-control" runat="server">
                     <asp:ListItem Value="0">No</asp:ListItem>
                     <asp:ListItem Value="1">Si</asp:ListItem>
                </asp:DropDownList>
            </div>
          </div>
        <div class="modal-footer">
             <button type="button" class="btn btn-default" data-dismiss="modal">Cancelar</button>
            <button type="button" class="btn btn-success" runat="server" onserverclick="btnModificar_Click"><i class="fa fa-floppy-o"></i> Guardar</button>
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
             return confirm('¿Desea modificar esta cita?') == true;
         }
 </script>
</body>

</html>
