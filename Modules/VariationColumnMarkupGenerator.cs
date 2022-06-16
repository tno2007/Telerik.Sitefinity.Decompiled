// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.VariationColumnMarkupGenerator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using Telerik.Microsoft.Practices.Unity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Contracts;

namespace Telerik.Sitefinity.Modules
{
  /// <summary>Variation columns markup generator</summary>
  public class VariationColumnMarkupGenerator : Control, IDynamicMarkupGenerator
  {
    /// <summary>
    /// Initialize properties of the markup generator implementing <see cref="!:IDynamicMarkupGenerator" />
    /// with default values from the configuration definition object.
    /// </summary>
    /// <param name="definition">The definition configuration.</param>
    public virtual void Configure(IDynamicMarkupGeneratorDefinition definition)
    {
    }

    /// <summary>Generates HTML markup for a dynamic column.</summary>
    /// <returns>The generated HTML markup.</returns>
    public virtual string GetMarkup()
    {
      IEnumerable<IPageVariationPlugin> source = ObjectFactory.Container.ResolveAll<IPageVariationPlugin>();
      if (!source.Any<IPageVariationPlugin>())
        return "<div class=\"sfVisibilityHidden\"></div>";
      string markup = string.Empty;
      foreach (IPageVariationPlugin pageVariationPlugin in source)
      {
        IGrantedActions grantedActions = pageVariationPlugin.GetGrantedActions();
        if (grantedActions != null && grantedActions.ViewGranted)
        {
          string key = pageVariationPlugin.Key;
          string str1 = key == "pers" ? " || $dataItem.HasPersonalizedWidgets" : string.Empty;
          string str2 = "jQuery.grep($dataItem.PageVariationViewModels ? $dataItem.PageVariationViewModels : [], function(v) { if(v.Key == '" + key + "') { $dataItem.sfCurrentPageVariation = v.Value; return true; } else { return false; } })";
          markup = markup + "<div sys:class=\"{{ (" + str2 + ".length > 0" + str1 + ")\r\n                            ? 'sfInlineBlock sfMRight5 sfPersonalizedLabel' : 'sfDisplayNone sfPersonalizedLabel' }}\">" + this.GetMarkupInnerContent(key) + "</div>";
        }
      }
      return markup;
    }

    /// <summary>Gets the inner markup content for the column</summary>
    /// <param name="key">The key of the current extender</param>
    /// <returns>The inner content</returns>
    protected virtual string GetMarkupInnerContent(string key) => "{{ typeof ($dataItem.sfCurrentPageVariation) != 'undefined' ? $dataItem.sfCurrentPageVariation.Description : '' }}\r\n                            <span sys:class=\"" + this.GetItemLinkClass(key) + "\">\r\n                                <span class=\"sfSeparatorCircle\"></span>\r\n                                <a sys:href=\"" + this.GetItemLinkTemplate(key) + "\" \r\n                                  target=\"_blank\">\r\n                                    {{ typeof ($dataItem.sfCurrentPageVariation) != 'undefined' ? $dataItem.sfCurrentPageVariation.LinkTitle : '' }}\r\n                                </a>\r\n                            </span>";

    /// <summary>Gets the item link template</summary>
    /// <param name="key">The key of the current extender</param>
    /// <returns>The link template</returns>
    protected virtual string GetItemLinkClass(string key) => "{{ typeof ($dataItem.sfCurrentPageVariation) == 'undefined' || $dataItem.sfCurrentPageVariation.Link == '' ? 'sfDisplayNone' : '' }}";

    /// <summary>Gets the item link template</summary>
    /// <param name="key">The key of the current extender</param>
    /// <returns>The link template</returns>
    protected virtual string GetItemLinkTemplate(string key) => "{{ typeof ($dataItem.sfCurrentPageVariation) != 'undefined' ? $dataItem.sfCurrentPageVariation.Link : '' }}";
  }
}
