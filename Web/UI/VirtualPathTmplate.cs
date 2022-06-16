// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.VirtualPathTemplate
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Telerik.Sitefinity.Web.UI
{
  public class VirtualPathTemplate : IContentPlaceHolderContainer, ITemplate
  {
    private bool isPage;
    private string virtualPath;
    private readonly bool addChildrenAsDirectDescendants;

    internal VirtualPathTemplate(string virtualPath, bool addChildrenAsDirectDescendants)
    {
      this.addChildrenAsDirectDescendants = addChildrenAsDirectDescendants;
      this.virtualPath = virtualPath;
      this.isPage = virtualPath.EndsWith(".aspx", StringComparison.OrdinalIgnoreCase);
    }

    public bool IsPage => this.isPage;

    public string VirtualPath => this.virtualPath;

    public void InstantiateIn(Control container) => this.InstantiateIn(container, (PlaceHoldersCollection) null);

    public void InstantiateIn(Control container, PlaceHoldersCollection placeHolders)
    {
      if (container == null)
        throw new ArgumentNullException(nameof (container));
      if (container is GenericContainer genericContainer)
      {
        genericContainer.IsExternal = true;
        genericContainer.TemplateName = this.virtualPath;
      }
      bool flag = false;
      TemplateControl instance = this.GetInstance(container.Page);
      if (placeHolders != null)
      {
        foreach (object control1 in instance.Controls)
        {
          if (control1 is Content content)
          {
            flag = true;
            Control placeHolder = placeHolders[content.ContentPlaceHolderID];
            foreach (Control control2 in content.Controls)
              placeHolder.Controls.Add(control2);
          }
        }
        foreach (Control control in new ControlTraverser((Control) instance, TraverseMethod.DepthFirst))
        {
          if (control is HtmlGenericControl htmlGenericControl && htmlGenericControl.Attributes["class"].Contains("sf_colsIn"))
            placeHolders.Add((Control) htmlGenericControl);
        }
        foreach (Control control in new ControlTraverser(container, TraverseMethod.DepthFirst))
        {
          if (control is ContentPlaceHolder || control is SitefinityPlaceHolder)
            placeHolders.Add(control);
        }
      }
      if (flag)
        return;
      if (!this.addChildrenAsDirectDescendants)
      {
        container.Controls.Add((Control) instance);
      }
      else
      {
        Control[] controlArray = new Control[instance.Controls.Count];
        instance.Controls.CopyTo((Array) controlArray, 0);
        for (int index = 0; index < controlArray.Length; ++index)
        {
          container.Controls.Add(controlArray[index]);
          instance.Controls.Remove(controlArray[index]);
        }
      }
    }

    PlaceHoldersCollection IContentPlaceHolderContainer.InstantiateIn(
      Control container)
    {
      this.InstantiateIn(container);
      PlaceHoldersCollection placeHolders = new PlaceHoldersCollection();
      this.InstantiateIn(container, placeHolders);
      return placeHolders;
    }

    public TemplateControl GetInstance() => this.GetInstance((Page) null);

    internal TemplateControl GetInstance(Page page) => ControlUtilities.LoadControl(this.virtualPath, page);
  }
}
