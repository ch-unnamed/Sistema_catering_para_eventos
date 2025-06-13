
// en base a el cambio del valor del id del evento voy a mostrar los menus correspondinetes en base a el id y la capacidad

document.getElementById("idEvento").addEventListener("input", async function () {

    const eventoId = this.value;
    console.log(`Se seleccionó el id: ${eventoId}`);

    if (eventoId) {
        try {
            const response = await fetch(`/Home/ObtenerCapacidadPorIdEvento?evento_id=${eventoId}`);
            const data = await response.json();
            const capacidad = data.data.Capacidad;

            console.log(`La capacidad de este evento es ${capacidad}`);

            // limpiar si ya hay menus
            document.querySelectorAll(".menu-dinamico").forEach(menu => menu.remove());

            //debugger;

            let cantidadMenus = 0;
            let maximaPlatos = 0;

            if (capacidad >= 20 && capacidad <= 30) {
                cantidadMenus = 1;
                maximaPlatos = 15;
            }
            else if (capacidad >= 30 && capacidad <= 60) {
                cantidadMenus = 2;
                maximaPlatos = 30;
            }
            else if (capacidad >= 61 && capacidad <= 90) {
                cantidadMenus = 3;
                maximaPlatos = 45;
            }
            else if (capacidad >= 91 && capacidad <= 120) {
                cantidadMenus = 4;
                maximaPlatos = 60;
            }
            else if (capacidad >= 121 && capacidad <= 150) {
                cantidadMenus = 5;
                maximaPlatos = 75;
            }
            else if (capacidad > 150) {
                cantidadMenus = 6; 
                maximaPlatos = 90;
            }

            // uso el dni de referencia para mostrar los menus debajo de el
            const dniContainer = document.querySelector("#dni").parentElement;

            for (let i = cantidadMenus; i >= 1; i--) { // lo hago al reves para que se muestre en orden

                const nuevoMenu = document.createElement("div");
                nuevoMenu.className = "col-sm-6 menu-dinamico";
                nuevoMenu.innerHTML = `
                    <label for="menu-${i}" class="form-label">Menu ${i}</label>
                    <select id="menu-${i}" class="form-select"></select>
                `;

                // inserto antes del primer menu dinamico existente para mantener el orden correcto
                const primerMenuExistente = document.querySelector(".menu-dinamico");

                if (primerMenuExistente) {
                    // inserto antes el menu mas bajo, por ejemplo si esta 3 inserto 2
                    primerMenuExistente.parentElement.insertBefore(nuevoMenu, primerMenuExistente);
                } else {
                    // si no hay un primer menu agrego el mas grande
                    dniContainer.insertAdjacentElement("afterend", nuevoMenu);
                }

                await cargarMenusDinamico(`menu-${i}`, maximaPlatos); 
            }

        } catch (error) {
            console.error("Error al obtener la cantidad y asignar más menús:", error);
        }
    }
});
