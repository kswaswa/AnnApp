// render viewed list every time page loads
$(document).ready(function () {
    $.ajax({
        url: "/Anns/BuildViewedAnnTable",
        success: function (result) {
            $("#viewedAnnouncement").html(result);
        }
    });
});