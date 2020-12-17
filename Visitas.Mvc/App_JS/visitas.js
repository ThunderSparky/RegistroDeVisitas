(function (visitas) {
    visitas.getModal = getModalContent; //getModalContent, es una funcion
    visitas.closeModal = closeModal; //closeModal, es una funcion

    return visitas;

    //Función getModalContent
    function getModalContent(url) {
        //function (data), la variable data va recibir toda la informacion que devuelve el evento get, en este caso recibe la estructura de la vista del _create
        $.get(url, function (data) {
            //Lo que nos devuelva como data, lo vamos a inscrustar en el modal-body
            $('.modal-body').html(data);
        })
    }

    //Funcion closeModal
    function closeModal(option) {
        $("button[data-dismiss='modal']").click(); //Aquí generamos el cierre del modal
        $('.modal-body').html(""); //Aquí limpiamos el modal-body
        modifyAlertsClasses(option); //Esto es para modificar las alertas y muestra la que se debe mostrar
    }

    //Funcion modifyAlertsClasses
    function modifyAlertsClasses(option) {
        //Indicamos (los id que indicamos en el Helper AlertMessages, que esta en la carpeta App_Code dentro del archivo Messages)
        $('#createMessage').addClass('hidden');//Estamos agregando la clase HIDDEN
        $('#deleteMessage').addClass('hidden');//Estamos agregando la clase HIDDEN
        $('#editMessage').addClass('hidden');//Estamos agregando la clase HIDDEN

        //Evaluamos el parametro que le estamos pasando al modifyAlertsClasses
        //El parametro opcion, se establece las vistas de _Create, _Edit, ..., en el OnSuccess = "oficina.success('create')", este por ejm en el caso de _Create
        if (option === 'create') {
            $('#createMessage').removeClass('hidden');
            Swal.fire({
                position: 'center',
                icon: 'success',
                title: 'Creación éxitosa!',
                showConfirmButton: false,
                timer: 1500
            });
        }
        else if (option === 'edit') {
            $('#editMessage').removeClass('hidden');
            Swal.fire({
                position: 'center',
                icon: 'success',
                title: 'Edición éxitosa!',
                showConfirmButton: false,
                timer: 1500
            });
        }
        else if (option === 'delete') {
            $('#deleteMessage').removeClass('hidden');
            Swal.fire({
                position: 'center',
                icon: 'success',
                title: 'Eliminación éxitosa!',
                showConfirmButton: false,
                timer: 1500
            });
        }
    }
})(window.visitas = window.visitas || {});