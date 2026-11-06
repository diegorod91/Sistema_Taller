let tarjetaMovida = null;
let estadoDestinoGlobal = null;
let ordenIdGlobal = null;
let columnaOrigenTemp = null;

document.addEventListener("DOMContentLoaded", function() {
    var columns = document.querySelectorAll(".kanban-column");

    columns.forEach(function(column) {
        new Sortable(column, {
            group: 'kanban-taller',
            animation: 150,
            onAdd: function(evt) {
                tarjetaMovida = evt.item;
                ordenIdGlobal = tarjetaMovida.getAttribute("data-id");
                estadoDestinoGlobal = evt.target.getAttribute("data-estado");
                columnaOrigenTemp = evt.from; // Guardamos de qué columna vino

                // Si va al estado de Diagnóstico (Id = 2)
                if (estadoDestinoGlobal == "2" || estadoDestinoGlobal== "3" || estadoDestinoGlobal== "4") {
                    // 1. REVERTIMOS EL MOVIMIENTO VISUAL DE INMEDIATO
                    // Devolvemos la tarjeta a su columna original para que no quede mal ubicada mientras decide
                    columnaOrigenTemp.appendChild(tarjetaMovida);

                    // 2. Abrimos el modal de observaciones
                    $('#modalTareaDiagnostico').modal('show');
                } else {
                    // Si es otro estado que no requiere modal, se mueve y guarda directo
                    ejecutarCambioEstado(ordenIdGlobal, estadoDestinoGlobal, "");
                }
            }
        });
    });
});

// Cuando el usuario hace clic en "Guardar y Avanzar"
// function confirmarCambioConTarea() {
//     let textarea = document.getElementById("txtTareaRealizadaModal");
//     let tarea = textarea ? textarea.value : "";

//     // Cerramos el modal
//     $('#modalTareaDiagnostico').modal('hide');
//     if (textarea) textarea.value = "";

//     // Ejecutamos el guardado y cambio de estado real en el servidor
//     ejecutarCambioEstado(ordenIdGlobal, estadoDestinoGlobal, tarea);
// }

function ejecutarCambioEstado(idOrden, idEstado, tarea) {
    $.ajax({
        type: "POST",
        url: "Default.aspx/CambiarEstadoConTarea",
        data: JSON.stringify({ idOrden: parseInt(idOrden), idEstado: parseInt(idEstado), tareaRealizada: tarea }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function(response) {
            if (response.d) {
                location.reload(); // Recarga limpia con el cambio ya guardado en base de datos
            } else {
                alert("Ocurrió un error al actualizar el estado.");
                location.reload();
            }
        },
        error: function() {
            alert("Error de comunicación con el servidor.");
            location.reload();
        }
    });
}

// Cuando el usuario hace clic en "Guardar y Avanzar"
function confirmarCambioConTarea() {
    let textarea = document.getElementById("txtTareaRealizadaModal");
    let lblError = document.getElementById('MainContent_LBL_Error') || document.querySelector("[id*='LBL_Error']");
    let tarea = textarea ? textarea.value.trim() : "";

    // 1. VALIDACIÓN OBLIGATORIA
    if (tarea === "") {
        if (lblError) {
            lblError.innerHTML = "Debe ingresar una breve descripción u observación obligatoria para cambiar de estado.";
            lblError.style.display = "block";
        } else {
            alert("Debe ingresar una breve descripción u observación obligatoria para cambiar de estado.");
        }
        return; 
    }

    // Si pasa la validación, limpiamos el error por si quedó sucio
    if (lblError) {
        lblError.innerHTML = "";
    }

    // 2. Cerramos el modal solo si el texto es válido
    $('#modalTareaDiagnostico').modal('hide');
    if (textarea) textarea.value = "";

    // 3. Ejecutamos el guardado y cambio de estado real en el servidor
    ejecutarCambioEstado(ordenIdGlobal, estadoDestinoGlobal, tarea);
}