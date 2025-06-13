

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
let totalMenusRenderizados = 1; // Siempre comienza en 1 por el menú inicial

async function cargarMenusDinamico(menuId, maximaPlatos) {

    try {
        await cargarOpcionesMenu(menuId);

        const selectMenu = document.getElementById(menuId);
        let contenedorPlatos = document.getElementById(`contenedor-platos-${menuId}`);

        // CREO LOS CONTENEDORES DE PLATOS

        if (!contenedorPlatos) {
            contenedorPlatos = document.createElement("div");
            contenedorPlatos.id = `contenedor-platos-${menuId}`;
            contenedorPlatos.className = "mt-3 d-none";
            contenedorPlatos.style.display = "block";
            contenedorPlatos.style.width = "100%";
            contenedorPlatos.style.clear = "both";

            selectMenu.parentElement.insertAdjacentElement("afterend", contenedorPlatos);
        }

        // EVENTOS AL SELECCIONAR UN MENU

        selectMenu.addEventListener("change", async function () {

            let menuIdSeleccionado = this.value;
            const idNumericoMenu = menuId.replace("menu-", "");

            contenedorPlatos.classList.remove('d-none');
            contenedorPlatos.innerHTML = "";

            if (menuIdSeleccionado) {

                // SI EXISTE EL MENU MUESTRO LOS PLATOS 

                try {
                    const responsePlatos = await fetch(`/Home/ObtenerNombresPlatosPorMenu?menu_id=${menuIdSeleccionado}`);
                    const data = await responsePlatos.json();
                    const platos = data.data.platos;

                    if (platos.length === 0) {
                        contenedorPlatos.innerHTML = "<p>No hay platos disponibles para este menú.</p>";
                        return;
                    }

                    const label = document.createElement("label");
                    label.textContent = `Platos del Menú ${idNumericoMenu} seleccionado`;
                    label.className = "form-label";
                    contenedorPlatos.appendChild(label);

                    const lista = document.createElement("div");

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

                        console.log(`la cantidad de menus son:${menuIdSeleccionado}`);

                        // LOGICA DE POR CADA PLATO PODER AGREGAR MAS O REGINSTRIR 

                        checkbox.addEventListener("click", () => {

                            console.log(`se selecciono el plato con id ${plato.IdPlato} del menu ${menuIdSeleccionado}`);

                            const cantidadPlatos = contarPlatosSeleccionados(maximaPlatos);
                            console.log(`la cantidad de platos de ${menuIdSeleccionado} es ${cantidadPlatos}`);
                            const contenedorPregunta = document.getElementById("contenedor-pregunta");

                            if (cantidadPlatos < maximaPlatos) {
                                contenedorPregunta.classList.remove("d-none");

                                const opcionSi = document.getElementById("opcion-si");
                                const opcionNo = document.getElementById("opcion-no");

                                opcionSi.replaceWith(opcionSi.cloneNode(true));
                                opcionNo.replaceWith(opcionNo.cloneNode(true));

                                const nuevaOpcionSi = document.getElementById("opcion-si");
                                const nuevaOpcionNo = document.getElementById("opcion-no");

                                nuevaOpcionSi.addEventListener("change", () => {
                                    if (nuevaOpcionSi.checked) {
                                        nuevaOpcionNo.checked = false;
                                        menuIdSeleccionado++;
                                        let mensajeExtra = document.querySelector(".mensaje-menu-extra");
                                        let wrapperExtra = document.getElementById(`wrapper-menu-${menuIdSeleccionado}`);

                                        if (!mensajeExtra && !wrapperExtra) {
                                            // Solo incrementamos si realmente vamos a renderizar un nuevo menú
                                            //totalMenusRenderizados++;

                                            mensajeExtra = document.createElement("p");
                                            mensajeExtra.textContent = "Agregue un menú más";
                                            mensajeExtra.className = "mt-3 fw-bold mensaje-menu-extra";

                                            wrapperExtra = document.createElement("div");
                                            wrapperExtra.className = "mt-1";
                                            wrapperExtra.id = `wrapper-menu-${menuIdSeleccionado}`;

                                            const nuevoSelect = document.createElement("select");
                                            nuevoSelect.id = `menu-${menuIdSeleccionado}`;
                                            nuevoSelect.className = "form-select";

                                            wrapperExtra.appendChild(nuevoSelect);
                                            contenedorPregunta.insertAdjacentElement("afterend", mensajeExtra);
                                            mensajeExtra.insertAdjacentElement("afterend", wrapperExtra);

                                            cargarMenusDinamico(nuevoSelect.id, maximaPlatos);
                                        } else {
                                            mensajeExtra.style.display = "block";
                                            wrapperExtra.style.display = "block";
                                        }
                                    } else {
                                        document.querySelector(".mensaje-menu-extra")?.style && (
                                            document.querySelector(".mensaje-menu-extra").style.display = "none"
                                        );
                                        document.getElementById(`wrapper-menu-${menuIdSeleccionado}`)?.style && (
                                            document.getElementById(`wrapper-menu-${menuIdSeleccionado}`).style.display = "none"
                                        );
                                    }
                                });

                                nuevaOpcionNo.addEventListener("change", () => {
                                    if (nuevaOpcionNo.checked) {
                                        nuevaOpcionSi.checked = false;
                                        document.querySelector(".mensaje-menu-extra")?.style && (
                                            document.querySelector(".mensaje-menu-extra").style.display = "none"
                                        );
                                        document.getElementById(`wrapper-menu-${menuIdSeleccionado}`)?.style && (
                                            document.getElementById(`wrapper-menu-${menuIdSeleccionado}`).style.display = "none"
                                        );
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
                } catch (error) {
                    console.error("Error al obtener los platos:", error);
                    contenedorPlatos.innerHTML = "<p class='text-danger'>Error al cargar los platos.</p>";
                }
            } else {
                contenedorPlatos.innerHTML = "<p>Seleccione un menú para ver los platos.</p>";
                contenedorPlatos.classList.add('d-none');
            }


            const btnGuardarSeleccion = document.getElementById('btnGuardarSeleccion');

            btnGuardarSeleccion.addEventListener('click', async () => {
                try {

                    const cotizacionId = 1;

                    const menuIdSeleccionado = selectMenu.value;

                    console.log(`al enviar el menu id es ${menuIdSeleccionado}`);

                    const checkboxes = document.querySelectorAll(`#contenedor-platos-${menuId} input[name="platosSeleccionados"]:checked`);


                    const platosSeleccionados = Array.from(checkboxes).map(cb => parseInt(cb.value));

                    console.log(`al evniar los platos son ${platosSeleccionados}`);
                    if (!menuIdSeleccionado) {
                        alert("Por favor, seleccione un menú antes de guardar.");
                        return;
                    }

                    if (platosSeleccionados.length === 0) {
                        alert("No hay platos seleccionados para guardar.");
                        return;
                    }

                  
                    const data = {
                        cotizacionId,
                        menuId: parseInt(menuIdSeleccionado),
                        platosSeleccionados
                    };

          
                    const response = await fetch('/home/InsertarPlatosCotizacionPersonalizada', {
                        method: 'POST',
                        headers: {
                            'Content-Type': 'application/json'
                        },
                        body: JSON.stringify(data)
                    });

                    const result = await response.json();

                    if (result.success) {
                        alert(result.message);
                    } else {
                        alert("Error: " + result.message);
                    }
                } catch (error) {
                    alert("Error al enviar la selección: " + error.message);
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