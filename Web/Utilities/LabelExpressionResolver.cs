// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Utilities.LabelExpressionResolver
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text.RegularExpressions;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Web.Utilities
{
  /// <summary>Parse Sitefinity Label Markup expressions</summary>
  /// <remarks>
  /// <para>
  /// This is a very simple custom makrup language, consisting of two constructs: conditions and property replacements.
  /// </para>
  /// <para>
  /// Property replacements have the format of <c>{{contextKey.contextPropertyName:optionalFormatString}}</c>.
  /// Conditional templates have the format of <c>[[[contextKey.contextPropertyName]templateText]]</c>.
  /// </para>
  /// <para>When one parses a template, the <c>context</c> parameter contains a named collection of objects
  /// whose properties are going to be used for resolving "contextKey.contextPropertyName". <c>contextKey</c> is used to retrieve the value
  /// in the context with that key, and the value's properties are looked up for <c>contextPropertyName</c>.</para>
  /// <para>In property replacements, the resolved value of "contextKey.contextPropertyName" is converted to string, using
  /// the <c>optionalFormatString</c> if the value is IFormattable.
  /// </para>
  /// <para>In conditional templates, the resolved value of "contextKey.contextPropertyName" is used to determine whether
  /// <c>templateText</c> is rendered or not. If the value is not null, boolean and is true, type converter or ToString result in
  /// non-empty string, then the condition is considered as true and the inner template is rendered.
  /// </para>
  /// </remarks>
  /// <example>
  /// <![CDATA[
  /// class TestClass1
  /// {
  ///    public DateTime PublicationDate { get; set; }
  ///    public DateTime? ExpirationDate { get; set; }
  /// }
  /// 
  /// class TestClass2
  /// {
  ///    public DateTime? LastModified { get; set; }
  /// }
  /// 
  /// class Program
  /// {
  ///    static void Main()
  ///    {
  ///        string curTemplate = @"Scheduled publish on {{content.PublicationDate:dd/MM/yy 'at' hh:mm}}[[[workflow.LastModified], unpublish on {{workflow.LastModified:dd/MM/yy 'at' hh:mm}}]]";
  ///        Dictionary<string, object> context = new Dictionary<string, object>();
  ///        context["content"] = new TestClass1() { PublicationDate = DateTime.Parse("2010/02/14"), ExpirationDate = null };
  ///        context["workflow2"] = new TestClass2() { LastModified = null };
  ///        context["workflow"] = new TestClass2() { LastModified = DateTime.Parse("2010/11/29") };
  /// 
  ///        string parsed = LabelExpressionResolver.Parse(curTemplate, context);
  ///        Console.WriteLine("{0}\n\t=>\n{1}", curTemplate, parsed);
  ///     }
  /// }
  /// ]]>
  /// </example>
  public class LabelExpressionResolver
  {
    private static Regex templateExpression = new Regex("\\[\\[\\[(?<condition>[^\\]]+)\\](?<template>.+)\\]\\]", RegexOptions.Multiline | RegexOptions.Compiled);
    private static Regex valueReplaceExpression = new Regex("\\{\\{(?<propName>[^:(\\}\\})]+)(?<format>:[^\\}\\}]+)?\\}\\}", RegexOptions.Multiline | RegexOptions.Compiled);
    private Dictionary<string, object> context;
    private CultureInfo culture;

    /// <summary>
    /// Use <c>LabelExpressionResolver.Parse</c> unless you are overriding this class
    /// </summary>
    /// <param name="context"></param>
    public LabelExpressionResolver(Dictionary<string, object> context) => this.context = context;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="template"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public static string Parse(string template, Dictionary<string, object> context) => LabelExpressionResolver.Parse(template, context, (CultureInfo) null);

    public static string Parse(
      string template,
      Dictionary<string, object> context,
      CultureInfo info)
    {
      return new LabelExpressionResolver(context).Evaluate(template, info);
    }

    protected virtual string EvaluateTemplates(Match match)
    {
      if (match.Groups["condition"].Success)
      {
        LabelExpressionResolver.PropertyContext propertyContext = this.GetPropertyContext(match.Groups["condition"].Value);
        if (propertyContext != null && propertyContext.Property != null)
          return this.ShouldRenderTemplate(propertyContext.Property, propertyContext.Context) && match.Groups["template"].Success ? match.Groups["template"].Value : string.Empty;
      }
      return match.Value;
    }

    protected virtual string EvaluateValueReplace(Match match)
    {
      if (match.Groups["propName"].Success)
      {
        LabelExpressionResolver.PropertyContext propertyContext = this.GetPropertyContext(match.Groups["propName"].Value);
        object obj;
        if (propertyContext != null && propertyContext.Context != null && (obj = propertyContext.Property.GetValue(propertyContext.Context)) != null)
        {
          if (propertyContext.Property.PropertyType == typeof (DateTime) && ((DateTime) obj).Kind != DateTimeKind.Local)
            obj = (object) ((DateTime) obj).ToSitefinityOrLocaUITime();
          if (propertyContext.Property.PropertyType == typeof (DateTime?))
          {
            DateTime? nullable = (DateTime?) obj;
            if (nullable.HasValue && nullable.Value.Kind != DateTimeKind.Local)
              nullable = new DateTime?(nullable.Value.ToSitefinityOrLocaUITime());
            obj = (object) nullable;
          }
          string valueReplace;
          if (obj is IFormattable && match.Groups["format"].Success)
          {
            string format = match.Groups["format"].Value.TrimStart(':');
            valueReplace = ((IFormattable) obj).ToString(format, (IFormatProvider) this.culture);
          }
          else
            valueReplace = !(propertyContext.Property.PropertyType == typeof (string)) ? (!(propertyContext.Property.PropertyType == typeof (Lstring)) ? (!propertyContext.Property.Converter.CanConvertTo(typeof (string)) ? obj.ToString() : propertyContext.Property.Converter.ConvertToString(obj)) : ((Lstring) obj).GetString(this.culture, true)) : (string) obj;
          return valueReplace;
        }
      }
      return match.Value;
    }

    protected virtual bool ShouldRenderTemplate(PropertyDescriptor prop, object context)
    {
      object obj = prop.GetValue(context);
      if (obj == null)
        return false;
      return prop.PropertyType == typeof (bool) && (bool) obj || !string.IsNullOrEmpty(obj.ToString());
    }

    private string Evaluate(string template, CultureInfo info)
    {
      if (this.context == null)
        return template;
      this.culture = info ?? SystemManager.CurrentContext.Culture;
      string input = LabelExpressionResolver.valueReplaceExpression.Replace(template, new MatchEvaluator(this.EvaluateValueReplace));
      return LabelExpressionResolver.templateExpression.Replace(input, new MatchEvaluator(this.EvaluateTemplates));
    }

    private LabelExpressionResolver.PropertyContext GetPropertyContext(
      string text)
    {
      if (!string.IsNullOrEmpty(text))
      {
        text = text.Trim();
        string[] strArray = text.Split(new char[1]{ '.' }, StringSplitOptions.RemoveEmptyEntries);
        if (strArray.Length == 2)
        {
          LabelExpressionResolver.PropertyContext propertyContext = new LabelExpressionResolver.PropertyContext();
          if (this.context.TryGetValue(strArray[0], out propertyContext.Context))
          {
            propertyContext.Property = TypeDescriptor.GetProperties(propertyContext.Context).Find(strArray[1], false);
            if (propertyContext.Property != null)
              return propertyContext;
          }
        }
      }
      return (LabelExpressionResolver.PropertyContext) null;
    }

    private class PropertyContext
    {
      public PropertyDescriptor Property;
      public object Context;
    }
  }
}
