<%@Page Language="C#"%>

<%
    var filePath = MapPath("~/Errors/500.html");
    Response.StatusCode = 500;
    Response.ContentType = "text/html; charset=utf-8";
    Response.WriteFile(filePath);
%>