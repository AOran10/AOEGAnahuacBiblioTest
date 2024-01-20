function PreviewImagen(event) {

    var output = document.getElementById('imgMedia');
    output.src = URL.createObjectURL(event.target.files[0]);
    output.onload = function () {
        URL.revokeObjectURL(output.src)//free memoryxd
    }
}
function SendForm(event) {
    event.preventDefault();

    var form = document.getElementById("form");
    var id = parseInt(form[0].value);
    var verboSend = "";
    var urlSend = "";

    var myfile = form[9].files[0];
    var imgModelo = form[10].value;


    if (myfile != undefined) {

        var preimg = await getBase64(myfile);

        var imagenSend = preimg.replace(/^data:image\/[a-z]+;base64,/, "");
    }
    else {
        var imagenSend = imgModelo;
    }

    if (id == 0) {
        var verboSend = "POST";
        var urlSend = "http://localhost:5056/api/Medio/add";
    } else {
        var verboSend = "PUT";
        var urlSend = "http://localhost:5056/api/Medio/update";
    }

    var medio = {
        IdMedio: form[0].value,
        Titulo: form[1].value,
        TipoMedio: form[3].value,
        Editorial: form[6].value,
        Idioma: form[7].value,
        Autor: form[8].value,
        Genero: form[9].value,
        Paginas: form[2].value,
        //Publicacion: fparseInt(form[8].value),
        Publicacion: form[4].value,
        CantidadEjemplares: form[5].value,
        Imagen: imagenSend,
        Medios: ["string"]
    }

    $.ajax({
        type: verboSend,
        url: urlSend,
        data: JSON.stringify(medio),
        dataType: 'json',
        contentType: "application/json; charset=uft-8",
        success: function (result) {
            alert("Formulario enviado correctamente");
            window.location.href = `/MediaAdmin/GetAll   `;
        },
        error: function (xhr, status, error) {
            alert('No se pudo enviar el formulario.' + error);
        }
    });
}