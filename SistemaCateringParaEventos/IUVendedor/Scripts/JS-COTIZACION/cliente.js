
    let clienteValido = false; 

    async function consultarCantidadClientes(clienteId) {
        try {
            const response = await fetch(`/Home/cantidadCliente?cliente_id=${clienteId}`);
            const data = await response.json();
            let cantidad = data.data;
            return cantidad ? cantidad : 0;
        } catch (error) {
            console.error(`Error al obtener el cliente`, error);
            return 0;
        }
    }

    document.addEventListener("DOMContentLoaded", function () {

        const clienteId = document.getElementById("idCliente");

        clienteId.addEventListener("input", async function () {
            let cantidad = await consultarCantidadClientes(clienteId.value);
            let mensajeExistente = document.getElementById("mensajeClienteNoExiste");

            if (mensajeExistente) mensajeExistente.remove();

            if (cantidad === 0) {
                clienteValido = false;

                let mensaje = document.createElement("div");
                mensaje.id = "mensajeClienteNoExiste";
                mensaje.textContent = "El cliente no existe";
                mensaje.style.color = "red";
                mensaje.style.fontSize = "12px";
                mensaje.style.marginTop = "5px";

                clienteId.parentNode.insertBefore(mensaje, clienteId.nextSibling);

                // desactivo si no existe el cliente
                console.log("Ahora cliente no tiene valor");
                document.querySelectorAll(".menu-dinamico select").forEach(sel => sel.disabled = true);
                document.querySelectorAll('[id*="contenedor-platos-menu-"]').forEach(div => {
                    div.style.pointerEvents = "none";
                    div.style.userSelect = "none";
                    div.style.opacity = "0.5";
                    div.style.filter = "greenscale(100%)";
                });
                // desmarco todos los checkboxes de platos
                document.querySelectorAll('input[type="checkbox"][id^="plato_"]').forEach(checkbox => {
                    checkbox.checked = false;
                });

                // el total a 0 ya que si cambia de cliente puede ser otro con otro descuento
                document.getElementById("total").value = 0;
                totalPrecioBase = 0; // resetear el precio base

            } else {
                clienteValido = true;

                document.querySelectorAll(".menu-dinamico select").forEach(sel => sel.disabled = false);
                document.querySelectorAll('[id*="contenedor-platos-menu-"]').forEach(div => {
                    div.style.backgroundColor = "";
                    div.style.opacity = "";
                    div.style.pointerEvents = "auto";
                });
            }
        });
    });