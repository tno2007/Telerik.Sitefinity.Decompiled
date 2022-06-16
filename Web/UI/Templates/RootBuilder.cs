// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Templates.RootBuilder
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Localization;

namespace Telerik.Sitefinity.Web.UI.Templates
{
  internal class RootBuilder : ControlBuilder
  {
    private bool isContent;

    public RootBuilder(string templatePath, string html)
      : this(templatePath, html, (ObjectBuilder) null)
    {
    }

    public RootBuilder(string templatePath, string html, ObjectBuilder parent)
      : base(parent, html, templatePath)
    {
      this.TagName = nameof (RootBuilder);
    }

    internal override Type ChildrenType
    {
      get => this.Parent != null ? this.Parent.ChildrenType : (Type) null;
      set
      {
        if (this.Parent == null)
          return;
        this.Parent.ChildrenType = value;
      }
    }

    internal override InnerTagsMode InnerTagsMode
    {
      get => InnerTagsMode.ChildControls;
      set => base.InnerTagsMode = value;
    }

    internal override Control CreateControl(Control bindingContainer)
    {
      Control parent = new Control();
      this.CreateChildControls(parent, bindingContainer);
      return parent;
    }

    internal void CreateChildControls(Control parent, Control bindingContainer)
    {
      if (parent == null)
        throw new ArgumentNullException(nameof (parent));
      foreach (ControlBuilder childBuilder in this.ChildBuilders)
      {
        if (childBuilder is RootBuilder)
          ((RootBuilder) childBuilder).CreateChildControls(parent, bindingContainer);
        else
          parent.Controls.Add(childBuilder.CreateControl(bindingContainer));
      }
    }

    internal void CreateChildControls(
      Control parent,
      Control bindingContainer,
      PlaceHoldersCollection placeHolders)
    {
      if (parent == null)
        throw new ArgumentNullException(nameof (parent));
      for (int index1 = 0; index1 < this.ChildBuilders.Count; ++index1)
      {
        ControlBuilder childBuilder = (ControlBuilder) this.ChildBuilders[index1];
        if (childBuilder is RootBuilder rootBuilder)
        {
          rootBuilder.CreateChildControls(parent, bindingContainer, placeHolders);
        }
        else
        {
          Control control1 = childBuilder.CreateControl(bindingContainer, placeHolders);
          if (control1 is Content content)
          {
            if (!this.isContent)
            {
              this.isContent = true;
              for (int index2 = index1 - 1; index2 > -1; --index2)
                parent.Controls.RemoveAt(parent.Controls.Count - 1);
            }
            Control control2;
            if (!placeHolders.TryGetValue(content.ContentPlaceHolderID, out control2))
              throw new TemplateException(Res.Get<ErrorMessages>().InvalidPlaceHolderSpecified.Arrange((object) content.GetType().FullName, (object) content.ID, (object) this.TemplatePath, (object) content.ContentPlaceHolderID));
            foreach (Control child in new ArrayList((ICollection) content.Controls))
              control2.Controls.Add(child);
          }
          else if (!this.isContent)
            parent.Controls.Add(control1);
        }
      }
    }
  }
}
