$(document).ready(function () {
    renderMedios();
});
function renderMedios() {
    $("#mediaContainer").empty();

    var settings = {
        type: 'GET',
        url: 'http://localhost:5083/MediaAdmin/GetAllMedia',
        contentType: "application/json; charset=uft-8",
    };
    $.ajax(settings).done(function (result) {

        $.each(result.objects, function (i, medio) {

            var idMedio = medio.idMedio;
            var Titulo = medio.titulo;
            var Autor = medio.autor.nombre;
            var Imagen = medio.imagen != null ? `data:image/png;base64,${medio.imagen}` : 'https://th.bing.com/th/id/OIP.dhBwcZT_mUoZpOBSNsjHzgAAAA?rs=1&pid=ImgDetMain';

            var cardMedio = `
                        <div class="col-md-4">
                            <a href="#" class="card">
                              <img src="${Imagen}" alt="image_source" class="card__img">
                              <span class="card__footer">
                                <span>${Titulo}</span>
                                <span>${Autor}</span>
                              </span>
                              <span class="card__action">
                                <svg viewBox="0 0 448 512" title="play">
                                  <path d="M424.4 214.7L72.4 6.6C43.8-10.3 0 6.1 0 47.9V464c0 37.5 40.7 60.1 72.4 41.3l352-208c31.4-18.5 31.5-64.1 0-82.6z" />
                                </svg>
                              </span>
                            </a>
                        </div>
                `;

            $("#mediaContainer").append(cardMedio);
        }); 
    }).fail(function (xhr, status, error) {
        alert('Error en la actualizacion.' + error);

    });


}