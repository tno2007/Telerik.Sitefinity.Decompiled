// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.DeletedUserRecord
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Security
{
  /// <summary>
  /// Used to temporary store the deleted users till the next request
  /// where the user will be redirected to the login page
  /// </summary>
  public class DeletedUserRecord
  {
    public Guid UserId;
    public string Provider;
    public DateTime LogDate;

    /// <summary>Gets the key.</summary>
    /// <returns></returns>
    public virtual string GetKey() => DeletedUserRecord.GetKey(this.UserId, this.Provider);

    /// <summary>Gets the key.</summary>
    /// <param name="userId">The user pageId.</param>
    /// <param name="provider">The provider.</param>
    /// <returns></returns>
    public static string GetKey(Guid userId, string provider) => string.Format("{0}|{1}", (object) userId, (object) provider);
  }
}
