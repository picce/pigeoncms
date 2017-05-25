using System;
using System.Net;
using System.IO;
using System.Web;
using System.Web.Script.Serialization;


namespace LogMeBot
{

    /// <summary>
    /// Model for /AccessToken api result
    /// </summary>
    public class AccessTokenResult
    {
        public string access_token { get; set; }

        public string token_type { get; set; }

        public int expires_in { get; set; }
    }

    /// <summary>
    /// model for /Me api result
    /// give info about current logged user such as email, user_id etc..
    /// </summary>
    public class MeResult
    {
        public string UserId { get; set; }

        public string Email { get; set; }

        public string Nickname { get; set; }

        public bool Expired { get; set; }

        public int ExpiresIn { get; set; }
    }

    public class LogMeBotException : Exception
    {

        private int statusCode;
        public int StatusCode
        {
            set { statusCode = value; }
            get { return statusCode; }
        }

        public LogMeBotException(int statusCode, string message) : base(message)
        {
            this.StatusCode = statusCode;
        }
    }

    public class LogMeBotClient
    {
        private const string SessionStateKey = "LogMeBot.LogMeBotClient.State";
        private const string BasePath = "http://www.logmebot.com/";
        //private const string BasePath = "http://localhost:3979/";

        public string LogonEndpoint
        {
            get { return BasePath + "account/logon"; }
        }

        public string TokenEndpoint
        {
            get { return BasePath + "oauth/AccessToken"; }
        }

        public string MeEndpoint
        {
            get { return BasePath + "oauth/Me"; }
        }

        private string clientId = "";
        public string ClientId
        {
            get { return clientId; }
        }

        private string clientSecret = "";
        public string ClientSecret
        {
            get { return clientSecret; }
        }

        private string callbackUri = "";
        public string CallbackUri
        {
            get { return callbackUri; }
        }

        private string scope = "email";
        public string Scope
        {
            set { scope = value; }
            get { return scope; }
        }

        public string State
        {
            get
            {
                string res = "";
                if (HttpContext.Current.Session[SessionStateKey] != null)
                {
                    res = HttpContext.Current.Session[SessionStateKey].ToString();
                }
                return res;
            }
        }

        private string accessToken = "";
        public string AccessToken
        {
            get { return accessToken; }
        }


        public LogMeBotClient(string clientId, string clientSecret, string callbackUri)
        {
            if (string.IsNullOrEmpty(clientId)
                || string.IsNullOrEmpty(clientSecret)
                || string.IsNullOrEmpty(callbackUri))
            {
                throw new ArgumentException("Invalid Logmebot app settings");
            }

            this.clientId = clientId;
            this.clientSecret = clientSecret;
            this.callbackUri = callbackUri;
        }

        /// <summary>
        /// redir to logmebot auth page
        /// </summary>
        public void LogOn(string state = "")
        {
            string url = GetLogOnUri(state);
            HttpContext.Current.Response.Redirect(url);
        }

        /// <summary>
        /// retrieve the access token
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public string GetAccessToken(string code, string state)
        {
            int statusCode = 0;

            try
            {
                if (state != this.State)
                    throw new LogMeBotException(401, "Invalid state");

                var accessTokenResult = new AccessTokenResult();
                var serializer = new JavaScriptSerializer();

                string reqUri = (TokenEndpoint
                    + "?client_id={client_id}"
                    + "&redirect_uri={redirect_uri}"
                    + "&client_secret={client_secret}"
                    + "&code={code}")
                    .Replace("{client_id}", this.ClientId)
                    .Replace("{redirect_uri}", this.CallbackUri)
                    .Replace("{client_secret}", this.ClientSecret)
                    .Replace("{code}", code);

                HttpWebRequest wrLogon = (HttpWebRequest)WebRequest.Create(reqUri);
                wrLogon.AllowAutoRedirect = false;
                wrLogon.KeepAlive = true;
                HttpWebResponse retreiveResponse = (HttpWebResponse)wrLogon.GetResponse();
                statusCode = (int)retreiveResponse.StatusCode;
                Stream objStream = retreiveResponse.GetResponseStream();
                StreamReader objReader = new StreamReader(objStream);
                string json = objReader.ReadToEnd();
                retreiveResponse.Close();

                accessTokenResult = serializer.Deserialize<AccessTokenResult>(json);
                accessToken = accessTokenResult.access_token;
                //SaveToken();
            }
            catch (WebException wex)
            {
                HttpWebResponse wrs = (HttpWebResponse)wex.Response;
                throw new LogMeBotException((int)wrs.StatusCode, wex.Message);
            }
            catch (Exception ex)
            {
                throw new LogMeBotException(statusCode, ex.Message);
            }
            return accessToken;
        }

        /// <summary>
        /// retrieve the access token
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public MeResult GetMe(string accessToken)
        {
            int statusCode = 0;
            var res = new MeResult();
            var serializer = new JavaScriptSerializer();

            try
            {

                string reqUri = (MeEndpoint
                    + "?access_token={access_token}")
                    .Replace("{access_token}", accessToken);

                HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(reqUri);
                wr.AllowAutoRedirect = false;
                wr.KeepAlive = true;
                HttpWebResponse retreiveResponse = (HttpWebResponse)wr.GetResponse();
                statusCode = (int)retreiveResponse.StatusCode;
                Stream objStream = retreiveResponse.GetResponseStream();
                StreamReader objReader = new StreamReader(objStream);
                string json = objReader.ReadToEnd();
                retreiveResponse.Close();

                res = serializer.Deserialize<MeResult>(json);
            }
            catch (WebException wex)
            {
                HttpWebResponse wrs = (HttpWebResponse)wex.Response;
                throw new LogMeBotException((int)wrs.StatusCode, wex.Message);
            }
            catch (Exception ex)
            {
                throw new LogMeBotException(statusCode, ex.Message);
            }
            return res;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="state">anti forgery state. new Guid if empty</param>
        /// <returns></returns>
        public string GetLogOnUri(string state = "")
        {
            if (string.IsNullOrEmpty(state))
                state = Guid.NewGuid().ToString();

            HttpContext.Current.Session.Remove(SessionStateKey);
            HttpContext.Current.Session.Add(SessionStateKey, state);

            string url = (LogonEndpoint
                + "?response_type=code"
                + "&client_id={clientId}"
                + "&scope={scope}"
                + "&state={state}"
                + "&redirect_uri={callbackUri}")
                .Replace("{clientId}", clientId)
                .Replace("{scope}", scope)
                .Replace("{state}", state)
                .Replace("{callbackUri}", callbackUri);
            return url;
        }

    }
}