#Labels and images
You can show dynamic and localized texts in your page using the following ways.
Each method has the following parameters: 
- `string resourceSet` used to group resources of the same type (example: page name) 
- `string resourceId` the resource identifies
- `string defaultValue` the default value of the label
- `EditorTypeEnum textMode` optional parameters. The editor type shown in backend area
 
On first method call, if not present, it will be automatically created in backend **labels admin** area. All resources are automatically cached.

###Labels

**Sample 1:** 
Using `GetLabel` method.
```ASP
<%=GetLabel("Page1", "Sample1", "Using GetLabel method")%>
```

**Sample 2:** 
Using `<pgn:Label>` user control with `TextMode="Text"`. This is useful to include big data in plain text.
```ASP
<pgn:Label runat="server" ResourceSet="Page1" ResourceId="Sample2" TextMode="Text">
    Your simple plain text content
</pgn:Label>
```

**Sample 3:** 
Using `<pgn:Label>` user control with `TextMode="BasicHtml"`. This is useful to include big data with basic html content. In the backend the user could change the content with a basic WYSIWYG editor.
```ASP
<pgn:Label runat="server" ResourceSet="Page1" ResourceId="Sample3" TextMode="BasicHtml">
    Your <em>simple</em> html content.<br>
</pgn:Label>
```

**Sample 4:** 
Using `<pgn:Label>` user control with `TextMode="Html"`. This is useful to include big data with complex html content. In the backend the user could change the content with a full WYSIWYG editor.
```ASP
<pgn:Label runat="server" ResourceSet="Page1" ResourceId="Sample4" TextMode="Html">
    <p>
    Your <em>complex</em> html content.<br>
    This is a <a href="http://www.google.com">link</a>
    </p>
</pgn:Label>
```

**Sample 5:** 
Using `GetLabel` method in code behind.
```ASP
<asp:Literal runat="server" ID="LitSample5"></asp:Literal>
```
```C#
//code behind
LitSample5.Text = GetLabel("Page1", "Sample5", "Label value calling method in code behind");
```

###Images

**Sample image 1:** 
Using `<pgn:Image>` user control with a simple `<img>` tag.
```ASP
<pgn:Image runat="server" ResourceSet="Page1" ResourceId="Image1" Allowed="jpg|png|gif" MaxSize="1024" Width="640" Height="480" AutoResize="true">
   <img src="/assets/img/roadrunner.gif"  alt='' />
</pgn:Image>
```

**Sample image 2:** 
Use the resource as `div` background.
```ASP
<pgn:Image runat="server" ResourceSet="Page1" ResourceId="Image2" SrcAttr="url" >
  <div style="width:320px; height:320px; background-image:url(/assets/img/coyote.jpg);"></div>
</pgn:Image>
```

**Sample image 3:** 
Use the resource in a html `data-` attribute.
```ASP
<pgn:Image runat="server" ResourceSet="Page1" ResourceId="Image3" SrcAttr="data-image" >
  <span data-image='/assets/img/tnt.png'></span>
</pgn:Image>
```
