// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Comments.Configuration.CommentableTypeElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Modules.Comments.Configuration
{
  internal class CommentableTypeElement : CommentsSettingsElement
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Comments.Configuration.CommentableTypeElement" /> class.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    /// <remarks>
    /// ConfigElementCollection generally needs to have a parent, however, sometimes it is necessary
    /// to create a collection in memory only which is then used later on in the context of a parent.
    /// Therefore, is the element is of ConfigElementCollection, exception for a non existing parent
    /// will not be thrown.
    /// </remarks>
    public CommentableTypeElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    /// Gets or sets the friendly name of the type that is commentable. Appropriate for use in UI.
    /// </summary>
    [ConfigurationProperty("FriendlyName")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "CommentableTypeFriendlyNameDescription", Title = "CommentableTypeFriendlyNameCaption")]
    public string FriendlyName
    {
      get => (string) this[nameof (FriendlyName)];
      set => this[nameof (FriendlyName)] = (object) value;
    }

    /// <summary>
    /// Gets or sets the type name of the type that is commentable.
    /// </summary>
    [ConfigurationProperty("ItemType", Options = ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "CommentableTypeItemTypeDescription", Title = "CommentableTypeItemTypeCaption")]
    public string ItemType
    {
      get => (string) this[nameof (ItemType)];
      set => this[nameof (ItemType)] = (object) value;
    }

    private new static class ConfigProps
    {
      public const string FriendlyName = "FriendlyName";
      public const string ItemType = "ItemType";
    }
  }
}
