$(document).ready(function () {
    var fontStyle = "Arial";
    var fontSize = 28;
    $("#<%=lblMessage.ClientID%>").css("font-family", fontStyle);
    $("#<%=lblMessage.ClientID%>").css("font-size", fontSize);
    $("#<%=lblMessage.ClientID%>").text("Hello World!!");
});
