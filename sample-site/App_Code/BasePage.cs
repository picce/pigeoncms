using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PigeonCms;
using System.Threading;
using System.Globalization;

public class BasePage : PigeonCms.Engine.BasePage
{

    /// <summary>
    /// lang priority DESC
    /// 3- qrystring
    /// 2- browser settings
    /// 1- default from web.config
    /// </summary>
    protected override void InitializeCulture()
    {
        bool setCultureDone = false;
        string matchingCulture = "";
        var list = new Dictionary<string, string>(Config.CultureList, StringComparer.InvariantCultureIgnoreCase);

        //try different qrystring param
        string lngParam = Utility._QueryString("lng");
        string cultureParam = lang2Culture(lngParam);


        //check if user/linked requested lang is enabled
        if (!string.IsNullOrEmpty(cultureParam))
        {
            if (!containsLang(cultureParam, list, out matchingCulture))
                cultureParam = Config.CultureDefault;

            setCulture(cultureParam, "");
            setCultureDone = true;
        }

        //check if browser requested lang is enabled
        if (!setCultureDone)
        {
            string len = "";
            if (Request.UserLanguages != null && Request.UserLanguages.Length > 0)
                len = Request.UserLanguages[0];

            if (containsLang(len, list, out matchingCulture))
            {
                setCulture(matchingCulture, "");
                setCultureDone = true;
            }
            else
            {
                //if not enabled then set default lang
                len = Config.CultureDefault;
                setCulture(len, "");
                setCultureDone = true;
            }

        }
        //base.InitializeCulture();
    }

    /// <summary>
    /// cast lang to similar culture code
    /// </summary>
    /// <param name="lng"></param>
    /// <returns></returns>
    private string lang2Culture(string lng)
    {
        string res = "";
        switch (lng.ToLower())
        {
            case "it":
                res = "it-IT";
                break;
            case "en":
                res = "en-US";
                break;
            case "es":
                res = "es-ES";
                break;
        }

        return res;
    }

    /// <summary>
    /// check if lang exists (not culture) in list
    /// </summary>
    private bool containsLang(string lang, Dictionary<string, string> list, out string matchingCulture)
    {
        bool res = false;
        matchingCulture = lang;

        if (lang.Length > 2)
            lang = lang.Substring(0, 2);

        foreach (var l in list)
        {
            string key = l.Key;
            if (key.Length > 2)
                key = key.Substring(0, 2);

            if (lang.Equals(key, StringComparison.InvariantCultureIgnoreCase))
            {
                matchingCulture = l.Key;
                res = true;
                break;
            }
        }
        return res;
    }

    /// <summary>
    /// returns lang from culture code
    /// </summary>
    /// <param name="culture"></param>
    /// <returns></returns>
    private string culture2Lang(string culture)
    {
        string res = culture.ToLower();
        if (res.Length > 2)
            res = res.Substring(0, 2);

        return res;
    }

    /// <Summary>
    /// Sets the current UICulture and CurrentCulture based on
    /// the arguments
    /// </Summary>
    private void setCulture(string culture, string displayName)
    {
        Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);
        Thread.CurrentThread.CurrentCulture = new CultureInfo(culture);
    }

}