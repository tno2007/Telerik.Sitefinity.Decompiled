// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.Web.UI.Designers.ContentSelectorsPipeDesignerView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ControlDesign;

namespace Telerik.Sitefinity.Publishing.Web.UI.Designers
{
  /// <summary>
  /// 
  /// </summary>
  public class ContentSelectorsPipeDesignerView : ContentSelectorsDesignerView
  {
    internal const string controlScript = "Telerik.Sitefinity.Publishing.Web.UI.Scripts.ContentSelectorsPipeDesignerView.js";
    public string templatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Designers.ContentView.ContentSelectorsPipeDesignerView.ascx");
    private readonly Dictionary<string, object> resources = new Dictionary<string, object>();

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    /// <value></value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(this.templatePath) ? ContentSelectorsDesignerView.layoutTemplatePath : this.templatePath;
      set => this.templatePath = value;
    }

    internal Dictionary<string, object> Resources => this.resources;

    internal void AddResource(IContentPipeDesignerView view)
    {
      Type contentType = view.ContentType;
      this.AddResource(view.GetDefaultDesignerInfo(), contentType);
    }

    internal void AddResource(IContentPipeDefaultDesignerView view, Type type) => this.resources.Add(type.FullName, (object) new ContentPipeDesignerResources()
    {
      TitleText = view.TitleText,
      ChooseAllText = view.ChooseAllText,
      SelectionOfItem = view.SelectionOfItem,
      SelectorDateRangesTitle = view.SelectorDateRangesTitle,
      ItemsName = view.ItemsName
    });

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      base.InitializeControls(container);
      this.InitializeResources();
    }

    private void InitializeResources()
    {
      Labels labels = Res.Get<Labels>();
      this.resources.Add("default", (object) new ContentPipeDesignerResources()
      {
        TitleText = labels.WhichContentItemsToDisplay,
        ChooseAllText = labels.AllPublishedItems,
        SelectionOfItem = labels.SelectionOfItems,
        SelectorDateRangesTitle = labels.DisplayItemsPublishedIn,
        AllItems = Res.Get<PublishingMessages>().AllItems
      });
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = base.GetScriptDescriptors().Last<ScriptDescriptor>() as ScriptControlDescriptor;
      controlDescriptor.AddProperty("resources", (object) this.resources);
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        controlDescriptor
      };
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
    {
      new ScriptReference("Telerik.Sitefinity.Publishing.Web.UI.Scripts.ContentSelectorsPipeDesignerView.js", typeof (ContentSelectorsPipeDesignerView).Assembly.FullName)
    };
  }
}
