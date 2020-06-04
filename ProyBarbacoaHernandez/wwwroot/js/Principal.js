
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
            var id = getParameterByName('id');
            if (id != null) {
                document.getElementById("Input_Role").selectedIndex = 1;
                document.getElementById("imageRgistrar").innerHTML = ['<img class="responsive-img " src="', URL + "images/fotos/Usuarios/" + id + ".png", '" title="', escape(id), '"/>'].join('');
                $("#divPassword").hide();
            }
        }
    }
}
