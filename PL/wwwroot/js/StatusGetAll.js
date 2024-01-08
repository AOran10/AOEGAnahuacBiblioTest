$(document).ready(function () {
    renderStatus();
});

function renderStatus() {
    $("#table_Container").empty();

    var settings = {
        type: 'GET',
        url: 'http://localhost:5083/Status/StatusGetAll',
        contentType: "application/json; charset=uft-8",
    };
    $.ajax(settings).done(function (result) {
        var theadTemplate = `
                        <table class="table table-hover" id="tableStatus">
                        <thead>
                            <tr>
                                 <th>Editar</th>
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

                + '<td class="text-center"><button class="btn btn-info" onclick="GetById(' + status.IdStatus + ')"><span class="bi bi-pencil-square"></span></button></td>'
                + "<td class='text-center'>" + status.descripcion + "</td>"
                + '<td class="text-center"><button class="btn btn-danger" onclick="Delete(' + status.IdStatus + ')"><span class="bi bi-trash"></span></button></td>'
     
                + "</tr>";
            
            $("#tableStatus tbody").append(trowTemplate);
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
    window.location.href = `/Status/Form?IdStatus=${id}`;
}

function Delete(id) {

    if (confirm("¿Estas seguro de eliminar el Status seleccionado?")) {
        window.location.href = `/Status/Delete?IdStatus=${id}`;
    };
};