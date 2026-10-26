function inicializarSelect2EnModales() {
    $('.select2-modal').each(function () {
        var $select = $(this);
        var $modal = $select.closest('.modal');

        // Si ya estaba inicializado, lo destruimos para limpiar
        if ($select.hasClass("select2-hidden-accessible")) {
            $select.select2('destroy');
        }

        // Inicializamos Select2
        $select.select2({
            dropdownParent: $modal,
            width: '100%'
        });

        // Forzar el foco en el buscador al abrir
        $select.off('select2:open').on('select2:open', function () {
            var searchField = $modal.find('.select2-container--open .select2-search__field')[0];
            if (searchField) {
                searchField.focus();
            }
        });
    });

    // Limpieza profunda al cerrar cualquier modal
    $('.modal').off('hidden.bs.modal.select2').on('hidden.bs.modal.select2', function () {
        var $modalActual = $(this);
        $modalActual.find('.select2-modal').select2('close');
        $('.select2-container').hide();
    });
}

// Inicializar cuando se muestra el modal de Bootstrap (Garantiza que el DOM del modal ya existe y tiene los datos)
$(document).ready(function () {
    // Cada vez que cualquier modal termine de mostrarse, refrescamos los Select2 de su interior
    $('.modal').on('shown.bs.modal', function () {
        inicializarSelect2EnModales();
    });
});

// Mantener pageLoad por los PostBacks de Web Forms
function pageLoad() {
    // Pequeño respiro para dejar que el DOM asíncrono pinte los datos del servidor
    setTimeout(function () {
        inicializarSelect2EnModales();
    }, 100);
}