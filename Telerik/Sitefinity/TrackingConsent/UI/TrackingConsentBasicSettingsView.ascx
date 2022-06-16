<%@ Control Language="C#" %>
<%@ Register Assembly="Telerik.Sitefinity" Namespace="Telerik.Sitefinity.Web.UI" TagPrefix="sf" %>
<%@ Register Assembly="Telerik.Sitefinity" Namespace="Telerik.Sitefinity.TrackingConsent.UI" TagPrefix="sfTracking" %>

<sf:ResourceLinks runat="server" UseEmbeddedThemes="true" UseBackendTheme="True">
    <sf:ResourceFile Name="Styles/Grid.css" />
</sf:ResourceLinks>

<div ng-app="trackingConsent">
    <div ng-controller="trackingConsentPage">

        <div ng-cloak ng-show="error" class="sfMessage sfDialogMessage sfFailure">
            Error: {{ error }}
        </div>

        <h2>
            <asp:Literal runat="server" ID="lGeneralSettings" Text="<%$Resources:Labels, TrackingConsentSettings %>" />
        </h2>

        <div ng-hide="loading">
            <sfTracking:TrackingConsentDetailsWindow runat="server" ID="consentDetailsDialog" />

            <sf:ClientLabelManager id="clientLabelManager" runat="server">
                <labels>
                    <sf:ClientLabel ClassId="Labels" Key="Yes" runat="server" />
                    <sf:ClientLabel ClassId="Labels" Key="No" runat="server" />
                    <sf:ClientLabel ClassId="Labels" Key="TrackingConsentViewEmptyError" runat="server" />
                    <sf:ClientLabel ClassId="Labels" Key="TrackingConsentViewUniqueError" runat="server" />
                    <sf:ClientLabel ClassId="Labels" Key="TrackingConsentViewSpecialCharactersError" runat="server" />
                    <sf:ClientLabel ClassId="Labels" Key="TrackingConsentViewDotAtTheEndError" runat="server" />
                    <sf:ClientLabel ClassId="Labels" Key="TrackingConsentDefaultDomainDisplay" runat="server" />
                </labels>
            </sf:ClientLabelManager>

            <sf:PromptDialog ID="domainConfirmationDialog"
                runat="server"
                Width="350"
                Height="300"
                Mode="Confirm"
                AllowCloseButton="true"
                InputRows="5"
                ShowOnLoad="false"
                Message="<%$ Resources:Labels, TrackingConsentDomainDeleteConfirmation %>">
                <commands>
                    <sf:CommandToolboxItem Text="<%$ Resources:Labels, Remove %>" CommandName="delete" CommandType="NormalButton" WrapperTagName="LI" CssClass="sfDelete" />
                    <sf:CommandToolboxItem Text="<%$ Resources:Labels, Cancel %>" CommandName="cancel" CommandType="CancelButton" WrapperTagName="LI" />
                </commands>
            </sf:PromptDialog>

            <div class="sfHelpOnRight sfMTop10">
                <div class="sfFormFAQ">
                    <h3><asp:Literal runat="server" Text="<%$Resources:Labels, TrackingConsentViewFaqTitle %>" /></h3>
                    <dl>
                        <dt><asp:Literal runat="server" Text="<%$Resources:Labels, TrackingConsentViewFaqQuestion1 %>" /></dt>
                        <dd><asp:Literal runat="server" Text="<%$Resources:Labels, TrackingConsentViewFaqAnswer1 %>" /></dd>
                    </dl>
                    <dl>
                        <dt><asp:Literal runat="server" Text="<%$Resources:Labels, TrackingConsentViewFaqQuestion2 %>" /></dt>
                        <dd><asp:Literal runat="server" Text="<%$Resources:Labels, TrackingConsentViewFaqAnswer2 %>" /></dd>
                    </dl>
                </div>
            </div>

            <div class="sfMBottom20 k-grid k-widget k-display-block sfW565">
                <table id="domainInfo">
                        <thead class="k-grid-header">
                            <tr role="row">
                                <th class="sfMedium k-header"><asp:Literal runat="server" Text="<%$Resources:Labels, TrackingConsentViewDomainTitle %>"/></th>
                                <th class="sfMedium k-header"><asp:Literal runat="server" Text="<%$Resources:Labels, TrackingConsentViewConsentTitle %>"/></th>
                                <th class="sfEditCol k-header"></th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr ng-repeat="consent in trackingConsents">
                                <td class="sfMedium" ng-click="showDetails(consent)">
                                    <span class="sfLnk" ng-bind="getConsentLabel(consent)"></span>
                                </td>
                                <td class="sfMedium">
                                    <span ng-bind="boolToText(consent.ConsentIsRequired)"></span>
                                </td>
                                <td class="sfEditCol sfAlignRight">
                                    <span ng-click="askForDeletion(consent)" ng-class="consent.IsMaster ? 'sfDeleteProfileOff' : 'sfDeleteProfileOn'"><i class="fa fa-trash-o"></i></span>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>

            <a id="addDomain" class="sfLinkBtn" ng-click="createNewDomain()">
                <span class="sfLinkBtnIn"><asp:Literal runat="server" Text="<%$Resources:Labels, TrackingConsentViewAddNew %>" /></span>
            </a>
        </div>

        <div ng-show="loading" class="sfSimpleLoadingWrp">
            View is loading...
        </div>

    </div>
</div>
