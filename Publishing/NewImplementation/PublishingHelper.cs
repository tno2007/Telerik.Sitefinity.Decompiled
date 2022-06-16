// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.PublishingHelper
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Text;
using System.Xml;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Publishing.Model;

namespace Telerik.Sitefinity.Publishing
{
  public static class PublishingHelper
  {
    /// <summary>Copies the mapping properties to another mapping.</summary>
    /// <param name="source">The source.</param>
    /// <param name="destination">The destination.</param>
    public static void Copy(this IDefinitionField source, IDefinitionField destination)
    {
      if (destination == null)
        throw new ArgumentNullException(nameof (destination));
      destination.AllowMultipleTaxons = source.AllowMultipleTaxons;
      destination.ClrType = source.ClrType;
      destination.DBType = source.DBType;
      destination.DefaultValue = source.DefaultValue;
      destination.IsModified = source.IsModified;
      destination.IsRequired = source.IsRequired;
      destination.MaxLength = source.MaxLength;
      destination.Name = source.Name;
      destination.Precision = source.Precision;
      destination.SQLDBType = source.SQLDBType;
      destination.TaxonomyId = source.TaxonomyId;
      destination.TaxonomyProviderName = source.TaxonomyProviderName;
      destination.Title = source.Title;
    }

    /// <summary>Sanitizes the string to be used in XML.</summary>
    /// <param name="value">The value.</param>
    /// <returns></returns>
    public static string SanitizeStringForXml(string value)
    {
      StringBuilder stringBuilder = new StringBuilder(value.Length);
      foreach (char ch in value)
      {
        if (XmlConvert.IsXmlChar(ch))
          stringBuilder.Append(ch);
      }
      return stringBuilder.ToString();
    }

    /// <summary>Handles the error.</summary>
    /// <param name="message">The message.</param>
    /// <param name="ex">The ex.</param>
    public static void HandleError(this IPipe pipe, string message, Exception ex)
    {
      if (Exceptions.HandleException((Exception) new ApplicationException(message, ex), ExceptionPolicyName.IgnoreExceptions))
        throw ex;
    }
  }
}
