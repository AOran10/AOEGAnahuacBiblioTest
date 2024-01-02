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

    var form_data = $(this).serialize();

    var form = document.getElementById("form");

    var IdAutor = parseInt(form[0].value);
    var TxtNombre = form[1].value;
    var TxtInfo = form[2].value;
    var ImgUp = form[3].files[0];


    var autor = {
        "IdAutor": IdAutor,
        "Nombre": TxtNombre,
        "InformacionAdicional": TxtInfo,
        "Imagen": null,
        "Autores": {},
    }

    var fuImagen = ImgUp;

    //"fuImagen": fuImagen
    var settings = {
        type: 'POST',
        url: 'http://localhost:5083/Autor/Form',
        dataType: 'json',
        data: JSON.stringify(autor),
        contentType: "application/json; charset=uft-8",
    };
    $.ajax(settings).done(function (result) {

    }).fail(function (xhr, status, error) {
        alert('No se pudo enviar el formulario.' + error);

    });
}

function renderProductos() {
    $("#productos").empty();
    var area = document.getElementById("ddlArea").value;
    var depa = document.getElementById("ddlDepartamento").value;
    var consultaAbierta = document.getElementById("Busqueda").value;

    var IdArea = area != "" ? parseInt(area) : 0;
    var IdDepartamento = depa != "" ? parseInt(depa) : 0;

    var consulta = {
        "consultaAbierta": consultaAbierta,
        "idArea": IdArea,
        "idDepartamento": IdDepartamento
    }

    var settings = {
        type: 'POST',
        url: 'http://localhost:5286/api/Producto/getall',
        dataType: 'json',
        data: JSON.stringify(consulta),
        contentType: "application/json; charset=uft-8",
    };
    $.ajax(settings).done(function (result) {
        $.each(result.objects, function (i, producto) {
            var id = producto.id;
            var nombre = producto.nombre;
            var imagen = producto.imagen;
            var precio = producto.precio;
            var descripcion = producto.descripcion;
            var cardTemplate = `
                        <div class="col-md-3">
                            <div class="cardProduct" id="${id}" onclick="ProductoGet(this.id)">
                                <div class="cardProductHead">
                                    <img src="data:image;base64,${imagen}" class="img-fluid rounded-start" alt="${nombre}" >
                                </div>
                                <div class="cardProductBody">
                                    <div class="nombreProducto"> 
                                        <p>${nombre}</p>
                                    </div>
                                    <div class="precioProducto">
                                        <p>$ ${precio}.00</p>
                                    </div>
                                </div>
                            </div>
                       </div>
                    `;
            $("#productos").append(cardTemplate);
        });
    }).fail(function (xhr, status, error) {
        alert('Error en la actualizacion.' + error);

    });


}