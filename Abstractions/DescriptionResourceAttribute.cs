// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Abstractions.DescriptionResourceAttribute
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ComponentModel;
using Telerik.Sitefinity.Localization;

namespace Telerik.Sitefinity.Abstractions
{
  /// <summary>
  /// Specifies a description for a property or event using the specified resource class ID and key.
  /// </summary>
  [AttributeUsage(AttributeTargets.All)]
  public sealed class DescriptionResourceAttribute : DescriptionAttribute
  {
    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Abstractions.DescriptionResourceAttribute" /> with the specified
    /// resource class ID and key.
    /// </summary>
    /// <param name="classId">
    /// Specifies the global resource class to use for retrieving the description value.
    /// </param>
    /// <param name="key">Specifies the key of the description value.</param>
    public DescriptionResourceAttribute(string classId, string key)
    {
      if (string.IsNullOrEmpty(classId))
        throw new ArgumentNullException(nameof (classId));
      if (string.IsNullOrEmpty(key))
        throw new ArgumentNullException(nameof (key));
      this.ClassId = classId;
      this.Key = key;
    }

    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Abstractions.DescriptionResourceAttribute" /> with the specified
    /// resource class type and key.
    /// </summary>
    /// <param name="classType">
    /// Specifies the global resource class to use for retrieving the description value.
    /// </param>
    /// <param name="key">Specifies the key of the description value.</param>
    public DescriptionResourceAttribute(Type classType, string key)
      : this(classType.Name, key)
    {
    }

    /// <summary>Gets the description stored in this attribute.</summary>
    /// <returns>The description stored in this attribute.</returns>
    public override string Description => Res.Get(this.ClassId, this.Key);

    /// <summary>
    /// Specifies the global resource class to use for retrieving the description value.
    /// </summary>
    public string ClassId { get; private set; }

    /// <summary>Specifies the key of the description value.</summary>
    public string Key { get; private set; }
  }
}
