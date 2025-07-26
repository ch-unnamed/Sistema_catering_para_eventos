$("#tabla tbody").on("click", '.btn-cargar-insumos', async function () {

    var data = tabledata.row(filaSeleccionada).data();

    // DATA DE LOS PLATOS

    var data = tabledata.row(filaSeleccionada).data();

    let resultado = await consultarPlatos(data.Id);

    const ids = resultado.data.map(item => item.Plato.Id);

    const response = await fetch('/Home/CargarCantidadInsumo', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(ids)
    });

    if (response.ok) {

        swal("¡Insumos Cargados Exitosamente!", "Presiona OK para continuar", "success");
        $(this).addClass('d-none');

        return true;
    }
    else {

        swal("¡Error al Cargar stock!", "Presiona OK para continuar", "error");

        return false;
    }

});