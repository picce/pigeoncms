using System.Runtime.Serialization;


namespace PigeonCms.Controls.ItemsAdmin
{
    [DataContract]
    public class UploadHandlerResult
    {
        [DataMember(Name = "status")]
        public bool Status = true;

        [DataMember(Name = "message")]
        public string Message = "";

        [DataMember(Name = "preview")]
        public string Preview = "";

        [DataMember(Name = "fileName")]
        public string FileName = "";
    }
}
