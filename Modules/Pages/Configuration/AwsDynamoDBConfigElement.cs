// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Configuration.AwsDynamoDBConfigElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Modules.Pages.Configuration
{
  /// <summary>Represents AwsDynamoDB configuration element.</summary>
  [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed.")]
  public class AwsDynamoDBConfigElement : ConfigElement
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Pages.Configuration.AwsDynamoDBConfigElement" /> class.
    /// </summary>
    /// <param name="parent">Parent configuration element.</param>
    public AwsDynamoDBConfigElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Pages.Configuration.AwsDynamoDBConfigElement" /> class.
    /// </summary>
    internal AwsDynamoDBConfigElement()
      : base(false)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Pages.Configuration.AwsDynamoDBConfigElement" /> class.
    /// </summary>
    /// <param name="check">if set to <c>true</c> [check].</param>
    internal AwsDynamoDBConfigElement(bool check)
      : base(check)
    {
    }

    /// <summary>Gets or sets the value.</summary>
    /// <value>The value.</value>
    [ConfigurationProperty("AccessKey")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = null, Title = "AwsDynamoDBAccessKeyTitle")]
    public string AccessKey
    {
      get => (string) this[nameof (AccessKey)];
      set => this[nameof (AccessKey)] = (object) value;
    }

    /// <summary>Gets or sets the value.</summary>
    /// <value>The value.</value>
    [ConfigurationProperty("SecretKey")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = null, Title = "AwsDynamoDBSecretKeyTitle")]
    public string SecretKey
    {
      get => (string) this[nameof (SecretKey)];
      set => this[nameof (SecretKey)] = (object) value;
    }

    /// <summary>Gets or sets the value.</summary>
    /// <value>The value.</value>
    [ConfigurationProperty("Region")]
    public string Region
    {
      get => (string) this[nameof (Region)];
      set => this[nameof (Region)] = (object) value;
    }
  }
}
