// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Templates.TemplateParser
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Security;
using System.Text;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Utilities.HtmlParsing;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.Web.UI.Templates
{
  internal class TemplateParser
  {
    protected bool FirstOpen;
    protected string Html;
    protected int CurrentLevel;
    protected ObjectBuilder CurrentBuilder;
    protected ObjectBuilder ParentBuilder;
    protected StringBuilder StringBuffer;
    protected Stack<string> TagStack;
    private static Dictionary<string, Type> htmlTypes;
    private static Type literalType = typeof (LiteralControl);
    private static readonly TagNamespace[] namespaces;

    static TemplateParser()
    {
      try
      {
        PagesSection section = (PagesSection) WebConfigurationManager.GetSection("system.web/pages");
        TemplateParser.namespaces = new TagNamespace[section.Controls.Count];
        for (int index = 0; index < section.Controls.Count; ++index)
        {
          TagPrefixInfo control = section.Controls[index];
          TemplateParser.namespaces[index] = new TagNamespace(control.TagPrefix, control.Namespace);
        }
      }
      catch (SecurityException ex)
      {
        TemplateParser.namespaces = WebConfig.TagNamespaces;
      }
    }

    internal TemplateParser(ObjectBuilder parentBuilder, string html)
    {
      this.ParentBuilder = parentBuilder;
      this.Html = html;
    }

    internal static TagNamespace[] RegisteredNamespaces => TemplateParser.namespaces;

    internal virtual void Parse()
    {
      this.TagStack = new Stack<string>();
      using (HtmlParser htmlParser = new HtmlParser(this.Html))
      {
        htmlParser.SetChunkHashMode(false);
        htmlParser.AutoExtractBetweenTagsOnly = false;
        htmlParser.CompressWhiteSpaceBeforeTag = false;
        htmlParser.KeepRawHTML = true;
        HtmlChunk next;
        while ((next = htmlParser.ParseNext()) != null)
        {
          bool chunk = this.ParseChunk(next);
          if (this.StringBuffer != null && next.TagName != "%--")
            this.StringBuffer.Append(chunk ? next.GenerateHtml() : next.Html);
        }
      }
      this.FinalizeCurrentBuilder();
    }

    internal virtual bool ParseChunk(HtmlChunk chunk)
    {
      bool chunk1 = false;
      switch (chunk.Type)
      {
        case HtmlChunkType.Text:
        case HtmlChunkType.Script:
          this.ParseText(chunk);
          break;
        case HtmlChunkType.OpenTag:
          chunk1 = this.ParseOpenTag(chunk);
          if (!chunk.IsEndClosure && chunk.TagName != "%@")
            this.TagStack.Push(chunk.TagName);
          this.FirstOpen = true;
          break;
        case HtmlChunkType.CloseTag:
          if (!chunk.IsEndClosure && this.TagStack.Count > 0)
          {
            if (chunk.TagName == this.TagStack.Peek())
              this.TagStack.Pop();
            else if (this.TagStack.Contains(chunk.TagName))
            {
              while (chunk.TagName != this.TagStack.Peek())
                this.TagStack.Pop();
              this.TagStack.Pop();
            }
          }
          this.ParseCloseTag(chunk);
          break;
      }
      return chunk1;
    }

    internal virtual bool ParseOpenTag(HtmlChunk chunk)
    {
      if (chunk.TagName == "%@")
      {
        this.ParentBuilder.Namespaces.Add(new TagNamespace(chunk));
        return false;
      }
      if (this.TagStack.Count == this.CurrentLevel || this.CurrentBuilder != null && this.CurrentBuilder.ObjectType == TemplateParser.literalType)
      {
        if (chunk.HasAttribute("runat"))
          return this.CreateObjectBuilder(chunk);
        this.CreateLiteral();
      }
      else if (this.StringBuffer == null)
        this.StringBuffer = new StringBuilder();
      return false;
    }

    internal virtual void ParseCloseTag(HtmlChunk chunk)
    {
      if (chunk == null)
        throw new ArgumentNullException(nameof (chunk));
      if (this.CurrentLevel > this.TagStack.Count)
        this.CurrentLevel = this.TagStack.Count;
      if (this.TagStack.Count == this.CurrentLevel && this.CurrentBuilder != null && this.CurrentBuilder.ObjectType != TemplateParser.literalType)
        this.FinalizeCurrentBuilder();
      else
        this.CreateLiteral();
    }

    internal virtual void ParseText(HtmlChunk chunk)
    {
      if (chunk == null)
        throw new ArgumentNullException(nameof (chunk));
      if (this.TagStack.Count > this.CurrentLevel)
      {
        if (this.StringBuffer != null)
          return;
        this.StringBuffer = new StringBuilder();
      }
      else
      {
        if (!this.FirstOpen && this.ParentBuilder.Parent == null)
          return;
        this.CreateLiteral();
      }
    }

    protected virtual ObjectBuilder CreateBuilder() => (ObjectBuilder) new ControlBuilder(this.ParentBuilder);

    protected virtual bool CreateObjectBuilder(HtmlChunk chunk)
    {
      this.FinalizeCurrentBuilder();
      bool objectBuilder = false;
      Type c = this.ResolveType(chunk.TagName);
      if (this.ParentBuilder.ChildrenType != (Type) null && !this.ParentBuilder.ChildrenType.IsAssignableFrom(c))
        throw new FormatException(string.Format("The parent control \"{0}\" allows only child controls of type \"{1}\".", (object) this.ParentBuilder.ObjectType.FullName, (object) this.ParentBuilder.ChildrenType.FullName));
      ObjectBuilder builder = this.CreateBuilder();
      builder.TagName = chunk.TagName;
      builder.ObjectType = c;
      PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(builder.ObjectType);
      for (int index = 0; index < chunk.ParamsCount; ++index)
      {
        string attribute1 = chunk.Attributes[index];
        if (!(attribute1 == "runat"))
        {
          Attribute attribute2 = new Attribute(attribute1, chunk.Values[index], builder, properties);
          if (attribute2.Substitutions != null)
          {
            chunk.Values[index] = attribute2.ChangedValue;
            objectBuilder = true;
          }
        }
      }
      bool flag = false;
      if (System.Attribute.GetCustomAttribute((MemberInfo) builder.ObjectType, typeof (ParseChildrenAttribute)) is ParseChildrenAttribute customAttribute)
      {
        builder.ChildrenType = customAttribute.ChildControlType;
        if (customAttribute.ChildrenAsProperties && !string.IsNullOrEmpty(customAttribute.DefaultProperty))
        {
          builder.InnerTagsMode = InnerTagsMode.Properties;
          builder.DefaultProperty = customAttribute.DefaultProperty;
          flag = true;
        }
      }
      if (!flag)
      {
        foreach (PropertyDescriptor propertyDescriptor in properties)
        {
          PersistenceModeAttribute attribute = (PersistenceModeAttribute) propertyDescriptor.Attributes[typeof (PersistenceModeAttribute)];
          if (attribute != null)
          {
            switch (attribute.Mode)
            {
              case PersistenceMode.InnerProperty:
                builder.InnerTagsMode = InnerTagsMode.Properties;
                flag = true;
                break;
              case PersistenceMode.InnerDefaultProperty:
              case PersistenceMode.EncodedInnerDefaultProperty:
                builder.InnerTagsMode = InnerTagsMode.Text;
                builder.DefaultProperty = propertyDescriptor.Name;
                flag = true;
                break;
            }
            if (flag)
              break;
          }
        }
      }
      this.CurrentBuilder = builder;
      this.CurrentLevel = this.TagStack.Count;
      if (chunk.IsEndClosure)
        this.FinalizeCurrentBuilder();
      return objectBuilder;
    }

    protected virtual Type ResolveType(string tag)
    {
      Type type = (Type) null;
      int length = tag.IndexOf(':');
      if (length == -1)
      {
        if (!TemplateParser.HtmlTypes.TryGetValue(tag, out type))
          type = typeof (HtmlGenericControl);
      }
      else
      {
        string str1 = tag.Substring(0, length);
        string str2 = tag.Substring(length + 1);
        int num = 0;
        if (TemplateParser.namespaces != null)
        {
          while (type == (Type) null && num < this.ParentBuilder.Namespaces.Count)
          {
            TagNamespace tagNamespace = this.ParentBuilder.Namespaces[num++];
            if (tagNamespace.TagPrefix == str1)
            {
              type = TypeResolutionService.ResolveType(tagNamespace.Namespace + "." + str2, false);
              if (type != (Type) null)
                break;
            }
          }
        }
      }
      return !(type == (Type) null) ? type : throw new ArgumentException(Res.Get<ErrorMessages>("CannotResolveTypeInTemplate", (object) tag));
    }

    protected virtual void CreateLiteral()
    {
      if (this.CurrentBuilder != null)
        return;
      Type c = typeof (LiteralControl);
      if (!(this.ParentBuilder.ChildrenType == (Type) null) && !this.ParentBuilder.ChildrenType.IsAssignableFrom(c))
        return;
      this.StringBuffer = new StringBuilder();
      this.CurrentBuilder = (ObjectBuilder) new ControlBuilder(this.ParentBuilder);
      this.CurrentBuilder.TagName = "asp:LiteralControl";
      this.CurrentBuilder.ObjectType = c;
    }

    protected virtual void FinalizeCurrentBuilder()
    {
      if (this.CurrentBuilder == null)
        return;
      if (this.CurrentBuilder.TagName == "asp:LiteralControl")
      {
        Attribute attribute = new Attribute("Text", this.StringBuffer.ToString(), this.CurrentBuilder);
      }
      else if (this.StringBuffer != null)
      {
        switch (this.CurrentBuilder.InnerTagsMode)
        {
          case InnerTagsMode.Properties:
            PropertiesBuilder propertiesBuilder1 = new PropertiesBuilder(this.StringBuffer.ToString(), this.CurrentBuilder);
            break;
          case InnerTagsMode.Text:
            if (!this.CurrentBuilder.TagName.StartsWith("asp:", StringComparison.OrdinalIgnoreCase))
            {
              PropertiesBuilder propertiesBuilder2 = new PropertiesBuilder(this.StringBuffer.ToString(), this.CurrentBuilder);
              break;
            }
            RootBuilder rootBuilder1 = new RootBuilder((string) null, this.StringBuffer.ToString(), this.CurrentBuilder);
            break;
          default:
            RootBuilder rootBuilder2 = new RootBuilder((string) null, this.StringBuffer.ToString(), this.CurrentBuilder);
            break;
        }
      }
      this.CurrentBuilder = (ObjectBuilder) null;
      this.StringBuffer = (StringBuilder) null;
    }

    protected static Dictionary<string, Type> HtmlTypes
    {
      get
      {
        if (TemplateParser.htmlTypes == null)
        {
          TemplateParser.htmlTypes = new Dictionary<string, Type>();
          TemplateParser.htmlTypes.Add("a", typeof (HtmlAnchor));
          TemplateParser.htmlTypes.Add("form", typeof (HtmlForm));
          TemplateParser.htmlTypes.Add("head", typeof (HtmlHead));
          TemplateParser.htmlTypes.Add("img", typeof (HtmlImage));
          TemplateParser.htmlTypes.Add("link", typeof (HtmlLink));
          TemplateParser.htmlTypes.Add("meta", typeof (HtmlMeta));
          TemplateParser.htmlTypes.Add("select", typeof (HtmlSelect));
          TemplateParser.htmlTypes.Add("table", typeof (HtmlTable));
          TemplateParser.htmlTypes.Add("tr", typeof (HtmlTableRow));
          TemplateParser.htmlTypes.Add("td", typeof (HtmlTableCell));
          TemplateParser.htmlTypes.Add("title", typeof (HtmlTitle));
          TemplateParser.htmlTypes.Add("textarea", typeof (HtmlTextArea));
        }
        return TemplateParser.htmlTypes;
      }
    }
  }
}
