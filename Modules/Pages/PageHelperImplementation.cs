// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.PageHelperImplementation
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Routing;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Backend;

namespace Telerik.Sitefinity.Modules.Pages
{
  public class PageHelperImplementation
  {
    private RouteInfoCollection childRoutes = new RouteInfoCollection();

    internal void AddPageTemplates(
      IEnumerable list,
      Dictionary<string, ITemplate> pageTmps,
      IDictionary<string, ITemplate> layoutTmps)
    {
      foreach (PresentationData presentationData in list)
      {
        string key = presentationData.Theme ?? "Default";
        if (presentationData.DataType == "HTML_DOCUMENT")
        {
          if (!pageTmps.ContainsKey(key))
            pageTmps.Add(key, ControlUtilities.GetTemplate((string) null, presentationData.Id.ToString(), (Type) null, presentationData.Data));
        }
        else if (presentationData.DataType == "HTML_LAYOUT" && !pageTmps.ContainsKey(key))
          layoutTmps.Add(key, ControlUtilities.GetTemplate((string) null, presentationData.Id.ToString(), (Type) null, presentationData.Data));
      }
    }

    internal void ApplyLayouts(
      IList<Dictionary<string, ITemplate>> layouts,
      Page page,
      string theme)
    {
      if (layouts == null || layouts.Count <= 0)
        return;
      PlaceHoldersCollection placeHolders = page.GetPlaceHolders();
      if (string.IsNullOrEmpty(theme))
        theme = Config.Get<PagesConfig>().DefaultTheme;
      foreach (Dictionary<string, ITemplate> layout in (IEnumerable<Dictionary<string, ITemplate>>) layouts)
      {
        ITemplate current;
        if (!layout.TryGetValue(theme, out current))
        {
          Dictionary<string, ITemplate>.ValueCollection.Enumerator enumerator = layout.Values.GetEnumerator();
          enumerator.MoveNext();
          current = enumerator.Current;
        }
        IContentPlaceHolderContainer placeHolderContainer = current as IContentPlaceHolderContainer;
        if (placeHolders.Count > 0 && placeHolderContainer != null)
          placeHolderContainer.InstantiateIn((Control) page.Form, placeHolders);
        else
          current.InstantiateIn((Control) page.Form);
      }
    }

    internal void CreateChildControls(
      IList<ControlBuilder> controls,
      Page page,
      bool ignoreCultures,
      bool addControlIdScript)
    {
      if (controls == null || controls.Count <= 0)
        return;
      Dictionary<Guid, Control> dictionary = new Dictionary<Guid, Control>();
      RequestContext requestContext = page.GetRequestContext();
      PlaceHoldersCollection placeHolders = page.GetPlaceHolders();
      List<ControlBuilder> controlBuilderList1 = (List<ControlBuilder>) null;
      while (true)
      {
        List<ControlBuilder> controlBuilderList2 = new List<ControlBuilder>();
        if (controlBuilderList1 == null)
          controlBuilderList1 = new List<ControlBuilder>((IEnumerable<ControlBuilder>) controls);
        foreach (ControlBuilder builder in controlBuilderList1)
        {
          if (builder.Shared && PageHelper.IsAccessibleToUser(requestContext.HttpContext, builder))
          {
            if (!ignoreCultures && builder.Culture != null && builder.Culture != SystemManager.CurrentContext.Culture)
            {
              for (CultureInfo parent = SystemManager.CurrentContext.Culture.Parent; parent != null; parent = parent.Parent)
              {
                if (builder.Culture != parent)
                {
                  if (parent.IsNeutralCulture || parent == CultureInfo.InvariantCulture)
                    break;
                }
                else
                  goto label_14;
              }
              continue;
            }
label_14:
            Control control1 = builder.CreateControl(page);
            if (control1 is MvcProxyBase mvcProxyBase)
            {
              string str = builder.ControlId.ToString();
              mvcProxyBase.ControlDataId = str;
            }
            if (builder.IsPartialRouteHandler)
              PageHelper.SetPartialRouteHandler((object) control1, requestContext, "Params");
            Control placeHolder;
            if (placeHolders.TryGetValue(builder.PlaceHolder, out placeHolder))
            {
              if (builder.SiblingId == Guid.Empty)
              {
                PageHelperImplementation.AddControlToPlaceholder(0, builder.ControlId, control1, placeHolder, addControlIdScript);
              }
              else
              {
                Control control2;
                dictionary.TryGetValue(builder.SiblingId, out control2);
                if (control2 != null)
                  PageHelperImplementation.AddControlToPlaceholder(placeHolder.Controls.IndexOf(control2) + 1, builder.ControlId, control1, placeHolder, addControlIdScript);
                else
                  PageHelperImplementation.AddControlToPlaceholder(builder.ControlId, control1, placeHolder, addControlIdScript);
              }
              dictionary[builder.ControlId] = control1;
            }
            else
              controlBuilderList2.Add(builder);
            if (control1 is LayoutControl)
            {
              LayoutControl layoutControl = (LayoutControl) control1;
              layoutControl.PlaceHolder = builder.PlaceHolder;
              foreach (Control placeholder in (Collection<Control>) layoutControl.Placeholders)
              {
                if (!placeHolders.Contains(placeholder.ID))
                  placeHolders.Add(placeholder);
              }
            }
          }
        }
        if (controlBuilderList1.Count != 0 && controlBuilderList1.Count != controlBuilderList2.Count)
          controlBuilderList1 = controlBuilderList2;
        else
          break;
      }
    }

    /// <summary>Adds the control to the specified placeholder.</summary>
    /// <param name="index">The index at which the control should be added.</param>
    /// <param name="contolId">The control id.</param>
    /// <param name="ctrl">The CTRL.</param>
    /// <param name="placeHolder">The place holder.</param>
    /// <param name="addControlIdScript">if set to <c>true</c> then a literal control with script containing the control Id is also added to the placeholder right before the specified control. </param>
    internal static void AddControlToPlaceholder(
      int index,
      Guid controlId,
      Control ctrl,
      Control placeHolder,
      bool addControlIdScript)
    {
      if (addControlIdScript)
      {
        placeHolder.Controls.AddAt(index, (Control) new LiteralControl()
        {
          Text = string.Format("<script data-sf-ctrlid=\"{0}\"></script>", (object) controlId)
        });
        placeHolder.Controls.AddAt(index + 1, ctrl);
      }
      else
        placeHolder.Controls.AddAt(index, ctrl);
    }

    /// <summary>Adds the control to placeholder.</summary>
    /// <param name="controlId">The control id.</param>
    /// <param name="ctrl">The CTRL.</param>
    /// <param name="placeHolder">The place holder.</param>
    /// <param name="addControlIdScript">if set to <c>true</c> then a literal control with script containing the control Id is also added to the placeholder right before the specified control. </param>
    internal static void AddControlToPlaceholder(
      Guid controlId,
      Control ctrl,
      Control placeHolder,
      bool addControlIdScript)
    {
      PageHelperImplementation.AddControlToPlaceholder(placeHolder.Controls.Count == 0 ? 0 : placeHolder.Controls.Count - 1, controlId, ctrl, placeHolder, addControlIdScript);
    }

    internal bool IsAccessibleToUser(HttpContextBase context, ControlBuilder builder)
    {
      bool result = false;
      if (((context.Items[(object) "versionpreview"] == null ? 0 : (bool.TryParse(context.Items[(object) "versionpreview"].ToString(), out result) ? 1 : 0)) & (result ? 1 : 0)) != 0)
        return true;
      if (builder.SecurityObject != null)
      {
        if (builder.SecurityObject.IsPermissionSetSupported("Controls"))
          return builder.SecurityObject.IsGranted("Controls", "ViewControl");
        if (builder.SecurityObject.IsPermissionSetSupported("LayoutElement"))
          return builder.SecurityObject.IsGranted("LayoutElement", "ViewLayout");
      }
      if (builder.Roles == null)
        return true;
      SitefinityPrincipal principal = ClaimsManager.GetCurrentPrincipal();
      return ((SitefinityIdentity) principal.Identity).IsUnrestricted || builder.Roles.Any<Guid>((Func<Guid, bool>) (roleId => principal.IsInRole(roleId)));
    }

    internal ITemplate GetPageTemplate(
      IDictionary<string, ITemplate> pageTemplates,
      string theme)
    {
      if (pageTemplates == null || pageTemplates.Count == 0)
        return (ITemplate) null;
      if (string.IsNullOrEmpty(theme))
        theme = Config.Get<PagesConfig>().DefaultTheme;
      ITemplate pageTemplate;
      if (!string.IsNullOrEmpty(theme) && pageTemplates.TryGetValue(theme, out pageTemplate))
        return pageTemplate;
      IEnumerator<ITemplate> enumerator = pageTemplates.Values.GetEnumerator();
      enumerator.MoveNext();
      return enumerator.Current;
    }

    internal void ProcessPresentationData(
      IEnumerable<PresentationData> presentation,
      IList<Dictionary<string, ITemplate>> layoutTmps,
      Dictionary<string, ITemplate> pageTmps)
    {
      if (presentation == null)
        return;
      Dictionary<string, ITemplate> layoutTmps1 = new Dictionary<string, ITemplate>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
      PageHelper.AddPageTemplates((IEnumerable) presentation, pageTmps, (IDictionary<string, ITemplate>) layoutTmps1);
      if (layoutTmps1.Count <= 0)
        return;
      layoutTmps.Add(layoutTmps1);
    }

    internal void ProcessTemplates(
      IPageTemplate template,
      List<Dictionary<string, ITemplate>> layoutTmps,
      Dictionary<string, ITemplate> pageTmps,
      List<IControlsContainer> controlConainers,
      bool optimized,
      out string masterPage,
      out bool includeScriptManager)
    {
      masterPage = (string) null;
      includeScriptManager = false;
      for (; template != null; template = template.ParentTemplate)
      {
        if (optimized)
          controlConainers.Add((IControlsContainer) new TemplateControlsContainerWrapper((IControlsContainer) template, ((IDataItem) template).Provider as PageDataProvider));
        else
          controlConainers.Add((IControlsContainer) template);
        PageHelper.ProcessPresentationData(template.Presentation, (IList<Dictionary<string, ITemplate>>) layoutTmps, pageTmps);
        if (!string.IsNullOrEmpty(template.MasterPage))
        {
          masterPage = template.MasterPage;
          break;
        }
        masterPage = this.ResolveDynamicMasterPage(template);
        if (!string.IsNullOrEmpty(masterPage))
          break;
        includeScriptManager |= template.IncludeScriptManager;
      }
    }

    /// <summary>Resolves the dynamic master page.</summary>
    /// <param name="template">The template.</param>
    /// <returns></returns>
    internal string ResolveDynamicMasterPage(IPageTemplate template)
    {
      string str = string.Empty;
      if (ObjectFactory.IsTypeRegistered<ILayoutResolver>())
        str = ObjectFactory.Resolve<ILayoutResolver>().GetVirtualPath(template);
      return str;
    }

    internal void ProcessControls(
      IList<ControlData> controls,
      IList<ControlData> placeholders,
      IList<IControlsContainer> controlContainers)
    {
      List<ControlData> source = this.SortControls(controlContainers.Reverse<IControlsContainer>(), controlContainers.Count);
      IEnumerable<ControlData> controlDatas = source.Where<ControlData>((Func<ControlData, bool>) (control =>
      {
        Guid baseControlId = control.BaseControlId;
        return control.BaseControlId != Guid.Empty;
      }));
      foreach (ControlData overridenData in source)
      {
        Guid baseControlId = overridenData.BaseControlId;
        if (!(overridenData.BaseControlId != Guid.Empty))
        {
          foreach (ControlData baseData in controlDatas)
          {
            if (baseData.BaseControlId == overridenData.Id)
              overridenData.OverrideFrom(baseData);
          }
          if (overridenData.IsLayoutControl)
            placeholders.Add(overridenData);
          else
            controls.Add(overridenData);
        }
      }
    }

    internal void ProcessControls(
      IList<ControlBuilder> builders,
      IList<IControlsContainer> controlContainers,
      Guid pageDataId)
    {
      foreach (IControlsContainer controlContainer in (IEnumerable<IControlsContainer>) controlContainers)
      {
        foreach (ControlData control in controlContainer.Controls)
          control.ContainerType = controlContainer.GetType();
      }
      List<ControlData> source = this.SortControls(controlContainers.Reverse<IControlsContainer>(), controlContainers.Count);
      IEnumerable<ControlData> controlDatas = source.Where<ControlData>((Func<ControlData, bool>) (control =>
      {
        Guid baseControlId = control.BaseControlId;
        return control.BaseControlId != Guid.Empty;
      }));
      foreach (ControlData controlData in source)
      {
        Guid baseControlId = controlData.BaseControlId;
        if (!(controlData.BaseControlId != Guid.Empty))
        {
          foreach (ControlData baseData in controlDatas)
          {
            if (baseData.BaseControlId == controlData.Id)
              controlData.OverrideFrom(baseData);
          }
          if (ToolboxesConfig.Current.ValidateWidget(controlData))
            builders.Add(new ControlBuilder((ObjectData) controlData, pageDataId));
        }
      }
    }

    internal void AssertNotCyclic(ControlData ctrl, IEnumerable<ControlData> controls)
    {
      HashSet<Guid> guidSet = new HashSet<Guid>();
      while (ctrl.SiblingId != Guid.Empty)
      {
        guidSet.Add(ctrl.Id);
        if (guidSet.Contains(ctrl.SiblingId))
          throw new InvalidDataException(Res.Get<PageResources>().CyclicChildParentRelationship);
        ctrl = controls.Where<ControlData>((Func<ControlData, bool>) (c => c.Id == ctrl.SiblingId)).FirstOrDefault<ControlData>();
        if (ctrl == null)
          break;
      }
    }

    public List<ControlData> GetOrderedControlsCollection(
      List<ControlNode> controlNodes)
    {
      List<ControlData> controlsCollection1 = new List<ControlData>();
      foreach (ControlNode controlNode in controlNodes)
      {
        if (controlNode.Control != null)
          controlsCollection1.Add(controlNode.Control);
        if ((controlNode.Control != null || controlNode.NodeType == ControlNodeType.Root) && controlNode.Children.Count > 0)
        {
          List<ControlData> controlsCollection2 = PageHelper.GetOrderedControlsCollection(controlNode.Children);
          controlsCollection1.AddRange((IEnumerable<ControlData>) controlsCollection2);
        }
      }
      return controlsCollection1;
    }

    public List<ControlNode> GetControlsHierarchical(
      IEnumerable<IControlsContainer> controlContainersOrdered,
      List<string> staticPlaceholders)
    {
      List<ControlData> controls = new List<ControlData>();
      foreach (IControlsContainer controlsContainer in controlContainersOrdered)
        controls.AddRange(controlsContainer.Controls);
      List<Guid> containerIdsOrdered = new List<Guid>(controlContainersOrdered.Select<IControlsContainer, Guid>((Func<IControlsContainer, Guid>) (c => c.Id)));
      return PageHelper.GetControlsHierarchical(controls, containerIdsOrdered, staticPlaceholders);
    }

    public List<ControlNode> GetControlsHierarchical(
      List<ControlData> controls,
      List<Guid> containerIdsOrdered,
      List<string> staticPlaceholders)
    {
      Dictionary<string, ControlNode> dictionary1 = new Dictionary<string, ControlNode>();
      Dictionary<string, ControlNode> dictionary2 = new Dictionary<string, ControlNode>();
      foreach (ControlData control in controls)
      {
        ControlNode controlNode1 = (ControlNode) null;
        int num = containerIdsOrdered.IndexOf(control.ContainerId);
        if (control.IsLayoutControl)
        {
          foreach (string placeHolder in control.PlaceHolders)
          {
            ControlNode controlNode2;
            dictionary2.TryGetValue(placeHolder, out controlNode2);
            if (controlNode2 != null)
            {
              dictionary2.Remove(placeHolder);
              if (controlNode1 == null)
              {
                controlNode1 = controlNode2;
                controlNode1.Control = control;
                controlNode1.NodeType = ControlNodeType.Normal;
              }
              else
                controlNode1.Children.AddRange((IEnumerable<ControlNode>) controlNode2.Children);
            }
          }
          if (controlNode1 == null)
            controlNode1 = new ControlNode(control);
          foreach (string placeHolder in control.PlaceHolders)
            dictionary1.Add(placeHolder, controlNode1);
        }
        if (controlNode1 == null)
          controlNode1 = new ControlNode(control);
        ControlNode controlNode3 = (ControlNode) null;
        if (control.PlaceHolder != null)
        {
          dictionary1.TryGetValue(control.PlaceHolder, out controlNode3);
          if (controlNode3 == null)
            dictionary2.TryGetValue(control.PlaceHolder, out controlNode3);
        }
        if (controlNode3 == null)
        {
          controlNode3 = new ControlNode();
          controlNode3.NodeType = control.PlaceHolder == null || !staticPlaceholders.Contains(control.PlaceHolder) ? ControlNodeType.Orphaned : ControlNodeType.Root;
          if (control.PlaceHolder != null)
            dictionary2[control.PlaceHolder] = controlNode3;
        }
        controlNode1.ContainerIndex = num;
        controlNode3.Children.Add(controlNode1);
      }
      ControlNode node = new ControlNode();
      node.Children.AddRange((IEnumerable<ControlNode>) dictionary2.Values);
      PageHelper.SortControlsTree(node, containerIdsOrdered);
      return node.Children;
    }

    public void SortControlsTree(ControlNode node, List<Guid> containerIdsOrdered)
    {
      List<ControlNode> collection = new List<ControlNode>();
      Queue<ControlNode> source1 = new Queue<ControlNode>((IEnumerable<ControlNode>) node.Children);
      Dictionary<string, List<ControlData>> insertedEmptySiblingControls = new Dictionary<string, List<ControlData>>();
      while (source1.Count > 0)
      {
        ControlNode child = source1.Dequeue();
        if (child.NodeType == ControlNodeType.Normal)
        {
          if (collection.Count == 0 && child.SiblingId == Guid.Empty)
            collection.Add(child);
          else if (child.SiblingId == Guid.Empty)
          {
            int emptySiblingControl = PageHelper.GetInsertIndexOfEmptySiblingControl(child.Control, insertedEmptySiblingControls, containerIdsOrdered);
            collection.Insert(emptySiblingControl, child);
          }
          else
          {
            int index = collection.FindIndex((Predicate<ControlNode>) (s => s.Control.Id == child.SiblingId));
            if (index > -1)
            {
              IEnumerable<ControlNode> source2 = source1.Where<ControlNode>((Func<ControlNode, bool>) (cn => cn.Control.SiblingId == child.SiblingId));
              if (source2.Count<ControlNode>() > 0)
              {
                if (child.ContainerIndex > source2.Min<ControlNode>((Func<ControlNode, int>) (o => o.ContainerIndex)))
                {
                  this.AssertNotCyclic(child.Control, source1.Select<ControlNode, ControlData>((Func<ControlNode, ControlData>) (n => n.Control)));
                  source1.Enqueue(child);
                }
                else
                  collection.Insert(index + 1, child);
              }
              else
                collection.Insert(index + 1, child);
            }
            else if (source1.Any<ControlNode>((Func<ControlNode, bool>) (cn => cn.Control.Id == child.SiblingId)))
            {
              this.AssertNotCyclic(child.Control, source1.Select<ControlNode, ControlData>((Func<ControlNode, ControlData>) (n => n.Control)));
              source1.Enqueue(child);
            }
            else
              collection.Add(child);
          }
        }
        else
          collection.Add(child);
      }
      foreach (ControlNode node1 in collection)
        PageHelper.SortControlsTree(node1, containerIdsOrdered);
      node.Children.Clear();
      node.Children.AddRange((IEnumerable<ControlNode>) collection);
    }

    /// <summary>
    /// This method returns the index at which the given control should be inserted in its placeholder, when the control's
    /// sibling is Guid.Empty. This is normally 0 (e.g. as first control in the placeholder), but not always. If you have
    /// template inheritance, it is possible that more than one control has the same placeholder and Guid.Empty as SiblingId.
    /// In those cases, we must calculate the index based on that.
    /// </summary>
    /// <param name="control"></param>
    /// <returns></returns>
    public int GetInsertIndexOfEmptySiblingControl(
      ControlData control,
      Dictionary<string, List<ControlData>> insertedEmptySiblingControls,
      List<Guid> containerIdsOrdered)
    {
      string placeHolder = control.PlaceHolder;
      List<ControlData> controlDataList;
      insertedEmptySiblingControls.TryGetValue(placeHolder, out controlDataList);
      if (controlDataList == null || controlDataList.Count <= 0)
        return 0;
      int emptySiblingControl = 0;
      int num1 = containerIdsOrdered.IndexOf(control.ContainerId);
      foreach (ControlData controlData in controlDataList)
      {
        int num2 = containerIdsOrdered.IndexOf(controlData.ContainerId);
        if (num1 >= num2)
          ++emptySiblingControl;
        else
          break;
      }
      return emptySiblingControl;
    }

    internal List<ControlData> SortControls(
      IEnumerable<IControlsContainer> controlContainers,
      int count)
    {
      int num = 0;
      List<ControlData> source = new List<ControlData>();
      List<ControlData> controlDataList = new List<ControlData>();
      foreach (IControlsContainer controlContainer in controlContainers)
      {
        bool flag = ++num == count;
        Queue<ControlData> controlDataQueue = new Queue<ControlData>(controlContainer.Controls);
        while (controlDataQueue.Count > 0)
        {
          ControlData ctrl = controlDataQueue.Dequeue();
          ctrl.Editable |= flag;
          if (ctrl.SiblingId != Guid.Empty)
          {
            if (source.FirstOrDefault<ControlData>((Func<ControlData, bool>) (c => c.Id == ctrl.SiblingId)) != null)
            {
              this.AssertNotCyclic(ctrl, (IEnumerable<ControlData>) controlDataQueue);
              source.Add(ctrl);
            }
            else if (controlDataQueue.FirstOrDefault<ControlData>((Func<ControlData, bool>) (c => c.Id == ctrl.SiblingId)) == null)
            {
              controlDataList.Add(ctrl);
            }
            else
            {
              this.AssertNotCyclic(ctrl, (IEnumerable<ControlData>) controlDataQueue);
              controlDataQueue.Enqueue(ctrl);
            }
          }
          else if (source.FirstOrDefault<ControlData>((Func<ControlData, bool>) (c => c.IsLayoutControl && ((IEnumerable<string>) c.PlaceHolders).Contains<string>(ctrl.PlaceHolder, (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase))) != null)
          {
            this.AssertNotCyclic(ctrl, (IEnumerable<ControlData>) controlDataQueue);
            source.Add(ctrl);
          }
          else if (controlDataQueue.FirstOrDefault<ControlData>((Func<ControlData, bool>) (c => c.IsLayoutControl && ((IEnumerable<string>) c.PlaceHolders).Contains<string>(ctrl.PlaceHolder, (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase))) == null)
          {
            ControlData controlData = controlDataList.FirstOrDefault<ControlData>((Func<ControlData, bool>) (c => c.IsLayoutControl && ((IEnumerable<string>) c.PlaceHolders).Contains<string>(ctrl.PlaceHolder, (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase)));
            if (controlData != null)
            {
              controlDataList.Remove(controlData);
              source.Add(controlData);
            }
            source.Add(ctrl);
          }
          else
          {
            this.AssertNotCyclic(ctrl, (IEnumerable<ControlData>) controlDataQueue);
            controlDataQueue.Enqueue(ctrl);
          }
        }
      }
      source.AddRange((IEnumerable<ControlData>) controlDataList);
      return source;
    }

    /// <summary>
    /// Gets the theme of the page in all languages using fallback to templates.
    /// </summary>
    /// <param name="pageData">The page to get theme for.</param>
    /// <returns></returns>
    public Dictionary<CultureInfo, string> GetTheme(PageData pageData) => this.GetTheme(PageTemplateHelper.ResolvePageTemplate(pageData, (Func<PageSiteNode>) (() => pageData.NavigationNode.GetSiteMapNode())), pageData.Themes, (IEnumerable<CultureInfo>) pageData.NavigationNode.AvailableCultures);

    /// <summary>
    /// Gets the theme of the page draft in all languages using fallback to templates.
    /// </summary>
    /// <param name="pageData">The page to get theme for.</param>
    /// <returns></returns>
    public Dictionary<CultureInfo, string> GetTheme(
      PageDraft pageDraft,
      PageTemplate baseTemplate)
    {
      return this.GetTheme(baseTemplate, pageDraft.Themes, (IEnumerable<CultureInfo>) pageDraft.ParentPage.NavigationNode.AvailableCultures);
    }

    /// <summary>
    /// Gets the theme of the template draft in all languages using fallback to templates.
    /// </summary>
    /// <param name="templateDraft">The template to get theme for.</param>
    /// <returns></returns>
    public Dictionary<CultureInfo, string> GetTheme(
      TemplateDraft templateDraft,
      PageTemplate baseTemplate)
    {
      return this.GetTheme(baseTemplate, templateDraft.Themes, (IEnumerable<CultureInfo>) templateDraft.ParentTemplate.AvailableCultures);
    }

    public Dictionary<CultureInfo, string> GetTheme(
      PageTemplate baseTemplate,
      Lstring theme,
      IEnumerable<CultureInfo> availableLanguages)
    {
      Dictionary<CultureInfo, string> theme1 = new Dictionary<CultureInfo, string>();
      if (!SystemManager.CurrentContext.AppSettings.Multilingual)
        availableLanguages = (IEnumerable<CultureInfo>) new CultureInfo[1]
        {
          CultureInfo.InvariantCulture
        };
      else if (!availableLanguages.Contains<CultureInfo>(CultureInfo.InvariantCulture))
      {
        availableLanguages = (IEnumerable<CultureInfo>) new List<CultureInfo>(availableLanguages);
        ((List<CultureInfo>) availableLanguages).Add(CultureInfo.InvariantCulture);
      }
      foreach (CultureInfo availableLanguage in availableLanguages)
      {
        bool flag = true;
        if (theme != (Lstring) null)
        {
          string stringNoFallback = theme.GetStringNoFallback(availableLanguage);
          if (!string.IsNullOrEmpty(stringNoFallback))
          {
            theme1[availableLanguage] = stringNoFallback;
            flag = false;
          }
        }
        if (flag)
        {
          for (PageTemplate pageTemplate = baseTemplate; pageTemplate != null; pageTemplate = pageTemplate.ParentTemplate)
          {
            string stringNoFallback1 = pageTemplate.Themes.GetStringNoFallback(availableLanguage);
            if (!string.IsNullOrEmpty(stringNoFallback1))
            {
              theme1[availableLanguage] = stringNoFallback1;
              break;
            }
            if (pageTemplate.ParentTemplate == null)
            {
              string stringNoFallback2 = pageTemplate.Themes.GetStringNoFallback(SystemManager.CurrentContext.AppSettings.DefaultFrontendLanguage);
              if (!string.IsNullOrEmpty(stringNoFallback2))
              {
                theme1[availableLanguage] = stringNoFallback2;
                break;
              }
            }
          }
        }
      }
      return theme1;
    }

    public string GetThemeName(IDictionary<CultureInfo, string> themes, CultureInfo language = null)
    {
      if (language == null)
        language = SystemManager.CurrentContext.Culture;
      for (; language != null; language = language.Parent)
      {
        string themeName;
        if (themes.TryGetValue(language, out themeName))
          return themeName;
        if (language.Equals((object) CultureInfo.InvariantCulture))
          break;
      }
      return string.Empty;
    }

    /// <summary>Tries to get the full URL path from the cache</summary>
    internal bool TryGetFullUrlFromCache(Guid pageId, string key, out string fullPath)
    {
      PageNodeCacheItem data = (PageNodeCacheItem) SystemManager.GetCacheManager(CacheManagerInstance.PageFullPath).GetData(pageId.ToString());
      if (data != null)
      {
        string str = (string) null;
        if (data.FullPathDictionary.TryGetValue(key, out str))
        {
          fullPath = str;
          return true;
        }
      }
      fullPath = (string) null;
      return false;
    }

    /// <summary>Gets a dictionary of child routes.</summary>
    internal RouteInfoCollection ChildRoutes => this.childRoutes;

    /// <summary>Initializes a partial route handler.</summary>
    /// <param name="obj">The instance of the partial route handler to initialize.</param>
    /// <param name="requestContext">The request context.</param>
    /// <param name="routeKey">The route key.</param>
    internal void SetPartialRouteHandler(
      object obj,
      RequestContext requestContext,
      string routeKey)
    {
      if (!(obj is IPartialRouteHandler partHandler) && obj is ControlPanelBuilder controlPanelBuilder)
        partHandler = controlPanelBuilder.ControlPanel as IPartialRouteHandler;
      if (partHandler == null)
        return;
      string key = partHandler.GetType().FullName + "|" + partHandler.Name;
      RouteInfo routeInfo;
      if (!PageHelper.ChildRoutes.TryGetValue(key, out routeInfo))
      {
        routeInfo = new RouteInfo()
        {
          Key = key,
          Routes = partHandler.CreateRoutes()
        };
        PageHelper.ChildRoutes.Add(routeInfo);
      }
      this.SetPartialRouteHandler(partHandler, routeInfo.Routes, requestContext, routeKey);
    }

    /// <summary>Sets the partial route handler.</summary>
    /// <param name="partHandler">The partial route handler.</param>
    /// <param name="routes">The routes.</param>
    /// <param name="requestContext">The request context.</param>
    /// <param name="routeKey">The route key.</param>
    internal void SetPartialRouteHandler(
      IPartialRouteHandler partHandler,
      RouteCollection routes,
      RequestContext requestContext,
      string routeKey)
    {
      if (string.IsNullOrEmpty(routeKey))
        routeKey = "Params";
      partHandler.ParentRouteHandler = requestContext.HttpContext.Handler as IPartialRouteHandler;
      PartialHttpContext httpContext;
      RouteData routeData;
      if (routes != null)
      {
        string[] strArray = (string[]) requestContext.RouteData.Values[routeKey];
        httpContext = new PartialHttpContext(strArray != null ? string.Join("/", strArray) : (string) null);
        routeData = routes.GetRouteData((HttpContextBase) httpContext) ?? new RouteData();
      }
      else
      {
        httpContext = new PartialHttpContext(string.Empty);
        routeData = new RouteData();
      }
      partHandler.PartialRequestContext = new PartialRequestContext(httpContext, routeData, routeKey);
    }

    /// <summary>
    /// Gets the page node for the given language. If the node is in SYNCED mode, the same node is returned.
    /// If the node is in SPLIT mode, the linked pages are searched for a page in the given language. If such
    /// page is not found, null is returned. If such page is found, a random(the first) of its nodes is returned.
    /// If the node is a split group  or split external will return the corresponding for the culture node
    /// </summary>
    /// <param name="node">The source node.</param>
    /// <param name="language">The language.</param>
    /// <returns></returns>
    [Obsolete("As of Sitefinity 7.0 there is only one PageNode for the split strategy. Returns the PageNode passed as argument.")]
    public PageNode GetPageNodeForLanguage(PageNode node, CultureInfo language) => node;

    [Obsolete("As of Sitefinity 7.0 there is only one PageNode for the split strategy. Returns the PageData's navigation node.")]
    public PageNode GetPageNodeForLanguage(PageData page, CultureInfo language) => page.NavigationNode;

    /// <summary>
    /// Returns a value indicating whether the given page node has a translation in the given language.
    /// </summary>
    /// <param name="node">The node to be checked.</param>
    /// <param name="language">The culture to be looked for.</param>
    /// <returns></returns>
    public bool PageNodeHasTranslation(PageNode node, CultureInfo language)
    {
      bool flag = true;
      if (node.LocalizationStrategy == LocalizationStrategy.Split)
      {
        PageData pageData = node.GetPageData(language);
        flag = ((IEnumerable<CultureInfo>) node.AvailableCultures).Contains<CultureInfo>(language) && pageData != null && pageData.Visible;
      }
      return flag;
    }

    /// <summary>
    /// Determines whether the specified node is a node of a backend page.
    /// </summary>
    /// <param name="node">The node to check.</param>
    /// <returns>
    /// 	<c>true</c> if the specified node is a node of a backend page; otherwise, <c>false</c>.
    /// </returns>
    public bool IsPageNodeBackend(PageNode node)
    {
      bool flag = false;
      if (node.RootNode != null)
        flag = node.RootNode.Id == SiteInitializer.BackendRootNodeId;
      return flag;
    }

    /// <summary>
    /// Determines whether the specified page node is relevant for the specified language.
    /// A page is considered relevant if it is in SYNCED mode or if its UiCulture is equal
    /// to that language.
    /// </summary>
    /// <param name="pageNode">The page node.</param>
    /// <param name="language">The language.</param>
    /// <returns>
    /// 	<c>true</c> if the specified page node is relevant for the specified language; otherwise, <c>false</c>.
    /// </returns>
    [Obsolete("Don't use this method, because it is not relevant any more")]
    public bool IsPageNodeForLanguage(PageNode pageNode, CultureInfo language) => ((IEnumerable<CultureInfo>) pageNode.AvailableCultures).Contains<CultureInfo>(language);

    /// <summary>Filters the pages for language.</summary>
    /// <param name="pageNodes">The page nodes.</param>
    /// <param name="language">The language.</param>
    /// <returns></returns>
    [Obsolete("This method does not filter nodes anymore (except for duplicates).")]
    public IList<PageNode> FilterPagesForLanguage(
      IQueryable<PageNode> pageNodes,
      CultureInfo language)
    {
      return (IList<PageNode>) pageNodes.Distinct<PageNode>().ToList<PageNode>();
    }

    internal string GetPageEditUrl(string pageUrl) => pageUrl + "/" + "Action" + "/" + "Edit";

    internal void DeleteLinkingPageNodes(PageNode linkedNode, PageManager manager) => this.LinkingPageNodesDo(linkedNode, manager, (Action<PageManager, PageNode>) ((pageManager, linkingNode) => pageManager.Delete(linkingNode)));

    /// <summary>
    /// Performs an action for each linking node that points to the <paramref name="linkedNode" />.
    /// Iterates over all possible nodes that are linking to the <paramref name="linkedNode" />.
    /// </summary>
    /// <param name="linkedNode">The linked node.</param>
    /// <param name="pageManager">The page manager.</param>
    /// <param name="linkedNodeAction">The linked node action.</param>
    internal void LinkingPageNodesDo(
      PageNode linkedNode,
      PageManager pageManager,
      Action<PageManager, PageNode> linkedNodeAction)
    {
      IQueryable<PageNode> pageNodes = pageManager.GetPageNodes();
      Expression<Func<PageNode, bool>> predicate = (Expression<Func<PageNode, bool>>) (pn => pn.LinkedNodeId == linkedNode.Id && (int) pn.NodeType == 3);
      foreach (PageNode pageNode in (IEnumerable<PageNode>) pageNodes.Where<PageNode>(predicate))
        linkedNodeAction(pageManager, pageNode);
    }

    internal void DeletePersonalizedPages(PageNode pageNode, PageManager manager)
    {
      PageData pageData = pageNode.GetPageData();
      if (pageData == null)
        return;
      IQueryable<PageData> pageDataList = manager.GetPageDataList();
      Expression<Func<PageData, bool>> predicate = (Expression<Func<PageData, bool>>) (p => p.PersonalizationMasterId == pageData.Id);
      foreach (PageData pageData1 in (IEnumerable<PageData>) pageDataList.Where<PageData>(predicate))
        manager.Delete(pageData1);
    }

    /// <summary>
    /// Deletes the page nodes tree with the specified root page node.
    /// </summary>
    /// <param name="rootNode">The root node.</param>
    /// <param name="manager">The manager.</param>
    internal void DeletePageNodesTree(PageNode rootNode, PageManager manager)
    {
      if (rootNode.Nodes != null && !rootNode.Nodes.Any<PageNode>((Func<PageNode, bool>) (n => n.Nodes != null && n.Nodes.Count > 0)))
      {
        for (int index = rootNode.Nodes.Count - 1; index >= 0; --index)
          manager.Delete(rootNode.Nodes[index]);
        manager.SaveChanges();
      }
      for (int index = rootNode.Nodes.Count - 1; index >= 0; --index)
        this.DeletePageNodesTree(rootNode.Nodes[index], manager);
      manager.Delete(rootNode);
      manager.SaveChanges();
    }

    internal PlaceHoldersCollection CreateHandlerPlaceHolders(Page handler)
    {
      if (handler == null)
        throw new ArgumentNullException(nameof (handler));
      PlaceHoldersCollection source = new PlaceHoldersCollection();
      foreach (Control control in new ControlTraverser((Control) handler, TraverseMethod.DepthFirst))
      {
        Control ctrl = control;
        bool flag = false;
        if (ctrl is ContentPlaceHolder)
        {
          if (ctrl.TemplateControl == handler.Master)
            flag = true;
        }
        else if (ctrl is SitefinityPlaceHolder && (ctrl.TemplateControl == handler || ctrl.TemplateControl == handler.Master))
          flag = true;
        if (flag)
        {
          if (source.Any<Control>((Func<Control, bool>) (item => item.ID == ctrl.ID)))
            source.Remove(ctrl.ID);
          source.Add(ctrl);
        }
      }
      return source;
    }

    internal List<PageTemplate> GetTemplates(PageData pageData)
    {
      PageTemplate template = pageData.Template;
      List<PageTemplate> templates = new List<PageTemplate>();
      for (PageTemplate pageTemplate = template; pageTemplate != null; pageTemplate = pageTemplate.ParentTemplate)
        templates.Add(pageTemplate);
      return templates;
    }

    /// <summary>
    /// Gets the ids of the segments used for personalization of any widget on the page.
    /// </summary>
    /// <param name="pageDataId">The page data ID.</param>
    /// <param name="templateIds">The template ids.</param>
    /// <param name="pageManager">The page manager.</param>
    /// <returns>
    /// A lookup containing all segments used on the page grouped by control ID
    /// </returns>
    internal ILookup<Guid, Guid> GetWidgetSegmentIds(
      Guid pageDataId,
      IList<Guid> templateIds,
      PageManager pageManager = null)
    {
      pageManager = pageManager ?? PageManager.GetManager();
      return pageManager.GetControlsSegmentIds(pageDataId, templateIds);
    }
  }
}
