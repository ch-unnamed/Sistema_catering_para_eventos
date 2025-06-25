
    $("#tabla tbody").on("click", '.btn-eliminar', function () {

        var usuarioSeleccionado = $(this).closest("tr");

        var data = tabledata.row(usuarioSeleccionado).data(); // los datos de la fila seleccionada

        console.log(data);

        swal({
            title: "¿Esta Seguro?",
            text: "¿Queres Eliminar el Cliente?",
            type: "warning",
            showCancelButton: true,
            confirmButtonClass: "btn-primary",
            confirmButtonText: "Si",
            cancelButtonText: "No",
            closeOnConfirm: true
        },
            async function () {
                try {
                    const response = await fetch("/Home/EliminarCliente", {
                        method: "POST",
                        headers: {
                            "Content-Type": "application/json; charset=utf-8"
                        },
                        body: JSON.stringify({ id: data.IdCliente })

                    });

                    if (!response.ok) {
                        throw new Error("Error al realizar la solicitud al servidor.")
                    }

                    const result = await response.json();

                    if (result.resultado) {
                        tabledata.row(usuarioSeleccionado).remove().draw(false);
                    } else {
                        Swal.fire({
                            icon: "error",
                            title: "No se pudo eliminar",
                            text: result.mensaje
                        });
                    }

                } catch (error) {
                    $("#msjError").text("Error con fetch");
                    console.error("Error en la solicitud fetch:", error);
                    swal("Error", "Faltan completar campos", "error");
                }
            });

    });