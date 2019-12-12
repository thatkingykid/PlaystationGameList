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
})

