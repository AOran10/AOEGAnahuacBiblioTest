﻿//$(function () {
//    $('#datepicker').datetimepicker();
//});

function SendForm(event) {
    event.preventDefault();

    var form = document.getElementById("form");
    var formData = new FormData(form);

    var prestamo = {
        IdPrestamo: parseInt(form[0].value),
        IdUsuario: form[1].value,
        IdMedio: parseInt(form[2].value),
        FechaPretamo: Date(form[3].value),
        FechaDevolucion: Date(form[4].value),
        IdStatus: parseInt(form[5].value)
    };

    formData.append('prestamo.IdPrestamo', prestamo.IdPrestamo);
    formData.append('prestamo.IdentityUsers.IdUsuario'), prestamo.IdUsuario);
    formData.append('prestamo.Medio.IdMedio'), prestamo.IdMedio);
    formData.append('prestamo.FechaPrestamo'), prestamo.FechaPrestamo);
    formData.append('prestamo.FechaDevolucion'), prestamo.FechaDevolucion);
    formData.append('prestamo.Status.IdStatus'), prestamo.IdStatus);




    $.ajax({
        type: 'POST',
        url: 'http://localhost:5083/Prestamo/PrestamoGetAll',
            data: fromData,
        processData: false,
        contentType: false,
        success: function (result) {
            alert("Formulario enviado Correctamente");
            window.location.href = `/Prestamo/GetAll   `;
        },
        error: function (xhr, status, error) {
            alert('No se pudo enviar este formulario.' + error);
        }
    });
}


