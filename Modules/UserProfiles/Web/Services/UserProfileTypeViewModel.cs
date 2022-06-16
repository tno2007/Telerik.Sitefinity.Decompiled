// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.UserProfiles.Web.Services.UserProfileTypeViewModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Modules.UserProfiles.Web.Services.Model;

namespace Telerik.Sitefinity.Modules.UserProfiles.Web.Services
{
  [BlankItemDelegate(typeof (UserProfileTypeViewModel), "GetBlankItem")]
  [DataContract]
  public class UserProfileTypeViewModel
  {
    internal UserProfileTypeViewModel()
    {
    }

    public UserProfileTypeViewModel(MetaTypeDescription profileTypeDescription, MetaType metaType)
    {
      this.Id = profileTypeDescription.Id;
      this.Title = profileTypeDescription.UserFriendlyName;
      this.Name = metaType.ClassName;
      this.DynamicTypeName = metaType.FullTypeName;
    }

    /// <summary>
    /// Returns a list of <see cref="T:Telerik.Sitefinity.Modules.UserProfiles.Web.Services.Model.ProviderViewModel" /> objects from a string array of provider names.
    /// </summary>
    /// <param name="membershipProviderNames">The membership provider names.</param>
    /// <returns></returns>
    public static List<ProviderViewModel> GetMembershipProviders(
      string[] membershipProviderNames)
    {
      List<ProviderViewModel> membershipProviders = new List<ProviderViewModel>();
      foreach (string membershipProviderName in membershipProviderNames)
        membershipProviders.Add(new ProviderViewModel(membershipProviderName, membershipProviderName));
      return membershipProviders;
    }

    /// <summary>
    /// Returns a blank UserProfileTypeViewModel object. This object is bound
    /// when a new profile is created.
    /// </summary>
    /// <param name="contentType">Type of the content.</param>
    /// <param name="providerName">Name of the provider (it is not meaningful here).</param>
    /// <returns></returns>
    public static object GetBlankItem(Type contentType, string providerName) => (object) new UserProfileTypeViewModel()
    {
      MembershipProvidersUsage = MembershipProvidersUsage.AllProviders
    };

    [DataMember]
    public Guid Id { get; set; }

    [DataMember]
    public string Title { get; set; }

    /// <summary>Gets or sets the name of the profile provider to use.</summary>
    /// <value>The name of the profile provider to use.</value>
    [DataMember]
    public string ProfileProviderName { get; set; }

    /// <summary>
    /// Gets or sets the name of the profile. This is what the user enters as Name for developers.
    /// </summary>
    /// <value>The name.</value>
    [DataMember]
    public string Name { get; set; }

    /// <summary>Gets or sets the full name of the dynamic type.</summary>
    /// <value>The full name of the dynamic type.</value>
    [DataMember]
    public string DynamicTypeName { get; set; }

    /// <summary>
    /// Gets the names of the membership providers that this profile type applies to.
    /// </summary>
    /// <value>The names of the membership providers that this profile type applies to.</value>
    [DataMember]
    public string AppliedTo
    {
      get
      {
        string appliedTo = "";
        if (this.MembershipProvidersUsage == MembershipProvidersUsage.AllProviders)
          appliedTo = Res.Get<UserProfilesResources>().AllUserProviders;
        else if (this.SelectedMembershipProviders != null)
        {
          foreach (ProviderViewModel membershipProvider in this.SelectedMembershipProviders)
          {
            if (appliedTo.Length > 0)
              appliedTo += ", ";
            appliedTo += membershipProvider.ProviderFriendlyName;
          }
        }
        return appliedTo;
      }
      set
      {
      }
    }

    /// <summary>
    /// Gets or sets whether this profile type can be used by all membership providers,
    /// or just by some specified providers.
    /// </summary>
    /// <value>The membership providers that can be used with this profile type.</value>
    [DataMember]
    public MembershipProvidersUsage MembershipProvidersUsage { get; set; }

    /// <summary>
    /// Gets or sets the membership providers that can be used with this profile type.
    /// This property is only used if MembershipProviderUsage is set to SpecifiedProviders.
    /// </summary>
    /// <value>The membership providers that can be used with this profile type.</value>
    [DataMember]
    public ProviderViewModel[] SelectedMembershipProviders { get; set; }
  }
}
