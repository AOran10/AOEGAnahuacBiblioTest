$(document).ready(function () {
    renderAutores();
});

function renderAutores() {
    $("#table_Container").empty();

    var settings = {
        type: 'GET',
        url: 'http://localhost:5083/Autor/GetAllAutor',
        contentType: "application/json; charset=uft-8",
    };
    $.ajax(settings).done(function (result) {
        var theadTemplate = `
                        <table class="table table-hover" id="tableAutores">
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
        $.each(result.objects, function (i, autor) {


            var PreImagen = autor.imagen
            var ImagenFill = `data:image/png;base64,@Convert.ToBase64String(${PreImagen})`;
            var ImagenNull = `https://th.bing.com/th/id/OIP.dhBwcZT_mUoZpOBSNsjHzgAAAA?rs=1&pid=ImgDetMain`;
            var Imagen = autor.imagen != null ? ImagenFill : ImagenNull;

            var trowTemplate =
                '<tr>'

                + '<td class="text-center"> <button class="btn btn-info" onclick="GetById(' + autor.idAutor + ')"><span class="bi bi-pencil-square"></span></button></td>'
                + "<td class='text-center'>" + autor.nombre + "</td>"
                + "<td class='text-center'>" + autor.informacionAdicional + "</td>"
                + "<td class='text-center'><img src='data:image/png;base64,'" + autor.imagen + "' id='imgAutor' style='  width:100px; height:100px; ' /></td>"
                + '<td class="text-center"><button class="btn btn-danger " onclick="Delete(' + autor.idAutor + ')" ><span class="bi bi-trash-fill"></span></button></td>'

                + "</tr>";
            //$("#table_Container").append(trowTemplate);
            $("#tableAutores tbody").append(trowTemplate);
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
    window.location.href = `/Autor/Form?idAutor=${id}`;
}

function Delete(id) {

    if (confirm("¿Estas seguro de eliminar al autor seleccionado?")) {
        window.location.href = `/Autor/Delete?idAutor=${id}`;
    };
};

