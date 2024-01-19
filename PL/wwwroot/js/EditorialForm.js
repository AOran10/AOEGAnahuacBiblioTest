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
        var urlSend = "http://localhost:5056/api/Editorial/add";
    } else {
        var verboSend = "PUT";
        var urlSend = "http://localhost:5056/api/Editorial/update";
    }



    var editorial = {
        "idEditorial": id,
        "nombre": form[1].value,
        "informacionAdicional": form[2].value,
        "imagen": imagenSend,
        "editoriales": [
            "string"
        ]
    }
     
    $.ajax({
        type: verboSend,
        url: urlSend,
        data: JSON.stringify(editorial),
        headers: {
            accept: 'application/json',
            'content-type': 'application/json',
            Authorization: `Bearer ${token}`
        },
        dataType: 'json',
        contentType: "application/json; charset=uft-8",
        success: function (result) {
            alert("Formulario enviado correctamente");
            window.location.href = `/Editorial/GetAll   `;
        },
        error: function (xhr, status, error) {
            alert('No se pudo enviar el formulario.' + error);
        }
    });
}