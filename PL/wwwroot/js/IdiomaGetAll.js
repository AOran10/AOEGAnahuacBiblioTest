<link rel="StyleSheet" href=~/css/Botones.css type = "text/css" >

$(document).ready(function () {
    renderIdiomas();
});

function renderIdiomas() {
    $("#table_Container").empty();

    var settings = {
        type: 'GET',
        url: 'http://localhost:5083/Idioma/GetAllIdioma',
        contentType: "application/json; charset=uft-8",
    };
    $.ajax(settings).done(function (result) {
        var theadTemplate = `
                        <table class="table table-hover" id="tableIdiomas">
                        <thead>
                            <tr>
                                <th>Editar</th>
                                <th>Nombre</th>
                                <th>Eliminar</th>
                            </tr>
                        </thead>
                        <tbody>
                    `;
        $("#table_Container").append(theadTemplate);
        $.each(result.objects, function (i, idioma) {


            var trowTemplate =
                '<tr>'

                + '<td class="text-center"> <button class="btn btn-info" onclick="GetById(' + idioma.idIdioma + ')"><span class="bi bi-pencil-square"></span></button></td>'
                + "<td class='text-center'>" + idioma.nombre + "</td>"
                + '<td class="text-center"><button class="btn btn-danger " onclick="Delete(' + idioma.idIdioma + ')" ><span class="bi bi-trash-fill"></span></button></td>'

                + "</tr>";
            //$("#table_Container").append(trowTemplate);
            $("#tableIdiomas tbody").append(trowTemplate);
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
    window.location.href = `/Idioma/Form?idIdioma=${id}`;
}

function Delete(id) {

    if (confirm("¿Estas seguro de eliminar el idioma seleccionado?")) {
        window.location.href = `/Idioma/Delete?idIdioma=${id}`;
    };
};

