<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RefreshApp.aspx.cs" Inherits="RefreshApp" 
ValidateRequest="false" EnableTheming="false" EnableViewState="false" Theme="" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PigeonCms</title>
    <style type="text/css">
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <span style="color:Red;"><%=LitErr %></span><br />
        <span style="color:black;"><%=LitSuccess %></span>
    </form>
</body>
</html>
