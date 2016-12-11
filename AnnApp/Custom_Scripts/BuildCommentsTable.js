// render this comment table every time the page loads
$(document).ready(function () {
    $.ajax({
        url: "/Anns/BuildCommentTable",
        success: function (result) {
            $("#commentDiv").html(result);
        }
    });
});