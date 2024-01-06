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
    var formData = new FormData(form);

    var medio = {
        IdMedio: parseInt(form[0].value),
        Titulo: form[1].value,
        TipoMedio: parseInt(form[3].value),
        Editorial: parseInt(form[6].value),
        Idioma: parseInt(form[7].value),
        Autor: parseInt(form[8].value),
        Genero: parseInt(form[9].value),
        Paginas: parseInt(form[2].value),
        //Publicacion: fparseInt(form[8].value),
        Publicacion: form[4].value,
        CantidadEjemplares: parseInt(form[5].value),
        Imagen: null,
        Medios: ["string"]
    };

    formData.append('medio.IdMedio', medio.IdMedio);
    formData.append('medio.Titulo', medio.Titulo);
    formData.append('medio.TipoMedio.IdTipoMedio', medio.TipoMedio);
    formData.append('medio.Editorial.IdEditorial', medio.Editorial);
    formData.append('medio.Idioma.IdIdioma', medio.Idioma);
    formData.append('medio.Autor.IdAutor', medio.Autor);
    formData.append('medio.Genero.IdGenero', medio.Genero);
    formData.append('medio.Paginas', medio.Paginas);
    formData.append('medio.Publicacion', medio.Publicacion);
    formData.append('medio.CantidadEjemplares', medio.CantidadEjemplares);
    formData.append('medio.Imagen', medio.Imagen);


    formData.set('fuImagen', form[10].files[0]);

    $.ajax({
        type: 'POST',
        url: 'http://localhost:5083/MediaAdmin/Form',
        data: formData,
        processData: false,
        contentType: false,
        success: function (result) {
            alert("Formulario enviado correctamente");
            window.location.href = `/MediaAdmin/GetAll   `;
        },
        error: function (xhr, status, error) {
            alert('No se pudo enviar el formulario.' + error);
        }
    });
}