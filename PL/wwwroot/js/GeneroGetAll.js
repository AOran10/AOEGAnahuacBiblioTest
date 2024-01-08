$(document).ready(function () {
    renderGeneros();
});

function renderGeneros() {
    $("#table_Container").empty();

    var settings = {
        type: 'GET',
        url: 'http://localhost:5083/Genero/GetAllGenero',
        contentType: "application/json; charset=uft-8",
    };
    $.ajax(settings).done(function (result) {
        var theadTemplate = `
                        <table class="table table-hover" id="tableGeneros">
                        <thead>
                            <tr>
                                <th>Editar</th>
                                <th>Nombre</th>
                                <th>Descripcion</th>
                                <th>Eliminar</th>
                            </tr>
                        </thead>
                        <tbody>
                    `;
        $("#table_Container").append(theadTemplate);
            $.each(result.objects, function (i, genero) {
                var trowTemplate =
                    '<tr>'
                        + '<td class="text-center"> <button class="btn btn-info" onclick="GetById(' + genero.idGenero + ')"><span class="bi bi-pencil-square"></span></button></td>'
                        + "<td class='text-center'>" + genero.nombre + "</td>"
                        + "<td class='text-center'>" + genero.descripcion + "</td>"
                        + '<td class="text-center"><button class="btn btn-danger" onclick="Delete(' + genero.idGenero + ')" ><span class="bi bi-trash-fill"></span></button></td>'
                    + "</tr>";

                $("#tableGeneros tbody").append(trowTemplate);
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
    window.location.href = `/Genero/Form?idGenero=${id}`;
}

function Delete(id) {

    if (confirm("¿Estas seguro de eliminar el genero seleccionado?")) {
        window.location.href = `/Genero/Delete?idGenero=${id}`;
    };
};

