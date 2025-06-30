
    let btnEditar = document.getElementById("btnEditarSeleccion");
    let btnGuardarCotizacion = document.getElementById("btnGuardarSeleccion");

    $('#tablaCotizacion tbody').on('click', '.btn-editar', function () {

        btnGuardarCotizacion.classList.add('d-none');
        btnEditar.classList.remove('d-none');

        btnGuardarCotizacion.classList.add('d-none');
        var data = tabledata.row($(this).closest('tr')).data();

        abrirModal(data)

        // si se quiere editar, solo se podra la fecha y el estado
        document.getElementById("id").value = data.IdCotizacion;
        document.getElementById("fechaRealizacion").value = data.FechaRealizacion?.split("T")[0] || "";
        document.getElementById("estado").value = data.Estado.IdEstado || "";

    });

    // Cuando crea una nueva cotizacion oculto lo de editar
    document.getElementById('crearNuevo').addEventListener('click', async () => {
        btnEditar.classList.add('d-none');
        btnGuardarCotizacion.classList.remove('d-none');
    });

    btnGuardarCotizacion.addEventListener("click", function () {
        const eventoID = document.getElementById("idEvento").value;
        const clienteID = document.getElementById("idCliente").value;
        const fecha = document.getElementById("fechaRealizacion").value;
        const estado = document.getElementById("estado").value;

        if (eventoID == 0 || clienteID == 0 || fecha == 0 || estado == 0) {
            swal("Error", "Faltan completar campos", "error");
        }

    });

    document.getElementById("btnEditarSeleccion").addEventListener("click", async () => {
        const idCotizacion = parseInt(document.getElementById("id").value);
        const fechaRealizacion = document.getElementById("fechaRealizacion").value;
        const estadoId = parseInt(document.getElementById("estado").value);

        if (!idCotizacion || !fechaRealizacion || !estadoId) {
            swal("¡Faltan datos para editar la cotización!", "Presiona OK para continuar", "error");
            return;
        }

        const cotizacion = {
            IdCotizacion: idCotizacion,
            FechaRealizacion: fechaRealizacion,
            Estado: {
                IdEstado: estadoId
            }
        };

        try {
            const response = await fetch("/Home/EditarCotizacion", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify(cotizacion)
            });

            const result = await response.json();

            if (result.resultado) {
                swal("¡Editado!", result.mensaje, "success");
                tabledata.ajax.reload(null, false); 

                // actualizar fila de datatable
                const rowIndex = tabledata.rows().indexes().toArray().find(i => tabledata.row(i).data().IdCotizacion === idCotizacion);

                if (rowIndex !== undefined) {

                    // actualizar los campos modificados
                    const rowData = tabledata.row(rowIndex).data();
                    rowData.FechaRealizacion = fechaRealizacion;
                    rowData.Estado.IdEstado = estadoId;
                    rowData.Estado.Nombre = document.querySelector(`#estado option[value="${estadoId}"]`)?.textContent || rowData.Estado.Nombre;

                    tabledata.row(rowIndex).data(rowData).draw(false);
                }

                $("#FormModal").modal("hide");
            } else {
                swal("Error", result.mensaje, "error");
            }

        } catch (error) {
            swal("Error", "No se pudo editar la cotización, revise los campos.", "error");
        }
    });

    // ELIMINAR COTIZACIONs
    $('#tablaCotizacion tbody').on('click', '.btn-eliminar', function () {
        const fila = $(this).closest('tr');
        const data = tabledata.row(fila).data();

        swal({
            title: "¿Está seguro?",
            text: "¿Querés eliminar la cotización?",
            type: "warning",
            showCancelButton: true,
            confirmButtonClass: "btn-primary",
            confirmButtonText: "Sí",
            cancelButtonText: "No",
            closeOnConfirm: false
        }, async function () {
            try {
                const response = await fetch('/Home/EliminarCotizacion', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify({ cotizacion_id: data.IdCotizacion })  
                });

                const result = await response.json();

                if (result.resultado) {
                    tabledata.row(fila).remove().draw(false);
                    swal("Eliminada", result.mensaje, "success");
                } else {
                    swal("No se pudo eliminar", result.mensaje, "error");
                }
            } catch (error) {
                swal("Error", error, "error");
            }
        });
    });