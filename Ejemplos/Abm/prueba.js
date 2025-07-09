//  Funciones Alta  /////////////////////////////////////////////////////////
function validar_alta()
{
	mensaje="";
	     mensaje += verificarCadena("NombreConsecionaria", getElement("MainContent_TB_NombreConsecionaria").value, true);
     mensaje += verificarCadena("Direccion", getElement("MainContent_TB_Direccion").value, true);

	if (mensaje == "") {
        return true;
    }
    else {
        mostrarMsjValidacion(mensaje);
        return false;
    }
}

//  Funciones Edicion  /////////////////////////////////////////////////////////
function validarEdicion()
{
	mensaje="";
	     mensaje += verificarCadena("NombreConsecionaria", getElement("MainContent_TB_Edit_NombreConsecionaria").value, true);
     mensaje += verificarCadena("Direccion", getElement("MainContent_TB_Edit_Direccion").value, true);

	if (mensaje == "") {
        return true;
    }
    else {
        mostrarMsjValidacion(mensaje);
        return false;
    }
}
function editar(id)
{
	callJQAJAX("pruebaH.ashx/GetById", 'id=' + id, mostrarDatosEditar);
}
function mostrarDatosEditar(objeto)
{
	getElement("MainContent_HF_IdAfectado").value = objeto.Data.Id;
	     getElement("MainContent_TB_Edit_NombreConsecionaria").value = objeto.Data.NombreConsecionaria;
     getElement("MainContent_TB_Edit_Direccion").value = objeto.Data.Direccion;

    fadeIn('modalEditar');
}

//  Funciones Baja  /////////////////////////////////////////////////////////
function eliminar(id)
{
	callJQAJAX("pruebaH.ashx/GetById", 'id=' + id, mostrarDatosBaja);
}
function mostrarDatosBaja(objeto)
{
	getElement("MainContent_HF_IdAfectado").value = objeto.Data.Id;
	getElement("txtBorrar").innerHTML = objeto.Data.NombreConsecionaria;
    fadeIn('modalBaja');
}

//  Funciones Info  /////////////////////////////////////////////////////////
function info(id)
{
	callJQAJAX("pruebaH.ashx/GetById", 'id=' + id, mostrarDatosInfo);
}
function mostrarDatosInfo(objeto) {
	    getElement("LBL_Info_NombreConsecionaria").innerHTML = objeto.Data.NombreConsecionaria;
    getElement("LBL_Info_Direccion").innerHTML = objeto.Data.Direccion;

    fadeIn('modalInfo');
}

//  Funciones Filtrar  /////////////////////////////////////////////////////////
function validarFiltrar()
{
	mensaje="";
	mensaje+=verificarCadena("NombreConsecionaria", getElement('MainContent_TB_Busqueda').value, true);
	if (mensaje == "") {
        return true;
    }
    else {
        mostrarMsjValidacion(mensaje);
        return false;
    }
}
