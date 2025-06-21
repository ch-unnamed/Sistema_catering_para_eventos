// Funcion para abrir el modal
        function abrirModal(json) {

            // Limpiar campos para que no queden datos de otras filas
            $("#id").val(0);
            $("#nombre").val("");
            $("#apellido").val("");
            $("#dni").val("");
            $("#email").val("");
            $("#region").val("");
            $("#telefono").val("");
            $("#tipo_cliente").val(""); // asignar segun el value


            $("#msjError").hide();


            if (json != null) { // entra aca si le da a editar, ya que habrian datos en la fila

                // le damos el valor al campo especifico del json q obtuvimos con ajax anteriormente

                $("#id").val(json.IdCliente);
                $("#nombre").val(json.Nombre);
                $("#apellido").val(json.Apellido);
                $("#dni").val(json.Dni);
                $("#email").val(json.Email);
                $("#region").val(json.Region);
                $("#telefono").val(json.Telefono);
                $("#tipo_cliente").val(json.Tipo_Cliente.Nombre == 'Personal' ? 1 : 2); // asignar segun el value*/
                
            }

            $("#FormModal").modal("show");
        }