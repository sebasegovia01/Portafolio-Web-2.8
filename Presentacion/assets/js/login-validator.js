 $(document).ready(function () {
         
         

        $("#btnLogin").click(function (e) { 
            e.preventDefault();

         LimpiarAlertas();   

       if($("#inputEmail").val() == ""){
         Alerta("alert alert-danger", "Ingrese correo");
         $("#inputEmail").focus();
         return false;

       } else if($("#inputEmail").val().indexOf('@', 0) == -1 || $("#inputEmail").val().indexOf('.', 0) == -1) {
            Alerta("alert alert-danger","Ingrese un correo válido");
            return false;

        } 

        if($("#inputPswd").val() == "") {
            Alerta("alert alert-danger", "Ingrese contraseña");
         $("#inputPswd").focus();
         return false;
        } 
     }); //btn-login

     
     //Functions

     function Alerta(tipo, mensaje) {

        $("#personal-alert").append('<div class="'+tipo+'"'+
              '<strong>'+mensaje+'</strong>'+
              '</div>');  
        
        $("#persona-alert").show();
     }


     function LimpiarAlertas() {

         $("#personal-alert").empty();
     } 


    });//document