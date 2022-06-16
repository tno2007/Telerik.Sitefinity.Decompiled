// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.DialogRoute
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Routing;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Web
{
  /// <summary>
  /// Represents route for all the dialogs inside of Sitefinity.
  /// </summary>
  public class DialogRoute : RouteBase
  {
    private DialogRouteHandler dialogRouteHandler;
    private SpellCheckRouteHandler spellCheckHandler;
    private string dialogUrlPath = "Sitefinity" + "/Dialog/";
    internal const string SpellCheckHanlderFullName = "Telerik.Web.UI.SpellCheckHandler.axd";

    /// <summary>
    /// Gets the route data for all sitefinity dialogs.
    /// Dialog url path should look like 'Sitefinity/Dialog/{Name}/{*Params}'
    /// </summary>
    /// <param name="httpContext">The HTTP context.</param>
    /// <returns></returns>
    public override RouteData GetRouteData(HttpContextBase httpContext)
    {
      string str1 = httpContext.Request.AppRelativeCurrentExecutionFilePath.Substring(2) + httpContext.Request.PathInfo;
      if (str1.StartsWith(this.DialogUrlPath, StringComparison.OrdinalIgnoreCase))
      {
        RouteHelper.ApplyThreadCulturesForCurrentUser();
        IList<string> pathSegmentStrings = RouteHelper.SplitUrlToPathSegmentStrings(str1.Substring(this.DialogUrlPath.Length), true);
        string empty = string.Empty;
        string str2;
        switch (pathSegmentStrings.Count)
        {
          case 1:
            str2 = pathSegmentStrings[0];
            break;
          case 2:
            empty = pathSegmentStrings[1];
            str2 = pathSegmentStrings[0];
            break;
          default:
            return (RouteData) null;
        }
        if (str2 != null)
        {
          if ("Telerik.Web.UI.SpellCheckHandler.axd".Equals(!empty.IsNullOrEmpty() ? empty : str2, StringComparison.OrdinalIgnoreCase))
            return this.GetHandler(str2, empty, true);
          return ObjectFactory.IsTypeRegistered<DialogBase>(str2) ? this.GetHandler(str2, empty, false) : (RouteData) null;
        }
      }
      else if (str1.Contains("Telerik.Web.UI.SpellCheckHandler.axd"))
        return this.GetHandler(string.Empty, string.Empty, true);
      return (RouteData) null;
    }

    public override VirtualPathData GetVirtualPath(
      RequestContext requestContext,
      RouteValueDictionary values)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// Gets the handler that is going to process the dialog routes.
    /// </summary>
    /// <param name="dialogName">Name of the dialog.</param>
    /// <param name="parameters">The parameters.</param>
    /// <param name="useSpellCheckHandler">if set to <c>true</c> route data will be instanciated with an instance of <see cref="T:Telerik.Sitefinity.Web.SpellCheckRouteHandler" /> otherwise  .</param>
    /// <returns></returns>
    private RouteData GetHandler(
      string dialogName,
      string parameters,
      bool useSpellCheckHandler)
    {
      IRouteHandler routeHandler;
      if (!useSpellCheckHandler)
      {
        if (this.dialogRouteHandler == null)
          this.dialogRouteHandler = ObjectFactory.Resolve<DialogRouteHandler>();
        routeHandler = (IRouteHandler) this.dialogRouteHandler;
      }
      else
      {
        if (this.spellCheckHandler == null)
          this.spellCheckHandler = ObjectFactory.Resolve<SpellCheckRouteHandler>();
        routeHandler = (IRouteHandler) this.spellCheckHandler;
      }
      return new RouteData((RouteBase) this, routeHandler)
      {
        Values = {
          {
            "Name",
            (object) dialogName
          },
          {
            "Params",
            (object) parameters
          }
        }
      };
    }

    /// <summary>Gets or sets the dialog URL path.</summary>
    /// <value>The dialog URL path.</value>
    protected string DialogUrlPath
    {
      get => this.dialogUrlPath;
      set => this.dialogUrlPath = value;
    }
  }
}
