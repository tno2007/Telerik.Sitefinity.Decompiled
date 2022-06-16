// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Composition.Merger
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using Telerik.Sitefinity.Newsletters.Model;

namespace Telerik.Sitefinity.Modules.Newsletters.Composition
{
  /// <summary>
  /// This class is responsible for merging the merge tags of the messages through the context
  /// </summary>
  public static class Merger
  {
    private static Regex regex = new Regex("\\{\\|([a-zA-Z0-9\\s_-]+?).([a-zA-Z0-9\\s_-]+?)\\|\\}", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);

    /// <summary>
    /// Merges the merge tags from the source with the actual values provided by the context.
    /// </summary>
    /// <param name="source">Text that contains merge tags to be merged.</param>
    /// <param name="context">Context that contains the objects from which the merge tag values ought to be obtained.</param>
    /// <returns>Merged text with actual values.</returns>
    public static string MergeTags(string source, params object[] context) => string.IsNullOrEmpty(source) || context == null || context.Length == 0 ? source : Merger.regex.Replace(source, (MatchEvaluator) (match => Merger.ReplaceMergeTag(match, context, true)));

    public static string MergeMatchedTagsOnly(string source, params object[] context) => string.IsNullOrEmpty(source) || context == null || context.Length == 0 ? source : Merger.regex.Replace(source, (MatchEvaluator) (match => Merger.ReplaceMergeTag(match, context, true)));

    private static string ReplaceMergeTag(Match m, object[] context, bool suppressException = false)
    {
      string mergeTagValue = Merger.GetMergeTagValue(m.Groups[1].Value.Trim(), m.Groups[2].Value.Trim(), context, suppressException);
      if (mergeTagValue == string.Empty)
        mergeTagValue = m.ToString();
      return mergeTagValue;
    }

    private static string GetMergeTagValue(
      string typeName,
      string propertyName,
      object[] context,
      bool suppressException = false)
    {
      object targetObject = (object) null;
      PropertyDescriptor propertyDescriptor = Merger.GetPropertyDescriptor(typeName, propertyName, context, out targetObject, suppressException);
      if (propertyDescriptor == null)
      {
        if (!suppressException)
          throw new InvalidOperationException(string.Format("The object of type '{0}' does not contain property '{1}'", (object) typeName, (object) propertyName));
        return string.Empty;
      }
      return targetObject is Subscriber && (targetObject as Subscriber).Id == Guid.Empty ? string.Empty : propertyDescriptor.GetValue(targetObject).ToString();
    }

    private static PropertyDescriptor GetPropertyDescriptor(
      string typeName,
      string propertyName,
      object[] context,
      out object targetObject,
      bool suppressException = false)
    {
      foreach (object obj in context)
      {
        Type type = obj.GetType();
        bool flag = false;
        if (typeName == type.Name)
        {
          flag = true;
        }
        else
        {
          Type baseType = type.BaseType;
          while (!(baseType.Name == typeName))
          {
            baseType = baseType.BaseType;
            if (!(baseType == typeof (object)))
              goto label_7;
          }
          flag = true;
        }
label_7:
        if (flag)
        {
          targetObject = obj;
          return TypeDescriptor.GetProperties(targetObject).Find(propertyName, false);
        }
      }
      if (!suppressException)
        throw new InvalidOperationException(string.Format("The property '{0}' cannot be found on the type '{1}' or any of it base types.", (object) propertyName, (object) typeName));
      targetObject = (object) null;
      return (PropertyDescriptor) null;
    }
  }
}
