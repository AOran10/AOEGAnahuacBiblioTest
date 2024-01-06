$(document).ready(function () {
    renderTipoMedios();
});

function renderTipoMedios() {
    $("#table_Container").empty();

    var settings = {
        type: 'GET',
        url: 'http://localhost:5083/TipoMedio/GetAllTipoMedio',
        contentType: "application/json; charset=uft-8",
    };
    $.ajax(settings).done(function (result) {
        var theadTemplate = `
                        <table class="table table-hover" id="tableTipoMedio">
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
        $.each(result.objects, function (i, tipoMedio) {



            var trowTemplate =
                '<tr>'

                + '<td class="text-center"> <button class="btn btn-info" onclick="GetById(' + tipoMedio.idTipoMedio + ')"><span class="bi bi-pencil-square"></span></button></td>'
                + "<td class='text-center'>" + tipoMedio.nombre + "</td>"
                + "<td class='text-center'>" + tipoMedio.descripcion + "</td>"
                + '<td class="text-center"><button class="btn btn-danger " onclick="Delete(' + tipoMedio.idTipoMedio + ')" ><span class="bi bi-trash-fill"></span></button></td>'

                + "</tr>";
            //$("#table_Container").append(trowTemplate);
            $("#tableTipoMedio tbody").append(trowTemplate);
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
    window.location.href = `/TipoMedio/Form?idTipoMedio=${id}`;
}

function Delete(id) {

    if (confirm("¿Estas seguro de eliminar el tipo de medio seleccionado?")) {
        window.location.href = `/TipoMedio/Delete?idTipoMedio=${id}`;
    };
};

