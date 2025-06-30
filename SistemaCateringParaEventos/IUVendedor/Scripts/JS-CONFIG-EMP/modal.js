    function abrirModal(json) {

        // limpiar
        $("#id").val(0);
        $("#nombre").val("");
        $("#porcentaje").val("");


        $("#msjError").hide();


        if (json != null) { 

            // valor del json
            $("#id").val(json.ID);
            $("#nombre").val(json.Nombre);
            $("#porcentaje").val(json.Porcentaje);
        }

        $("#FormModal").modal("show");
    }