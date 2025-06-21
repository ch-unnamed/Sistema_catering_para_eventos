
    function abrirModal(json) {

        $("#id").val(0);
        $("#nombre").val("");
        $("#capacidad").val("");
        $("#tipo").val("");
        $("#calle").val("");
        $("#altura").val("");
        $("#ciudad").val("");
        $("#provincia").val("");
        $("#fecha").val("");

        // por primera vez no mostrar error, ya que no estan los campos pero se van a completar
        $("#msjError").hide();

        // si el json contiene valores presiono editar
        if (json != null) {
            $("#id").val(json.IdEvento);
            $("#nombre").val(json.Nombre);
            $("#capacidad").val(json.Capacidad);
            $("#tipo").val(json.Tipo_Evento.Nombre);
            // Verifica si Ubicacion está definida
            if (json.Ubicacion) {
                $("#calle").val(json.Ubicacion.Calle);
                $("#altura").val(json.Ubicacion.Altura);
                $("#ciudad").val(json.Ubicacion.Ciudad);
                $("#provincia").val(json.Ubicacion.Provincia);
            }
            $("#fecha").val(json.Fecha);
        }

        $("#FormModal").modal("show");
    }