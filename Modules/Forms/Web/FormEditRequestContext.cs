// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Forms.Web.FormEditRequestContext
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Specialized;
using System.Web;
using Telerik.Sitefinity.Security;

namespace Telerik.Sitefinity.Modules.Forms.Web
{
  /// <summary>
  /// This class contains properties and methods for decrypting query string when a request is perform to a specific forms control.
  /// </summary>
  internal class FormEditRequestContext
  {
    /// <summary>Gets the specified query collection.</summary>
    /// <param name="queryCollection">The query collection.</param>
    /// <param name="fromId">From id.</param>
    /// <returns>Gets a FormEditRequestContext instance.</returns>
    public static FormEditRequestContext Get(
      NameValueCollection queryCollection,
      Guid fromId)
    {
      FormEditRequestContext editRequestContext = new FormEditRequestContext();
      string str1 = queryCollection.Get("data");
      if (string.IsNullOrWhiteSpace(str1))
        return editRequestContext;
      string str2 = str1.Replace(" ", "+");
      try
      {
        editRequestContext.QueryData = HttpUtility.UrlEncode(str2);
        NameValueCollection queryString = HttpUtility.ParseQueryString(SecurityManager.DecryptData(str2));
        Guid result1;
        if (Guid.TryParse(queryString.Get("formId"), out result1))
        {
          if (result1 == fromId)
          {
            Guid result2 = Guid.Empty;
            DateTime result3 = DateTime.MinValue;
            string s = queryString.Get("expiration");
            editRequestContext.IsValidUpdateRequest = Guid.TryParse(queryString.Get("responseEntryId"), out result2) && DateTime.TryParse(s, out result3);
            if (editRequestContext.IsValidUpdateRequest)
            {
              editRequestContext.FormEntryId = result2;
              editRequestContext.ProviderName = queryCollection.Get("providerName");
              editRequestContext.IsExpired = DateTime.UtcNow > result3;
            }
          }
        }
      }
      catch
      {
      }
      return editRequestContext;
    }

    /// <summary>
    /// Gets a value indicating whether is valid update request.
    /// </summary>
    /// <value>The is valid update request.</value>
    public bool IsValidUpdateRequest { get; private set; }

    /// <summary>Gets the form entry id.</summary>
    /// <value>The form entry id.</value>
    public Guid FormEntryId { get; private set; }

    /// <summary>Gets the name of the provider.</summary>
    /// <value>The name of the provider.</value>
    public string ProviderName { get; private set; }

    /// <summary>
    /// Gets a value indicating whether update request is expired.
    /// </summary>
    /// <value>The is expired.</value>
    public bool IsExpired { get; private set; }

    /// <summary>Gets a value indicating the query data.</summary>
    /// <value>The query data.</value>
    public string QueryData { get; private set; }
  }
}
