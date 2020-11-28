(function (visita) {
    visita.success = successReload;
    visita.pages = 1;
    visita.rowSize = 10;

    init();

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
    }

    //Funcion getOficinas, que es utilizada en la funcion Init
    function getVisita(num) {
        var url = '/Visita/List/' + num + '/' + visita.rowSize;
        $.get(url, function (data) {
            $('.content').html(data);
        })
    }
})(window.visita = window.visita || {})