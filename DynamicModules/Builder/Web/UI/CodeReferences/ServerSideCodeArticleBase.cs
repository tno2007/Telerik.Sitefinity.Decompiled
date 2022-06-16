// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.Web.UI.CodeReferences.ServerSideCodeArticleBase
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.DynamicModules.Builder.Web.UI.CodeReferences
{
  /// <summary>
  /// Base class for all code articles that explain server side code.
  /// </summary>
  public abstract class ServerSideCodeArticleBase : CodeArticleBase
  {
    private static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.ModuleBuilder.ServerSideCodeArticle.ascx");
    protected readonly string[] TaxonomiesNamespaces = new string[2]
    {
      "Telerik.Sitefinity.Taxonomies",
      "Telerik.Sitefinity.Taxonomies.Model"
    };
    protected readonly string[] MediaNamespaces = new string[1]
    {
      "Telerik.Sitefinity.Modules.Libraries"
    };
    protected readonly string[] MultilingualNamespaces = new string[2]
    {
      "System.Globalization",
      "System.Threading"
    };
    protected readonly string[] AddressNamespaces = new string[4]
    {
      "Telerik.Sitefinity.Locations.Configuration",
      "Telerik.Sitefinity.GeoLocations.Model",
      "Telerik.Sitefinity.Configuration",
      "Telerik.Sitefinity.Locations"
    };

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    public override string LayoutTemplatePath
    {
      get => ServerSideCodeArticleBase.layoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets the reference to the control which displays the code
    /// example in c# syntax.
    /// </summary>
    protected virtual Literal CSharpCode => this.Container.GetControl<Literal>("cSharpCode", true);

    /// <summary>
    /// Gets the reference to the control which displays the code
    /// example in VB.NET syntax.
    /// </summary>
    protected virtual Literal VBCode => this.Container.GetControl<Literal>("vbCode", true);

    /// <summary>
    /// Gets the reference to the control which displays the code
    /// usings needed for the c# example.
    /// </summary>
    protected virtual Label CSharpCodeUsings => this.Container.GetControl<Label>("cSharpCodeUsings", true);

    /// <summary>
    /// Gets the reference to the control which displays the code
    /// imports needed for the VB.NET example.
    /// </summary>
    protected virtual Label VBCodeImports => this.Container.GetControl<Label>("vbNetCodeImports", true);

    /// <summary>Initializes the controls.</summary>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      base.InitializeControls(container);
      this.CSharpCode.Text = this.GenerateCSharpCode(this.Module, this.ModuleType);
      this.VBCode.Text = this.GenerateVBCode(this.Module, this.ModuleType);
      this.CSharpCodeUsings.Text = this.GenerateCSharpCodeUsings(this.ModuleType);
      this.VBCodeImports.Text = this.GenerateVBCodeImports(this.ModuleType);
    }

    /// <summary>
    /// Generates the contextualized code sample in c# syntax for the given module and module type.
    /// </summary>
    /// <param name="module">
    /// The instance of <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModule" /> for which the code sample should be contextualized.
    /// </param>
    /// <param name="moduleType">
    /// The instance of <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleType" /> for which the code sample should be contextualized.
    /// </param>
    /// <returns>The code sample in c# syntax.</returns>
    protected abstract string GenerateCSharpCode(
      DynamicModule module,
      DynamicModuleType moduleType,
      CultureInfo culture = null);

    /// <summary>
    /// Generates the contextualized code sample in VB.NET syntax for the given module and module type.
    /// </summary>
    /// <param name="module">
    /// The instance of <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModule" /> for which the code sample should be contextualized.
    /// </param>
    /// <param name="moduleType">
    /// The instance of <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleType" /> for which the code sample should be contextualized.
    /// </param>
    /// <returns>The code sample in VB.NET syntax.</returns>
    protected abstract string GenerateVBCode(
      DynamicModule module,
      DynamicModuleType moduleType,
      CultureInfo culture = null);

    /// <summary>
    /// Generates the contextualized code sample usings for c# syntax.
    /// </summary>
    /// <returns>The namespaces that must be used.</returns>
    protected virtual string GenerateCSharpCodeUsings(DynamicModuleType moduleType)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.AppendLine("<br/>Note: You must include the following namespaces:\n<br/> using ");
      stringBuilder.Append(string.Join(";\n<br/> using ", this.DefaultNamespaces));
      stringBuilder.Append(";\n<br/><br/>For taxonomies include the following:\n<br/> using ");
      stringBuilder.Append(string.Join(";\n<br/> using ", this.TaxonomiesNamespaces));
      stringBuilder.Append(";\n<br/><br/>For media type include the following:\n<br/> using ");
      stringBuilder.Append(string.Join(";\n<br/> using ", this.MediaNamespaces));
      if (this.RelatedDataNamespaces.Count > 0)
      {
        stringBuilder.Append(";\n<br/><br/>For related data fields include the following:\n<br/> using ");
        stringBuilder.Append(string.Join(";\n<br/> using ", (IEnumerable<string>) this.RelatedDataNamespaces));
      }
      stringBuilder.Append(";\n<br/><br/>For address fields include the following:\n<br/> using ");
      stringBuilder.Append(string.Join(";\n<br/> using ", this.AddressNamespaces));
      if (this.IsMultilingual)
      {
        stringBuilder.Append(";\n<br/><br/>For multilingual include the following:\n<br/> using ");
        stringBuilder.Append(string.Join(";\n<br/> using ", this.MultilingualNamespaces));
      }
      stringBuilder.AppendLine(";");
      return stringBuilder.ToString();
    }

    /// <summary>
    /// Generates the contextualized code sample imports for vb syntax.
    /// </summary>
    /// <returns>The namespaces that must be imported.</returns>
    protected virtual string GenerateVBCodeImports(DynamicModuleType moduleType)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.AppendLine("<br/>Note: You must include the following namespaces:\n<br/> Imports ");
      stringBuilder.AppendLine(string.Join("\n<br/> Imports ", this.DefaultNamespaces));
      stringBuilder.AppendLine("<br/><br/>For taxonomies include the following:\n<br/> Imports ");
      stringBuilder.AppendLine(string.Join("\n<br/> Imports ", this.TaxonomiesNamespaces));
      stringBuilder.AppendLine("<br/><br/>For media type include the following:\n<br/> Imports ");
      stringBuilder.AppendLine(string.Join("\n<br/> Imports ", this.MediaNamespaces));
      if (this.RelatedDataNamespaces.Count > 0)
      {
        stringBuilder.AppendLine("<br/><br/>For related data fields include the following:\n<br/> Imports ");
        stringBuilder.AppendLine(string.Join("\n<br/> Imports ", (IEnumerable<string>) this.RelatedDataNamespaces));
      }
      stringBuilder.AppendLine("<br/><br/>For address fields include the following:\n<br/> Imports ");
      stringBuilder.AppendLine(string.Join("\n<br/> Imports ", this.AddressNamespaces));
      if (this.IsMultilingual)
      {
        stringBuilder.AppendLine("<br/><br/>For multilingual include the following:\n<br/> Imports ");
        stringBuilder.AppendLine(string.Join("\n<br/> Imports ", this.MultilingualNamespaces));
      }
      return stringBuilder.ToString();
    }

    protected virtual string[] DefaultNamespaces => new string[11]
    {
      "Telerik.Sitefinity",
      "Telerik.Sitefinity.Model",
      "Telerik.Sitefinity.DynamicModules",
      "Telerik.Sitefinity.Data.Linq.Dynamic",
      "Telerik.Sitefinity.DynamicModules.Model",
      "Telerik.Sitefinity.GenericContent.Model",
      "Telerik.Sitefinity.Utilities.TypeConverters",
      "Telerik.Sitefinity.Security",
      "Telerik.Sitefinity.Lifecycle",
      "Telerik.Sitefinity.Data",
      "Telerik.Sitefinity.Versioning"
    };
  }
}
