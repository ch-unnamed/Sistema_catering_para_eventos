
    async function abrirModal(json) {

        $("#menuNuevo").remove();
        const modal = document.getElementById("FormModal");
        const btnGuardarCotizacion = document.getElementById("btnGuardarSeleccion");
        btnGuardarCotizacion.classList.remove('d-none');

        // eliminar mensajes y menus si es que existian
        document.querySelectorAll('.mensaje-menu-extra').forEach(el => el.remove());
        document.querySelectorAll(".menu-dinamico, [id*='contenedor-platos-']").forEach(el => el.remove());
        document.getElementById("contenedor-pregunta").classList.add('d-none');

        // Resetear todos los inputs dentro del modal por prevencion 
        const inputs = modal.querySelectorAll("input, select, textarea");
        inputs.forEach(input => {
            const type = input.type;
            if (type === "checkbox" || type === "radio") {
                input.checked = false;
            } else {
                input.value = "";
            }
        });

        $("#estado").val("0");
        $("#total").val("0");

        // Si es nuevo (sin datos JSON)
        if (json == null) {

            await cargarEstados();

            // Cambiar nombre de Labels para ingresar lo adecuado
            $("label[for='idEvento']").text("Evento (ID)");
            $("label[for='idCliente']").text("Cliente (ID)");

            // cambiar input
            $("#idEvento").attr("type", "number");

            // Habilitar inputs
            $("#idEvento, #idCliente").prop("readonly", false)
                .removeAttr("data-bs-toggle")
                .removeAttr("title")
                .css({
                    "background-color": "",
                    "pointer-events": "",
                    "user-select": "",
                    "cursor": ""
                });
        } else {

            await cargarEstados(1);

            // Si viene con datos JSON, cargar los valores
            btnGuardarCotizacion.classList.add('d-none');

            $("label[for='idEvento']").text("Evento (Nombre)");
            $("label[for='idCliente']").text("Cliente (DNI)");
            $("#idEvento").attr("type", "text");

            $("#idEvento").prop("readonly", true)
                .attr("data-bs-toggle", "tooltip")
                .css({
                    "background-color": "#e9ecef",
                    "pointer-events": "none",
                    "user-select": "none",
                    "cursor": "not-allowed"
                });

            $("#idCliente").prop("readonly", true)
                .attr("data-bs-toggle", "tooltip")
                .css({
                    "background-color": "#e9ecef",
                    "pointer-events": "none",
                    "user-select": "none",
                    "cursor": "not-allowed"
                });

            $('[data-bs-toggle="tooltip"]').tooltip();

            // Cargar datos del JSON
            $("#id").val(json?.CotizacionID || json?.IdCotizacion);
            $("#idEvento").val(json?.Evento?.Nombre || "");
            $("#idCliente").val(json?.Cliente?.Dni || "");
            $("#fechaRealizacion").val(json?.FechaRealizacion || "");
            $("#total").val(
                json?.Total != null
                    ? parseFloat(json.Total).toLocaleString('es-AR', { style: 'currency', currency: 'ARS' })
                    : ""
            );

            const estados = {
                "Completado": "1",
                "Confirmado": "2",
                "Pendiente": "3",
                "Rechazado": "4"
            };
            const estadoNombre = json?.Estado?.Nombre;
            $("#estado").val(estados[estadoNombre] || "0");
        }  

        // Mostrar modal
        $("#FormModal").modal("show");
    }