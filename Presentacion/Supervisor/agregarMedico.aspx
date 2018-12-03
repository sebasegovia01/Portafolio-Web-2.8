<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="agregarMedico.aspx.cs" Inherits="Presentacion.Supervisor.agregarMedico" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
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
    <style>

        .ctnGenerador {
            display: inline-flex;
        }

        #btnGenerarPassword {
           
        }
      

    </style>
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
                                    Añadir Médico
                                </div>
                                <div class="panel-body">   
                                    <form id="form1" runat="server">
                        <div class="col-lg-6">    
                                        <div class="form-group">
                                            <label for="">Empresa</label>
                                            <asp:DropDownList ID="cmbEmpresa" CssClass="form-control" runat="server"></asp:DropDownList>
                                        </div>  
                                        <div class="form-group">
                                            <label for="">Rut*</label>
                                            <asp:TextBox class="form-control" ID="txtRut" placeholder="" runat="server"></asp:TextBox>
                                        </div>       
                                        <div class="form-group">
                                            <label for="">Apellido materno*</label>
                                            <asp:TextBox class="form-control" ID="txtApMaterno" placeholder="" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label for="">Correo*</label>
                                            <asp:TextBox type="email" class="form-control" ID="txtCorreo" placeholder="Ejemplo: correo@empresa.cl" runat="server"></asp:TextBox>
                                        </div> 
                                        <div class="form-group">
                                        <div class="row">   
                                        <div class="col-lg-12">
                                            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                             <label for="">Contraseña*</label>
                                             <div class="input-group">
                                                 <div class="row">
                                                     <div class="col-md-6">
                                                         <asp:TextBox type="text" class="form-control" ID="txtClave" placeholder="" runat="server"></asp:TextBox><br />
                                                     </div>
                                                       <div class="col-md-6">
                                                         <asp:Button ID="btnGenerarPassword" class="form-control" runat="server" CssClass="btn btn-default" Text="Generar Contraseña" OnClick="btnGenerarPassword_Click" />
                                                       </div>
                                                 </div>                                       
                                                </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            
                                        </div>
                                        </div>
                                        </div>  
                                                                 
                                </div>
                                <div class="col-lg-6">
                                        <div class="form-group">
                                            <label for="">Nombre*</label>
                                            <asp:TextBox class="form-control" ID="txtNombre" placeholder="" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                                <label for="">Apellido paterno*</label>
                                            <asp:TextBox ID="txtApPaterno" class="form-control" placeholder="" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                                <label for="">Fecha de nacimiento*</label>
                                            <asp:TextBox type="date" class="form-control" ID="dtNacimiento" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                                <label for="">Telefono</label>
                                            <asp:TextBox ID="txtFono" class="form-control" placeholder="Ejemplo: (+569) 4424552233" runat="server" MaxLength="17"></asp:TextBox>
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
                 

				<footer><p>Portafolio 2018</p></footer>
            </div>
            <!-- /. PAGE INNER  -->
        </div>
        <!-- /. PAGE WRAPPER  -->
    </div>
    <!-- /. WRAPPER  -->

    <!-- jQuery Js -->
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <!-- Bootstrap Js -->
    <script src="../assets/js/bootstrap.min.js"></script>
    <!-- Metis Menu Js -->
    <script src="../assets/js/jquery.metisMenu.js"></script>
    <!-- Morris Chart Js -->
    <script src="../assets/js/morris/raphael-2.1.0.min.js"></script>
    <script src="../assets/js/morris/morris.js"></script>
    <!-- Custom Js -->
    <script src="../assets/js/custom-scripts.js"></script>
    <!-- show/hide password -->
    <script src="../assets/js/showhidepassword.js"></script>
    <!-- user Script -->
    <script src="../assets/js/userScript.js"></script>

</body>

</html>

