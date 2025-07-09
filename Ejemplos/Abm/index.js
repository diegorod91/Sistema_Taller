

function cancelarAlta() {
    $('#btnColapse').click();
}

// Valida los campos del alta, muestra el mensaje y retorna si puede continuar con la edicion, si retorna false el click del boton no genera el evento en el servidor
function validarAlta() {
    mensaje="";
    mensaje += verificarCadena("Nombre", getElement('MainContent_TBNombre').value, true);
    mensaje += verificarCadena("Domicilio", getElement('MainContent_TBDomicilio').value, true);

    if (mensaje == "") {
        return true;
    }
    else {
        mostrarMsjValidacion(mensaje);
        return false;
    }
}



// Valida los campos de la edicion, muestra el mensaje y retorna si puede continuar con la edicion, si retorna false el click del boton no genera el evento en el servidor
function validarEdicion(){
    mensaje="";
    mensaje += verificarCadena("Nombre", getElement('MainContent_TBNombreE').value, true);
    mensaje += verificarCadena("Domicilio", getElement('MainContent_TBDomicilioE').value, true);

    if (mensaje == "") {
        return true;
    }
    else {
        mostrarMsjValidacion(mensaje);
        return false;
    }

}

// Carga la informacion del item seleccionado en los controles de la tabla de edicion y la muestra con el efecto modal
function editar(id) {
    callJQAJAX("EmpresaH.ashx/GetById", 'id=' + id, mostrarDatosEditar);
}
function mostrarDatosEditar(objeto) {
    if (!objeto) {
        return;
    }
    if (objeto.Status != "OK") {
        alert(objeto.Message);
        return;
    }
    getElement("MainContent_idAfectado").value = objeto.Data.IdEmpresa;

    getElement("MainContent_TBNombreE").value = objeto.Data.NombreEmpresa;
    getElement("MainContent_TBDomicilioE").value = objeto.Data.DomicilioEmpresa;

    fadeIn('TEdicion');
}


// Carga la informacion del item seleccionado en la tabla de baja y la muestra con el efecto modal
function eliminar(id) {
    callJQAJAX("EmpresaH.ashx/GetById", 'id=' + id, mostrarDatosEliminar);
}

function mostrarDatosEliminar(objeto) {
    if (!objeto) {
        return;
    }
    if (objeto.Status != "OK") {
        alert(objeto.Message);
        return;
    }
    //precarga los valores de los campos de la informacion
    getElement("MainContent_idAfectado").value = objeto.Data.IdEmpresa;
    getElement('txtBorrar').innerHTML = objeto.Data.NombreEmpresa;

    fadeIn('TBaja');
}


// Carga la informacion del item seleccionado en los controles de la tabla de informacion y la muestra con el efecto modal
function info(id) {
    callJQAJAX("EmpresaH.ashx/GetById", 'id=' + id, mostrarInfoGrilla);
}
function mostrarInfoGrilla(objeto) {
    if (!objeto) {
        return;
    }
    if (objeto.Status != "OK") {
        alert(objeto.Message);
        return;
    }
    //precarga los valores de los campos de la informacion
    getElement('LBLNombre').innerHTML = objeto.Data.NombreEmpresa;
    getElement('LBLDomicilio').innerHTML = objeto.Data.DomicilioEmpresa;

    fadeIn('TInfoGrilla');
}
    


// Valida los campos de la busqueda, muestra el mensaje y retorna si puede continuar con la busqueda, si retorna false el click del boton no genera el evento en el servidor
function validarFiltrar() {
    mensaje = '';
    mensaje += verificarCadena('Nombre', getElement('MainContent_TBNombreBusqueda').value, true);

    if (mensaje == "") {
        return true;
    }
    else {
        mostrarMsjValidacion(mensaje);
        return false;
    }
}

