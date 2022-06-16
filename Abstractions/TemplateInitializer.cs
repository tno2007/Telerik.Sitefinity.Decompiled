// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Abstractions.TemplateInitializer
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Backend;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Abstractions
{
  /// <summary>
  /// Helper class for initial creation and restore of page templates.
  /// </summary>
  public class TemplateInitializer
  {
    private Type assemblyInfo = Config.Get<ControlsConfig>().ResourcesAssemblyInfo;
    private Hashtable methods;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Abstractions.TemplateInitializer" /> class.
    /// </summary>
    /// <param name="pageManager">The page manager.</param>
    public TemplateInitializer(PageManager pageManager)
    {
      this.PageManager = pageManager;
      this.InitializeBasicTemplates();
    }

    /// <summary>
    /// Invokes all methods registered with hash table containing methods for templates creation.
    /// </summary>
    /// <param name="node">The node.</param>
    public void InvokeAllMethods(HierarchicalTaxon node)
    {
      this.InvokeMethod(SiteInitializer.TemplateId1ColumnHeaderFooter, node, (short) 0);
      this.InvokeMethod(SiteInitializer.TemplateIdLeftbarHeaderFooter, node, (short) 1);
      this.InvokeMethod(SiteInitializer.TemplateIdRightSidebarHeaderFooter, node, (short) 2);
      this.InvokeMethod(SiteInitializer.TemplateIdLeftSideBar, node, (short) 3);
      this.InvokeMethod(SiteInitializer.TemplateIdRightSideBar, node, (short) 4);
      this.InvokeMethod(SiteInitializer.TemplateId2EqualHeaderFooter, node, (short) 5);
      this.InvokeMethod(SiteInitializer.TemplateId3EqualHeaderFooter, node, (short) 6);
      this.InvokeMethod(SiteInitializer.TemplateId2Sidebars, node, (short) 7);
      this.InvokeMethod(SiteInitializer.TemplateIdPromo3ColumnsHeaderFooter, node, (short) 8);
      this.InvokeMethod(SiteInitializer.DefaultBackendTemplateEmptyId, node, (short) 1);
      this.InvokeMethod(SiteInitializer.BackendHtml5TemplateId, node, (short) 2);
    }

    /// <summary>Invokes the method.</summary>
    /// <param name="key">The key.</param>
    /// <param name="node">The node.</param>
    /// <param name="ordinal">The ordinal.</param>
    /// <param name="restoreToDefaultTemplate">if set to <c>true</c> [restore to default template].</param>
    public void InvokeMethod(
      Guid key,
      HierarchicalTaxon node,
      short ordinal,
      bool restoreToDefaultTemplate = false)
    {
      if (this.PageManager == null)
        throw new ArgumentNullException("PageManager");
      if (!this.IsDefaultTemplate(key))
        return;
      ((TemplateInitializer.MethodToInvoke) this.methods[(object) key])(node, ordinal, restoreToDefaultTemplate);
    }

    /// <summary>
    /// Determines whether the template with the specified key is default template created upon site install.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <returns>
    /// 	<c>true</c> if [is default template] [the specified key]; otherwise, <c>false</c>.
    /// </returns>
    public bool IsDefaultTemplate(Guid key) => this.methods.ContainsKey((object) key) || key == SiteInitializer.WebFormsEmptyTemplateId || key == SiteInitializer.HybridEmptyTemplateId;

    /// <summary>Initializes the basic templates.</summary>
    private void InitializeBasicTemplates()
    {
      this.methods = new Hashtable();
      this.methods.Add((object) SiteInitializer.TemplateId1ColumnHeaderFooter, (object) new TemplateInitializer.MethodToInvoke(this.Create1ColumnHeaderFooter));
      this.methods.Add((object) SiteInitializer.TemplateIdLeftbarHeaderFooter, (object) new TemplateInitializer.MethodToInvoke(this.CreateLeftbarHeaderFooter));
      this.methods.Add((object) SiteInitializer.TemplateIdRightSidebarHeaderFooter, (object) new TemplateInitializer.MethodToInvoke(this.CreateRightbarHeaderFooter));
      this.methods.Add((object) SiteInitializer.TemplateIdLeftSideBar, (object) new TemplateInitializer.MethodToInvoke(this.CreateLeftSidebar));
      this.methods.Add((object) SiteInitializer.TemplateIdRightSideBar, (object) new TemplateInitializer.MethodToInvoke(this.CreateRightSidebar));
      this.methods.Add((object) SiteInitializer.TemplateId2EqualHeaderFooter, (object) new TemplateInitializer.MethodToInvoke(this.Create2EqualHeaderFooter));
      this.methods.Add((object) SiteInitializer.TemplateId3EqualHeaderFooter, (object) new TemplateInitializer.MethodToInvoke(this.Create3EqualHeaderFooter));
      this.methods.Add((object) SiteInitializer.TemplateId2Sidebars, (object) new TemplateInitializer.MethodToInvoke(this.Create2Sidebars));
      this.methods.Add((object) SiteInitializer.TemplateIdPromo3ColumnsHeaderFooter, (object) new TemplateInitializer.MethodToInvoke(this.CreatePromo3ColumnsHeaderFooter));
      this.methods.Add((object) SiteInitializer.DefaultBackendTemplateId, (object) new TemplateInitializer.MethodToInvoke(this.CreateBackendDefaultTemplate));
      this.methods.Add((object) SiteInitializer.BackendHtml5TemplateId, (object) new TemplateInitializer.MethodToInvoke(this.CreateBackendHtml5Template));
      this.methods.Add((object) SiteInitializer.DefaultBackendTemplateEmptyId, (object) new TemplateInitializer.MethodToInvoke(this.CreateBackendTemplateEmpty));
    }

    /// <summary>Gets the template.</summary>
    /// <param name="id">The id.</param>
    /// <param name="restoreToDefaultTemplate">The restore to default template.</param>
    /// <returns></returns>
    protected virtual PageTemplate GetTemplate(Guid id, bool restoreToDefaultTemplate)
    {
      PageTemplate template;
      if (restoreToDefaultTemplate)
      {
        template = this.PageManager.GetTemplate(id);
        foreach (ControlData controlData in new List<TemplateControl>((IEnumerable<TemplateControl>) template.Controls))
          this.PageManager.Delete(controlData);
        foreach (PresentationData presentationData in new List<TemplatePresentation>((IEnumerable<TemplatePresentation>) template.Presentation))
          this.PageManager.Delete(presentationData);
        template.Presentation.Clear();
        template.Controls.Clear();
        template.LastControlId = 0;
        template.ParentTemplate = (PageTemplate) null;
        template.Drafts.ToList<TemplateDraft>().ForEach((Action<TemplateDraft>) (td => this.PageManager.Delete(td)));
        this.PageManager.Provider.FlushTransaction();
      }
      else
        template = this.PageManager.CreateTemplate(id);
      return template;
    }

    /// <summary>Create1s the column header footer.</summary>
    /// <param name="node">The node.</param>
    /// <param name="ordinal">The ordinal.</param>
    /// <param name="restoreToDefaultTemplate">The restore to default template.</param>
    protected virtual void Create1ColumnHeaderFooter(
      HierarchicalTaxon node,
      short ordinal,
      bool restoreToDefaultTemplate = false)
    {
      PageTemplate template = this.GetTemplate(SiteInitializer.TemplateId1ColumnHeaderFooter, restoreToDefaultTemplate);
      template.Name = "OneColumnHeaderFooter";
      Res.SetLstring(template.Title, typeof (PageResources), "OneColumnHeaderFooter");
      template.Category = node.Id;
      template.Ordinal = ordinal;
      TemplatePresentation presentationItem1 = this.PageManager.CreatePresentationItem<TemplatePresentation>();
      presentationItem1.DataType = "HTML_DOCUMENT";
      presentationItem1.Name = "master";
      string fileName = "Telerik.Sitefinity.Resources.Pages.Frontend.aspx";
      presentationItem1.Data = ControlUtilities.GetTextResource(fileName, this.assemblyInfo);
      template.Presentation.Add(presentationItem1);
      TemplatePresentation presentationItem2 = this.PageManager.CreatePresentationItem<TemplatePresentation>();
      presentationItem2.DataType = "IMAGE_URL";
      presentationItem2.Name = "icon";
      presentationItem2.Theme = "Default";
      presentationItem2.Data = "Telerik.Sitefinity.Resources.Themes.Light.Images.PageTemplates.1ColumnHeaderFooter.gif";
      template.Presentation.Add(presentationItem2);
      Guid empty = Guid.Empty;
      LayoutControl component1 = new LayoutControl();
      component1.Layout = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Layouts.Labeled.Column1TemplateHeader.ascx");
      TemplateControl control1 = this.PageManager.CreateControl<TemplateControl>(false);
      control1.ObjectType = component1.GetType().FullName;
      control1.PlaceHolder = "Body";
      control1.SiblingId = empty;
      Guid id1 = control1.Id;
      control1.Caption = "Header";
      control1.Description = "Represents the header of the template.";
      this.PageManager.ReadProperties((object) component1, (ObjectData) control1);
      this.PageManager.SetControlId((IControlsContainer) template, (ObjectData) control1, (CultureInfo) null);
      control1.SetDefaultPermissions((IControlManager) this.PageManager);
      template.Controls.Add(control1);
      LayoutControl component2 = new LayoutControl();
      component2.Layout = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Layouts.Labeled.Column1Template.ascx");
      TemplateControl control2 = this.PageManager.CreateControl<TemplateControl>(false);
      control2.ObjectType = component2.GetType().FullName;
      control2.PlaceHolder = "Body";
      control2.SiblingId = id1;
      Guid id2 = control2.Id;
      control2.Caption = "Content";
      control2.Description = "Represents the content area of the template.";
      this.PageManager.ReadProperties((object) component2, (ObjectData) control2);
      this.PageManager.SetControlId((IControlsContainer) template, (ObjectData) control2, (CultureInfo) null);
      control2.SetDefaultPermissions((IControlManager) this.PageManager);
      template.Controls.Add(control2);
      LayoutControl component3 = new LayoutControl();
      component3.Layout = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Layouts.Labeled.Column1TemplateFooter.ascx");
      TemplateControl control3 = this.PageManager.CreateControl<TemplateControl>(false);
      control3.ObjectType = component3.GetType().FullName;
      control3.PlaceHolder = "Body";
      control3.SiblingId = id2;
      Guid id3 = control3.Id;
      control3.Caption = "Footer";
      control3.Description = "Represents the footer of the template.";
      this.PageManager.ReadProperties((object) component3, (ObjectData) control3);
      this.PageManager.SetControlId((IControlsContainer) template, (ObjectData) control3, (CultureInfo) null);
      control3.SetDefaultPermissions((IControlManager) this.PageManager);
      template.Controls.Add(control3);
      this.CreateTemplateInvariantLanguageData(template);
    }

    /// <summary>Creates the left sidebar, header and footer template.</summary>
    /// <param name="node">The node.</param>
    /// <param name="ordinal">The ordinal.</param>
    /// <param name="restoreToDefaultTemplate">The restore to default template.</param>
    protected virtual void CreateLeftbarHeaderFooter(
      HierarchicalTaxon node,
      short ordinal,
      bool restoreToDefaultTemplate = false)
    {
      PageTemplate template = this.GetTemplate(SiteInitializer.TemplateIdLeftbarHeaderFooter, restoreToDefaultTemplate);
      template.Name = "LeftSidebarHeaderFooter";
      Res.SetLstring(template.Title, typeof (PageResources), "LeftSidebarHeaderFooter");
      template.Category = node.Id;
      template.Ordinal = ordinal;
      TemplatePresentation presentationItem1 = this.PageManager.CreatePresentationItem<TemplatePresentation>();
      presentationItem1.DataType = "HTML_DOCUMENT";
      presentationItem1.Name = "master";
      string fileName = "Telerik.Sitefinity.Resources.Pages.Frontend.aspx";
      presentationItem1.Data = ControlUtilities.GetTextResource(fileName, this.assemblyInfo);
      template.Presentation.Add(presentationItem1);
      TemplatePresentation presentationItem2 = this.PageManager.CreatePresentationItem<TemplatePresentation>();
      presentationItem2.DataType = "IMAGE_URL";
      presentationItem2.Name = "icon";
      presentationItem2.Theme = "Default";
      presentationItem2.Data = "Telerik.Sitefinity.Resources.Themes.Light.Images.PageTemplates.LeftSidebarHeaderFooter.gif";
      template.Presentation.Add(presentationItem2);
      Guid empty = Guid.Empty;
      LayoutControl component1 = new LayoutControl();
      component1.Layout = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Layouts.Labeled.Column1TemplateHeader.ascx");
      TemplateControl control1 = this.PageManager.CreateControl<TemplateControl>(false);
      control1.ObjectType = component1.GetType().FullName;
      control1.PlaceHolder = "Body";
      control1.SiblingId = empty;
      Guid id1 = control1.Id;
      control1.Caption = "Header";
      control1.Description = "Represents the header of the template.";
      this.PageManager.ReadProperties((object) component1, (ObjectData) control1);
      this.PageManager.SetControlId((IControlsContainer) template, (ObjectData) control1, (CultureInfo) null);
      control1.SetDefaultPermissions((IControlManager) this.PageManager);
      template.Controls.Add(control1);
      LayoutControl component2 = new LayoutControl();
      component2.Layout = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Layouts.Labeled.Column2Template1.ascx");
      TemplateControl control2 = this.PageManager.CreateControl<TemplateControl>(false);
      control2.ObjectType = component2.GetType().FullName;
      control2.PlaceHolder = "Body";
      control2.SiblingId = id1;
      Guid id2 = control2.Id;
      control2.Caption = "Sidebar + Content";
      control2.Description = "Represents the sidebar and content areas of the template.";
      this.PageManager.ReadProperties((object) component2, (ObjectData) control2);
      this.PageManager.SetControlId((IControlsContainer) template, (ObjectData) control2, (CultureInfo) null);
      control2.SetDefaultPermissions((IControlManager) this.PageManager);
      template.Controls.Add(control2);
      LayoutControl component3 = new LayoutControl();
      component3.Layout = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Layouts.Labeled.Column1TemplateFooter.ascx");
      TemplateControl control3 = this.PageManager.CreateControl<TemplateControl>(false);
      control3.ObjectType = component3.GetType().FullName;
      control3.PlaceHolder = "Body";
      control3.SiblingId = id2;
      Guid id3 = control3.Id;
      control3.Caption = "Footer";
      control3.Description = "Represents the footer of the template.";
      this.PageManager.ReadProperties((object) component3, (ObjectData) control3);
      this.PageManager.SetControlId((IControlsContainer) template, (ObjectData) control3, (CultureInfo) null);
      control3.SetDefaultPermissions((IControlManager) this.PageManager);
      template.Controls.Add(control3);
      this.CreateTemplateInvariantLanguageData(template);
    }

    /// <summary>
    /// Creates the right sidebar, header and footer template.
    /// </summary>
    /// <param name="node">The node.</param>
    /// <param name="ordinal">The ordinal.</param>
    /// <param name="restoreToDefaultTemplate">The restore to default template.</param>
    protected virtual void CreateRightbarHeaderFooter(
      HierarchicalTaxon node,
      short ordinal,
      bool restoreToDefaultTemplate = false)
    {
      PageTemplate template = this.GetTemplate(SiteInitializer.TemplateIdRightSidebarHeaderFooter, restoreToDefaultTemplate);
      template.Name = "RightSidebarHeaderFooter";
      Res.SetLstring(template.Title, typeof (PageResources), "RightSidebarHeaderFooter");
      template.Ordinal = ordinal;
      template.Category = node.Id;
      TemplatePresentation presentationItem1 = this.PageManager.CreatePresentationItem<TemplatePresentation>();
      presentationItem1.DataType = "HTML_DOCUMENT";
      presentationItem1.Name = "master";
      string fileName = "Telerik.Sitefinity.Resources.Pages.Frontend.aspx";
      presentationItem1.Data = ControlUtilities.GetTextResource(fileName, this.assemblyInfo);
      template.Presentation.Add(presentationItem1);
      TemplatePresentation presentationItem2 = this.PageManager.CreatePresentationItem<TemplatePresentation>();
      presentationItem2.DataType = "IMAGE_URL";
      presentationItem2.Name = "icon";
      presentationItem2.Theme = "Default";
      presentationItem2.Data = "Telerik.Sitefinity.Resources.Themes.Light.Images.PageTemplates.RightSidebarHeaderFooter.gif";
      template.Presentation.Add(presentationItem2);
      Guid empty = Guid.Empty;
      LayoutControl component1 = new LayoutControl();
      component1.Layout = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Layouts.Labeled.Column1TemplateHeader.ascx");
      TemplateControl control1 = this.PageManager.CreateControl<TemplateControl>(false);
      control1.ObjectType = component1.GetType().FullName;
      control1.PlaceHolder = "Body";
      control1.SiblingId = empty;
      Guid id1 = control1.Id;
      control1.Caption = "Header";
      control1.Description = "Represents the header of the template.";
      this.PageManager.ReadProperties((object) component1, (ObjectData) control1);
      this.PageManager.SetControlId((IControlsContainer) template, (ObjectData) control1, (CultureInfo) null);
      control1.SetDefaultPermissions((IControlManager) this.PageManager);
      template.Controls.Add(control1);
      LayoutControl component2 = new LayoutControl();
      component2.Layout = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Layouts.Labeled.Column2Template5.ascx");
      TemplateControl control2 = this.PageManager.CreateControl<TemplateControl>(false);
      control2.ObjectType = component2.GetType().FullName;
      control2.PlaceHolder = "Body";
      control2.SiblingId = id1;
      Guid id2 = control2.Id;
      control2.Caption = "Sidebar + Content";
      control2.Description = "Represents the sidebar and content areas of the template.";
      this.PageManager.ReadProperties((object) component2, (ObjectData) control2);
      this.PageManager.SetControlId((IControlsContainer) template, (ObjectData) control2, (CultureInfo) null);
      control2.SetDefaultPermissions((IControlManager) this.PageManager);
      template.Controls.Add(control2);
      LayoutControl component3 = new LayoutControl();
      component3.Layout = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Layouts.Labeled.Column1TemplateFooter.ascx");
      TemplateControl control3 = this.PageManager.CreateControl<TemplateControl>(false);
      control3.ObjectType = component3.GetType().FullName;
      control3.PlaceHolder = "Body";
      control3.SiblingId = id2;
      Guid id3 = control3.Id;
      control3.Caption = "Footer";
      control3.Description = "Represents the footer of the template.";
      this.PageManager.ReadProperties((object) component3, (ObjectData) control3);
      this.PageManager.SetControlId((IControlsContainer) template, (ObjectData) control3, (CultureInfo) null);
      control3.SetDefaultPermissions((IControlManager) this.PageManager);
      template.Controls.Add(control3);
      this.CreateTemplateInvariantLanguageData(template);
    }

    /// <summary>Creates the left sidebar template.</summary>
    /// <param name="node">The node.</param>
    /// <param name="ordinal">The ordinal.</param>
    /// <param name="restoreToDefaultTemplate">The restore to default template.</param>
    protected virtual void CreateLeftSidebar(
      HierarchicalTaxon node,
      short ordinal,
      bool restoreToDefaultTemplate = false)
    {
      PageTemplate template = this.GetTemplate(SiteInitializer.TemplateIdLeftSideBar, restoreToDefaultTemplate);
      template.Name = "LeftSidebar";
      Res.SetLstring(template.Title, typeof (PageResources), "LeftSidebar");
      template.Category = node.Id;
      template.Ordinal = ordinal;
      TemplatePresentation presentationItem1 = this.PageManager.CreatePresentationItem<TemplatePresentation>();
      presentationItem1.DataType = "HTML_DOCUMENT";
      presentationItem1.Name = "master";
      string fileName = "Telerik.Sitefinity.Resources.Pages.Frontend.aspx";
      presentationItem1.Data = ControlUtilities.GetTextResource(fileName, this.assemblyInfo);
      template.Presentation.Add(presentationItem1);
      TemplatePresentation presentationItem2 = this.PageManager.CreatePresentationItem<TemplatePresentation>();
      presentationItem2.DataType = "IMAGE_URL";
      presentationItem2.Name = "icon";
      presentationItem2.Theme = "Default";
      presentationItem2.Data = "Telerik.Sitefinity.Resources.Themes.Light.Images.PageTemplates.LeftSidebarContent.gif";
      template.Presentation.Add(presentationItem2);
      LayoutControl component = new LayoutControl();
      component.Layout = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Layouts.Labeled.Column2Template1.ascx");
      TemplateControl control = this.PageManager.CreateControl<TemplateControl>(false);
      control.ObjectType = component.GetType().FullName;
      control.PlaceHolder = "Body";
      control.Caption = "Sidebar + Content";
      control.Description = "Represents the sidebar and content areas of the template.";
      this.PageManager.ReadProperties((object) component, (ObjectData) control);
      this.PageManager.SetControlId((IControlsContainer) template, (ObjectData) control, (CultureInfo) null);
      control.SetDefaultPermissions((IControlManager) this.PageManager);
      template.Controls.Add(control);
      this.CreateTemplateInvariantLanguageData(template);
    }

    /// <summary>Creates the right sidebar template.</summary>
    /// <param name="node">The node.</param>
    /// <param name="ordinal">The ordinal.</param>
    /// <param name="restoreToDefaultTemplate">The restore to default template.</param>
    protected virtual void CreateRightSidebar(
      HierarchicalTaxon node,
      short ordinal,
      bool restoreToDefaultTemplate = false)
    {
      PageTemplate template = this.GetTemplate(SiteInitializer.TemplateIdRightSideBar, restoreToDefaultTemplate);
      template.Name = "RightSidebar";
      Res.SetLstring(template.Title, typeof (PageResources), "RightSidebar");
      template.Category = node.Id;
      template.Ordinal = ordinal;
      TemplatePresentation presentationItem1 = this.PageManager.CreatePresentationItem<TemplatePresentation>();
      presentationItem1.DataType = "HTML_DOCUMENT";
      presentationItem1.Name = "master";
      string fileName = "Telerik.Sitefinity.Resources.Pages.Frontend.aspx";
      presentationItem1.Data = ControlUtilities.GetTextResource(fileName, this.assemblyInfo);
      template.Presentation.Add(presentationItem1);
      TemplatePresentation presentationItem2 = this.PageManager.CreatePresentationItem<TemplatePresentation>();
      presentationItem2.DataType = "IMAGE_URL";
      presentationItem2.Name = "icon";
      presentationItem2.Theme = "Default";
      presentationItem2.Data = "Telerik.Sitefinity.Resources.Themes.Light.Images.PageTemplates.RidhtSidebarContent.gif";
      template.Presentation.Add(presentationItem2);
      LayoutControl component = new LayoutControl();
      component.Layout = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Layouts.Labeled.Column2Template5.ascx");
      TemplateControl control = this.PageManager.CreateControl<TemplateControl>(false);
      control.ObjectType = component.GetType().FullName;
      control.PlaceHolder = "Body";
      control.Caption = "Sidebar + Content";
      control.Description = "Represents the sidebar and content areas of the template.";
      this.PageManager.ReadProperties((object) component, (ObjectData) control);
      this.PageManager.SetControlId((IControlsContainer) template, (ObjectData) control, (CultureInfo) null);
      control.SetDefaultPermissions((IControlManager) this.PageManager);
      template.Controls.Add(control);
      this.CreateTemplateInvariantLanguageData(template);
    }

    /// <summary>
    /// Creates the 2 equal columns, header and footer template.
    /// </summary>
    /// <param name="node">The node.</param>
    /// <param name="ordinal">The ordinal.</param>
    /// <param name="restoreToDefaultTemplate">The restore to default template.</param>
    protected virtual void Create2EqualHeaderFooter(
      HierarchicalTaxon node,
      short ordinal,
      bool restoreToDefaultTemplate = false)
    {
      PageTemplate template = this.GetTemplate(SiteInitializer.TemplateId2EqualHeaderFooter, restoreToDefaultTemplate);
      template.Name = "TwoEqualHeaderFooter";
      Res.SetLstring(template.Title, typeof (PageResources), "TwoEqualHeaderFooter");
      template.Category = node.Id;
      template.Ordinal = ordinal;
      TemplatePresentation presentationItem1 = this.PageManager.CreatePresentationItem<TemplatePresentation>();
      presentationItem1.DataType = "HTML_DOCUMENT";
      presentationItem1.Name = "master";
      string fileName = "Telerik.Sitefinity.Resources.Pages.Frontend.aspx";
      presentationItem1.Data = ControlUtilities.GetTextResource(fileName, this.assemblyInfo);
      template.Presentation.Add(presentationItem1);
      TemplatePresentation presentationItem2 = this.PageManager.CreatePresentationItem<TemplatePresentation>();
      presentationItem2.DataType = "IMAGE_URL";
      presentationItem2.Name = "icon";
      presentationItem2.Theme = "Default";
      presentationItem2.Data = "Telerik.Sitefinity.Resources.Themes.Light.Images.PageTemplates.2EqualColumnsHeaderFooter.gif";
      template.Presentation.Add(presentationItem2);
      Guid empty = Guid.Empty;
      LayoutControl component1 = new LayoutControl();
      component1.Layout = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Layouts.Labeled.Column1TemplateHeader.ascx");
      TemplateControl control1 = this.PageManager.CreateControl<TemplateControl>(false);
      control1.ObjectType = component1.GetType().FullName;
      control1.PlaceHolder = "Body";
      control1.SiblingId = empty;
      Guid id1 = control1.Id;
      control1.Caption = "Header";
      control1.Description = "Represents the header of the template.";
      this.PageManager.ReadProperties((object) component1, (ObjectData) control1);
      this.PageManager.SetControlId((IControlsContainer) template, (ObjectData) control1, (CultureInfo) null);
      control1.SetDefaultPermissions((IControlManager) this.PageManager);
      template.Controls.Add(control1);
      LayoutControl component2 = new LayoutControl();
      component2.Layout = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Layouts.Labeled.Column2Template3.ascx");
      TemplateControl control2 = this.PageManager.CreateControl<TemplateControl>(false);
      control2.ObjectType = component2.GetType().FullName;
      control2.PlaceHolder = "Body";
      control2.SiblingId = id1;
      Guid id2 = control2.Id;
      control2.Caption = "2 Equal Columns";
      control2.Description = "Represents the content areas of the template.";
      this.PageManager.ReadProperties((object) component2, (ObjectData) control2);
      this.PageManager.SetControlId((IControlsContainer) template, (ObjectData) control2, (CultureInfo) null);
      control2.SetDefaultPermissions((IControlManager) this.PageManager);
      template.Controls.Add(control2);
      LayoutControl component3 = new LayoutControl();
      component3.Layout = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Layouts.Labeled.Column1TemplateFooter.ascx");
      TemplateControl control3 = this.PageManager.CreateControl<TemplateControl>(false);
      control3.ObjectType = component3.GetType().FullName;
      control3.PlaceHolder = "Body";
      control3.SiblingId = id2;
      Guid id3 = control3.Id;
      control3.Caption = "Footer";
      control3.Description = "Represents the footer of the template.";
      this.PageManager.ReadProperties((object) component3, (ObjectData) control3);
      this.PageManager.SetControlId((IControlsContainer) template, (ObjectData) control3, (CultureInfo) null);
      control3.SetDefaultPermissions((IControlManager) this.PageManager);
      template.Controls.Add(control3);
      this.CreateTemplateInvariantLanguageData(template);
    }

    /// <summary>
    /// Creates the 3 equal columns, header and footer template.
    /// </summary>
    /// <param name="node">The node.</param>
    /// <param name="ordinal">The ordinal.</param>
    /// <param name="restoreToDefaultTemplate">The restore to default template.</param>
    protected virtual void Create3EqualHeaderFooter(
      HierarchicalTaxon node,
      short ordinal,
      bool restoreToDefaultTemplate = false)
    {
      PageTemplate template = this.GetTemplate(SiteInitializer.TemplateId3EqualHeaderFooter, restoreToDefaultTemplate);
      template.Name = "ThreeEqualHeaderFooter";
      Res.SetLstring(template.Title, typeof (PageResources), "ThreeEqualHeaderFooter");
      template.Category = node.Id;
      template.Ordinal = ordinal;
      TemplatePresentation presentationItem1 = this.PageManager.CreatePresentationItem<TemplatePresentation>();
      presentationItem1.DataType = "HTML_DOCUMENT";
      presentationItem1.Name = "master";
      string fileName = "Telerik.Sitefinity.Resources.Pages.Frontend.aspx";
      presentationItem1.Data = ControlUtilities.GetTextResource(fileName, this.assemblyInfo);
      template.Presentation.Add(presentationItem1);
      TemplatePresentation presentationItem2 = this.PageManager.CreatePresentationItem<TemplatePresentation>();
      presentationItem2.DataType = "IMAGE_URL";
      presentationItem2.Name = "icon";
      presentationItem2.Theme = "Default";
      presentationItem2.Data = "Telerik.Sitefinity.Resources.Themes.Light.Images.PageTemplates.3EqualColumnsHeaderFooter.gif";
      template.Presentation.Add(presentationItem2);
      Guid empty = Guid.Empty;
      LayoutControl component1 = new LayoutControl();
      component1.Layout = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Layouts.Labeled.Column1TemplateHeader.ascx");
      TemplateControl control1 = this.PageManager.CreateControl<TemplateControl>(false);
      control1.ObjectType = component1.GetType().FullName;
      control1.PlaceHolder = "Body";
      control1.SiblingId = empty;
      Guid id1 = control1.Id;
      control1.Caption = "Header";
      control1.Description = "Represents the header of the template.";
      this.PageManager.ReadProperties((object) component1, (ObjectData) control1);
      this.PageManager.SetControlId((IControlsContainer) template, (ObjectData) control1, (CultureInfo) null);
      control1.SetDefaultPermissions((IControlManager) this.PageManager);
      template.Controls.Add(control1);
      LayoutControl component2 = new LayoutControl();
      component2.Layout = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Layouts.Labeled.Column3Template1.ascx");
      TemplateControl control2 = this.PageManager.CreateControl<TemplateControl>(false);
      control2.ObjectType = component2.GetType().FullName;
      control2.PlaceHolder = "Body";
      control2.SiblingId = id1;
      Guid id2 = control2.Id;
      control2.Caption = "3 Equal Columns";
      control2.Description = "Represents the content areas of the template.";
      this.PageManager.ReadProperties((object) component2, (ObjectData) control2);
      this.PageManager.SetControlId((IControlsContainer) template, (ObjectData) control2, (CultureInfo) null);
      control2.SetDefaultPermissions((IControlManager) this.PageManager);
      template.Controls.Add(control2);
      LayoutControl component3 = new LayoutControl();
      component3.Layout = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Layouts.Labeled.Column1TemplateFooter.ascx");
      TemplateControl control3 = this.PageManager.CreateControl<TemplateControl>(false);
      control3.ObjectType = component3.GetType().FullName;
      control3.PlaceHolder = "Body";
      control3.SiblingId = id2;
      Guid id3 = control3.Id;
      control3.Caption = "Footer";
      control3.Description = "Represents the footer of the template.";
      this.PageManager.ReadProperties((object) component3, (ObjectData) control3);
      this.PageManager.SetControlId((IControlsContainer) template, (ObjectData) control3, (CultureInfo) null);
      control3.SetDefaultPermissions((IControlManager) this.PageManager);
      template.Controls.Add(control3);
      this.CreateTemplateInvariantLanguageData(template);
    }

    /// <summary>Creates the two sidebars template.</summary>
    /// <param name="node">The node.</param>
    protected virtual void Create2Sidebars(
      HierarchicalTaxon node,
      short ordinal,
      bool restoreToDefaultTemplate = false)
    {
      PageTemplate template = this.GetTemplate(SiteInitializer.TemplateId2Sidebars, restoreToDefaultTemplate);
      template.Name = "TwoSidebarsHeaderFooter";
      Res.SetLstring(template.Title, typeof (PageResources), "TwoSidebarsHeaderFooter");
      template.Category = node.Id;
      template.Ordinal = ordinal;
      TemplatePresentation presentationItem1 = this.PageManager.CreatePresentationItem<TemplatePresentation>();
      presentationItem1.DataType = "HTML_DOCUMENT";
      presentationItem1.Name = "master";
      string fileName = "Telerik.Sitefinity.Resources.Pages.Frontend.aspx";
      presentationItem1.Data = ControlUtilities.GetTextResource(fileName, this.assemblyInfo);
      template.Presentation.Add(presentationItem1);
      TemplatePresentation presentationItem2 = this.PageManager.CreatePresentationItem<TemplatePresentation>();
      presentationItem2.DataType = "IMAGE_URL";
      presentationItem2.Name = "icon";
      presentationItem2.Theme = "Default";
      presentationItem2.Data = "Telerik.Sitefinity.Resources.Themes.Light.Images.PageTemplates.2SidebarsHeaderFooter.gif";
      template.Presentation.Add(presentationItem2);
      Guid empty = Guid.Empty;
      LayoutControl component1 = new LayoutControl();
      component1.Layout = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Layouts.Labeled.Column1TemplateHeader.ascx");
      TemplateControl control1 = this.PageManager.CreateControl<TemplateControl>(false);
      control1.ObjectType = component1.GetType().FullName;
      control1.PlaceHolder = "Body";
      control1.SiblingId = empty;
      Guid id1 = control1.Id;
      control1.Caption = "Header";
      control1.Description = "Represents the header of the template.";
      this.PageManager.ReadProperties((object) component1, (ObjectData) control1);
      this.PageManager.SetControlId((IControlsContainer) template, (ObjectData) control1, (CultureInfo) null);
      control1.SetDefaultPermissions((IControlManager) this.PageManager);
      template.Controls.Add(control1);
      LayoutControl component2 = new LayoutControl();
      component2.Layout = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Layouts.Labeled.Column3Template2.ascx");
      TemplateControl control2 = this.PageManager.CreateControl<TemplateControl>(false);
      control2.ObjectType = component2.GetType().FullName;
      control2.PlaceHolder = "Body";
      control2.SiblingId = id1;
      Guid id2 = control2.Id;
      control2.Caption = "2 Sidebars";
      control2.Description = "Represents the content areas of the template.";
      this.PageManager.ReadProperties((object) component2, (ObjectData) control2);
      this.PageManager.SetControlId((IControlsContainer) template, (ObjectData) control2, (CultureInfo) null);
      control2.SetDefaultPermissions((IControlManager) this.PageManager);
      template.Controls.Add(control2);
      LayoutControl component3 = new LayoutControl();
      component3.Layout = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Layouts.Labeled.Column1TemplateFooter.ascx");
      TemplateControl control3 = this.PageManager.CreateControl<TemplateControl>(false);
      control3.ObjectType = component3.GetType().FullName;
      control3.PlaceHolder = "Body";
      control3.SiblingId = id2;
      Guid id3 = control3.Id;
      control3.Caption = "Footer";
      control3.Description = "Represents the footer of the template.";
      this.PageManager.ReadProperties((object) component3, (ObjectData) control3);
      this.PageManager.SetControlId((IControlsContainer) template, (ObjectData) control3, (CultureInfo) null);
      control3.SetDefaultPermissions((IControlManager) this.PageManager);
      template.Controls.Add(control3);
      this.CreateTemplateInvariantLanguageData(template);
    }

    /// <summary>
    /// Creates the promo, 3 columns, header and footer template.
    /// </summary>
    /// <param name="node">The node.</param>
    protected virtual void CreatePromo3ColumnsHeaderFooter(
      HierarchicalTaxon node,
      short ordinal,
      bool restoreToDefaultTemplate = false)
    {
      PageTemplate template = this.GetTemplate(SiteInitializer.TemplateIdPromo3ColumnsHeaderFooter, restoreToDefaultTemplate);
      template.Name = "Promo3ColumnsHeaderFooter";
      Res.SetLstring(template.Title, typeof (PageResources), "Promo3ColumnsHeaderFooter");
      template.Category = node.Id;
      template.Ordinal = ordinal;
      TemplatePresentation presentationItem1 = this.PageManager.CreatePresentationItem<TemplatePresentation>();
      presentationItem1.DataType = "HTML_DOCUMENT";
      presentationItem1.Name = "master";
      string fileName = "Telerik.Sitefinity.Resources.Pages.Frontend.aspx";
      presentationItem1.Data = ControlUtilities.GetTextResource(fileName, this.assemblyInfo);
      template.Presentation.Add(presentationItem1);
      TemplatePresentation presentationItem2 = this.PageManager.CreatePresentationItem<TemplatePresentation>();
      presentationItem2.DataType = "IMAGE_URL";
      presentationItem2.Name = "icon";
      presentationItem2.Theme = "Default";
      presentationItem2.Data = "Telerik.Sitefinity.Resources.Themes.Light.Images.PageTemplates.Promo3ColumnsHeaderFooter.gif";
      template.Presentation.Add(presentationItem2);
      Guid empty = Guid.Empty;
      LayoutControl component1 = new LayoutControl();
      component1.Layout = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Layouts.Labeled.Column1TemplateHeader.ascx");
      TemplateControl control1 = this.PageManager.CreateControl<TemplateControl>(false);
      control1.ObjectType = component1.GetType().FullName;
      control1.PlaceHolder = "Body";
      control1.SiblingId = empty;
      Guid id1 = control1.Id;
      control1.Caption = "Header";
      control1.Description = "Represents the header of the template.";
      this.PageManager.ReadProperties((object) component1, (ObjectData) control1);
      this.PageManager.SetControlId((IControlsContainer) template, (ObjectData) control1, (CultureInfo) null);
      control1.SetDefaultPermissions((IControlManager) this.PageManager);
      template.Controls.Add(control1);
      LayoutControl component2 = new LayoutControl();
      component2.Layout = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Layouts.Labeled.Column1TemplatePromo.ascx");
      TemplateControl control2 = this.PageManager.CreateControl<TemplateControl>(false);
      control2.ObjectType = component2.GetType().FullName;
      control2.PlaceHolder = "Body";
      control2.SiblingId = id1;
      Guid id2 = control2.Id;
      control2.Caption = "Promo";
      control2.Description = "Represents the promo area of the template.";
      this.PageManager.ReadProperties((object) component2, (ObjectData) control2);
      this.PageManager.SetControlId((IControlsContainer) template, (ObjectData) control2, (CultureInfo) null);
      control2.SetDefaultPermissions((IControlManager) this.PageManager);
      template.Controls.Add(control2);
      LayoutControl component3 = new LayoutControl();
      component3.Layout = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Layouts.Labeled.Column3Template1.ascx");
      TemplateControl control3 = this.PageManager.CreateControl<TemplateControl>(false);
      control3.ObjectType = component3.GetType().FullName;
      control3.PlaceHolder = "Body";
      control3.SiblingId = id2;
      Guid id3 = control3.Id;
      control3.Caption = "3 Equal Columns";
      control3.Description = "Represents the content areas of the template.";
      this.PageManager.ReadProperties((object) component3, (ObjectData) control3);
      this.PageManager.SetControlId((IControlsContainer) template, (ObjectData) control3, (CultureInfo) null);
      control3.SetDefaultPermissions((IControlManager) this.PageManager);
      template.Controls.Add(control3);
      LayoutControl component4 = new LayoutControl();
      component4.Layout = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Layouts.Labeled.Column1TemplateFooter.ascx");
      TemplateControl control4 = this.PageManager.CreateControl<TemplateControl>(false);
      control4.ObjectType = component4.GetType().FullName;
      control4.PlaceHolder = "Body";
      control4.SiblingId = id3;
      Guid id4 = control4.Id;
      control4.Caption = "Footer";
      control4.Description = "Represents the footer of the template.";
      this.PageManager.ReadProperties((object) component4, (ObjectData) control4);
      this.PageManager.SetControlId((IControlsContainer) template, (ObjectData) control4, (CultureInfo) null);
      control4.SetDefaultPermissions((IControlManager) this.PageManager);
      template.Controls.Add(control4);
      this.CreateTemplateInvariantLanguageData(template);
    }

    /// <summary>Creates the backend default template.</summary>
    /// <param name="node">The node.</param>
    /// <param name="ordinal">The ordinal.</param>
    /// <param name="restoreToDefaultTemplate">if set to <c>true</c> [restore to default template].</param>
    /// <returns></returns>
    public virtual void CreateBackendDefaultTemplate(
      HierarchicalTaxon node,
      short ordinal,
      bool restoreToDefaultTemplate = false)
    {
      this.CreateBackendDefaultTemplate(restoreToDefaultTemplate);
    }

    /// <summary>Creates the backend html 5 template.</summary>
    /// <param name="node">The node.</param>
    /// <param name="ordinal">The ordinal.</param>
    /// <param name="restoreToDefaultTemplate">if set to <c>true</c> [restore to default template].</param>
    /// <returns></returns>
    public virtual void CreateBackendHtml5Template(
      HierarchicalTaxon node,
      short ordinal,
      bool restoreToDefaultTemplate = false)
    {
      this.CreateBackendHtml5Template(restoreToDefaultTemplate);
    }

    /// <summary>Gets the backend template.</summary>
    /// <param name="restoreToDefaultTemplate">The restore to default template.</param>
    /// <value>The backend template.</value>
    /// <returns></returns>
    public virtual PageTemplate CreateBackendDefaultTemplate(
      bool restoreToDefaultTemplate = false)
    {
      PageTemplate template = this.GetTemplate(SiteInitializer.DefaultBackendTemplateId, restoreToDefaultTemplate);
      template.Name = "DefaultBackend";
      template.Title = (Lstring) "Default Backend Template";
      template.Category = SiteInitializer.BackendTemplatesCategoryId;
      template.Ordinal = (short) 0;
      template.IncludeScriptManager = true;
      template.Key = "T00112233";
      TemplatePresentation presentationItem1 = this.PageManager.CreatePresentationItem<TemplatePresentation>();
      presentationItem1.DataType = "HTML_DOCUMENT";
      presentationItem1.Name = "master";
      string fileName = "Telerik.Sitefinity.Resources.Pages.Backend.master";
      presentationItem1.Data = ControlUtilities.GetTextResource(fileName, this.assemblyInfo);
      template.Presentation.Add(presentationItem1);
      TemplatePresentation presentationItem2 = this.PageManager.CreatePresentationItem<TemplatePresentation>();
      presentationItem2.DataType = "IMAGE_URL";
      presentationItem2.Name = "icon";
      presentationItem2.Theme = "Default";
      presentationItem2.Data = "Telerik.Sitefinity.Resources.Themes.Light.Images.PageTemplates.HubPage.gif";
      template.Presentation.Add(presentationItem2);
      this.CreateTemplateControls(template);
      return template;
    }

    /// <summary>Gets the HTML5 based backend template.</summary>
    /// <param name="restoreToDefaultTemplate">The restore to default template.</param>
    /// <value>The HTML5 based backend template.</value>
    /// <returns></returns>
    public virtual PageTemplate CreateBackendHtml5Template(bool restoreToDefaultTemplate = false)
    {
      PageTemplate template = this.GetTemplate(SiteInitializer.BackendHtml5TemplateId, restoreToDefaultTemplate);
      template.Name = "BackendHtml5";
      template.Title = (Lstring) "HTML5 Backend Template";
      template.Category = SiteInitializer.BackendTemplatesCategoryId;
      template.Ordinal = (short) 0;
      template.IncludeScriptManager = false;
      template.Key = "T44556677";
      TemplatePresentation presentationItem1 = this.PageManager.CreatePresentationItem<TemplatePresentation>();
      presentationItem1.DataType = "HTML_DOCUMENT";
      presentationItem1.Name = "master";
      string fileName = "Telerik.Sitefinity.Resources.Pages.BackendHtml5.master";
      presentationItem1.Data = ControlUtilities.GetTextResource(fileName, this.assemblyInfo);
      template.Presentation.Add(presentationItem1);
      TemplatePresentation presentationItem2 = this.PageManager.CreatePresentationItem<TemplatePresentation>();
      presentationItem2.DataType = "IMAGE_URL";
      presentationItem2.Name = "icon";
      presentationItem2.Theme = "Default";
      presentationItem2.Data = "Telerik.Sitefinity.Resources.Themes.Light.Images.PageTemplates.HubPage.gif";
      template.Presentation.Add(presentationItem2);
      this.CreateTemplateControls(template);
      return template;
    }

    /// <summary>
    /// Gets an empty backend template - with no controls attached to it.
    /// </summary>
    /// <value>The empty backend template.</value>
    protected virtual void CreateBackendTemplateEmpty(
      HierarchicalTaxon node,
      short ordinal,
      bool restoreToDefaultTemplate = false)
    {
      PageTemplate template = this.GetTemplate(SiteInitializer.DefaultBackendTemplateEmptyId, restoreToDefaultTemplate);
      template.Name = "DefaultBackendEmpty";
      template.Title = (Lstring) "Empty Backend Template";
      template.Category = SiteInitializer.BackendTemplatesCategoryId;
      template.Ordinal = (short) 1;
      template.IncludeScriptManager = true;
      template.Key = "T8899AABB";
      TemplatePresentation presentationItem1 = this.PageManager.CreatePresentationItem<TemplatePresentation>();
      presentationItem1.DataType = "HTML_DOCUMENT";
      presentationItem1.Name = "master";
      string fileName = "Telerik.Sitefinity.Resources.Pages.Backend.master";
      presentationItem1.Data = ControlUtilities.GetTextResource(fileName, this.assemblyInfo);
      template.Presentation.Add(presentationItem1);
      TemplatePresentation presentationItem2 = this.PageManager.CreatePresentationItem<TemplatePresentation>();
      presentationItem2.DataType = "IMAGE_URL";
      presentationItem2.Name = "icon";
      presentationItem2.Theme = "Default";
      presentationItem2.Data = "Telerik.Sitefinity.Resources.Themes.Light.Images.PageTemplates.HubPage.gif";
      template.Presentation.Add(presentationItem2);
    }

    /// <summary>Creates the template controls.</summary>
    /// <param name="template">The template.</param>
    protected virtual void CreateTemplateControls(PageTemplate template)
    {
      MainMenuPanel component1 = new MainMenuPanel();
      component1.ID = "MainMenuPanel";
      component1.ExpandAnimationType = AnimationType.None;
      component1.CollapseAnimationType = AnimationType.None;
      TemplateControl control1 = this.PageManager.CreateControl<TemplateControl>(false);
      control1.ObjectType = component1.GetType().FullName;
      control1.PlaceHolder = "Header";
      control1.Caption = "MainMenu";
      control1.Description = "Represents the main menu in Sitefinity's backend.";
      this.PageManager.ReadProperties((object) component1, (ObjectData) control1);
      this.PageManager.SetControlId((IControlsContainer) template, (ObjectData) control1, (CultureInfo) null);
      control1.SetDefaultPermissions((IControlManager) this.PageManager);
      Guid id = control1.Id;
      template.Controls.Add(control1);
      Header component2 = new Header();
      TemplateControl control2 = this.PageManager.CreateControl<TemplateControl>(false);
      control2.ObjectType = component2.GetType().FullName;
      control2.PlaceHolder = "Header";
      control2.Caption = "Header";
      control2.Description = "Represents the header and the top menu of all backend pages.";
      this.PageManager.ReadProperties((object) component2, (ObjectData) control2);
      this.PageManager.SetControlId((IControlsContainer) template, (ObjectData) control2, (CultureInfo) null);
      control2.SetDefaultPermissions((IControlManager) this.PageManager);
      control2.SiblingId = id;
      template.Controls.Add(control2);
      Footer component3 = new Footer();
      TemplateControl control3 = this.PageManager.CreateControl<TemplateControl>(false);
      control3.ObjectType = component3.GetType().FullName;
      control3.PlaceHolder = "Footer";
      control3.Caption = "Footer";
      control3.Description = "Represents the footer of all backend pages.";
      this.PageManager.ReadProperties((object) component3, (ObjectData) control3);
      this.PageManager.SetControlId((IControlsContainer) template, (ObjectData) control3, (CultureInfo) null);
      control3.SetDefaultPermissions((IControlManager) this.PageManager);
      template.Controls.Add(control3);
    }

    /// <summary>Creates the template invariant language data.</summary>
    /// <param name="template">The template.</param>
    protected virtual void CreateTemplateInvariantLanguageData(PageTemplate template)
    {
      LanguageData publishedLanguageData = this.PageManager.CreatePublishedLanguageData();
      template.LanguageData.Add(publishedLanguageData);
    }

    private PageManager PageManager { get; set; }

    private delegate void MethodToInvoke(
      HierarchicalTaxon node,
      short ordinal,
      bool restoreToDefaultTemplate = false);
  }
}
