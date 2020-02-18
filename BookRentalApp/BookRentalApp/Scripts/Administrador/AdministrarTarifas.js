$(document).ready(function () {
    var Tabla = $("#myDataTable").DataTable({
        "language": {
            "lengthMenu": "Registros por página _MENU_",
            "search": "Buscar",
            "paginate": { "previous": "Anterior", "next": "Siguiente" },
            "zeroRecords": "No se encontraron resultados",
            "info": "Mostrando página _PAGE_ de _PAGES_",
            "infoEmpty": "Sin registros",
            "infoFiltered": "(filtrado de _MAX_ regitros totales)"
        },
        "processing": true,
        "lengthMenu": [[3, 5, 10, 25, 50, -1], [3, 5, 10, 25, 50, "Todos"]],
        "pageLength": 10,

        ajax: {
            "url": "/Administrador/ObtenerTarifas",
            "type": "GET",
            "dataType": "json"
        },

        "columns": [
            { "data": "Nombre", "title": "Nombre", "autowidth": true },
            { "data": "Tarifa", "title": "Tarifa", "autowidth": true },
            {
                'data': null,
                'render': function (data) {
                    return '<a href="/Administrador/EditarTarifa?IdTarifa=' + data.ID_Tarifa
                        + '" class="btn btn-primary" style="margin:10px">Editar'
                }
            }
        ]
    });
});