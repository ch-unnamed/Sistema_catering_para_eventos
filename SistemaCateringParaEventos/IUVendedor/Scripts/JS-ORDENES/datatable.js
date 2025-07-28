
tabledata = $('#tabla').DataTable({
    responsive: true,
    ordening: false,
    "ajax": {
        "url": urlListarCotizacionMenu,
        "type": "GET",
        "datatype": "json",
        "complete": function () {
            $('#tabla').DataTable().columns.adjust().draw();
        }
    },
    "columns": [
        { "data": "Id" },
        {
            "data": "Cotizacion",
            "render": function (data, type, row) {
                return row.Cotizacion.IdCotizacion;
            }
        },
        {
            "data": "Menu",
            "render": function (data, type, row) {
                return row.Menu.Id;
            }
        },
        {
            "data": "Estado",
            "render": function (data, type, row) {
                if (data === 1) {
                    return '<span class="btn btn-warning btn-sm">En espera</span>';
                } else {
                    return '<span class="btn btn-success btn-sm">Concretado</span>';
                }
            }
        },
        {
            "data": null,
            "defaultContent": `<div class="btn-group gap-2">
                <button class="btn btn-primary btn-sm rounded-2 btn-editar"><i class="fas fa-pen"></i></button>
                <button class="btn btn-info btn-sm rounded-2 btn-ver-platosCotizacionMenu">Ver platos</button>
                <button class= "btn btn-secondary btn-sm rounded-2 btn-cargar-insumos d-none" >Cargar Insumos</button> 
                </div>`,
            "orderable": false,
            "searchable": false
        }

    ],
    language: {
        url: '//cdn.datatables.net/plug-ins/2.3.1/i18n/es-ES.json',
    }
});