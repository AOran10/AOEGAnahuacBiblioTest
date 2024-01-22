function PreviewImagen(event) {

    var output = document.getElementById('imgMedia');
    output.src = URL.createObjectURL(event.target.files[0]);
    output.onload = function () {
        URL.revokeObjectURL(output.src)//free memoryxd
    }
}
function getBase64(file) {
    return new Promise((resolve, reject) => {
        const reader = new FileReader();
        reader.readAsDataURL(file);
        reader.onload = () => resolve(reader.result);
        reader.onerror = error => reject(error);
    });
}
async function SendForm(event) {
    event.preventDefault();

    var token = localStorage.getItem('token');

    var form = document.getElementById("form");

    var id = parseInt(form[0].value);
    var verboSend = "";
    var urlSend = "";

    var myfile = form[10].files[0];
    var imgModelo = form[11].value;

    if (id == 0) {
        var verboSend = "POST";
        var urlSend = "http://localhost:5056/api/Medio/add";
    } else {
        var verboSend = "PUT";
        var urlSend = "http://localhost:5056/api/Medio/update";
    }

    if (myfile != undefined) {

        var preimg = await getBase64(myfile);

        var imagenSend = preimg.replace(/^data:image\/[a-z]+;base64,/, "");
    }
    else {
        var imagenSend = imgModelo;
    }
    //"2024-01-19T00:00:00"
    

    fecha1 = form[4].value;
    fecha2 = Date(form[4].value);

    fechafinal = fecha1 + "T00:00:00";

    var medio = {
        "idMedio": id,
        "titulo": form[1].value,
        "tipoMedio": {
            "idTipoMedio": parseInt(form[3].value),
            "nombre": "Ccwu.DNLUebfJhEwMcrcDNmVKUAXK ",
            "descripcion": "string",
            "tipoMedios": [
                "string"
            ]
        },
        "editorial": {
            "idEditorial": parseInt(form[6].value),
            "nombre": "string",
            "informacionAdicional": "string",
            "imagen": "null",
            "editoriales": [
                "string"
            ]
        },
        "idioma": {
            "idIdioma": parseInt(form[7].value),
            "nombre": "mXu. ",
            "idiomas": [
                "string"
            ]
        },
        "autor": {
            "idAutor": parseInt(form[8].value),
            "nombre": "string",
            "informacionAdicional": "string",
            "imagen": "null",
            "autores": [
                "string"
            ]
        },
        "genero": {
            "idGenero": parseInt(form[9].value),
            "nombre": "tWi775X96F-9UtQZ61edJiPzRXWOlELE3SjWQc7ZL08ksbWzHn",
            "descripcion": "lOrM. jUOvBdGyK0x6X8JSU99lQVmM5fqqONX7xISo2uhhGaaBlAE1H1MOi4Oak31",
            "generos": [
                "string"
            ]
        },
        "paginas": parseInt(form[2].value),
        "publicacion": fechafinal,
        "cantidadEjemplares": parseInt(form[5].value),
        "cantidadEnPrestamo": 0,
        "imagen": imagenSend,
        "estatusMedio": {
            "idEstatusMedio": 0,
            "descripcion": "string",
            "estatusMedioList": [
                "string"
            ]
        },
        "medios": [
            "string"
        ]
    }


    $.ajax({
        type: verboSend,
        url: urlSend,
        headers: {
            accept: 'application/json',
            'content-type': 'application/json',
            Authorization: `Bearer ${token}`
        },
        data: JSON.stringify(medio),
        dataType: 'json',
        contentType: "application/json; charset=uft-8",
        success: function (result) {
            alert("Formulario enviado correctamente");
            window.location.href = `/MediaAdmin/GetAll `;
        },
        error: function (xhr, status, error) {
            alert('No se pudo enviar el formulario.' + error);
        }
    });
}