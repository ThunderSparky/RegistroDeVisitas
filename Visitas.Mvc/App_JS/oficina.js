(function (oficina) {
    oficina.success = successReload;
    oficina.pages = 1;
    oficina.rowSize = 10;

    //Lo que viene ahora es del SignalR
    oficina.hub = {};
    oficina.ids = [];
    oficina.recordInUse = false; //Este es como un flag que me indicará cuando esta en uso un modal

    oficina.addOficina = addOficinaId;
    oficina.removeOficina = removeOficinaId;
    oficina.validate = validate;

    $(function () {
        connectToHub();
        init();
    });

    return oficina;

    //Funcion successReload, la cual es llamada al inicio
    function successReload(option) {
        visitas.closeModal(option); //Aquí cerramos el modal, llamamos al JS que se llama visitas y ejecutamos el metodo closeModal
        //alert("Todo bien");
    }

    //Funcion Init
    function init() {
        $.get('/Oficina/CountPages/' + oficina.rowSize,
            //Esta es la logica cuando recuperemos la informacion
            function (data) {
                oficina.pages = data;
                $('.pagination').bootpag({
                    total: oficina.pages,
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
                    getOficinas(num); //cuando exista un cambio de pagina vamos a volver a traer informacion de la base de datos
                });
                getOficinas(1); //por defecto vamos a llamar a la pagina 1
            });
    }

    //Funcion getOficinas, que es utilizada en la funcion Init
    function getOficinas(num) {
        var url = '/Oficina/List/' + num + '/' + oficina.rowSize;
        $.get(url, function (data) {
            $('.content').html(data);
        })
    }

    //Lo de abajo es para el SignalR
    function addOficinaId(id) {
        oficina.hub.server.addOficinaId(id);
    }

    function removeOficinaId(id) {
        oficina.hub.server.removeOficinaId(id);
    }

    function connectToHub() {
        oficina.hub = $.connection.oficinaHub;
        oficina.hub.client.oficinaStatus = oficinaStatus;
    }

    function oficinaStatus(oficinaIds) {
        console.log(oficinaIds);
        oficina.ids = oficinaIds;
    }

    function validate(id) {
        //evalua si el registro esta en uso
        oficina.recordInUse = (oficina.ids.indexOf(id) > -1);
        if (oficina.recordInUse)
            $('#inUse').removeClass('hidden');
        $('#btn-save').html("");
    }
})(window.oficina = window.oficina || {})