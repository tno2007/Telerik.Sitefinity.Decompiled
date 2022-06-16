// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.SitefinityExceptions.TemplateNotFoundException
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.SitefinityExceptions
{
  /// <summary>Template not found exception</summary>
  public class TemplateNotFoundException : ItemNotFoundException
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.SitefinityExceptions.ItemNotFoundException" /> class.
    /// </summary>
    /// <param name="message">The message.</param>
    public TemplateNotFoundException(string message)
      : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.SitefinityExceptions.ItemNotFoundException" /> class.
    /// </summary>
    public TemplateNotFoundException()
    {
    }
  }
}
