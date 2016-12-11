$(document).ready(function () {
    $.ajax({
        url: "/Anns/BuildCommentTable",
        success: function (result) {
            $("#commentDiv").html(result);
        }
    });
});