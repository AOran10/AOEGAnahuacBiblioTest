
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
            var idMedio = medio.idMedio;
            var Titulo = medio.titulo;
            var TipoMedio = medio.tipoMedio.nombre;
            var Editorial = medio.editorial.nombre;
            var Idioma = medio.idioma.nombre;
            var Autor = medio.autor.nombre;
            var Genero = medio.genero.nombre;
            var CantidadEjemplares = medio.cantidadEjemplares;
            var CantidadEnPrestamo = medio.cantidadEnPrestamo;

            var Imagen = medio.imagen != null ? `data:image/png;base64,${medio.imagen}` : 'https://th.bing.com/th/id/OIP.dhBwcZT_mUoZpOBSNsjHzgAAAA?rs=1&pid=ImgDetMain';

            var fila = `
                        <tr>
                                    <td class="text-center"><a class="btn btn-primary bi bi-pencil-fill " href="@Url.Action("Form", "MediaAdmin", new {IdMedio = ${idMedio}})"><i class=""></i></a></td>

                                    <td>${idMedio}</td>
                                    <td>${Titulo}</td>
                                    <td>${TipoMedio}</td>
                                    <td>${Editorial}</td>
                                    <td>${Idioma}</td>
                                    <td>${Autor}</td>
                                    <td>${Genero}</td>
                                    <td>${CantidadEjemplares}</td>
                                    <td>${CantidadEnPrestamo}</td>
                                    
                                    <td>
                                            <img ${Imagen} id="imgMedio" style="  width:100px; height:100px; " />
                                        
                                    </td>
                                    <td class="text-center">
                                        <a class="btn btn-danger bi bi-trash" onclick="return confirm('¿Estas seguro de eliminar este medio?');" href="@Url.Action("Delete", "MediaAdmin", new {IdMedio = ${idMedio}})"><i class=""></i></a>
                                    </td>
                                </tr>
                    `;
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

                window.location.href = `/MediaAdmin/Form?idMedio=${id}`;
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

    if (confirm("¿Estas seguro de eliminar el medio seleccionado?")) {
        window.location.href = `/MediaAdmin/Delete?idMedio=${id}`;
    };
};

