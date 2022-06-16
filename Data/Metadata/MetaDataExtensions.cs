// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Metadata.MetaDataExtensions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Telerik.Sitefinity.Metadata.Model;

namespace Telerik.Sitefinity.Data.Metadata
{
  public static class MetaDataExtensions
  {
    /// <summary>
    /// Can start with underscores then at least one letter and after that letters, digits and underscores
    /// </summary>
    private static readonly Regex FieldNameRegex = new Regex("^_*[a-zA-Z]+\\w*$", RegexOptions.Compiled);

    /// <summary>Gets the name of the valid unique meta field.</summary>
    /// <param name="metaType">Type of the meta.</param>
    /// <param name="fieldName">Name of the field.</param>
    /// <returns></returns>
    public static string GetValidUniqueMetaFieldName(this MetaType metaType, string fieldName)
    {
      string validFieldName = string.Empty;
      if (!MetaDataExtensions.TryCreateValidFieldName(fieldName, out validFieldName))
        throw new ArgumentException("Not a valid field name, should contain only letters, digits or underscores. Can begin only with _ or a letter.", nameof (fieldName));
      IList<MetaField> fields = metaType.Fields;
      string name = validFieldName;
      int num = 0;
      while (fields.Any<MetaField>((Func<MetaField, bool>) (f => f.FieldName == name)))
        name = validFieldName + "_" + (object) num++;
      return name;
    }

    /// <summary>Validates the name of the meta field.</summary>
    /// <param name="fieldName">Name of the field.</param>
    /// <returns></returns>
    public static bool ValidateMetaFieldName(string fieldName) => MetaDataExtensions.FieldNameRegex.IsMatch(fieldName);

    /// <summary>
    /// Tries to generate a valid field name based on the specified one
    /// </summary>
    /// <param name="fieldName">Name of the field.</param>
    /// <param name="validFieldName">Name of the valid field.</param>
    /// <returns></returns>
    public static bool TryCreateValidFieldName(string fieldName, out string validFieldName)
    {
      if (MetaDataExtensions.ValidateMetaFieldName(fieldName))
      {
        validFieldName = fieldName;
        return true;
      }
      if (fieldName.Contains(" "))
      {
        fieldName = fieldName.Replace(" ", "").Trim();
        return MetaDataExtensions.TryCreateValidFieldName(fieldName, out validFieldName);
      }
      validFieldName = string.Empty;
      return false;
    }
  }
}
