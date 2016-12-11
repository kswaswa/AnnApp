$(document).ready(function () {
    $.ajax({
        url: "/Anns/BuildViewedAnnTable",
        success: function (result) {
            $("#viewedAnnouncement").html(result);
        }
    });
});