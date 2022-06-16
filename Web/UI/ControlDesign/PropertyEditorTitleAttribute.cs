// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ControlDesign.PropertyEditorTitleAttribute
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Localization;

namespace Telerik.Sitefinity.Web.UI.ControlDesign
{
  /// <summary>
  /// Specifies the title which is going to be used in the property editor for the control this attribute is applied on
  /// </summary>
  [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
  public class PropertyEditorTitleAttribute : Attribute
  {
    private string propertyEditorTitle;
    private string propertyEditorTitleKey;
    private Type resourceClassType;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.ControlDesign.PropertyEditorTitleAttribute" /> class.
    /// </summary>
    /// <param name="title">The title.</param>
    public PropertyEditorTitleAttribute(string title) => this.propertyEditorTitle = title;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.ControlDesign.PropertyEditorTitleAttribute" /> class.
    /// </summary>
    /// <param name="resourceClassType">Type of the resource class.</param>
    /// <param name="key">The key to get the title from the resource class.</param>
    public PropertyEditorTitleAttribute(Type resourceClassType, string key)
    {
      this.propertyEditorTitleKey = key;
      this.resourceClassType = resourceClassType;
    }

    /// <summary>
    /// Gets the title to be used in the property editor of this control
    /// </summary>
    public string PropertyEditorTitle
    {
      get
      {
        if (!this.propertyEditorTitleKey.IsNullOrEmpty())
          this.propertyEditorTitle = Res.Get(this.resourceClassType, this.propertyEditorTitleKey);
        return this.propertyEditorTitle;
      }
    }
  }
}
