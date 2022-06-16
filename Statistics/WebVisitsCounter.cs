// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Statistics.WebVisitsCounter
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Text;
using System.Web;
using System.Web.UI;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Statistics
{
  /// <summary>
  /// A counter control that enables page visits counting through an HttpHandler.
  /// The visits can be defined with ObjectType and ObjectId.
  /// The control allows to count visits even when we use Output Caching
  /// The control can work with JavaScript ON - helps to avoid web crawler visits to be counted
  /// It can also work with JavaScript OFF - for no JavaScript enabled browsers, but might also count web crawlers
  /// 
  /// Works by inserting an image tag with a custom generated SRC attribute to a handler counting unique visits.
  /// 
  /// The control works in the following states:
  /// 1. Enabled UseJavaScriptCounter (by default):
  ///     A JavaScript is constructed to read the UniqueVisitorCookieName cookie. If the cookie is set, an IMG element is constructed with
  ///     SRC attribute concatenated from the cookie. If the cookie is not set, the JavaScript generates a random number and stores it
  ///     as a cookie and generates the SRC attribute of the IMG element.
  /// 2. Not enabled UseJavaScriptCounter (not recommended):
  ///     2.1. A UniqueVisitorCookieName is received in the client's request:
  ///             Output cache prevents reading cookie.
  ///     2.2. A UniqueVisitorCookieName is not received in the client's request:
  ///             Output cache prevents reading cookie value, generating a random number and setting cookie.
  /// </summary>
  public class WebVisitsCounter : Control
  {
    private double uniqueVisitorCookieDuration = 10.0;
    private string uniqueVisitorCookieName = "VisitorsCounterUniqueId";
    private bool useJavaScriptCounter = true;
    private string imageHandlerUrl = "Sitefinity/WebCounter";
    private string counterRequestPattern = "counter.gif?action=visit&objectType={0}&objectid={1}&imgExpire={2}&random=";
    private int counterExpirationInHours = 24;

    /// <summary>Gets or sets the type of the object.</summary>
    /// <value>The type of the object.</value>
    public string ObjectType { get; set; }

    /// <summary>Gets or sets the object Id.</summary>
    /// <value>The object Id.</value>
    public string ObjectId { get; set; }

    /// <summary>
    /// Gets or sets the unique visitor cookie duration in years .
    /// This affects how long a user's browser should be considered a unique visitor and not counted twice.
    /// Default value is 10.
    /// </summary>
    /// <value>The cookie hours to expire.</value>
    public double UniqueVisitorCookieDuration
    {
      get => this.uniqueVisitorCookieDuration;
      set => this.uniqueVisitorCookieDuration = value;
    }

    /// <summary>
    /// Gets or sets the unique visitor cookie name. This cookie is used in case we want to add this as additional unique parameter to the counter request
    /// and thus avoid caching of counter request at some proxy between the browser and the server
    /// Default value "VisitorsCounterUniqueId".
    /// </summary>
    /// <value>The cookie key.</value>
    public string UniqueVisitorCookieName
    {
      get => this.uniqueVisitorCookieName;
      set => this.uniqueVisitorCookieName = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the Control to use JavaScript to initiate the call to the counter Http handler.
    /// The implementation creates img tag and sets the SRC attribute of the image counter using JavaScript.
    /// Most web crawlers do not support JavaScript and therefore setting enabling this setting can prevent some web crawler(bots) visits to be counted.
    /// If set to false, the img tag and src attribute will be generated on the server and the counting will happen without the use of JavaScript.
    /// Default value: True.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if use JavaScript in the control; otherwise, <c>false</c>.
    /// </value>
    public bool UseJavaScriptCounter
    {
      get => this.useJavaScriptCounter;
      set => this.useJavaScriptCounter = value;
    }

    /// <summary>
    /// Gets or sets the image handler.
    /// Default value is "Sitefinity/WebCounter".
    /// </summary>
    /// <value>The image handler.</value>
    public string CounterHandlerUrl
    {
      get => this.imageHandlerUrl;
      set => this.imageHandlerUrl = value;
    }

    public string CounterRequestPattern
    {
      get => this.counterRequestPattern;
      set => this.counterRequestPattern = value;
    }

    /// <summary>
    /// Gets or sets the image expiration hours that are send as response header to the browser.
    /// This setting affects when the next count for the same object id on the same browser can happen
    /// Default value is 24.
    /// </summary>
    /// <value>The image expiration hours.</value>
    public int CounterExpirationInHours
    {
      get => this.counterExpirationInHours;
      set => this.counterExpirationInHours = value;
    }

    protected override void Render(HtmlTextWriter writer)
    {
      char ch = '\n';
      string str1 = UrlPath.ResolveAbsoluteUrl("~/" + this.CounterHandlerUrl);
      if (this.UseJavaScriptCounter)
      {
        string str2 = string.Format(this.CounterRequestPattern, (object) HttpUtility.UrlEncode(this.ObjectType), (object) HttpUtility.UrlEncode(this.ObjectId), (object) this.CounterExpirationInHours);
        string str3 = string.Format("div_visit_{0}_{1}", (object) this.ObjectType, (object) this.ObjectId);
        string str4 = string.Format("<div id=\"{0}\" style=\"display:none;\" >", (object) str3);
        writer.Write(str4);
        string str5 = "<script>" + (object) ch + "(function () {" + (object) ch + "function readCookie() {" + (object) ch + "  var ca = document.cookie.split(';');" + (object) ch + "  for(var i=0;i < ca.length;i++) {" + (object) ch + "   var c = ca[i];" + (object) ch + "   while (c.charAt(0)==' ') c = c.substring(1,c.length);" + (object) ch + "   if (c.indexOf('" + this.UniqueVisitorCookieName + "=') == 0) return c.substring('" + this.UniqueVisitorCookieName + "='.length,c.length);" + (object) ch + "  }" + (object) ch + "  return null;" + (object) ch + "}" + (object) ch + "var cookieStoredValue = readCookie();" + (object) ch + "if (cookieStoredValue === null) {" + (object) ch + "  var exdate = new Date();" + (object) ch + "  exdate.setFullYear(exdate.getFullYear() + " + (object) this.UniqueVisitorCookieDuration + ");" + (object) ch + "  var randomValue = Math.floor(Math.random()*" + (object) 100000000 + ");" + (object) ch + "  document.cookie='" + this.UniqueVisitorCookieName + "=' + randomValue + '; expires=' + exdate.toUTCString();" + (object) ch + "  cookieStoredValue = randomValue;" + (object) ch + "}" + (object) ch + "var image = document.createElement('img');" + (object) ch + "image.setAttribute('src', '" + str1 + "/" + str2 + "' + cookieStoredValue);" + (object) ch + "document.getElementById('" + str3 + "').appendChild(image);" + (object) ch + "}());" + (object) ch + "</script>";
        writer.Write(str5);
        writer.Write("</div>");
      }
      else
      {
        string str6 = "1";
        string str7 = string.Format(this.CounterRequestPattern, (object) HttpUtility.UrlEncode(this.ObjectType), (object) HttpUtility.UrlEncode(this.ObjectId), (object) this.CounterExpirationInHours);
        string str8 = string.Format("{0}/{1}{2}", (object) str1, (object) str7, (object) str6);
        string str9 = string.Format("<img id=\"{0}\" src=\"{1}\" height=\"1\" width=\"1\" style=\"display:none;\" />", (object) string.Format("img_visit_{0}_{1}", (object) this.ObjectType, (object) this.ObjectId), (object) str8);
        writer.Write(str9);
      }
      base.Render(writer);
    }

    protected override void OnInit(EventArgs e)
    {
      base.OnInit(e);
      StringBuilder stringBuilder = new StringBuilder();
      bool flag = false;
      if (this.ObjectId.IsNullOrWhitespace() || this.ObjectType.IsNullOrWhitespace())
      {
        flag = true;
        stringBuilder.AppendLine("ObjectId, ObjectType are required parameters and must be non-whitespace.");
      }
      if (this.UniqueVisitorCookieDuration < 0.0)
      {
        flag = true;
        stringBuilder.AppendLine("CookieYearsToExpire property can take only a non-negative value.");
      }
      if (this.UniqueVisitorCookieName.IsNullOrWhitespace())
      {
        flag = true;
        stringBuilder.AppendLine("UniqueVisitorCookieKey must have a non-whitespace value.");
      }
      if (this.CounterExpirationInHours < 0)
      {
        flag = true;
        stringBuilder.AppendLine("ImageHoursToExpire property can take only a non-negative value.");
      }
      if (this.CounterHandlerUrl.IsNullOrWhitespace())
      {
        flag = true;
        stringBuilder.AppendLine("ImageHandler must have a non-whitespace value");
      }
      if (this.CounterRequestPattern.IsNullOrWhitespace() || !this.CounterRequestPattern.Contains("{0}") || !this.CounterRequestPattern.Contains("{1}") || !this.CounterRequestPattern.Contains("{2}") || !this.CounterRequestPattern.EndsWith("="))
      {
        flag = true;
        stringBuilder.AppendLine("ImageNamePattern must have 3 place holders and end with '=' for a random number parameter preventing proxy caching.");
      }
      if (flag)
        throw new ArgumentException(stringBuilder.ToString());
    }
  }
}
