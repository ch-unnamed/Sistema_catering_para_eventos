
    $("#tabla tbody").on("click", '.btn-eliminar', function () {

        var configuracionSeleccionada = $(this).closest("tr");

        var data = tabledata.row(configuracionSeleccionada).data(); 

        swal({
            title: "¿Esta Seguro?",
            text: "¿Queres Eliminar la Configuracion?",
            type: "warning",
            showCancelButton: true,
            confirmButtonClass: "btn-primary",
            confirmButtonText: "Si",
            cancelButtonText: "No",
            closeOnConfirm: true
        },
            async function () {
                try {
                    const response = await fetch("/Home/EliminarConfiguracion", {
                        method: "POST",
                        headers: {
                            "Content-Type": "application/json; charset=utf-8"
                        },
                        body: JSON.stringify({ idConfiguracion: data.ID })

                    });

                    if (!response.ok) {
                        throw new Error("Error al realizar la solicitud al servidor.")
                    }

                    const result = await response.json();

                    if (result.resultado) {
                        tabledata.row(configuracionSeleccionada).remove().draw(false);
                    } else {
                        Swal.fire({
                            icon: "error",
                            title: "No se pudo eliminar",
                            text: result.mensaje
                        });
                    }

                } catch (error) {
                    $("#msjError").text("Error con fetch");
                    swal("Error", "Faltan completar campos", "error");
                }
            });

    });