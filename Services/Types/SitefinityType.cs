// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.SitefinityType
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Services
{
  /// <summary>
  /// <para>
  /// Represents a content/data (and possibly other kind of) type in the system.
  /// </para>
  /// <para>
  /// The term "type" is used in a sense broader than .NET type or Sitefinity content type,
  /// which is why a string is used as a key.
  /// </para>
  /// <seealso cref="!:SitefintiyTypeRegistry" />
  /// <seealso cref="P:Telerik.Sitefinity.Services.SystemManager.TypeRegistry" />
  /// </summary>
  public class SitefinityType
  {
    private string shortPluralTitle;
    private string shortSingularTitle;

    public SitefinityType()
    {
    }

    internal SitefinityType(SitefinityType source)
    {
      this.Key = source.Key;
      this.Kind = source.Kind;
      this.Parent = source.Parent;
      this.ModuleName = source.ModuleName;
      this.SingularTitle = source.SingularTitle;
      this.PluralTitle = source.PluralTitle;
      this.ShortPluralTitle = source.ShortPluralTitle;
      this.ShortSingularTitle = source.ShortSingularTitle;
      this.ResourceClassId = source.ResourceClassId;
      this.Icon = source.Icon;
    }

    /// <summary>The full name of the type, in case of a .NET type.</summary>
    public string Key { get; set; }

    /// <summary>
    /// The kind of the registry entry - one of the <see cref="T:Telerik.Sitefinity.Services.SitefinityTypeKind" /> values.
    /// </summary>
    public SitefinityTypeKind Kind { get; set; }

    /// <summary>
    /// The parent type, if any. <c>null</c> otherwise.
    /// </summary>
    public string Parent { get; set; }

    /// <summary>The name of the module, defining the type.</summary>
    public string ModuleName { get; set; }

    /// <summary>User-friendly type name in singular.</summary>
    public string SingularTitle { get; set; }

    /// <summary>User-friendly type name in plural.</summary>
    public string PluralTitle { get; set; }

    internal string ShortPluralTitle
    {
      get => string.IsNullOrEmpty(this.shortPluralTitle) ? this.PluralTitle : this.shortPluralTitle;
      set => this.shortPluralTitle = value;
    }

    internal string ShortSingularTitle
    {
      get => string.IsNullOrEmpty(this.shortSingularTitle) ? this.SingularTitle : this.shortSingularTitle;
      set => this.shortSingularTitle = value;
    }

    /// <summary>
    /// The ID of the resource class for user-friendly names lookup.
    /// When non-<c>null</c> <see cref="P:Telerik.Sitefinity.Services.SitefinityType.SingularTitle" /> and <see cref="P:Telerik.Sitefinity.Services.SitefinityType.PluralTitle" />
    /// are treated as resource keys of that class.
    /// </summary>
    public string ResourceClassId { get; set; }

    /// <summary>Gets or sets the default icon for the type.</summary>
    public string Icon { get; set; }
  }
}
