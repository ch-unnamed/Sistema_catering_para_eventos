
function abrirModal(json) {

    
    $("#id").val(0);
    $("#cotizacionId").val("");
    $("#menuId").val("");
    $("#dni").val("");
    $("#email").val("");
    $("#region").val("");
    $("#telefono").val("");
    $("#tipo_cliente").val(""); // asignar segun el value


    $("#msjError").hide();


    if (json != null) { // entra aca si le da a editar, ya que habrian datos en la fila

        $("#id").val(json.Id);
        $("#cotizacionId").val(json.Cotizacion.IdCotizacion);
        $("#menuId").val(json.Menu.Id);
        $("#estado").val(json.Estado === 1 ? 2 : 1);

    }

    $("#FormModal").modal("show");
}