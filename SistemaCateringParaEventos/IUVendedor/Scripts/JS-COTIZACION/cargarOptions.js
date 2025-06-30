
    async function cargarEstados(edita) {

        try {

            const response = await fetch('/Home/ListarEstados');
            const data = await response.json();

            const selectEstado = document.getElementById("estado");
            selectEstado.innerHTML = '';

            selectEstado.innerHTML += '<option value="" selected>Elija una opción</option>';

            if (edita === 1) {
                
                data.data.forEach(item => {
                    selectEstado.innerHTML += `<option value="${item.IdEstado}">${item.Nombre}</option>`;
                });
            } else {
                
                const pendiente = data.data.find(item => item.IdEstado === 3);
                if (pendiente) {
                    selectEstado.innerHTML += `<option value="${pendiente.IdEstado}">${pendiente.Nombre}</option>`;
                }
            }


        } catch (error) {
            swal("Error", error, "error");
        }
    }

    async function cargarOpcionesMenu(menuId) {

        try {

            const response = await fetch('/Home/ListarMenusVendedor');
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
            swal("Error", error, "error");
        }
    }

    async function cargarGanancia() {

        try {

            const response = await fetch('/Home/ObtenerPorcentajeGananciaGeneral');
            const data = await response.json();

            if (data.data) {
                const porcentaje = parseFloat(data.data.Porcentaje);
                
                return porcentaje;
            } else {
                return 0;
            }

        } catch (error) {
            swal("Error", error, "error");
        }
    }

    async function descuentoPrimeraVez() {

        try {

            const response = await fetch('/Home/ObtenerPorcentajeDescuentoPrimeraVez');
            const data = await response.json();

            if (data.data) {
                const porcentaje = parseFloat(data.data.Porcentaje);
                return porcentaje;
            } else {
                return 0;
            }

        } catch (error) {
            swal("Error", error, "error");
            return 0;
        }
    }

    async function descuentoMasDe3Menus() {

        try {

            const response = await fetch('/Home/ObtenerPorcentajeDescuentoMasDe3Menus');
            const data = await response.json();

            if (data.data) {
                const porcentaje = parseFloat(data.data.PorcentajeGanancia);
                
                return porcentaje;
            } else {
                
                return 0;
            }

        } catch (error) {
            swal("Error", error, "error");
        }
    }

    async function consultarClientes(clienteId) {

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
            swal("Error", error, "error");
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
            swal("Error", error, "error");
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
            swal("Error", error, "error");
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
            swal("Error", error, "error");
            return "";
        }
    }

    async function consultarMailCliente(clienteId) {

        try {
            const response = await fetch(`/Home/ObtenerMailCliente?cliente_id=${clienteId}`);
            const data = await response.json();

            let email = data.data.email;

            if (email) {
                return email.trim();
            } else {
                return "error";
            }

        } catch (error) {
            swal("Error", error, "error");
        }
    }

    let totalMenusRenderizados = 1;

    async function obtenerPlatos(menuId) {
        const response = await fetch(`/Home/ObtenerNombresPlatosPorMenu?menu_id=${menuId}`);
        const data = await response.json();

        return data.data.platos;
    }

    async function consultarPlato(idPlato) {
        const response = await fetch(`/Home/ConsultarPlato?idPlato=${idPlato}`);
        const data = await response.json();

        return data.data;
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
            .filter(v => v); 

        todosLosSelects.forEach(select => {
            const opciones = select.querySelectorAll("option");

            opciones.forEach(opcion => {
                if (opcion.value === "" || opcion.value === select.value) {
                    
                    opcion.disabled = false;
                } else if (menusSeleccionados.includes(opcion.value)) {
                    opcion.disabled = true;
                } else {
                    opcion.disabled = false;
                }
            });
        });
    }

    let descuentoPrimeraVezFinal = 0;
    let descuentoMasDe3MenusFinal = 0;
    let nombreMenuElecto = "";

    async function cargarMenusDinamico(menuId, maximaPlatos, cantidadMenus) {

        try {

            await cargarOpcionesMenu(menuId);
            actualizarMenusDisponibles();
            const selectMenu = document.getElementById(menuId);
            const contenedorPlatos = crearContenedorPlatos(menuId, selectMenu);


            // EVENTOS AL SELECCIONAR UN MENU

            selectMenu.addEventListener("change", async function () {
                actualizarMenusDisponibles();
                let menuIdSeleccionado = this.value;
                const idNumericoMenu = menuId.replace("menu-", "");

                contenedorPlatos.classList.remove('d-none');
                contenedorPlatos.innerHTML = "";

                if (menuIdSeleccionado) {
                    
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


                        let descripcionVisible = false;

                        // LOGICA DE POR CADA PLATO PODER AGREGAR MAS O REGINSTRIR 

                        checkbox.addEventListener("click", async () => {

                            let clienteId = document.getElementById("idCliente").value;
                            let cantidad = await consultarClientes(clienteId);

                            const precioPlato = parseFloat(plato.Precio);

                            if (checkbox.checked) {
                                totalPrecioBase += precioPlato;
                            } else {
                                totalPrecioBase -= precioPlato;
                            }

                            // Por cada plato se le asigna el % de ganancia y descuento

                            const porcentajeGanancia = await cargarGanancia();
                            const ganancia = totalPrecioBase * (porcentajeGanancia / 100);

                            if (cantidad === 0) {
                                const descuentPrimeraVez = await descuentoPrimeraVez();
                                descuentoPrimeraVezFinal = totalPrecioBase * (descuentPrimeraVez / 100);

                                dtoPrimeraVez = 1;
                            }

                            if (cantidadMenus > 3) {
                                const descuentMasDe3Menus = await descuentoMasDe3Menus();
                                descuentoMasDe3MenusFinal = totalPrecioBase * (descuentMasDe3Menus / 100);

                                dtoMas3Menus = 1;
                            }

                            let totalFinal = totalPrecioBase + ganancia - descuentoPrimeraVezFinal - descuentoMasDe3MenusFinal;

                            const inputTotal = document.getElementById("total");
                            inputTotal.value = parseFloat(totalFinal).toLocaleString('es-AR', { style: 'currency', currency: 'ARS' });

                            const cantidadPlatos = contarPlatosSeleccionados(maximaPlatos);

                            const contenedorPregunta = document.getElementById("contenedor-pregunta");

                            //MOSTRAR OPCION DE AGREGAR MENU SI HAY MENOS PLATOS DEL MAXIMO PERMITIDO

                            if (cantidadPlatos < maximaPlatos) {
                                contenedorPregunta.classList.remove("d-none");

                                // Si elije que si quiere agregar un menu se cargan menus + platos
                                opcionSi.addEventListener("change", () => {

                                    if (opcionSi.checked) {

                                        let nuevoValorMenu = cantidadMenus + 1;
                                        menuIdSeleccionado++;

                                        opcionNo.checked = false;
                                        let mensajeExtra = document.querySelector(".mensaje-menu-extra");
                                        let wrapperExtra = document.getElementById(`wrapper-menu-${menuIdSeleccionado}`);

                                        if (!mensajeExtra && !wrapperExtra) {
                                            
                                            const contenedorNuevo = document.createElement("div");
                                            contenedorNuevo.id = "menuNuevo";

                                            
                                            mensajeExtra = document.createElement("p");
                                            mensajeExtra.textContent = "Agregue un menú más";
                                            mensajeExtra.className = "mt-3 fw-bold mensaje-menu-extra";

                                            wrapperExtra = document.createElement("div");
                                            wrapperExtra.className = "mt-1";
                                            wrapperExtra.id = `wrapper-menu-${nuevoValorMenu}`;

                                            const nuevoSelect = document.createElement("select");
                                            nuevoSelect.id = `menu-${nuevoValorMenu}`;
                                            nuevoSelect.className = "form-select";

                                            // armo la estructura completa
                                            wrapperExtra.appendChild(nuevoSelect);
                                            contenedorNuevo.appendChild(mensajeExtra);
                                            contenedorNuevo.appendChild(wrapperExtra);
                                            contenedorPregunta.insertAdjacentElement("afterend", contenedorNuevo);

                                            // en base lo nuevo cargar el menu electo
                                            cargarMenusDinamico(nuevoSelect.id, maximaPlatos);

                                        } else {
                                            mensajeExtra.style.display = "block";
                                            wrapperExtra.style.display = "block";
                                        }

                                    } else {
                                        menuIdSeleccionado--;

                                        document.querySelector(".mensaje-menu-extra")?.style && (
                                            document.querySelector(".mensaje-menu-extra").style.display = "none"
                                        );
                                        document.getElementById(`wrapper-menu-${menuIdSeleccionado}`)?.style && (
                                            document.getElementById(`wrapper-menu-${menuIdSeleccionado}`).style.display = "none"
                                        );
                                    }
                                });

                                // Si no quiere agregar nada
                                opcionNo.addEventListener("change", () => {
                                    if (opcionNo.checked) {
                                        opcionSi.checked = false;

                                        const menuViejo = document.getElementById("menuNuevo");
                                        if (menuViejo) {
                                            menuViejo.remove();
                                        }

                                        menuIdSeleccionado = Math.max(0, menuIdSeleccionado - 1);
                                    }
                                });
                            } else if (cantidadPlatos > maximaPlatos) {

                                // Si hay mas cantidad de platos que la maxima se deshabilitan ls paltos
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

                        // DESCRIPCION DEL PLATO
                        botonIcono.addEventListener("click", () => {
                            descripcionVisible = !descripcionVisible;

                            descripcionDiv.style.display = descripcionVisible ? "block" : "none";
                            botonIcono.classList.toggle("btn-outline-success", !descripcionVisible);
                            botonIcono.classList.toggle("btn-outline-danger", descripcionVisible);
                            botonIcono.innerHTML = `<i class="fas fa-${descripcionVisible ? "minus" : "plus"}"></i>`;
                        });

                        // ARMO LA ESTRUCTURA COMPLETA
                        subFila.appendChild(checkbox);
                        subFila.appendChild(labelCheckbox);
                        izquierda.appendChild(subFila);
                        izquierda.appendChild(descripcionDiv);

                        divPlato.appendChild(izquierda);
                        divPlato.appendChild(botonIcono);
                        lista.appendChild(divPlato);
                    });

                    contenedorPlatos.appendChild(lista);

                } else {
                    contenedorPlatos.innerHTML = "<p>Seleccione un menú para ver los platos.</p>";
                    contenedorPlatos.classList.add('d-none');
                }

                // MOMENTO EN EL QUE SE GUARDA LA COTIZACION Y SE DEBEN ENVIAR LOS DATOS RECIENTEMENTE GENERADOS

                btnGuardarSeleccion.replaceWith(btnGuardarSeleccion.cloneNode(true));
                const newBtnGuardarSeleccion = document.getElementById('btnGuardarSeleccion');

                newBtnGuardarSeleccion.addEventListener('click', async () => {

                    try {

                        // OBTENER LOS VALORES GENERADOS Y MAS DATOS PARA GENERAR LA COTIZACION Y LUEGO EL PDF

                        let clienteId = document.getElementById("idCliente").value;
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
                            if (!menuIdSeleccionado) return; 

                            const menuNombre = select.options[select.selectedIndex].text;

                            const contenedorId = `contenedor-platos-${select.id}`;
                            const checkboxes = document.querySelectorAll(`#${contenedorId} input[name="platosSeleccionados"]:checked`);
                            const platos = Array.from(checkboxes).map(cb => parseInt(cb.value));

                            if (platos.length > 0) {
                                menusEnviados.push({
                                    menuId: menuIdSeleccionado,
                                    nombre: menuNombre, 
                                    platos: platos
                                });
                            }
                        });



                        if (menusEnviados.length === 0) {
                            swal("Error", "Hay campos incompletos.", "error");
                            return;
                        }

                        // formato que se va a enviar
                        const data = {
                            eventoId,
                            clienteId,
                            menus: menusEnviados,
                            fechaRealizacion,
                            total,
                            estado_id,
                            vendedor_id
                        };

                        const response = await fetch('/home/InsertarPlatosCotizacionPersonalizada', {
                            method: 'POST',
                            headers: {
                                'Content-Type': 'application/json'
                            },
                            body: JSON.stringify(data)
                        });

                        const result = await response.json();
                        const idCotizacion = result.idCotizacion;

                        let nombreEvento = await consultarNombreEventos(eventoId);
                        let DNI = await consultarDniCliente(clienteId);

                        // SE CREA UNA COTIZACION
                        if (result.success) {

                            let mail = await consultarMailCliente(clienteId);
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
                            const totalDescuento = descuentoPrimeraVezFinal + descuentoMasDe3MenusFinal;
                            const formateadoARS = new Intl.NumberFormat('es-AR', {
                                style: 'currency',
                                currency: 'ARS'
                            }).format(totalDescuento);

                            // PDF
                            const platosElegidos = [];

                            // Obtener los platos electos
                            for (const menu of menusEnviados) {

                                for (const idPlato of menu.platos) {

                                    try {

                                        const plato = await consultarPlato(idPlato);
                                        platosElegidos.push({
                                            nombre: plato.Nombre,
                                            precio: parseFloat(plato.Precio)
                                        });

                                    } catch (error) {
                                        swal("Error", error, "error");
                                    }
                                }
                            }

                            // logica pdf
                            const { jsPDF } = window.jspdf;
                            const doc = new jsPDF();

                            // fuente general 
                            doc.setFont("courier");

                            // ===== ENCABEZADO =====
                            doc.setFontSize(18);
                            doc.setFont("courier", "bold");
                            doc.text("COTIZACIÓN", 105, 20, null, null, "center");
                            doc.setLineWidth(0.5);
                            doc.line(20, 25, 190, 25);

                            // ===== CABECERA: COTIZACION Y CLIENTE =====
                            doc.setFontSize(11);
                            doc.setFont("courier", "bold");
                            doc.text("Cotización", 20, 30);

                            doc.setFont("courier", "normal");
                            doc.text(`N°: ${nuevaFila.CotizacionID}`, 20, 36);
                            doc.text(`Estado: ${nuevaFila.Estado.Nombre}`, 20, 42);

                            doc.setFont("courier", "bold");
                            doc.text("Vendedor ID:", 20, 48);
                            doc.setFont("courier", "normal");
                            doc.text(`${nuevaFila.Vendedor.IdUsuario}`, 50, 48);
                            doc.setFont("courier", "bold");
                            doc.text("Cliente", 120, 30);
                            doc.setFont("courier", "normal");
                            doc.text(`DNI: ${nuevaFila.Cliente.Dni}`, 120, 36);
                            doc.text(`Mail: ${mail}`, 120, 42);

                            // ===== EVENTO Y FECHAS =====
                            doc.setFont("courier", "bold");
                            doc.text("Evento", 20, 52);
                            doc.setFont("courier", "normal");
                            doc.text(`${nuevaFila.Evento.Nombre}`, 20, 58);
                            doc.text(`Fecha Pedido: ${nuevaFila.FechaPedido}`, 20, 64);
                            doc.text(`Fecha Realización: ${nuevaFila.FechaRealizacion}`, 20, 70);

                            // ===== MENUS ELEGIDOS =====
                            const nombresMenus = data.menus.map(menu => menu.nombre);
                            doc.setFont("courier", "bold");
                            doc.text("Menús Elegidos:", 120, 52);
                            doc.setFont("courier", "normal");

                            let yMenu = 58;
                            nombresMenus.forEach(nombre => {
                                doc.text(`- ${nombre}`, 120, yMenu);
                                yMenu += 6;
                            });

                            // ===== TABLA DE PLATOS =====
                            let y = Math.max(80, yMenu + 10);

                            doc.setFont("courier", "bold");
                            doc.setFontSize(12);
                            doc.text("Detalle de Platos Seleccionados", 105, y, null, null, "center");
                            y += 6;

                            doc.setLineWidth(0.2);
                            doc.rect(20, y, 170, 8);
                            doc.setFont("courier", "bold");
                            doc.setFontSize(10);
                            doc.text("Nombre del Plato", 25, y + 6);
                            doc.text("Precio", 170, y + 6, null, null, "right");
                            y += 8;

                            doc.setFont("courier", "normal");

                            platosElegidos.forEach(plato => {
                                doc.rect(20, y, 170, 8);
                                doc.text(plato.nombre, 25, y + 6);
                                doc.text(`$${plato.precio.toFixed(2)}`, 170, y + 6, null, null, "right");
                                y += 8;

                                if (y > 240) {
                                    doc.addPage();
                                    doc.setFont("courier");
                                    y = 20;
                                }
                            });

                            // ===== RESUMEN =====
                            y += 10;
                            doc.setFont("courier", "bold");
                            doc.text("Resumen", 20, y);
                            y += 6;
                            doc.setFont("courier", "normal");
                            // arreglos para mostrar los datos de forma prolija
                            const columnaIzqX = 20;
                            const columnaDerY1 = y;         
                            const columnaDerY2 = y + 8;     

                            doc.text(`Descuento Primera Vez: ${dtoPrimeraVez === 1 ? "APLICA" : "NO APLICA"}`, columnaIzqX, columnaDerY1);
                            doc.text(`Descuento por Cantidad: ${dtoMas3Menus === 1 ? "APLICA" : "NO APLICA"}`, columnaIzqX, columnaDerY2);

                            // ==== COLUMNA DERECHA--DESCUENTO Y TOTAL ====
                            const labelX = 120;
                            const montoX = 190;

                            doc.setFont("courier", "bold");
                            doc.text("Descuento Total:", labelX, columnaDerY1);
                            doc.setFont("courier", "normal");
                            doc.text(`${formateadoARS}`, montoX, columnaDerY1, null, null, "right");

                            doc.setFont("courier", "bold");
                            doc.text("TOTAL:", labelX, columnaDerY2);
                            doc.setFont("courier", "normal");
                            doc.text(`$${nuevaFila.Total.toFixed(2)}`, montoX, columnaDerY2, null, null, "right");

                            // ==== PIE DE PAGINA ====
                            doc.setFontSize(10);
                            doc.setFont("courier", "italic");
                            doc.text("Gracias por confiar en nosotros 'Alta Mesa' Presente.", 105, 285, null, null, "center");


                            // ===== GUARDAR PDF =====
                            doc.save(`cotizacion_${nuevaFila.CotizacionID}.pdf`);

                            tabledata.row.add(nuevaFila).draw(false); // agrego la fila al datatable de cotizacion

                            document.querySelectorAll('.mt-3.fw-bold.mensaje-menu-extra').forEach(el => {
                                el.remove();
                            });
                            const wrappers = document.querySelectorAll('[id^="wrapper-menu-"]');

                            // remover errores
                            wrappers.forEach(wrapper => {
                                const contenedor = wrapper.closest('*');
                                if (contenedor) {
                                    contenedor.remove();
                                }
                            });

                        } else {
                            swal("Error", result.message || "Faltan completar campos", "error");
                        }
                    } catch (error) {
                        swal("Error", "Faltan completar campos", "error");
                    }
                });

            });

        } catch (error) {
            swal("Error", error, "error");
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