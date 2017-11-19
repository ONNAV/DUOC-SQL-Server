$(document).ready(function () {
    console.log("ready!");
    $(".select2").select2({
        placeholder: 'Selecciona una opcion'
    });
    $('.select2').css("width", "100%");


    /*
    $('#ProveedorID').on('select2:select', function (e) {
        var data = e.params.data;
        console.log(data);
        console.log();
    });
    */

});