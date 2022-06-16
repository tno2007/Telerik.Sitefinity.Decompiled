// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.ResourceProperty
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Localization
{
  /// <summary>
  /// Provides information about property that is defined as localizable resource entry.
  /// </summary>
  public class ResourceProperty
  {
    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Localization.ResourceProperty" /> with the provided <see cref="T:Telerik.Sitefinity.Localization.ResourceEntryAttribute" />.
    /// </summary>
    /// <param name="attribute">A <see cref="T:Telerik.Sitefinity.Localization.ResourceEntryAttribute" /> defining this property.</param>
    public ResourceProperty(ResourceEntryAttribute attribute)
    {
      this.Key = attribute != null ? attribute.Key : throw new ArgumentNullException(nameof (attribute));
      this.DefaultValue = attribute.Value;
      this.Description = attribute.Description;
      this.AlwaysReturnDefaultValue = attribute.AlwaysReturnDefaultValue;
      DateTime result;
      DateTime.TryParse(attribute.LastModified, out result);
      this.LastModified = result;
    }

    /// <summary>Gets the key by which this entry is accessed.</summary>
    public string Key { get; private set; }

    /// <summary>Gets the value for this entry.</summary>
    public string DefaultValue { get; private set; }

    /// <summary>Gets description for this entry.</summary>
    public string Description { get; private set; }

    /// <summary>
    /// Gets the date of last modification of the value of this property.
    /// </summary>
    public DateTime LastModified { get; private set; }

    /// <summary>
    /// Specifies that the resource entry will always return its default value
    /// <remarks>
    /// There are some resources which should not be translated like FilterExpression in radgrd.main which when set breaks the grid
    /// </remarks>
    /// </summary>
    public bool AlwaysReturnDefaultValue { get; private set; }
  }
}
