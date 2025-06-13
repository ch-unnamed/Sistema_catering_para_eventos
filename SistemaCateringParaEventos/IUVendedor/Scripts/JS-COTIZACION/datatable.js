    var tabledata;

    tabledata = $('#tablaCotizacion').DataTable({
        responsive: true,
        orening: false,
        "ajax": {
            "url": urlListarCotizaciones,
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            {
                "data": "Evento",
                "render": function (data, type, row) {
                    return row.Evento.Nombre;
                }
            },
            {
                "data": "Cliente",
                "render": function (data, type, row) {
                    return row.Cliente.Dni;
                }
            },
            {
                "data": "Vendedor",
                "render": function (data, type, row) {
                    return row.Vendedor.IdUsuario;
                }
            },
            {
                "data": "Menu",
                "render": function (data, type, row) {
                    return row.Menu.Nombre;
                }
            },
            { "data": "FechaPedido" },
            { "data": "FechaRealizacion" },
            { "data": "Total" },
            {
                "data": "Estado",
                "render": function (data, type, row) {
                    return row.Estado.Nombre;
                }
            },
            {
                "data": null,
                "defaultContent": '<div class="btn-group">' +
                    '<button class="btn btn-primary btn-sm rounded-2 btn-editar"><i class="fas fa-pen"></i></button>' +
                    '<button class="btn btn-danger btn-sm rounded-2 ms-2 btn-eliminar"><i class="fas fa-trash"></i></button>' +
                    '</div>',
                "orderable": false,
                "searchable": false
            }
        ],
        language: {
            url: '//cdn.datatables.net/plug-ins/2.3.1/i18n/es-ES.json',
        }
    });