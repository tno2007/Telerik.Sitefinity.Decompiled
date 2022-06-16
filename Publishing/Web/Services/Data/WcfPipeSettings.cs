// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.Web.Services.Data.WcfPipeSettings
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web.Script.Serialization;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Publishing.Model;
using Telerik.Sitefinity.Publishing.Pipes;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Publishing.Web.Services.Data
{
  /// <summary>
  /// Web service view model of a the persistent model pipe setting, used in the REST services and UI
  /// </summary>
  [DataContract(Name = "WcfPipeSettings", Namespace = "Telerik.Sitefinity.Publishing.Web.Services.Data")]
  public class WcfPipeSettings
  {
    private List<string> languageIds;
    private string providerName;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Publishing.Web.Services.Data.WcfPipeSettings" /> class.
    /// </summary>
    public WcfPipeSettings()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Publishing.Web.Services.Data.WcfPipeSettings" /> class.
    /// </summary>
    /// <param name="pipeName">The pipe name</param>
    /// <param name="providerName">The provider name</param>
    public WcfPipeSettings(string pipeName, string providerName)
    {
      PipeSettings defaultSettings = PublishingSystemFactory.GetPipe(pipeName).GetDefaultSettings();
      this.providerName = providerName;
      this.InitializeFromModel(defaultSettings, providerName);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Publishing.Web.Services.Data.WcfPipeSettings" /> class.
    /// </summary>
    /// <param name="initSettings">The init settings.</param>
    /// <param name="providerName">The provider name</param>
    public WcfPipeSettings(PipeSettings initSettings, string providerName)
    {
      this.providerName = providerName;
      this.InitializeFromModel(initSettings, providerName);
    }

    /// <summary>
    /// Gets or sets the provider name the settings were created with
    /// </summary>
    public string ProviderName
    {
      get => this.providerName;
      set => this.providerName = value;
    }

    /// <summary>Gets or sets the scheduled time</summary>
    [DataMember]
    public string ScheduleTime { get; set; }

    /// <summary>Gets or sets the Pipe setting id.</summary>
    /// <value>The id.</value>
    [DataMember]
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the pipe. (RSS,Content,Tweeter)
    /// </summary>
    /// <value>The name of the pipe.</value>
    [DataMember]
    public string PipeName { get; set; }

    /// <summary>Gets or sets the publishing point name</summary>
    [DataMember]
    public string PublishingPointName { get; set; }

    /// <summary>Gets or sets the mapping settings</summary>
    [DataMember]
    public MappingSettingsViewModel MappingSettings { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the pipe is inbound
    /// </summary>
    [DataMember]
    public bool IsInbound { get; set; }

    /// <summary>Gets or sets the pipe definition</summary>
    [DataMember]
    public SimpleDefinitionField[] Definition { get; set; }

    /// <summary>
    /// Gets or sets the specific pipe settings in a serialized string (currently we use JSON data contract serialization).
    /// </summary>
    /// <value>The settings.</value>
    [DataMember]
    public string Settings { get; set; }

    /// <summary>
    /// Gets or sets the specific pipe settings additional data in a serialized string (using the JavaScriptSerializer).
    /// </summary>
    /// <value>The additional settings.</value>
    [DataMember]
    public string AdditionalSettings { get; set; }

    /// <summary>Gets or sets the additional filter.</summary>
    /// <value>The additional filter.</value>
    [DataMember]
    public string AdditionalFilter { get; set; }

    /// <summary>Gets or sets the language filter.</summary>
    /// <value>The language filter.</value>
    [DataMember]
    public List<string> LanguageIds
    {
      get
      {
        if (this.languageIds == null)
          this.languageIds = new List<string>();
        return this.languageIds;
      }
      set => this.languageIds = new List<string>((IEnumerable<string>) value);
    }

    /// <summary>
    /// Gets or sets the pipe title. For example - this can be the RSS feed title
    /// </summary>
    /// <value>The title.</value>
    [DataMember]
    public string Title { get; set; }

    /// <summary>
    /// Gets or sets the pipe description this can be the RSS description.
    /// </summary>
    /// <value>The description.</value>
    [DataMember]
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets the UI name. This is user friendly name of the pipe (RSS/Atom feed)
    /// </summary>
    /// <value>The UI name.</value>
    [DataMember]
    public string UIName { get; set; }

    /// <summary>
    /// Gets or sets the UI description. This is automatically generated description by the pipe
    /// </summary>
    /// <example>Selected Blogs: My blog1, My blog2</example>
    /// <value>The UI description.</value>
    [DataMember]
    public string UIDescription { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this pipe is active.
    /// </summary>
    [DataMember]
    public bool IsActive { get; set; }

    /// <summary>
    /// Gets or sets the content location. This property is relevant only for content pipes.
    /// It shows the location where the imported content is rendered and serves in cases like: RSS back links, search result links etc.
    /// </summary>
    /// <value>The content location.</value>
    [DataMember]
    public string ContentLocation { get; set; }

    /// <summary>
    /// Gets or sets the content URL page ID. This property is relevant only for content pipes.
    /// It keeps the id of the page where the imported content is rendered and serves in cases like: RSS back links, search result links etc.
    /// </summary>
    /// <value>The content URL page ID.</value>
    [DataMember]
    public Guid? ContentLocationPageID { get; set; }

    /// <summary>
    /// Gets or sets the url location of the content. This property is relevant only for content pipes.
    /// It shows the page url location where the imported content is rendered and serves in cases like: RSS back links, search result links etc.
    /// </summary>
    /// <value>The name of the content page location.</value>
    [DataMember]
    public string ContentUrlLocation { get; set; }

    /// <summary>Gets or sets the name of the content.</summary>
    /// <value>The name of the content.</value>
    [DataMember]
    public string ContentName { get; set; }

    /// <summary>
    /// Initializes the wcf view model of pipe settings from an instance of the persistent model of the settings.
    /// </summary>
    /// <param name="initSettings">The init settings.</param>
    /// <param name="providerName">The provider name.</param>
    public void InitializeFromModel(PipeSettings initSettings, string providerName)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      WcfPipeSettings.\u003C\u003Ec__DisplayClass85_0 cDisplayClass850 = new WcfPipeSettings.\u003C\u003Ec__DisplayClass85_0();
      this.providerName = providerName;
      this.PipeName = initSettings.PipeName;
      this.Id = initSettings.Id;
      this.IsActive = initSettings.IsActive;
      using (MemoryStream memoryStream = new MemoryStream())
      {
        new DataContractJsonSerializer(initSettings.GetType()).WriteObject((Stream) memoryStream, (object) initSettings);
        this.Settings = Encoding.Default.GetString(memoryStream.ToArray());
      }
      this.AdditionalSettings = new JavaScriptSerializer().Serialize((object) initSettings.AdditionalData);
      IPipe pipe = PublishingSystemFactory.GetPipe(this.PipeName);
      pipe.PipeSettings = initSettings;
      if (pipe is IDynamicPipe dynamicPipe)
        dynamicPipe.SetPipeName(initSettings.PipeName);
      this.UIDescription = pipe.GetPipeSettingsShortDescription(initSettings);
      this.UIName = initSettings.GetLocalizedUIName();
      this.IsInbound = initSettings.IsInbound;
      if (pipe.Definition != null && ((IEnumerable<IDefinitionField>) pipe.Definition).Any<IDefinitionField>())
        this.Definition = ((IEnumerable<IDefinitionField>) pipe.Definition).Select<IDefinitionField, SimpleDefinitionField>((Func<IDefinitionField, SimpleDefinitionField>) (d => new SimpleDefinitionField(d.Name, d.Title, d.IsRequired))).ToArray<SimpleDefinitionField>();
      if (initSettings.Mappings != null)
        this.MappingSettings = MappingSettingsViewModel.FromModel(initSettings.Mappings);
      // ISSUE: reference to a compiler-generated field
      cDisplayClass850.contentSettings = initSettings as SitefinityContentPipeSettings;
      // ISSUE: reference to a compiler-generated field
      if (cDisplayClass850.contentSettings != null)
      {
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        this.ContentName = cDisplayClass850.contentSettings.ContentTypeName != null ? ContentExtensions.GetTypeUIPluralName(cDisplayClass850.contentSettings.ContentTypeName) : (string) null;
        // ISSUE: reference to a compiler-generated field
        if (cDisplayClass850.contentSettings.BackLinksPageId.HasValue)
        {
          ParameterExpression parameterExpression;
          // ISSUE: reference to a compiler-generated field
          // ISSUE: method reference
          // ISSUE: type reference
          // ISSUE: method reference
          PageNode pageNode = PageManager.GetManager().GetPageNodes().Where<PageNode>(Expression.Lambda<Func<PageNode, bool>>((Expression) Expression.Equal(p.Id, (Expression) Expression.Call(cDisplayClass850.contentSettings.BackLinksPageId, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Guid?.GetValueOrDefault), __typeref (Guid?)), Array.Empty<Expression>()), false, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Guid.op_Equality))), parameterExpression)).FirstOrDefault<PageNode>();
          if (pageNode != null)
          {
            // ISSUE: reference to a compiler-generated field
            this.ContentLocationPageID = cDisplayClass850.contentSettings.BackLinksPageId;
            if (pageNode.Parent != null)
            {
              this.ContentLocation = string.Format(pageNode.GetFullTitlesPath(">"));
              this.ContentUrlLocation = RouteHelper.ResolveUrl(pageNode.GetFullUrl(), UrlResolveOptions.Absolute);
            }
          }
        }
      }
      this.ScheduleTime = (initSettings.ScheduleTime.HasValue ? initSettings.ScheduleTime.Value : DateTime.UtcNow).ToUniversalTime().ToString((IFormatProvider) CultureInfo.InvariantCulture);
    }

    /// <summary>Converts to model settings.</summary>
    /// <returns>The pipe settings</returns>
    public PipeSettings ConvertToModel()
    {
      Type pipeSettingsType = PublishingSystemFactory.GetPipe(this.PipeName).PipeSettingsType;
      PipeSettings model = (PipeSettings) null;
      using (MemoryStream memoryStream = new MemoryStream(Encoding.Unicode.GetBytes(this.Settings)))
      {
        model = new DataContractJsonSerializer(pipeSettingsType).ReadObject((Stream) memoryStream) as PipeSettings;
        model.IsActive = this.IsActive;
        model.IsInbound = this.IsInbound;
        if (!this.ScheduleTime.IsNullOrEmpty())
          model.ScheduleTime = new DateTime?(DateTime.Parse(this.ScheduleTime, (IFormatProvider) CultureInfo.InvariantCulture));
      }
      IDictionary<string, string> dictionary = new JavaScriptSerializer().Deserialize<IDictionary<string, string>>(this.AdditionalSettings);
      IDictionary<string, string> additionalData = model.AdditionalData;
      additionalData.Clear();
      if (dictionary != null)
      {
        foreach (KeyValuePair<string, string> keyValuePair in (IEnumerable<KeyValuePair<string, string>>) dictionary)
          additionalData.Add(keyValuePair.Key, keyValuePair.Value);
      }
      return model;
    }
  }
}
