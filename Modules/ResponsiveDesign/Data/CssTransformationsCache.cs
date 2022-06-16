// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.ResponsiveDesign.Data.CssTransformationsCache
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Modules.ResponsiveDesign.Configuration;
using Telerik.Sitefinity.Modules.ResponsiveDesign.Web.Services;
using Telerik.Sitefinity.ResponsiveDesign.Model;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Modules.ResponsiveDesign.Data
{
  internal class CssTransformationsCache
  {
    private readonly ResponsiveDesignCache mainCache;
    private readonly Dictionary<Guid, string> cssCache = new Dictionary<Guid, string>();
    private readonly object cssCacheSync = new object();
    private static readonly Regex HiddenCssRegex = new Regex(".sf_([A-Za-z0-9\\-]*)_hide_(\\d{1})+");

    private CssTransformationsCache(ResponsiveDesignCache mainCache) => this.mainCache = mainCache;

    internal static CssTransformationsCache GetInstance()
    {
      ResponsiveDesignCache mainCache = ResponsiveDesignCache.GetInstance();
      return mainCache.GetOrAddSubCache("CssTransformations", (Func<object>) (() => (object) new CssTransformationsCache(mainCache))) as CssTransformationsCache;
    }

    internal string GetCss(PageDataProxy pageData = null) => this.GetCss(this.mainCache.GetMediaQueries(pageData, ResponsiveDesignBehavior.TransformLayout));

    internal string GetCss(Guid pageDataId, Func<IEnumerable<Guid>> templateIdsByPageId) => this.GetCss(this.mainCache.GetMediaQueries(pageDataId, templateIdsByPageId, ResponsiveDesignBehavior.TransformLayout));

    private string GetCss(IEnumerable<IMediaQuery> mediaQueries)
    {
      StringBuilder stringBuilder = new StringBuilder();
      foreach (IMediaQuery mediaQuery in mediaQueries)
        stringBuilder.Append(this.GetCss(mediaQuery));
      return stringBuilder.ToString();
    }

    private string GetCss(IMediaQuery mediaQuery)
    {
      string css = (string) null;
      if (!this.cssCache.TryGetValue(mediaQuery.Id, out css))
      {
        lock (this.cssCacheSync)
        {
          if (!this.cssCache.TryGetValue(mediaQuery.Id, out css))
          {
            css = this.GenerateCss(mediaQuery);
            this.cssCache.Add(mediaQuery.Id, css);
          }
        }
      }
      return css;
    }

    private string GenerateCss(IMediaQuery mediaQuery)
    {
      StringBuilder stringBuilder = new StringBuilder();
      string combinedMediaQueryRule = this.GetCombinedMediaQueryRule(mediaQuery.MediaQueryRules);
      if (!combinedMediaQueryRule.IsNullOrEmpty())
      {
        stringBuilder.Append(combinedMediaQueryRule);
        stringBuilder.Append("{");
        stringBuilder.AppendLine();
      }
      if (!mediaQuery.LayoutTransformationsJSON.IsNullOrEmpty())
      {
        try
        {
          foreach (LayoutTransformationViewModel layoutTransformation in new JavaScriptSerializer().Deserialize<LayoutTransformationViewModel[]>(mediaQuery.LayoutTransformationsJSON))
            stringBuilder.Append(this.ProcessCss(mediaQuery.Name, this.GetLayoutTransformationCss(layoutTransformation)));
        }
        catch (Exception ex)
        {
          Telerik.Sitefinity.Abstractions.Log.Write((object) string.Format("Could not deserialize layout transformations of media query \"{0}\": {1}", (object) mediaQuery.Name, (object) ex), TraceEventType.Error);
        }
      }
      foreach (INavigationTransformation navigationTransformation in mediaQuery.NavigationTransformations)
        stringBuilder.Append(this.GetCssForTransformation(navigationTransformation));
      stringBuilder.Append(this.ProcessCss(mediaQuery.Name, Config.Get<ResponsiveDesignConfig>().HiddenLayoutsCss));
      if (!combinedMediaQueryRule.IsNullOrEmpty())
      {
        stringBuilder.AppendLine();
        stringBuilder.Append("}");
        stringBuilder.AppendLine();
      }
      return stringBuilder.ToString();
    }

    private string GetCombinedMediaQueryRule(IEnumerable<IMediaQueryRule> rules) => rules.Any<IMediaQueryRule>() ? "@media " + string.Join(", ", rules.Select<IMediaQueryRule, string>((Func<IMediaQueryRule, string>) (mqr => !mqr.ResultingRule.StartsWith("@media ") ? mqr.ResultingRule : mqr.ResultingRule.Remove(0, 7)))) : string.Empty;

    private string ProcessCss(string mediaQueryName, string css)
    {
      string fixedName = mediaQueryName.Replace(" ", string.Empty).Trim().ToLowerInvariant();
      return CssTransformationsCache.HiddenCssRegex.Replace(css, (MatchEvaluator) (m => ".sf_" + fixedName + "_" + m.Value.Remove(0, 4)));
    }

    private string GetLayoutTransformationCss(LayoutTransformationViewModel layoutTransformation)
    {
      ConfigElementDictionary<string, OriginalLayoutElement> layoutElements = Config.Get<ResponsiveDesignConfig>().LayoutElements;
      return layoutTransformation.OriginalLayoutElementName == layoutTransformation.AlternatLayoutElementName ? string.Empty : layoutElements.Values.Where<OriginalLayoutElement>((Func<OriginalLayoutElement, bool>) (le => le.Name == layoutTransformation.OriginalLayoutElementName)).Single<OriginalLayoutElement>().AlternateLayouts.Values.Where<LayoutElement>((Func<LayoutElement, bool>) (al => al.Name == layoutTransformation.AlternatLayoutElementName)).Single<LayoutElement>().LayoutCss;
    }

    private string GetCssForTransformation(INavigationTransformation transformation)
    {
      string transformationCss = Config.Get<ResponsiveDesignConfig>().NavigationTransformations[transformation.TransformationName].TransformationCss;
      if (transformation.CssClasses.IsNullOrWhitespace())
        return transformationCss.Replace("{{selector}}", ".sfNavWrp");
      string[] array = ((IEnumerable<string>) transformation.CssClasses.Split(new char[1]
      {
        ','
      }, StringSplitOptions.RemoveEmptyEntries)).Select<string, string>((Func<string, string>) (c => ".sfNavWrp." + c.Trim())).ToArray<string>();
      StringBuilder stringBuilder = new StringBuilder(transformationCss.Length * (array.Length + 1));
      foreach (string newValue in array)
        stringBuilder.Append(transformationCss.Replace("{{selector}}", newValue));
      return stringBuilder.ToString();
    }
  }
}
