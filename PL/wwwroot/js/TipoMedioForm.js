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
    var txtDescripcion = document.getElementById("txtDescripcion");

    var lblError = document.getElementById("lblErrorDescripcion");

    if (txtDescripcion.selectionEnd > 119) {
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

    var tipoMedio = {
        IdTipoMedio: parseInt(form[0].value),
        Nombre: form[1].value,
        Descripcion: form[2].value,
        TipoMedios: ["string"]
    };

    formData.append('tipoMedio.IdTipoMedio', tipoMedio.IdTipoMedio);
    formData.append('tipoMedio.Nombre', tipoMedio.Nombre);
    formData.append('tipoMedio.Descripcion', tipoMedio.Descripcion);



    $.ajax({
        type: 'POST',
        url: 'http://localhost:5083/TipoMedio/Form',
        data: formData,
        processData: false,
        contentType: false,
        success: function (result) {
            alert("Formulario enviado correctamente");
            window.location.href = `/TipoMedio/GetAll   `;
        },
        error: function (xhr, status, error) {
            alert('No se pudo enviar el formulario.' + error);
        }
    });
}