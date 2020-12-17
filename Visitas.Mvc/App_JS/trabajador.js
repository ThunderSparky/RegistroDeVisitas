(function (trabajador) {
    trabajador.success = successReload;
    trabajador.pages = 1;
    trabajador.rowSize = 10;

    //Lo que viene ahora es del SignalR
    trabajador.hub = {};
    trabajador.ids = [];
    trabajador.recordInUse = false; //Este es como un flag que me indicará cuando esta en uso un modal

    trabajador.addTrabajador = addTrabajadorId;
    trabajador.removeTrabajador = removeTrabajadorId;
    trabajador.validate = validate;

    $(function () {
        connectToHub();
        init();
    });

    return trabajador;

    //Funcion successReload, la cual es llamada al inicio
    function successReload(option) {
        visitas.closeModal(option); //Aquí cerramos el modal, llamamos al JS que se llama visitas y ejecutamos el metodo closeModal
        //alert("Todo bien");
    }

    //Funcion Init
    function init() {
        $.get('/Trabajador/CountPages/' + trabajador.rowSize,
            //Esta es la logica cuando recuperemos la informacion
            function (data) {
                trabajador.pages = data;
                $('.pagination').bootpag({
                    total: trabajador.pages,
                    page: 1,
                    maxVisible: 5,
                    leaps: true,
                    firstLastUse: true,
                    first: '<-',
                    last: '->',
                    wrapClass: 'pagination',
                    activeClass: 'active',
                    disabledClass: 'disabled',
                    nextClass: 'next',
                    prevClass: 'prev',
                    lastClass: 'last',
                    firstClass: 'first'
                }).on('page', function (event, num) {
                    getTrabajador(num); //cuando exista un cambio de pagina vamos a volver a traer informacion de la base de datos
                });
                getTrabajador(1); //por defecto vamos a llamar a la pagina 1
            });
    }

    //Funcion getOficinas, que es utilizada en la funcion Init
    function getTrabajador(num) {
        var url = '/Trabajador/List/' + num + '/' + trabajador.rowSize;
        $.get(url, function (data) {
            $('.content').html(data);
        })
    }

    //Lo de abajo es para el SignalR
    function addTrabajadorId(id) {
        trabajador.hub.server.addTrabajadorId(id);
    }

    function removeTrabajadorId(id) {
        trabajador.hub.server.removeTrabajadorId(id);
    }

    function connectToHub() {
        trabajador.hub = $.connection.trabajadorHub;
        trabajador.hub.client.trabajadorStatus = trabajadorStatus;
    }

    function trabajadorStatus(trabajadorIds) {
        console.log(trabajadorIds);
        trabajador.ids = trabajadorIds;
    }

    function validate(id) {
        //evalua si el registro esta en uso
        trabajador.recordInUse = (trabajador.ids.indexOf(id) > -1);
        if (trabajador.recordInUse)
            $('#inUse').removeClass('hidden');
        $('#btn-save').html("");
    }
})(window.trabajador = window.trabajador || {})