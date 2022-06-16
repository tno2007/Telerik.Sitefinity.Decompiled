// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ContentUI.PersonProfileView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Web.DataResolving;

namespace Telerik.Sitefinity.Web.UI.ContentUI
{
  /// <summary>Displays user information related to content item.</summary>
  public class PersonProfileView : Repeater
  {
    private bool dataBound;
    private const string invalidPersonNameFild = "Invalid PersonNameDataField specified. There is no field with the name \"{0}\" for item type: \"{1}\"";
    private const string invalidPersonIdFild = "Invalid PersonIdDataField specified. There is no field with the name \"{0}\" for item type: \"{1}\"";

    /// <summary>
    /// Gets or sets the text caption.
    /// If this property is set layouts will be ignored and no data binding will be performed.
    /// </summary>
    /// <value>The text.</value>
    public virtual string Text { get; set; }

    /// <summary>Gets or sets the person name data field.</summary>
    /// <value>The person name data field.</value>
    public virtual string PersonNameDataField { get; set; }

    /// <summary>
    /// Gets or sets the person identity data field.
    /// The field must be GUID.
    /// </summary>
    /// <value>The person pageId data field.</value>
    public virtual string PersonIdDataField { get; set; }

    /// <summary>Raises the DataBinding event.</summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnDataBinding(EventArgs e)
    {
      base.OnDataBinding(e);
      if (!string.IsNullOrEmpty(this.Text) || this.dataBound)
        return;
      IDataItemContainer dataItemContainer = this.GetDataItemContainer();
      if (dataItemContainer == null)
        throw new InvalidOperationException("This control can be used only within a data bound item template.");
      this.dataBound = true;
      object dataItem = dataItemContainer.DataItem;
      if (dataItem == null)
        return;
      if (this.ItemTemplate != null)
      {
        if (!string.IsNullOrEmpty(this.PersonNameDataField))
        {
          string s = (TypeDescriptor.GetProperties(dataItem)[this.PersonNameDataField] ?? throw new ArgumentException("Invalid PersonNameDataField specified. There is no field with the name \"{0}\" for item type: \"{1}\"".Arrange((object) this.PersonNameDataField, (object) dataItem.GetType().FullName))).GetValue(dataItem) as string;
          if (!string.IsNullOrEmpty(s))
          {
            this.Text = HttpUtility.HtmlEncode(s);
            return;
          }
        }
        Guid id = Guid.Empty;
        if (!string.IsNullOrEmpty(this.PersonIdDataField))
          id = (Guid) (TypeDescriptor.GetProperties(dataItem)[this.PersonIdDataField] ?? throw new ArgumentException("Invalid PersonIdDataField specified. There is no field with the name \"{0}\" for item type: \"{1}\"".Arrange((object) this.PersonNameDataField, (object) dataItem.GetType().FullName))).GetValue(dataItem);
        else if (dataItem is IOwnership)
          id = ((IOwnership) dataItem).Owner;
        if (!(id != Guid.Empty))
          return;
        User user = UserManager.FindUser(id);
        this.DataSource = (object) new ProfileDataItem[1]
        {
          new ProfileDataItem()
          {
            User = user,
            Profile = (UserProfile) null
          }
        };
      }
      else
        this.Text = HttpUtility.HtmlEncode(DataResolver.Resolve(dataItem, "Author", this.PersonNameDataField));
    }

    /// <summary>Renders the content.</summary>
    /// <param name="writer">The writer.</param>
    protected override void Render(HtmlTextWriter writer)
    {
      if (!string.IsNullOrEmpty(this.Text))
        writer.Write(this.Text);
      else
        base.Render(writer);
    }
  }
}
