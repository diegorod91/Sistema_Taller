let tarjetaMovida = null;
let estadoDestinoGlobal = null;
let ordenIdGlobal = null;

document.addEventListener("DOMContentLoaded", function () {
    var columns = document.querySelectorAll(".kanban-column");

    columns.forEach(function (column) {
        new Sortable(column, {
            group: 'kanban-taller',
            animation: 150,
            onAdd: function (evt) {
                tarjetaMovida = evt.item;
                ordenIdGlobal = tarjetaMovida.getAttribute("data-id");
                estadoDestinoGlobal = evt.target.getAttribute("data-estado");

                if (estadoDestinoGlobal == "2" || estadoDestinoGlobal=="3" || estadoDestinoGlobal=="4") {
                    $('#modalTareaDiagnostico').modal('show');
                } else {
                    ejecutarCambioEstado(ordenIdGlobal, estadoDestinoGlobal, "");
                }
            }
        });
    });
});

function confirmarCambioConTarea() {
    let textarea = document.getElementById("txtTareaRealizadaModal");
    let tarea = textarea ? textarea.value : "";

    $('#modalTareaDiagnostico').modal('hide');
    if (textarea) textarea.value = "";

    ejecutarCambioEstado(ordenIdGlobal, estadoDestinoGlobal, tarea);
}

function ejecutarCambioEstado(idOrden, idEstado, tarea) {
    $.ajax({
        type: "POST",
        url: "Default.aspx/CambiarEstadoConTarea",
        data: JSON.stringify({ idOrden: parseInt(idOrden), idEstado: parseInt(idEstado), tareaRealizada: tarea }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response.d) {
                location.reload();
            } else {
                alert("Ocurrió un error al actualizar el estado.");
                location.reload();
            }
        },
        error: function () {
            alert("Error de comunicación con el servidor.");
            location.reload();
        }
    });
}
