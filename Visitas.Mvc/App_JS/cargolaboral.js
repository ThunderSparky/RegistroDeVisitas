(function (cargolaboral) {
    cargolaboral.success = successReload;
    cargolaboral.pages = 1;
    cargolaboral.rowSize = 10;
    //Lo que viene ahora es del SignalR
    cargolaboral.hub = {};
    cargolaboral.ids = [];
    cargolaboral.recordInUse = false; //Este es como un flag que me indicará cuando esta en uso un modal

    cargolaboral.addCargolaboral = addCargolaboralId;
    cargolaboral.removeCargolaboral = removeCargolaboralId;
    cargolaboral.validate = validate;

    $(function () {
        connectToHub();
        init();
    });

    return cargolaboral;

    //Funcion successReload, la cual es llamada al inicio
    function successReload(option) {
        visitas.closeModal(option); //Aquí cerramos el modal, llamamos al JS que se llama visitas y ejecutamos el metodo closeModal
        //alert("Todo bien");
        alertUpdate(); //metodo hub
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

    //Lo de abajo es para el SignalR
    function addCargolaboralId(id) {
        cargolaboral.hub.server.addCargolaboralId(id);
    }

    function removeCargolaboralId(id) {
        cargolaboral.hub.server.removeCargolaboralId(id);
    }

    function connectToHub() {
        cargolaboral.hub = $.connection.cargolaboralHub;
        cargolaboral.hub.client.cargolaboralStatus = cargolaboralStatus;
        cargolaboral.hub.client.updateGrid = updateGrid;
    }

    function cargolaboralStatus(cargolaboralIds) {
        console.log(cargolaboralIds);
        cargolaboral.ids = cargolaboralIds;
    }

    function validate(id) {
        //evalua si el registro esta en uso
        cargolaboral.recordInUse = (cargolaboral.ids.indexOf(id) > -1);
        if (cargolaboral.recordInUse)
            $('#inUse').removeClass('hidden');
        $('#btn-save').html("");
    }
    //2 funciones para la actualización de la grilla cuando ocurra una creación
    function alertUpdate() {
        cargolaboral.hub.server.alertUpdate();
    }
    function updateGrid() {
        //Lógica para listar

        elements = document.getElementsByClassName('active'); // elementos <i>
        activePage = elements[0].children;
        getCargolaboral(activePage[0].text);
    }

})(window.cargolaboral = window.cargolaboral || {})