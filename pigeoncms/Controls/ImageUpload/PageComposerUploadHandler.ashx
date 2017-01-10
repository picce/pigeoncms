<%@ WebHandler Language="C#" Class="PageComposerUploadHandler" %>

using System;
using System.Web;
using System.IO;
using Newtonsoft.Json;
using PigeonCms;
using PigeonCms.Core.Helpers;
using PigeonCms.Controls.ItemsAdmin;
using System.Web.SessionState;

public class PageComposerUploadHandler : AbstractUploadHandler, IRequiresSessionState
{
    protected override string Prefix { get { return "PageComposer"; } }

    protected override bool IsImage { get { return true; } }

    protected override bool IsEncrypted { get { return false; } }

    public override void ProcessRequest(HttpContext context)
    {
        UploadParameters parameters = null;
        if (IsEncrypted)
        {
            parameters = JsonConvert.DeserializeObject<UploadParameters>(Utility.Encryption.Decrypt(UrlUtils.Base64Decode(context.Request.Params["parameters"]), Config.EncryptKey));
        }
        else
        {
            parameters = JsonConvert.DeserializeObject<UploadParameters>(UrlUtils.Base64Decode(context.Request.Params["parameters"]));
        }

        if (context.Request["action"] == "delete")
        {
            Delete(context, parameters);
            return;
        }

        base.ProcessRequest(context);
    }

    public void Delete(HttpContext context, UploadParameters parameters)
    {
        context.Session[Prefix + "_" + parameters.UniqueID + "_Deleted"] = "__deleted__";
    }
}