// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Comments.CommentsBehaviorUtilities
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Text;
using Telerik.Sitefinity.Modules.Comments.Configuration;

namespace Telerik.Sitefinity.Modules.Comments
{
  public static class CommentsBehaviorUtilities
  {
    public const string ReviewBehaviorIdent = "review";

    public static string GetLocalizedKeySuffix(string threadType)
    {
      CommentsSettingsElement threadConfigByType = CommentsUtilities.GetThreadConfigByType(threadType);
      StringBuilder stringBuilder = new StringBuilder();
      if (threadConfigByType.EnableRatings)
        stringBuilder.Append("review");
      return stringBuilder.ToString();
    }
  }
}
