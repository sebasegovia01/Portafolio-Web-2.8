<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Consulta.aspx.cs" Inherits="Presentacion.Consulta" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<meta name="viewport" content="width=device-width, initial-scale=1.0"/>
<meta http-equiv="X-UA-Compatible" content="ie=edge"/>

  <title>Consulta de Información</title>
    <!-- CSS Bootstrap-->
      <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.1.3/css/bootstrap.min.css" />
    <!-- Personal Style-->
      <link rel="stylesheet" href="assets/css/consulta-style.css" />
</head>
<body>

    <div class="container">
        <h4>SAFE</h4>
    </div>
    <!-- container -->
    <div class="container">
            <!-- form -->
            <form method="POST" runat="server">
                <!-- row -->
         <div class="row">
               
             <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                 
                 
                     <div class="form-title">
                        <h3>Consultar Información</h3>
                     </div>  
                      <!-- Alert -->
            <div class="alert alert-danger" id="alerta" runat="server">
                <asp:Label ID="lblAlertMsge" runat="server" Text=""></asp:Label>
            </div>

                 <div class="form-group">  
                         <label>Ingrese rut trabajador:</label>
                         <input id="txtRut" class="form-control" type="text" placeholder="Rut" maxlength="9" runat="server" required/>
                     </div>
                  <div class="form-group">  
                         <label>Ingrese contraseña:</label>
                         <input type="password" id="txtPaswd" class="form-control" placeholder="*****" runat="server" required/>
                     </div>
             </div><!-- /col-12 -->
             
         </div><!--row-->
         <!--- row -->
         <div class="row">
        <asp:Button ID="btnConsultar" runat="server" class="btn btn-primary" Text="Consultar" OnClick="btnConsultar_Click" />
              </div><!-- /btn-row -->

        </form><!-- /form -->

    </div><!--container-->
    


    <!-- JS Bootstrap-->
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.1.3/js/bootstrap.min.js"></script>
    <!-- jQuery -->
    <script src="https://code.jquery.com/jquery-3.3.1.js"></script>

</body>
</html>
