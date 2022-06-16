// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.PublishingPoints.IPublishingPointInitializer
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Publishing.Web.Services.Data;

namespace Telerik.Sitefinity.Publishing.PublishingPoints
{
  /// <summary>A contract for creation of a publishing point.</summary>
  internal interface IPublishingPointInitializer
  {
    /// <summary>
    /// Configures an instance of type <see cref="T:Telerik.Sitefinity.Publishing.Web.Services.Data.PublishingPointDetailViewModel" /> that is used to transfer data when publishing point is being created.
    /// </summary>
    /// <returns>An instance of type <see cref="T:Telerik.Sitefinity.Publishing.Web.Services.Data.PublishingPointDetailViewModel" /> that is used to transfer data when publishing point is being created.</returns>
    PublishingPointDetailViewModel ConfigurePublishingPoint();

    /// <summary>Creates the publishing point.</summary>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="templateName">Name of the template.</param>
    void CreatePublishingPoint(string providerName, string templateName);
  }
}
