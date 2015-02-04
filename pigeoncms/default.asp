<%@  language="VBScript" %>
<% Option Explicit %>
<html>
<head>
    <meta http-equiv="Content-Language" content="it">
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
    <title><%= Request.ServerVariables("HTTP_HOST") %></title>   
</head>
<body>
    <p align="center">
        <b><font face="Verdana" size="6" color="#003399">
            <%= Request.ServerVariables("HTTP_HOST") %></font></b>
        <br />
        <br />
        <font face="Verdana" size="6" color="#003399">Sito web in fase di aggiornamento</font>
    </p>
</body>
</html>
