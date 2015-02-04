Imports System
Imports System.Web
Imports System.Drawing

''' <summary>
''' Captcha image stream HttpModule. Retrieves CAPTCHA objects from cache, renders them to memory, 
''' and streams them to the browser.
''' </summary>
''' <remarks>
''' You *MUST* enable this HttpHandler in your web.config, like so:
'''
'''	  &lt;httpHandlers&gt;
'''		  &lt;add verb="GET" path="CaptchaImage.aspx" type="WebControlCaptcha.CaptchaImageHandler, WebControlCaptcha" /&gt;
'''	  &lt;/httpHandlers&gt;
'''
''' Jeff Atwood
''' http://www.codinghorror.com/
'''</remarks>
Public Class CaptchaImageHandler
    Implements IHttpHandler

    Public Sub ProcessRequest(ByVal context As System.Web.HttpContext) Implements System.Web.IHttpHandler.ProcessRequest
        Dim app As HttpApplication = context.ApplicationInstance

        '-- get the unique GUID of the captcha; this must be passed in via the querystring
        Dim guid As String = app.Request.QueryString("guid")
        Dim ci As CaptchaImage = Nothing

        If guid <> "" Then
            If String.IsNullOrEmpty(app.Request.QueryString("s")) Then
                ci = CType(HttpRuntime.Cache.Get(guid), CaptchaImage)
            Else
                ci = CType(HttpContext.Current.Session.Item(guid), CaptchaImage)
            End If

        End If

        If ci Is Nothing Then
            app.Response.StatusCode = 404
            context.ApplicationInstance.CompleteRequest()
            Return
        End If

        '-- write the image to the HTTP output stream as an array of bytes
        Dim b As Bitmap = ci.RenderImage
        b.Save(app.Context.Response.OutputStream, Drawing.Imaging.ImageFormat.Jpeg)
        b.Dispose()
        app.Response.ContentType = "image/jpeg"
        app.Response.StatusCode = 200
        context.ApplicationInstance.CompleteRequest()
    End Sub

    Public ReadOnly Property IsReusable() As Boolean Implements System.Web.IHttpHandler.IsReusable
        Get
            Return True
        End Get
    End Property

End Class