$(function () {
    $("#gameId").on('change', function () {
        var gameId = $("#gameId").val();
        $.post("/admin/players", { gameId: gameId }, function (result) {
            $('#playerList').empty();
            result.forEach(p => $('#playerList').append(`<li style="font-size:20px">${p.firstName} ${p.lastName}</li>`))
        });
    });
});