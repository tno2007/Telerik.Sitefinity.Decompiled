// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.PublicControls.Designers.TwitterFeedDesigner
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Web.UI.PublicControls.Designers
{
  /// <summary>Represents a designer for twitter feed control.</summary>
  [Obsolete("Will be remove in future relases")]
  public class TwitterFeedDesigner : ControlDesignerBase
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Designers.Social.TwitterFeedDesigner.ascx");
    internal const string designerScriptName = "Telerik.Sitefinity.Web.UI.PublicControls.Designers.Scripts.TwitterFeedDesigner.js";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.PublicControls.Designers.TwitterFeedDesigner" /> class.
    /// </summary>
    public TwitterFeedDesigner() => this.LayoutTemplatePath = TwitterFeedDesigner.layoutTemplatePath;

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Gets the wrapper tag of rendered control</summary>
    /// <value></value>
    /// <remarks>Override this property to change wrapper tag</remarks>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Gets the choice field listing all possible widget types
    /// </summary>
    protected ChoiceField WidgetTypeChoiceField => this.Container.GetControl<ChoiceField>("widgetTypeChoiceField", true);

    /// <summary>Choicefield that the widged will have scrollbar</summary>
    protected ChoiceField IncludeScrollBarChoiceField => this.Container.GetControl<ChoiceField>(nameof (IncludeScrollBarChoiceField), true);

    /// <summary>
    /// Gets the RadTabStrip control which wraps the content of the desinger
    /// </summary>
    protected RadTabStrip RadTabStrip1 => this.Container.GetControl<RadTabStrip>(nameof (RadTabStrip1), true);

    /// <summary>
    /// Gets the TextField control representing the Username of the Twitter feed widget
    /// </summary>
    protected TextField UsernameTextField => this.Container.GetControl<TextField>(nameof (UsernameTextField), true);

    /// <summary>
    /// Gets the TextField control representing the Title of the Twitter feed widget
    /// </summary>
    protected TextField TitleTextField => this.Container.GetControl<TextField>(nameof (TitleTextField), true);

    /// <summary>
    /// Gets the TextField control representing the SubTitle of the Twitter feed widget
    /// </summary>
    protected TextField SubtitleTextField => this.Container.GetControl<TextField>(nameof (SubtitleTextField), true);

    /// <summary>
    /// Gets the TextField control representing the Search field of the Twitter feed widget
    /// </summary>
    protected TextField SearchTextField => this.Container.GetControl<TextField>(nameof (SearchTextField), true);

    /// <summary>
    /// Gets the choice field listing all possible loading types
    /// </summary>
    protected ChoiceField TimingChoiceField => this.Container.GetControl<ChoiceField>(nameof (TimingChoiceField), true);

    /// <summary>Gets the label which shows the username</summary>
    protected Label UserNameLabel => this.Container.GetControl<Label>(nameof (UserNameLabel), true);

    /// <summary>
    /// Gets the choice field listing all possible values for tweets loading
    /// </summary>
    protected ChoiceField LoadEveryChoiceField => this.Container.GetControl<ChoiceField>(nameof (LoadEveryChoiceField), true);

    /// <summary>
    /// Gets the choice field listing all list names for twitter account
    /// </summary>
    protected TextField ListOfTextField => this.Container.GetControl<TextField>(nameof (ListOfTextField), true);

    /// <summary>
    /// Gets the TextField control representing the number of displayed tweets
    /// </summary>
    protected TextField DisplayTweetsTextField => this.Container.GetControl<TextField>(nameof (DisplayTweetsTextField), true);

    /// <summary>
    /// Choicefield that the widged will show Timestamp, Avatars, Hashtags
    /// </summary>
    protected ChoiceField IncludeChoiceField => this.Container.GetControl<ChoiceField>(nameof (IncludeChoiceField), true);

    /// <summary>
    /// Choicefield that the widged will be sized automatically or manually
    /// </summary>
    protected ChoiceField SizeChoiceField => this.Container.GetControl<ChoiceField>(nameof (SizeChoiceField), true);

    /// <summary>
    /// Choicefield that the widged will be colorized automatically or manually
    /// </summary>
    protected ChoiceField ColorChoiceField => this.Container.GetControl<ChoiceField>(nameof (ColorChoiceField), true);

    /// <summary>
    /// Gets the TextField control representing the width of the widget
    /// </summary>
    protected TextField WidthTextField => this.Container.GetControl<TextField>(nameof (WidthTextField), true);

    /// <summary>
    /// Gets the TextField control representing the height of the widget
    /// </summary>
    protected TextField HeightTextField => this.Container.GetControl<TextField>(nameof (HeightTextField), true);

    /// <summary>
    /// Gets the RadColorPicker control that sets the shell background color of the widget
    /// </summary>
    protected RadColorPicker ShellBackgroundPicker => this.Container.GetControl<RadColorPicker>(nameof (ShellBackgroundPicker), true);

    /// <summary>
    /// Gets the RadColorPicker control that sets the shell text color of the widget
    /// </summary>
    protected RadColorPicker ShellTextColorPicker => this.Container.GetControl<RadColorPicker>(nameof (ShellTextColorPicker), true);

    /// <summary>
    /// Gets the RadColorPicker control that sets the tweet background color of the widget
    /// </summary>
    protected RadColorPicker TweetBackgroundColorPicker => this.Container.GetControl<RadColorPicker>(nameof (TweetBackgroundColorPicker), true);

    /// <summary>
    /// Gets the RadColorPicker control that sets the tweet text color of the widget
    /// </summary>
    protected RadColorPicker TweetTextColorColorPicker => this.Container.GetControl<RadColorPicker>(nameof (TweetTextColorColorPicker), true);

    /// <summary>
    /// Gets the RadColorPicker control that sets the links color of the widget
    /// </summary>
    protected RadColorPicker LinksColorPicker => this.Container.GetControl<RadColorPicker>(nameof (LinksColorPicker), true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
    }

    /// <summary>Gets the script descriptors.</summary>
    /// <returns></returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      List<ScriptDescriptor> source = new List<ScriptDescriptor>(base.GetScriptDescriptors());
      ScriptControlDescriptor controlDescriptor = (ScriptControlDescriptor) source.Last<ScriptDescriptor>();
      controlDescriptor.AddComponentProperty("widgetTypeChoiceField", this.WidgetTypeChoiceField.ClientID);
      controlDescriptor.AddComponentProperty("usernameTextField", this.UsernameTextField.ClientID);
      controlDescriptor.AddComponentProperty("titleTextField", this.TitleTextField.ClientID);
      controlDescriptor.AddComponentProperty("subtitleTextField", this.SubtitleTextField.ClientID);
      controlDescriptor.AddComponentProperty("searchTextField", this.SearchTextField.ClientID);
      controlDescriptor.AddComponentProperty("timingChoiceField", this.TimingChoiceField.ClientID);
      controlDescriptor.AddComponentProperty("loadEveryChoiceField", this.LoadEveryChoiceField.ClientID);
      controlDescriptor.AddComponentProperty("displayTweetsTextField", this.DisplayTweetsTextField.ClientID);
      controlDescriptor.AddComponentProperty("includeChoiceField", this.IncludeChoiceField.ClientID);
      controlDescriptor.AddComponentProperty("sizeChoiceField", this.SizeChoiceField.ClientID);
      controlDescriptor.AddComponentProperty("colorChoiceField", this.ColorChoiceField.ClientID);
      controlDescriptor.AddComponentProperty("widthTextField", this.WidthTextField.ClientID);
      controlDescriptor.AddComponentProperty("heightTextField", this.HeightTextField.ClientID);
      controlDescriptor.AddComponentProperty("shellBackgroundPicker", this.ShellBackgroundPicker.ClientID);
      controlDescriptor.AddComponentProperty("shellTextColorPicker", this.ShellTextColorPicker.ClientID);
      controlDescriptor.AddComponentProperty("tweetBackgroundColorPicker", this.TweetBackgroundColorPicker.ClientID);
      controlDescriptor.AddComponentProperty("tweetTextColorColorPicker", this.TweetTextColorColorPicker.ClientID);
      controlDescriptor.AddComponentProperty("linksColorPicker", this.LinksColorPicker.ClientID);
      controlDescriptor.AddComponentProperty("listOfTextField", this.ListOfTextField.ClientID);
      controlDescriptor.AddElementProperty("userNameLabel", this.UserNameLabel.ClientID);
      controlDescriptor.AddComponentProperty("includeScrollBarChoiceField", this.IncludeScrollBarChoiceField.ClientID);
      controlDescriptor.AddComponentProperty("radTabStrip", this.RadTabStrip1.ClientID);
      controlDescriptor.AddProperty("valueOfUsernameLabel", (object) this.UserNameLabel.Text);
      return (IEnumerable<ScriptDescriptor>) source;
    }

    /// <summary>Gets the script references.</summary>
    /// <returns></returns>
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      string assembly = typeof (TwitterFeedDesigner).Assembly.GetName().ToString();
      return (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
      {
        new ScriptReference("Telerik.Sitefinity.Web.UI.PublicControls.Designers.Scripts.TwitterFeedDesigner.js", assembly)
      };
    }
  }
}
