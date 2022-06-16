// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Forms.FormsConnectorDesignerExtender
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;

namespace Telerik.Sitefinity.Modules.Forms
{
  /// <summary>Extends forms designer.</summary>
  public abstract class FormsConnectorDesignerExtender
  {
    /// <summary>Gets the localized title of the extender</summary>
    public abstract string Title { get; }

    /// <summary>Gets the name of the extender</summary>
    public abstract string Name { get; }

    /// <summary>Gets the properties for the connector</summary>
    /// <returns>The properties</returns>
    public abstract IList<FormsConnectorDesignerExtender.PropertyDescription> GetProperties();

    /// <summary>A class describing a connector property</summary>
    public class PropertyDescription
    {
      /// <summary>Gets or sets the type of the property</summary>
      public FormsConnectorDesignerExtender.PropertyDescription.PropertyType Type { get; set; }

      /// <summary>Gets or sets the name of the property</summary>
      public string Name { get; set; }

      /// <summary>Gets or sets the title of the property</summary>
      public string Title { get; set; }

      /// <summary>Gets or sets the text for the property</summary>
      public string Text { get; set; }

      /// <summary>
      /// Gets or sets the conditional expression defining if the property is visible. If left empty it will be ignored.
      /// </summary>
      public string ConditionalVisibility { get; set; }

      /// <summary>Specifies the property types</summary>
      public enum PropertyType
      {
        /// <summary>Informational text</summary>
        InformationText,
        /// <summary>Boolean property</summary>
        Bool,
        /// <summary>String property</summary>
        String,
      }
    }
  }
}
