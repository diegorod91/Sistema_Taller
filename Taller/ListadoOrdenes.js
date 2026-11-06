(function () {
    // Función de protección interna para asegurar que jQuery esté disponible
    function ejecutarCuandoHayaJQuery(callback) {
        if (window.jQuery) {
            callback(window.jQuery);
        } else {
            setTimeout(function () {
                ejecutarCuandoHayaJQuery(callback);
            }, 50);
        }
    }

    ejecutarCuandoHayaJQuery(function ($) {

        // 1. Inicialización de Select2 en modales
        window.inicializarSelect2EnModales = function () {
            $('.select2-modal').each(function () {
                var $select = $(this);
                var $modal = $select.closest('.modal');

                if ($select.hasClass("select2-hidden-accessible")) {
                    $select.select2('destroy');
                }

                $select.select2({
                    dropdownParent: $modal,
                    width: '100%'
                });

                $select.off('select2:open').on('select2:open', function () {
                    var searchField = $modal.find('.select2-container--open .select2-search__field')[0];
                    if (searchField) {
                        searchField.focus();
                    }
                });
            });

            $('.modal').off('hidden.bs.modal.select2').on('hidden.bs.modal.select2', function () {
                var $modalActual = $(this);
                $modalActual.find('.select2-modal').select2('close');
                $('.select2-container').hide();
            });
        };

        // 2. Validación obligatoria de observaciones en cambio de estado
        // 2. Validación obligatoria de observaciones en cambio de estado
        window.validarYGuardarCambioEstado = function (event) {
            // Buscamos el textarea por ID parcial o por su selector en el DOM
            var txtObs = document.getElementById('txtTareaRealizadaEstado') ||
                document.querySelector("[id*='txtTareaRealizadaEstado']") ||
                document.querySelector("textarea");

            var lblError = document.getElementById('MainContent_LBL_Error') ||
                document.querySelector("[id*='LBL_Error']");

            var observaciones = txtObs ? txtObs.value.trim() : "";

            if (observaciones === "") {
                // Si viene el evento, frenamos la propagación para que Bootstrap no cierre el modal
                if (event) {
                    event.preventDefault();
                    event.stopPropagation();
                }

                if (lblError) {
                    lblError.innerHTML = "Debe ingresar una breve descripción u observación obligatoria para cambiar de estado.";
                    lblError.style.display = "block";
                } else {
                    alert("Debe ingresar una breve descripción u observación obligatoria para cambiar de estado.");
                }
                return false; // 🛑 FRENA EL POSTBACK Y EVITA EL CIERRE
            }

            // Si pasa la validación, limpiamos el error
            if (lblError) {
                lblError.innerHTML = "";
            }

            return true; // Continúa con el cambio de estado y el cierre natural del modal
        };
        // 3. Eventos al cargar el DOM
        $(document).ready(function () {
            $('.modal').on('shown.bs.modal', function () {
                window.inicializarSelect2EnModales();
            });
        });

    });

})();

// 4. Mantener pageLoad global para los UpdatePanels de WebForms
function pageLoad() {
    setTimeout(function () {
        if (typeof window.inicializarSelect2EnModales === 'function') {
            window.inicializarSelect2EnModales();
        }
    }, 100);
}