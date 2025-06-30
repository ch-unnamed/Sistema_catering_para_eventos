
    async function consultarIdAdmin(rolId) {

        try {

            const response = await fetch(`/Home/idVendedor?rol_id=${rolId}`);
            const data = await response.json();
            let idAdmin = data.data.idUsuario;

            if (idAdmin) {
                return idAdmin;
            } else {
                return "";
            }

        } catch (error) {
            return "";
        }
    }

    async function guardar() {

        const admin_id = await consultarIdAdmin(idRol);

        // obj estructurado igual que los datos usados en tabledata
        var Configuracion = {
            ID: $("#id").val(),
            Nombre: $("#nombre").val(),
            Porcentaje: $("#porcentaje").val(),
            Admin: {
                IdUsuario: admin_id
            }
        };

        try {
            const response = await fetch("/Home/GuardarConfiguracion", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json; charset=utf-8"
                },
                body: JSON.stringify(Configuracion)
            });

            if (!response.ok) {
                throw new Error("Error en la solicitud fetch");
            }

            const data = await response.json();

            $(".text-danger").text("");

            if (Configuracion.ID == 0) {
                // se crea una configuracion

                if (data.resultado != 0) {

                    Configuracion.ID = data.resultado; 

                    tabledata.row.add(Configuracion).draw(false);

                    $("#FormModal").modal("hide");
                    swal("¡Configuracion Creada Exitosamente!", "Presiona OK para continuar", "success");
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
                    
                    tabledata.row(filaSeleccionada).data(Configuracion).draw(false);
                    filaSeleccionada = null;

                    $("#FormModal").modal("hide");
                    swal("¡Configuracion Editada Exitosamente!", "Presiona OK para continuar", "success");
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