    async function guardar() {

        // trabajamos de la misma forma que tabledata
        var Cliente = {
            IdCliente: $("#id").val(),
            Nombre: $("#nombre").val(),
            Apellido: $("#apellido").val(),
            Dni: $("#dni").val(),
            Email: $("#email").val(),
            Region: $("#region").val(),
            Telefono: $("#telefono").val(),
            Tipo_Cliente: {
                Nombre: $("#tipo_cliente option:selected").text()
            }

        };
        console.log("Cliente antes de enviar:", Cliente);

        try {
            const response = await fetch("/Home/GuardarCliente", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json; charset=utf-8"
                },
                body: JSON.stringify(Cliente)
            });

            if (!response.ok) {
                throw new Error("Error en la solicitud fetch");
            }

            const data = await response.json();

            // limpiar errores
            $(".text-danger").text("");

            if (Cliente.IdCliente == 0) {
                // se crea un usuario

                if (data.resultado != 0) {
                    console.log(`a el id de cliente se le asigna${data.resultado}`);
                    Cliente.IdCliente = data.resultado; // asignar el id al cliente
                    console.log(`${data.resultado}`);
                    tabledata.row.add(Cliente).draw(false); // agregar la fila a la tabla

                    $("#FormModal").modal("hide");
                    swal("¡Cliente Creado Exitosamente!", "Presiona OK para continuar", "success");
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

            } else {
                // se edita un usuario
                if (data.resultado) {
                    // se edito correctamente
                    tabledata.row(filaSeleccionada).data(Cliente).draw(false);
                    filaSeleccionada = null;

                    $("#FormModal").modal("hide");
                    swal("¡Cliente Editado Exitosamente!", "Presiona OK para continuar", "success");
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