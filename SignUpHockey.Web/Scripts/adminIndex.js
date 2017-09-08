$(function () {
    $("#newGame").on('click', function () {
        $('#maxPeople').val('0');
        $('#date').val('');
        $('#addGame').prop('disabled', true);
        $('.modal').modal('show');
    });
    $("#addGame").on('click', function () {
        var max = $('#maxPeople').val();
        var date = $('#date').val();
        $.post("/admin/addGame", { maxPeople: max, date: date }, function (result) {
            $('#top').prepend(`<div class="alert well">New Game on ${result.gameDate} Added!</div>`)
            $('.modal').modal('hide');
        });
    });
    $('#maxPeople, #date').on('change', function () {
        var max = $('#maxPeople').val();
        $('#addGame').prop('disabled', (max == 0));
    });
});