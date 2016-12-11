// Render ann table every time page loads
$(document).ready(function () {
    $.ajax({
        url: "/Anns/BuildAnnTable",
        success: function (result) {
            $("#tableDiv").html(result);
        }
    });
});

// When any of the radio buttons change, sort the ann table
// and render it again
$("input[name=sort]:radio").change(function () {
    var check = $("input[name=sort]:radio").val();
    $.ajax({
        url: "/Anns/SortAnnTable",
        data: { "check": check },
        success: function (result) {
            $("#tableDiv").html(result);
        }
    });
});

// Every time this input text box changes,
// filter and re-render ann table
$("#filterTitle").change(function () {
    var title = $("#filterTitle").val();
    $.ajax({
        url: "/Anns/FilterTitle",
        data: { "title": title },
        success: function (result) {
            $("#tableDiv").html(result);
        }
    });
});
$("#filterContent").change(function () {
    var content = $("#filterContent").val();
    $.ajax({
        url: "/Anns/FilterContent",
        data: { "content": content },
        success: function (result) {
            $("#tableDiv").html(result);
        }
    });
});
$("#filterPostDate").change(function () {
    var postDate = $("#filterPostDate").val();
    $.ajax({
        url: "/Anns/FilterPostDate",
        data: { "postDate": postDate },
        success: function (result) {
            $("#tableDiv").html(result);
        }
    });
});