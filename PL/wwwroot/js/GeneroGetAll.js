
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
    //AJAX
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
                var tokenCreado = result.token;
                localStorage.setItem('token', tokenCreado)

                window.location.href = `/Genero/Form?idGenero=${id}`;s


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

    if (confirm("¿Estas seguro de eliminar el genero seleccionado?")) {
        window.location.href = `/Genero/Delete?idGenero=${id}`;
    };
};

