

    // ver fila para editar
    $("#tabla tbody").on("click", '.btn-editar', function () {

        filaSeleccionada = $(this).closest("tr");
        
        var data = tabledata.row(filaSeleccionada).data(); 

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

    // Impido que el usuario seleccione una fecha q no sea de una anticipacion de 7 dias
    document.addEventListener("DOMContentLoaded", function () {
        const inputFecha = document.getElementById("fecha");
        const hoy = new Date();
        hoy.setDate(hoy.getDate() + 7); 

        const yyyy = hoy.getFullYear();
        const mm = String(hoy.getMonth() + 1).padStart(2, '0');
        const dd = String(hoy.getDate()).padStart(2, '0');

        const fechaMinima = `${yyyy}-${mm}-${dd}`;
        inputFecha.min = fechaMinima;
    });