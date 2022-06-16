// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.Web.UI.Designers.DefaultContentOutPipeDesignerView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web.UI;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ControlDesign;

namespace Telerik.Sitefinity.Publishing.Web.UI.Designers
{
  internal class DefaultContentOutPipeDesignerView : ControlDesignerView
  {
    private readonly Dictionary<string, object> resources = new Dictionary<string, object>();
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Publishing.DefaultContentOutPipeDesignerView.ascx");
    internal const string controlScript = "Telerik.Sitefinity.Publishing.Web.UI.Scripts.DefaultContentOutPipeDesignerView.js";

    protected override string LayoutTemplateName => (string) null;

    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? DefaultContentOutPipeDesignerView.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    internal Dictionary<string, object> Resources => this.resources;

    internal void AddResource(IContentOutPipeDesignerView view)
    {
      string fullName = view.ContentType.FullName;
      IContentOutPipeDefaultDesignerView defaultDesignerInfo = view.GetDefaultDesignerInfo();
      this.resources.Add(fullName, (object) new ContentOutPipeDesignerResources()
      {
        ItemsName = defaultDesignerInfo.ItemsName
      });
    }

    protected override void InitializeControls(GenericContainer container) => this.resources.Add("default", (object) new ContentOutPipeDesignerResources()
    {
      AllItems = Res.Get<PublishingMessages>().AllItems
    });

    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(typeof (DefaultContentOutPipeDesignerView).FullName, this.ClientID);
      controlDescriptor.AddProperty("resources", (object) this.resources);
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        controlDescriptor
      };
    }

    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new ScriptReference[1]
    {
      new ScriptReference("Telerik.Sitefinity.Publishing.Web.UI.Scripts.DefaultContentOutPipeDesignerView.js", typeof (DefaultContentOutPipeDesignerView).Assembly.FullName)
    };
  }
}
