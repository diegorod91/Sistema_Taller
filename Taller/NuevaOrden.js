function abrirModalCliente() {
    $('#modalCliente').modal('show');
}

function cerrarModalCliente() {
    $('#modalCliente').modal('hide');
}

// Declaración global para que el botón del HTML pueda abrir el modal del patrón
window.abrirModalPatron = function () {
    $('#modalPatronCliente').modal('show');
    if (typeof window.limpiarCanvasPatron === 'function') {
        window.limpiarCanvasPatron();
    }
};

document.addEventListener("DOMContentLoaded", function () {
    const grid = document.getElementById("patternLockGrid");
    const canvas = document.getElementById("patternCanvas");
    if (!grid || !canvas) return;

    const ctx = canvas.getContext("2d");
    const dots = grid.querySelectorAll(".pattern-dot");
    const txtClave = document.getElementById(window.idTextBoxClave);
    const btnLimpiar = document.getElementById("btnLimpiarPatron");

    let drawing = false;
    let selectedSequence = [];
    let selectedPointsCoords = []; // Puntos centrales (x, y) de los nodos encendidos
    let currentCursorPos = null;    // Posición actual del dedo/mouse para el trazo elástico

    function getDotCenter(dot) {
        const rect = dot.getBoundingClientRect();
        const gridRect = grid.getBoundingClientRect();
        return {
            x: rect.left + rect.width / 2 - gridRect.left,
            y: rect.top + rect.height / 2 - gridRect.top,
            dot: dot
        };
    }

    function checkHit(clientX, clientY) {
        for (let dot of dots) {
            const rect = dot.getBoundingClientRect();
            // Área de sensibilidad limpia alrededor del punto
            if (clientX >= rect.left - 12 &&
                clientX <= rect.right + 12 &&
                clientY >= rect.top - 12 &&
                clientY <= rect.bottom + 12) {
                return dot;
            }
        }
        return null;
    }

    function redrawCanvas() {
        ctx.clearRect(0, 0, canvas.width, canvas.height);

        if (selectedPointsCoords.length === 0) return;

        ctx.beginPath();
        ctx.strokeStyle = "#007bff"; // Color del trazo estilo neón
        ctx.lineWidth = 4;
        ctx.lineCap = "round";
        ctx.lineJoin = "round";

        // Trazar líneas uniendo todos los puntos ya encendidos
        ctx.moveTo(selectedPointsCoords[0].x, selectedPointsCoords[0].y);
        for (let i = 1; i < selectedPointsCoords.length; i++) {
            ctx.lineTo(selectedPointsCoords[i].x, selectedPointsCoords[i].y);
        }

        // Trazar línea dinámica desde el último punto hasta el puntero actual
        if (drawing && currentCursorPos) {
            ctx.lineTo(currentCursorPos.x, currentCursorPos.y);
        }

        ctx.stroke();
    }

    function processPoint(dot) {
        let id = dot.getAttribute("data-id");
        if (!selectedSequence.includes(id)) {
            selectedSequence.push(id);
            dot.classList.add("active"); // Enciende el punto con animación CSS

            const center = getDotCenter(dot);
            selectedPointsCoords.push(center);

            if (txtClave) {
                txtClave.value = "Patrón: " + selectedSequence.join("-");
            }
        }
    }

    function getGridCoordinates(clientX, clientY) {
        const gridRect = grid.getBoundingClientRect();
        return {
            x: clientX - gridRect.left,
            y: clientY - gridRect.top
        };
    }

    function startDrawing(e) {
        e.preventDefault();
        drawing = true;
        selectedSequence = [];
        selectedPointsCoords = [];
        resetGrid();

        let clientX = e.clientX || (e.touches ? e.touches[0].clientX : 0);
        let clientY = e.clientY || (e.touches ? e.touches[0].clientY : 0);

        currentCursorPos = getGridCoordinates(clientX, clientY);

        let dot = checkHit(clientX, clientY);
        if (dot) processPoint(dot);

        redrawCanvas();
    }

    function handleMove(e) {
        if (!drawing) return;
        e.preventDefault();

        let clientX = e.clientX;
        let clientY = e.clientY;

        if (e.touches && e.touches.length > 0) {
            clientX = e.touches[0].clientX;
            clientY = e.touches[0].clientY;
        }

        if (clientX !== undefined && clientY !== undefined) {
            currentCursorPos = getGridCoordinates(clientX, clientY);
            let dot = checkHit(clientX, clientY);
            if (dot) processPoint(dot);

            redrawCanvas();
        }
    }

    function stopDrawing() {
        if (!drawing) return;
        drawing = false;
        currentCursorPos = null;
        redrawCanvas();
    }

    function resetGrid() {
        dots.forEach(d => d.classList.remove("active"));
        selectedSequence = [];
        selectedPointsCoords = [];
        currentCursorPos = null;
        ctx.clearRect(0, 0, canvas.width, canvas.height);
        if (txtClave) txtClave.value = "";
    }

    // Exponemos las funciones para uso externo
    window.limpiarCanvasPatron = resetGrid;
    window.obtenerSecuenciaActual = function () {
        return selectedSequence;
    };

    // Eventos Mouse
    grid.addEventListener("mousedown", startDrawing);
    window.addEventListener("mousemove", handleMove);
    window.addEventListener("mouseup", stopDrawing);

    // Eventos Táctiles (Móvil)
    grid.addEventListener("touchstart", startDrawing, { passive: false });
    window.addEventListener("touchmove", handleMove, { passive: false });
    window.addEventListener("touchend", stopDrawing);

    // Botón Limpiar interno
    if (btnLimpiar) {
        btnLimpiar.addEventListener("click", function () {
            resetGrid();
        });
    }
});

// Lógica al confirmar el patrón trazado
document.addEventListener("DOMContentLoaded", function () {
    let btnConfirmar = document.getElementById('btnConfirmarPatron');
    if (btnConfirmar) {
        btnConfirmar.addEventListener('click', function () {
            var secuenciaPatron = window.obtenerSecuenciaActual ? window.obtenerSecuenciaActual() : [];

            if (secuenciaPatron.length < 3) {
                alert("Por favor, dibuje un patrón válido de al menos 3 puntos.");
                return;
            }

            // Buscamos el HiddenField de forma segura sin usar etiquetas de servidor <%= %>
            var hf = document.querySelector("[id$='hfPatronResultado']");
            if (hf) {
                hf.value = JSON.stringify(secuenciaPatron);
            }

            // Cerramos el modal de forma limpia
            $('#modalPatronCliente').modal('hide');

            // Indicador visual en la pantalla principal
            let lblEstado = document.getElementById('lblEstadoPatronCargado');
            if (lblEstado) {
                lblEstado.innerHTML = "✔ Patrón registrado correctamente";
                lblEstado.className = "text-success font-weight-bold";
            }
        });
    }
});
// Función para limpiar el resultado si cancela o cierra el modal
function limpiarResultadoPatron() {
    // 1. Limpiamos el canvas visualmente usando la función global
    if (typeof window.limpiarCanvasPatron === 'function') {
        window.limpiarCanvasPatron();
    }

    // 2. Vaciamos el HiddenField por si tenía un patrón viejo cargado
    var hf = document.querySelector("[id$='hfPatronResultado']");
    if (hf) {
        hf.value = "";
    }

    // 3. Limpiamos el texto indicador de la pantalla principal si existía
    let lblEstado = document.getElementById('lblEstadoPatronCargado');
    if (lblEstado) {
        lblEstado.innerHTML = "";
    }
}

// Evento nativo de Bootstrap para detectar cuando el modal termina de cerrarse (incluso con la tecla Escape o la X)
$(document).ready(function () {
    $('#modalPatronCliente').on('hidden.bs.modal', function () {
        limpiarResultadoPatron();
    });
});