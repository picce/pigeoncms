#Labels and images

###Labels
Sample label1
```ASP
<%=GetLabel("AQ_default", "Title", "Labels resources")%>
```

Sample label2
```ASP
<pgn:Label runat="server" ResourceSet="AQ_default" Id="Sample2" TextMode="Text">
    Label using server control with TextMode="Text"
</pgn:Label>
```

Sample label3
```ASP
<pgn:Label runat="server" ResourceSet="AQ_default" Id="Sample3" TextMode="BasicHtml">
    Label using server control with TextMode="BasicHtml" <em>(simple editor)</em>
</pgn:Label>
```

Sample label4
```ASP
<pgn:Label runat="server" ResourceSet="AQ_default" Id="Sample4" TextMode="Html">
    Label using server control with TextMode="Html" <em>(advanced editor)</em><br />
    This is a <a href="#">link</a>
</pgn:Label>
```

Sample label5
```ASP
<asp:Literal runat="server" ID="LitSample5"></asp:Literal>
```

###Images
