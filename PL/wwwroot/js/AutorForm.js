function PreviewImagen(event) {

    var output = document.getElementById('imgAutor');
    output.src = URL.createObjectURL(event.target.files[0]);
    output.onload = function () {
        URL.revokeObjectURL(output.src)//free memoryxd
    }
}