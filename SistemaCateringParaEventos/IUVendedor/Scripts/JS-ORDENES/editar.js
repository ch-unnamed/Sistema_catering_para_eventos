$("#tabla tbody").on("click", '.btn-editar', function () {

    filaSeleccionada = $(this).closest("tr");

    var data = tabledata.row(filaSeleccionada).data(); 

    abrirModal(data);
});

async function editar() {

    var estadoActual = parseInt($("#estado").val());

    // editar el valor del estado en base al del html
    var nuevoEstado = estadoActual === 1 ? 2 : 1;

    // trabajar de la misma forma que tabledata

    var CotizacionMenu = {
        Id: $("#id").val(),
        Cotizacion: {
            IdCotizacion: $("#cotizacionId").val()
        },
        Menu: {
            Id: $("#menuId").val()
        },
        Estado: nuevoEstado

    };

    try {
        const response = await fetch("/Home/EditarCotizacionMenu", {
            method: "POST",
            headers: {
                "Content-Type": "application/json; charset=utf-8"
            },
            body: JSON.stringify(CotizacionMenu)
        });

        if (!response.ok) {
            throw new Error("Error en la solicitud fetch");
        }

        const data = await response.json();

        // limpiar errores
        $(".text-danger").text("");

        if (CotizacionMenu.Id == 0) {
            // se crea una cotizacion-menu

            $("#FormModal").modal("hide");
            swal("¡ERROR!", "No se pueden crear Cotizacion-Menus", "error");

        } else {
            // se edita un usuario
            if (data.resultado) {
                // se edito correctamente
                tabledata.row(filaSeleccionada).data(CotizacionMenu).draw(false);
                filaSeleccionada = null;

                $("#FormModal").modal("hide");
                swal("¡Cotizacion-Menu Editado Exitosamente!", "Presiona OK para continuar", "success");
            } else {
                // mostrar errores por campo
                if (data.errores) {
                    for (const campo in data.errores) {
                        $(`#error-${campo}`).text(data.errores[campo]);
                    }
                } else {
                    $("#msjError").text("Ocurrió un error desconocido").show();
                }
                return;
            }
        }

    } catch (error) {
        Swal.fire({
            icon: "error",
            title: "Hubo un error inesperado",
            text: error
        });
    }

}