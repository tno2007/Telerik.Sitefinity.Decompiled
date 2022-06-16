// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Templates.ObjectBuilder
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Telerik.Sitefinity.Web.UI.Templates
{
  internal class ObjectBuilder
  {
    private string tagName;
    private Type objectType;
    private List<Attribute> attributes;
    private List<ObjectBuilder> childBuilders;
    private ObjectBuilder parent;
    private InnerTagsMode innerTagsMode;
    private List<TagNamespace> namespaces;
    private Type childrenType;
    private string defaultProperty;
    private string templatePath;
    private bool? isPlaceHolder;

    internal ObjectBuilder(ObjectBuilder parent, string html, string templatePath)
      : this(parent, html)
    {
      this.templatePath = templatePath;
    }

    internal ObjectBuilder(ObjectBuilder parent, string html)
    {
      this.Parent = parent;
      this.ParseHtml(html);
    }

    internal ObjectBuilder(ObjectBuilder parent)
      : this(parent, (string) null)
    {
    }

    internal virtual string TagName
    {
      get => this.tagName;
      set => this.tagName = value;
    }

    internal virtual Type ObjectType
    {
      get => this.objectType;
      set => this.objectType = !(value == (Type) null) ? value : throw new ArgumentNullException(nameof (value));
    }

    internal virtual Type ChildrenType
    {
      get => this.childrenType;
      set => this.childrenType = value;
    }

    internal virtual InnerTagsMode InnerTagsMode
    {
      get => this.innerTagsMode;
      set => this.innerTagsMode = value;
    }

    internal virtual string DefaultProperty
    {
      get => this.defaultProperty;
      set => this.defaultProperty = value;
    }

    internal virtual string TemplatePath => this.templatePath == null && this.parent != null ? this.parent.TemplatePath : this.templatePath;

    internal List<TagNamespace> Namespaces
    {
      get
      {
        if (this.namespaces == null)
          this.namespaces = this.Parent == null ? new List<TagNamespace>((IEnumerable<TagNamespace>) TemplateParser.RegisteredNamespaces) : this.Parent.Namespaces;
        return this.namespaces;
      }
      set => this.namespaces = value;
    }

    internal List<Attribute> Attributes
    {
      get
      {
        if (this.attributes == null)
          this.attributes = new List<Attribute>();
        return this.attributes;
      }
    }

    internal List<ObjectBuilder> ChildBuilders
    {
      get
      {
        if (this.childBuilders == null)
          this.childBuilders = new List<ObjectBuilder>();
        return this.childBuilders;
      }
    }

    internal virtual ObjectBuilder Parent
    {
      get => this.parent;
      set
      {
        if (this.parent != null)
          this.parent.ChildBuilders.Remove(this);
        this.parent = value;
        if (this.parent == null)
          return;
        this.parent.ChildBuilders.Add(this);
      }
    }

    internal virtual void ParseHtml(string html)
    {
      if (string.IsNullOrEmpty(html))
        return;
      this.ParseHtml(new TemplateParser(this, html));
    }

    internal virtual void ParseHtml(TemplateParser parser)
    {
      if (parser == null)
        throw new ArgumentNullException(nameof (parser));
      parser.Parse();
    }

    internal virtual object CreateObject(Control bindingContainer) => this.CreateObject(bindingContainer, (PlaceHoldersCollection) null);

    internal virtual object CreateObject(
      Control bindingContainer,
      PlaceHoldersCollection placeHolders)
    {
      object instance = Activator.CreateInstance(this.ObjectType);
      if (this.attributes != null)
      {
        foreach (Attribute attribute in this.Attributes)
          attribute.SetAttribute(instance, bindingContainer);
      }
      if (placeHolders != null)
      {
        if (!this.isPlaceHolder.HasValue)
        {
          this.isPlaceHolder = new bool?(instance is ContentPlaceHolder || instance is SitefinityPlaceHolder);
          bool? isPlaceHolder = this.isPlaceHolder;
          bool flag = false;
          if (isPlaceHolder.GetValueOrDefault() == flag & isPlaceHolder.HasValue && instance is HtmlGenericControl)
          {
            string attribute = (instance as HtmlGenericControl).Attributes["class"];
            this.isPlaceHolder = new bool?(attribute != null && attribute.Contains("sf_colsIn"));
          }
        }
        if (this.isPlaceHolder.Value)
          placeHolders.Add((Control) instance);
      }
      if (this.childBuilders != null)
      {
        foreach (ObjectBuilder childBuilder in this.childBuilders)
        {
          switch (childBuilder)
          {
            case PropertiesBuilder propertiesBuilder:
              propertiesBuilder.SetProperties(instance, bindingContainer);
              continue;
            case RootBuilder rootBuilder:
              rootBuilder.CreateChildControls((Control) instance, bindingContainer, placeHolders);
              continue;
            case ControlBuilder controlBuilder:
              ((Control) instance).Controls.Add(controlBuilder.CreateControl(bindingContainer, placeHolders));
              continue;
            default:
              throw new InvalidOperationException(string.Format("Unsupported Builder \"{0}\".", (object) childBuilder.GetType().FullName));
          }
        }
      }
      return instance;
    }
  }
}
