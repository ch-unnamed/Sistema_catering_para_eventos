
    async function nombreCliente(nombreCliente) {
        try {
            const response = await fetch(`/Home/NombresClientes?nombre=${encodeURIComponent(nombreCliente)}`);
            const result = await response.json();
            return result.data;
        } catch (error) {
            swal("Error en fetch nombre de los clientes", error.message, "error");
            return [];
        }
    }

    async function nombreClientePorId(idCliente) {
        try {
            const response = await fetch(`/Home/NombreClientePorId?idCliente=${idCliente}`);
            const result = await response.json();
            return result.data;
        } catch (error) {
            swal("Error en fetch nombre de los clientes", error.message, "error");
            return [];
        }
    }

    async function buscarPorNombre() {
        const inputCliente = document.getElementById("cliente").value.trim();
        const dropdown = document.getElementById("listaClientes");
        dropdown.innerHTML = "";

        if (inputCliente.length < 2) {
            dropdown.classList.remove("show");
            return;
        }

        const clientes = await nombreCliente(inputCliente);

        if (clientes.length === 0) {
            dropdown.classList.remove("show");
            return;
        }

        clientes.forEach((cliente, index) => {
            const item = document.createElement("li");
            item.classList.add("dropdown-item");
            item.textContent = `${cliente.Nombre} ${cliente.Apellido}`;

            item.onclick = () => {
                document.getElementById("idCliente").value = cliente.IdCliente;
                document.getElementById("cliente").value = `${cliente.Nombre} ${cliente.Apellido}`;

                const dropdown = document.getElementById("listaClientes");
                dropdown.innerHTML = "";
                dropdown.classList.remove("show");
            };


            dropdown.appendChild(item);

            // separo los datos
            if (index < clientes.length - 1) {
                const divider = document.createElement("li");
                divider.classList.add("dropdown-divider");
                dropdown.appendChild(divider);
            }
        });

        dropdown.classList.add("show");
    }

    async function abrirModal(json) {

        let delayTimer;

        document.getElementById("cliente").onkeyup = function () {
            clearTimeout(delayTimer);
            delayTimer = setTimeout(buscarPorNombre, 300); // evito errores en el fetch
        }

        $("#id").val(0);
        $("#idCliente").val("");
        $("#nombre").val("");
        $("#cliente").val("");
        $("#capacidad").val("");
        $("#tipo").val("");
        $("#calle").val("");
        $("#altura").val("");
        $("#ciudad").val("");
        $("#provincia").val("");
        $("#fecha").val("");

        // por primera vez no mostrar error, ya que no estan los campos pero se van a completar
        $("#msjError").hide();

        // si el json contiene valores presiono editar
        if (json != null) {
            $("#id").val(json.IdEvento);
            $("#nombre").val(json.Nombre);
            $("#idCliente").val(json.Cliente.IdCliente);
            $("#cliente").val(json.Cliente.IdCliente);
            $("#capacidad").val(json.Capacidad);
            $("#tipo").val(json.Tipo_Evento.Nombre);

            if (json.Ubicacion) {
                $("#calle").val(json.Ubicacion.Calle);
                $("#altura").val(json.Ubicacion.Altura);
                $("#ciudad").val(json.Ubicacion.Ciudad);
                $("#provincia").val(json.Ubicacion.Provincia);
            }
            $("#fecha").val(json.Fecha);

            let nombre = await nombreClientePorId(json.Cliente.IdCliente);
            let cliente = nombre[0];
            $("#cliente").val(cliente.Nombre + " " + cliente.Apellido); 

            // cliente ineditable
            $("#cliente").prop("readonly", true).prop("disabled", true).addClass("bg-light");
        }

        $("#FormModal").modal("show");
    }