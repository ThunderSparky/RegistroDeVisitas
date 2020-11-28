(function (oficina) {
    oficina.success = successReload;
    oficina.pages = 1;
    oficina.rowSize = 10;

    init();

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
})(window.oficina = window.oficina || {})