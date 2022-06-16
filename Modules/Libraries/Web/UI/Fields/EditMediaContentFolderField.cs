// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.Fields.EditMediaContentFolderField
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI.Fields
{
  /// <summary>
  /// A field control for changing Media content items parent libraries.
  /// </summary>
  [RequiresDataItem]
  public class EditMediaContentFolderField : FolderField
  {
    internal new const string scriptReference = "Telerik.Sitefinity.Modules.Libraries.Web.UI.Fields.Scripts.EditMediaContentFolderField.js";
    private const string iparentSelectorField = "Telerik.Sitefinity.Web.Scripts.IParentSelectorField.js";
    private const string reqDataItemScriptFileName = "Telerik.Sitefinity.Web.UI.Fields.Scripts.IRequiresDataItem.js";

    /// <summary>
    /// Gets the type (full name) of the script descriptor to be used. By default
    /// it returns current type.
    /// </summary>
    protected override string ScriptDescriptorType => typeof (EditMediaContentFolderField).FullName;

    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      List<ScriptReference> scriptReferenceList = new List<ScriptReference>(base.GetScriptReferences());
      string fullName = typeof (EditMediaContentFolderField).Assembly.FullName;
      ScriptReference scriptReference = new ScriptReference()
      {
        Assembly = fullName,
        Name = "Telerik.Sitefinity.Modules.Libraries.Web.UI.Fields.Scripts.EditMediaContentFolderField.js"
      };
      scriptReferenceList.Add(new ScriptReference("Telerik.Sitefinity.Web.Scripts.IParentSelectorField.js", fullName));
      scriptReferenceList.Add(new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.IRequiresDataItem.js", fullName));
      scriptReferenceList.Add(scriptReference);
      return (IEnumerable<ScriptReference>) scriptReferenceList.ToArray();
    }
  }
}
