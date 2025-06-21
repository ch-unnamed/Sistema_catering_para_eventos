
    async function consultarCantidadEventos(eventoId) {
        try {
            const response = await fetch(`/Home/cantidadEvento?evento_id=${eventoId}`);
            const data = await response.json();

            let cantidad = data.data;
            if (cantidad) {
                return cantidad;
            } else {
                return 0;
            }

        } catch (error) {
            console.error(`Error al obtener el descuento`, error);
            return 0;
        }
    }

    document.addEventListener("DOMContentLoaded", function () {

        const idEvento = document.getElementById("idEvento");
        const contenedorPregunta = document.getElementById("contenedor-pregunta");

        idEvento.addEventListener("input", async function () {
            const eventoId = this.value;
            console.log(`Se seleccionó el id: ${eventoId}`);

            document.querySelectorAll(".menu-dinamico").forEach(menu => menu.remove());
            contenedorPregunta.classList.toggle("d-none", true);
            document.querySelectorAll('[id*="contenedor-platos-"]').forEach(elemento => elemento.remove());

            let mensajeExistente = document.getElementById("mensajeEventoNoExiste");
            if (mensajeExistente) {
                mensajeExistente.remove();
            }

            let cantidadEventos = await consultarCantidadEventos(eventoId);

            if (cantidadEventos === 0) {
                
                let mensaje = document.createElement("div");
                mensaje.id = "mensajeEventoNoExiste";
                mensaje.textContent = "El evento no existe";
                mensaje.style.color = "red";
                mensaje.style.fontSize = "12px";
                mensaje.style.marginTop = "5px";

                idEvento.parentNode.insertBefore(mensaje, idEvento.nextSibling);

                setTimeout(() => {
                    mensaje.remove();
                }, 3000);
            } else {
                
                try {

                    const response = await fetch(`/Home/ObtenerCapacidadPorIdEvento?evento_id=${eventoId}`);
                    const data = await response.json();
                    const capacidad = data.data.Capacidad;

                    console.log(`La capacidad de este evento es ${capacidad}`);

                    let cantidadMenus = 0;
                    let maximaPlatos = 0;

                    if (capacidad >= 20 && capacidad <= 30) {
                        cantidadMenus = 1;
                        maximaPlatos = 15;
                    } else if (capacidad >= 30 && capacidad <= 60) {
                        cantidadMenus = 2;
                        maximaPlatos = 30;
                    } else if (capacidad >= 61 && capacidad <= 90) {
                        cantidadMenus = 3;
                        maximaPlatos = 45;
                    } else if (capacidad >= 91 && capacidad <= 120) {
                        cantidadMenus = 4;
                        maximaPlatos = 60;
                    } else if (capacidad >= 121 && capacidad <= 150) {
                        cantidadMenus = 5;
                        maximaPlatos = 75;
                    } else if (capacidad > 150) {
                        cantidadMenus = 6;
                        maximaPlatos = 90;
                    }

                    const idCliente = document.querySelector("#idCliente").parentElement;

                    for (let i = cantidadMenus; i >= 1; i--) {
                        const nuevoMenu = document.createElement("div");
                        nuevoMenu.className = "col-sm-6 menu-dinamico";
                        nuevoMenu.innerHTML = `
                            <label for="menu-${i}" class="form-label">Menu ${i}</label>
                            <select id="menu-${i}" class="form-select"></select>
                        `;

                        const primerMenuExistente = document.querySelector(".menu-dinamico");

                        if (primerMenuExistente) {
                            primerMenuExistente.parentElement.insertBefore(nuevoMenu, primerMenuExistente);
                        } else {
                            idCliente.insertAdjacentElement("afterend", nuevoMenu);
                        }

                        await cargarMenusDinamico(`menu-${i}`, maximaPlatos, cantidadMenus);
                    }

                    // si el cliente no tiene valor no se podra seleccionar un plato
                    const idClienteInput = document.getElementById("idCliente");
                    if (!idClienteInput.value) {
                        
                        document.querySelectorAll(".menu-dinamico select").forEach(sel => sel.disabled = true);

                        
                        document.querySelectorAll('[id*="contenedor-platos-menu-"]').forEach(div => {
                            div.style.pointerEvents = "none";
                            div.style.userSelect = "none";
                            div.style.opacity = "0.5";
                            div.style.filter = "greenscale(100%)";
                        });
                    }

                } catch (error) {
                    console.error("Error al obtener la cantidad y asignar más menús:", error);
                }
            }
        });
    });