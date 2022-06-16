// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Forms.FormEntryDTO
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Net;
using Telerik.Sitefinity.Forms.Model;
using Telerik.Sitefinity.Modules.Forms.Web;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities;

namespace Telerik.Sitefinity.Modules.Forms
{
  /// <summary>
  /// This class provides DTO for working with form entries.
  /// </summary>
  public class FormEntryDTO
  {
    private Guid userId;
    private Guid entryId;
    private string ipaddress;
    private string formLanguage;
    private IEnumerable<string> notificationEmails;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Forms.FormEntryDTO" /> class.
    /// </summary>
    /// <param name="formDescription">The form description.</param>
    public FormEntryDTO(FormDescription formDescription)
      : this(formDescription, (IDictionary<string, object>) new Dictionary<string, object>(), (IDictionary<string, List<FormHttpPostedFile>>) new Dictionary<string, List<FormHttpPostedFile>>(), (string) null, Guid.Empty, (string) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Forms.FormEntryDTO" /> class.
    /// </summary>
    /// <param name="formDescription">The form description.</param>
    /// <param name="postedData">The posted data.</param>
    /// <param name="files">The files.</param>
    /// <param name="userIpAddress">The user IP address.</param>
    /// <param name="userId">The user identifier.</param>
    /// <param name="formLanguage">The form language.</param>
    public FormEntryDTO(
      FormDescription formDescription,
      IDictionary<string, object> postedData,
      IDictionary<string, List<FormHttpPostedFile>> files,
      string userIpAddress,
      Guid userId,
      string formLanguage)
    {
      this.FormDescription = formDescription;
      this.IpAddress = userIpAddress;
      this.UserId = userId;
      this.FormLanguage = formLanguage;
      this.PostedData = new FormPostedData()
      {
        FormsData = postedData,
        Files = files
      };
    }

    /// <summary>Gets or sets the form description.</summary>
    /// <value>The form description.</value>
    public FormDescription FormDescription { get; set; }

    /// <summary>Gets or sets the form posted data.</summary>
    /// <value>The posted data.</value>
    public FormPostedData PostedData { get; set; }

    /// <summary>Gets or sets the form language.</summary>
    /// <value>The form language.</value>
    public string FormLanguage
    {
      get
      {
        if (string.IsNullOrEmpty(this.formLanguage))
          this.formLanguage = SystemManager.CurrentContext.AppSettings.Multilingual ? SystemManager.CurrentContext.Culture.Name : SystemManager.CurrentContext.AppSettings.DefaultFrontendLanguage.Name;
        return this.formLanguage;
      }
      set => this.formLanguage = value;
    }

    /// <summary>Gets or sets the user IP address.</summary>
    /// <value>The user IP address.</value>
    public string IpAddress
    {
      get
      {
        if (string.IsNullOrEmpty(this.ipaddress))
        {
          IPAddress ipAddress = SystemManager.CurrentHttpContext.Request.GetIpAddress();
          this.ipaddress = ipAddress != null ? ipAddress.ToString() : string.Empty;
        }
        return this.ipaddress;
      }
      set => this.ipaddress = value;
    }

    /// <summary>Gets or sets the user identifier.</summary>
    /// <value>The user identifier.</value>
    public Guid UserId
    {
      get
      {
        if (this.userId == Guid.Empty)
          this.userId = ClaimsManager.GetCurrentUserId();
        return this.userId;
      }
      set => this.userId = value;
    }

    /// <summary>
    /// Gets or sets the id of the <see cref="T:Telerik.Sitefinity.Forms.Model.FormEntry" />.
    /// </summary>
    /// <value>
    /// The <see cref="T:Telerik.Sitefinity.Forms.Model.FormEntry" /> id.
    /// </value>
    public Guid EntryId
    {
      get => this.entryId;
      set => this.entryId = value;
    }

    /// <summary>Gets or sets the notification emails.</summary>
    /// <value>The notification emails.</value>
    public IEnumerable<string> NotificationEmails
    {
      get => this.notificationEmails;
      set => this.notificationEmails = value;
    }
  }
}
