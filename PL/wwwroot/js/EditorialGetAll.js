
$(document).ready(function () {
    renderEditoriales();
});

function renderEditoriales() {
    $("#table_Container").empty();

    var settings = {
        type: 'GET',
        url: 'http://localhost:5056/api/Editorial/getall',
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

    var inputEscrito = prompt("Debes ingresar tu contraseña primero para esto");
    //title="Manage"
    var emailInput = document.getElementById("emailUserLayout");
    var emailPre = emailInput.text;

    let emailUser = emailPre.substring(6, emailPre.length - 1);

    if (emailUser == inputEscrito) {
        var usuario = {
            "idUsuario": "string",
            "userName": "string",
            "password": "string",
            "email": inputEscrito,
            "rol": {
                "name": "string",
                "roleId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
                "roles": [
                    "string"
                ]
            },
            "identityUsers": [
                "string"
            ]
        }

        $.ajax({
            type: "POST",
            url: "http://localhost:5056/api/Authentication/Validar",
            data: JSON.stringify(usuario),
            dataType: 'json',
            contentType: "application/json; charset=uft-8",
            success: function (result) {
                alert("Se ha autentificado, tienes 2 minutos para modificar el registro");
                //var ready = localStorage.getItem('jwtToken');
                var tokenCreado = result.token;
                localStorage.setItem('token', tokenCreado)

                window.location.href = `/Editorial/Form?idEditorial=${id}`;


            },
            error: function (xhr, status, error) {
                alert('No se pudo autentificar' + error);
            }
        });
    }
    else {
        alert('El correo no coincide');
    }
}


function Delete(id) {

    if (confirm("¿Estas seguro de eliminar al editorial seleccionado?")) {

        var settings = {
            type: 'DELETE',
            url: 'http://localhost:5056/api/Editorial/delete/' + id,
            contentType: "application/json; charset=uft-8",
        };
        $.ajax(settings).done(function (result) {
            alert('Se ha eliminado el autor');
        }).fail(function (xhr, status, error) {
            alert('Error al eliminar la editorial.' + error);

        });
    };
};

