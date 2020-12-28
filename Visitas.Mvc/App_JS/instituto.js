(function (instituto) {
    instituto.success = successReload;
    instituto.pages = 1;
    instituto.rowSize = 10;

    instituto.searchByFilter = searchByFilter;

    instituto.limpiarFilter = limpiarFilter;

    //Lo que viene ahora es del SignalR
    instituto.hub = {};
    instituto.ids = [];
    instituto.recordInUse = false; //Este es como un flag que me indicará cuando esta en uso un modal

    instituto.addInstituto = addInstitutoId;
    instituto.removeInstituto = removeInstitutoId;
    instituto.validate = validate;

    $(function () {
        connectToHub();
        init();
    });

    return instituto;

    //Funcion successReload, la cual es llamada al inicio
    function successReload(option) {
        visitas.closeModal(option); //Aquí cerramos el modal, llamamos al JS que se llama visitas y ejecutamos el metodo closeModal
        //alert("Todo bien");
        alertUpdate(); //metodo hub
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

    function searchByFilter() {
        var institutoName = document.getElementById("institutoName");
        console.log(institutoName.value);
        var institutoNum = document.getElementById("institutoNum");
        console.log(institutoNum.value);

        if (institutoName.value == '') institutoName.value = '-';
        if (institutoNum.value == '') institutoNum.value = '-';

        var url = '/Instituto/ListByFilters/' + institutoName.value + '/' + institutoNum.value;
        $.get(url, function (data) {
            $('.content').html(data);
        }) 
        $('#paginacion1').addClass('hidden');
        $('#paginacion2').addClass('hidden');
    }

    function limpiarFilter() {
        $('#paginacion1').removeClass('hidden');
        $('#paginacion2').removeClass('hidden');
        institutoName.value = '';
        institutoNum.value = '';
        init();
    }

    //Lo de abajo es para el SignalR
    function addInstitutoId(id) {
        instituto.hub.server.addInstitutoId(id);
    }

    function removeInstitutoId(id) {
        instituto.hub.server.removeInstitutoId(id);
    }

    function connectToHub() {
        instituto.hub = $.connection.institutoHub;
        instituto.hub.client.institutoStatus = institutoStatus;
        instituto.hub.client.updateGrid = updateGrid;
    }

    function institutoStatus(institutoIds) {
        console.log(institutoIds);
        instituto.ids = institutoIds;
    }

    function validate(id) {
        //evalua si el registro esta en uso
        instituto.recordInUse = (instituto.ids.indexOf(id) > -1);
        if (instituto.recordInUse)
            $('#inUse').removeClass('hidden');
        $('#btn-save').html("");
    }
    //2 funciones para la actualización de la grilla cuando ocurra una creación
    function alertUpdate() {
        instituto.hub.server.alertUpdate();
    }
    function updateGrid() {
        //Lógica para listar

        elements = document.getElementsByClassName('active'); // elementos <i>
        activePage = elements[0].children;
        getInstituto(activePage[0].text);
    }
})(window.instituto = window.instituto || {})