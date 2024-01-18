$(document).ready(function () {
    renderPrestamos();
});

function renderPrestamos() {
    $("#table_container").empty();

    var settings = {
        type: 'GET',
        url: 'http://localhost:5056/api/Prestamo/getall',
        contentType: "application/json; charset=uft-8",
    };
    $.ajax(settings).done(function (result) {
        var theadTemplate =`
            <table class="table table-hover" id="tablePrestamos">
                <thead>
                    <tr>
                        <th>Editar</th>
                        <th>IdPrestamo</th>
                        <th>Usuario</th>
                        <th>Medio</th>
                        <th>Fecha Prestamo</th>
                        <th>Fecha Devolución</th>
                        <th>Estatus</th>
                        <th>Eliminar</th>
                    </tr>
                </thead>
                <tbody>               
                  `;

        $("#table_Container").append(theadTemplate);

        $.each(result.objects, function (i, prestamo) {


            //var Imagen = editorial.imagen != null ? `data:image/png;base64,${editorial.imagen}` : 'https://th.bing.com/th/id/OIP.dhBwcZT_mUoZpOBSNsjHzgAAAA?rs=1&pid=ImgDetMain';

            var trowTemplate =
                '<tr>'
                + '<td class="text-center"> <button class="btn btn-info" onclick="GetById(' + prestamo.idPrestamo + ')"><span class="bi bi-pencil-square"></span></button></td>'
                + "<td class='text-center'>" + prestamo.idPrestamo + "</td>"
                + "<td class='text-center'>" + prestamo.identityUsers.userName + "</td>"
                + "<td class='text-center'>" + prestamo.medio.titulo + "</td>"
                + "<td class='text-center'>" + prestamo.fechaPrestamo + "</td>"
                + "<td class='text-center'>" + prestamo.fechaDevolucion + "</td>"
                + "<td class='text-center'>" + prestamo.estatusPrestamo.descripcion + "</td>"
                + '<td class="text-center"><button class="btn btn-danger " onclick="Delete(' + prestamo.idPrestamo + ')" ><span class="bi bi-trash-fill"></span></button></td>'

                + "</tr>";
            //$("#table_Container").append(trowTemplate);
            $("#tablePrestamos tbody").append(trowTemplate);
        });

        var tBodyEndTemplate = `
                        </tbody>
                    </table>
                    `;
        $("#table_Container").append(tBodyEndTemplate);
        }).fail(function (xhr, status, error) {
        alert('Error en la actualizacion.' + error);

        });
    }
function GetById(id) {
    window.location.href = `/Prestamo/Form?IdPrestamo=${id}`;
}

function Delete(id) {

    
    if (confirm("¿Estas seguro de finalizar seleccionado?")) {

        var settings = {
            type: 'DELETE',
            url: 'http://localhost:5056/api/Prestamo/delete/' + id,
            contentType: "application/json; charset=uft-8",
        };
        $.ajax(settings).done(function (result) {
            alert('Se ha eliminado el autor');
        }).fail(function (xhr, status, error) {
            alert('Error al eliminar el prestamo.' + error);

        });
    };

};