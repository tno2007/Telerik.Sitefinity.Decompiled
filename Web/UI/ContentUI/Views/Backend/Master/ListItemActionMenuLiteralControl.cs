// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.ListItemActionMenuLiteralControl
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Contracts;

namespace Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master
{
  /// <summary>Control for rendering action menu</summary>
  public class ListItemActionMenuLiteralControl : Control
  {
    private const string ActionMenuTemplateBegin = "<ul id=\"actions\" class=\"actionsMenu\">";
    private const string ActionMenuTemplateTitleBegin = "<li><a menu=\"actions\" href=\"javascript:void(0);\">{0}</a><ul>";
    private const string ActionMenuTemplateItem = "<li{0}><a sys:href=\"javascript:void(0);\" class=\"sf_binderCommand_{1}{2}\">{3}</a></li>";
    private const string ActionMenuSeparatorColumnTemplateItem = "<li{0} class='sfSeparator'><strong class=\"{1}\">{2}</strong></li>";
    private const string ActionMenuTemplateTitleEnd = "</li></ul>";
    private const string ActionMenuTemplateEnd = "</ul>";
    private const string ActionMenuItemCondition = " sys:if=\"{0}\"";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.ListItemActionMenuLiteralControl" /> class.
    /// </summary>
    public ListItemActionMenuLiteralControl()
      : this((IActionMenuDefinition) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.ListItemActionMenuLiteralControl" /> class.
    /// </summary>
    /// <param name="actionMenu">The action menu.</param>
    public ListItemActionMenuLiteralControl(IActionMenuDefinition actionMenu) => this.ActionMenu = actionMenu;

    /// <summary>
    /// Gets or sets a value indicating whether the action menu is an extension of another menu.
    /// </summary>
    /// <value>True if the menu is an extension, otherwise - false.</value>
    public bool IsPartial { get; set; }

    /// <summary>Gets the label.</summary>
    /// <param name="classId">The class id.</param>
    /// <param name="key">The key.</param>
    /// <returns>The label</returns>
    protected string GetLabel(string classId, string key) => !string.IsNullOrEmpty(classId) ? Res.ResolveLocalizedValue(classId, key) : key;

    /// <summary>
    /// Sends server control content to a provided <see cref="T:System.Web.UI.HtmlTextWriter" />
    /// object, which writes the content to be rendered on the client.
    /// </summary>
    /// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> object
    /// that receives the server control content.</param>
    protected override void Render(HtmlTextWriter writer)
    {
      IActionMenuDefinition actionMenu = this.ActionMenu;
      if (actionMenu == null)
        return;
      List<IWidgetDefinition> list = actionMenu.MenuItems.Where<IWidgetDefinition>((Func<IWidgetDefinition, bool>) (a => SystemManager.IsItemAccessble((object) a))).ToList<IWidgetDefinition>();
      if (!list.Any<IWidgetDefinition>())
        return;
      if (!this.IsPartial)
        writer.Write(string.Format("<ul id=\"actions\" class=\"actionsMenu\">"));
      if (!string.IsNullOrEmpty(actionMenu.MainAction.CommandName))
        this.WriteCommandWidget(writer, actionMenu.MainAction);
      bool flag = false;
      if (actionMenu is IActionMenuColumnDefinition columnDefinition)
      {
        if (!string.IsNullOrEmpty(columnDefinition.TitleText))
          writer.Write(string.Format("<li><a menu=\"actions\" href=\"javascript:void(0);\">{0}</a><ul>", (object) Res.ResolveLocalizedValue(columnDefinition.ResourceClassId, columnDefinition.TitleText)));
        else
          writer.Write(string.Format("<li><a menu=\"actions\" href=\"javascript:void(0);\">{0}</a><ul>", (object) Res.ResolveLocalizedValue(columnDefinition.ResourceClassId, columnDefinition.HeaderText)));
        flag = true;
      }
      foreach (IWidgetDefinition widget in list)
      {
        switch (widget)
        {
          case ICommandWidgetDefinition _:
            this.WriteCommandWidget(writer, (ICommandWidgetDefinition) widget);
            continue;
          case ILiteralWidgetDefinition _:
            this.WriteLiteralWidget(writer, (ILiteralWidgetDefinition) widget);
            continue;
          default:
            continue;
        }
      }
      if (flag)
        writer.Write("</li></ul>");
      if (this.IsPartial)
        return;
      writer.Write("</ul>");
    }

    private void WriteLiteralWidget(HtmlTextWriter writer, ILiteralWidgetDefinition widget)
    {
      string str = string.Empty;
      if (!string.IsNullOrEmpty(widget.CssClass))
        str = widget.CssClass;
      if (!widget.IsSeparator)
        return;
      writer.Write(string.Format("<li{0} class='sfSeparator'><strong class=\"{1}\">{2}</strong></li>", (object) string.Empty, (object) str, (object) this.GetLabel(widget.ResourceClassId, widget.Text)));
    }

    private void WriteCommandWidget(HtmlTextWriter writer, ICommandWidgetDefinition widget)
    {
      string str1 = string.Empty;
      if (!string.IsNullOrEmpty(widget.CssClass))
        str1 = " " + widget.CssClass;
      string str2 = string.Empty;
      if (!widget.Condition.IsNullOrEmpty())
        str2 = string.Format(" sys:if=\"{0}\"", (object) widget.Condition);
      if (!WidgetDefinition.IsWidgetAccessible((IWidgetDefinition) widget))
        return;
      writer.Write(string.Format("<li{0}><a sys:href=\"javascript:void(0);\" class=\"sf_binderCommand_{1}{2}\">{3}</a></li>", (object) str2, (object) widget.CommandName, (object) str1, (object) this.GetLabel(widget.ResourceClassId, widget.Text)));
    }

    /// <summary>Gets or sets the action menu.</summary>
    /// <value>The action menu.</value>
    internal IActionMenuDefinition ActionMenu { get; set; }
  }
}
