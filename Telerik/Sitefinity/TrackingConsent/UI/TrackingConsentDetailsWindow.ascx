<%@ Control Language="C#" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="Telerik.Sitefinity" Namespace="Telerik.Sitefinity.Web.UI" TagPrefix="sf" %>

<sf:ResourceLinks ID="resourcesLinks1" runat="server">
    <sf:ResourceFile Name="Styles/Window.css" />
    <sf:ResourceFile Name="Styles/MaxWindow.css" />
    <sf:ResourceFile Name="Styles/FileExplorer.css" />
    <sf:ResourceFile Name="Styles/Treeview.css" />
</sf:ResourceLinks>

<sf:ResourceLinks runat="server" UseEmbeddedThemes="true" UseBackendTheme="True">
    <sf:ResourceFile Name="Telerik.Sitefinity.Resources.Scripts.Kendo.styles.kendo_common_min.css" Static="true" />
    <sf:ResourceFile Name="Telerik.Sitefinity.Resources.Scripts.Kendo.styles.kendo_default_min.css" Static="true" />
</sf:ResourceLinks>

<div runat="server" id="windowBody" style="display: none;" class="sfSelectorDialog">
    <h1 id="addConsentTitle"><asp:Literal runat="server" Text="<%$Resources:Labels, TrackingConsentViewAddTitle %>" /></h1>
    <h1 id="editConsentTitle"><asp:Literal runat="server" Text="<%$Resources:Labels, TrackingConsentViewEditTitle %>" /></h1>

    <div class="sfBasicDim">
        <div class="sfContentViews">
            <div class="sfFormCtrl">
                <label for="nameInput" class="sfTxtLbl"> <asp:Literal runat="server" Text="<%$Resources:Labels, TrackingConsentViewDomainDescription %>" /></label>
                <div id="domainInputContainer">
                    <input runat="server" class="sfTxt" id="nameInput" type="text" data-bind="value: Domain"/>
                    <span class="sfError" id="errorMessage"></span>
                    <p class="sfExample"><asp:Literal runat="server" Text="<%$Resources:Labels, TrackingConsentViewDomainExample %>" /></p>
                </div>
                <div id="domainLabelContainer">
                    <asp:Literal runat="server" Text="<%$Resources:Labels, TrackingConsentDefaultDomainDisplay %>" />
                </div>
            </div>

            <div class="sfFormCtrl">
                <strong><asp:Literal runat="server" Text="<%$Resources:Labels, TrackingConsentViewConsentDescription %>" /></strong>
                <p class="sfCheckBox">
                    <input runat="server" id="consentIsRequiredCheckbox" data-bind="checked: ConsentIsRequired" type="checkbox" />
                    <asp:Label runat="server" AssociatedControlID="consentIsRequiredCheckbox" Text="<%$Resources:Labels, TrackingConsent %>"></asp:Label>
                </p>
            </div>
            <div class="sfFormCtrl" data-bind="visible: ConsentIsRequired">
                <label class="sfTxtLbl"><asp:Literal runat="server" Text="<%$Resources:Labels, TrackingConsentViewTemplateDescription %>" /></label>
                <span class="sfSelectedItem" data-bind="text: ConsentDialog"></span>
                <a runat="server" id="changeButton" class="sfLinkBtn"><span class="sfLinkBtnIn"><asp:Literal runat="server" Text="<%$Resources:Labels, TrackingConsentViewTemplateChange %>" /></span></a>
                <span class="sfError" id="fileError"><asp:Literal runat="server" Text="<%$Resources:Labels, TrackingConsentViewFileError %>" /></span>
            </div>

            <div class="sfButtonArea sfSelectorBtns">
                <a runat="server" id="doneButton" class="sfLinkBtn sfSave">
                    <span class="sfLinkBtnIn">
                        <asp:Literal runat="server" Text="<%$Resources:Labels, Done %>" />
                    </span>
                </a>&nbsp;
                <asp:Literal runat="server" Text="<%$Resources:Labels, or %>" />&nbsp;
                <a runat="server" id="cancelButton" class="sfCancel"><asp:Literal runat="server" Text="<%$Resources:Labels, Cancel %>" /></a>
            </div>
        </div>
    </div>

    <div id="templateSelector" style="display: none;" class="sfSelectorDialog">
        <h1><asp:Literal runat="server" Text="<%$Resources:Labels, TrackingConsentViewSelectTemplate %>" /></h1>
    
        <div class="sfFilesTree sfTrackingConsentTree"><telerik:RadFileExplorer runat="server" ID="fileExplorer" Width="100%" Height="300" Skin="Default"></telerik:RadFileExplorer></div>
        <p class="sfExample"><asp:Literal runat="server" Text="<%$Resources:Labels, TrackingConsentViewFileDescription %>" /></p>

        <div class="sfBasicDim">
            <div class="sfContentViews">
                <div class="sfButtonArea sfSelectorBtns">
                    <a id="templateSelectorDone" class="sfLinkBtn sfSave">
                        <span class="sfLinkBtnIn"><asp:Literal runat="server" Text="<%$Resources:Labels, Done %>" /></span>
                    </a>&nbsp;
                    <asp:Literal runat="server" Text="<%$Resources:Labels, or %>" />&nbsp;
                    <a id="templateSelectorCancel" class="sfCancel"><asp:Literal runat="server" Text="<%$Resources:Labels, Cancel %>" /></a>
                </div>
            </div>
        </div>
    </div>

</div>