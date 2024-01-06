function PreviewImagen(event) {

    var output = document.getElementById('imgAutor');
    output.src = URL.createObjectURL(event.target.files[0]);
    output.onload = function () {
        URL.revokeObjectURL(output.src)//free memoryxd
    }
}

function OnlyLetters(event) {
    var letra = event.key;
    var regexOnlyLetter = /^([a-zA-Z áéíóúüÁÉÍÓÚÜñÑ]{1,60}[\,\-\.]{0,1}[\s]{0,1}){1,3}$/;
    var txtNombre = document.getElementById("txtNombre");

    var lblError = document.getElementById("lblErrorNombre");

    if (txtNombre.selectionEnd > 49) {
        lblError.innerHTML = "No se admiten más de 50 carácteres";
        lblError.style.color = "red";
        return false;
    } else {
        if (regexOnlyLetter.test(letra)) {
            lblError.innerHTML = "Entrada Valida";
            lblError.style.color = "green";
            return true;
        }
        else {
            lblError.innerHTML = "Solo se admiten letras";
            lblError.style.color = "red";
            return false;
        }
    }
}
function SizeInfo() {
    var txtInfo = document.getElementById("txtInfo");

    var lblError = document.getElementById("lblErrorInfo");

    if (txtInfo.selectionEnd > 119)
    {
        lblError.innerHTML = "No se admiten más de 120 carácteres";
        lblError.style.color = "red";
        return false;
    } else
    {
        lblError.innerHTML = "";
        return true;
    }
}

function SendForm(event) {
    event.preventDefault();

    var form = document.getElementById("form");
    var formData = new FormData(form);

    var autor = {
        IdAutor: parseInt(form[0].value),
        Nombre: form[1].value,
        InformacionAdicional: form[2].value,
        Imagen: null,
        Autores: ["string"]
    };

    formData.append('autor.IdAutor', autor.IdAutor);
    formData.append('autor.Nombre', autor.Nombre);
    formData.append('autor.InformacionAdicional', autor.InformacionAdicional);
    formData.append('autor.Imagen', autor.Imagen);


    formData.set('fuImagen', form[3].files[0]);

    $.ajax({
        type: 'POST',
        url: 'http://localhost:5083/Autor/Form',
        data: formData,
        processData: false,
        contentType: false,
        success: function (result) {
            alert("Formulario enviado correctamente");
            window.location.href = `/Autor/GetAll   `;
        },
        error: function (xhr, status, error) {
            alert('No se pudo enviar el formulario.' + error);
        }
    });
}

