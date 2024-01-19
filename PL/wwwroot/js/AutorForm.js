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

    var myfile = form[3].files[0];
    var imgModelo = form[4].value;


    if (myfile != undefined) {

        var preimg = await getBase64(myfile);

        var imagenSend = preimg.replace(/^data:image\/[a-z]+;base64,/, "");
    }
    else {
        var imagenSend = imgModelo;
    }

    if (id == 0) {
        var verboSend = "POST";
        var urlSend = "http://localhost:5056/api/Autor/add";
    } else {
        var verboSend = "PUT";
        var urlSend = "http://localhost:5056/api/Autor/update";
    }
    


    var autor = {
        "idAutor": id,
        "nombre": form[1].value,
        "informacionAdicional": form[2].value,
        "imagen": imagenSend,
        "autores": [
            "string"
        ]
    }

    //var autorizacion = { "Authorization": "Bearer" + token }

    $.ajax({
        type: verboSend,
        url: urlSend,
        //Authorization: token,
        headers: {
            accept: 'application/json',
            'content-type': 'application/json',
            Authorization: `Bearer ${token}`
        },
        data: JSON.stringify(autor),
        dataType: 'json',
        contentType: "application/json; charset=uft-8",
        success: function (result) {
            alert("Formulario enviado correctamente");
            window.location.href = `/Autor/GetAll   `;
        },
        error: function (xhr, status, error) {
            alert('No se pudo enviar el formulario.' + error);
        }
    });
}

