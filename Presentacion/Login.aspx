<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Presentacion.Login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" lang="es">
<head runat="server">
    <meta charset="UTF-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <meta http-equiv="Content-Type" content="ie=edge"/>
    <title>Autentación usuario</title>
    <!-- CSS Bootstrap-->
      <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.1.3/css/bootstrap.min.css"/>
    <!-- Personal Style-->
      <link rel="stylesheet" href="assets/css/main-autentication.css"/>
</head>
<body>
    <div class="container">
        <h4>SAFE</h4>
    </div>
    <div class="container">
        <form method="POST" runat="server">
         <div class="row">
             <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                 
                     <div class="form-title">
                        <h3>Ingresar</h3>
                     </div>
                     <!-- Alert -->
                     <div class="alert alert-danger" id="alerta" runat="server">
                         <asp:Label ID="lblAlertMsge" runat="server" Text=""></asp:Label>
                    </div> 
                    <!-- /Alert -->              
                     <div class="form-group">   
                         <asp:TextBox class="form-control" type="email" ID="inputEmail" placeholder="Email" required="required" runat="server"></asp:TextBox>            
                     </div>
                     <div class="form-group">
                         <asp:TextBox ID="inputPswd" type="password" class="form-control" placeholder="Contraseña" required="required" runat="server"></asp:TextBox>
                     </div>  
             </div>             
         </div><!--row-->
         <div class="row">
             <asp:Button ID="btnLogin" runat="server" class="btn btn-primary" Text="Iniciar sesión" OnClick="btnLogin_Click" />
         </div>
            <div class="row">
              <div class="form-group">
                 <a href="Consulta.aspx">Consulta trabajador</a>
              </div>
            </div>
            
           
         </form>
    </div><!--container-->
    


    <!-- JS Bootstrap-->
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.1.3/js/bootstrap.min.js"></script>
    <!-- jQuery -->
    <script src="https://code.jquery.com/jquery-3.3.1.js"></script>
    <!-- Validar usuario
    <script src="assets/js/login-validator.js"></script>-->

</body>
</html>
