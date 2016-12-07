using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Acme
{
    /// <summary>
    /// sample masterpage interface to share masterpage properties in pages
    /// </summary>
    public interface IMaster
    {
        string DataSection { get; set; }
        string LinkFooter { get; set; }
        string TextLinkFooter { get; set; }

        bool IsHomepage { get; set; }
    }
}