(function (visita) {
    visita.success = successReload;
    visita.pages = 1;
    visita.rowSize = 10;

    visita.searchByFilter = searchByFilter;

    //visita.limpiarFilter = limpiarFilter;

    //Lo que viene ahora es del SignalR
    visita.hub = {};
    visita.ids = [];
    visita.recordInUse = false; //Este es como un flag que me indicará cuando esta en uso un modal

    visita.addVisita = addVisitaId;
    visita.removeVisita = removeVisitaId;
    visita.validate = validate;

    $(function () {
        connectToHub();
        init();
    });

    return visita;

    //Funcion successReload, la cual es llamada al inicio
    function successReload(option) {
        visitas.closeModal(option); //Aquí cerramos el modal, llamamos al JS que se llama visitas y ejecutamos el metodo closeModal
        //alert("Todo bien");
    }

    //Funcion Init
    function init() {
        $.get('/Visita/CountPages/' + visita.rowSize,
            //Esta es la logica cuando recuperemos la informacion
            function (data) {
                visita.pages = data;
                $('.pagination').bootpag({
                    total: visita.pages,
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
                    getVisita(num); //cuando exista un cambio de pagina vamos a volver a traer informacion de la base de datos
                });
                getVisita(1); //por defecto vamos a llamar a la pagina 1
            });
        document.getElementById("visitasFecha1").defaultValue = "2020-01-01";
        document.getElementById("visitasFecha2").defaultValue = "2020-01-01";
    }

    //Funcion getOficinas, que es utilizada en la funcion Init
    function getVisita(num) {
        var url = '/Visita/List/' + num + '/' + visita.rowSize;
        $.get(url, function (data) {
            $('.content').html(data);
        })
    }

    function searchByFilter() {
        var visitaFecha1 = document.getElementById("visitasFecha1");
        console.log(visitaFecha1.value);
        var visitaFecha2 = document.getElementById("visitasFecha2");
        console.log(visitaFecha2.value);

        //if (visitaFecha1.value == 'dd/mm/aaaa' || visitaFecha2.value == 'dd/mm/aaaa')
        //{
        //    visitaFecha1.value = '-';
        //    visitaFecha2.value = '-';
        //} 

        var url = '/Visita/ListByFilters/' + visitaFecha1.value + '/' + visitaFecha2.value;
        $.get(url, function (data) {
            $('.content').html(data);
        })
        $('#paginacion1').addClass('hidden');
        $('#paginacion2').addClass('hidden');
    }

    //function limpiarFilter() {
    //    //$('#paginacion1').removeClass('hidden');
    //    //$('#paginacion2').removeClass('hidden');
    //    //visitaFecha1.value = '';
    //    //visitaFecha2.value = 'dd/mm/aaaa';
    //    //init();
    //    $.get('/Visita')
    //}

    //Lo de abajo es para el SignalR
    function addVisitaId(id) {
        visita.hub.server.addVisitaId(id);
    }

    function removeVisitaId(id) {
        visita.hub.server.removeVisitaId(id);
    }

    function connectToHub() {
        visita.hub = $.connection.visitaHub;
        visita.hub.client.visitaStatus = visitaStatus;
    }

    function visitaStatus(visitaIds) {
        console.log(visitaIds);
        visita.ids = visitaIds;
    }

    function validate(id) {
        //evalua si el registro esta en uso
        visita.recordInUse = (visita.ids.indexOf(id) > -1);
        if (visita.recordInUse)
            $('#inUse').removeClass('hidden');
        $('#btn-save').html("");
    }

})(window.visita = window.visita || {})