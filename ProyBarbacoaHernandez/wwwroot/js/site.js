// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

//PRINCIPAL

var principal = new Principal();

$().ready(() => {
    let URLactual = window.location.pathname;
    principal.userLink(URLactual);
    //$('.sidenav').sidenav();
    M.AutoInit();

});