function guardarTab(tabId) {
    var input = document.getElementById('<%= hfTabActiva.ClientID %>');
    if (input) {
        input.value = tabId;
    }
}
function abrirModalModelo() {
    $('#modalModelo').modal('show');
}
function cerrarModalModelo() {
    $('#modalModelo').modal('hide');
}

function abrirModalCategoria() {
    $('#modalCategoria').modal('show');
}
function cerrarModalCategoria() {
    $('#modalCategoria').modal('hide');
}
function abrirModalMarca() {
    $('#modalMarca').modal('show');
}
function cerrarModalMarca() {
    $('#modalMarca').modal('hide');
}