
    tabledata = $('#tabla').DataTable({ 

        responsive: true,
        ordening: false,

        "ajax": {
            "url": urlListarEventos,
            "type": "GET",
            "datatype": "json"
        },

        "columns": [

            { "data": "IdEvento" },
            { "data": "Nombre" },
            { "data": "Capacidad" },
            {
                "data": "Tipo_Evento",
                "render": function (data, type, row) {
                    return row.Tipo_Evento.Nombre;
                }
            },
            {
                "data": "Calle",
                "render": function (data, type, row) {
                    return row.Ubicacion.Calle;
                }
            },
            {
                "data": "Altura",
                "render": function (data, type, row) {
                    return row.Ubicacion.Altura;
                }
            },
            {
                "data": "Ciudad",
                "render": function (data, type, row) {
                    return row.Ubicacion.Ciudad;
                }
            },
            {
                "data": "Provincia",
                "render": function (data, type, row) {
                    return row.Ubicacion.Provincia;
                }
            },
            { "data": "Fecha" },
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
