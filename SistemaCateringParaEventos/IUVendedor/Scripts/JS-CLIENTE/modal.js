
    function abrirModal(json) {

        // Limpiar 
        $("#id").val(0);
        $("#nombre").val("");
        $("#apellido").val("");
        $("#dni").val("");
        $("#email").val("");
        $("#localidad").val("");
        $("#provincia").val("");
        $("#telefono").val("");
        $("#tipo_cliente").val("");


        $("#msjError").hide();


        if (json != null) {

            // asingar valores en base al json

            $("#id").val(json.IdCliente);
            $("#nombre").val(json.Nombre);
            $("#apellido").val(json.Apellido);
            $("#dni").val(json.Dni);
            $("#email").val(json.Email);
            $("#localidad").val(json.Localidad.Nombre);
            $("#provincia").val(json.Localidad.Provincia.Nombre);
            $("#telefono").val(json.Telefono);
            $("#tipo_cliente").val(json.Tipo_Cliente.Nombre == 'Personal' ? 1 : 2);

        }

        $("#FormModal").modal("show");
    }