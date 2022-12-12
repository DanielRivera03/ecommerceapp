function HoraActualSistema() {
    Ahora = new Date();
    Hora = Ahora.getHours();
    Minutos = Ahora.getMinutes();
    Segundos = Ahora.getSeconds();
    Minutos = ValidarHoraSistema(Minutos);
    Segundos = ValidarHoraSistema(Segundos);
    document.getElementById("reloj").innerHTML = Hora + ":" + Minutos;
    t = setTimeout("HoraActualSistema()", 500);
}
function ValidarHoraSistema(i) {
    if (i < 10) {
        i = "0" + i;
    }
    return i;
}
window.onload = function () {
    HoraActualSistema();
}