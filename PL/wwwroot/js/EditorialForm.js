function PreviewImagen(event) {

    var output = document.getElementById('imgEditorial');
    output.src = URL.createObjectURL(event.target.files[0]);
    output.onload = function () {
        URL.revokeObjectURL(output.src)//free memoryxd
    }
}

function SizeNombre() {
    var txtNombre = document.getElementById("txtNombre");

    var lblError = document.getElementById("lblErrorNombre");

    if (txtNombre.selectionEnd > 49) {
        lblError.innerHTML = "No se admiten más de 50 carácteres";
        lblError.style.color = "red";
        return false;
    } else {
        lblError.innerHTML = "";
        return true;
    }
}

function SizeInfo() {
    var txtInfo = document.getElementById("txtInfo");

    var lblError = document.getElementById("lblErrorInfo");

    if (txtInfo.selectionEnd > 119) {
        lblError.innerHTML = "No se admiten más de 120 carácteres";
        lblError.style.color = "red";
        return false;
    } else {
        lblError.innerHTML = "";
        return true;
    }
}

function SendForm(event) {
    event.preventDefault();

    var form = document.getElementById("form");
    var formData = new FormData(form);

    var editorial = {
        IdEditorial: parseInt(form[0].value),
        Nombre: form[1].value,
        InformacionAdicional: form[2].value,
        Imagen: null,
        Editoriales: ["string"]
    };

    formData.append('editorial.IdEditorial', editorial.IdEditorial);
    formData.append('editorial.Nombre', editorial.Nombre);
    formData.append('editorial.InformacionAdicional', editorial.InformacionAdicional);
    formData.append('editorial.Imagen', editorial.Imagen);


    formData.set('fuImagen', form[3].files[0]);

    $.ajax({
        type: 'POST',
        url: 'http://localhost:5083/Editorial/Form',
        data: formData,
        processData: false,
        contentType: false,
        success: function (result) {
            alert("Formulario enviado correctamente");
            window.location.href = `/Editorial/GetAll   `;
        },
        error: function (xhr, status, error) {
            alert('No se pudo enviar el formulario.' + error);
        }
    });
}
