// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.Contracts.PersistentPropertyMappingProxy
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Diagnostics.CodeAnalysis;

namespace Telerik.Sitefinity.Web.Services.Contracts
{
  internal class PersistentPropertyMappingProxy : 
    PropertyMappingProxy,
    IPersistentPropertyMapping,
    IPropertyMapping
  {
    [SuppressMessage("Microsoft.StyleCop.CSharp.NamingRules", "SA1305:FieldNamesMustNotUseHungarianNotation", Justification = "This is not Hungarian notation.")]
    private string persistentName;
    private bool allowFilter = true;
    private bool allowSort = true;
    private bool readOnly;
    private bool selectedByDefault = true;
    private bool isKey;
    private string description;
    private string validateCondition;

    public PersistentPropertyMappingProxy()
    {
    }

    public PersistentPropertyMappingProxy(IPersistentPropertyMapping source)
      : base((IPropertyMapping) source)
    {
      this.persistentName = source.PersistentName;
      this.allowSort = source.AllowSort;
      this.allowFilter = source.AllowFilter;
      this.readOnly = source.ReadOnly;
      this.selectedByDefault = source.SelectedByDefault;
      this.isKey = source.IsKey;
      this.description = source.Description;
      this.validateCondition = source.ValidateCondition;
    }

    public string PersistentName
    {
      get => string.IsNullOrEmpty(this.persistentName) ? this.Name : this.persistentName;
      set => this.persistentName = value;
    }

    public bool AllowFilter
    {
      get => this.allowFilter;
      set => this.allowFilter = value;
    }

    public bool AllowSort
    {
      get => this.allowSort;
      set => this.allowSort = value;
    }

    public bool ReadOnly
    {
      get => this.readOnly;
      set => this.readOnly = value;
    }

    public bool SelectedByDefault
    {
      get => this.selectedByDefault;
      set => this.selectedByDefault = value;
    }

    public bool IsKey
    {
      get => this.isKey;
      set => this.isKey = value;
    }

    public string Description
    {
      get => this.description;
      set => this.description = value;
    }

    public new string ValidateCondition
    {
      get => this.validateCondition;
      set => this.validateCondition = value;
    }
  }
}
