// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.TagsContainerExtender
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Modules.Pages;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>Summary description for TagsContainerExtender</summary>
  [TargetControlType(typeof (Panel))]
  public class TagsContainerExtender : ExtenderControl
  {
    private Control targetComponent;
    private string categoriesListCSS = "sfCategoriesList";

    /// <summary>CSS class for the selected categories</summary>
    public string CategoriesListCSS
    {
      get => this.categoriesListCSS;
      set => this.categoriesListCSS = value;
    }

    protected override IEnumerable<ScriptDescriptor> GetScriptDescriptors(
      Control target)
    {
      ScriptBehaviorDescriptor behaviorDescriptor = new ScriptBehaviorDescriptor("Telerik.Sitefinity.Web.UI.TagsContainerExtender", target.ClientID);
      behaviorDescriptor.AddProperty("_categoriesListCSS", (object) this.categoriesListCSS);
      return (IEnumerable<ScriptDescriptor>) new ScriptBehaviorDescriptor[1]
      {
        behaviorDescriptor
      };
    }

    protected override IEnumerable<ScriptReference> GetScriptReferences()
    {
      ScriptReferenceCollection scriptReferences = PageManager.GetScriptReferences(ScriptRef.JQuery);
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Web.Scripts.TagsContainerExtender.js", this.GetType().Assembly.GetName().ToString()));
      return (IEnumerable<ScriptReference>) scriptReferences;
    }
  }
}
