// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.GenericContent.Web.UI.ContentTypesSelector
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Modules.GenericContent.Web.UI
{
  public class ContentTypesSelector : SimpleScriptView
  {
    private static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Content.Selectors.ContentTypesSelector.ascx");
    private const string scriptName = "Telerik.Sitefinity.Modules.GenericContent.Web.UI.Scripts.ContentTypesSelector.js";
    private string selectContentTypesTitle = Res.Get<ContentResources>().SelectContentTypes;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.GenericContent.Web.UI.ContentTypesSelector" /> class.
    /// </summary>
    public ContentTypesSelector() => this.LayoutTemplatePath = ContentTypesSelector.layoutTemplatePath;

    public RadTreeView ContentTypes => this.Container.GetControl<RadTreeView>("contentTypes", true);

    public Label ContentTypeSelectorViewName => this.Container.GetControl<Label>("contentTypeSelectorViewName", true);

    public string[] Types { get; set; }

    private LinkButton LinkDoneBtn => this.Container.GetControl<LinkButton>("lnkDoneSelecting", true);

    private LinkButton LinkCancelBtn => this.Container.GetControl<LinkButton>("lnkCancel", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      this.ContentTypes.DataTextField = "Name";
      this.ContentTypes.DataFieldParentID = "Parent";
      this.ContentTypes.DataFieldID = "Item";
      this.ContentTypes.DataValueField = "Item";
      this.ContentTypes.DataSource = (object) this.GetContentTypes();
      this.ContentTypes.CssClass = "sfTreeView";
      this.ContentTypes.DataBind();
      this.ContentTypeSelectorViewName.Text = this.selectContentTypesTitle;
    }

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(typeof (ContentTypesSelector).FullName, this.ClientID);
      controlDescriptor.AddComponentProperty("contentTypes", this.ContentTypes.ClientID);
      controlDescriptor.AddElementProperty("contentTypeSelectorViewName", this.ContentTypeSelectorViewName.ClientID);
      controlDescriptor.AddElementProperty("linkDoneBtn", this.LinkDoneBtn.ClientID);
      controlDescriptor.AddElementProperty("linkCancelBtn", this.LinkCancelBtn.ClientID);
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
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      string str = this.GetType().Assembly.GetName().ToString();
      return (IEnumerable<ScriptReference>) new ScriptReference[2]
      {
        new ScriptReference()
        {
          Assembly = str,
          Name = "Telerik.Sitefinity.Modules.GenericContent.Web.UI.Scripts.ContentTypesSelector.js"
        },
        new ScriptReference()
        {
          Assembly = str,
          Name = "Telerik.Sitefinity.Web.Scripts.ClientManager.js"
        }
      };
    }

    private IEnumerable<ContentTypesSelector.TypeDataItem> GetContentTypes()
    {
      List<ContentTypesSelector.TypeDataItem> source = new List<ContentTypesSelector.TypeDataItem>(this.Types.Length);
      ISet<string> stringSet = (ISet<string>) new HashSet<string>((IEnumerable<string>) this.Types);
      foreach (string type1 in this.Types)
      {
        SitefinityType type2 = SystemManager.TypeRegistry.GetType(type1);
        if (type2 != null && type2.Kind == SitefinityTypeKind.Type)
          source.Add(new ContentTypesSelector.TypeDataItem()
          {
            Item = type1,
            Name = type2.PluralTitle,
            Parent = type2.Parent == null || !stringSet.Contains(type2.Parent) ? (string) null : type2.Parent
          });
      }
      return (IEnumerable<ContentTypesSelector.TypeDataItem>) source.OrderBy<ContentTypesSelector.TypeDataItem, string>((Func<ContentTypesSelector.TypeDataItem, string>) (t => t.Name));
    }

    private struct TypeDataItem
    {
      public string Item { get; set; }

      public string Name { get; set; }

      public string Parent { get; set; }
    }
  }
}
