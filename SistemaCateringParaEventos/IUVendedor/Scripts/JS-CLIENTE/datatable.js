
    tabledata = $('#tabla').DataTable({ 
        responsive: true,
        ordening: false, 
        "ajax": {
            "url": urlListarClientes,
            "type": "GET",
            "datatype": "json",
            "complete": function () {
                $('#tabla').DataTable().columns.adjust().draw(); 
            }
        },
        "columns": [
            { "data": "IdCliente" },
            {
                "data": null,
                "render": function (data, type, row) {
                    return row.Nombre + " " + row.Apellido;
                }
            },
            { "data": "Email" },
            {
                "data": "Localidad",
                "render": function (data, type, row) {
                    return row.Localidad.Nombre;
                }
            },
            {
                "data": "Provincia",
                "render": function (data, type, row) {
                    return row.Localidad.Provincia.Nombre;
                }
            },
            { "data": "Telefono" },
            {   
                "data": "Tipo_Cliente",
                "title": "Tipo",
                "render": function (data, type, row) {
                    return row.Tipo_Cliente.Nombre;
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