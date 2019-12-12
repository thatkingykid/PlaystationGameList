$(document).ready(function () {
    $.ajax({
        url: "/Game/GetGamesForHomePage",
        type: "GET",
        success: function (content) {
            $('#gameList').append(content);
        }
    })

    $('.multiselect').selectize({
        delimiter: ',',
        persist: 'true',
        create: function (input) {
            return {
                value: input,
                text: input
            }
        }
    })

    $('.datepicker').datepicker({
        format: 'dd/mm/yyyy'
    })
})

$.validator.setDefaults({
    ignore: ':hidden:not([class~=selectized]),:hidden > .selectized, .selectize-control .selectize-input input'
})

function gameAddSuccess(ajaxContent) {
    $('#gameList').html("");
    $('#gameList').append(ajaxContent);
}