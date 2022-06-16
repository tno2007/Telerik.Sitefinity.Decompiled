// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Extensions.MessageBodyExtensions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Newsletters.Model;

namespace Telerik.Sitefinity.Modules.Newsletters.Extensions
{
  /// <summary>
  /// Provides methods that extend the functionality of the <see cref="T:Telerik.Sitefinity.Newsletters.Model.MessageBody" /> persistent class.
  /// </summary>
  public static class MessageBodyExtensions
  {
    /// <summary>Gets the text body for the campaign.</summary>
    /// <param name="campaign">The instance of the campaign for which the text body ought to be retrieved.</param>
    /// <returns>A string representing the text body of the campaign.</returns>
    public static string GetTextBody(this MessageBody messageBody) => messageBody.BodyText;
  }
}
