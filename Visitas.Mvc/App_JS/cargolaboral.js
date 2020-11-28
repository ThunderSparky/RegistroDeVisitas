(function (cargolaboral) {
    cargolaboral.success = successReload;
    cargolaboral.pages = 1;
    cargolaboral.rowSize = 10;

    init();

    return cargolaboral;

    //Funcion successReload, la cual es llamada al inicio
    function successReload(option) {
        visitas.closeModal(option); //Aquí cerramos el modal, llamamos al JS que se llama visitas y ejecutamos el metodo closeModal
        //alert("Todo bien");
    }

    //Funcion Init
    function init() {
        $.get('/CargoLaboral/CountPages/' + cargolaboral.rowSize,
            //Esta es la logica cuando recuperemos la informacion
            function (data) {
                cargolaboral.pages = data;
                $('.pagination').bootpag({
                    total: cargolaboral.pages,
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
                    getCargolaboral(num); //cuando exista un cambio de pagina vamos a volver a traer informacion de la base de datos
                });
                getCargolaboral(1); //por defecto vamos a llamar a la pagina 1
            });
    }

    //Funcion getOficinas, que es utilizada en la funcion Init
    function getCargolaboral(num) {
        var url = '/CargoLaboral/List/' + num + '/' + cargolaboral.rowSize;
        $.get(url, function (data) {
            $('.content').html(data);
        })
    }
})(window.cargolaboral = window.cargolaboral || {})