<link rel="StyleSheet" href=~/css/Botones.css type = "text/css" >

$(document).ready(function () {
    renderPrestamos();
});

function renderPrestamos() {
    $("#table_container").empty();

    var settings = {
        type: 'GET',
        url: 'http://localhost:5083/Prestamo/GetAllPrestamo',
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
                        <th>Id Medio</th>
                        <th>Fecha Prestamo</th>
                        <th>Fecha Devolución</th>
                        <th>Status</th>
                        <th>Eliminar</th>
                    </tr>
                </thead>
                <tbody>               
                  `;

        $("#table_Container").append(theadTemplate);

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
    window.location.href = `/ Prestamo / Form ? IdPrestamo = ${ id } `;
}

        function Delete(id) {

    if (confirm("¿Estas seguro de Eliminar el Registro del Prestamo seleccionado?")) {
        window.location.href = `/ Prestamo / Delete ? IdPrestamo = ${ id } `;
    };

        };