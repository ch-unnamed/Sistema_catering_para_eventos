   

async function abrirModal(json) {
    let btnGuardarCotizacion = document.getElementById("btnGuardarSeleccion");
    btnGuardarCotizacion.classList.remove('d-none');
    document.querySelectorAll('.mt-3.fw-bold.mensaje-menu-extra').forEach(el => {
        el.remove();
    });
    document.querySelectorAll('*').forEach(el => {
        if (el.querySelector('#menu')) {
            el.remove();
        }
    });
        await cargarEstados();

        document.querySelectorAll('[id*="contenedor-platos-"]').forEach(elemento => elemento.remove());
        document.querySelectorAll(".menu-dinamico").forEach(menu => menu.remove());
        document.getElementById("contenedor-pregunta").classList.add('d-none');

        $("#id").val(0);
        $("#idEvento").val("");
        $("#idCliente").val("");
        $("#menu").val("");
        $("#fechaRealizacion").val("");
        $("#total").val("");
        $("#estado").val("");

        if (json != null) {
            document.querySelectorAll('.mt-3.fw-bold.mensaje-menu-extra').forEach(el => {
                el.remove();
            });
            document.querySelectorAll('*').forEach(el => {
                if (el.querySelector('#menu')) {
                    el.remove();
                }
            });

            btnGuardarCotizacion.classList.add('d-none');
            // cambio los nombres
            $("label[for='idEvento']").text("Evento (Nombre)");
            $("label[for='idCliente']").text("Cliente (DNI)");

            // cambio el tipo de input para que me acepte el nombre del evento
            $("#idEvento").attr("type", "text");

            // impido q pueda editar el evento y cliente, solo se podra el estado y fecha
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
        
            // cargo los datos
            $("#id").val(json?.CotizacionID || json?.IdCotizacion);
            $("#idEvento").val(json?.Evento.Nombre || "");
            $("#idCliente").val(json?.Cliente.Dni || "");
            $("#fechaRealizacion").val(json?.FechaRealizacion || "");
            $("#total").val(json?.Total != null ? parseFloat(json.Total).toLocaleString('es-AR', { style: 'currency', currency: 'ARS' }) : "");
            const estados = {
                "Completado": "1",
                "Confirmado": "2",
                "Pendiente": "3",
                "Rechazado": "4"
            };

            const estadoNombre = json?.Estado?.Nombre;
            $("#estado").val(estados[estadoNombre] || "0");

        }
        else {

            // VUELVO A PONER TODO COMO ESTA EN EL HTML

            $("label[for='idEvento']").text("Evento (ID)");
            $("label[for='idCliente']").text("Cliente (ID)");

            $("#idEvento").attr("type", "number");

            $("#idEvento").prop("readonly", false)
                .removeAttr("data-bs-toggle")
                .removeAttr("title")
                .css({
                    "background-color": "",
                    "pointer-events": "",
                    "user-select": "",
                    "cursor": ""
                });

            $("#idCliente").prop("readonly", false)
                .removeAttr("data-bs-toggle")
                .removeAttr("title")
                .css({
                    "background-color": "",
                    "pointer-events": "",
                    "user-select": "",
                    "cursor": ""
                });
        }

        $("#FormModal").modal("show");
    }