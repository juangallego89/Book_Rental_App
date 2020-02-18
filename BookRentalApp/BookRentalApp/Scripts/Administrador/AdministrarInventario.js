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
        //"serverSide": true, // filtrado desde el servidor. Falta implementar en controlador.
        "filter": true, // habilita filtro de búsqueda. Lado del cliente
        "lengthMenu": [[3, 5, 10, 25, 50, -1], [3, 5, 10, 25, 50, "Todos"]],
        "pageLength": 10,

        ajax: {
            "url": "/Administrador/ObtenerLibros",
            "type": "GET",
            "dataType": "json"
        },

        "columns": [
            {
                "data": "Imagen", "title": "Portada",
                "render": function (Imagen) {
                    return '<img class="rounded img-thumbnail" style="width:50px; height:50px;" src="'+Imagen+'"/>';
                }
            },
            { "data": "Titulo", "title": "Titulo", "autowidth": true },
            { "data": "Autor", "title": "Autor", "autowidth": true },
            { "data": "Ejemplares", "title": "Disponible", "autowidth": true },
            { "data": "Ejemplares", "title": "Reservado", "autowidth": true },
            {
                'data': null,
                'render': function (data, type, row) {
                    return '<a href="/Administrador/EditarLibro?IdLibro=' + data.ID_Libro
                        + '" class="btn btn-primary" style="margin:10px">Editar</a><a href="/Administrador/EliminarLibro?IdLibro='
                        + data.ID_Libro + '" class="btn btn-danger" style="margin:10px">Eliminar</a>'
                }
            }
        ]
    });
});