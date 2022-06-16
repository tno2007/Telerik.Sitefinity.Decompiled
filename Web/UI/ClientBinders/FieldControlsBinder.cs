// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.FieldControls.FieldControlsBinder
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web.UI;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages;

namespace Telerik.Sitefinity.Web.UI.FieldControls
{
  /// <summary>Summary description for FieldControlsBinder</summary>
  [ToolboxBitmap(typeof (FieldControlsBinder), "Telerik.Sitefinity.Resources.Sitefinity.bmp")]
  [ToolboxData("<{0}:FieldControlsBinder runat=\"server\"></{0}:FieldControlsBinder>")]
  public class FieldControlsBinder : ClientBinder
  {
    private string itemType;
    private string contentItemId;
    private static readonly string binderName = typeof (FieldControlsBinder).FullName;

    /// <summary>
    /// Gets the name of the javascript class exposed by the concrete implementation of the
    /// ClientBinder name.
    /// </summary>
    /// <value></value>
    protected override string BinderName => FieldControlsBinder.binderName;

    /// <summary>Gets or sets the default sort expression.</summary>
    /// <value></value>
    public new string DefaultSortExpression
    {
      get => string.Empty;
      set
      {
      }
    }

    /// <summary>The system type of the content item</summary>
    public string ItemType
    {
      get => this.itemType;
      set => this.itemType = value;
    }

    /// <summary>
    /// if set (server side or client side) the binder will
    /// bind its controls only to this item
    /// </summary>
    public string ContentItemId
    {
      get => this.contentItemId;
      set => this.contentItemId = value;
    }

    /// <summary>
    /// If set to false, the generated JSON on the client will be the value of the Item property of an 'item context' object.
    /// If set to true, the generated JSON will be posted as is to the web service.
    /// False is required for compatilibity with ContentServiceBase. By default, it is false.
    /// </summary>
    public bool DoNotUseContentItemContext { get; set; }

    protected override ScriptRef GetRequiredCoreScripts() => ScriptRef.JQuery | ScriptRef.JQueryValidate;

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript)
    /// client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of
    /// <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptBehaviorDescriptor behaviorDescriptor = (ScriptBehaviorDescriptor) base.GetScriptDescriptors().Last<ScriptDescriptor>();
      behaviorDescriptor.AddProperty("_itemType", (object) this.ItemType);
      behaviorDescriptor.AddProperty("_doNotUseContentItemContext", (object) this.DoNotUseContentItemContext);
      behaviorDescriptor.AddProperty("_lockedFieldMessage", (object) Res.Get<ContentLifecycleMessages>().LockedFieldMessage);
      if (!string.IsNullOrEmpty(this.ContentItemId))
        behaviorDescriptor.AddProperty("contentItemId", (object) this.ContentItemId);
      return (IEnumerable<ScriptDescriptor>) new ScriptDescriptor[1]
      {
        (ScriptDescriptor) behaviorDescriptor
      };
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define
    /// script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of
    /// <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      List<ScriptReference> scriptReferences = new List<ScriptReference>(base.GetScriptReferences());
      string assembly = this.GetType().Assembly.GetName().ToString();
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Web.Scripts.FieldControlsBinder.js", assembly));
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Web.Scripts.ISelfExecutableField.js", assembly));
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Web.Scripts.IParentSelectorField.js", assembly));
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.ILocalizableFieldControl.js", assembly));
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.IRequiresDataItem.js", assembly));
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.IRequiresDataItemContext.js", assembly));
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Web.Scripts.FieldChangeNotifier.js", assembly));
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Web.Scripts.IDataItemAccessField.js", assembly));
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.IRelatedDataField.js", assembly));
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.IRelatingDataField.js", assembly));
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Web.SitefinityJS.Telerik.Sitefinity.js", assembly));
      return (IEnumerable<ScriptReference>) scriptReferences;
    }
  }
}
