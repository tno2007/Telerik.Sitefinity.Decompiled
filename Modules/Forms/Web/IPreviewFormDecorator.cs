// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Forms.Web.IPreviewFormDecorator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Web.UI;
using Telerik.Sitefinity.Forms.Model;

namespace Telerik.Sitefinity.Modules.Forms.Web
{
  /// <summary>Interface for objects decorating form preview markup</summary>
  internal interface IPreviewFormDecorator
  {
    /// <summary>Decorates the form preview.</summary>
    /// <param name="page">The page.</param>
    /// <param name="draftForm">The draft form.</param>
    void DecorateFormPreview(Page page, FormDraft draftForm);
  }
}
