// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Abstractions.VirtualPath.PageResolverBase
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Web.Compilation;
using System.Web.Hosting;
using System.Web.Routing;
using System.Web.UI;
using Telerik.Microsoft.Practices.Unity;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Modules.ResponsiveDesign;
using Telerik.Sitefinity.Modules.ResponsiveDesign.Data;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Personalization;
using Telerik.Sitefinity.ResponsiveDesign.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Configuration;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities.HtmlParsing;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit;

namespace Telerik.Sitefinity.Abstractions.VirtualPath
{
  /// <summary>
  /// Base implementation for the virtual path provider virtual file resolver
  /// for Sitefinity pages.
  /// </summary>
  public abstract class PageResolverBase : IVirtualFileResolver, IHashedVirtualFileResolver
  {
    /// <summary>The name of the required controls placeholder.</summary>
    public const string RequiredControlsPlaceHolderName = "sf_RequiredControls";
    /// <summary>The name of the head placeholder.</summary>
    public const string HeadPlaceHolderName = "sf_Head";
    /// <summary>The name of the body close placeholder.</summary>
    public const string BodyClosePlaceHolderName = "sf_Body_Close";
    private IList<RequiresEmbeddedWebResourceAttribute> embeddedResources;

    /// <summary>Writes all holders.</summary>
    /// <param name="placeHolders">The place holders.</param>
    /// <param name="output">The output.</param>
    /// <param name="name">The name.</param>
    /// <param name="pageData">The page data.</param>
    /// <param name="theme">The theme.</param>
    protected virtual void WriteAllHolders(
      CursorCollection placeHolders,
      StringBuilder output,
      string name,
      PageData pageData,
      string theme)
    {
      int num = 0;
      foreach (PlaceHolderCursor placeHolderCursor in placeHolders.Where<PlaceHolderCursor>((Func<PlaceHolderCursor, bool>) (p => p.ParentHolder == name)))
      {
        this.WriteAllHolders(placeHolders, placeHolderCursor.Output, placeHolderCursor.Name, pageData, theme);
        output.Insert(placeHolderCursor.Position + num, (object) placeHolderCursor.Output);
        num += placeHolderCursor.Output.Length;
      }
    }

    /// <summary>Builds the controls.</summary>
    /// <param name="pageData">The page data.</param>
    /// <param name="controlContainers">The control containers.</param>
    /// <param name="placeHolders">The place holders.</param>
    /// <param name="directives">The directives.</param>
    /// <exception cref="T:System.ArgumentException">Invalid layout template.</exception>
    /// <exception cref="T:System.InvalidOperationException">Could not find suitable place holder for required control.</exception>
    protected virtual void BuildControls(
      PageData pageData,
      List<IControlsContainer> controlContainers,
      CursorCollection placeHolders,
      DirectiveCollection directives)
    {
      controlContainers.Reverse();
      List<ControlData> controls;
      if (!Config.Get<PagesConfig>().UseOldControlsOrderAlgorithm)
      {
        List<string> staticPlaceholders = new List<string>(placeHolders.Select<PlaceHolderCursor, string>((Func<PlaceHolderCursor, string>) (p => p.Name)));
        controls = PageHelper.GetOrderedControlsCollection(PageHelper.GetControlsHierarchical((IEnumerable<IControlsContainer>) controlContainers, staticPlaceholders));
      }
      else
        controls = PageHelper.SortControls((IEnumerable<IControlsContainer>) controlContainers, controlContainers.Count);
      PageHelper.ProcessOverridenControls(pageData, (IEnumerable<IControlsContainer>) controlContainers);
      this.EmbeddedResources.Clear();
      bool flag = false;
      foreach (ControlData controlData in controls)
      {
        PlaceHolderCursor parentPlaceHolder;
        if (ToolboxesConfig.Current.ValidateWidget(controlData) && placeHolders.TryGetValue(controlData.PlaceHolder, out parentPlaceHolder))
        {
          StringBuilder output = parentPlaceHolder.Output;
          Type type;
          if (controlData.ObjectType.StartsWith("~/"))
          {
            type = BuildManager.GetCompiledType(controlData.ObjectType);
            if (typeof (ILayoutControl).IsAssignableFrom(type))
            {
              ControlProperty controlProperty = controlData.Properties.Single<ControlProperty>((Func<ControlProperty, bool>) (p => p.Name == "ID"));
              this.AppendLayout(controlData.ObjectType, (string) null, parentPlaceHolder, placeHolders, controlProperty.Value, directives);
            }
            else
              this.RenderUserControlWrapper(pageData, output, placeHolders, controlData);
          }
          else
          {
            type = TypeResolutionService.ResolveType(controlData.ObjectType, true);
            bool needsWrapper;
            string securedObjectTagName;
            if (typeof (LayoutControl).IsAssignableFrom(type))
            {
              this.StartSecuredControl(placeHolders, controlData, output, true, out needsWrapper, out securedObjectTagName);
              IEnumerable<ControlProperty> properties = controlData.GetProperties(true);
              ControlProperty controlProperty1 = properties.SingleOrDefault<ControlProperty>((Func<ControlProperty, bool>) (p => p.Name == "Layout"));
              string layoutTemplate = (string) null;
              if (controlProperty1 != null)
                layoutTemplate = controlProperty1.Value;
              if (string.IsNullOrEmpty(layoutTemplate))
              {
                try
                {
                  if (Activator.CreateInstance(type) is LayoutControl instance)
                    layoutTemplate = instance.Layout;
                }
                catch
                {
                }
              }
              if (layoutTemplate.IsNullOrEmpty())
                throw new ArgumentException("Invalid layout template.");
              ControlProperty controlProperty2 = properties.Single<ControlProperty>((Func<ControlProperty, bool>) (p => p.Name == "ID"));
              ControlProperty controlProperty3 = properties.SingleOrDefault<ControlProperty>((Func<ControlProperty, bool>) (p => p.Name == "AssemblyInfo"));
              string assemblyInfo = controlProperty3 == null ? string.Empty : controlProperty3.Value;
              this.AppendLayout(layoutTemplate, assemblyInfo, parentPlaceHolder, placeHolders, controlProperty2.Value, directives);
              this.EndSecuredControl(output, needsWrapper, securedObjectTagName);
            }
            else
              this.RenderControl(pageData, output, placeHolders, controlData, type, out needsWrapper, out securedObjectTagName);
          }
          flag = flag || this.CheckWidgetRequiresLayoutTransformation(type, controlData);
          Type controlType = TypeResolutionService.ResolveType(ObjectFactory.Resolve<IControlBehaviorResolver>().GetBehaviorObjectType(controlData), false);
          if ((object) controlType == null)
            controlType = type;
          foreach (RequiresEmbeddedWebResourceAttribute resourceAttribute in (IEnumerable<RequiresEmbeddedWebResourceAttribute>) ControlExtensions.GetRequiredEmbeddedWebResourceAttributes(controlType))
            this.EmbeddedResources.Add(resourceAttribute);
        }
      }
      PlaceHolderCursor placeHolder;
      if (!placeHolders.TryGetValue("sf_RequiredControls", out placeHolder))
      {
        int index = 0;
        if (placeHolders[index].Name == "sf_Namespaces")
          ++index;
        if (index < placeHolders.Count)
          placeHolder = placeHolders[index];
      }
      if (placeHolder == null)
        throw new InvalidOperationException("Could not find suitable place holder for required control.");
      string str1 = placeHolders.GetTagPrefix("Telerik.Sitefinity.Web", "Telerik.Sitefinity", "sf") + ":";
      placeHolder.Output.AppendFormat("<{0}SitefinityRequiredControls runat=\"server\" />", (object) str1).AppendLine();
      if (this.EmbeddedResources.Count > 0)
      {
        IEnumerable<RequiresEmbeddedWebResourceAttribute> resourceAttributes = this.EmbeddedResources.Distinct<RequiresEmbeddedWebResourceAttribute>();
        str1 = placeHolders.GetTagPrefix("Telerik.Sitefinity.Web.UI", "Telerik.Sitefinity", "sf") + ":";
        placeHolder.Output.AppendFormat("<{0}ResourceLinks runat=\"server\" UseEmbeddedThemes=\"True\" Theme=\"Basic\">", (object) str1).AppendLine().AppendLine("<Links>");
        foreach (RequiresEmbeddedWebResourceAttribute resourceAttribute in resourceAttributes)
          placeHolder.Output.AppendFormat("<{0}ResourceFile Static=\"True\" Name=\"{1}\" AssemblyInfo=\"{2}\"/>", (object) str1, (object) resourceAttribute.Name, (object) resourceAttribute.TypeFullName).AppendLine();
        placeHolder.Output.AppendLine("</Links>").AppendFormat("</{0}ResourceLinks>", (object) str1).AppendLine();
      }
      if (flag && SystemManager.IsModuleAccessible("ResponsiveDesign"))
      {
        Guid id = pageData.Id;
        RequestContext requestContext = (RequestContext) null;
        PageSiteNode requestedPageNode = this.GetRequestedPageNode(out requestContext);
        if (ResponsiveDesignCache.GetInstance().GetMediaQueries(requestedPageNode.CurrentPageDataItem, ResponsiveDesignBehavior.TransformLayout).Any<IMediaQuery>())
        {
          string str2 = "<{0}ResourceFile Name=\"~/Sitefinity/Public/ResponsiveDesign/layout_transformations.css?pageDataId={1}&pageSiteNode={2}\" Static=\"true\"/>".Arrange((object) str1, (object) id, (object) new PageSiteNodeResolver(requestedPageNode));
          if (pageData.LocalizationStrategy == LocalizationStrategy.Split)
            str2 = "<{0}ResourceFile Name=\"~/Sitefinity/Public/ResponsiveDesign/layout_transformations.css?pageDataId={1}&pageSiteNode={2}&culture={3}\" Static=\"true\"/>".Arrange((object) str1, (object) id, (object) new PageSiteNodeResolver(requestedPageNode), (object) pageData.Culture);
          placeHolder.Output.AppendFormat("<{0}ResourceLinks runat=\"server\" UseEmbeddedThemes=\"False\">", (object) str1).AppendLine().AppendLine("<Links>").AppendLine(str2).AppendLine().AppendLine("</Links>").AppendFormat("</{0}ResourceLinks>", (object) str1).AppendLine();
        }
      }
      PageHelper.ClearOverridenControls(controls);
    }

    /// <summary>
    /// Checks if a layout transformation CSS has to be injected in the page - for widgets like layouts and responsive navigation
    /// </summary>
    /// <param name="widgetType">The widget type.</param>
    /// <param name="controlData">The control data.</param>
    /// <returns>A value indicating whether a layout transformation CSS has to be injected in the page.</returns>
    protected virtual bool CheckWidgetRequiresLayoutTransformation(
      Type widgetType,
      ControlData controlData)
    {
      return widgetType.GetCustomAttributes(typeof (RequiresLayoutTransformationAttribute), true).Length != 0;
    }

    /// <summary>Renders the user control wrapper.</summary>
    /// <param name="pageData">The page data.</param>
    /// <param name="output">The output.</param>
    /// <param name="placeHolders">The place holders.</param>
    /// <param name="ctrlData">The control data.</param>
    private void RenderUserControlWrapper(
      PageData pageData,
      StringBuilder output,
      CursorCollection placeHolders,
      ControlData ctrlData)
    {
      bool needsWrapper;
      string securedObjectTagName;
      this.StartSecuredControl(placeHolders, ctrlData, output, false, out needsWrapper, out securedObjectTagName);
      List<ControlProperty> list = ctrlData.GetProperties(true).Where<ControlProperty>((Func<ControlProperty, bool>) (x => x.Value != null)).ToList<ControlProperty>();
      Type type = typeof (UserControlWrapper);
      string tagPrefix = placeHolders.GetTagPrefix(type.Namespace, type.Assembly.GetName().Name, "sf");
      output.AppendFormat("<{0}:{1} VirtualPath=\"{2}\" runat=\"server\">", (object) tagPrefix, (object) type.Name, (object) ctrlData.ObjectType).AppendLine();
      if (list.Count > 0)
      {
        output.AppendLine("<Properties>");
        string format = "<sf:Property Name=\"{0}\" Value=\"{1}\" />";
        foreach (ControlProperty controlProperty in list)
          output.AppendFormat(format, (object) controlProperty.Name, (object) this.HtmlEncode(controlProperty.Value)).AppendLine();
        output.AppendLine("</Properties>");
      }
      output.AppendFormat("</{0}:{1}>", (object) tagPrefix, (object) type.Name).AppendLine();
      this.EndSecuredControl(output, needsWrapper, securedObjectTagName);
    }

    /// <summary>Renders the control.</summary>
    /// <param name="pageData">The page data.</param>
    /// <param name="output">The output.</param>
    /// <param name="placeHolders">The place holders.</param>
    /// <param name="ctrlData">The control data.</param>
    /// <param name="ctrlType">Type of the control.</param>
    /// <param name="needsWrapper">if set to <c>true</c> [needs wrapper].</param>
    /// <param name="securedObjectTagName">Name of the secured object tag.</param>
    protected virtual void RenderControl(
      PageData pageData,
      StringBuilder output,
      CursorCollection placeHolders,
      ControlData ctrlData,
      Type ctrlType,
      out bool needsWrapper,
      out string securedObjectTagName)
    {
      int num = !SystemManager.IsModuleAccessible("Personalization") ? 0 : (ctrlData.IsPersonalized ? 1 : 0);
      this.StartSecuredControl(placeHolders, ctrlData, output, false, out needsWrapper, out securedObjectTagName);
      string empty1 = string.Empty;
      string empty2 = string.Empty;
      IPersonalizedWidgetResolver personalizedWidgetResolver = (IPersonalizedWidgetResolver) null;
      string tagPrefix;
      string name;
      if (num != 0)
      {
        personalizedWidgetResolver = ObjectFactory.Resolve<IPersonalizedWidgetResolver>();
        Type type = personalizedWidgetResolver.ResolveWrapperType(ctrlType);
        tagPrefix = placeHolders.GetTagPrefix(type.Namespace, type.Assembly.GetName().Name, "sf");
        name = type.Name;
      }
      else
      {
        tagPrefix = placeHolders.GetTagPrefix(ctrlType.Namespace, ctrlType.Assembly.GetName().Name, "sf");
        name = ctrlType.Name;
      }
      output.Append("<").Append(tagPrefix).Append(":").Append(name);
      bool appendAllProperties = true;
      if (num != 0)
        personalizedWidgetResolver.AppendPersonalizationProperties(output, ctrlData, ctrlType, pageData.Id, out appendAllProperties);
      else if (typeof (MvcProxyBase).IsAssignableFrom(ctrlType))
        output.Append(string.Format(" ControlDataId=\"{0}\"", (object) ctrlData.Id));
      if (appendAllProperties)
      {
        PageTemplateFramework framework = pageData.Template != null ? pageData.Template.Framework : PageTemplateFramework.WebForms;
        this.AppendProperties(output, (ObjectData) ctrlData, ctrlType, placeHolders, framework);
      }
      output.Append("</").Append(tagPrefix).Append(":").Append(name).Append(">");
      this.EndSecuredControl(output, needsWrapper, securedObjectTagName);
    }

    /// <summary>Starts the secured control.</summary>
    /// <param name="placeHolders">The place holders.</param>
    /// <param name="ctrlData">The control data.</param>
    /// <param name="output">The output.</param>
    /// <param name="isLayout">if set to <c>true</c> [is layout].</param>
    /// <param name="needsWrapper">if set to <c>true</c> [needs wrapper].</param>
    /// <param name="securedObjectTagName">Name of the secured object tag.</param>
    protected void StartSecuredControl(
      CursorCollection placeHolders,
      ControlData ctrlData,
      StringBuilder output,
      bool isLayout,
      out bool needsWrapper,
      out string securedObjectTagName)
    {
      needsWrapper = this.NeedsWrapper(ctrlData, isLayout);
      string tagPrefix = placeHolders.GetTagPrefix("Telerik.Sitefinity.Web.UI", "Telerik.Sitefinity", "sf");
      securedObjectTagName = tagPrefix + ":SecuredControl";
      if (!needsWrapper)
        return;
      string str = SitefinityControlPersister.PersistObject((object) new SecuredObjectInfo((ISecuredObject) ctrlData), placeHolders);
      output.Append("<").Append(securedObjectTagName).Append(" runat=\"server\">").Append("<Info>").Append(str).Append("</Info>").Append("<Control>");
      this.SetContextSecuredControls(true);
    }

    /// <summary>Appends the layout.</summary>
    /// <param name="layoutTemplate">The layout template.</param>
    /// <param name="assemblyInfo">The assembly information.</param>
    /// <param name="parentPlaceHolder">The parent place holder.</param>
    /// <param name="placeHolders">The place holders.</param>
    /// <param name="layoutId">The layout identifier.</param>
    /// <param name="directives">The directives.</param>
    /// <exception cref="T:System.ArgumentException">Invalid layout template.</exception>
    protected virtual void AppendLayout(
      string layoutTemplate,
      string assemblyInfo,
      PlaceHolderCursor parentPlaceHolder,
      CursorCollection placeHolders,
      string layoutId,
      DirectiveCollection directives)
    {
      string template;
      if (layoutTemplate.StartsWith("~/"))
      {
        using (Stream stream = SitefinityFile.Open(layoutTemplate, false))
        {
          using (StreamReader streamReader = new StreamReader(stream))
            template = streamReader.ReadToEnd();
        }
      }
      else if (layoutTemplate.EndsWith(".ascx", StringComparison.OrdinalIgnoreCase))
      {
        Type assemblyInfo1 = !string.IsNullOrEmpty(assemblyInfo) ? TypeResolutionService.ResolveType(assemblyInfo, true) : Config.Get<ControlsConfig>().ResourcesAssemblyInfo;
        template = ControlUtilities.GetTextResource(layoutTemplate, assemblyInfo1);
      }
      else
        template = layoutTemplate.Trim().StartsWith("<") ? layoutTemplate : throw new ArgumentException("Invalid layout template.");
      this.ProcessLayoutString(template, parentPlaceHolder, placeHolders, layoutId, directives);
    }

    /// <summary>Ends the secured control.</summary>
    /// <param name="output">The output.</param>
    /// <param name="needsWrapper">if set to <c>true</c> [needs wrapper].</param>
    /// <param name="securedObjectTagName">Name of the secured object tag.</param>
    protected void EndSecuredControl(
      StringBuilder output,
      bool needsWrapper,
      string securedObjectTagName)
    {
      if (!needsWrapper)
        return;
      output.Append("</Control>").Append("</").Append(securedObjectTagName).Append(">");
    }

    /// <summary>Appends the properties.</summary>
    /// <param name="output">The output.</param>
    /// <param name="ctrlData">The control data.</param>
    /// <param name="ctrlType">Type of the control.</param>
    /// <param name="placeHolders">The place holders.</param>
    /// <param name="framework">The framework.</param>
    protected virtual void AppendProperties(
      StringBuilder output,
      ObjectData ctrlData,
      Type ctrlType,
      CursorCollection placeHolders,
      PageTemplateFramework framework)
    {
      output.Append(" ");
      StringBuilder stringBuilder = new StringBuilder();
      List<ControlProperty> list = ctrlData.GetProperties(true).ToList<ControlProperty>();
      PropertyDescriptorCollection properties;
      if (typeof (MvcProxyBase).IsAssignableFrom(ctrlType))
      {
        MvcProxyBase component = (MvcProxyBase) PageManager.GetManager().LoadControl(ctrlData, (CultureInfo) null);
        properties = TypeDescriptor.GetProperties((object) component);
        // ISSUE: reference to a compiler-generated field
        if (PageResolverBase.\u003C\u003Eo__8.\u003C\u003Ep__1 == null)
        {
          // ISSUE: reference to a compiler-generated field
          PageResolverBase.\u003C\u003Eo__8.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, PropertyDescriptorCollection>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (PropertyDescriptorCollection), typeof (PageResolverBase)));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, PropertyDescriptorCollection> target = PageResolverBase.\u003C\u003Eo__8.\u003C\u003Ep__1.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, PropertyDescriptorCollection>> p1 = PageResolverBase.\u003C\u003Eo__8.\u003C\u003Ep__1;
        // ISSUE: reference to a compiler-generated field
        if (PageResolverBase.\u003C\u003Eo__8.\u003C\u003Ep__0 == null)
        {
          // ISSUE: reference to a compiler-generated field
          PageResolverBase.\u003C\u003Eo__8.\u003C\u003Ep__0 = CallSite<Func<CallSite, Type, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "GetProperties", (IEnumerable<Type>) null, typeof (PageResolverBase), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj = PageResolverBase.\u003C\u003Eo__8.\u003C\u003Ep__0.Target((CallSite) PageResolverBase.\u003C\u003Eo__8.\u003C\u003Ep__0, typeof (TypeDescriptor), component.Settings);
        PropertyDescriptorCollection descriptorCollection = target((CallSite) p1, obj);
        this.AppendAttributes(output, properties, (IList<ControlProperty>) list, ctrlType);
        this.AppendMvcModeAttribute(output, framework);
      }
      else
      {
        properties = TypeDescriptor.GetProperties(ctrlType);
        this.AppendAttributes(output, (IList<ControlProperty>) list, ctrlType);
      }
      output.Append(" runat=\"server\">\r\n");
      foreach (ControlProperty controlProperty in list)
      {
        PropertyDescriptor propertyDescriptor = properties.Find(controlProperty.Name, true);
        if (propertyDescriptor != null)
        {
          PersistenceModeAttribute attribute = (PersistenceModeAttribute) propertyDescriptor.Attributes[typeof (PersistenceModeAttribute)];
          if (attribute != null)
          {
            switch (attribute.Mode)
            {
              case PersistenceMode.InnerProperty:
                this.AppendInnerProperties(output, controlProperty, propertyDescriptor.PropertyType, placeHolders, framework);
                continue;
              case PersistenceMode.InnerDefaultProperty:
                this.AppendInnerDefaultProperty(output, controlProperty, false);
                continue;
              case PersistenceMode.EncodedInnerDefaultProperty:
                this.AppendInnerDefaultProperty(output, controlProperty, true);
                continue;
              default:
                continue;
            }
          }
        }
      }
      if (stringBuilder.Length <= 0)
        return;
      output.AppendLine(stringBuilder.ToString());
    }

    /// <summary>Appends the browse and edit feature.</summary>
    /// <param name="output">The output.</param>
    /// <param name="ctrlType">Type of the control.</param>
    /// <param name="pageData">The page data.</param>
    /// <param name="ctrlData">The control data.</param>
    protected virtual void AppendBrowseAndEdit(
      StringBuilder output,
      Type ctrlType,
      PageData pageData,
      ControlData ctrlData)
    {
      if (!typeof (IBrowseAndEditable).IsAssignableFrom(ctrlType))
        return;
      StringBuilder stringBuilder = output;
      Guid id = ctrlData.Id;
      string str1 = id.ToString();
      id = pageData.Id;
      string str2 = id.ToString();
      string objectType = ctrlData.ObjectType;
      stringBuilder.AppendFormat("<BrowseAndEditableInfo ControlDataId=\"{0}\" PageId=\"{1}\" ControlType=\"{2}\" />", (object) str1, (object) str2, (object) objectType);
    }

    /// <summary>Sets the page directives.</summary>
    /// <param name="pageData">The page data.</param>
    /// <param name="directives">The directives.</param>
    protected virtual void SetPageDirectives(PageData pageData, DirectiveCollection directives)
    {
      NameValueCollection nameValueCollection = new NameValueCollection((IEqualityComparer) StringComparer.OrdinalIgnoreCase);
      nameValueCollection["Language"] = "C#";
      string str = this.HtmlEncode(this.HtmlEncode((string) pageData.HtmlTitle));
      this.AddPageDirectiveValue(nameValueCollection, "Title", str);
      this.AddPageDirectiveValue(nameValueCollection, "EnableViewState", (object) pageData.EnableViewState, (object) true);
      this.AddPageDirectiveValue(nameValueCollection, "EnableViewStateMac", (object) pageData.EnableViewStateMac, (object) true);
      this.AddPageDirectiveValue(nameValueCollection, "ViewStateEncryptionMode", (object) pageData.ViewStateEncryption, (object) ViewStateEncryptionMode.Auto);
      this.AddPageDirectiveValue(nameValueCollection, "MaintainScrollPositionOnPostback", (object) pageData.MaintainScrollPositionOnPostback, (object) false);
      this.AddPageDirectiveValue(nameValueCollection, "Trace", (object) pageData.Trace, (object) false);
      this.AddPageDirectiveValue(nameValueCollection, "TraceMode", (object) pageData.TraceMode, (object) TraceMode.SortByTime);
      this.AddPageDirectiveValue(nameValueCollection, "ErrorPage", pageData.ErrorPage);
      this.AddPageDirectiveValue(nameValueCollection, "EnableTheming", (object) pageData.EnableTheming, (object) true);
      this.AddPageDirectiveValue(nameValueCollection, "EnableEventValidation", (object) pageData.EnableEventValidation, (object) true);
      this.AddPageDirectiveValue(nameValueCollection, "EnableSessionState", (object) pageData.EnableSessionState, (object) true);
      this.AddPageDirectiveValue(nameValueCollection, "ResponseEncoding", pageData.ResponseEncoding);
      this.AddPageDirectiveValue(nameValueCollection, "ValidateRequest", (object) pageData.ValidateRequest, (object) true);
      this.AddPageDirectiveValue(nameValueCollection, "MasterPageFile", pageData.MasterPage);
      this.AddPageDirectiveValue(nameValueCollection, "MetaDescription", this.HtmlEncode((string) pageData.Description));
      this.AddPageDirectiveValue(nameValueCollection, "MetaKeywords", this.HtmlEncode((string) pageData.Keywords));
      this.AddPageDirectiveValue(nameValueCollection, "Buffer", (object) pageData.BufferOutput, (object) true);
      this.AddPageDirectiveValue(nameValueCollection, "Inherits", pageData.CodeBehindType);
      directives.Add(new Directive("Page", nameValueCollection));
    }

    /// <summary>Adds the page directive value.</summary>
    /// <param name="pageAttribs">The page attributes.</param>
    /// <param name="name">The name.</param>
    /// <param name="value">The value.</param>
    protected void AddPageDirectiveValue(
      NameValueCollection pageAttribs,
      string name,
      string value)
    {
      if (string.IsNullOrEmpty(value))
        return;
      pageAttribs[name] = value;
    }

    /// <summary>Adds the page directive value.</summary>
    /// <param name="pageAttribs">The page attributes.</param>
    /// <param name="name">The name.</param>
    /// <param name="value">The value.</param>
    /// <param name="defaultValue">The default value.</param>
    protected void AddPageDirectiveValue(
      NameValueCollection pageAttribs,
      string name,
      object value,
      object defaultValue)
    {
      if (value.Equals(defaultValue))
        return;
      pageAttribs[name] = value.ToString();
    }

    private bool NeedsWrapper(ControlData ctrlData, bool isLayout)
    {
      string key = isLayout ? "ViewLayout" : "ViewControl";
      string str = isLayout ? "LayoutElement" : "Controls";
      int actions = Config.Get<SecurityConfig>().Permissions[str].Actions[key].Value;
      if (!ctrlData.IsGranted(str, new Guid[1]
      {
        SecurityManager.EveryoneRole.Id
      }, actions))
        return true;
      bool flag = false;
      foreach (Telerik.Sitefinity.Security.Model.Permission activePermission in (IEnumerable<Telerik.Sitefinity.Security.Model.Permission>) ctrlData.GetActivePermissions())
      {
        if (activePermission.IsDenied(key))
        {
          flag = true;
          break;
        }
      }
      return flag;
    }

    /// <summary>Sets the context secured controls.</summary>
    /// <param name="value">if set to <c>true</c> [value].</param>
    protected void SetContextSecuredControls(bool value)
    {
      HttpContextBase currentHttpContext = SystemManager.CurrentHttpContext;
      if (currentHttpContext == null)
        return;
      currentHttpContext.Items[(object) Telerik.Sitefinity.Web.PageRouteHandler.HttpContextSecuredControls] = (object) value;
    }

    /// <summary>Processes the layout string.</summary>
    /// <param name="template">The template.</param>
    /// <param name="parentPlaceHolder">The parent place holder.</param>
    /// <param name="placeholders">The placeholders.</param>
    /// <param name="layoutId">The layout identifier.</param>
    /// <param name="directives">The directives.</param>
    protected virtual void ProcessLayoutString(
      string template,
      PlaceHolderCursor parentPlaceHolder,
      CursorCollection placeholders,
      string layoutId,
      DirectiveCollection directives)
    {
      using (HtmlParser htmlParser = new HtmlParser(template))
      {
        htmlParser.SetChunkHashMode(false);
        htmlParser.AutoExtractBetweenTagsOnly = false;
        htmlParser.CompressWhiteSpaceBeforeTag = false;
        htmlParser.KeepRawHTML = true;
        int num = 0;
        StringBuilder output = parentPlaceHolder.Output;
        HtmlChunk next;
        while ((next = htmlParser.ParseNext()) != null)
        {
          bool flag = false;
          string name = (string) null;
          if (next.Type == HtmlChunkType.OpenTag)
          {
            if (next.TagName.StartsWith("%"))
            {
              if (this.GetDirectiveName(next) == "Register")
              {
                this.ProcessRegisterDirective(next, placeholders, true);
                continue;
              }
              continue;
            }
            string attributeValue1 = this.GetAttributeValue(next, "class");
            if (attributeValue1 != null && attributeValue1.Contains("sf_colsIn"))
            {
              string str = layoutId + "_";
              string attributeValue2 = this.GetAttributeValue(next, "id");
              name = str + (string.IsNullOrEmpty(attributeValue2) ? "Col" + num++.ToString("00") : attributeValue2);
              next.SetAttribute("id", name);
              next.SetAttribute("runat", "server");
              flag = true;
            }
          }
          output.Append(flag ? next.GenerateHtml() : next.Html);
          if (name != null)
            placeholders.Add(new PlaceHolderCursor(name, output.Length, parentPlaceHolder.Name));
        }
      }
    }

    /// <summary>Appends the attributes.</summary>
    /// <param name="output">The output.</param>
    /// <param name="propertyDescriptors">The property descriptors.</param>
    /// <param name="properties">The properties.</param>
    /// <param name="ctrlType">Type of the control.</param>
    /// <param name="parentName">Name of the parent.</param>
    protected virtual void AppendAttributes(
      StringBuilder output,
      PropertyDescriptorCollection propertyDescriptors,
      IList<ControlProperty> properties,
      Type ctrlType,
      string parentName = null)
    {
      foreach (ControlProperty property in (IEnumerable<ControlProperty>) properties)
      {
        if (!property.HasListItems())
        {
          PropertyDescriptor propDesc = propertyDescriptors.Find(property.Name, true);
          if (propDesc != null)
          {
            PersistenceModeAttribute attribute = (PersistenceModeAttribute) propDesc.Attributes[typeof (PersistenceModeAttribute)];
            if (attribute == null || attribute.Mode == PersistenceMode.Attribute)
              this.AppendAttribute(output, property, propDesc, parentName);
          }
        }
      }
    }

    /// <summary>Appends the MVC mode attribute.</summary>
    /// <param name="output">The output.</param>
    /// <param name="framework">The framework.</param>
    protected virtual void AppendMvcModeAttribute(
      StringBuilder output,
      PageTemplateFramework framework)
    {
      output.Append("IsInPureMode").Append("=\"").Append((framework == PageTemplateFramework.Mvc).ToString()).Append("\" ");
    }

    /// <summary>Appends the attributes.</summary>
    /// <param name="output">The output.</param>
    /// <param name="properties">The properties.</param>
    /// <param name="ctrlType">Type of the control.</param>
    /// <param name="parentName">Name of the parent.</param>
    protected virtual void AppendAttributes(
      StringBuilder output,
      IList<ControlProperty> properties,
      Type ctrlType,
      string parentName = null)
    {
      this.AppendAttributes(output, TypeDescriptor.GetProperties(ctrlType), properties, ctrlType, parentName);
    }

    /// <summary>
    /// Handling for inner default properties like HyperLink.Text - this should be rendered as a href=''&gt;Text
    /// </summary>
    /// <param name="output">The output.</param>
    /// <param name="prop">The property.</param>
    /// <param name="encode">if set to <c>true</c> [encode].</param>
    protected virtual void AppendInnerDefaultProperty(
      StringBuilder output,
      ControlProperty prop,
      bool encode)
    {
      if (prop.HasChildProps())
      {
        IPropertySerializer propertySerializer = ObjectFactory.Resolve<IPropertySerializer>();
        output.Append(propertySerializer.SerializeProperties(prop));
      }
      else
      {
        string val = prop.Value;
        if (encode)
          val = this.HtmlEncode(val);
        output.Append(val);
      }
    }

    /// <summary>Appends the inner properties.</summary>
    /// <param name="output">The output.</param>
    /// <param name="ctrlData">The control data.</param>
    /// <param name="ctrlType">Type of the control.</param>
    /// <param name="placeHolders">The place holders.</param>
    /// <param name="framework">The framework.</param>
    protected virtual void AppendInnerProperties(
      StringBuilder output,
      ControlProperty ctrlData,
      Type ctrlType,
      CursorCollection placeHolders,
      PageTemplateFramework framework)
    {
      output.Append(" <").Append(ctrlData.Name).Append(" ");
      this.AppendAttributes(output, ctrlData.ChildProperties, ctrlType);
      output.Append("  runat=\"server\" >\r\n");
      StringBuilder output1 = new StringBuilder();
      PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(ctrlType);
      List<ControlProperty> controlPropertyList = new List<ControlProperty>();
      foreach (ControlProperty childProperty in (IEnumerable<ControlProperty>) ctrlData.ChildProperties)
      {
        PropertyDescriptor propertyDescriptor = properties.Find(childProperty.Name, true);
        if (propertyDescriptor != null)
        {
          PersistenceModeAttribute attribute = (PersistenceModeAttribute) propertyDescriptor.Attributes[typeof (PersistenceModeAttribute)];
          if ((attribute == null ? 0 : (attribute.Mode == PersistenceMode.InnerProperty ? 1 : (attribute.Mode == PersistenceMode.InnerDefaultProperty ? 1 : 0))) != 0 || childProperty.HasListItems())
            controlPropertyList.Add(childProperty);
        }
      }
      foreach (ControlProperty controlProperty in controlPropertyList)
      {
        output1.Append(" <").Append(controlProperty.Name).Append(" >");
        foreach (ObjectData listItem in (IEnumerable<ObjectData>) controlProperty.ListItems)
        {
          ctrlType = TypeResolutionService.ResolveType(listItem.ObjectType, true);
          string tagPrefix = placeHolders.GetTagPrefix(ctrlType.Namespace, ctrlType.Assembly.GetName().Name, "sf");
          output1.Append("<").Append(tagPrefix).Append(":").Append(ctrlType.Name).Append(" ");
          this.AppendProperties(output1, listItem, ctrlType, placeHolders, framework);
          output1.Append(" </").Append(tagPrefix).Append(":").Append(ctrlType.Name).Append(" >");
        }
        output1.Append(" </").Append(controlProperty.Name).Append(" >");
      }
      if (output1.Length > 0)
        output.AppendLine(output1.ToString());
      output.Append(" </").Append(ctrlData.Name).Append(" >");
    }

    /// <summary>Appends the attribute.</summary>
    /// <param name="output">The output.</param>
    /// <param name="prop">The property.</param>
    /// <param name="propDesc">The property DESC.</param>
    /// <param name="parentName">Name of the parent.</param>
    protected virtual void AppendAttribute(
      StringBuilder output,
      ControlProperty prop,
      PropertyDescriptor propDesc,
      string parentName)
    {
      string val = prop.Value;
      if (prop.HasChildProps())
      {
        this.AppendAttributes(output, prop.ChildProperties, propDesc.PropertyType, prop.Name);
      }
      else
      {
        DefaultValueAttribute attribute = (DefaultValueAttribute) propDesc.Attributes[typeof (DefaultValueAttribute)];
        if (attribute != null)
        {
          TypeConverter converter = propDesc.Converter;
          if (converter.CanConvertTo(typeof (string)))
          {
            string invariantString = converter.ConvertToInvariantString(attribute.Value);
            if (val == invariantString)
              return;
          }
        }
        string str1 = this.HtmlEncode(val);
        string str2 = parentName == null ? prop.Name : string.Format("{0}-{1}", (object) parentName, (object) prop.Name);
        output.Append(str2).Append("=\"").Append(str1).Append("\" ");
      }
    }

    /// <summary>
    /// Determines whether a file with the specified virtual path exists.
    /// </summary>
    /// <param name="definition">The definition.</param>
    /// <param name="virtualPath">The virtual path to check.</param>
    /// <returns>A value indicating whether a file with the specified virtual path exists.</returns>
    public abstract bool Exists(PathDefinition definition, string virtualPath);

    /// <summary>Opens the the file with the specified virtual path.</summary>
    /// <param name="definition">The definition.</param>
    /// <param name="virtualPaht">The virtual path.</param>
    /// <returns>A stream with the content.</returns>
    public abstract Stream Open(PathDefinition definition, string virtualPaht);

    /// <summary>
    /// Creates a cache dependency based on the specified virtual paths.
    /// </summary>
    /// <param name="definition">The definition.</param>
    /// <param name="virtualPath">The path to the primary virtual resource.</param>
    /// <param name="virtualPathDependencies">The virtual path dependencies.</param>
    /// <param name="utcStart">The UTC start.</param>
    /// <returns>
    /// A <see cref="T:System.Web.Caching.CacheDependency" /> object for the specified virtual resources.
    /// </returns>
    public virtual CacheDependency GetCacheDependency(
      PathDefinition definition,
      string virtualPath,
      IEnumerable virtualPathDependencies,
      DateTime utcStart)
    {
      return (CacheDependency) null;
    }

    /// <summary>Returns a hash of the specified virtual paths.</summary>
    /// <param name="definition">The file resolver definition.</param>
    /// <param name="virtualPath">The path to the primary virtual resource.</param>
    /// <param name="virtualPathDependencies">An array of paths to other virtual resources required by the primary virtual resource.</param>
    /// <returns>A hash of the specified virtual paths.</returns>
    /// <exception cref="T:System.InvalidOperationException">Invalid SiteMap node specified. Either the current group node doesn't have child nodes or the current user does not have rights to view any of the child nodes.</exception>
    public virtual string GetFileHash(
      PathDefinition definition,
      string virtualPath,
      IEnumerable virtualPathDependencies)
    {
      PageSiteNode requestedPageNode = this.GetRequestedPageNode();
      if (requestedPageNode == null)
        throw new InvalidOperationException("Invalid SiteMap node specified. Either the current group node doesn't have child nodes or the current user does not have rights to view any of the child nodes.");
      if (requestedPageNode.IsPersonalized())
      {
        Guid? variationId = this.GetVariationId(virtualPath);
        return requestedPageNode.GetPersonalizedVersionKey(variationId);
      }
      StringBuilder stringBuilder = new StringBuilder(requestedPageNode.VersionKey);
      foreach (IPageTemplateResolver templateResolver in ObjectFactory.Container.ResolveAll(typeof (IPageTemplateResolver)))
        stringBuilder.Append(";").Append(templateResolver.GetFileHash(definition, virtualPath, virtualPathDependencies));
      return stringBuilder.ToString();
    }

    /// <summary>Gets the requested page node.</summary>
    /// <param name="requestContext">The request context.</param>
    /// <returns>The page node.</returns>
    /// <exception cref="T:System.ArgumentException">This resolver hasn’t been invoked with the proper route handler.</exception>
    protected virtual PageSiteNode GetRequestedPageNode(
      out RequestContext requestContext)
    {
      HttpContextBase currentHttpContext = SystemManager.CurrentHttpContext;
      requestContext = currentHttpContext.Request.RequestContext;
      return RouteHelper.GetFirstPageDataNode((PageSiteNode) requestContext.RouteData.DataTokens["SiteMapNode"] ?? throw new ArgumentException("This resolver hasn’t been invoked with the proper route handler."), true);
    }

    private PageSiteNode GetRequestedPageNode() => this.GetRequestedPageNode(out RequestContext _);

    /// <summary>Builds the page template.</summary>
    /// <param name="pageData">The page data.</param>
    /// <param name="theme">The theme.</param>
    /// <param name="context">The context.</param>
    /// <param name="output">The output.</param>
    /// <param name="placeHolders">The place holders.</param>
    /// <param name="directives">The directives.</param>
    /// <param name="controlContainers">The control containers.</param>
    protected virtual void BuildPageTemplate(
      PageData pageData,
      string theme,
      RequestContext context,
      StringBuilder output,
      CursorCollection placeHolders,
      DirectiveCollection directives,
      List<IControlsContainer> controlContainers)
    {
      controlContainers.Add((IControlsContainer) new PageDataControlsContainerWrapper((IControlsContainer) pageData, (PageDataProvider) null, true));
      if (!string.IsNullOrEmpty(pageData.MasterPage))
        this.BuildWithMasterPage(pageData.MasterPage, context, output, placeHolders, directives);
      else if (!string.IsNullOrEmpty(pageData.ExternalPage))
      {
        this.BuildWithExternalPage(pageData.ExternalPage, context, output, placeHolders, directives);
      }
      else
      {
        if (this.BuildTemplateFromPresentationData((IEnumerable<PresentationData>) pageData.Presentation, theme, output, placeHolders, directives))
          return;
        PageTemplate pageTemplate = this.GetPageTemplate(pageData);
        if (pageTemplate != null)
          this.BuildPageTemplateRecursive((IPageTemplate) pageTemplate, theme, context, output, placeHolders, directives, controlContainers);
        else
          this.ProcessStringTemplate(ControlUtilities.GetSitefinityTextResource("Telerik.Sitefinity.Resources.Pages.Frontend.aspx"), output, placeHolders, directives);
      }
    }

    /// <summary>Builds with master page.</summary>
    /// <param name="virtualPath">The virtual path.</param>
    /// <param name="context">The context.</param>
    /// <param name="output">The output.</param>
    /// <param name="placeHolders">The place holders.</param>
    /// <param name="directives">The directives.</param>
    protected virtual void BuildWithMasterPage(
      string virtualPath,
      RequestContext context,
      StringBuilder output,
      CursorCollection placeHolders,
      DirectiveCollection directives)
    {
      string masterPage = string.Empty;
      Stream stream = HostingEnvironment.VirtualPathProvider.GetFile(virtualPath).Open();
      if (stream != null && stream.CanRead)
      {
        if (stream.CanSeek)
          stream.Seek(0L, SeekOrigin.Begin);
        masterPage = new StreamReader(stream).ReadToEnd();
      }
      if (string.IsNullOrEmpty(masterPage))
        return;
      Directive directive = directives.Where<Directive>((Func<Directive, bool>) (item => item.Name == "Page")).FirstOrDefault<Directive>();
      if (directive == null)
      {
        directive = new Directive("Page", new NameValueCollection());
        directives.Add(directive);
      }
      directive.Attributes.Add("MasterPageFile", virtualPath);
      this.AddRemainingDirectives(output, directives, placeHolders);
      this.AddContentControlsForPlaceHolders(masterPage, output, placeHolders, directives);
    }

    /// <summary>Builds the with external page.</summary>
    /// <param name="virtualPath">The virtual path.</param>
    /// <param name="context">The context.</param>
    /// <param name="output">The output.</param>
    /// <param name="placeHolders">The place holders.</param>
    /// <param name="directives">The directives.</param>
    protected virtual void BuildWithExternalPage(
      string virtualPath,
      RequestContext context,
      StringBuilder output,
      CursorCollection placeHolders,
      DirectiveCollection directives)
    {
      this.ProcessStringTemplate(File.ReadAllText(context.HttpContext.Server.MapPath(virtualPath)), output, placeHolders, directives);
    }

    /// <summary>Builds the template from presentation data.</summary>
    /// <param name="presentation">The presentation.</param>
    /// <param name="theme">The theme.</param>
    /// <param name="output">The output.</param>
    /// <param name="placeHolders">The place holders.</param>
    /// <param name="directives">The directives.</param>
    /// <returns>If the template was build correctly.</returns>
    protected virtual bool BuildTemplateFromPresentationData(
      IEnumerable<PresentationData> presentation,
      string theme,
      StringBuilder output,
      CursorCollection placeHolders,
      DirectiveCollection directives)
    {
      string template = (string) null;
      foreach (PresentationData presentationData in presentation)
      {
        if (presentationData.DataType == "HTML_DOCUMENT")
        {
          if ((presentationData.Theme ?? "Default").Equals(theme, StringComparison.OrdinalIgnoreCase))
          {
            template = presentationData.Data;
            break;
          }
          if (template == null)
            template = presentationData.Data;
        }
      }
      if (string.IsNullOrEmpty(template))
        return false;
      this.ProcessStringTemplate(template, output, placeHolders, directives);
      return true;
    }

    /// <summary>Builds the page template recursive.</summary>
    /// <param name="pageTemplate">The page template.</param>
    /// <param name="theme">The theme.</param>
    /// <param name="context">The context.</param>
    /// <param name="output">The output.</param>
    /// <param name="placeHolders">The place holders.</param>
    /// <param name="directives">The directives.</param>
    /// <param name="controlContainers">The control containers.</param>
    protected virtual void BuildPageTemplateRecursive(
      IPageTemplate pageTemplate,
      string theme,
      RequestContext context,
      StringBuilder output,
      CursorCollection placeHolders,
      DirectiveCollection directives,
      List<IControlsContainer> controlContainers)
    {
      controlContainers.Add((IControlsContainer) new TemplateControlsContainerWrapper((IControlsContainer) pageTemplate, (PageDataProvider) null, true));
      if (string.IsNullOrEmpty(pageTemplate.MasterPage))
        pageTemplate.MasterPage = PageHelper.ResolveDynamicMasterPage(pageTemplate);
      if (!string.IsNullOrEmpty(pageTemplate.MasterPage))
        this.BuildWithMasterPage(pageTemplate.MasterPage, context, output, placeHolders, directives);
      else if (!string.IsNullOrEmpty(pageTemplate.ExternalPage))
      {
        this.BuildWithExternalPage(pageTemplate.ExternalPage, context, output, placeHolders, directives);
      }
      else
      {
        if (this.BuildTemplateFromPresentationData(pageTemplate.Presentation, theme, output, placeHolders, directives))
          return;
        if (pageTemplate.ParentTemplate != null)
          this.BuildPageTemplateRecursive(pageTemplate.ParentTemplate, theme, context, output, placeHolders, directives, controlContainers);
        else
          this.ProcessStringTemplate(ControlUtilities.GetSitefinityTextResource(this.DefaultFrontendPageTemplate), output, placeHolders, directives);
      }
    }

    /// <summary>Gets the default frontend page template.</summary>
    /// <value>The default frontend page template.</value>
    protected virtual string DefaultFrontendPageTemplate => "Telerik.Sitefinity.Resources.Pages.Frontend.aspx";

    /// <summary>Processes the string template.</summary>
    /// <param name="template">The template.</param>
    /// <param name="output">The output.</param>
    /// <param name="placeHolders">The place holders.</param>
    /// <param name="directives">The directives.</param>
    protected virtual void ProcessStringTemplate(
      string template,
      StringBuilder output,
      CursorCollection placeHolders,
      DirectiveCollection directives)
    {
      bool flag1 = false;
      bool flag2 = false;
      bool flag3 = false;
      string str = (string) null;
      using (HtmlParser htmlParser = new HtmlParser(template))
      {
        htmlParser.SetChunkHashMode(false);
        htmlParser.AutoExtractBetweenTagsOnly = false;
        htmlParser.CompressWhiteSpaceBeforeTag = false;
        htmlParser.KeepRawHTML = true;
        HtmlChunk next;
        while ((next = htmlParser.ParseNext()) != null)
        {
          bool modified = false;
          string name = (string) null;
          if (next.Type == HtmlChunkType.OpenTag)
          {
            if (flag1)
            {
              string tagName = next.TagName;
              if (tagName.Equals("asp:ContentPlaceHolder", StringComparison.OrdinalIgnoreCase))
              {
                next.TagName = str + "SitefinityPlaceHolder";
                modified = true;
                name = this.GetElementId(next);
              }
              else if (tagName.EndsWith(":SitefinityPlaceHolder", StringComparison.OrdinalIgnoreCase))
                name = this.GetElementId(next);
              else if (!flag2 && tagName.Equals("form", StringComparison.OrdinalIgnoreCase))
                name = "sf_RequiredControls";
              else if (!flag3 && tagName.Equals("head", StringComparison.OrdinalIgnoreCase))
                name = "sf_Head";
            }
            else if (next.TagName.StartsWith("%"))
            {
              this.ProcessDirective(next, directives, placeHolders, ref modified);
            }
            else
            {
              this.AddRemainingDirectives(output, directives, placeHolders);
              str = placeHolders.GetTagPrefix("Telerik.Sitefinity.Web.UI", "Telerik.Sitefinity", "sf") + ":";
              flag1 = true;
            }
          }
          else if (next.Type == HtmlChunkType.CloseTag && next.TagName.Equals("asp:ContentPlaceHolder", StringComparison.OrdinalIgnoreCase))
          {
            next.TagName = str + "SitefinityPlaceHolder";
            modified = true;
          }
          if (name != null)
          {
            int num = next.IsEndClosure ? 1 : 0;
            if ((num | (modified ? 1 : 0)) != 0)
            {
              next.IsEndClosure = false;
              output.Append(next.GenerateHtml());
            }
            else
              output.Append(next.Html);
            placeHolders.Add(new PlaceHolderCursor(name, output.Length));
            if (num != 0)
              output.Append("</").Append(next.TagName).AppendLine(">");
          }
          else
            output.Append(modified ? next.GenerateHtml() : next.Html);
        }
      }
    }

    /// <summary>Adds the remaining directives.</summary>
    /// <param name="output">The output.</param>
    /// <param name="directives">The directives.</param>
    /// <param name="placeHolders">The place holders.</param>
    protected virtual void AddRemainingDirectives(
      StringBuilder output,
      DirectiveCollection directives,
      CursorCollection placeHolders)
    {
      foreach (Directive directive in (List<Directive>) directives)
      {
        if (!directive.IsSet)
        {
          if (directive.Name == "Register")
            placeHolders.RegisterNamespace(directive.Attributes["Namespace"], directive.Attributes["Assembly"], directive.Attributes["TagPrefix"], false);
          output.Append("<%@ ").Append(directive.Name).Append(" ");
          foreach (string allKey in directive.Attributes.AllKeys)
          {
            directive.Attributes[allKey] = directive.Attributes[allKey];
            output.Append(allKey).Append("=\"").Append(directive.Attributes[allKey]).Append("\" ");
          }
          output.Append("%>");
        }
      }
      placeHolders.CreateNamspacePlaceHolder(output.Length);
    }

    /// <summary>Adds the content controls for placeholders.</summary>
    /// <param name="masterPage">The master page.</param>
    /// <param name="output">The output.</param>
    /// <param name="placeHolders">The place holders.</param>
    /// <param name="directives">The directives.</param>
    public virtual void AddContentControlsForPlaceHolders(
      string masterPage,
      StringBuilder output,
      CursorCollection placeHolders,
      DirectiveCollection directives)
    {
      using (HtmlParser htmlParser = new HtmlParser(masterPage))
      {
        htmlParser.SetChunkHashMode(false);
        htmlParser.AutoExtractBetweenTagsOnly = false;
        htmlParser.KeepRawHTML = true;
        HtmlChunk next;
        while ((next = htmlParser.ParseNext()) != null)
        {
          if (next.Type == HtmlChunkType.OpenTag && next.TagName.Equals("asp:ContentPlaceHolder", StringComparison.OrdinalIgnoreCase))
          {
            for (int index = 0; index < next.ParamsCount; ++index)
            {
              if (next.Attributes[index].Equals("ID", StringComparison.OrdinalIgnoreCase))
              {
                string placeHolderName = next.Values[index];
                if (!placeHolders.Any<PlaceHolderCursor>((Func<PlaceHolderCursor, bool>) (ps => ps.Name == placeHolderName)))
                {
                  output.Append("<asp:Content ContentPlaceHolderID=\"").Append(placeHolderName).Append("\" runat=\"Server\">\r\n");
                  placeHolders.Add(new PlaceHolderCursor(placeHolderName, output.Length));
                  output.Append("</asp:Content>");
                }
              }
            }
          }
        }
      }
    }

    /// <summary>Gets the element identifier.</summary>
    /// <param name="chunk">The chunk.</param>
    /// <returns>The element ID.</returns>
    protected virtual string GetElementId(HtmlChunk chunk) => this.GetAttributeValue(chunk, "ID");

    /// <summary>Processes the directive.</summary>
    /// <param name="chunk">The chunk.</param>
    /// <param name="directives">The directives.</param>
    /// <param name="placeHolders">The place holders.</param>
    /// <param name="modified">if set to <c>true</c> [modified].</param>
    protected virtual void ProcessDirective(
      HtmlChunk chunk,
      DirectiveCollection directives,
      CursorCollection placeHolders,
      ref bool modified)
    {
      string derName = this.GetDirectiveName(chunk);
      string str = derName;
      if (!(str == "Master"))
      {
        if (!(str == "Page"))
        {
          if (str == "Register")
            this.ProcessRegisterDirective(chunk, placeHolders, false);
          foreach (Directive directive in directives.Where<Directive>((Func<Directive, bool>) (d => d.Name == derName)))
          {
            bool flag = true;
            for (int index = 0; index < chunk.ParamsCount; ++index)
            {
              if (string.Equals(directive.Attributes[chunk.Attributes[index]], chunk.Values[index], StringComparison.OrdinalIgnoreCase))
              {
                flag = false;
                break;
              }
            }
            if (flag)
            {
              directive.IsSet = true;
              break;
            }
          }
        }
        else
        {
          Directive directive = directives.SingleOrDefault<Directive>((Func<Directive, bool>) (d => d.Name == "Page"));
          if (directive == null)
            return;
          this.MergeDirectiveAttributes(chunk, directive);
          directive.IsSet = true;
          modified = true;
        }
      }
      else
      {
        this.ReplaceDirective(chunk, "Master", "Page");
        Directive directive = directives.Single<Directive>((Func<Directive, bool>) (d => d.Name == "Page"));
        this.MergeDirectiveAttributes(chunk, directive);
        directive.IsSet = true;
        modified = true;
      }
    }

    /// <summary>Gets the attribute value.</summary>
    /// <param name="chunk">The chunk.</param>
    /// <param name="attributeName">Name of the attribute.</param>
    /// <returns>The value of the attribute.</returns>
    protected virtual string GetAttributeValue(HtmlChunk chunk, string attributeName)
    {
      int index = Array.FindIndex<string>(chunk.Attributes, 0, chunk.ParamsCount, (Predicate<string>) (i => i.Equals(attributeName, StringComparison.OrdinalIgnoreCase)));
      return index != -1 ? chunk.Values[index] : (string) null;
    }

    /// <summary>Gets the name of the directive.</summary>
    /// <param name="chunk">The chunk.</param>
    /// <returns>The directive name.</returns>
    /// <exception cref="T:System.InvalidOperationException">This tag does not represent directive.</exception>
    protected virtual string GetDirectiveName(HtmlChunk chunk)
    {
      string tagName = chunk.TagName;
      if (tagName.Length > 2 && tagName.StartsWith("%@"))
        return tagName.Substring(2);
      if (tagName == "%@" && chunk.ParamsCount > 0 && string.IsNullOrEmpty(chunk.Values[0]))
        return chunk.Attributes[0];
      if (tagName == "%" && chunk.ParamsCount > 0)
      {
        string attribute = chunk.Attributes[0];
        if (attribute.Length > 1 && attribute.StartsWith("@") && string.IsNullOrEmpty(chunk.Values[0]))
          return attribute.Substring(1);
        if (attribute == "@" && chunk.ParamsCount > 1 && string.IsNullOrEmpty(chunk.Values[1]))
          return chunk.Attributes[1];
      }
      throw new InvalidOperationException("This tag does not represent directive.");
    }

    /// <summary>Replaces the directive.</summary>
    /// <param name="chunk">The chunk.</param>
    /// <param name="directiveName">Name of the directive.</param>
    /// <param name="replacement">The replacement.</param>
    /// <exception cref="T:System.ArgumentException">The provided chunk does not represent the specified directive. - chunk</exception>
    protected virtual void ReplaceDirective(
      HtmlChunk chunk,
      string directiveName,
      string replacement)
    {
      if (chunk.TagName.StartsWith("%@" + directiveName, StringComparison.OrdinalIgnoreCase))
        chunk.TagName = "%@" + replacement;
      else if (chunk.TagName == "%@" && chunk.ParamsCount > 0 && chunk.Attributes[0].Equals(directiveName, StringComparison.OrdinalIgnoreCase))
        chunk.Attributes[0] = replacement;
      else if (chunk.TagName == "%" && chunk.ParamsCount > 0 && chunk.Attributes[0].Equals("@" + directiveName, StringComparison.OrdinalIgnoreCase))
      {
        chunk.Attributes[0] = "@" + replacement;
      }
      else
      {
        if (!(chunk.TagName == "%") || chunk.ParamsCount <= 1 || !(chunk.Attributes[0] == "@") || !chunk.Attributes[1].Equals(directiveName, StringComparison.OrdinalIgnoreCase))
          throw new ArgumentException("The provided chunk does not represent the specified directive.", nameof (chunk));
        chunk.Attributes[1] = replacement;
      }
    }

    /// <summary>Merges the directive attributes.</summary>
    /// <param name="chunk">The chunk.</param>
    /// <param name="directive">The directive.</param>
    protected virtual void MergeDirectiveAttributes(HtmlChunk chunk, Directive directive)
    {
      bool flag = false;
      if (chunk.HasAttribute("%"))
      {
        chunk.RemoveAttribute("%");
        flag = true;
      }
      foreach (string allKey in directive.Attributes.AllKeys)
      {
        if (chunk.HasAttribute(allKey))
          chunk.SetAttribute(allKey, directive.Attributes[allKey]);
        else
          chunk.AddAttribute(allKey, directive.Attributes[allKey]);
      }
      if (!flag)
        return;
      chunk.AddParameter("%", string.Empty, (byte) 32);
    }

    /// <summary>Processes the register directive.</summary>
    /// <param name="chunk">The chunk.</param>
    /// <param name="placeHolders">The place holders.</param>
    /// <param name="include">if set to <c>true</c> [include].</param>
    protected virtual void ProcessRegisterDirective(
      HtmlChunk chunk,
      CursorCollection placeHolders,
      bool include)
    {
      string namespc = (string) null;
      string assembly = (string) null;
      string tagPrefix = (string) null;
      string tagName = (string) null;
      string src = (string) null;
      for (int index = 0; index < chunk.ParamsCount; ++index)
      {
        string attribute = chunk.Attributes[index];
        string str = chunk.Values[index];
        if ("Namespace".Equals(attribute, StringComparison.OrdinalIgnoreCase))
          namespc = str;
        else if ("Assembly".Equals(attribute, StringComparison.OrdinalIgnoreCase))
          assembly = str;
        else if ("TagPrefix".Equals(attribute, StringComparison.OrdinalIgnoreCase))
          tagPrefix = str;
        else if ("TagName".Equals(attribute, StringComparison.OrdinalIgnoreCase))
          tagName = str;
        else if ("Src".Equals(attribute, StringComparison.OrdinalIgnoreCase))
          src = str;
      }
      if (!string.IsNullOrEmpty(namespc) && !string.IsNullOrEmpty(assembly) && !string.IsNullOrEmpty(tagPrefix))
      {
        placeHolders.RegisterNamespace(namespc, assembly, tagPrefix, include);
      }
      else
      {
        if (string.IsNullOrEmpty(src) || string.IsNullOrEmpty(tagName) || string.IsNullOrEmpty(tagPrefix))
          return;
        placeHolders.RegisterUserControl(src, tagName, tagPrefix, include);
      }
    }

    /// <summary>Gets the theme from virtual path.</summary>
    /// <param name="virtualPath">The virtual path.</param>
    /// <returns>The theme.</returns>
    protected string GetThemeFromVirtualPath(string virtualPath)
    {
      int startIndex1 = virtualPath.LastIndexOf('/');
      int startIndex2 = virtualPath.LastIndexOf('.');
      int num = virtualPath.IndexOf('_', startIndex1);
      if (this.GetRequestedPageNode(out RequestContext _).IsMultilingual)
        startIndex2 = virtualPath.LastIndexOf('_', startIndex2);
      return virtualPath.Sub(num + 1, startIndex2 - 1);
    }

    /// <summary>Gets the variation identifier.</summary>
    /// <param name="resourcePath">The resource path.</param>
    /// <returns>The variation ID.</returns>
    internal Guid? GetVariationId(string resourcePath)
    {
      string[] source = resourcePath.Split('_');
      if (source.Length > 2)
      {
        foreach (IPageVariationPlugin pageVariationPlugin1 in ObjectFactory.Container.ResolveAll<IPageVariationPlugin>())
        {
          IPageVariationPlugin pageVariationPlugin = pageVariationPlugin1;
          foreach (string str in ((IEnumerable<string>) source).Where<string>((Func<string, bool>) (part => part.StartsWith(pageVariationPlugin.Key))))
          {
            Guid result;
            if (Guid.TryParse(str.Replace(".aspx", string.Empty).Replace(pageVariationPlugin.Key, string.Empty), out result))
              return new Guid?(result);
          }
        }
      }
      return new Guid?();
    }

    /// <summary>
    /// Wrapping the HttpUtility method so it can be replaced safely if needed.
    /// </summary>
    /// <param name="val">The string to encode.</param>
    /// <returns>An encoded string.</returns>
    private string HtmlEncode(string val) => HttpUtility.HtmlEncode(val);

    private PageTemplate GetPageTemplate(PageData pageData) => PageTemplateHelper.ResolvePageTemplate(pageData, new Func<PageSiteNode>(this.GetRequestedPageNode));

    /// <summary>Gets the embedded resources.</summary>
    public IList<RequiresEmbeddedWebResourceAttribute> EmbeddedResources
    {
      get
      {
        if (this.embeddedResources == null)
          this.embeddedResources = (IList<RequiresEmbeddedWebResourceAttribute>) new List<RequiresEmbeddedWebResourceAttribute>();
        return this.embeddedResources;
      }
    }
  }
}
