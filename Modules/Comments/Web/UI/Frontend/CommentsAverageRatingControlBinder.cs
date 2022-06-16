// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend.CommentsAverageRatingControlBinder
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web.UI;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend
{
  /// <summary>
  /// This control is used for setting the values of all CommentsAverageRatingControl on the page
  /// </summary>
  public class CommentsAverageRatingControlBinder : SimpleScriptView
  {
    internal static readonly string layoutTemplateName = "Telerik.Sitefinity.Resources.Templates.Frontend.Comments.CommentsAverageRatingControlBinder.ascx";
    internal const string scriptReference = "Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend.Scripts.CommentsAverageRatingControlBinder.js";
    internal static readonly string layoutTemplatePath = ControlUtilities.ToVppPath(CommentsAverageRatingControlBinder.layoutTemplateName);

    /// <summary>Obsolete. Use LayoutTemplatePath instead.</summary>
    protected override string LayoutTemplateName => string.Empty;

    /// <summary>Gets the layout template's relative or virtual path.</summary>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? CommentsAverageRatingControlBinder.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      if (this.Page.Items.Contains((object) typeof (CommentsAverageRatingControlBinder).FullName))
        return (IEnumerable<ScriptDescriptor>) null;
      this.Page.Items[(object) typeof (CommentsAverageRatingControlBinder).FullName] = (object) this;
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(this.GetType().FullName, this.ClientID);
      string str = this.Page.ResolveUrl("~/RestApi/comments-api");
      controlDescriptor.AddProperty("serviceUrl", (object) str);
      controlDescriptor.AddProperty("writeAReviewText", (object) Res.Get<CommentsResources>().WriteAReview);
      controlDescriptor.AddProperty("averageRatingText", (object) Res.Get<CommentsResources>().AverageRating);
      controlDescriptor.AddProperty("reviewText", (object) Res.Get<CommentsResources>().Review);
      controlDescriptor.AddProperty("reviewsText", (object) Res.Get<CommentsResources>().Reviews);
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        controlDescriptor
      };
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>()
    {
      new ScriptReference("Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend.Scripts.CommentsAverageRatingControlBinder.js", typeof (CommentsAverageRatingControlBinder).Assembly.GetName().ToString()),
      new ScriptReference("Telerik.Sitefinity.Resources.Scripts.Rating.rating.js", "Telerik.Sitefinity.Resources")
    };
  }
}
