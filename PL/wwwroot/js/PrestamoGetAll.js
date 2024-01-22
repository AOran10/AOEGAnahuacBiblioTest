$(document).ready(function () {
    renderPrestamos();
});

function renderPrestamos(filtro) {

    $("#table_Container").empty();

    filtro = filtro != undefined ? filtro.value : 0;
    var settings = {
        type: 'GET',
        url: 'http://localhost:5056/api/Prestamo/getall/' + filtro,
        contentType: "application/json; charset=uft-8",
    };
    $.ajax(settings).done(function (result) {
        var theadTemplate =`
            <table class="table table-hover" id="tablePrestamos">
                <thead>
                    <tr>
                        <th>Editar</th>
                        <th>IdPrestamo</th>
                        <th>Usuario</th>
                        <th>Medio</th>
                        <th>Fecha Prestamo</th>
                        <th>Fecha Devolución</th>
                        <th>Estatus</th>
                        <th>Eliminar</th>
                    </tr>
                </thead>
                <tbody>               
                  `;

        $("#table_Container").append(theadTemplate);

        $.each(result.objects, function (i, prestamo) {


            //var Imagen = editorial.imagen != null ? `data:image/png;base64,${editorial.imagen}` : 'https://th.bing.com/th/id/OIP.dhBwcZT_mUoZpOBSNsjHzgAAAA?rs=1&pid=ImgDetMain';

            var trowTemplate =
                '<tr>'
                + '<td class="text-center"> <button class="btn btn-info" onclick="GetById(' + prestamo.idPrestamo + ')"><span class="bi bi-pencil-square"></span></button></td>'
                + "<td class='text-center'>" + prestamo.idPrestamo + "</td>"
                + "<td class='text-center'>" + prestamo.identityUsers.userName + "</td>"
                + "<td class='text-center'>" + prestamo.medio.titulo + "</td>"
                + "<td class='text-center'>" + prestamo.fechaPrestamo + "</td>"
                + "<td class='text-center'>" + prestamo.fechaDevolucion + "</td>"
                + "<td class='text-center'>" + prestamo.estatusPrestamo.descripcion + "</td>"
                + '<td class="text-center"><button class="btn btn-danger " onclick="Delete(' + prestamo.idPrestamo + ')" ><span class="bi bi-trash-fill"></span></button></td>'

                + "</tr>";
            //$("#table_Container").append(trowTemplate);
            $("#tablePrestamos tbody").append(trowTemplate);
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

                window.location.href = `/Prestamo/Form?IdPrestamo=${id}`;


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

    
    if (confirm("¿Estas seguro de finalizar seleccionado?")) {

        var settings = {
            type: 'DELETE',
            url: 'http://localhost:5056/api/Prestamo/delete/' +id,
            contentType: "application/json; charset=uft-8",
        };
        $.ajax(settings).done(function (result) {
            alert('Se ha finalizado');
        }).fail(function (xhr, status, error) {
            alert('Error al eliminar el prestamo.' + error);

        });
    };

};