// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.Thumbnails.MethodInfoContainer
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Libraries.ImageProcessing;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI.Thumbnails
{
  /// <summary>
  /// This class creates and maintains the state of the dynamic field controls that are used for displaying the
  /// parameters for the different thumbnail resize methods.
  /// </summary>
  public class MethodInfoContainer : CompositeControl
  {
    private bool dropDownSelectedIndexChanged;
    private string currentMethod;
    private IList<FieldControl> fields = (IList<FieldControl>) new List<FieldControl>();
    private IList<FieldControl> fieldsCausingRegen = (IList<FieldControl>) new List<FieldControl>();
    private DropDownList methodsList;
    private HtmlGenericControl whatsThisMethodWrapper;
    private HtmlGenericControl whatsThisMethodLink;
    private HtmlGenericControl whatsThisMethodInfo;
    private IImageProcessor imageProcessor;

    /// <inheritdoc />
    protected override void CreateChildControls()
    {
      this.fields.Clear();
      this.Controls.Clear();
      this.fieldsCausingRegen.Clear();
      IImageProcessor imageProcessor = this.ImageProcessor;
      List<ImageProcessingMethod> list = imageProcessor.Methods.Values.Where<ImageProcessingMethod>((Func<ImageProcessingMethod, bool>) (m => m.IsBrowsable)).ToList<ImageProcessingMethod>();
      if (list != null && list.Count > 0)
      {
        this.InitializeDropDownList((IEnumerable<ImageProcessingMethod>) list);
        if (this.currentMethod == null)
          this.currentMethod = list.First<ImageProcessingMethod>().MethodKey;
        ImageProcessingMethod method = (ImageProcessingMethod) null;
        if (imageProcessor.Methods.TryGetValue(this.currentMethod, out method))
          this.InitializeMethodProperties(method);
        else
          this.AddErrorMessage(Res.Get<LibrariesResources>("InvalidMethodName", (object) this.currentMethod));
      }
      else
        this.AddErrorMessage(Res.Get<LibrariesResources>("GeneratorHasNoMethods"));
    }

    /// <inheritdoc />
    protected override void OnInit(EventArgs e)
    {
      base.OnInit(e);
      if (this.Page == null)
        return;
      this.Page.RegisterRequiresControlState((Control) this);
    }

    /// <inheritdoc />
    protected override void OnPreRender(EventArgs e)
    {
      base.OnPreRender(e);
      if (this.methodsList == null)
        return;
      this.methodsList.SelectedValue = this.currentMethod;
      ImageProcessingMethod method = this.ImageProcessor.Methods[this.currentMethod];
      if (string.IsNullOrEmpty(method.DescriptionText) && string.IsNullOrEmpty(method.DescriptionImageResourceName))
        return;
      this.whatsThisMethodWrapper.Visible = true;
      this.whatsThisMethodWrapper.Attributes.Add("onclick", string.Format("javascript:jQuery(\"#{0}\").toggle(); dialogBase.resizeToContent();", (object) this.whatsThisMethodInfo.ClientID));
      this.whatsThisMethodInfo.Controls.Add((Control) new LiteralControl("<h2 class='sfGroupingTitle'>{0}</h2>".Arrange((object) method.GetTitle())));
      if (!string.IsNullOrEmpty(method.DescriptionText))
        this.whatsThisMethodInfo.Controls.Add((Control) new LiteralControl("<p class='sfNote'>{0}</p>".Arrange((object) method.DescriptionText)));
      if (string.IsNullOrEmpty(method.DescriptionImageResourceName))
        return;
      this.whatsThisMethodInfo.Controls.Add((Control) new LiteralControl("<img src=\"{0}\"></img>".Arrange((object) this.Page.ClientScript.GetWebResourceUrl(method.AssemblyInfo, method.DescriptionImageResourceName))));
    }

    /// <inheritdoc />
    protected override object SaveControlState() => (object) this.currentMethod;

    /// <inheritdoc />
    protected override void LoadControlState(object savedState) => this.currentMethod = (string) savedState;

    /// <summary>
    /// Initializes the drop down list for the resize methods.
    /// </summary>
    protected virtual void InitializeDropDownList(IEnumerable<ImageProcessingMethod> methods)
    {
      HtmlGenericControl child1 = new HtmlGenericControl("div");
      child1.Attributes.Add("class", "sfFormCtrl");
      this.Controls.Add((Control) child1);
      HtmlGenericControl child2 = new HtmlGenericControl("h2");
      child2.Attributes.Add("class", "sfGroupingTitle");
      child2.InnerText = Res.Get<LibrariesResources>().ResizeImageCaption;
      child1.Controls.Add((Control) child2);
      this.methodsList = new DropDownList();
      this.methodsList.AutoPostBack = true;
      this.methodsList.SelectedIndexChanged += new EventHandler(this.DropDown_SelectedIndexChanged);
      this.methodsList.Items.Add(new ListItem(string.Empty, string.Empty, false));
      foreach (ImageProcessingMethod method in methods)
        this.methodsList.Items.Add(new ListItem(method.GetTitle(), method.MethodKey));
      child1.Controls.Add((Control) this.methodsList);
      this.whatsThisMethodWrapper = new HtmlGenericControl("div");
      this.whatsThisMethodWrapper.Visible = false;
      this.whatsThisMethodWrapper.Attributes.Add("class", "sfDetailsPopupWrp sfInlineBlock sfLegendWrp sfLeftAligned");
      child1.Controls.Add((Control) this.whatsThisMethodWrapper);
      this.whatsThisMethodLink = new HtmlGenericControl("a");
      this.whatsThisMethodLink.InnerText = "What is this?";
      this.whatsThisMethodLink.Attributes.Add("class", "sfMoreDetails");
      this.whatsThisMethodWrapper.Controls.Add((Control) this.whatsThisMethodLink);
      this.whatsThisMethodInfo = new HtmlGenericControl("div");
      this.whatsThisMethodInfo.Attributes.Add("class", "sfDetailsPopup");
      this.whatsThisMethodInfo.Style.Add(HtmlTextWriterStyle.Display, "none");
      this.whatsThisMethodWrapper.Controls.Add((Control) this.whatsThisMethodInfo);
    }

    /// <summary>Initializes the dynamic controls area.</summary>
    /// <param name="properties">The properties.</param>
    /// <param name="methodKey">The method key.</param>
    protected virtual void InitializeMethodProperties(ImageProcessingMethod method)
    {
      IEnumerable<ImageProcessingProperty> argumentProperties = method.GetArgumentProperties();
      if (argumentProperties.Count<ImageProcessingProperty>() == 0)
        return;
      method.CreateArgumentInstance();
      foreach (ImageProcessingProperty processingProperty in argumentProperties)
      {
        PropertyInfo propertyInfo = processingProperty.PropertyInfo;
        Type propertyType = propertyInfo.PropertyType;
        Type baseType = propertyInfo.PropertyType.BaseType;
        FieldControl child;
        if (propertyType == typeof (bool))
        {
          child = (FieldControl) new ChoiceField()
          {
            RenderChoicesAs = RenderChoicesAs.SingleCheckBox
          };
          (child as ChoiceField).Choices.Add(new ChoiceItem()
          {
            Text = processingProperty.GetTitle()
          });
          child.CssClass = "sfFormCtrl sfCheckBox";
        }
        else if (propertyType == typeof (int))
        {
          child = (FieldControl) new TextField();
          child.CssClass = "sfFormCtrl sfShortField80";
          child.Title = processingProperty.GetTitle();
        }
        else if (baseType == typeof (Enum))
        {
          child = (FieldControl) new ChoiceField()
          {
            RenderChoicesAs = RenderChoicesAs.DropDown
          };
          child.CssClass = "sfFormCtrl";
          foreach (string name in Enum.GetNames(propertyType))
            (child as ChoiceField).Choices.Add(new ChoiceItem()
            {
              Text = name,
              Value = name
            });
          child.Title = processingProperty.GetTitle();
        }
        else
        {
          child = (FieldControl) new TextField();
          child.CssClass = "sfFormCtrl";
          child.Title = processingProperty.GetTitle();
        }
        child.ValidatorDefinition.RegularExpression = processingProperty.RegularExpression;
        child.ValidatorDefinition.RegularExpressionViolationMessage = processingProperty.GetRegularExpressionViolationMessage();
        child.ValidatorDefinition.Required = new bool?(processingProperty.IsRequired);
        child.ValidatorDefinition.MessageCssClass = "sfError";
        child.ID = propertyInfo.Name;
        child.DisplayMode = FieldDisplayMode.Write;
        child.DataFieldName = propertyInfo.Name;
        if (child is TextField)
          (child as TextField).Unit = processingProperty.GetUnits();
        this.fieldsCausingRegen.Add(child);
        this.fields.Add(child);
        this.Controls.Add((Control) child);
      }
    }

    /// <summary>Gets the field values.</summary>
    public NameValueCollection GetFieldValues()
    {
      NameValueCollection configCollection = new NameValueCollection();
      this.fields.Select(x => new
      {
        Key = x.DataFieldName,
        Value = x.Value.ToString()
      }).ToList().ForEach(x => configCollection.Add(x.Key, x.Value));
      return configCollection;
    }

    internal void LoadDefault()
    {
      this.EnsureChildControls();
      this.LoadCurrentMethod();
    }

    internal void LoadMethod(string methodName, NameValueCollection parameters)
    {
      this.CurrentMethod = methodName;
      this.LoadCurrentMethod(parameters);
    }

    private void LoadCurrentMethod(NameValueCollection parameters = null)
    {
      ImageProcessingMethod processingMethod = (ImageProcessingMethod) null;
      if (this.currentMethod.IsNullOrEmpty() || !this.ImageProcessor.Methods.TryGetValue(this.currentMethod, out processingMethod))
        return;
      object argumentInstance = processingMethod.CreateArgumentInstance(parameters);
      foreach (ImageProcessingProperty argumentProperty in processingMethod.GetArgumentProperties())
      {
        ImageProcessingProperty argProperty = argumentProperty;
        FieldControl fieldControl = this.fields.FirstOrDefault<FieldControl>((Func<FieldControl, bool>) (f => f.DataFieldName == argProperty.Name));
        if (fieldControl != null)
          fieldControl.Value = (object) argProperty.GetStringValue(argumentInstance);
      }
    }

    /// <summary>
    /// Determines whether the child field controls are valid.
    /// </summary>
    public bool IsValid()
    {
      bool valid = true;
      this.fields.OfType<TextField>().Where<TextField>((Func<TextField, bool>) (x => x.DisplayMode == FieldDisplayMode.Write)).ToList<TextField>().ForEach((Action<TextField>) (x => valid = valid && x.IsValid()));
      try
      {
        this.ImageProcessor.Methods[this.CurrentMethod].ValidateArguments((object) this.GetFieldValues());
      }
      catch (TargetInvocationException ex)
      {
        this.AddErrorMessage(ex.GetBaseException().Message);
        return false;
      }
      return valid;
    }

    internal IEnumerable<string> GetFieldControlIds() => (IEnumerable<string>) this.fields.OfType<TextField>().Where<TextField>((Func<TextField, bool>) (x => x.DisplayMode == FieldDisplayMode.Write)).Select<TextField, string>((Func<TextField, string>) (x => x.ClientID)).ToList<string>();

    internal IEnumerable<string> GetRegenFieldIds() => this.fieldsCausingRegen.Select<FieldControl, string>((Func<FieldControl, string>) (x => x.ClientID));

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    private void DropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.CurrentMethod = (sender as DropDownList).SelectedValue;
      this.dropDownSelectedIndexChanged = true;
    }

    private void AddErrorMessage(string message) => this.Controls.AddAt(0, (Control) new LiteralControl(string.Format("<div class=\"sfError\">{0}</div>", (object) message)));

    private IImageProcessor ImageProcessor
    {
      get
      {
        if (this.imageProcessor == null)
          this.imageProcessor = ObjectFactory.Resolve<IImageProcessor>();
        return this.imageProcessor;
      }
    }

    /// <summary>Gets or sets the current method.</summary>
    internal string CurrentMethod
    {
      get => this.currentMethod;
      set
      {
        if (!(this.currentMethod != value))
          return;
        this.currentMethod = value;
        this.RecreateChildControls();
      }
    }

    /// <summary>
    /// Gets a value indicating whether the current profile method has changed.
    /// </summary>
    public bool DropDownSelectedIndexChanged => this.dropDownSelectedIndexChanged;
  }
}
