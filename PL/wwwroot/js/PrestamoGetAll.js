$(document).ready(function () {
    renderPrestamos();
});

function renderPrestamos() {
    $("#table_container").empty();

    var settings = {
        type: 'GET',
        url: '',
        contentType: "application/json; charset=uft-8",
    };
    $.ajax(settings).done(function (result) {
        var theadTemplate =
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
                $("#table_container").append(theadTemplate);
                $.each(result.objects, function (i, prestamo){

                    '<tr>'
                    + '<td class="text-center"><button class="btn btn-info" onclick="GetById(' + prestamo.IdPrestamo + ')"><span class=""></span></button></td>'
                    + "<td class='text-center'>" + prestamo.IdPrestamo + "</td>"
                    + "<td class='text-center'>" + prestamo.IdentityUser + "</td>"
                    + "<td class='text-center'>" + prestamo.IdMedio + "</td>"
                    + "<td class='text-center'>" + prestamo.FechaPrestamo + "</td>"
                    + "<td class='text-center'>" + prestamo.FechaDevolucion + "</td>"
                    + "<td class='text-center'>" + prestamo.IdStatus + "</td>"
                    + '<td class="text-center"><button class="btn btn-danger " onclick="Delete(' + autor.idAutor + ')" ><span class="bi bi-trash-fill"></span></button></td>'

                    + "</tr>";
                   
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
    window.location.href = `/ Prestamo / Form ? IdPrestamo = ${ id } `;
}

function Delete(id) {

    if (confirm("¿Estas seguro de Eliminar el Registro del Prestamo seleccionado?")) {
        window.location.href = `/ Prestamo / Delete ? IdPrestamo = ${ id } `;
    };
};