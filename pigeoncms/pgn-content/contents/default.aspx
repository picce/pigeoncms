<%@ Page Title="" Language="C#" MasterPageFile="~/pgn-content/masterpages/puppets.master" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="_default" %>
<%@ Register Assembly="PigeonCms.Core" Namespace="PigeonCms.Controls" TagPrefix="pgn" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphHead" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="CphMain" Runat="Server">

    <h1>
        <%=GetLabel("AQ_default", "Title", "Labels resources")%>    
    </h1>

    <p>
        Sample label 1:<br />
        <%=GetLabel("AQ_default", "Sample1", "Label value calling method in html")%>
    </p>

    <p>
        Sample label 2:<br />
        <pgn:Label runat="server" ResourceSet="AQ_default" Id="Sample2" TextMode="Text">
            Label using server control with TextMode="Text"
        </pgn:Label>
    </p>

    <p>
        Sample label 3:<br />
        <pgn:Label runat="server" ResourceSet="AQ_default" Id="Sample3" TextMode="BasicHtml">
            Label using server control with TextMode="BasicHtml" <em>(simple editor)</em>
        </pgn:Label>
    </p>

    <p>
        Sample label 4:<br />
        <pgn:Label runat="server" ResourceSet="AQ_default" Id="Sample4" TextMode="Html">
            Label using server control with TextMode="Html" <em>(advanced editor)</em><br />
            This is a <a href="#">link</a>
        </pgn:Label>
    </p>

    <p>
        Sample label 5:<br />
        <asp:Literal runat="server" ID="LitSample5"></asp:Literal>
    </p>

</asp:Content>

