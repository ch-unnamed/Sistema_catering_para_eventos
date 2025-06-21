
    // Funcionalidad de crear evento o editarlo
    async function guardar() {

        const Evento = {
            IdEvento: $("#id").val(),
            Nombre: $("#nombre").val(),
            Capacidad: $("#capacidad").val(),
            Tipo_Evento: {
                Nombre: $("#tipo").val(),
            },
            Ubicacion: {
                Calle: $("#calle").val(),
                Altura: $("#altura").val(),
                Ciudad: $("#ciudad").val(),
                Provincia: $("#provincia").val(),
            },
            Fecha: $("#fecha").val(),
        };

        console.log("Evento antes de enviar:", Evento);

        try {
            
            const response = await fetch("/Home/GuardarEvento", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json; charset=utf-8"
                },
                body: JSON.stringify(Evento)
            });

            if (!response.ok) {
                throw new Error("Error en la solicitud");
            }

            const data = await response.json();

            // limpiar errores
            $(".text-danger").text("");

            if (Evento.IdEvento == 0 || Evento.IdEvento === "" || Evento.IdEvento === null) {

                // se crea un evento
                if (data.resultado != 0) {
                    Evento.IdEvento = data.resultado;

                    tabledata.row.add(Evento).draw(false); // agregar la fila a la tabla

                    $("#FormModal").modal("hide");
                    swal("¡Evento Creado Exitosamente!", "Presiona OK para continuar", "success");
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
                // se edita un evento existente

                if (data.resultado) {
                    tabledata.row(filaSeleccionada).data(Evento).draw(false);
                    filaSeleccionada = null;
                    $("#FormModal").modal("hide");
                    swal("¡Evento Editado Exitosamente!", "Presiona OK para continuar", "success");
                } else {
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