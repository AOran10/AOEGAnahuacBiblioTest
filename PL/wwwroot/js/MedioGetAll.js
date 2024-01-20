
$(document).ready(function () {
    renderMedios();
});

function renderMedios() {
    $("#table_Container").empty();

    var settings = {
        type: 'GET',
        url: 'http://localhost:5083/MediaAdmin/GetAllMedia',
        contentType: "application/json; charset=uft-8",
    };
    $.ajax(settings).done(function (result) {
        var theadTemplate = `
                        <table class="table table-hover" id="tableMedios">
                        <thead>
                            <tr>
                                <th>Editar</th>
                                <th>IdMedio</th>
                                <th>Titulo</th>
                                <th>Tipo de Medio</th>
                                <th>Editorial</th>
                                <th>Idioma</th>
                                <th>Autor</th>
                                <th>Genero</th>
                                <th>Ejemplares</th>
                                <th>En prestamo</th>
                                <th>Imagen</th>
                                <th>Eliminar</th>
                            </tr>
                        </thead>
                        <tbody>
                    `;
        $("#table_Container").append(theadTemplate);
        $.each(result.objects, function (i, medio) {         
            var Imagen = medio.imagen != null ? `data:image/png;base64,${medio.imagen}` : 'https://th.bing.com/th/id/OIP.dhBwcZT_mUoZpOBSNsjHzgAAAA?rs=1&pid=ImgDetMain';

            var trowTemplate =
                '<tr>'

                + '<td class="text-center"> <button class="btn btn-info" onclick="GetById(' + medio.idMedio + ')"><span class="bi bi-pencil-square"></span></button></td>'
                + "<td class='text-center'>" + medio.idMedio + "</td>"
                + "<td class='text-center'>" + medio.titulo + "</td>"
                + "<td class='text-center'>" + medio.tipoMedio.nombre + "</td>"
                + "<td class='text-center'>" + medio.editorial.nombre + "</td>"
                + "<td class='text-center'>" + medio.idioma.nombre + "</td>"
                + "<td class='text-center'>" + medio.autor.nombre + "</td>"
                + "<td class='text-center'>" + medio.genero.nombre + "</td>"
                + "<td class='text-center'>" + medio.cantidadEjemplares + "</td>"
                + "<td class='text-center'>" + medio.cantidadEnPrestamo + "</td>"
                + "<td class='text-center'><img src='" + Imagen + "' id='imgMedio' style='width:100px; height:100px;' /></td>"
                + '<td class="text-center"><button class="btn btn-danger " onclick="Delete(' + medio.idMedio + ')" ><span class="bi bi-trash-fill"></span></button></td>'

                + "</tr>";
            //$("#table_Container").append(trowTemplate);
            $("#tableMedios tbody").append(trowTemplate);
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
    window.location.href = `/MediaAdmin/Form?idMedio=${id}`;
}

function Delete(id) {

    if (confirm("¿Estas seguro de eliminar el medio seleccionado?")) {
        window.location.href = `/MediaAdmin/Delete?idMedio=${id}`;
    };
};

