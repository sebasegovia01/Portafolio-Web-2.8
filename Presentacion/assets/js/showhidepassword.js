 $(document).ready(function () {

            $('#txtClave').attr('type', 'password');

            $('#show_password').hover(function show() {
                //Cambiar el atributo a texto
                $('#txtClave').attr('type', 'text');
                $('.icon').removeClass('fa fa-eye-slash').addClass('fa fa-eye');
            },
            function () {
                //Cambiar el atributo a contraseña
                $('#txtClave').attr('type', 'password');
                $('.icon').removeClass('fa fa-eye').addClass('fa fa-eye-slash');
            });
            //CheckBox mostrar contraseña
            $('#ShowPassword').click(function () {
                $('#Password').attr('type', $(this).is(':checked') ? 'text' : 'password');
            });
        });