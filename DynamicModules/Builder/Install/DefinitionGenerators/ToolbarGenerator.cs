// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.Install.DefinitionGenerators.ToolbarGenerator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web.UI;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data.Configuration;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.DynamicModules.Builder.Web.UI;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Config;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Enums;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets;

namespace Telerik.Sitefinity.DynamicModules.Builder.Install.DefinitionGenerators
{
  /// <summary>
  /// This class generates the toolbar definition for the given dynamic module type.
  /// </summary>
  internal static class ToolbarGenerator
  {
    /// <summary>
    /// Generates the toolbar element for the given dynamic module type and returns it.
    /// </summary>
    /// <param name="moduleType">
    /// The instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleType" /> for which the toolbar ought to be
    /// generated.
    /// </param>
    /// <param name="parentElement">
    /// The parent element of the toolbar element.
    /// </param>
    /// <remarks>
    /// The toolbar element will not be added to the parent element.
    /// </remarks>
    /// <returns>
    /// An instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Backend.Elements.Config.WidgetBarSectionElement" /> representing the toolbar.
    /// </returns>
    public static WidgetBarSectionElement Generate(
      IDynamicModuleType moduleType,
      ConfigElement parentElement)
    {
      if (moduleType == null)
        throw new ArgumentNullException(nameof (moduleType));
      if (parentElement == null)
        throw new ArgumentNullException(nameof (parentElement));
      IndefiniteArticleResolver indefiniteArticleResolver = new IndefiniteArticleResolver();
      indefiniteArticleResolver.ResolveModuleTypeName(moduleType);
      WidgetBarSectionElement barSectionElement = new WidgetBarSectionElement(parentElement)
      {
        Name = "toolbar"
      };
      string str1 = string.Format("{0} {1} {2}", (object) ToolbarGenerator.GetCreateLabel(), (object) indefiniteArticleResolver.Prefix, (object) moduleType.DisplayName);
      ConfigElementList<WidgetElement> items1 = barSectionElement.Items;
      CommandWidgetElement element1 = new CommandWidgetElement((ConfigElement) barSectionElement.Items);
      element1.Name = ModuleNamesGenerator.GenerateWidgetElementName("Create");
      element1.ButtonType = CommandButtonType.Create;
      element1.CommandName = "create";
      element1.CssClass = "sfMainAction";
      element1.Text = str1;
      element1.WidgetType = typeof (CommandWidget);
      element1.PermissionSet = "General";
      element1.ActionName = "Create";
      element1.RelatedSecuredObjectId = moduleType.Id.ToString();
      element1.RelatedSecuredObjectTypeName = typeof (DynamicModuleType).FullName;
      items1.Add((WidgetElement) element1);
      ConfigElementList<WidgetElement> items2 = barSectionElement.Items;
      CommandWidgetElement element2 = new CommandWidgetElement((ConfigElement) barSectionElement.Items);
      element2.Name = ModuleNamesGenerator.GenerateWidgetElementName("Delete");
      element2.ButtonType = CommandButtonType.Standard;
      element2.CommandName = "groupDelete";
      element2.Text = ToolbarGenerator.GetDeleteLabel();
      element2.WidgetType = typeof (CommandWidget);
      element2.PermissionSet = "General";
      element2.ActionName = "Delete";
      element2.RelatedSecuredObjectId = moduleType.Id.ToString();
      element2.RelatedSecuredObjectTypeName = typeof (DynamicModuleType).FullName;
      items2.Add((WidgetElement) element2);
      ActionMenuWidgetElement menuWidgetElement = new ActionMenuWidgetElement((ConfigElement) barSectionElement.Items);
      menuWidgetElement.Name = ModuleNamesGenerator.GenerateWidgetElementName("MoreActions");
      menuWidgetElement.Text = ToolbarGenerator.GetMoreActionsLabel();
      menuWidgetElement.WrapperTagKey = HtmlTextWriterTag.Li;
      menuWidgetElement.WidgetType = typeof (ActionMenuWidget);
      ActionMenuWidgetElement element3 = menuWidgetElement;
      ConfigElementList<WidgetElement> menuItems1 = element3.MenuItems;
      CommandWidgetElement element4 = new CommandWidgetElement((ConfigElement) element3.MenuItems);
      element4.Name = ModuleNamesGenerator.GenerateWidgetElementName("Publish");
      element4.Text = ToolbarGenerator.GetPublishLabel();
      element4.CssClass = "sfPublishItm";
      element4.CommandName = "groupPublish";
      menuItems1.Add((WidgetElement) element4);
      ConfigElementList<WidgetElement> menuItems2 = element3.MenuItems;
      CommandWidgetElement element5 = new CommandWidgetElement((ConfigElement) element3.MenuItems);
      element5.Name = ModuleNamesGenerator.GenerateWidgetElementName("Unpublish");
      element5.Text = ToolbarGenerator.GetUnpublishLabel();
      element5.CssClass = "sfUnpublishItm";
      element5.CommandName = "groupUnpublish";
      menuItems2.Add((WidgetElement) element5);
      barSectionElement.Items.Add((WidgetElement) element3);
      ConfigElementList<WidgetElement> items3 = barSectionElement.Items;
      SearchWidgetElement element6 = new SearchWidgetElement((ConfigElement) barSectionElement.Items);
      element6.Name = ModuleNamesGenerator.GenerateWidgetElementName("Search");
      element6.ButtonType = CommandButtonType.Standard;
      element6.CommandName = "search";
      element6.Text = ToolbarGenerator.GetSearchLabel();
      element6.WidgetType = typeof (SearchWidget);
      items3.Add((WidgetElement) element6);
      DynamicCommandWidgetElement commandWidgetElement = new DynamicCommandWidgetElement((ConfigElement) barSectionElement.Items);
      commandWidgetElement.Name = "EditCustomSorting";
      commandWidgetElement.Text = "Sort";
      commandWidgetElement.BindTo = BindCommandListTo.ComboBox;
      commandWidgetElement.HeaderText = "Sort";
      commandWidgetElement.PageSize = 10;
      commandWidgetElement.WrapperTagKey = HtmlTextWriterTag.Li;
      commandWidgetElement.WidgetType = typeof (SortWidget);
      commandWidgetElement.ResourceClassId = typeof (ModuleBuilderResources).Name;
      commandWidgetElement.CssClass = "sfQuickSort sfNoMasterViews";
      commandWidgetElement.DynamicModuleTypeId = moduleType.Id;
      DynamicCommandWidgetElement element7 = commandWidgetElement;
      barSectionElement.Items.Add((WidgetElement) element7);
      List<SortingExpressionElement> expressionForAdynamicType = ToolbarGenerator.GetSortingExpressionForADynamicType(moduleType, parentElement.Section);
      foreach (SortingExpressionElement expressionElement in expressionForAdynamicType.Where<SortingExpressionElement>((Func<SortingExpressionElement, bool>) (s => !s.IsCustom)))
      {
        string str2 = expressionElement.SortingExpression;
        string title = expressionElement.SortingExpressionTitle;
        if (expressionElement.SortingExpression == "{0} ASC" || expressionElement.SortingExpression == "{0} DESC")
        {
          title = string.Format(expressionElement.SortingExpressionTitle, (object) moduleType.MainShortTextFieldName);
          str2 = string.Format(expressionElement.SortingExpression, (object) moduleType.MainShortTextFieldName);
        }
        DynamicItemElement dynamicItemElement = DefinitionsHelper.CreateDynamicItemElement((ConfigElement) element7.Items, title, str2, (string) null, (NameValueCollection) null);
        element7.Items.Add(dynamicItemElement);
        element7.DesignTimeItems.Add(dynamicItemElement.GetKey());
      }
      foreach (SortingExpressionElement expressionElement in expressionForAdynamicType.Where<SortingExpressionElement>((Func<SortingExpressionElement, bool>) (s => s.IsCustom)))
      {
        DynamicItemElement dynamicItemElement = DefinitionsHelper.CreateDynamicItemElement((ConfigElement) element7.CustomItems, expressionElement.SortingExpressionTitle, expressionElement.SortingExpression, expressionElement.ResourceClassId, (NameValueCollection) null);
        element7.CustomItems.Add(dynamicItemElement);
      }
      return barSectionElement;
    }

    /// <summary>
    /// Adds the type of the sorting expression for a dynamic.
    /// </summary>
    /// <param name="moduleType">Type of the module.</param>
    /// <param name="sortingExpressions">The sorting expressions.</param>
    private static List<SortingExpressionElement> GetSortingExpressionForADynamicType(
      IDynamicModuleType moduleType,
      ConfigSection section)
    {
      List<SortingExpressionElement> expressionForAdynamicType = new List<SortingExpressionElement>();
      string fullTypeName = moduleType.GetFullTypeName();
      SortingExpressionElement expressionElement1 = new SortingExpressionElement((ConfigElement) section);
      expressionElement1.ContentType = fullTypeName;
      expressionElement1.SortingExpressionTitle = "NewModifiedFirst";
      expressionElement1.ResourceClassId = typeof (ModuleBuilderResources).Name;
      expressionElement1.SortingExpression = "LastModified DESC";
      expressionForAdynamicType.Add(expressionElement1);
      SortingExpressionElement expressionElement2 = new SortingExpressionElement((ConfigElement) section);
      expressionElement2.ContentType = fullTypeName;
      expressionElement2.SortingExpressionTitle = "NewCreatedFirst";
      expressionElement2.ResourceClassId = typeof (ModuleBuilderResources).Name;
      expressionElement2.SortingExpression = "DateCreated DESC";
      expressionForAdynamicType.Add(expressionElement2);
      SortingExpressionElement expressionElement3 = new SortingExpressionElement((ConfigElement) section);
      expressionElement3.ContentType = fullTypeName;
      expressionElement3.SortingExpressionTitle = "By {0} (Ascending)";
      expressionElement3.SortingExpression = "{0} ASC";
      expressionForAdynamicType.Add(expressionElement3);
      SortingExpressionElement expressionElement4 = new SortingExpressionElement((ConfigElement) section);
      expressionElement4.ContentType = fullTypeName;
      expressionElement4.SortingExpressionTitle = "By {0} (Descending)";
      expressionElement4.SortingExpression = "{0} DESC";
      expressionForAdynamicType.Add(expressionElement4);
      SortingExpressionElement expressionElement5 = new SortingExpressionElement((ConfigElement) section);
      expressionElement5.ContentType = fullTypeName;
      expressionElement5.SortingExpressionTitle = "CustomSorting";
      expressionElement5.ResourceClassId = typeof (ModuleBuilderResources).Name;
      expressionElement5.SortingExpression = "Custom";
      expressionElement5.IsCustom = true;
      expressionForAdynamicType.Add(expressionElement5);
      return expressionForAdynamicType;
    }

    private static string GetCreateLabel() => Res.Get<Labels>().Create;

    private static string GetDeleteLabel() => Res.Get<ModuleBuilderResources>().Delete;

    private static string GetMoreActionsLabel() => Res.Get<Labels>().MoreActions;

    private static string GetPublishLabel() => Res.Get<Labels>().Publish;

    private static string GetUnpublishLabel() => Res.Get<Labels>().Unpublish;

    private static string GetSearchLabel() => Res.Get<Labels>().Search;
  }
}
