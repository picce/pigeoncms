<%@ WebHandler Language="C#" Class="FileUploadModernHandler" %>

using System;
using System.Web.SessionState;
using PigeonCms.Controls.ItemsAdmin;

public class FileUploadModernHandler : AbstractUploadHandler, IRequiresSessionState
{
    protected override string Prefix { get { return "FileUpload"; } }

    protected override bool IsImage { get { return false; } }

    protected override bool IsEncrypted { get { return true; } }
}