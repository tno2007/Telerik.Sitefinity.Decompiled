// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.AssetsFieldBinder
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.UI;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Model.ContentLinks;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>
  /// This class provides binding functionality for the assets field
  /// on the public side in the read mode.
  /// </summary>
  public class AssetsFieldBinder
  {
    /// <summary>
    /// Finds all the assets fields in the container and binds them to the model.
    /// </summary>
    /// <param name="container">
    /// The container in which the instances of <see cref="T:Telerik.Sitefinity.Web.UI.Fields.AssetsField" /> should
    /// be found.
    /// </param>
    /// <param name="model">
    /// The instance of the data item to which the assets fields should be bound.
    /// </param>
    public static void BindAllAssetsFields(Control container, IDataItem model)
    {
      if (container == null)
        throw new ArgumentNullException(nameof (container));
      if (model == null)
        throw new ArgumentNullException(nameof (model));
      IList<AssetsField> assetsFields = (IList<AssetsField>) new List<AssetsField>();
      AssetsFieldBinder.FindAllAssetsControls(container, assetsFields);
      foreach (AssetsField assetsField in (IEnumerable<AssetsField>) assetsFields)
        AssetsFieldBinder.BindAssetFieldControl(assetsField, model);
    }

    private static void FindAllAssetsControls(Control container, IList<AssetsField> assetsFields)
    {
      foreach (Control control in container.Controls)
      {
        if (control is AssetsField assetsField)
          assetsFields.Add(assetsField);
        else
          AssetsFieldBinder.FindAllAssetsControls(control, assetsFields);
      }
    }

    internal static void BindAssetFieldControl(AssetsField assetsField, IDataItem model)
    {
      if (assetsField == null)
        throw new ArgumentNullException(nameof (assetsField));
      if (model == null)
        throw new ArgumentNullException(nameof (model));
      string dataFieldName = assetsField.DataFieldName;
      PropertyDescriptor propertyDescriptor = !string.IsNullOrEmpty(dataFieldName) ? TypeDescriptor.GetProperties((object) model)[dataFieldName] : throw new ArgumentException("The AssetsField must have defined DataFieldName.");
      if (propertyDescriptor == null)
        return;
      ContentLink[] source = (ContentLink[]) propertyDescriptor.GetValue((object) model);
      if (source.Length != 0)
      {
        string childItemType = ((IEnumerable<ContentLink>) source).First<ContentLink>().ChildItemType;
        if (childItemType == typeof (Image).FullName)
          assetsField.WorkMode = source.Length <= 1 ? AssetsWorkMode.SingleImage : AssetsWorkMode.MultipleImages;
        else if (childItemType == typeof (Document).FullName)
          assetsField.WorkMode = source.Length <= 1 ? AssetsWorkMode.SingleDocument : AssetsWorkMode.MultipleDocuments;
        else if (childItemType == typeof (Video).FullName)
          assetsField.WorkMode = source.Length <= 1 ? AssetsWorkMode.SingleVideo : AssetsWorkMode.MultipleVideos;
      }
      assetsField.ContentLinks = ((IEnumerable<ContentLink>) source).OrderBy<ContentLink, float>((Func<ContentLink, float>) (c => c.Ordinal)).ToArray<ContentLink>();
    }
  }
}
