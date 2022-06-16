// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Templates.PropertiesParser
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ComponentModel;
using System.Text;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Utilities.HtmlParsing;

namespace Telerik.Sitefinity.Web.UI.Templates
{
  internal class PropertiesParser : TemplateParser
  {
    private PropertyDescriptorCollection properties;

    internal PropertiesParser(ObjectBuilder parentBuilder, string html)
      : base(parentBuilder, html)
    {
    }

    private PropertyDescriptorCollection Props
    {
      get
      {
        if (this.properties == null)
          this.properties = TypeDescriptor.GetProperties(this.ParentBuilder.ObjectType);
        return this.properties;
      }
    }

    internal override bool ParseOpenTag(HtmlChunk chunk)
    {
      if (this.TagStack.Count == 0)
      {
        string name = this.ParentBuilder.DefaultProperty;
        if (string.IsNullOrEmpty(name))
          name = chunk.TagName;
        else if (this.StringBuffer == null)
          this.StringBuffer = new StringBuilder();
        PropertyDescriptor propDescr = this.Props.Find(name, false);
        if (propDescr == null)
          throw new FormatException(string.Format(Res.Get<ErrorMessages>().PropertyNotFound, (object) this.ParentBuilder.ObjectType, (object) name) + "\n" + this.Html);
        int num = this.CreatePropertyBuilder(chunk, propDescr) ? 1 : 0;
        if (!chunk.IsEndClosure)
          return num != 0;
        this.ParseCloseTag(chunk);
        return num != 0;
      }
      if (this.StringBuffer == null)
        this.StringBuffer = new StringBuilder();
      return false;
    }

    internal override void ParseCloseTag(HtmlChunk chunk)
    {
      if (this.TagStack.Count == 0 && this.StringBuffer != null)
      {
        if (!string.IsNullOrEmpty(this.ParentBuilder.DefaultProperty))
          this.StringBuffer.Append(chunk.Html);
        ((PropertyBuilder) this.ParentBuilder.ChildBuilders[this.ParentBuilder.ChildBuilders.Count - 1]).ParseProperty(this.StringBuffer.ToString());
        this.CurrentBuilder = (ObjectBuilder) null;
        this.StringBuffer = (StringBuilder) null;
      }
      else
      {
        if (this.StringBuffer != null || chunk.IsEndClosure)
          return;
        this.StringBuffer = new StringBuilder();
      }
    }

    internal override void ParseText(HtmlChunk chunk)
    {
    }

    private bool CreatePropertyBuilder(HtmlChunk chunk, PropertyDescriptor propDescr)
    {
      bool propertyBuilder = false;
      ObjectBuilder builder = (ObjectBuilder) new PropertyBuilder(propDescr, this.ParentBuilder);
      if (string.IsNullOrEmpty(this.ParentBuilder.DefaultProperty))
      {
        PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(builder.ObjectType);
        for (int index = 0; index < chunk.ParamsCount; ++index)
        {
          Attribute attribute = new Attribute(chunk.Attributes[index], chunk.Values[index], builder, properties);
          if (attribute.Substitutions != null)
          {
            chunk.Values[index] = attribute.ChangedValue;
            propertyBuilder = true;
          }
        }
      }
      this.CurrentBuilder = builder;
      return propertyBuilder;
    }
  }
}
