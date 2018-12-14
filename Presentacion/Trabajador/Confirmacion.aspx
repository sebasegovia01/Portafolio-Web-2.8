<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Confirmacion.aspx.cs" Inherits="Presentacion.Trabajador.Confirmacion" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Sistema - Confirmación</title>
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
                        <a href="index.aspx"><i class="fa fa-home"></i> Principal</a>
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
                            <small><a id="btnVolver" href="index.aspx">Volver</a></small>
                        </h1>
                    </div>
                </div>
                 <!-- /. ROW  -->     
                 <div class="row">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    Firmar asistencia a capacitación
                                </div>
                                <div class="panel-body">   
                                    <form id="form1" runat="server">
                                            <!-- Alert -->
                                              <div class="" id="alerta" runat="server">
                                                 <asp:Label ID="lblAlertMsge" runat="server" Text=""></asp:Label>
                                              </div> 
                                              <!-- /Alert --> 
                                        <div id="firmaContainer" runat="server">
                                <div class="col-lg-6">
                                    <div class="form-group">
                                        <label for="">Ingrese firma:</label>
                                    </div>
                                <!-- firma canva -->
                                    <canvas id="sigCanvas" width="400" height="300" style="border: 1px solid black" runat="server">
                                      Explorador no compatible, contactese con el administrador.
                                    </canvas>


                                </div>
			                  <div class="col-md-6">
				                <img id="sigImage" src="" runat="server"/>
			                  </div>
                               <div class="col-md-12">
			                	<textarea id="sigDataUrl" class="form-control" rows="5" runat="server" visible="false"></textarea>
		                    	</div>
                                <div class="col-lg-12">
                                    <br />

                                    <button  type="button" class="btn btn-primary" runat="server" onserverclick="btnFirmar_Click"><i class="fa fa-floppy-o"></i>Generar Firma</button>

                                    <button type="button" class="btn btn-default" id="sigClearBtn">Limpiar</button>
                                     <br />
                                    <br />
                         
                                </div>
                                <div class="col-lg-12">
                                    <asp:Button ID="btnSubirFirma" CssClass="btn btn-success" runat="server" Text="Subir firma" OnClick="btnSubirFirma_Click" Visible="False" />
                                </div>

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
    <!-- Firma Js -->
    <script src="../assets/js/firma.js"></script>

</body>

</html>
