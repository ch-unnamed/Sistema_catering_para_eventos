
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
            { "data": "FechaPedido" },
            { "data": "FechaRealizacion" },
            {
                "data": "Total",
                render: function (data, type, row) {
                    if (type === 'display' || type === 'filter') {
                        // formato pesos argentinos
                        return data != null
                            ? data.toLocaleString('es-AR', { style: 'currency', currency: 'ARS' })
                            : "";
                    }
                    return data;
                }
            },
            {
                "data": "Estado",
                "render": function (data, type, row) {
                    const estado = row.Estado.Nombre;

                    let clase = '';
                    switch (estado) {
                        case "Completado":
                            clase = 'background-color: #28a745; color: white;'; // verde
                            break;
                        case "Confirmado":
                            clase = 'background-color: #0056b3; color: white;'; // azul oscuro
                            break;
                        case "Pendiente":
                            clase = 'background-color: #ffc107; color: white;'; // amarillo
                            break;
                        case "Rechazado":
                            clase = 'background-color: #dc3545; color: white;'; // rojo/naranja
                            break;
                        default:
                            clase = 'background-color: #6c757d; color: white;'; // gris por defecto
                    }

                    return `<span class="badge" style="padding: 5px 12px; font-size: 1rem; font-weight: 600; border-radius: 8px; ${clase}">${estado}</span>`;
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