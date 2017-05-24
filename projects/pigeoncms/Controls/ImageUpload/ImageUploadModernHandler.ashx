<%@ WebHandler Language="C#" Class="ImageUploadModernHandler" %>

using System;
using System.Web.SessionState;
using PigeonCms.Controls.ItemsAdmin;

public class ImageUploadModernHandler : AbstractUploadHandler, IRequiresSessionState
{
    protected override string Prefix { get { return "ImageUpload"; } }

    protected override bool IsImage { get { return true; } }

    protected override bool IsEncrypted { get { return true; } }
}   