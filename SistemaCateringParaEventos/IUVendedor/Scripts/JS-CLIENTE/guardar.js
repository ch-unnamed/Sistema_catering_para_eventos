
    // consultar dni
    async function repiteDNI(dniCliente, idCliente) {

        try {

            const response = await fetch(`/Home/repiteDNI?dni=${dniCliente}&id=${idCliente}`);
            const data = await response.json();

            return data.data;

        } catch (error) {
            swal("Error en el fetch de consultar el dni del cliente", error, "error")
        }

    }

    // consultar mail
    async function repiteEmail(emailCliente, idCliente) {

        try {

            const response = await fetch(`/Home/repiteEmail?email=${emailCliente}&id=${idCliente}`);
            const data = await response.json();

            return data.data;

        } catch (error) {
            swal("Error en el fetch de consultar el email del cliente", error, "error")
        }

    }

    // consultar telefono
    async function repiteTelefono(telefonoCliente, idCliente) {

        try {

            const response = await fetch(`/Home/repiteTelefono?telefono=${telefonoCliente}&id=${idCliente}`);
            const data = await response.json();

            return data.data;

        } catch (error) {
            swal("Error en el fetch de consultar el telefono del cliente", error, "error")
        }

    }

    // consultar localidad y provincia
    async function consultarProvincia(nombreLocalidad) {

        const response = await fetch(`/Home/ObtenerProvincia?nombreLocalidad=${nombreLocalidad}`);
        const data = await response.json();

        return data.data.Nombre;

    }

    // COMPLETAR PROVINCIA SI TIENE LA LOCALIDAD

    $("#localidad").on("blur", async function () { // blur es cuando se hace tab o se sale del foco
        const localidad = $(this).val();

        if (localidad.trim() !== "") {
            const provincia = await consultarProvincia(localidad);

            if (provincia) {
                $("#provincia").val(provincia).trigger("change");;
            }
        } else {
            $("#provincia").val("").trigger("change"); // para limpiar el error
        }
    });

    async function guardar() {

        // FORMATO PARA EL TABLEDATA

        var Cliente = {
            IdCliente: $("#id").val(),
            Nombre: $("#nombre").val(),
            Apellido: $("#apellido").val(),
            Dni: $("#dni").val(), Email: $("#email").val(),
            Localidad: {
                Nombre: $("#localidad").val(),
                Provincia:
                {
                    Nombre: $("#provincia").val(),
                },
            },
            Telefono: $("#telefono").val(),
            Tipo_Cliente: {
                Nombre: $("#tipo_cliente option:selected").text()
            }

        };
        console.log("Cliente antes de enviar:", Cliente);


        // ERRORES DE DATOS REPETIDOS

        let id = $("#id").val();
        let dniCliente = $("#dni").val();
        let emailCliente = $("#email").val();
        let telefonoCliente = $("#telefono").val();

        let errores = {};

        if (dniCliente > 0) {
            let dniRepite = await repiteDNI(dniCliente,id);
            if (dniRepite) errores.dni = "El DNI repite";
        }

        if (telefonoCliente > 0) {
            let telefonoRepite = await repiteTelefono(telefonoCliente,id);
            if (telefonoRepite) errores.telefono = "El teléfono repite";
        }

        if (emailCliente?.trim() !== "") {
            let emailRepite = await repiteEmail(emailCliente,id);
            if (emailRepite) errores.email = "El email repite";
        }

        $(`#error-dni`).text(errores.dni ?? "");
        $(`#error-telefono`).text(errores.telefono ?? "");
        $(`#error-email`).text(errores.email ?? "");


        // GUARDAR EL CLIENTE

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
            console.log(data);
            console.log(response);
            // limpiar errores
            //$(".text-danger").text("");

            if (Cliente.IdCliente == 0) {
                // se crea un usuario

                if (data.resultado != 0) {
                    console.log(`a el id de cliente se le asigna${data.resultado}`);
                    Cliente.IdCliente = data.resultado; // asignar el id al cliente
                    console.log(`${data.resultado}`);

                    // cambiar el formato de provincia para mostrarlo en tabledata correctamente
                    let clienteParaTabla = {
                        ...Cliente,
                        Localidad: {
                            Nombre: Cliente.Localidad?.Nombre ?? "",
                            Provincia: {
                                Nombre: Cliente.Localidad?.Provincia?.Nombre ?? ""
                            }
                        },
                        Provincia: {
                            Nombre: Cliente.Localidad?.Provincia?.Nombre ?? ""
                        }
                    };
                    console.log(clienteParaTabla);
                    tabledata.row.add(clienteParaTabla).draw(false); // agregar la fila a la tabla

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