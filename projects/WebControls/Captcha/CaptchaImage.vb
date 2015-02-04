Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging

''' <summary>
''' CAPTCHA image generation class
''' </summary>
''' <remarks>
''' Adapted from the excellent code at 
''' http://www.codeproject.com/aspnet/CaptchaImage.asp
'''
''' Jeff Atwood
''' http://www.codinghorror.com/
''' </remarks>
Public Class CaptchaImage

    Private _height As Integer
    Private _width As Integer
    Private _rand As Random
    Private _generatedAt As DateTime
    Private _randomText As String
    Private _randomTextLength As Integer
    Private _randomTextChars As String
    Private _fontFamilyName As String
    Private _fontWarp As FontWarpFactor
    Private _backgroundNoise As BackgroundNoiseLevel
    Private _lineNoise As LineNoiseLevel
    Private _guid As String
    Private _fontWhitelist As String

#Region "  Public Enums"

    ''' <summary>
    ''' Amount of random font warping to apply to rendered text
    ''' </summary>
    Public Enum FontWarpFactor
        None
        Low
        Medium
        High
        Extreme
    End Enum

    ''' <summary>
    ''' Amount of background noise to add to rendered image
    ''' </summary>
    Public Enum BackgroundNoiseLevel
        None
        Low
        Medium
        High
        Extreme
    End Enum

    ''' <summary>
    ''' Amount of curved line noise to add to rendered image
    ''' </summary>
    Public Enum LineNoiseLevel
        None
        Low
        Medium
        High
        Extreme
    End Enum

#End Region

#Region "  Public Properties"

    ''' <summary>
    ''' Returns a GUID that uniquely identifies this Captcha
    ''' </summary>
    Public ReadOnly Property UniqueId() As String
        Get
            Return _guid
        End Get
    End Property

    ''' <summary>
    ''' Returns the date and time this image was last rendered
    ''' </summary>
    Public ReadOnly Property RenderedAt() As DateTime
        Get
            Return _generatedAt
        End Get
    End Property

    ''' <summary>
    ''' Font family to use when drawing the Captcha text. If no font is provided, a random font will be chosen from the font whitelist for each character.
    ''' </summary>
    Public Property Font() As String
        Get
            Return _fontFamilyName
        End Get
        Set(ByVal Value As String)
            Try
                Dim font1 As Font = New Font(Value, 12.0!)
                _fontFamilyName = Value
                font1.Dispose()
            Catch ex As Exception
                _fontFamilyName = Drawing.FontFamily.GenericSerif.Name
            End Try
        End Set
    End Property

    ''' <summary>
    ''' Amount of random warping to apply to the Captcha text.
    ''' </summary>
    Public Property FontWarp() As FontWarpFactor
        Get
            Return _fontWarp
        End Get
        Set(ByVal Value As FontWarpFactor)
            _fontWarp = Value
        End Set
    End Property

    ''' <summary>
    ''' Amount of background noise to apply to the Captcha image.
    ''' </summary>
    Public Property BackgroundNoise() As BackgroundNoiseLevel
        Get
            Return _backgroundNoise
        End Get
        Set(ByVal Value As BackgroundNoiseLevel)
            _backgroundNoise = Value
        End Set
    End Property

    Public Property LineNoise() As LineNoiseLevel
        Get
            Return _lineNoise
        End Get
        Set(ByVal value As LineNoiseLevel)
            _lineNoise = value
        End Set
    End Property

    ''' <summary>
    ''' A string of valid characters to use in the Captcha text. 
    ''' A random character will be selected from this string for each character.
    ''' </summary>
    Public Property TextChars() As String
        Get
            Return _randomTextChars
        End Get
        Set(ByVal Value As String)
            _randomTextChars = Value
            _randomText = GenerateRandomText()
        End Set
    End Property

    ''' <summary>
    ''' Number of characters to use in the Captcha text. 
    ''' </summary>
    Public Property TextLength() As Integer
        Get
            Return _randomTextLength
        End Get
        Set(ByVal Value As Integer)
            _randomTextLength = Value
            _randomText = GenerateRandomText()
        End Set
    End Property

    ''' <summary>
    ''' Returns the randomly generated Captcha text.
    ''' </summary>
    Public ReadOnly Property [Text]() As String
        Get
            Return _randomText
        End Get
    End Property

    ''' <summary>
    ''' Width of Captcha image to generate, in pixels 
    ''' </summary>
    Public Property Width() As Integer
        Get
            Return _width
        End Get
        Set(ByVal Value As Integer)
            If (Value <= 60) Then
                Throw New ArgumentOutOfRangeException("width", Value, "width must be greater than 60.")
            End If
            _width = Value
        End Set
    End Property

    ''' <summary>
    ''' Height of Captcha image to generate, in pixels 
    ''' </summary>
    Public Property Height() As Integer
        Get
            Return _height
        End Get
        Set(ByVal Value As Integer)
            If Value <= 30 Then
                Throw New ArgumentOutOfRangeException("height", Value, "height must be greater than 30.")
            End If
            _height = Value
        End Set
    End Property

    ''' <summary>
    ''' A semicolon-delimited list of valid fonts to use when no font is provided.
    ''' </summary>
    Public Property FontWhitelist() As String
        Get
            Return _fontWhitelist
        End Get
        Set(ByVal value As String)
            _fontWhitelist = value
        End Set
    End Property

#End Region

    Public Sub New()
        _rand = New Random
        _fontWarp = FontWarpFactor.Low
        _backgroundNoise = BackgroundNoiseLevel.Low
        _lineNoise = LineNoiseLevel.None
        _width = 180
        _height = 50
        _randomTextLength = 5
        _randomTextChars = "ACDEFGHJKLNPQRTUVXYZ2346789"
        _fontFamilyName = ""
        ' -- a list of known good fonts in on both Windows XP and Windows Server 2003
        _fontWhitelist = _
            "arial;arial black;comic sans ms;courier new;estrangelo edessa;franklin gothic medium;" & _
            "georgia;lucida console;lucida sans unicode;mangal;microsoft sans serif;palatino linotype;" & _
            "sylfaen;tahoma;times new roman;trebuchet ms;verdana"
        _randomText = GenerateRandomText()
        _generatedAt = DateTime.Now
        _guid = Guid.NewGuid.ToString()
    End Sub

    ''' <summary>
    ''' Forces a new Captcha image to be generated using current property value settings.
    ''' </summary>
    Public Function RenderImage() As Bitmap
        Return GenerateImagePrivate()
    End Function

    ''' <summary>
    ''' Returns a random font family from the font whitelist
    ''' </summary>
    Private Function RandomFontFamily() As String
        Static ff() As String
        '-- small optimization so we don't have to split for each char
        If ff Is Nothing Then
            ff = _fontWhitelist.Split(";"c)
        End If
        Return ff(_rand.Next(0, ff.Length))
    End Function

    ''' <summary>
    ''' generate random text for the CAPTCHA
    ''' </summary>
    Private Function GenerateRandomText() As String
        Dim sb As New System.Text.StringBuilder(_randomTextLength)
        Dim maxLength As Integer = _randomTextChars.Length
        For n As Integer = 0 To _randomTextLength - 1
            sb.Append(_randomTextChars.Substring(_rand.Next(maxLength), 1))
        Next
        Return sb.ToString
    End Function

    ''' <summary>
    ''' Returns a random point within the specified x and y ranges
    ''' </summary>
    Private Function RandomPoint(ByVal xmin As Integer, ByVal xmax As Integer, ByRef ymin As Integer, ByRef ymax As Integer) As PointF
        Return New PointF(_rand.Next(xmin, xmax), _rand.Next(ymin, ymax))
    End Function

    ''' <summary>
    ''' Returns a random point within the specified rectangle
    ''' </summary>
    Private Function RandomPoint(ByVal rect As Rectangle) As PointF
        Return RandomPoint(rect.Left, rect.Width, rect.Top, rect.Bottom)
    End Function

    ''' <summary>
    ''' Returns a GraphicsPath containing the specified string and font
    ''' </summary>
    Private Function TextPath(ByVal s As String, ByVal f As Font, ByVal r As Rectangle) As GraphicsPath
        Dim sf As StringFormat = New StringFormat
        sf.Alignment = StringAlignment.Near
        sf.LineAlignment = StringAlignment.Near
        Dim gp As GraphicsPath = New GraphicsPath
        gp.AddString(s, f.FontFamily, CType(f.Style, Integer), f.Size, r, sf)
        Return gp
    End Function

    ''' <summary>
    ''' Returns the CAPTCHA font in an appropriate size 
    ''' </summary>
    Private Function GetFont() As Font
        Dim fsize As Single
        Dim fname As String = _fontFamilyName
        If fname = "" Then
            fname = RandomFontFamily()
        End If
        Select Case Me.FontWarp
            Case FontWarpFactor.None
                fsize = Convert.ToInt32(_height * 0.7)
            Case FontWarpFactor.Low
                fsize = Convert.ToInt32(_height * 0.8)
            Case FontWarpFactor.Medium
                fsize = Convert.ToInt32(_height * 0.85)
            Case FontWarpFactor.High
                fsize = Convert.ToInt32(_height * 0.9)
            Case FontWarpFactor.Extreme
                fsize = Convert.ToInt32(_height * 0.95)
        End Select
        Return New Font(fname, fsize, FontStyle.Bold)
    End Function

    ''' <summary>
    ''' Renders the CAPTCHA image
    ''' </summary>
    Private Function GenerateImagePrivate() As Bitmap
        Dim fnt As Font = Nothing
        Dim rect As Rectangle
        Dim br As Brush
        Dim bmp As Bitmap = New Bitmap(_width, _height, PixelFormat.Format32bppArgb)
        Dim gr As Graphics = Graphics.FromImage(bmp)
        gr.SmoothingMode = SmoothingMode.AntiAlias

        '-- fill an empty white rectangle
        rect = New Rectangle(0, 0, _width, _height)
        br = New SolidBrush(Color.White)
        gr.FillRectangle(br, rect)

        Dim charOffset As Integer = 0
        Dim charWidth As Double = _width / _randomTextLength
        Dim rectChar As Rectangle

        For Each c As Char In _randomText
            '-- establish font and draw area
            fnt = GetFont()
            rectChar = New Rectangle(Convert.ToInt32(charOffset * charWidth), 0, Convert.ToInt32(charWidth), _height)

            '-- warp the character
            Dim gp As GraphicsPath = TextPath(c, fnt, rectChar)
            WarpText(gp, rectChar)

            '-- draw the character
            br = New SolidBrush(Color.Black)
            gr.FillPath(br, gp)

            charOffset += 1
        Next

        AddNoise(gr, rect)
        AddLine(gr, rect)

        '-- clean up unmanaged resources
        fnt.Dispose()
        br.Dispose()
        gr.Dispose()

        Return bmp
    End Function

    ''' <summary>
    ''' Warp the provided text GraphicsPath by a variable amount
    ''' </summary>
    Private Sub WarpText(ByVal textPath As GraphicsPath, ByVal rect As Rectangle)
        Dim WarpDivisor As Single
        Dim RangeModifier As Single

        Select Case _fontWarp
            Case FontWarpFactor.None
                Return
            Case FontWarpFactor.Low
                WarpDivisor = 6
                RangeModifier = 1
            Case FontWarpFactor.Medium
                WarpDivisor = 5
                RangeModifier = 1.3
            Case FontWarpFactor.High
                WarpDivisor = 4.5
                RangeModifier = 1.4
            Case FontWarpFactor.Extreme
                WarpDivisor = 4
                RangeModifier = 1.5
        End Select

        Dim rectF As RectangleF
        rectF = New RectangleF(Convert.ToSingle(rect.Left), 0, Convert.ToSingle(rect.Width), rect.Height)

        Dim hrange As Integer = Convert.ToInt32(rect.Height / WarpDivisor)
        Dim wrange As Integer = Convert.ToInt32(rect.Width / WarpDivisor)
        Dim left As Integer = rect.Left - Convert.ToInt32(wrange * RangeModifier)
        Dim top As Integer = rect.Top - Convert.ToInt32(hrange * RangeModifier)
        Dim width As Integer = rect.Left + rect.Width + Convert.ToInt32(wrange * RangeModifier)
        Dim height As Integer = rect.Top + rect.Height + Convert.ToInt32(hrange * RangeModifier)

        If left < 0 Then left = 0
        If top < 0 Then top = 0
        If width > Me.Width Then width = Me.Width
        If height > Me.Height Then height = Me.Height

        Dim leftTop As PointF = RandomPoint(left, left + wrange, top, top + hrange)
        Dim rightTop As PointF = RandomPoint(width - wrange, width, top, top + hrange)
        Dim leftBottom As PointF = RandomPoint(left, left + wrange, height - hrange, height)
        Dim rightBottom As PointF = RandomPoint(width - wrange, width, height - hrange, height)

        Dim points As PointF() = New PointF() {leftTop, rightTop, leftBottom, rightBottom}
        Dim m As New Matrix
        m.Translate(0, 0)
        textPath.Warp(points, rectF, m, WarpMode.Perspective, 0)
    End Sub


    ''' <summary>
    ''' Add a variable level of graphic noise to the image
    ''' </summary>
    Private Sub AddNoise(ByVal graphics1 As Graphics, ByVal rect As Rectangle)
        Dim density As Integer
        Dim size As Integer

        Select Case _backgroundNoise
            Case BackgroundNoiseLevel.None
                Return
            Case BackgroundNoiseLevel.Low
                density = 30
                size = 40
            Case BackgroundNoiseLevel.Medium
                density = 18
                size = 40
            Case BackgroundNoiseLevel.High
                density = 16
                size = 39
            Case BackgroundNoiseLevel.Extreme
                density = 12
                size = 38
        End Select

        Dim br As New SolidBrush(Color.Black)
        Dim max As Integer = Convert.ToInt32(Math.Max(rect.Width, rect.Height) / size)

        For i As Integer = 0 To Convert.ToInt32((rect.Width * rect.Height) / density)
            graphics1.FillEllipse(br, _rand.Next(rect.Width), _rand.Next(rect.Height), _
                _rand.Next(max), _rand.Next(max))
        Next
        br.Dispose()
    End Sub

    ''' <summary>
    ''' Add variable level of curved lines to the image
    ''' </summary>
    Private Sub AddLine(ByVal graphics1 As Graphics, ByVal rect As Rectangle)

        Dim length As Integer
        Dim width As Single
        Dim linecount As Integer

        Select Case _lineNoise
            Case LineNoiseLevel.None
                Return
            Case LineNoiseLevel.Low
                length = 4
                width = Convert.ToSingle(_height / 31.25) ' 1.6
                linecount = 1
            Case LineNoiseLevel.Medium
                length = 5
                width = Convert.ToSingle(_height / 27.7777) ' 1.8
                linecount = 1
            Case LineNoiseLevel.High
                length = 3
                width = Convert.ToSingle(_height / 25) ' 2.0
                linecount = 2
            Case LineNoiseLevel.Extreme
                length = 3
                width = Convert.ToSingle(_height / 22.7272) ' 2.2
                linecount = 3
        End Select

        Dim pf(length) As PointF
        Dim p As New Pen(Color.Black, width)

        For l As Integer = 1 To linecount
            For i As Integer = 0 To length
                pf(i) = RandomPoint(rect)
            Next
            graphics1.DrawCurve(p, pf, 1.75)
        Next

        p.Dispose()
    End Sub

End Class