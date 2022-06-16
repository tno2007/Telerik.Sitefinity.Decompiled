// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.Pipes.Pop3InboundPipe
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model.Publishing.Model.Pop3;
using Telerik.Sitefinity.Publishing.Model;
using Telerik.Sitefinity.Publishing.Pop3;
using Telerik.Sitefinity.Publishing.Web.UI.Designers;
using Telerik.Sitefinity.Utilities.OpenPOP.MIME;
using Telerik.Sitefinity.Utilities.OpenPOP.POP3;

namespace Telerik.Sitefinity.Publishing.Pipes
{
  [PipeDesigner(typeof (Pop3PipeDesignerView), null)]
  public class Pop3InboundPipe : BasePipe<Pop3PipeSettings>, IPushPipe, IInboundPipe
  {
    public const string PipeName = "Pop3InboundPipe";
    private IDefinitionField[] definition;
    private IPublishingPointBusinessObject publishingPoint;

    /// <summary>Gets or sets the POP3 pipe settings.</summary>
    /// <value>The POP3 pipe settings.</value>
    public virtual Pop3PipeSettings Pop3PipeSettings
    {
      get => (Pop3PipeSettings) this.PipeSettings;
      set => this.PipeSettings = (PipeSettings) value;
    }

    /// <summary>Pushes the data.</summary>
    /// <param name="items">The items.</param>
    public virtual void PushData(IList<PublishingSystemEventInfo> items)
    {
      List<WrapperObject> list = items.Select<PublishingSystemEventInfo, WrapperObject>((Func<PublishingSystemEventInfo, WrapperObject>) (i =>
      {
        object theInstance = i.Item;
        if (theInstance is WrapperObject)
          return (WrapperObject) theInstance;
        return new WrapperObject(theInstance)
        {
          MappingSettings = this.PipeSettings.Mappings,
          Language = i.Language
        };
      })).ToList<WrapperObject>();
      this.publishingPoint.RemoveItems((IList<WrapperObject>) list);
      this.publishingPoint.AddItems((IList<WrapperObject>) list);
    }

    /// <summary>Toes the publishing point.</summary>
    public virtual void ToPublishingPoint()
    {
      IEnumerable<Message> messages = this.ReadInboundData();
      List<PublishingSystemEventInfo> items = new List<PublishingSystemEventInfo>();
      string str = this.PipeSettings.LanguageIds.FirstOrDefault<string>();
      foreach (Message messageItem in messages)
        items.Add(new PublishingSystemEventInfo()
        {
          Item = (object) this.ConvertToWraperObject(messageItem),
          ItemAction = "PublisingPointItemImported",
          Language = str
        });
      this.PushData((IList<PublishingSystemEventInfo>) items);
    }

    /// <summary>
    /// Returns the items to be used as inbound data for the current pipe.
    /// </summary>
    /// <returns>A collection of e-mail items.</returns>
    protected virtual IEnumerable<Message> ReadInboundData()
    {
      int num = !string.IsNullOrEmpty(this.Pop3PipeSettings.Pop3Server) ? this.Pop3PipeSettings.Pop3Port : throw new ApplicationException("Missing POP3 server address or port in the pipe settings. Pop3Pipe cannot import mails.");
      if (this.Pop3PipeSettings.Pop3Port > 0)
      {
        try
        {
          return Pop3Manager.RetrieveAndDeleteMails(this.Pop3PipeSettings.IsSslConnection, this.Pop3PipeSettings.Pop3Server, this.Pop3PipeSettings.Pop3Port, this.Pop3PipeSettings.Pop3UserName, this.Pop3PipeSettings.Pop3Password);
        }
        catch (PopServerNotFoundException ex)
        {
          throw new ApplicationException("POP3 server not found. Pop3Pipe cannot import mails.");
        }
        catch (PopServerNotAvailableException ex)
        {
          throw new ApplicationException("POP3 server not available. Pop3Pipe cannot import mails.");
        }
        catch (InvalidLoginException ex)
        {
          throw new ApplicationException("POP3 server have not accepted user login. Pop3Pipe cannot import mails.");
        }
        catch (PopServerLockException ex)
        {
          throw new ApplicationException("POP3 server is locked. Pop3Pipe cannot import mails.");
        }
        catch (InvalidPasswordException ex)
        {
          throw new ApplicationException("POP3 server responded wrong user/password pair. Pop3Pipe cannot import mails.");
        }
      }
    }

    /// <summary>
    /// Converts the object currently imported into a dictionary of property values, suitable for mapping. The keys of the dictionary are the mapping source field names
    /// The default implementation retrieves the necessary property values(according to the Mapping) using TypeDescriptor
    /// Specific Pipe implementations should do their own conversion to dictionary depending on the format of the data they receive
    /// which might be xml or some hierarchical object and not be suitable to query directly with TypeDescriptor
    /// </summary>
    /// <param name="item">The item from which the data is extracted.</param>
    /// <returns>A wrapper object containing extracted values.</returns>
    protected WrapperObject ConvertToWraperObject(Message messageItem)
    {
      WrapperObject wraperObject = new WrapperObject((object) null);
      wraperObject.MappingSettings = this.PipeSettings.Mappings;
      wraperObject.Language = this.PipeSettings.LanguageIds.FirstOrDefault<string>();
      string str1 = messageItem.Headers.From.Address;
      if (string.IsNullOrWhiteSpace(str1))
        str1 = string.Empty;
      wraperObject.AddProperty("OwnerEmail", (object) str1);
      string displayName = messageItem.Headers.From.DisplayName;
      string empty1 = string.Empty;
      string empty2 = string.Empty;
      if (displayName != null)
      {
        char[] separator = new char[3]{ ' ', ',', ';' };
        string[] strArray = displayName.Split(separator, StringSplitOptions.RemoveEmptyEntries);
        if (strArray.Length != 0)
        {
          if (strArray.Length == 1)
            empty1 = strArray[0];
          if (strArray.Length > 1)
          {
            empty2 = strArray[0];
            StringBuilder stringBuilder = new StringBuilder();
            for (int index = 1; index <= strArray.Length - 1 - 1; ++index)
            {
              stringBuilder.Append(strArray[index]);
              stringBuilder.Append(" ");
            }
            stringBuilder.Append(strArray[strArray.Length - 1]);
            empty1 = stringBuilder.ToString();
          }
        }
      }
      if (string.IsNullOrWhiteSpace(empty1))
        empty1 = string.Empty;
      wraperObject.AddProperty("OwnerLastName", (object) empty1);
      if (string.IsNullOrWhiteSpace(empty2))
        empty2 = string.Empty;
      wraperObject.AddProperty("OwnerFirstName", (object) empty2);
      string str2 = messageItem.Headers.Subject ?? string.Empty;
      wraperObject.AddProperty("Title", (object) str2);
      string date = messageItem.Headers.Date;
      string str3;
      if (string.IsNullOrWhiteSpace(date))
      {
        str3 = DateTime.UtcNow.ToString();
      }
      else
      {
        try
        {
          DateTime universalTime = DateTime.Parse(date);
          universalTime = universalTime.ToUniversalTime();
          str3 = universalTime.ToString();
        }
        catch (FormatException ex)
        {
          str3 = DateTime.UtcNow.ToString();
        }
        catch (ArgumentNullException ex)
        {
          str3 = DateTime.UtcNow.ToString();
        }
      }
      wraperObject.AddProperty("PublicationDate", (object) str3);
      string str4 = string.Empty;
      if (messageItem.MessageBody.Count > 0)
        str4 = messageItem.MessageBody[messageItem.MessageBody.Count - 1] ?? string.Empty;
      wraperObject.AddProperty("Content", (object) str4);
      return wraperObject;
    }

    /// <summary>
    /// Used to get the default settings for this pipe, without creating persistent object
    /// </summary>
    /// <returns>The settings</returns>
    public static Pop3PipeSettings GetTemplatePipeSettings()
    {
      Pop3PipeSettings templatePipeSettings = new Pop3PipeSettings();
      templatePipeSettings.IsSslConnection = false;
      templatePipeSettings.Pop3Port = 110;
      templatePipeSettings.PipeName = nameof (Pop3InboundPipe);
      templatePipeSettings.IsActive = true;
      templatePipeSettings.IsInbound = true;
      templatePipeSettings.InvocationMode = PipeInvokationMode.Pull;
      return templatePipeSettings;
    }

    /// <summary>Used to get the default mappings for this pipe</summary>
    /// <returns>The settings</returns>
    public static List<Mapping> GetDefaultMappings() => new List<Mapping>()
    {
      PublishingSystemFactory.CreateMapping("Title", "concatenationtranslator", true, "Title"),
      PublishingSystemFactory.CreateMapping("PublicationDate", "TransparentTranslator", false, "PublicationDate"),
      PublishingSystemFactory.CreateMapping("Content", "concatenationtranslator", true, "Content"),
      PublishingSystemFactory.CreateMapping("OwnerFirstName", "TransparentTranslator", false, "OwnerFirstName"),
      PublishingSystemFactory.CreateMapping("OwnerLastName", "TransparentTranslator", false, "OwnerLastName"),
      PublishingSystemFactory.CreateMapping("OwnerEmail", "TransparentTranslator", false, "OwnerEmail")
    };

    /// <summary>
    /// Defines the data structure of the medium this pipe works with
    /// </summary>
    /// <value></value>
    public override IDefinitionField[] Definition
    {
      get
      {
        if (this.definition == null)
          this.definition = PublishingSystemFactory.GetPipeDefinitions(this.Name);
        return this.definition;
      }
    }

    /// <summary>Initializes the specified pipe settings.</summary>
    /// <param name="pipeSettings">The pipe settings.</param>
    public override void Initialize(PipeSettings pipeSettings)
    {
      this.PipeSettingsInternal = (Pop3PipeSettings) pipeSettings;
      this.publishingPoint = PublishingSystemFactory.GetPublishingPoint(this.PipeSettingsInternal.PublishingPoint);
    }

    /// <summary>
    /// Determines whether this instance [can process item] the specified item.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <returns>
    /// 	<c>true</c> if this instance [can process item] the specified item; otherwise, <c>false</c>.
    /// </returns>
    public override bool CanProcessItem(object item) => true;

    /// <summary>Gets the name.</summary>
    /// <value>The name.</value>
    public override string Name => nameof (Pop3InboundPipe);

    /// <summary>Gets the pipe settings short description.</summary>
    /// <param name="initSettings">The init settings.</param>
    /// <returns></returns>
    public override string GetPipeSettingsShortDescription(PipeSettings initSettings)
    {
      Pop3PipeSettings settingsInternal = this.PipeSettingsInternal;
      return string.Format(Res.Get<PublishingMessages>().Pop3PipeSettingsImportShortDescription, (object) settingsInternal.Pop3Server, (object) settingsInternal.Pop3UserName);
    }
  }
}
