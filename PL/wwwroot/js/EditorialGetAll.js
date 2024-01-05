$(document).ready(function () {
    renderEditoriales();
});

function renderEditoriales() {
    $("#table_Container").empty();

    var settings = {
        type: 'GET',
        url: 'http://localhost:5083/Editorial/GetAllEditorial',
        contentType: "application/json; charset=uft-8",
    };
    $.ajax(settings).done(function (result) {
        var theadTemplate = `
                        <table class="table table-hover" id="tableEditorial">
                        <thead>
                            <tr>
                                <th>Editar</th>
                                <th>Nombre</th>
                                <th>Informacion</th>
                                <th>Imagen</th>
                                <th>Eliminar</th>
                            </tr>
                        </thead>
                        <tbody>
                    `;
        $("#table_Container").append(theadTemplate);
        $.each(result.objects, function (i, editorial) {


            var Imagen = editorial.imagen != null ? `data:image/png;base64,${editorial.imagen}` : 'https://th.bing.com/th/id/OIP.dhBwcZT_mUoZpOBSNsjHzgAAAA?rs=1&pid=ImgDetMain';

            var trowTemplate =
                '<tr>'

                + '<td class="text-center"> <button class="btn btn-info" onclick="GetById(' + editorial.idEditorial + ')"><span class="bi bi-pencil-square"></span></button></td>'
                + "<td class='text-center'>" + editorial.nombre + "</td>"
                + "<td class='text-center'>" + editorial.informacionAdicional + "</td>"
                + "<td class='text-center'><img src='" + Imagen + "' id='imgEditorial' style='width:100px; height:100px;' /></td>"
                + '<td class="text-center"><button class="btn btn-danger " onclick="Delete(' + editorial.idEditorial + ')" ><span class="bi bi-trash-fill"></span></button></td>'

                + "</tr>";
            //$("#table_Container").append(trowTemplate);
            $("#tableEditorial tbody").append(trowTemplate);
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
    window.location.href = `/Editorial/Form?idEditorial=${id}`;
}

function Delete(id) {

    if (confirm("¿Estas seguro de eliminar la editorial seleccionada?")) {
        window.location.href = `/Editorial/Delete?idEditorial=${id}`;
    };
};

