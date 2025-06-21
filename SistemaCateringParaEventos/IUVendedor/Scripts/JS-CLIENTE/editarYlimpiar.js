// Funcionalidad de que al hacer click en editar se muestren los valores de la fila seleccionada
$("#tabla tbody").on("click", '.btn-editar', function () {

    filaSeleccionada = $(this).closest("tr");

    var data = tabledata.row(filaSeleccionada).data(); // los datos de la fila seleccionada

    console.log(data);

    abrirModal(data);
});

// LIMPIAR MENSAJES DE ERROR

$(document).ready(function () {
    // sacar error si el usuario lo corrige
    $("input").on("input change", function () {
        const idCampo = $(this).attr("id");
        $(`#error-${idCampo}`).text("");
    });
});
// borrar si se cierra
$("#btnCerrar").on("click", function () {
    $(".text-danger").text("");
});