
class Principal{
    constructor() {

    }
    userLink(URLactual) {
        if (URLactual == "/Principal" || URLactual == "/Principal/") {
            document.getElementById("enlace1").classList.add('active');
        } if (URLactual == "/Usuarios" || URLactual == "/Usuarios/") {
            document.getElementById("enlace2").classList.add('active');
        } if (URLactual == "/Usuarios/Registrar/Registrar" || URLactual == "/Usuarios/Registrar/Registrar/") {
            document.getElementById("enlace2").classList.add('active');
        }
    }
}
