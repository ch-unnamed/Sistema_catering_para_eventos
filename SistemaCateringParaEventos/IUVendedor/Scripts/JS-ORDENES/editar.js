
let resultado = 0;
$("#tabla tbody").on("click", '.btn-editar', async function () {

    filaSeleccionada = $(this).closest("tr");

    // DATA DE LA TABLA COTIZACION_MENU_PLATO
    var data = tabledata.row(filaSeleccionada).data(); 

    // Si ya se concreto no dejo que se pueda poner en esperar para evitar confictos
    if (data.Estado === 2) {
        return;
    }

    // DATA DE LOS PLATOS
    resultado = await consultarPlatos(data.Id);

    abrirModal(data);
});

async function descontarStock(resultado, menu) {

    const ids = resultado.map(item => item.Plato.Id);

    const response = await fetch('/Home/DescontarCantidadInsumo', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(ids)
    });

    if (response.ok) {
        swal("¡Orden atendida Exitosamente!", "Presiona OK para continuar", "success");
        return true;
    } else {
        swal("¡Error al descontar stock!", `Cargue insumos al Menu Nº${menu}`, "error");
        document.querySelector(".btn-cargar-insumos").classList.remove('d-none');
        return false;
    }
}

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

    if (CotizacionMenu.Estado === 2) {
        const success = await descontarStock(resultado.data, CotizacionMenu.Menu.Id);
        if (!success) return; 
    }

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