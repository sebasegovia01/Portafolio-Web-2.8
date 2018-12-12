<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdministrarRecomendacion.aspx.cs" Inherits="Presentacion.Ingeniero.AdministrarRecomendacion" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Sistema - Recomendación</title>
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
    <!-- DataTable Js -->
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
                            <a href="AdministrarEvaluacion.aspx">Administrar evaluaciones</a>
                            <a href="AdministrarRecomendacion.aspx">Administrar recomendación</a>
                        </li>
                    </ul>
                </li>
                  <li>
                    <a href="#"><i class="fa fa-calendar"></i> Capacitaciones<span class="fa arrow"></span></a>
                    <ul class="nav nav-second-level">
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
                 <!-- /. ROW  -->     
                 <div class="row">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    Evaluaciones
                                </div>
                                <div class="panel-body">   
                                    <form id="form1" runat="server">
                                         <!-- Alert -->
                                              <div class="" id="alerta" runat="server">
                                                 <asp:Label ID="lblAlertMsge" runat="server" Text=""></asp:Label>
                                              </div> 
                                              <!-- /Alert --> 
                                        <div class="col-lg-12">                          
                                            <div class="form-group">
                                                <label for="">Tipo</label><br />
                                                <asp:DropDownList ID="ddlTipo" runat="server" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlTipo_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <br />

                                                <asp:GridView ID="gvLista" class="table table-striped table-bordered table-hover" EmptyDataText="No se han encontrado resultados." runat="server" AutoGenerateColumns="False" OnRowDataBound="gvLista_RowDataBound" OnRowCommand="gvLista_RowCommand">
                                                    <Columns>
                                                        <asp:BoundField DataField="FECHA" HeaderText="Fecha" SortExpression="FECHA" />
                                                        <asp:BoundField DataField="OBSERVACION" HeaderText="Observación" SortExpression="OBSERVACION" />
                                                        <asp:BoundField DataField="RECOMENDACION" HeaderText="Recomendación" SortExpression="RECOMENDACION" />
                                                        <asp:BoundField DataField="AUTORIZADA" HeaderText="Autorizada" SortExpression="AUTORIZADA" />
                                                        <asp:BoundField DataField="EMPLEADO" HeaderText="Empleado" SortExpression="EMPLEADO" />
                                                        <asp:BoundField DataField="EMPRESA" HeaderText="Empresa" SortExpression="EMPRESA" />
                                                        <asp:BoundField DataField="TIPO" HeaderText="Tipo Evaluación" SortExpression="TIPO" />
                                                        <asp:TemplateField headertext="Opciones">
                                                            <ItemTemplate>
                                                                <asp:LinkButton CommandName="Enviar" CommandArgument='<%# Eval("EVALUACION") %>' class="btn btn-primary" OnClientClick="return Confirmar();" runat="server">Agregar</asp:LinkButton>
                                                                <asp:LinkButton CommandName="Modificar" CommandArgument='<%# Eval("EVALUACION") %>' class="btn btn-info" runat="server"><i class="fa fa-edit"></i> Modificar</asp:LinkButton>
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
          <h4 class="modal-title"> Modificar Recomendación</h4>
        </div>
        <div class="modal-body">
          <div class="form-group">
              <asp:HiddenField ID="hdnId" runat="server" />
             </div>
          <div class="form-group">
                <label for="">Recomendación</label>
              <textarea id="txtRecomendacion" class="form-control" cols="20" rows="2" runat="server"></textarea>
            </div>
              <div class="form-group">
                <label for="">Autorización</label>
                <asp:DropDownList ID="cmbAutorizacion" class="form-control" runat="server">
                    <asp:ListItem Value="empty">Selecciona opcion</asp:ListItem>
                    <asp:ListItem Value="1">Si</asp:ListItem>
                    <asp:ListItem Value="0">No</asp:ListItem>
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
             return confirm('¿Desea ingresar su recomendación?') == true;
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
