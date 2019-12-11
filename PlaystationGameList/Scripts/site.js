$(document).ready(function () {
    $.ajax({
        url: "/Game/GetGamesForHomePage",
        type: "GET",
        success: function (content) {
            $('#gameList').append(content);
        }
    })
})