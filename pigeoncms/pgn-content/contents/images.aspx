<%@ Page Title="" Language="C#" MasterPageFile="~/pgn-content/masterpages/puppets.master" AutoEventWireup="true" CodeFile="images.aspx.cs" Inherits="_images" %>
<%@ Register Assembly="PigeonCms.Core" Namespace="PigeonCms.Controls" TagPrefix="pgn" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphHead" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="CphMain" Runat="Server">

    <h1>
        <%=GetLabel("AQ_images", "Title", "Images resources")%>    
    </h1>

    <p>
        Sample image 1:<br />
        <pgn:Image ID="Image1" runat="server" ResourceSet="AQ_images" ResourceId="ImageRoadRunner" Allowed="jpg|png|gif" MaxSize="1024" Width="640" Height="480" AutoResize="true">
            <img src="/assets/img/roadrunner.gif"  alt='' />
        </pgn:Image>
    </p>

    <p>
        Sample image 2:<br />
        <pgn:Image ID="Image2" runat="server" ResourceSet="AQ_images" ResourceId="ImageCoyote" SrcAttr="url" >
            <div style="width:320px; height:320px; background-image:url(/assets/img/coyote.jpg);"></div>
        </pgn:Image>
    </p>

    <p>
        Sample image 3:<br />
        <pgn:Image ID="Image3" runat="server" ResourceSet="AQ_images" ResourceId="ImageTnt" SrcAttr="data-image" >
            <span data-image='/assets/img/tnt.png'></span>
        </pgn:Image>
    </p>
</asp:Content>

