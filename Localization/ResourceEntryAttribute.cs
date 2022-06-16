// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.ResourceEntryAttribute
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Localization
{
  /// <summary>Defines a property as localizable resource entry.</summary>
  [AttributeUsage(AttributeTargets.Property)]
  public sealed class ResourceEntryAttribute : Attribute
  {
    private readonly string key;
    private string value;
    private string description;
    private string lastModified;
    private bool alwaysReturnDefaultValue;

    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Localization.ResourceEntryAttribute" /> with the specified key.
    /// </summary>
    /// <param name="key">A key by which this entry is accessed.</param>
    public ResourceEntryAttribute(string key) => this.key = key;

    /// <summary>Gets the key by which this entry is accessed.</summary>
    public string Key => this.key;

    /// <summary>Gets or sets the value for this entry.</summary>
    public string Value
    {
      get => this.value;
      set => this.value = value;
    }

    /// <summary>Gets or sets description for this entry.</summary>
    public string Description
    {
      get => this.description;
      set => this.description = value;
    }

    /// <summary>
    /// Gets or sets the date when the value was last modified.
    /// </summary>
    public string LastModified
    {
      get => this.lastModified;
      set => this.lastModified = value;
    }

    /// <summary>
    /// Specifies that the resource entry will always return its default value
    /// <remarks>
    /// There are some resources which should not be translated because they can break some controls
    /// </remarks>
    /// </summary>
    public bool AlwaysReturnDefaultValue
    {
      get => this.alwaysReturnDefaultValue;
      set => this.alwaysReturnDefaultValue = value;
    }
  }
}
