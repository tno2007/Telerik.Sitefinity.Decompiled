// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Web.UI.TypeEditorAttribute
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Modules.Pages.Web.UI
{
  /// <summary>
  /// This attribute is used to instruct Sitefinity that the property ought to be edited through a
  /// type editor.
  /// </summary>
  [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
  public sealed class TypeEditorAttribute : Attribute
  {
    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Pages.Web.UI.TypeEditorAttribute" />.
    /// </summary>
    /// <param name="editorType">Type of the editor to be used for editing the property.</param>
    public TypeEditorAttribute(string editorType) => this.EditorType = editorType;

    /// <summary>
    /// Gets the full type name of the control that ought to be used as the
    /// type editor for the property marked with the TypeEditorAttribute.
    /// </summary>
    public string EditorType { get; private set; }
  }
}
