#Labels and images
You can show dynamic and localized texts in your page using the following ways.
Each method has the following parameters: 
- `string resourceSet` used to group resources of the same type (example: page name) 
- `string resourceId` the resource identifies
- `string defaultValue` the default value of the label
- `EditorTypeEnum textMode` optional parameters. The editor type shown in backend area

On first method call, if not present, it will be automatically created in backend **labels admin** area.

###Labels

**Sample 1:** 
Using `GetLabel` method.
```ASP
<%=GetLabel("Page1", "Sample1", "Using GetLabel method")%>
```

** Sample 2:** 
Using `<pgn:Label>` user control with `TextMode="Text"`. This is useful to include big data in plain text.
```ASP
<pgn:Label runat="server" ResourceSet="Page1" Id="Sample2" TextMode="Text">
    Your simple plain text content
</pgn:Label>
```

** Sample 3:** 
Using `<pgn:Label>` user control with `TextMode="BasicHtml"`. This is useful to include big data with basic html content. In the backend the user could change the content with a basic WYSIWYG editor.
```ASP
<pgn:Label runat="server" ResourceSet="Page1" Id="Sample3" TextMode="BasicHtml">
    Your <em>simple</em> html content.<br>
</pgn:Label>
```

** Sample 4:** 
Using `<pgn:Label>` user control with `TextMode="Html"`. This is useful to include big data with complex html content. In the backend the user could change the content with a full WYSIWYG editor.
```ASP
<pgn:Label runat="server" ResourceSet="AQ_default" Id="Sample4" TextMode="Html">
    <p>
    Your <em>complex</em> html content.<br>
    This is a <a href="http://www.google.com">link</a>
    </p>
</pgn:Label>
```

** Sample 5:** 
```ASP
<asp:Literal runat="server" ID="LitSample5"></asp:Literal>
```
```C#
//code behind
LitSample5.Text = GetLabel("AQ_default", "Sample5", "Label value calling method in code behind");
```

###Images
