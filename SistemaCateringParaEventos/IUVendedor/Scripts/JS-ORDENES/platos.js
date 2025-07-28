
async function consultarPlatos(IdCotizacionMenu){

    try {

        const response = await fetch(`/Home/ListarCotizacionMenuPlato?cotizacionMenuPlatoId=${IdCotizacionMenu}`);
        const data = await response.json();

        return data;

    } catch (error) {
        console.log(error);
    }
}

async function verPlatos(idCotizacionMenu) {

    // mostrar modal de platos
    $('#modalVerPlatosCotizacion').modal('show');
    $('#tituloModalPlatosCotizacion').text(`Platos de la Cotización-Menú #${idCotizacionMenu}`);

    // consultar platos y asingar valores

    const resultado = await consultarPlatos(idCotizacionMenu);

    console.log(resultado);

    if ($.fn.DataTable.isDataTable('#tablaPlatosCotizacion')) {
        $('#tablaPlatosCotizacion').DataTable().clear().destroy();
    }

    $('#tablaPlatosCotizacion').DataTable({
        data: resultado.data.map(item => item.Plato),
        columns: [
            { data: 'Id', title: 'ID' },
            { data: 'Nombre', title: 'Nombre del Plato' }
        ],
        language: {
            url: '//cdn.datatables.net/plug-ins/1.13.6/i18n/es-ES.json'
        }
    });

}

$("#tabla tbody").on("click", '.btn-ver-platosCotizacionMenu', async function () {

    const filaSeleccionada = $(this).closest("tr");
    const data = tabledata.row(filaSeleccionada).data();

    console.log(data);

    await verPlatos(data.Id);
})