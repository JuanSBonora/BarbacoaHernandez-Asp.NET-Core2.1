// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

//PRINCIPAL

var principal = new Principal();

//CODIGO DE USUSARIOS
var usuarios = new Usuarios();
var imageUser = (evt) => {
    usuarios.archivo(evt, "imageRgistrar");
}

$().ready(() => {
    let URLactual = window.location.pathname;
    principal.userLink(URLactual);
    //$('.sidenav').sidenav();
    M.AutoInit();

    if (URLactual == "/Usuarios/Registrar/Registrar" || URLactual == "/Usuarios/Registrar/Registrar/") {
        document.getElementById('files').addEventListener('change', imageUser, false);
    }
});