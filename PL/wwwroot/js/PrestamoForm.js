//$(function () {
//    $('#datepicker').datetimepicker();
//});

function SendForm(event) {
    event.preventDefault();
    var token = localStorage.getItem('token');
    var form = document.getElementById("form");

    var id = parseInt(form[0].value);
    var verboSend = "";
    var urlSend = "";

    fecha1 = form[3].value;
    fecha2 = Date(form[3].value);

    fechafinal = fecha1 + "T00:00:00";

    var prestamo = {
        "idPrestamo": id,
        "identityUsers": {
            "idUsuario": form[1].value,
            "userName": "string",
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
        },
        "medio": {
            "idMedio": parseInt(form[2].value),
            "titulo": "h86oahGbWex99fOiyinEn52tzq76K j6ktocxH6O9oVzMnoqqh",
            "tipoMedio": {
                "idTipoMedio": 0,
                "nombre": "PtKRCdboQqiYCFswEmmFGrML,zxsXsgFOnCznDjWnaEjREtgMA",
                "descripcion": "string",
                "tipoMedios": [
                    "string"
                ]
            },
            "editorial": {
                "idEditorial": 0,
                "nombre": "string",
                "informacionAdicional": "string",
                "imagen": "null",
                "editoriales": [
                    "string"
                ]
            },
            "idioma": {
                "idIdioma": 0,
                "nombre": "WdwoXldvxJMFzPyVikpD. ",
                "idiomas": [
                    "string"
                ]
            },
            "autor": {
                "idAutor": 0,
                "nombre": "string",
                "informacionAdicional": "string",
                "imagen": "null",
                "autores": [
                    "string"
                ]
            },
            "genero": {
                "idGenero": 0,
                "nombre": "1hT0FvN54nIcDZ4ZWDPg1hoDu4591ZKtsPxHnuqxLmkoFGtoV5",
                "descripcion": "ppdQGuklgZ3TACzzPOwnfJMBcXNfzLVfQkkZbuf647dF3v4uWxfCSySh",
                "generos": [
                    "string"
                ]
            },
            "paginas": 0,
            "publicacion": "2024-01-16T20:51:21.782Z",
            "cantidadEjemplares": 0,
            "cantidadEnPrestamo": 0,
            "imagen": "null",
            "medios": [
                "string"
            ]
        },
        "fechaPrestamo": "2024-01-16T20:51:21.782Z",
        "fechaDevolucion": fechafinal,
        "estatusPrestamo": {
            "idEstatusPrestamo": 0,
            "descripcion": "string",
            "estatusPrestamoList": [
                "string"
            ]
        },
        "prestamos": [
            "string"
        ]
    };


    if (id == 0) {
        var verboSend = "POST";
        var urlSend = "http://localhost:5056/api/Prestamo/add";
    } else {
        var verboSend = "PUT";
        var urlSend = "http://localhost:5056/api/Prestamo/update";
    }

    $.ajax({
        type: verboSend,
        url: urlSend,
        headers: {
            accept: 'application/json',
            'content-type': 'application/json',
            Authorization: `Bearer ${token}`
        },
        data: JSON.stringify(prestamo),
        dataType: 'json',
        contentType: "application/json; charset=uft-8",
        success: function (result) {
            alert("Formulario enviado Correctamente");
            window.location.href = `/Prestamo/GetAll   `;
        },
        error: function (xhr, status, error) {
            alert('No se pudo enviar este formulario.' + error);
        }
    });
}


