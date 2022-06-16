// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.CommentsPropertyResolver
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Reflection;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Modules.Comments;
using Telerik.Sitefinity.Modules.Comments.Configuration;
using Telerik.Sitefinity.Modules.GenericContent.Contracts;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.SiteSettings.Basic;

namespace Telerik.Sitefinity
{
  internal class CommentsPropertyResolver : ContentPropertyResolver
  {
    private static readonly Dictionary<string, string> configPropertyMapping = new Dictionary<string, string>()
    {
      {
        "PostRights",
        "RequiresAuthentication"
      },
      {
        "AllowComments",
        "AllowComments"
      },
      {
        "ApproveComments",
        "RequiresApproval"
      }
    };
    private Content contentItem;
    private const string globalCommentsSettingsConfigPath = "/CommentsConfig/commentsSettings";

    public CommentsPropertyResolver()
      : this((Content) null, "/CommentsConfig/commentsSettings")
    {
    }

    public CommentsPropertyResolver(Content contentItem)
      : this(contentItem, "/CommentsConfig/commentsSettings")
    {
    }

    public CommentsPropertyResolver(Content contentItem, string path)
      : base(contentItem, path)
    {
      if (string.Equals(path, "/CommentsConfig/commentsSettings"))
        this.RegisterSettingsResolverMethod(new SettingsResolverDelegate(this.GetSettingsObject));
      this.contentItem = contentItem;
    }

    private object GetSettingsObject() => (object) SystemManager.CurrentContext.GetSetting<CommentsSettingsContract, IGlobalCommentsSettings>() ?? (object) null;

    /// <summary>
    /// Resolves the property that do not have values specified to default values.
    /// </summary>
    /// <typeparam name="T">The type of the return value of property to be resolved</typeparam>
    /// <param name="propertyName">Name of the property.</param>
    public override T ResolveProperty<T>(string propertyName)
    {
      T obj;
      return this.TryResolvePropertyInCommentsModule<T>(propertyName, out obj) ? obj : base.ResolveProperty<T>(propertyName);
    }

    private bool TryResolvePropertyInCommentsModule<T>(string propertyName, out T value)
    {
      value = default (T);
      if (!SystemManager.IsModuleEnabled("Comments") || this.contentItem == null || !CommentsPropertyResolver.configPropertyMapping.ContainsKey(propertyName))
        return false;
      CommentsSettingsElement threadConfigByType = CommentsUtilities.GetThreadConfigByType(this.contentItem.GetType().FullName);
      PropertyInfo property = threadConfigByType.GetType().GetProperty(CommentsPropertyResolver.configPropertyMapping[propertyName]);
      if (propertyName == "PostRights")
      {
        object obj = (object) (PostRights) ((bool) property.GetValue((object) threadConfigByType, (object[]) null) ? 2 : 1);
        value = (T) obj;
      }
      else
        value = (T) property.GetValue((object) threadConfigByType, (object[]) null);
      return true;
    }
  }
}
