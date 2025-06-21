
    async function cargarEstados() {

        try {
            const response = await fetch('/Home/ListarEstados');
            const data = await response.json();

            const selectEstado = document.getElementById("estado");
            selectEstado.innerHTML = '';

            selectEstado.innerHTML += '<option value="" selected>Elija una opción</option>';

            data.data.forEach(item => {
                selectEstado.innerHTML += `<option value="${item.IdEstado}">${item.Nombre}</option>`;
            });

        } catch (error) {
            console.error("Error al obtener los estados:", error);
        }
    }

    async function cargarOpcionesMenu(menuId) {
        try {
            const response = await fetch('/Home/ListarMenus');
            const data = await response.json();

            const selectMenu = document.getElementById(menuId);
            selectMenu.innerHTML = '';

            const opcionDefault = document.createElement("option");
            opcionDefault.value = "";
            opcionDefault.selected = true;
            opcionDefault.textContent = "Elija una opción";
            selectMenu.appendChild(opcionDefault);

            data.data.forEach(item => {
                const option = document.createElement("option");
                option.value = item.Id;
                option.textContent = item.Nombre;
                selectMenu.appendChild(option);
            });

        } catch (error) {
            console.error(`Error al cargar opciones de menú en ${menuId}:`, error);
        }
    }

    async function cargarGanancia() {
        try {

            const response = await fetch('/Home/ObtenerPorcentajeGananciaGeneral');
            const data = await response.json();

            if (data.data) {
                const porcentaje = parseFloat(data.data.PorcentajeGanancia);
                console.log(`El porcentaje de ganancia es ${porcentaje}`);
                return porcentaje;
            } else {
                console.log("Algo falló");
                return 0;
            }

        } catch (error) {
            console.error(`Error al obtener el descuento`, error);
        }
    }

    async function descuentoPrimeraVez() {
        try {

            const response = await fetch('/Home/ObtenerPorcentajeDescuentoPrimeraVez');
            const data = await response.json();

            if (data.data) {
                const porcentaje = parseFloat(data.data.PorcentajeGanancia);
                console.log(`El porcentaje de descuento por primera vez es ${porcentaje}`);
                return porcentaje;
            } else {
                console.log("Algo falló");
                return 0;
            }

        } catch (error) {
            console.error(`Error al obtener el descuento`, error);
            return 0;
        }
    }

    async function descuentoMasDe3Menus() {
        try {

            const response = await fetch('/Home/ObtenerPorcentajeDescuentoMasDe3Menus');
            const data = await response.json();

            if (data.data) {
                const porcentaje = parseFloat(data.data.PorcentajeGanancia);
                console.log(`El porcentaje de descuento por selcionar mas de 3 menus es ${porcentaje}`);
                return porcentaje;
            } else {
                console.log("Algo falló");
                return 0;
            }

        } catch (error) {
            console.error(`Error al obtener el descuento`, error);
        }
    }

    async function consultarCantidadClientes(clienteId) {
        try {
            const response = await fetch(`/Home/consultarCliente?cliente_id=${clienteId}`);
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

    async function consultarNombreEventos(eventoId) {
        try {
            const response = await fetch(`/Home/nombreEvento?evento_id=${eventoId}`);
            const data = await response.json();

            let nombre = data.data;
            if (nombre) {
                return nombre;
            } else {
                return "";
            }

        } catch (error) {
            console.error(`Error al obtener el descuento`, error);
            return "";
        }
    }

    async function consultarDniCliente(clienteId) {
        try {
            const response = await fetch(`/Home/dniCliente?cliente_id=${clienteId}`);
            const data = await response.json();

            let dni = data.data.dni;
            if (dni) {
                return dni;
            } else {
                return "";
            }

        } catch (error) {
            console.error(`Error al obtener el descuento`, error);
            return "";
        }
    }

    async function consultarIdVendedor(rolId) {
        try {
            const response = await fetch(`/Home/idVendedor?rol_id=${rolId}`);
            const data = await response.json();
            let idVendedor = data.data.idUsuario;
            if (idVendedor) {
                return idVendedor;
            } else {
                return "";
            }
        } catch (error) {
            console.error(`Error al obtener el descuento`, error);
            return "";
        }
    }

async function consultarMailCliente(clienteID) {
    try {
        const response = await fetch(`/Home/ObtenerMailCliente?cliente_id=${clienteID}`);
        const data = await response.json();

        let email = data.data.email;

        if (email) {
            return email;
        } else {
            return "error";
        }

    } catch (error) {
        console.error(`Error al obtener el email`, error);
        return "";
    }
}

    let totalMenusRenderizados = 1;

    async function obtenerPlatos(menuId) {
        const response = await fetch(`/Home/ObtenerNombresPlatosPorMenu?menu_id=${menuId}`);
        const data = await response.json();

        return data.data.platos;
    }

function crearContenedorPlatos(menuId, selectMenu) {
    let contenedorPlatos = document.getElementById(`contenedor-platos-${menuId}`);

    if (!contenedorPlatos) {
        contenedorPlatos = document.createElement("div");
        contenedorPlatos.id = `contenedor-platos-${menuId}`;
        contenedorPlatos.className = "mt-3 d-none";
        contenedorPlatos.style.display = "block";
        contenedorPlatos.style.width = "100%";
        contenedorPlatos.style.clear = "both";

        selectMenu.parentElement.insertAdjacentElement("afterend", contenedorPlatos);
    }

    return contenedorPlatos;
}

function actualizarMenusDisponibles() {
    const todosLosSelects = document.querySelectorAll("select[id^='menu-']");
    const menusSeleccionados = Array.from(todosLosSelects)
        .map(s => s.value)
        .filter(v => v); // solo valores válidos

    todosLosSelects.forEach(select => {
        const opciones = select.querySelectorAll("option");

        opciones.forEach(opcion => {
            if (opcion.value === "" || opcion.value === select.value) {
                // Mantenemos la opción actual seleccionada
                opcion.disabled = false;
            } else if (menusSeleccionados.includes(opcion.value)) {
                opcion.disabled = true;
            } else {
                opcion.disabled = false;
            }
        });
    });
}
const opcionSi = document.getElementById("opcion-si");
const opcionNo = document.getElementById("opcion-no");

    async function cargarMenusDinamico(menuId, maximaPlatos, cantidadMenus) {

        try {

            await cargarOpcionesMenu(menuId);
            actualizarMenusDisponibles();
            const selectMenu = document.getElementById(menuId);
            const contenedorPlatos = crearContenedorPlatos(menuId, selectMenu);


            // EVENTOS AL SELECCIONAR UN MENU

            selectMenu.addEventListener("change", async function () {
                //actualizarMenusDisponibles();
                let menuIdSeleccionado = this.value;
                const idNumericoMenu = menuId.replace("menu-", "");

                contenedorPlatos.classList.remove('d-none');
                contenedorPlatos.innerHTML = "";

                if (menuIdSeleccionado) {
                    console.log(`el valor ahora PRIMERO del menu es:${menuIdSeleccionado}`);
                    // SI EXISTE EL MENU MUESTRO LOS PLATOS 

                    let platos = await obtenerPlatos(menuIdSeleccionado);

                        //FORMATO DEL EL TEXTO DEL PLATO DEL MENU
                        const label = document.createElement("label");
                        label.textContent = `Platos del Menú ${idNumericoMenu} seleccionado`;
                        label.className = "form-label";
                        contenedorPlatos.appendChild(label);

                        const lista = document.createElement("div"); // CONTENEDOR DE TODOS LOS PLATOS

                        // A CADA PLATO LE DOY UN FORMATO (CHECKBOX, NOMBRE, BTN-DETALLE)

                        platos.forEach(plato => {
                            const divPlato = document.createElement("div");
                            divPlato.className = "form-check d-flex justify-content-between align-items-start mb-2";

                            const izquierda = document.createElement("div");
                            izquierda.className = "d-flex flex-column";

                            const subFila = document.createElement("div");
                            subFila.className = "d-flex align-items-center gap-2";

                            const checkbox = document.createElement("input");
                            checkbox.type = "checkbox";
                            checkbox.className = "form-check-input";
                            checkbox.id = `plato_${plato.IdPlato}`;
                            checkbox.name = "platosSeleccionados";
                            checkbox.value = plato.IdPlato;

                            const labelCheckbox = document.createElement("label");
                            labelCheckbox.className = "form-check-label";
                            labelCheckbox.htmlFor = checkbox.id;
                            labelCheckbox.textContent = plato.Nombre;

                            const descripcionDiv = document.createElement("div");
                            descripcionDiv.className = "text-muted ms-4";
                            descripcionDiv.style.display = "none";
                            descripcionDiv.textContent = plato.Descripcion || "Sin descripción disponible.";

                            const botonIcono = document.createElement("button");
                            botonIcono.className = "btn btn-sm btn-outline-success";
                            botonIcono.innerHTML = `<i class="fas fa-plus"></i>`;
                            botonIcono.title = `Mostrar descripción del plato ID ${plato.IdPlato}`;

                            console.log("se creo el contenedor de platos");

                            let descripcionVisible = false;

                            console.log(`la cantidad de menus son:${menuIdSeleccionado}`);

                            // LOGICA DE POR CADA PLATO PODER AGREGAR MAS O REGINSTRIR 

                            checkbox.addEventListener("click", async () => {
                                console.log("asndkasnawdqf");
                                const clienteId = document.getElementById("idCliente").value;
                                let cantidad = await consultarCantidadClientes(clienteId);
                                let descuentoPrimeraVezFinal = 0;
                                let descuentoMasDe3MenusFinal = 0;

                                const precioPlato = parseFloat(plato.Precio);

                                console.log("---------------------------");

                                if (checkbox.checked) {
                                    totalPrecioBase += precioPlato;
                                } else {
                                    totalPrecioBase -= precioPlato;
                                }
                                console.log(`precio plato es ${precioPlato}`);
                                const porcentajeGanancia = await cargarGanancia();
                                const ganancia = totalPrecioBase * (porcentajeGanancia / 100);

                                if (cantidad === 0) {
                                    const descuentPrimeraVez = await descuentoPrimeraVez();
                                    descuentoPrimeraVezFinal = totalPrecioBase * (descuentPrimeraVez / 100);
                                    console.log(`El descuento por primera vez del plato es ${descuentoPrimeraVezFinal}`);
                                }

                                if (cantidadMenus > 3) {
                                    const descuentMasDe3Menus = await descuentoMasDe3Menus();
                                    descuentoMasDe3MenusFinal = totalPrecioBase * (descuentMasDe3Menus / 100);
                                    console.log(`El descuento por + de 3 menus del plato es ${descuentoMasDe3MenusFinal}`);
                                }

                                let totalFinal = totalPrecioBase + ganancia - descuentoPrimeraVezFinal - descuentoMasDe3MenusFinal;



                                const inputTotal = document.getElementById("total");
                                inputTotal.value = parseFloat(totalFinal).toLocaleString('es-AR', { style: 'currency', currency: 'ARS' });

                                console.log(`Total sin ganancia y descuentos: $${totalPrecioBase.toFixed(2)}`);
                                console.log(`Ganancia: ${porcentajeGanancia}%`);
                                console.log(`Total con ganancia + descuento primera vez + descuento + 3menus: $${totalFinal.toFixed(2)} del menu ${menuIdSeleccionado}`);

                                console.log(`se selecciono el plato con id ${plato.IdPlato} del menu ${menuIdSeleccionado}, su precio es ${plato.Precio}`);

                                const cantidadPlatos = contarPlatosSeleccionados(maximaPlatos);
                                console.log(`la cantidad de platos de ${menuIdSeleccionado} es ${cantidadPlatos}`);
                                const contenedorPregunta = document.getElementById("contenedor-pregunta");

                                console.log("---------------------------");

                                if (cantidadPlatos < maximaPlatos) {
                                    contenedorPregunta.classList.remove("d-none");

                                    opcionSi.addEventListener("change", () => {
                                        
                                        if (opcionSi.checked) {

                                            menuIdSeleccionado++;
                                            console.log(`el valor ahora del menu es:${menuIdSeleccionado}`);

                                            opcionNo.checked = false;
                                            let mensajeExtra = document.querySelector(".mensaje-menu-extra");
                                            let wrapperExtra = document.getElementById(`wrapper-menu-${menuIdSeleccionado}`);

                                            if (!mensajeExtra && !wrapperExtra) {
                                                // Crear el contenedor general
                                                const contenedorNuevo = document.createElement("div");
                                                contenedorNuevo.id = "menuNuevo";

                                                // Crear el mensaje
                                                mensajeExtra = document.createElement("p");
                                                mensajeExtra.textContent = "Agregue un menú más";
                                                mensajeExtra.className = "mt-3 fw-bold mensaje-menu-extra";

                                                // Crear el wrapper para el select
                                                wrapperExtra = document.createElement("div");
                                                wrapperExtra.className = "mt-1";
                                                wrapperExtra.id = `wrapper-menu-${menuIdSeleccionado}`;

                                                // Crear el select
                                                const nuevoSelect = document.createElement("select");
                                                nuevoSelect.id = `menu-${menuIdSeleccionado}`;
                                                nuevoSelect.className = "form-select";

                                                // Armar la estructura
                                                wrapperExtra.appendChild(nuevoSelect);
                                                contenedorNuevo.appendChild(mensajeExtra);
                                                contenedorNuevo.appendChild(wrapperExtra);

                                                // Insertar el contenedor completo
                                                contenedorPregunta.insertAdjacentElement("afterend", contenedorNuevo);

                                                // Lógica adicional
                                                cargarMenusDinamico(nuevoSelect.id, maximaPlatos);
                                             
                                            } else {
                                                mensajeExtra.style.display = "block";
                                                wrapperExtra.style.display = "block";
                                            }

                                        } else {
                                            menuIdSeleccionado--;
                                            console.log(`el valor ahora del menu es:${menuIdSeleccionado}`);

                                            document.querySelector(".mensaje-menu-extra")?.style && (
                                                document.querySelector(".mensaje-menu-extra").style.display = "none"
                                            );
                                            document.getElementById(`wrapper-menu-${menuIdSeleccionado}`)?.style && (
                                                document.getElementById(`wrapper-menu-${menuIdSeleccionado}`).style.display = "none"
                                            );
                                        }
                                    });

                                    opcionNo.addEventListener("change", () => {
                                        if (opcionNo.checked) {
                                            opcionSi.checked = false;

                                            const menuViejo = document.getElementById("menuNuevo");
                                            if (menuViejo) {
                                                menuViejo.remove();
                                                console.log("menuNuevo eliminado por cambio a NO");
                                            }

                                            menuIdSeleccionado = Math.max(0, menuIdSeleccionado - 1);
                                            console.log(`el valor ahora del menu es: ${menuIdSeleccionado}`);
                                        }
                                    });
                                } else if (cantidadPlatos > maximaPlatos) {
                                    const opcionSiExistente = document.getElementById("opcion-si");
                                    if (opcionSiExistente) {
                                        opcionSiExistente.disabled = true;
                                        opcionSiExistente.checked = false;
                                    }

                                    const opcionNoExistente = document.getElementById("opcion-no");
                                    if (opcionNoExistente) {
                                        opcionNoExistente.checked = true;
                                    }

                                    const mensajeLimite = document.createElement("p");
                                    mensajeLimite.textContent = "Se alcanzó el máximo de 15 platos. No se pueden agregar más.";
                                    mensajeLimite.className = "text-danger mt-2";

                                    const contenedorActual = document.getElementById(`contenedor-platos-${menuId}`);
                                    contenedorActual.insertAdjacentElement("afterend", mensajeLimite);
                                }
                            });

                            botonIcono.addEventListener("click", () => {
                                descripcionVisible = !descripcionVisible;

                                descripcionDiv.style.display = descripcionVisible ? "block" : "none";
                                botonIcono.classList.toggle("btn-outline-success", !descripcionVisible);
                                botonIcono.classList.toggle("btn-outline-danger", descripcionVisible);
                                botonIcono.innerHTML = `<i class="fas fa-${descripcionVisible ? "minus" : "plus"}"></i>`;
                            });

                            subFila.appendChild(checkbox);
                            subFila.appendChild(labelCheckbox);
                            izquierda.appendChild(subFila);
                            izquierda.appendChild(descripcionDiv);

                            divPlato.appendChild(izquierda);
                            divPlato.appendChild(botonIcono);
                            lista.appendChild(divPlato);
                        });

                    contenedorPlatos.appendChild(lista);

                    console.log(lista);
                } else {
                    contenedorPlatos.innerHTML = "<p>Seleccione un menú para ver los platos.</p>";
                    contenedorPlatos.classList.add('d-none');
                }


                btnGuardarSeleccion.replaceWith(btnGuardarSeleccion.cloneNode(true));
                const newBtnGuardarSeleccion = document.getElementById('btnGuardarSeleccion');

                newBtnGuardarSeleccion.addEventListener('click', async () => {
                    try {

                        const clienteId = document.getElementById("idCliente").value;
                        const eventoId = parseInt(document.getElementById("idEvento").value);
                        const fechaRealizacion = document.getElementById("fechaRealizacion").value;
                        const totalTexto = document.getElementById("total").value;
                        const total = parseFloat(
                            totalTexto.replace(/\$/g, "").replace(/\./g, "").replace(",", ".").trim()
                        );
                        const estado_id = parseInt(document.getElementById("estado").value);
                        const vendedor_id = await consultarIdVendedor(idRol);

                        const menusEnviados = [];

                        const selectsMenus = document.querySelectorAll("select[id^='menu-']");
                        selectsMenus.forEach(select => {
                            const menuIdSeleccionado = parseInt(select.value);
                            if (!menuIdSeleccionado) return; // ignorar si no se eligio menu

                            const contenedorId = `contenedor-platos-${select.id}`;
                            const checkboxes = document.querySelectorAll(`#${contenedorId} input[name="platosSeleccionados"]:checked`);
                            const platos = Array.from(checkboxes).map(cb => parseInt(cb.value));

                            if (platos.length > 0) {
                                menusEnviados.push({
                                    menuId: menuIdSeleccionado,
                                    platos: platos
                                });
                            }
                        });

                        if (menusEnviados.length === 0) {
                            swal("Error", "Hay campos incompletos.", "error");
                            return;
                        }

                        let mail = await consultarMailCliente(clienteId);
                        console.log(`EL MAIL DEL CLIENTE ES${mail}`);

                        const data = {
                            eventoId,
                            clienteId,
                            menus: menusEnviados,
                            fechaRealizacion,
                            total,
                            estado_id,
                            vendedor_id
                        };

                        console.log("Datos a enviar:", data);

                        const response = await fetch('/home/InsertarPlatosCotizacionPersonalizada', {
                            method: 'POST',
                            headers: {
                                'Content-Type': 'application/json'
                            },
                            body: JSON.stringify(data)
                        });

                        const result = await response.json();
                        const idCotizacion = result.idCotizacion;
                        console.log("ID Cotización generada:", idCotizacion);

                        let nombreEvento = await consultarNombreEventos(eventoId);
                        let DNI = await consultarDniCliente(clienteId);

                        // SE CREA UNA COTIZACION
                        if (result.success) {

                            const nuevaFila = {
                                CotizacionID: idCotizacion,
                                Evento: { Nombre: nombreEvento },
                                Cliente: { Dni: DNI },
                                Vendedor: { IdUsuario: data.vendedor_id },
                                FechaPedido: new Date().toISOString().split('T')[0],
                                FechaRealizacion: data.fechaRealizacion,
                                Total: data.total,
                                Estado: { Nombre: "Pendiente" },
                            };

                            $("#FormModal").modal("hide");

                            swal("¡Cotizacion Creada Exitosamente!", "Presiona OK para continuar", "success");
                            opcionSi.checked = false;
                            tabledata.row.add(nuevaFila).draw(false); // agrego la fila al datatable

                            document.querySelectorAll('.mt-3.fw-bold.mensaje-menu-extra').forEach(el => {
                                el.remove();
                            });
                            const wrappers = document.querySelectorAll('[id^="wrapper-menu-"]');

                            wrappers.forEach(wrapper => {
                                const contenedor = wrapper.closest('*'); // podés poner 'section', 'div', etc. para afinar
                                if (contenedor) {
                                    contenedor.remove();
                                }
                            });

                        } else {
                            console.log("Error con el sp de crear cotizacion");
                            console.log(result.message); // mostrará el mensaje personalizado
                            swal("Error", result.message || "Faltan completar campos", "error");
                        }
                    } catch (error) {
                        swal("Error", "Faltan completar campos", "error");
                    }
                });

            });

        } catch (error) {
            console.error(`Error al obtener los menús para ${menuId}:`, error);
        }
    }

    function contarPlatosSeleccionados(maximaPlatos) {
        const todosLosCheckbox = document.querySelectorAll('input[name="platosSeleccionados"]');
        const platosSeleccionados = document.querySelectorAll('input[name="platosSeleccionados"]:checked');

        if (platosSeleccionados.length < maximaPlatos) {

            todosLosCheckbox.forEach(cb => cb.disabled = false);
            return platosSeleccionados.length;
        } else {

            todosLosCheckbox.forEach(cb => {
                if (!cb.checked) cb.disabled = true;
            });
            return platosSeleccionados.length;
        }
}

function generarHtmlCotizacion(data) {
    let html = `<h2>Cotización Generada</h2>`;
    html += `<p>Evento ID: ${data.eventoId}</p>`;
    html += `<p>Fecha de realización: ${data.fechaRealizacion}</p>`;
    html += `<p>Total: $${data.total.toFixed(2)}</p>`;
    html += `<h3>Menús:</h3>`;

    data.menus.forEach((menu, index) => {
        html += `<p><strong>Menú ${index + 1} (ID: ${menu.menuId}):</strong> ${menu.platos.join(', ')}</p>`;
    });

    return html;
}
