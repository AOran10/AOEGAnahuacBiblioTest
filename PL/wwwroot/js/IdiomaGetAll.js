
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
                        <table class="table table-hover text-white" id="tableIdiomas">
                        <thead>
                            <tr>
                                <th class="text-white">Editar</th>
                                <th class="text-white">Nombre</th>
                                <th class="text-white">Eliminar</th>
                            </tr>
                        </thead>
                        <tbody>
                    `;
        $("#table_Container").append(theadTemplate);
        $.each(result.objects, function (i, idioma) {


            var trowTemplate =
                '<tr>'

                + '<td class="text-center text-white"> <button class="btn btn-info" onclick="GetById(' + idioma.idIdioma + ')"><span class="bi bi-pencil-square"></span></button></td>'
                + "<td class='text-center text-white'>" + idioma.nombre + "</td>"
                + '<td class="text-center text-white"><button class="btn btn-danger " onclick="Delete(' + idioma.idIdioma + ')" ><span class="bi bi-trash-fill"></span></button></td>'

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

                window.location.href = `/Idioma/Form?idIdioma=${id}`;


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

    if (confirm("¿Estas seguro de eliminar el idioma seleccionado?")) {
        window.location.href = `/Idioma/Delete?idIdioma=${id}`;
    };
};

