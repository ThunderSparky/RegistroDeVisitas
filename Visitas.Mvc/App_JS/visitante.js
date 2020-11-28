(function (visitante) {
    visitante.success = successReload;
    visitante.pages = 1;
    visitante.rowSize = 10;

    init();

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
})(window.visitante = window.visitante || {})