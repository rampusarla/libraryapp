$(document).ready(function () {
    $("#FirstName").on('input propertychange paste', function () {
        $("span.field-validation-error").html("")
        $(this).removeClass("focused")
    });
    $("#LasttName").on('input propertychange paste', function () {
        $("span.field-validation-error").html("");
        $(this).removeClass("focused")
    });
    $("#Title").on('input propertychange paste', function () {
        $("span.field-validation-error").html("");
        $(this).removeClass("focused")
    });
    $("#Author").on('input propertychange paste', function () {
        $("span.field-validation-error").html("");
        $(this).removeClass("focused")
    });
});