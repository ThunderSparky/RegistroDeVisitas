(function (instituto) {
    instituto.success = successReload;
    instituto.pages = 1;
    instituto.rowSize = 10;

    init();

    return instituto;

    //Funcion successReload, la cual es llamada al inicio
    function successReload(option) {
        visitas.closeModal(option); //Aquí cerramos el modal, llamamos al JS que se llama visitas y ejecutamos el metodo closeModal
        //alert("Todo bien");
    }

    //Funcion Init
    function init() {
        $.get('/Instituto/CountPages/' + instituto.rowSize,
            //Esta es la logica cuando recuperemos la informacion
            function (data) {
                instituto.pages = data;
                $('.pagination').bootpag({
                    total: instituto.pages,
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
                    getInstituto(num); //cuando exista un cambio de pagina vamos a volver a traer informacion de la base de datos
                });
                getInstituto(1); //por defecto vamos a llamar a la pagina 1
            });
    }

    //Funcion getOficinas, que es utilizada en la funcion Init
    function getInstituto(num) {
        var url = '/Instituto/List/' + num + '/' + instituto.rowSize;
        $.get(url, function (data) {
            $('.content').html(data);
        })
    }
})(window.instituto = window.instituto || {})