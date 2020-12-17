(function (visitante) {
    visitante.success = successReload;
    visitante.pages = 1;
    visitante.rowSize = 10;

    //Lo que viene ahora es del SignalR
    visitante.hub = {};
    visitante.ids = [];
    visitante.recordInUse = false; //Este es como un flag que me indicará cuando esta en uso un modal

    visitante.addVisitante = addVisitanteId;
    visitante.removeVisitante = removeVisitanteId;
    visitante.validate = validate;

    $(function () {
        connectToHub();
        init();
    });

    return visitante;

    //Funcion successReload, la cual es llamada al inicio
    function successReload(option) {
        visitas.closeModal(option); //Aquí cerramos el modal, llamamos al JS que se llama visitas y ejecutamos el metodo closeModal
        //alert("Todo bien");
    }

    //Funcion Init
    function init() {
        $.get('/Visitante/CountPages/' + visitante.rowSize,
            //Esta es la logica cuando recuperemos la informacion
            function (data) {
                visitante.pages = data;
                $('.pagination').bootpag({
                    total: visitante.pages,
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
                    getVisitante(num); //cuando exista un cambio de pagina vamos a volver a traer informacion de la base de datos
                });
                getVisitante(1); //por defecto vamos a llamar a la pagina 1
            });
    }

    //Funcion getOficinas, que es utilizada en la funcion Init
    function getVisitante(num) {
        var url = '/Visitante/List/' + num + '/' + visitante.rowSize;
        $.get(url, function (data) {
            $('.content').html(data);
        })
    }

    //Lo de abajo es para el SignalR
    function addVisitanteId(id) {
        visitante.hub.server.addVisitanteId(id);
    }

    function removeVisitanteId(id) {
        visitante.hub.server.removeVisitanteId(id);
    }

    function connectToHub() {
        visitante.hub = $.connection.visitanteHub;
        visitante.hub.client.visitanteStatus = visitanteStatus;
    }

    function visitanteStatus(visitanteIds) {
        console.log(visitanteIds);
        visitante.ids = visitanteIds;
    }

    function validate(id) {
        //evalua si el registro esta en uso
        visitante.recordInUse = (visitante.ids.indexOf(id) > -1);
        if (visitante.recordInUse)
            $('#inUse').removeClass('hidden');
        $('#btn-save').html("");
    }

})(window.visitante = window.visitante || {})