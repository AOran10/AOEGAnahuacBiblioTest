
$(document).ready(function () {
    renderAutores();
    //nuBank();
});
function renderAutores() {
    $("#table_Container").empty();

    var settings = {
        type: 'GET',
        url: 'http://localhost:5056/api/Autor/getall',
        contentType: "application/json; charset=uft-8",
    };
    $.ajax(settings).done(function (result) {
        var theadTemplate = `
                        <table class="table table-hover" id="tableAutores">
                        <thead>
                            <tr>
                                <th class="text-white">Editar</th>
                                <th class="text-white">Nombre</th>
                                <th class="text-white">Informacion</th>
                                <th class="text-white">Imagen</th>
                                <th class="text-white">Eliminar</th>
                            </tr>
                        </thead>
                        <tbody>
                    `;
        $("#table_Container").append(theadTemplate);
        $.each(result.objects, function (i, autor) {
            var Imagen = autor.imagen != null ? `data:image/png;base64,${autor.imagen}` : 'https://th.bing.com/th/id/OIP.dhBwcZT_mUoZpOBSNsjHzgAAAA?rs=1&pid=ImgDetMain';

            var trowTemplate =
                '<tr>'

                + '<td class="text-center"> <button class="btn btn-info" onclick="GetById(' + autor.idAutor + ')"><span class="bi bi-pencil-square"></span></button></td>'
                + "<td class='text-center'>" + autor.nombre + "</td>"
                + "<td class='text-center'>" + autor.informacionAdicional + "</td>"
                + "<td class='text-center'><img src='" + Imagen + "' id='imgAutor' style='width:100px; height:100px;' /></td>"
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
        alert('Error en la consulta.' + error);

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

                window.location.href = `/Autor/Form?idAutor=${id}`;
                //window.location.href = `/Autor/Form?idAutor=${id}&token=${tokenCreado}`;

                
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

    if (confirm("¿Estas seguro de eliminar al autor seleccionado?")) {

        var settings = {
            type: 'DELETE',
            url: 'http://localhost:5056/api/Autor/delete/' + id,
            contentType: "application/json; charset=uft-8",
        };
        $.ajax(settings).done(function (result) {
            alert('Se ha eliminado el autor');
        }).fail(function (xhr, status, error) {
            alert('Error al eliminar el autor.' + error);

        });
    };
};



function nuBank() {
    var cantidadDinero = prompt("Digita la cantidad del dinero");
    var cantidadDineroFijo = cantidadDinero;

    var dias = 365;
    var taza = 0.16;


    var tazageneral = parseFloat(cantidadDinero - cantidadDineroFijo).toFixed(2);
    var tazadiaria = parseFloat(tazageneral).toFixed(3);;

    for (i = 1; i <= dias; i++) {
        gananciaAlDia = cantidadDinero * tazadiaria;
        cantidadDinero = cantidadDinero + gananciaAlDia
    }

    var total = parseFloat(cantidadDinero).toFixed(2);
    var gananciaTotal = parseFloat(cantidadDinero - cantidadDineroFijo).toFixed(2);

    alert("Si empiezas con " + cantidadDineroFijo + " terminas con " + total + " pesos en 1 año con ganancia de " + gananciaTotal);
}