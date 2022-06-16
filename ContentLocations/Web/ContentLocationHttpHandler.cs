// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ContentLocations.Web.ContentLocationHttpHandler
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.ContentLocations.Web.Services;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.RecycleBin;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.ContentLocations.Web
{
  internal class ContentLocationHttpHandler : IHttpHandler
  {
    public const string ContentIdKey = "item_id";
    public const string ContentTypeKey = "item_type";
    public const string ContentProviderNameKey = "item_provider";
    public const string ContentCultureKey = "item_culture";
    public const string PageIdKey = "page_id";

    public bool IsReusable => true;

    public void ProcessRequest(HttpContext context)
    {
      System.Web.HttpResponse response = context.Response;
      System.Web.HttpRequest request = context.Request;
      Guid result1 = Guid.Empty;
      if (!Guid.TryParse(request.QueryString["item_id"], out result1))
      {
        this.SetNoContentLocationMessage(response);
      }
      else
      {
        Guid result2;
        Guid.TryParse(request.QueryString["page_id"], out result2);
        string name1 = request.QueryString["item_type"];
        if (string.IsNullOrEmpty(name1))
        {
          this.SetNoContentLocationMessage(response);
        }
        else
        {
          string name2 = request.QueryString["item_culture"];
          if (!string.IsNullOrEmpty(name2))
            SystemManager.CurrentContext.Culture = CultureInfo.GetCultureInfo(name2);
          string providerName = request.QueryString["item_provider"];
          object obj;
          IContentLocation contentLocation = ContentItemLocationService.GetFilteredContentItemLocations(TypeResolutionService.ResolveType(name1), providerName, result1, out obj, result2, true, true).FirstOrDefault<IContentLocation>();
          if (contentLocation != null)
          {
            string str1 = contentLocation.GetUrl(obj);
            if (!ObjectFactory.Resolve<IRedirectUriValidator>().IsValid(str1))
            {
              this.SetNoContentLocationMessage(response);
            }
            else
            {
              TimeSpan timeSpan = TimeSpan.FromHours(Config.Get<PagesConfig>().SharedLinkExpirationTime);
              Uri result3;
              string forUrlPath;
              if (Uri.TryCreate(str1, UriKind.Absolute, out result3))
              {
                forUrlPath = result3.AbsolutePath;
              }
              else
              {
                string str2 = str1.TrimStart('~');
                forUrlPath = str2.StartsWith("/") ? str2 : "/" + str2;
              }
              string paramValue = SecurityManager.GetUserAuthenticationKey(new TimeSpan?(timeSpan), forUrlPath).UrlEncode();
              if (obj is ILifecycleDataItemGeneric)
                str1 = Url.AppendUrlParameter(str1, "sf-lc-status", (obj as ILifecycleDataItemGeneric).Status.ToString());
              string baseUrl = Url.AppendUrlParameter(str1, "sf-content-action", "preview");
              Guid itemId = Guid.Empty;
              if (this.ShouldAddItemIdParam(obj, out itemId))
                baseUrl = Url.AppendUrlParameter(baseUrl, "sf-itemId", itemId.ToString());
              string url = Url.AppendUrlParameter(baseUrl, "sf-auth", paramValue);
              response.Redirect(url, false);
              context.ApplicationInstance.CompleteRequest();
            }
          }
          else
            this.SetNoContentLocationMessage(response);
        }
      }
    }

    private bool ShouldAddItemIdParam(object item, out Guid itemId)
    {
      if (item is IRecyclableDataItem recyclableDataItem)
      {
        itemId = recyclableDataItem.Id;
        return recyclableDataItem.IsDeleted;
      }
      itemId = Guid.Empty;
      return false;
    }

    public string GetWebResourceUrl(string resource) => new Page().ClientScript.GetWebResourceUrl(TypeResolutionService.ResolveType("Telerik.Sitefinity.Resources.Reference"), resource);

    private void SetNoContentLocationMessage(System.Web.HttpResponse response)
    {
      string s = "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n<html>\r\n<head>\r\n<meta http-equiv=\"content-type\" content=\"text/html; charset=utf-8\" />\r\n<style>\r\nbody,div,dl,dt,dd,ul,ol,li,h1,h2,h3,h4,h5,h6,pre,form,fieldset,input,textarea,p,blockquote,th,td \r\n{  \r\n    margin: 0; \r\n    padding: 0; \r\n} \r\nbody \r\n{\r\n\tfont-family: Arial,Verdana,Sans-serif;\r\n\tfont-size: 12px;\r\n\tline-height: 1.5;\r\n}\r\n.sfMsgWrp\r\n{\r\n    margin: 150px auto 50px;\r\n    width: 380px;\r\n}\r\n.sfMsgWrp .sfNote\r\n{\r\nmargin-bottom: 25px;\r\n    font-size: 14px;\r\n    color: #999;\r\n    text-align: center;\r\n}\r\n.sfNote span\r\n{\r\n    display: block;\r\n    margin: 0 auto 20px;\r\n    width:  67px;\r\n    height: 90px;\r\n    background-color: transparent;  \r\n    background-image: url(" + this.GetWebResourceUrl("Telerik.Sitefinity.Resources.Themes.Light.Images.sfDecisionScreen.gif") + ");\r\n    background-repeat: no-repeat;\r\n    background-position: 0 -350px;\r\n}\r\n.sfInstructionsWrp\r\n{\r\n    background-color: #f2f2f2;\r\n    border: 1px dashed #ccc;\r\n    padding: 15px 20px;\r\n}\r\n.sfInstructionsWrp h1\r\n{\r\n    font-weight: bold;\r\n    font-size: 12px;\r\n}\r\n.sfInstructionsWrp ol\r\n{\r\n    margin-left: 20px;\r\n    margin-top: 5px;\r\n}\r\n</style>\r\n</head>\r\n<body>\r\n<div class=\"sfMsgWrp\">\r\n    <p class=\"sfNote\"><span></span>We can't display Preview<br />because no pages has been set to display content like this</p>\r\n    <div class=\"sfInstructionsWrp\">\r\n        <h1>How to set a page to display content like this?</h1>\r\n        <ol>\r\n            <li>Go to Pages</li>\r\n            <li>In a page, drop a widget displaying content like this</li>\r\n            <li>Publish the page</li>\r\n        </ol>\r\n    </div>\r\n\r\n</div>\r\n</body>\r\n</html>";
      response.Write(s);
    }
  }
}
