async function abrirModal(json) {

    await cargarEstados();

    // cargar luego de que los selects estan listos
    $("#id").val(json?.IdCotizacion || 0);
    $("#idEvento").val(json?.IdEvento || 0);
    $("#dni").val(json?.Cliente?.Dni || "");
    $("#menu").val(json?.Menu?.Id || "");
    $("#fechaRealizacion").val(json?.FechaRealizacion || "");
    $("#total").val(json?.Total || "");
    $("#estado").val(json?.Estado?.IdEstado || "");

    $("#FormModal").modal("show");
}