// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Forms.IConnectorFormDataSender
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;

namespace Telerik.Sitefinity.Modules.Forms
{
  /// <summary>
  /// Used for sending form data for a custom connector integration.
  /// </summary>
  public interface IConnectorFormDataSender : IDisposable
  {
    /// <summary>
    /// Gets the key for the class that implements <see cref="T:Telerik.Sitefinity.Modules.Forms.ConnectorDataMappingExtender" />.
    /// Use null if there is no data mapping extender for your connector.
    /// </summary>
    string DataMappingExtenderKey { get; }

    /// <summary>
    /// Gets the name for the class that implements the <see cref="T:Telerik.Sitefinity.Modules.Forms.FormsConnectorDesignerExtender" />.
    /// Use null if there is no designer extender for your connector.
    /// </summary>
    string DesignerExtenderName { get; }

    /// <summary>
    /// Returns whether the data with the provided <see cref="T:Telerik.Sitefinity.Modules.Forms.ConnectorFormDataContext" /> context should be sent.
    /// If true the SendFormDataAsync will be called, otherwise not.
    /// </summary>
    /// <param name="dataContext">The data context around the submitted form fields.</param>
    /// <returns>Returns whether the data should be sent. If true the SendFormDataAsync will be called, otherwise not.</returns>
    bool ShouldSendFormData(ConnectorFormDataContext dataContext);

    /// <summary>Sends the provided data.</summary>
    /// <param name="data">The key value data containing the name and value for each submitted field of the form.
    /// The field names have already been mapped using the DataMappingExtenderKey and the provided mappings for the submitted form.</param>
    /// <param name="dataContext">The data context around the submitted form fields.</param>
    void SendFormData(IDictionary<string, string> data, ConnectorFormDataContext dataContext);
  }
}
