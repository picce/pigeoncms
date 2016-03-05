using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;
using System.Data.Common;
using System.Diagnostics;
using System.Collections.Specialized;
using System.Web.UI;
using System.Net;
using PigeonCms;
using PigeonCms.Core.Helpers;
using System.Text;
using System.Globalization;


namespace PigeonCms.Shop.PaymentsProvider
{

    /// <summary>
    /// Italian BancaSella GestPay payment provider
    /// last revision 20151130 @picce
    /// https://www.gestpay.it/
    /// https://www.gestpay.it/gestpay/doc/specifiche-tecniche/starter/gestpay_technical_specifications_paymentpage.pdf
    /// </summary>
    public class GestPay : BasePayment
    {

        public override void Submit(IOrder order, NameValueCollection data = null)
        {
            //string item = "Order " + order.OrderRef + " / " + order.OrderDate.ToShortDateString();
            string myshoplogin = base.PaymentData.PayAccount;
            string mycurrency = "242"; //EUR see page 64
            string myamount = order.TotalAmount.ToString("N2", System.Globalization.CultureInfo.InvariantCulture);
            string myshoptransactionID = order.OrderRef;

            //https://testecomm.sella.it/gestpay/pagam.asp //.aspx?
            //https://ecomm.sella.it/gestpay/pagam.asp
            string urlpayment = base.PaymentData.PaySubmitUrl;


            var objCrypt = new GestPayCrypt();
            objCrypt.SetShopLogin(myshoplogin);
            objCrypt.SetCurrency(mycurrency);
            objCrypt.SetAmount(myamount);
            objCrypt.SetShopTransactionID(myshoptransactionID);
            objCrypt.Encrypt();

            string err = objCrypt.GetErrorDescription();

            string a = "";
            string b = "";
            if (objCrypt.GetErrorCode().Equals("0"))
            {
                b = objCrypt.GetEncryptedString();
                a = objCrypt.GetShopLogin();
                HttpContext.Current.Response.Redirect(urlpayment + "?a=" + a + "&b=" + b);
            }
            else
            {
                return;
            }


            //fields.Add("notify_url", ParseUrl(PaymentData.PayCallbackUrl));
            //fields.Add("return", ParseUrl(PaymentData.SiteOkUrl));
            //fields.Add("cancel_return", ParseUrl(PaymentData.SiteKoUrl));
            //fields.Add("charset", "ISO-8859-1");
            //RedirHelper.RedirectAndPOST(base.Page, base.PaymentData.PaySubmitUrl, fields);
        }

        public override void Confirm()
        {
            #region SELLA

            var objCrypt = new GestPayCrypt();

            string a = String.Empty;
            string b = String.Empty;
            string myshoplogin = String.Empty;
            string mycurrency = String.Empty;
            string myamount = string.Empty;
            string myshoptransictionID = string.Empty;
            string mybuyername = string.Empty;
            string mybuyeremail = string.Empty;
            string mytransictionresult = string.Empty;
            string myauthorizationcode = string.Empty;
            string myerrorcode = string.Empty;
            string myerrordescription = string.Empty;
            string myalertcode = string.Empty;
            string myalertdescription = string.Empty;
            string mycustominfo = string.Empty;

            try { a = Utility._PostString("a"); }
            catch { a = String.Empty; }

            try { b = Utility._PostString("b"); }
            catch { b = String.Empty; }


            objCrypt.SetShopLogin(a);
            objCrypt.SetEncryptedString(b);
            objCrypt.Decrypt();

            string codiceerr = objCrypt.GetErrorCode();
            string descrerr = objCrypt.GetErrorDescription();

            if (objCrypt.GetErrorCode().Equals("0"))
            {
                myshoplogin = objCrypt.GetShopLogin();
                mycurrency = objCrypt.GetCurrency();
                myamount = objCrypt.GetAmount();
                myshoptransictionID = objCrypt.GetShopTransactionID();
                mybuyername = objCrypt.GetBuyerName();
                mybuyeremail = objCrypt.GetBuyerEmail();
                mytransictionresult = objCrypt.GetTransactionResult();
                myauthorizationcode = objCrypt.GetAuthorizationCode();
                myerrorcode = objCrypt.GetErrorCode();
                myerrordescription = objCrypt.GetErrorDescription();
                myalertcode = objCrypt.GetAlertCode();
                myalertdescription = objCrypt.GetAlertDescription();
                mycustominfo = objCrypt.GetCustomInfo();
            }
            else
            {

            }

            var checkString = new System.Text.StringBuilder();
            checkString.Append("verifica " + myshoptransictionID + ", " 
                + mytransictionresult + ", " 
                + myshoplogin + ", " 
                + mycurrency + ", " 
                + myamount + ", " 
                + mybuyername + ", " 
                + mybuyeremail + ", " 
                + myauthorizationcode + ", " 
                + myerrorcode + ", " + 
                myerrordescription + ", " + 
                myalertcode + ", " + 
                myalertdescription + ", " 
                + mycustominfo);

            //OrdiniManager mng_ordini = new OrdiniManager();
            //CarrelloManager car_mng = new CarrelloManager();
            //ConfDatisitoManager _sitoMng = new ConfDatisitoManager();

            //  controllo se è presente un numero ordine
            if (myshoptransictionID != null && !String.IsNullOrEmpty(myshoptransictionID))
            {
                string esito = String.Empty;
                if (!String.IsNullOrEmpty(mytransictionresult))
                {
                    esito = mytransictionresult;
                }
                else
                {
                    return;
                }

                // se arrivo qui per forza esito è valorizzato
                esito = (esito.ToUpper() == "OK" || esito.ToUpper() == "IC" || esito.ToUpper() == "CO") ? "OK" : "KO";

                if (esito.ToUpper() == "OK")
                {
                    //Ordini arr_ordine = mng_ordini.getOrdine(myshoptransictionID);
                    //Ordini ordine;
                    //if (arr_ordine != null)
                    //{
                    //    ordine = arr_ordine;
                    //    if (ordine.Esitobankpass == "OK")
                    //    {
                    //        return; // ritorno nel caso l'ordine sia già completo
                    //    }
                    //}
                    //else
                    //{
                    //    return;
                    //}

                    //ordine.Esitobankpass = "OK";
                    //ordine.Ordine = "SI";
                    //ordine.Pagato = Convert.ToDecimal(myamount.Replace(".", ","));
                    //ordine.Dataordine = DateTime.Now;

                    //try
                    //{
                    //    mng_ordini.update(ordine);
                    //}
                    //catch (Exception ex)
                    //{
                        
                    //}
                }
            }
            #endregion

            var args = new ConfirmEventArgs();
            args.PaymentData = base.PaymentData;
            args.Success = true;

            string orderRef = HttpContext.Current.Request.Form["item_number"];
            var man = new OrdersManager<Order, OrdersFilter, OrderRow, OrderRowsFilter>(); //new OM();
            args.OrderToPay = man.GetByOrderRef(orderRef);

            if (args.OrderToPay.Id == 0)
            {
                args.Success = false;
                args.MessageName = "InvalidOrderRef";

                OnPaymentConfirmed(args);
                return;
            }

            string strResponse = "";
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(base.PaymentData.PaySubmitUrl);
                req.Method = "POST";
                req.ContentType = "application/x-www-form-urlencoded";
                byte[] param = HttpContext.Current.Request.BinaryRead(HttpContext.Current.Request.ContentLength);
                string strRequest = Encoding.ASCII.GetString(param);
                strRequest += "&cmd=_notify-validate";
                req.ContentLength = strRequest.Length;

                StreamWriter streamOut = new StreamWriter(req.GetRequestStream(), System.Text.Encoding.ASCII);
                streamOut.Write(strRequest);
                streamOut.Close();
                StreamReader streamIn = new StreamReader(req.GetResponse().GetResponseStream());
                strResponse = streamIn.ReadToEnd();
                streamIn.Close();
            }
            catch (Exception ex)
            {
                args.MessageName = "RequestErr";
                args.Success = false;
                args.Exception = ex;

                OnPaymentConfirmed(args);
                return;
            }

            if (strResponse == "VERIFIED")
            {

                var allowedStatus = new List<string>();
                allowedStatus.Add("completed");
                if (base.PaymentData.IsDebug)
                    allowedStatus.Add("pending");


                args.TotalPaid = 0;
                Decimal.TryParse(
                    HttpContext.Current.Request.Form["mc_gross"].Replace(',', '.'),
                    NumberStyles.Any,
                    new CultureInfo("en-US"),
                    out args.TotalPaid);


                string payment_status = HttpContext.Current.Request.Form["payment_status"].ToString();
                string receiver_email = HttpContext.Current.Request.Form["receiver_email"].ToString();

                if (!allowedStatus.Contains(payment_status.ToLower()))
                {
                    args.Success = false;
                    args.MessageName = "InvalidPaymentStatus";

                    OnPaymentConfirmed(args);
                    return;
                }

                if (base.PaymentData.PayAccount != receiver_email)
                {
                    args.Success = false;
                    args.MessageName = "InvalidReceiverEmail";

                    OnPaymentConfirmed(args);
                    return;
                }

            }
            else
            {
                args.Success = false;
                args.MessageName = "InvalidRespone:["+ strResponse +"] ";

                OnPaymentConfirmed(args);
                return;
            }

            args.MessageName = "OK";
            OnPaymentConfirmed(args);

        }

        public override void OnPaymentConfirmed(ConfirmEventArgs e)
        {
            base.OnPaymentConfirmed(e);
        }

    }


    public class GestPayCrypt
    {
        private string ShopLogin;
        private string Currency;
        private string Amount;
        private string ShopTransactionID;
        private string BuyerName;
        private string BuyerEmail;
        private string Language;
        private string CustomInfo;
        private string AuthorizationCode;
        private string ErrorCode;
        private string ErrorDescription;
        private string BankTransactionID;
        private string AlertCode;
        private string AlertDescription;
        private string EncryptedString;
        private string ToBeEncript;
        private string Decripted;
        private string TransactionResult;
        private string ProtocolAuthServer;
        private string DomainName;
        private string separator;
        private string errDescription;
        private string errNumber;
        private string Version;
        private string Min;
        private string CVV;
        private string country;
        private string vbvrisp;
        private string vbv;
        private string trans;

        public GestPayCrypt()
        {
            /*init value*/
            ShopLogin = String.Empty;
            Currency = String.Empty;
            Amount = String.Empty;
            ShopTransactionID = String.Empty;
            BuyerName = String.Empty;
            BuyerEmail = String.Empty;
            Language = String.Empty;
            CustomInfo = String.Empty;
            AuthorizationCode = String.Empty;
            ErrorCode = String.Empty;
            ErrorDescription = String.Empty;
            BankTransactionID = String.Empty;
            AlertCode = String.Empty;
            AlertDescription = String.Empty;
            EncryptedString = String.Empty;
            ToBeEncript = String.Empty;
            Decripted = String.Empty;
            ProtocolAuthServer = "http://";
            DomainName = String.Empty;
            //DomainName = "ecomm.sella.it/CryptHTTP";
            separator = "*P1*";
            errDescription = String.Empty;
            errNumber = "0";
            Min = String.Empty;
            CVV = String.Empty;
            country = String.Empty;
            vbvrisp = String.Empty;
            vbv = String.Empty;
            trans = String.Empty;
            Version = "3.0";

        }

        /**************** SET *****************/

        public void SetShopLogin(String xstr)
        {
            ShopLogin = xstr;
        }
        public void SetCurrency(String xstr)
        {
            Currency = xstr;
        }
        public void SetAmount(String xstr)
        {
            Amount = xstr;
        }
        public void SetShopTransactionID(String xstr)
        {
            ShopTransactionID = URLDecode(xstr.Trim());

        }
        public void SetMIN(String xstr)
        {
            Min = xstr;
        }
        public void SetCVV(String xstr)
        {
            CVV = xstr;
        }

        public void SetBuyerName(String xstr)
        {
            BuyerName = URLDecode(xstr.Trim());

        }
        public void SetBuyerEmail(String xstr)
        {
            BuyerEmail = xstr.Trim();
        }
        public void SetLanguage(String xstr)
        {
            Language = xstr.Trim();
        }
        public void SetCustomInfo(String xstr)
        {
            CustomInfo = URLDecode(xstr.Trim());
        }
        public void SetEncryptedString(String xstr)
        {
            EncryptedString = xstr;
        }

        // giugno '07
        public void setProtocolServer(String xstr)
        {
            ProtocolAuthServer = xstr;
        }

        // giugno '07
        public void setDomainName(String xstr)
        {
            DomainName = xstr;
        }

        /************* GET *************/

        public String GetShopLogin()
        {
            return ShopLogin;
        }
        public String GetCurrency()
        {
            return Currency;
        }
        public String GetAmount()
        {
            return Amount;
        }
        public String GetCountry()
        {
            return country;
        }
        public String GetVBV()
        {
            return vbv;
        }
        public String GetVBVrisp()
        {
            return vbvrisp;
        }

        public String GetShopTransactionID()
        {
            String app = String.Empty;
            try
            {
                app = URLDecode(ShopTransactionID);
            }
            catch (Exception ex) { }
            return app;
        }
        public String GetBuyerName()
        {
            String appBuyername = String.Empty;
            try
            {
                appBuyername = URLDecode(BuyerName);
            }
            catch (Exception ex) { appBuyername = "errore"; }
            return appBuyername;
        }
        public String GetBuyerEmail()
        {
            return BuyerEmail;
        }
        public String GetCustomInfo()
        {
            String appCustom = String.Empty;
            try
            {
                appCustom = URLDecode(CustomInfo);
            }
            catch (Exception ex) { }
            return appCustom;
        }
        public String GetAuthorizationCode()
        {
            return AuthorizationCode;
        }
        public String GetErrorCode()
        {
            return ErrorCode;
        }
        public String GetErrorDescription()
        {
            return ErrorDescription;
        }
        public String GetBankTransactionID()
        {
            return BankTransactionID;
        }
        public String GetTransactionResult()
        {
            return TransactionResult;
        }
        public String GetAlertCode()
        {
            return AlertCode;
        }
        public String GetAlertDescription()
        {
            return AlertDescription;
        }
        public String GetEncryptedString()
        {
            return EncryptedString;
        }

        // giungo '07
        public String getProtocolServer()
        {
            return ProtocolAuthServer;
        }

        // giungo '07
        public String getDomainName()
        {
            return DomainName;
        }


        public bool Encrypt()
        {
            String sErr = String.Empty;
            ErrorCode = "0";
            ErrorDescription = String.Empty;
            try
            {/*contact Encryption Server*/

                // Giugno 2007
                // se il protocollo e il dominio non sono stati modificati dall'esercente questi puntano di default a
                // https://testecomm.sella.it--> per i codici di test oppure a
                // https://ecomms2s.sella.it --> per i codici di produzione

                if (String.IsNullOrEmpty(ProtocolAuthServer))
                {
                    ProtocolAuthServer = "http://";
                }

                trans = ShopLogin.Substring(0, 6);
                trans = trans.ToLower();

                if (String.IsNullOrEmpty(DomainName))
                {
                    if (trans.Equals("gespay"))
                    {
                        DomainName = "testecomm.sella.it/CryptHTTP"; // codici di test
                    }
                    else
                    {
                        DomainName = "ecomms2s.sella.it/CryptHTTP";  // codici di produzione
                    }
                }

                // ************

                if (ShopLogin.Length <= 0)
                {
                    ErrorCode = "546";
                    ErrorDescription = "IDshop not valid";
                    return false;
                }

                if (Currency.Length <= 0)
                {
                    ErrorCode = "552";
                    ErrorDescription = "Currency not valid";
                    return false;
                }

                if (Amount.Length <= 0)
                {
                    ErrorCode = "553";
                    ErrorDescription = "Amount not valid";
                    return false;
                }

                if (ShopTransactionID.Length <= 0)
                {
                    ErrorCode = "551";
                    ErrorDescription = "Shop Transaction ID not valid";
                    return false;
                }

                ToBeEncript = String.Empty;

                if (CVV.Length > 0)
                {
                    ToBeEncript = ToBeEncript + separator + "PAY1_CVV=" + CVV;
                }

                if (Min.Length > 0)
                {
                    ToBeEncript = ToBeEncript + separator + "PAY1_MIN=" + Min;
                }

                if (Currency.Length > 0)
                {
                    ToBeEncript = ToBeEncript + separator + "PAY1_UICCODE=" + Currency;
                }

                if (Amount.Length > 0)
                {
                    ToBeEncript = ToBeEncript + separator + "PAY1_AMOUNT=" + Amount;
                }

                if (ShopTransactionID.Length > 0)
                {
                    ToBeEncript = ToBeEncript + separator + "PAY1_SHOPTRANSACTIONID=" + ShopTransactionID;
                }

                if (BuyerName.Length > 0)
                {
                    ToBeEncript = ToBeEncript + separator + "PAY1_CHNAME=" + BuyerName;
                }

                if (BuyerEmail.Length > 0)
                {
                    ToBeEncript = ToBeEncript + separator + "PAY1_CHEMAIL=" + BuyerEmail;
                }

                if (Language.Length > 0)
                {
                    ToBeEncript = ToBeEncript + separator + "PAY1_IDLANGUAGE=" + Language;
                }

                if (CustomInfo.Length > 0)
                {
                    ToBeEncript = ToBeEncript + separator + CustomInfo;
                }

                String urlString =
                    ProtocolAuthServer + DomainName + "/Encrypt.asp?a=" +
                    ShopLogin + "&b=" + ToBeEncript.Substring(4, ToBeEncript.Length - 4) + "&c=" + Version;

                WebRequest wrGETURL;
                wrGETURL = WebRequest.Create(urlString);

                Stream objStream;
                objStream = wrGETURL.GetResponse().GetResponseStream();
                StreamReader objReader = new StreamReader(objStream);

                int nStart = 0;
                int nEnd = 0;
                String line = String.Empty;
                while (line != null)
                {
                    line = objReader.ReadLine();

                    //HttpContext.Current.Response.Write("1");

                    if (line != null)
                    {
                        //HttpContext.Current.Response.Write("2");
                        nStart = line.IndexOf("#cryptstring#");
                        nEnd = line.LastIndexOf("#/cryptstring#");
                        if (nStart != -1 & nEnd > nStart + 14)
                        {
                            //HttpContext.Current.Response.Write("3");
                            EncryptedString = line.Substring(nStart + 13, nEnd - (nStart + 13));
                        }

                        nStart = line.IndexOf("#error#");
                        nEnd = line.LastIndexOf("#/error#");
                        if (nStart != -1 & nEnd > nStart + 8)
                        {
                            //HttpContext.Current.Response.Write("4");
                            sErr = line.Substring(nStart + 7, nEnd - (nStart + 7));

                            int intsep = sErr.IndexOf("-");
                            ErrorCode = sErr.Substring(0, intsep);
                            ErrorDescription = sErr.Substring(intsep + 1, sErr.Length - (intsep + 1));
                            return false;
                        }
                    }
                }

                objReader.Close();
                return true;
            }
            catch (UriFormatException ex) { ErrorCode = "9999"; ErrorDescription = "Bad URL"; return false; }
            catch (IOException ex) { ErrorCode = "9999"; ErrorDescription = "Bad URL Request"; return false; }
            catch (Exception ex) { ErrorCode = "9999"; ErrorDescription = "ServiceException occurred."; return false; }
        }


        public bool Decrypt()
        {
            String sErr;
            ErrorCode = "0";
            ErrorDescription = String.Empty;
            String strdaelim = String.Empty;
            if (ShopLogin.Length <= 0)
            {
                ErrorCode = "546";
                ErrorDescription = "IDshop not valid";
                return false;
            }

            if (EncryptedString.Length <= 0)
            {
                ErrorCode = "1009";
                ErrorDescription = "String to Decrypt not valid";
                return false;
            }

            // Giugno 2007
            // se il protocollo e il dominio non sono stati modificati dall'esercente questi puntano di default a
            // https://testecomm.sella.it--> per i codici di test oppure a
            // https://ecomms2s.sella.it --> per i codici di produzione

            if (String.IsNullOrEmpty(ProtocolAuthServer))
            {
                ProtocolAuthServer = "http://";
            }

            trans = ShopLogin.Substring(0, 6);
            trans = trans.ToLower();

            if (String.IsNullOrEmpty(DomainName))
            {
                if (trans.Equals("gespay"))
                {
                    DomainName = "testecomm.sella.it/CryptHTTP"; // codici di test
                }
                else
                {
                    DomainName = "ecomms2s.sella.it/CryptHTTP";  // codici di produzione
                }
            }
            // ************

            try
            {
                /*contact Decryption Server*/

                String urlString =
                    ProtocolAuthServer + DomainName + "/Decrypt.asp?a=" +
                    ShopLogin + "&b=" + EncryptedString + "&c=" + Version;

                WebRequest wrGETURL;
                wrGETURL = WebRequest.Create(urlString);

                Stream objStream;
                objStream = wrGETURL.GetResponse().GetResponseStream();
                StreamReader objReader = new StreamReader(objStream);

                int nStart = 0;
                int nEnd = 0;
                String line = String.Empty;
                while (line != null)
                {
                    line = objReader.ReadLine();
                    if (line != null)
                    {
                        nStart = line.IndexOf("#decryptstring#");
                        nEnd = line.LastIndexOf("#/decryptstring#");
                        if (nStart != -1 & nEnd > nStart + 16)
                        {
                            Decripted = line.Substring(nStart + 15, nEnd - (nStart + 15));
                        }
                        nStart = line.IndexOf("#error#");
                        nEnd = line.LastIndexOf("#/error#");
                        if (nStart != -1 & nEnd > nStart + 8)
                        {
                            sErr = line.Substring(nStart + 7, nEnd - (nStart + 7));
                            int intsep = sErr.IndexOf("-");
                            ErrorCode = sErr.Substring(0, intsep);
                            ErrorDescription = sErr.Substring(intsep + 1, sErr.Length - (intsep + 1));
                            return false;
                        }
                    }
                }
                objReader.Close();
                if (Decripted.Trim().Equals(String.Empty))
                {
                    ErrorCode = "9999";
                    ErrorDescription = "Void String";
                    return false;
                }

                if (!Parsing(Decripted)) { return false; }
                return true;
            }
            catch (UriFormatException ex) { ErrorCode = "9999"; ErrorDescription = "Bad URL"; return false; }
            catch (IOException ex) { ErrorCode = "9999"; ErrorDescription = "Bad URL Request"; return false; }
            catch (Exception ex) { ErrorCode = "9999"; ErrorDescription = "Service Exception occurred."; return false; }

        }


        private bool Parsing(String StringToBeParsed)
        {
            int nStart = 0;
            int nEnd = 0;
            ErrorCode = String.Empty;
            ErrorDescription = String.Empty;

            try
            {
                /* set attribute from crypt string*/

                nStart = StringToBeParsed.IndexOf("PAY1_UICCODE");
                if (nStart != -1)
                {
                    nEnd = StringToBeParsed.IndexOf(separator, nStart);
                    if (nEnd == -1)
                    {
                        nEnd = StringToBeParsed.Length;
                        Currency = StringToBeParsed.Substring(nStart + 13, nEnd - (nStart + 13));
                        if (nStart >= 4) { StringToBeParsed = StringToBeParsed.Substring(0, nStart - 4); }
                        else { StringToBeParsed = StringToBeParsed.Substring(0, nStart); }
                    }
                    else
                    {
                        Currency = StringToBeParsed.Substring(nStart + 13, nEnd - (nStart + 13));
                        StringToBeParsed = StringToBeParsed.Substring(0, nStart) + StringToBeParsed.Substring(nEnd + 4, StringToBeParsed.Length - (nEnd + 4));
                    }
                }
                nStart = StringToBeParsed.IndexOf("PAY1_AMOUNT");
                if (nStart != -1)
                {
                    nEnd = StringToBeParsed.IndexOf(separator, nStart);
                    if (nEnd == -1)
                    {
                        nEnd = StringToBeParsed.Length;
                        Amount = StringToBeParsed.Substring(nStart + 12, nEnd - (nStart + 12));
                        if (nStart >= 4) { StringToBeParsed = StringToBeParsed.Substring(0, nStart - 4); }
                        else { StringToBeParsed = StringToBeParsed.Substring(0, nStart); }
                    }
                    else
                    {
                        Amount = StringToBeParsed.Substring(nStart + 12, nEnd - (nStart + 12));
                        StringToBeParsed = StringToBeParsed.Substring(0, nStart) + StringToBeParsed.Substring(nEnd + 4, StringToBeParsed.Length - (nEnd + 4));
                    }
                }
                nStart = StringToBeParsed.IndexOf("PAY1_SHOPTRANSACTIONID");
                if (nStart != -1)
                {
                    nEnd = StringToBeParsed.IndexOf(separator, nStart);
                    if (nEnd == -1)
                    {
                        nEnd = StringToBeParsed.Length;
                        ShopTransactionID = StringToBeParsed.Substring(nStart + 23, nEnd - (nStart + 23));
                        if (nStart >= 4) { StringToBeParsed = StringToBeParsed.Substring(0, nStart - 4); }
                        else { StringToBeParsed = StringToBeParsed.Substring(0, nStart); }
                    }
                    else
                    {
                        ShopTransactionID = StringToBeParsed.Substring(nStart + 23, nEnd - (nStart + 23));
                        StringToBeParsed = StringToBeParsed.Substring(0, nStart) + StringToBeParsed.Substring(nEnd + 4, StringToBeParsed.Length - (nEnd + 4));
                    }
                }
                nStart = StringToBeParsed.IndexOf("PAY1_CHNAME");
                if (nStart != -1)
                {
                    nEnd = StringToBeParsed.IndexOf(separator, nStart);
                    if (nEnd == -1)
                    {
                        nEnd = StringToBeParsed.Length;
                        BuyerName = StringToBeParsed.Substring(nStart + 12, nEnd - (nStart + 12));
                        if (nStart >= 4) { StringToBeParsed = StringToBeParsed.Substring(0, nStart - 4); }
                        else { StringToBeParsed = StringToBeParsed.Substring(0, nStart); }
                    }
                    else
                    {
                        BuyerName = StringToBeParsed.Substring(nStart + 12, nEnd - (nStart + 12));
                        StringToBeParsed = StringToBeParsed.Substring(0, nStart) + StringToBeParsed.Substring(nEnd + 4, StringToBeParsed.Length - (nEnd + 4));
                    }
                }
                nStart = StringToBeParsed.IndexOf("PAY1_CHEMAIL");
                if (nStart != -1)
                {
                    nEnd = StringToBeParsed.IndexOf(separator, nStart);
                    if (nEnd == -1)
                    {
                        nEnd = StringToBeParsed.Length;
                        BuyerEmail = StringToBeParsed.Substring(nStart + 13, nEnd - (nStart + 13));
                        if (nStart >= 4) { StringToBeParsed = StringToBeParsed.Substring(0, nStart - 4); }
                        else { StringToBeParsed = StringToBeParsed.Substring(0, nStart); }
                    }
                    else
                    {
                        BuyerEmail = StringToBeParsed.Substring(nStart + 13, nEnd - (nStart + 13));
                        StringToBeParsed = StringToBeParsed.Substring(0, nStart) + StringToBeParsed.Substring(nEnd + 4, StringToBeParsed.Length - (nEnd + 4));
                    }
                }
                nStart = StringToBeParsed.IndexOf("PAY1_AUTHORIZATIONCODE");
                if (nStart != -1)
                {
                    nEnd = StringToBeParsed.IndexOf(separator, nStart);
                    if (nEnd == -1)
                    {
                        nEnd = StringToBeParsed.Length;
                        AuthorizationCode = StringToBeParsed.Substring(nStart + 23, nEnd - (nStart + 23));
                        if (nStart >= 4) { StringToBeParsed = StringToBeParsed.Substring(0, nStart - 4); }
                        else { StringToBeParsed = StringToBeParsed.Substring(0, nStart); }
                    }
                    else
                    {
                        AuthorizationCode = StringToBeParsed.Substring(nStart + 23, nEnd - (nStart + 23));
                        StringToBeParsed = StringToBeParsed.Substring(0, nStart) + StringToBeParsed.Substring(nEnd + 4, StringToBeParsed.Length - (nEnd + 4));
                    }
                }
                nStart = StringToBeParsed.IndexOf("PAY1_ERRORCODE");
                if (nStart != -1)
                {
                    nEnd = StringToBeParsed.IndexOf(separator, nStart);
                    if (nEnd == -1)
                    {
                        nEnd = StringToBeParsed.Length;
                        ErrorCode = StringToBeParsed.Substring(nStart + 15, nEnd - (nStart + 15));
                        if (nStart >= 4) { StringToBeParsed = StringToBeParsed.Substring(0, nStart - 4); }
                        else { StringToBeParsed = StringToBeParsed.Substring(0, nStart); }
                    }
                    else
                    {
                        ErrorCode = StringToBeParsed.Substring(nStart + 15, nEnd - (nStart + 15));
                        StringToBeParsed = StringToBeParsed.Substring(0, nStart) + StringToBeParsed.Substring(nEnd + 4, StringToBeParsed.Length - (nEnd + 4));
                    }
                }
                nStart = StringToBeParsed.IndexOf("PAY1_ERRORDESCRIPTION");
                if (nStart != -1)
                {
                    nEnd = StringToBeParsed.IndexOf(separator, nStart);
                    if (nEnd == -1)
                    {
                        nEnd = StringToBeParsed.Length;
                        ErrorDescription = StringToBeParsed.Substring(nStart + 22, nEnd - (nStart + 22));
                        if (nStart >= 4) { StringToBeParsed = StringToBeParsed.Substring(0, nStart - 4); }
                        else { StringToBeParsed = StringToBeParsed.Substring(0, nStart); }
                    }
                    else
                    {
                        ErrorDescription = StringToBeParsed.Substring(nStart + 22, nEnd - (nStart + 22));
                        StringToBeParsed = StringToBeParsed.Substring(0, nStart) + StringToBeParsed.Substring(nEnd + 4, StringToBeParsed.Length - (nEnd + 4));
                    }
                }
                nStart = StringToBeParsed.IndexOf("PAY1_BANKTRANSACTIONID");
                if (nStart != -1)
                {
                    nEnd = StringToBeParsed.IndexOf(separator, nStart);
                    if (nEnd == -1)
                    {
                        nEnd = StringToBeParsed.Length;
                        BankTransactionID = StringToBeParsed.Substring(nStart + 23, nEnd - (nStart + 23));
                        if (nStart >= 4) { StringToBeParsed = StringToBeParsed.Substring(0, nStart - 4); }
                        else { StringToBeParsed = StringToBeParsed.Substring(0, nStart); }
                    }
                    else
                    {
                        BankTransactionID = StringToBeParsed.Substring(nStart + 23, nEnd - (nStart + 23));
                        StringToBeParsed = StringToBeParsed.Substring(0, nStart) + StringToBeParsed.Substring(nEnd + 4, StringToBeParsed.Length - (nEnd + 4));
                    }
                }
                nStart = StringToBeParsed.IndexOf("PAY1_ALERTCODE");
                if (nStart != -1)
                {
                    nEnd = StringToBeParsed.IndexOf(separator, nStart);
                    if (nEnd == -1)
                    {
                        nEnd = StringToBeParsed.Length;
                        AlertCode = StringToBeParsed.Substring(nStart + 15, nEnd - (nStart + 15));
                        if (nStart >= 4) { StringToBeParsed = StringToBeParsed.Substring(0, nStart - 4); }
                        else { StringToBeParsed = StringToBeParsed.Substring(0, nStart); }
                    }
                    else
                    {
                        AlertCode = StringToBeParsed.Substring(nStart + 15, nEnd - (nStart + 15));
                        StringToBeParsed = StringToBeParsed.Substring(0, nStart) + StringToBeParsed.Substring(nEnd + 4, StringToBeParsed.Length - (nEnd + 4));
                    }
                }
                nStart = StringToBeParsed.IndexOf("PAY1_ALERTDESCRIPTION");
                if (nStart != -1)
                {
                    nEnd = StringToBeParsed.IndexOf(separator, nStart);
                    if (nEnd == -1)
                    {
                        nEnd = StringToBeParsed.Length;
                        AlertDescription = StringToBeParsed.Substring(nStart + 22, nEnd - (nStart + 22));
                        if (nStart >= 4) { StringToBeParsed = StringToBeParsed.Substring(0, nStart - 4); }
                        else { StringToBeParsed = StringToBeParsed.Substring(0, nStart); }
                    }
                    else
                    {
                        AlertDescription = StringToBeParsed.Substring(nStart + 22, nEnd - (nStart + 22));
                        StringToBeParsed = StringToBeParsed.Substring(0, nStart) + StringToBeParsed.Substring(nEnd + 4, StringToBeParsed.Length - (nEnd + 4));
                    }
                }


                nStart = StringToBeParsed.IndexOf("PAY1_COUNTRY");
                if (nStart != -1)
                {
                    nEnd = StringToBeParsed.IndexOf(separator, nStart);
                    if (nEnd == -1)
                    {
                        nEnd = StringToBeParsed.Length;
                        country = StringToBeParsed.Substring(nStart + 13, nEnd - (nStart + 13));
                        if (nStart >= 4) { StringToBeParsed = StringToBeParsed.Substring(0, nStart - 4); }
                        else { StringToBeParsed = StringToBeParsed.Substring(0, nStart); }
                    }
                    else
                    {
                        country = StringToBeParsed.Substring(nStart + 13, nEnd - (nStart + 13));
                        StringToBeParsed = StringToBeParsed.Substring(0, nStart) + StringToBeParsed.Substring(nEnd + 4, StringToBeParsed.Length - (nEnd + 4));
                    }
                }
                nStart = StringToBeParsed.IndexOf("PAY1_VBVRISP");
                if (nStart != -1)
                {
                    nEnd = StringToBeParsed.IndexOf(separator, nStart);
                    if (nEnd == -1)
                    {
                        nEnd = StringToBeParsed.Length;
                        vbvrisp = StringToBeParsed.Substring(nStart + 13, nEnd - (nStart + 13));
                        if (nStart >= 4) { StringToBeParsed = StringToBeParsed.Substring(0, nStart - 4); }
                        else { StringToBeParsed = StringToBeParsed.Substring(0, nStart); }
                    }
                    else
                    {
                        vbvrisp = StringToBeParsed.Substring(nStart + 13, nEnd - (nStart + 13));
                        StringToBeParsed = StringToBeParsed.Substring(0, nStart) + StringToBeParsed.Substring(nEnd + 4, StringToBeParsed.Length - (nEnd + 4));
                    }
                }
                nStart = StringToBeParsed.IndexOf("PAY1_VBV");
                if (nStart != -1)
                {
                    nEnd = StringToBeParsed.IndexOf(separator, nStart);
                    if (nEnd == -1)
                    {
                        nEnd = StringToBeParsed.Length;
                        vbv = StringToBeParsed.Substring(nStart + 9, nEnd - (nStart + 9));
                        if (nStart >= 4) { StringToBeParsed = StringToBeParsed.Substring(0, nStart - 4); }
                        else { StringToBeParsed = StringToBeParsed.Substring(0, nStart); }
                    }
                    else
                    {
                        vbv = StringToBeParsed.Substring(nStart + 9, nEnd - (nStart + 9));
                        StringToBeParsed = StringToBeParsed.Substring(0, nStart) + StringToBeParsed.Substring(nEnd + 4, StringToBeParsed.Length - (nEnd + 4));
                    }
                }
                nStart = StringToBeParsed.IndexOf("PAY1_IDLANGUAGE");
                if (nStart != -1)
                {
                    nEnd = StringToBeParsed.IndexOf(separator, nStart);
                    if (nEnd == -1)
                    {
                        nEnd = StringToBeParsed.Length;
                        Language = StringToBeParsed.Substring(nStart + 16, nEnd - (nStart + 16));
                        if (nStart >= 4) { StringToBeParsed = StringToBeParsed.Substring(0, nStart - 4); }
                        else { StringToBeParsed = StringToBeParsed.Substring(0, nStart); }
                    }
                    else
                    {
                        Language = StringToBeParsed.Substring(nStart + 16, nEnd - (nStart + 16));
                        StringToBeParsed = StringToBeParsed.Substring(0, nStart) + StringToBeParsed.Substring(nEnd + 4, StringToBeParsed.Length - (nEnd + 4));
                    }
                }
                nStart = StringToBeParsed.IndexOf("PAY1_TRANSACTIONRESULT");
                if (nStart != -1)
                {
                    nEnd = StringToBeParsed.IndexOf(separator, nStart);
                    if (nEnd == -1)
                    {
                        nEnd = StringToBeParsed.Length;
                        TransactionResult = StringToBeParsed.Substring(nStart + 23, nEnd - (nStart + 23));
                        if (nStart >= 4) { StringToBeParsed = StringToBeParsed.Substring(0, nStart - 4); }
                        else { StringToBeParsed = StringToBeParsed.Substring(0, nStart); }
                    }
                    else
                    {
                        TransactionResult = StringToBeParsed.Substring(nStart + 23, nEnd - (nStart + 23));
                        StringToBeParsed = StringToBeParsed.Substring(0, nStart) + StringToBeParsed.Substring(nEnd + 4, StringToBeParsed.Length - (nEnd + 4));
                    }
                }
                CustomInfo = StringToBeParsed.Trim();
            }

            catch (Exception e) { ErrorCode = "9999"; ErrorDescription = "Error parsing String"; return false; }
            return true;
        }


        public String URLDecode(String str)
        {
            if (String.IsNullOrEmpty(str))
            {
                return null;
            }
            else
            {
                return HttpUtility.UrlDecode(str);
            }
        }
    }
}//ns