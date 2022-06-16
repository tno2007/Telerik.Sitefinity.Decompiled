// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.ContentWidgetResolver
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services.Statistics;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Modules.Pages
{
  /// <summary>
  /// This class is used to resolve the relation between content and widgets.
  /// </summary>
  internal class ContentWidgetResolver
  {
    private IDictionary<string, ContentWidgetResolver.ContentWidgetProperty> contentWidgetProperties;
    private ISet<string> affectedTypes;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Pages.ContentWidgetResolver" /> class.
    /// </summary>
    public ContentWidgetResolver()
    {
      this.contentWidgetProperties = (IDictionary<string, ContentWidgetResolver.ContentWidgetProperty>) new Dictionary<string, ContentWidgetResolver.ContentWidgetProperty>();
      ContentWidgetResolver.ContentWidgetProperty contentWidgetProperty = new ContentWidgetResolver.ContentWidgetProperty()
      {
        contentType = "Telerik.Sitefinity.GenericContent.Model.ContentItem",
        contentPropertyName = "SharedContentID",
        providerPropertyName = "ProviderName"
      };
      this.contentWidgetProperties["Telerik.Sitefinity.Modules.GenericContent.Web.UI.ContentBlock"] = contentWidgetProperty;
      this.contentWidgetProperties[typeof (IContentItemControl).FullName] = contentWidgetProperty;
      this.affectedTypes = (ISet<string>) new HashSet<string>();
      this.affectedTypes.Add(typeof (PageControl).FullName);
      this.affectedTypes.Add(typeof (PageDraftControl).FullName);
      this.affectedTypes.Add(typeof (TemplateControl).FullName);
      this.affectedTypes.Add(typeof (TemplateDraftControl).FullName);
      this.affectedTypes.Add("Telerik.Sitefinity.GenericContent.Model.ContentItem");
      this.affectedTypes.Add(typeof (PageTemplate).FullName);
      this.affectedTypes.Add(typeof (PageNode).FullName);
      this.affectedTypes.Add(typeof (TemplateDraft).FullName);
    }

    /// <summary>
    /// Determines whether the specified type could be part of a content relation.
    /// </summary>
    /// <param name="type">The fully qualified name of the type.</param>
    /// <returns>
    ///   <c>true</c> if the specified type could be part of a content relation; otherwise, <c>false</c>.
    /// </returns>
    public bool IsContentRelationType(string type) => this.affectedTypes.Contains(type);

    /// <summary>
    /// Determines whether the specified widget is related to content.
    /// </summary>
    /// <param name="widget">The widget.</param>
    /// <returns>
    ///   <c>true</c> if the widget is related to content; otherwise, <c>false</c>.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">widget</exception>
    public bool IsContentWidget(ControlData widget)
    {
      string objectType = widget != null ? ObjectFactory.Resolve<IControlBehaviorResolver>().GetBehaviorObjectType(widget) : throw new ArgumentNullException(nameof (widget));
      if (objectType.IsNullOrEmpty())
        return false;
      return this.contentWidgetProperties.Keys.Contains(objectType) || this.IsContentBlockType(objectType);
    }

    private bool IsContentBlockType(string objectType)
    {
      if (objectType.IsNullOrEmpty())
        return false;
      Type c = TypeResolutionService.ResolveType(objectType, false);
      return c != (Type) null && typeof (IContentItemControl).IsAssignableFrom(c);
    }

    /// <summary>
    /// Resolves the content relationship of the specified widget.
    /// </summary>
    /// <param name="widget">The widget.</param>
    /// <param name="contentId">Related content id.</param>
    /// <param name="contentType">Type of the content.</param>
    /// <param name="providerName">Name of the content provider.</param>
    /// <returns>
    ///   <c>true</c> if the widget is related to content; otherwise, <c>false</c>.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">widget</exception>
    private bool Resolve(
      ControlData widget,
      out Guid contentId,
      out string contentType,
      out string providerName)
    {
      if (widget == null)
        throw new ArgumentNullException(nameof (widget));
      contentId = Guid.Empty;
      contentType = (string) null;
      providerName = (string) null;
      IControlBehaviorResolver behaviorResolver = ObjectFactory.Resolve<IControlBehaviorResolver>();
      string str = behaviorResolver.GetBehaviorObjectType(widget);
      if (this.IsContentBlockType(str))
        str = typeof (IContentItemControl).FullName;
      ContentWidgetResolver.ContentWidgetProperty propParams;
      if (!this.contentWidgetProperties.TryGetValue(str, out propParams))
        return false;
      IEnumerable<ControlProperty> persistedProperties = behaviorResolver.GetPersistedProperties(widget);
      if (persistedProperties != null)
      {
        ControlProperty controlProperty1 = persistedProperties.FirstOrDefault<ControlProperty>((Func<ControlProperty, bool>) (p => p.Name == propParams.contentPropertyName && p.Value != Guid.Empty.ToString()));
        if (controlProperty1 != null && Guid.TryParse(controlProperty1.Value, out contentId) && !(contentId == Guid.Empty))
        {
          contentType = propParams.contentType;
          ControlProperty controlProperty2 = persistedProperties.FirstOrDefault<ControlProperty>((Func<ControlProperty, bool>) (p => p.Name == propParams.providerPropertyName));
          providerName = controlProperty2?.Value;
          return true;
        }
      }
      return false;
    }

    /// <summary>Gets the relation or null.</summary>
    /// <param name="widget">The widget.</param>
    /// <returns>A content relation record or null if there is no content relation in the specified widget</returns>
    /// <exception cref="T:System.ArgumentNullException">widget</exception>
    public IContentRelation GetRelationOrNull(ControlData widget)
    {
      if (widget == null)
        throw new ArgumentNullException(nameof (widget));
      try
      {
        Guid contentId;
        string contentType;
        string providerName;
        if (!this.Resolve(widget, out contentId, out contentType, out providerName))
          return (IContentRelation) null;
        Guid guid;
        string fullName;
        switch (widget)
        {
          case PageControl _:
            PageControl pageControl = (PageControl) widget;
            if (pageControl.Page == null || pageControl.Page.NavigationNode == null)
              return (IContentRelation) null;
            guid = ((PageControl) widget).Page.Id;
            fullName = typeof (PageData).FullName;
            break;
          case PageDraftControl _:
            PageDraftControl pageDraftControl = (PageDraftControl) widget;
            if (pageDraftControl.Page == null || pageDraftControl.Page.IsTempDraft || pageDraftControl.Page.ParentPage == null || pageDraftControl.Page.ParentPage.NavigationNode == null)
              return (IContentRelation) null;
            guid = pageDraftControl.Page.ParentId;
            fullName = typeof (PageData).FullName;
            break;
          case TemplateControl _:
            TemplateControl templateControl = (TemplateControl) widget;
            if (templateControl.Page == null)
              return (IContentRelation) null;
            guid = templateControl.Page.Id;
            fullName = typeof (PageTemplate).FullName;
            break;
          case TemplateDraftControl _:
            TemplateDraftControl templateDraftControl = (TemplateDraftControl) widget;
            if (templateDraftControl.Page == null)
              return (IContentRelation) null;
            guid = templateDraftControl.Page.Id;
            fullName = typeof (TemplateDraft).FullName;
            break;
          default:
            return (IContentRelation) null;
        }
        return (IContentRelation) new ContentRelationProxy()
        {
          Key = widget.Id,
          ObjectId = guid,
          ObjectType = fullName,
          RelationType = "Contains",
          SubjectId = contentId,
          SubjectType = contentType,
          SubjectProvider = providerName
        };
      }
      catch
      {
        return (IContentRelation) null;
      }
    }

    private class ContentWidgetProperty
    {
      public string contentPropertyName;
      public string providerPropertyName;
      public string contentType;
    }
  }
}
