// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.ObjectInfoAttribute
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Localization
{
  /// <summary>
  /// Specifies localizable UI information about a class or a member.
  /// </summary>
  [AttributeUsage(AttributeTargets.All)]
  public sealed class ObjectInfoAttribute : Attribute
  {
    private string name;
    private string title;
    private string titlePlural;
    private Type objectType;
    private string description;
    private string classId;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Localization.ObjectInfoAttribute" /> class.
    /// </summary>
    public ObjectInfoAttribute()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Localization.ObjectInfoAttribute" /> class.
    /// </summary>
    /// <param name="classType">
    /// Specifies the global resource class to use for retrieving the description value.
    /// </param>
    public ObjectInfoAttribute(Type classType)
      : this(classType, (string) null)
    {
    }

    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Localization.ObjectInfoAttribute" />
    /// with the provided object or member reference name.
    /// </summary>
    /// <param name="name">
    /// The reference name of the described object or member.
    /// Usually this name is identical to the actual name of the described object or member.
    /// </param>
    public ObjectInfoAttribute(string name)
      : this((Type) null, name)
    {
      this.name = name;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Localization.ObjectInfoAttribute" /> class.
    /// </summary>
    /// <param name="name">
    /// The reference name of the described object or member.
    /// Usually this name is identical to the actual name of the described object or member.
    /// </param>
    /// <param name="classType">
    /// Specifies the global resource class to use for retrieving the description value.
    /// </param>
    public ObjectInfoAttribute(Type classType, string name)
    {
      if (classType != (Type) null)
        this.classId = classType.Name;
      this.name = name;
    }

    /// <summary>
    /// Gets the reference name of the described object or member.
    /// </summary>
    public string Name
    {
      get => this.name;
      set => this.name = value;
    }

    /// <summary>
    /// Gets or sets the title that will be displayed on the user interface.
    /// </summary>
    public string Title
    {
      get
      {
        if (!string.IsNullOrEmpty(this.classId))
        {
          if (string.IsNullOrEmpty(this.title) && !string.IsNullOrEmpty(this.name))
            this.title = this.name + nameof (Title);
          if (!string.IsNullOrEmpty(this.title))
            return Res.Get(this.classId, this.title);
        }
        return this.title ?? this.name;
      }
      set => this.title = value;
    }

    /// <summary>
    /// Gets or sets the title in plural that will be displayed on the user interface.
    /// </summary>
    public string TitlePlural
    {
      get
      {
        if (!string.IsNullOrEmpty(this.classId))
        {
          if (string.IsNullOrEmpty(this.titlePlural) && !string.IsNullOrEmpty(this.name))
            this.titlePlural = this.name + nameof (TitlePlural);
          if (!string.IsNullOrEmpty(this.titlePlural))
            return Res.Get(this.classId, this.titlePlural);
        }
        return this.titlePlural ?? this.name;
      }
      set => this.titlePlural = value;
    }

    /// <summary>
    /// Gets or sets the type of object decorated with this attribute
    /// </summary>
    public Type ObjectType
    {
      get => this.objectType;
      set => this.objectType = value;
    }

    /// <summary>Gets or sets description of the object or member.</summary>
    public string Description
    {
      get
      {
        if (!string.IsNullOrEmpty(this.classId))
        {
          if (string.IsNullOrEmpty(this.description) && !string.IsNullOrEmpty(this.name))
            this.description = this.name + nameof (Description);
          if (!string.IsNullOrEmpty(this.description))
            return Res.Get(this.classId, this.description);
        }
        return this.description;
      }
      set => this.description = value;
    }

    /// <summary>
    /// Gets or sets the global resource class ID to use for localized strings.
    /// </summary>
    public string ResourceClassId
    {
      get => this.classId;
      set => this.classId = value;
    }
  }
}
