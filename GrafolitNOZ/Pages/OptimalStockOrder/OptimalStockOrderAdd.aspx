<%@ Page Title="Kreiraj naročilo" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="OptimalStockOrderAdd.aspx.cs" Inherits="GrafolitNOZ.Pages.OptimalStockOrder.OptimalStockOrderAdd" %>

<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v19.2, Version=19.2.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dx" %>

<%@ MasterType VirtualPath="~/Main.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContentHolder" runat="server">
    <script type="text/javascript" src="../../Scripts/jquery.smartWizard.js"></script>
    <script type="text/javascript">
        var isRequestInitiated = false;
        var btnRegreshTreeListVisible = false;

        function CheckFieldValidation() {
            var process = false;
            var inputItems = null;
            var dateEditItems = null;
            var lookupItmes = null;

            process = InputFieldsValidation(lookupItmes, inputItems, dateEditItems, null, null, null);

            return true;
        }

        function HandleUserAction(s, e) {
            var process = CheckFieldValidation(s, e);

            if (process && !isRequestInitiated) {
                LoadingPanel.Show();
                isRequestInitiated = true;
                e.processOnServer = true;
            }
            else
                e.processOnServer = false;
        }

        function CallbackPanelWizard_EndCallback(s, e) {
            LoadingPanel.Hide();

            if (CallbackPanelTreeHierarchyWithProducts.cpShowSubmitOrderButton != null && CallbackPanelTreeHierarchyWithProducts.cpShowSubmitOrderButton !== undefined) {

                $('.sw-btn-next').removeClass('d-none');
                $('.btn-primary').removeClass('d-none');
                delete (CallbackPanelTreeHierarchyWithProducts.cpShowSubmitOrderButton);
            }
            else if (CallbackPanelTreeHierarchyWithProducts.cpErrorOpenNewCodeForProduct != null && CallbackPanelTreeHierarchyWithProducts.cpErrorOpenNewCodeForProduct !== undefined) {
                ShowModal("Napaka pri izbiri dobavitelja in podskupin", CallbackPanelTreeHierarchyWithProducts.cpErrorOpenNewCodeForProduct);
                $('.btn-primary').addClass('d-none');
                $('.sw-btn-next').addClass('d-none');
                btnRefreshTreeList.SetVisible(true);
                delete (CallbackPanelTreeHierarchyWithProducts.cpErrorOpenNewCodeForProduct);
            }

            if (btnRegreshTreeListVisible) {
                btnRefreshTreeList.SetVisible(btnRegreshTreeListVisible);
                btnRegreshTreeListVisible = false;
            }
        }

        function lookUpCategory_Valuechanged(s, e) {
            LoadingPanel.Show();
            CallbackPanelTreeHierarchy.PerformCallback('CategoryChanged');
        }

        function lookUpColor_Valuechanged(s, e) {
            LoadingPanel.Show();
            CallbackPanelTreeHierarchy.PerformCallback('ColorChanged');
        }

        function CloseGridLookup(s, e) {
            lookUpCategory.ConfirmCurrentSelection();
            lookUpCategory.HideDropDown();
            lookUpCategory.Focus();
        }

        function lookUpSupplier_ValueChanged(s, e) {
            CallbackPanelTreeHierarchyWithProducts.PerformCallback("FilterProductsBySupplier");
        }


        function treeListWithProducts_NodeDoubleClick(s, e) {
            btnRegreshTreeListVisible = btnRefreshTreeList.GetVisible();
            CallbackPanelTreeHierarchyWithProducts.PerformCallback("PopupChildProducts;" + e.nodeKey);
        }

        function OnClosePopUpHandler(command, sender) {
            switch (command) {
                case 'Potrdi':
                    switch (sender) {
                        case 'MainProductNodeClick':
                            PopupControlMainProductProductsClick.Hide();
                            CallbackPanelTreeHierarchyWithProducts.PerformCallback("AddChildProductToList")
                            break;
                    }
                    break;
                case 'Preklici':
                    switch (sender) {
                        case 'MainProductNodeClick':
                            PopupControlMainProductProductsClick.Hide();
                    }
                    break;
            }
        }

        function RadioButtonList_ValueChanged(s, e) {
            LoadingPanel.Show();
            CallbackPanelTreeHierarchy.PerformCallback(s.GetValue());
        }

        function btnRefreshTreeList_Click(s, e) {

            btnRefreshTreeList.SetVisible(false);
            LoadingPanel.Show();
            CallbackPanelTreeHierarchyWithProducts.PerformCallback("FilterProductsBySupplier_RefreshTreeList");
        }

        function ShowModal(title, message) {
            $("#ModalTitle").empty();
            $("#ModalTitle").append(title);

            $("#ModalMessage").empty();
            $("#ModalMessage").append(message);

            $("#ConfirmModal").modal('show');
        }

        function HideModal() {
            $("#ConfirmModal").modal('hide');
        }

        function btnSearch_Click(s, e) {
            //var process = CheckFieldValidation();
            var process = true;

            if (process) {
                CallbackPanelTreeHierarchyWithProducts.PerformCallback("StartSearchPopup");
            }
            else
                e.processOnServer = false;
        }
    </script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="FooterScriptHolder" runat="server">
    <script type="text/javascript">

        $('#stepwizard').smartWizard({
            theme: 'arrows',
            transitionEffect: 'fade',
            transitionSpeed: '400',
            keyNavigation: false,
            lang: { next: 'Naprej', previous: 'Nazaj' },
            toolbarSettings: {


                showNextButton: true,
                showPreviousButton: true,
                toolbarExtraButtons: [
                    $('<button></button>').text('Oddaj naročilo').addClass('btn btn-success d-none').on('click', function () {
                        btnConfirm.DoClick();
                    }),
                    $('<button></button>').text('Prekliči').addClass('btn btn-danger').on('click', function () {
                        btnCancel.DoClick();
                    }),
                ]
            }
        });

        $("#stepwizard").on("showStep", function (e, anchorObject, stepNumber, stepDirection) {
            if (stepNumber == 0) {
                $('.btn-success').addClass('d-none');
            }
        });

        $("#stepwizard").on("leaveStep", function (e, anchorObject, stepNumber, stepDirection) {
            var process = false;
            //alert(stepNumber);

            if (stepDirection === 'forward' && stepNumber >= 1) {
                $('.btn-success').removeClass('d-none');
            }
            else
                $('.btn-success').addClass('d-none');

            if (stepDirection === 'backward' && stepNumber == 2)
                $('.btn-success').removeClass('d-none');

            if (stepDirection === 'forward' && stepNumber == 0 && lookUpSupplier.GetText() !== "") {
                $('.btn-success').removeClass('d-none');
            }

            //samo ko se pomikamo naprej preverimo validacijo kontrol
            if (stepDirection === 'forward') {

                switch (stepNumber) {
                    case 0://iz vnosa izbire optimalnih količin na izbiro artiklov glede na izbrane optimalne količine
                        process = InputFieldsValidation([lookUpCategory, lookUpColor], null, null, null, null, null);

                        if (process) {
                            LoadingPanel.Show();
                            CallbackPanelTreeHierarchyWithProducts.PerformCallback("GetProductForSelectedOptimalStock");
                        }

                        return process;
                        break;
                    case 1://iz izbranih artiklov na seznam vseh artiklov katerim dodelimo količino in opis
                        process = InputFieldsValidation([lookUpSupplier], null, null, null, null, null);;

                        if (process) {
                            LoadingPanel.Show();
                            CallbackPanelProducts.PerformCallback("BindSelectedProducts");
                        }
                        else
                            $('.btn-primary').addClass('d-none');

                        return process;
                        break;
                    case 2://iz prikaz artiklov na prikaz dodatnih podatkov za oddajo naročilnice
                        //process = InputFieldsValidation(null, [txtUsername, txtPassword], null, null, [ComboBoxRoles], null);
                        gridProducts.Refresh();
                        if (gridProducts.cpQuantitySum != null && gridProducts.cpQuantitySum !== undefined) {

                            var value = parseFloat(gridProducts.cpQuantitySum);
                            delete (gridProducts.cpQuantitySum);

                            if (value > 0)
                                process = true;
                            else
                                process = false;
                        }
                        else
                            process = false;

                        if (process) {
                            LoadingPanel.Show();
                            CallbackPanelOptimalStockOrder.PerformCallback("OpenNewOptimalStockOrder");
                        }
                        else
                            $('.btn-primary').addClass('d-none');

                        return process;
                        break;
                    case 3://iz prikaz ewonov na prikaz dodatnih podatkov korak
                        break;
                }
            }
            return true;
        });

    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <div id="stepwizard">
        <ul>
            <li class="w-25"><a href="#step1">Optimalna zaloga<br />
                <small>Izberi optimalno zalogo za naročilo</small></a></li>
            <li class="w-25"><a href="#step2">Artikli<br />
                <small>Izberi artikle za kreiranje naročila</small></a></li>
            <li class="w-25"><a href="#step3">Izbrani artikli<br />
                <small>Pregled artiklov in dobavitelja</small></a></li>
        </ul>
        <div class="mt-3">
            <div id="step1">
                <dx:ASPxCallbackPanel ID="CallbackPanelTreeHierarchy" runat="server" ClientInstanceName="CallbackPanelTreeHierarchy" OnCallback="CallbackPanelTreeHierarchy_Callback">
                    <SettingsLoadingPanel Enabled="false" />
                    <ClientSideEvents EndCallback="CallbackPanelWizard_EndCallback" />
                    <PanelCollection>
                        <dx:PanelContent>
                            <div class="card">
                                <div class="card-body">

                                    <div class="row m-0 pb-3">
                                        <div class="col-lg-6 mb-2 mb-lg-0">
                                            <div class="row m-0 align-items-center">
                                                <div class="col-0 p-0" style="margin-right: 66px;">
                                                    <dx:ASPxLabel ID="ASPxLabel19" runat="server" Font-Size="12px" Text="KATEGORIJA : " Font-Bold="true"></dx:ASPxLabel>
                                                </div>
                                                <div class="col p-0">
                                                    <dx:ASPxGridLookup ID="GridLookupCategory" runat="server" ClientInstanceName="lookUpCategory"
                                                        KeyFieldName="TempID" TextFormatString="{0}" CssClass="text-box-input"
                                                        Paddings-PaddingTop="0" Paddings-PaddingBottom="0" Width="100%" Font-Size="13px"
                                                        OnLoad="ASPxGridLookupLoad_WidthSmall" OnDataBinding="GridLookupCategory_DataBinding" IncrementalFilteringMode="Contains"
                                                        SelectionMode="Multiple" MultiTextSeparator=", ">
                                                        <ClientSideEvents ValueChanged="lookUpCategory_Valuechanged" />
                                                        <ClearButton DisplayMode="OnHover" />
                                                        <FocusedStyle CssClass="focus-text-box-input"></FocusedStyle>
                                                        <GridViewStyles>
                                                            <Header CssClass="gridview-no-header-padding" ForeColor="Black"></Header>
                                                        </GridViewStyles>
                                                        <GridViewProperties>
                                                            <SettingsBehavior EnableRowHotTrack="True" AllowEllipsisInText="true" AllowDragDrop="false" />
                                                            <SettingsPager ShowSeparators="True" NumericButtonCount="3" EnableAdaptivity="true" />
                                                            <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowVerticalScrollBar="True"
                                                                ShowHorizontalScrollBar="true" VerticalScrollableHeight="200" ShowStatusBar="Visible"></Settings>
                                                        </GridViewProperties>
                                                        <SettingsAdaptivity Mode="OnWindowInnerWidth" ModalDropDownCaption="Kategorija" SwitchToModalAtWindowInnerWidth="650" />
                                                        <Columns>
                                                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" />
                                                            <dx:GridViewDataTextColumn Caption="Naziv" FieldName="Naziv"
                                                                ReadOnly="true" MinWidth="230" MaxWidth="400" Width="100%">
                                                            </dx:GridViewDataTextColumn>
                                                        </Columns>
                                                        <GridViewProperties>
                                                            <Templates>
                                                                <StatusBar>
                                                                    <table class="OptionsTable" style="float: right">
                                                                        <tr>
                                                                            <td>
                                                                                <dx:ASPxButton ID="btnConfirmAndClose" runat="server" AutoPostBack="false" Text="Potrdi in zapri" ClientSideEvents-Click="CloseGridLookup" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </StatusBar>
                                                            </Templates>
                                                            <Settings ShowFilterRow="True" ShowStatusBar="Visible" />
                                                            <SettingsPager PageSize="7" EnableAdaptivity="true" />
                                                        </GridViewProperties>
                                                    </dx:ASPxGridLookup>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-lg-6 mb-2 mb-lg-0">
                                            <div class="row m-0 align-items-center justify-content-end">
                                                <div class="col-0 p-0 mr-3">
                                                    <dx:ASPxLabel ID="ASPxLabel2" runat="server" Font-Size="12px" Text="BARVA : " Font-Bold="true"></dx:ASPxLabel>
                                                </div>
                                                <div class="col p-0">
                                                    <dx:ASPxGridLookup ID="GridLookupColor" runat="server" ClientInstanceName="lookUpColor"
                                                        KeyFieldName="TempID" TextFormatString="{0}" CssClass="text-box-input"
                                                        Paddings-PaddingTop="0" Paddings-PaddingBottom="0" Width="100%" Font-Size="13px"
                                                        OnLoad="ASPxGridLookupLoad_WidthSmall" OnDataBinding="GridLookupColor_DataBinding" IncrementalFilteringMode="Contains">
                                                        <ClientSideEvents ValueChanged="lookUpColor_Valuechanged" />
                                                        <ClearButton DisplayMode="OnHover" />
                                                        <FocusedStyle CssClass="focus-text-box-input"></FocusedStyle>
                                                        <GridViewStyles>
                                                            <Header CssClass="gridview-no-header-padding" ForeColor="Black"></Header>
                                                        </GridViewStyles>
                                                        <GridViewProperties>
                                                            <SettingsBehavior EnableRowHotTrack="True" AllowEllipsisInText="true" AllowDragDrop="false" />
                                                            <SettingsPager ShowSeparators="True" NumericButtonCount="3" EnableAdaptivity="true" />
                                                            <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowVerticalScrollBar="True"
                                                                ShowHorizontalScrollBar="true" VerticalScrollableHeight="200"></Settings>
                                                        </GridViewProperties>
                                                        <SettingsAdaptivity Mode="OnWindowInnerWidth" ModalDropDownCaption="Osebe" SwitchToModalAtWindowInnerWidth="650" />
                                                        <Columns>

                                                            <dx:GridViewDataTextColumn Caption="Naziv" FieldName="Naziv"
                                                                ReadOnly="true" MinWidth="230" MaxWidth="400" Width="100%">
                                                            </dx:GridViewDataTextColumn>

                                                        </Columns>
                                                    </dx:ASPxGridLookup>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row m-0 pb-3 justify-content-end">
                                        <div class="col-lg-6 mb-2 mb-lg-0">
                                            <dx:ASPxRadioButtonList ID="RadioButtonList" runat="server" ValueType="System.String" RepeatColumns="4" RepeatLayout="Flow" CssClass="float-right">
                                                <CaptionSettings Position="Top" />
                                                <ClientSideEvents ValueChanged="RadioButtonList_ValueChanged" />
                                                <Items>
                                                    <dx:ListEditItem Text="Vsi" Value="AllValues" Selected="true" />
                                                    <dx:ListEditItem Text="Negativne" Value="NegativeValues" />
                                                    <dx:ListEditItem Text="Pozitivne" Value="PositiveValues" />
                                                </Items>
                                            </dx:ASPxRadioButtonList>
                                        </div>
                                    </div>

                                    <h5 class="font-italic border-bottom pl-2 pb-1 mt-2 mb-3">Optimalna zaloga</h5>
                                    <dx:ASPxTreeList ID="TreeListOptimalStock" runat="server" utoGenerateColumns="False" ClientInstanceName="treeList"
                                        Width="100%" KeyFieldName="ID" ParentFieldName="ParentID" OnDataBinding="TreeListOptimalStock_DataBinding" SettingsSelection-Recursive="true"
                                        SettingsSelection-Enabled="true" SettingsSelection-AllowSelectAll="true" SettingsBehavior-AutoExpandAllNodes="true" SettingsBehavior-AllowFocusedNode="true"
                                        OnHtmlDataCellPrepared="TreeListOptimalStock_HtmlDataCellPrepared" OnDataBound="TreeListOptimalStock_DataBound">
                                        <Settings VerticalScrollBarMode="Visible" ScrollableHeight="430" ShowTreeLines="true" />
                                        <Columns>
                                            <dx:TreeListDataColumn FieldName="Name" Caption="Naziv" />
                                            <dx:TreeListDataColumn FieldName="KolicinaOptimalna" Caption="Kol. optimalna" />
                                            <dx:TreeListDataColumn FieldName="KolicinaZaloga" Caption="Kol. zaloga" />
                                            <dx:TreeListDataColumn FieldName="RazlikaZalogaOptimalna" Caption="Z - O" />
                                            <dx:TreeListDataColumn FieldName="KolicinaNarocenoVTeku" Caption="Kol. Naročeno v teku" />
                                            <dx:TreeListDataColumn FieldName="VsotaZalNarRazlikaOpt" Caption="Z + N - O" />
                                            <dx:TreeListDataColumn FieldName="VsotaZalNarKolicnikOpt" Caption="Z + N / O" />
                                        </Columns>
                                        <SettingsResizing ColumnResizeMode="NextColumn" Visualization="Live" />
                                        <Settings ShowFilterRow="true" ShowFilterBar="Auto" ShowFilterRowMenu="True" />
                                        <SettingsSelection Recursive="true" />
                                        <SettingsBehavior ExpandCollapseAction="NodeDblClick" />
                                    </dx:ASPxTreeList>
                                </div>
                            </div>

                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxCallbackPanel>
            </div>
            <div id="step2">
                <dx:ASPxCallbackPanel ID="CallbackPanelTreeHierarchyWithProducts" runat="server" ClientInstanceName="CallbackPanelTreeHierarchyWithProducts" OnCallback="CallbackPanelTreeHierarchyWithProducts_Callback">
                    <SettingsLoadingPanel Enabled="false" />
                    <ClientSideEvents EndCallback="CallbackPanelWizard_EndCallback" />
                    <PanelCollection>
                        <dx:PanelContent>

                            <div class="card">
                                <div class="card-body">

                                    <div class="row m-0 pb-3 pt-3">
                                        <div class="col-lg-6 mb-2 mb-lg-0">
                                            <div class="row m-0 align-items-center">
                                                <div class="col-0 p-0" style="margin-right: 15px;">
                                                    <dx:ASPxLabel ID="ASPxLabel9" runat="server" Text="DOBAVITELJ : " Font-Size="12px"></dx:ASPxLabel>
                                                </div>
                                                <div class="col-4 p-0 mr-3">
                                                    <dx:ASPxGridLookup ID="GridLookupSupplier" runat="server" ClientInstanceName="lookUpSupplier"
                                                        KeyFieldName="TempID" TextFormatString="{0}" CssClass="text-box-input"
                                                        Paddings-PaddingTop="0" Paddings-PaddingBottom="0" Width="100%" Font-Size="13px"
                                                        OnLoad="ASPxGridLookupLoad_WidthMedium" OnDataBinding="GridLookupSupplier_DataBinding" IncrementalFilteringMode="Contains">
                                                        <ClientSideEvents ValueChanged="lookUpSupplier_ValueChanged" />
                                                        <ClearButton DisplayMode="OnHover" />
                                                        <FocusedStyle CssClass="focus-text-box-input"></FocusedStyle>
                                                        <GridViewStyles>
                                                            <Header CssClass="gridview-no-header-padding" ForeColor="Black"></Header>
                                                        </GridViewStyles>
                                                        <GridViewProperties>
                                                            <SettingsBehavior EnableRowHotTrack="True" AllowEllipsisInText="true" AllowDragDrop="false" />
                                                            <SettingsPager EnableAdaptivity="true" PageSize="100">
                                                                <Summary Visible="true" Text="Vseh zapisov : {2}" EmptyText="Ni zapisov" />
                                                            </SettingsPager>
                                                            <Settings ShowFilterRow="false" ShowFilterRowMenu="false" ShowVerticalScrollBar="True"
                                                                ShowHorizontalScrollBar="true" VerticalScrollableHeight="250"></Settings>
                                                        </GridViewProperties>
                                                        <SettingsAdaptivity Mode="OnWindowInnerWidth" ModalDropDownCaption="Osebe" SwitchToModalAtWindowInnerWidth="650" />
                                                        <Columns>

                                                            <dx:GridViewDataTextColumn Caption="Naziv" FieldName="NazivPrvi"
                                                                ReadOnly="true" MinWidth="230" MaxWidth="400" Width="100%">
                                                                <Settings ShowFilterRowMenu="False" />
                                                            </dx:GridViewDataTextColumn>

                                                            <dx:GridViewDataTextColumn Caption="Zaloga" FieldName="HasStock"
                                                                ReadOnly="true" MinWidth="230" MaxWidth="400" Width="20%" Visible="False">
                                                                <Settings ShowFilterRowMenu="False" />
                                                            </dx:GridViewDataTextColumn>

                                                        </Columns>
                                                    </dx:ASPxGridLookup>
                                                </div>
                                                <div class="col-4 p-0 mr-3">
                                                    <dx:ASPxButton ID="ASPxButton1" runat="server" AutoPostBack="false" ClientInstanceName="btnSearch"
                                                        Height="25" Width="50" UseSubmitBehavior="false" ClientEnabled="true">
                                                        <Paddings Padding="0" />
                                                        <Image Url="../../Images/search.png" UrlHottracked="../../Images/searchHover.png" UrlDisabled="../../Images/searchDisabled.png" />
                                                        <ClientSideEvents Click="btnSearch_Click" />
                                                    </dx:ASPxButton>
                                                </div>

                                            </div>
                                        </div>
                                        <div class="col-lg-6 mb-2 mb-lg-0 text-right">
                                            <dx:ASPxButton ID="btnRefreshTreeList" runat="server" Text="<i class='fas fa-sync'></i> Osveži" AutoPostBack="false"
                                                Height="25" Width="110" UseSubmitBehavior="false" ClientVisible="false" ClientInstanceName="btnRefreshTreeList" EncodeHtml="false">
                                                <Paddings PaddingLeft="10" PaddingRight="10" />
                                                <ClientSideEvents Click="btnRefreshTreeList_Click" />
                                            </dx:ASPxButton>
                                        </div>
                                    </div>

                                    <dx:ASPxTreeList ID="TreeListOptimalStockWithProducts" runat="server" AutoGenerateColumns="False" ClientInstanceName="treeListWithProducts"
                                        Width="100%" KeyFieldName="ID" ParentFieldName="ParentID" OnDataBinding="TreeListOptimalStockWithProducts_DataBinding" SettingsSelection-Recursive="true"
                                        SettingsSelection-Enabled="true" SettingsSelection-AllowSelectAll="true" SettingsBehavior-AutoExpandAllNodes="true" SettingsBehavior-AllowFocusedNode="true"
                                        OnDataBound="TreeListOptimalStockWithProducts_DataBound" OnHtmlRowPrepared="TreeListOptimalStockWithProducts_HtmlRowPrepared">
                                        <ClientSideEvents NodeDblClick="treeListWithProducts_NodeDoubleClick" />
                                        <Settings VerticalScrollBarMode="Visible" ScrollableHeight="430" ShowTreeLines="true" HorizontalScrollBarMode="Auto" />
                                        <Columns>
                                            <dx:TreeListDataColumn FieldName="Name" Caption="Naziv" Width="40%" />
                                            <dx:TreeListDataColumn FieldName="KolicinaOptimalna" Caption="Kol.opt." Width="15%" DisplayFormat="{0:n2}" />
                                            <dx:TreeListDataColumn FieldName="KolicinaZaloga" Caption="Kol.zaloga" Width="15%" DisplayFormat="{0:n2}" />
                                            <%--<dx:TreeListDataColumn FieldName="RazlikaZalogaOptimalna" Caption="Z - O" Width="10%" />
                                            <dx:TreeListDataColumn FieldName="KolicinaNarocenoVTeku" Caption="Kol.Naročeno v teku" Width="10%" />--%>
                                            <dx:TreeListDataColumn FieldName="VsotaZalNarRazlikaOpt" Caption="Z + N - O" Width="15%" DisplayFormat="{0:n2}" />
                                            <dx:TreeListDataColumn FieldName="KolicinaNarocilo" Caption="Naročilo" Width="15%" DisplayFormat="{0:n2}" />
                                        </Columns>
                                        <SettingsResizing ColumnResizeMode="NextColumn" Visualization="Live" />
                                        <Settings ShowFilterRow="true" ShowFilterBar="Auto" ShowFilterRowMenu="True" ShowFooter="True" />
                                        <SettingsSelection Recursive="true" />
                                        <SettingsBehavior ExpandCollapseAction="NodeDblClick" />
                                        <Styles>
                                            <%-- <FocusedNode BackColor="Transparent" />--%>
                                            <%--<SelectedNode BackColor="Transparent" />--%>
                                        </Styles>
                                        <Summary>
                                            <dx:TreeListSummaryItem DisplayFormat="Vsota: {0:n2}" SummaryType="Sum" FieldName="KolicinaNarocilo" ShowInColumn="KolicinaNarocilo" />
                                        </Summary>
                                    </dx:ASPxTreeList>

                                    <dx:ASPxPopupControl ID="PopupControlMainProductProductsClick" runat="server" ContentUrl="MainProductProducts_popup.aspx"
                                        ClientInstanceName="PopupControlMainProductProductsClick" Modal="True" HeaderText="IZDELKI NOSILNEGA IZDELKA"
                                        CloseAction="CloseButton" Width="1000px" Height="570px" PopupHorizontalAlign="WindowCenter"
                                        PopupVerticalAlign="WindowCenter" PopupAnimationType="Fade" AllowDragging="true" ShowSizeGrip="true"
                                        AllowResize="true" ShowShadow="true"
                                        OnWindowCallback="PopupControlMainProductProductsClick_WindowCallback">
                                        <SettingsAdaptivity Mode="OnWindowInnerWidth" SwitchAtWindowInnerWidth="800" VerticalAlign="WindowCenter" MinHeight="400px" MaxWidth="680px" MaxHeight="400px" />
                                        <ContentStyle BackColor="#F7F7F7">
                                            <Paddings Padding="0px"></Paddings>
                                        </ContentStyle>
                                    </dx:ASPxPopupControl>

                                    <dx:ASPxPopupControl ID="PopupControlSearchSupplier" runat="server" ContentUrl="SearchSupplier_popup.aspx"
                                        ClientInstanceName="PopupControlSearchSupplier" Modal="True" HeaderText="DOBAVITELJ"
                                        CloseAction="CloseButton" Width="800px" Height="635px" PopupHorizontalAlign="WindowCenter"
                                        PopupVerticalAlign="WindowCenter" PopupAnimationType="Fade" AllowDragging="true" ShowSizeGrip="true"
                                        AllowResize="true" ShowShadow="true"
                                        OnWindowCallback="PopupControlSearchSupplier_WindowCallback">
                                        <ContentStyle BackColor="#F7F7F7">
                                            <Paddings PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px" PaddingTop="0px"></Paddings>
                                        </ContentStyle>
                                    </dx:ASPxPopupControl>
                                </div>
                            </div>

                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxCallbackPanel>
            </div>
            <div id="step3">
                <dx:ASPxCallbackPanel ID="CallbackPanelProducts" runat="server" ClientInstanceName="CallbackPanelProducts" OnCallback="CallbackPanelProducts_Callback">
                    <SettingsLoadingPanel Enabled="false" />
                    <ClientSideEvents EndCallback="CallbackPanelWizard_EndCallback" />
                    <PanelCollection>
                        <dx:PanelContent>
                            <div class="card">
                                <div class="card-body">
                                    <div class="row m-0 pb-3 pt-3">
                                        <div class="col-lg-6 mb-2 mb-lg-0">
                                            <div class="row m-0 align-items-center">
                                                <div class="col-0 p-0 mr-3">
                                                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="DOBAVITELJ : " Font-Size="12px"></dx:ASPxLabel>
                                                </div>
                                                <div class="col p-0 mr-3">
                                                    <dx:ASPxTextBox runat="server" ID="txtSupplier" ClientInstanceName="txtSupplier"
                                                        CssClass="text-box-input" Font-Size="13px" Width="100%" MaxLength="250" AutoCompleteType="Disabled"
                                                        Font-Bold="true" ClientEnabled="false" BackColor="LightGray">
                                                        <FocusedStyle CssClass="focus-text-box-input"></FocusedStyle>
                                                    </dx:ASPxTextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row m-0 pb-3">
                                        <div class="col-lg-12 mb-2 mb-lg-0">
                                            <div class="row m-0 align-items-center">
                                                <div class="col-0 p-0" style="margin-right: 45px;">
                                                    <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="NAZIV : " Font-Size="12px"></dx:ASPxLabel>
                                                </div>
                                                <div class="col p-0">
                                                    <dx:ASPxTextBox runat="server" ID="txtName" ClientInstanceName="txtName"
                                                        CssClass="text-box-input" Font-Size="13px" Width="100%" MaxLength="250" AutoCompleteType="Disabled">
                                                        <FocusedStyle CssClass="focus-text-box-input"></FocusedStyle>
                                                    </dx:ASPxTextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row m-0 pb-3">
                                        <div class="col-lg-6 mb-2 mb-lg-0">
                                            <div class="row m-0 align-items-center">
                                                <div class="col-0 p-0" style="margin-right: 29px;">
                                                    <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="KOLIČINA : " Font-Size="12px"></dx:ASPxLabel>
                                                </div>
                                                <div class="col-6 p-0 mr-3">
                                                    <dx:ASPxTextBox runat="server" ID="txtSumQuantity" ClientInstanceName="txtSumQuantity"
                                                        CssClass="text-box-input" Font-Size="13px" Width="100%" MaxLength="250" AutoCompleteType="Disabled"
                                                        Font-Bold="true" ClientEnabled="false" BackColor="LightGray">
                                                        <FocusedStyle CssClass="focus-text-box-input"></FocusedStyle>
                                                    </dx:ASPxTextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-lg-6 mb-2 mb-lg-0">
                                            <div class="row m-0 align-items-center justify-content-end">
                                                <div class="col-0 p-0 mr-3">
                                                    <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="DATUM ODDAJE : " Font-Size="12px"></dx:ASPxLabel>
                                                </div>
                                                <div class="col-4 p-0">
                                                    <dx:ASPxDateEdit ID="DateEditSubmitOrder" runat="server" EditFormat="Date" Width="100%"
                                                        CssClass="text-box-input date-edit-padding" Font-Size="13px"
                                                        ClientInstanceName="DateEditSubmitOrder">
                                                        <FocusedStyle CssClass="focus-text-box-input" />
                                                        <CalendarProperties TodayButtonText="Danes" ClearButtonText="Izbriši" />
                                                        <DropDownButton Visible="true"></DropDownButton>
                                                    </dx:ASPxDateEdit>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row m-0 pb-3">
                                        <div class="col-lg-12 mb-2 mb-lg-0">
                                            <div class="row m-0 align-items-center">
                                                <div class="col-0 p-0" style="margin-right: 33px;">
                                                    <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="OPOMBE : " Font-Size="12px"></dx:ASPxLabel>
                                                </div>
                                                <div class="col p-0">
                                                    <dx:ASPxMemo ID="MemoNotes" runat="server" Width="100%" Rows="3" MaxLength="8000" CssClass="text-box-input" AutoCompleteType="Disabled">
                                                        <FocusedStyle CssClass="focus-text-box-input"></FocusedStyle>
                                                    </dx:ASPxMemo>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row m-0 pb-3">
                                        <div class="col-lg-12 mb-2 mb-lg-0">
                                            <dx:ASPxGridView ID="ASPxGridViewProducts" runat="server" Width="100%" KeyFieldName="NarociloOptimalnihZalogPozicijaID" OnDataBinding="ASPxGridViewProducts_DataBinding"
                                                CssClass="gridview-no-header-padding mw-100" EnableRowsCache="false" AutoGenerateColumns="False" ClientInstanceName="gridProducts"
                                                OnBatchUpdate="ASPxGridViewProducts_BatchUpdate" OnDataBound="ASPxGridViewProducts_DataBound" OnHtmlRowPrepared="ASPxGridViewProducts_HtmlRowPrepared">
                                                <%--<ClientSideEvents BatchEditEndEditing="gridDevices_EndEditing" />--%>
                                                <SettingsAdaptivity AdaptivityMode="HideDataCellsWindowLimit" AllowOnlyOneAdaptiveDetailExpanded="true" HideDataCellsAtWindowInnerWidth="800"
                                                    AllowHideDataCellsByColumnMinWidth="true">
                                                </SettingsAdaptivity>
                                                <SettingsBehavior AllowEllipsisInText="true" />
                                                <Paddings Padding="0" />
                                                <Settings ShowVerticalScrollBar="True"
                                                    ShowFilterBar="Auto" ShowFilterRow="True" VerticalScrollableHeight="300"
                                                    ShowFilterRowMenu="True" VerticalScrollBarStyle="Standard" VerticalScrollBarMode="Auto" ShowFooter="true" />
                                                <SettingsPager PageSize="50" ShowNumericButtons="true" NumericButtonCount="6">
                                                    <PageSizeItemSettings Visible="true" Items="50" Caption="Zapisi na stran : " AllItemText="Vsi">
                                                    </PageSizeItemSettings>
                                                    <Summary Visible="true" Text="Vseh zapisov : {2}" EmptyText="Ni zapisov" />
                                                </SettingsPager>
                                                <SettingsBehavior AllowFocusedRow="true" AllowEllipsisInText="true" />
                                                <Styles>
                                                    <Header Paddings-PaddingTop="5" HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="true"></Header>
                                                    <FocusedRow BackColor="#d1e6fe" Font-Bold="true" ForeColor="#606060"></FocusedRow>
                                                    <Cell Wrap="False" />
                                                </Styles>
                                                <SettingsText EmptyDataRow="Trenutno ni podatka o artiklih." CommandBatchEditUpdate="Spremeni" CommandBatchEditCancel="Prekliči"
                                                    CommandBatchEditPreviewChanges="Predogled" />
                                                <SettingsEditing Mode="Batch" BatchEditSettings-StartEditAction="DblClick" />

                                                <EditFormLayoutProperties>
                                                    <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="600" />
                                                </EditFormLayoutProperties>

                                                <SettingsCommandButton>
                                                    <DeleteButton Text="Odstrani" />
                                                    <RecoverButton Text="Povrni" />
                                                </SettingsCommandButton>

                                                <Columns>
                                                    <dx:GridViewCommandColumn Caption="Briši" ShowDeleteButton="true" Width="8%" VisibleIndex="0" />
                                                    <dx:GridViewDataTextColumn Caption="Kategorija" FieldName="KategorijaNaziv" AdaptivePriority="2" MinWidth="100" MaxWidth="180" Width="12%" EditFormSettings-Visible="False" />
                                                    <dx:GridViewDataTextColumn Caption="Ident Pantheon" FieldName="IdentArtikla_P" AdaptivePriority="2" MinWidth="130" MaxWidth="200" Width="20%" EditFormSettings-Visible="False" SortOrder="Descending" />
                                                    <dx:GridViewDataTextColumn Caption="Naziv" FieldName="NazivArtikla" AdaptivePriority="1" MinWidth="150" MaxWidth="350" Width="30%" EditFormSettings-Visible="False" />
                                                    <dx:GridViewDataTextColumn Caption="Količina" FieldName="Kolicina" AdaptivePriority="1" MinWidth="100" MaxWidth="180" Width="12%">
                                                        <PropertiesTextEdit>
                                                            <ValidationSettings>
                                                                <RegularExpression ErrorText="Vnesi število" ValidationExpression="^\d+([.,]\d{1,9})?$" />
                                                            </ValidationSettings>
                                                        </PropertiesTextEdit>
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="Opomba" FieldName="Opombe" AdaptivePriority="1" MinWidth="150" MaxWidth="400" Width="20%" />
                                                    <dx:GridViewDataTextColumn FieldName="VsotaZalNarRazlikaOpt" Caption="Z + N - O" AdaptivePriority="1" MinWidth="100" MaxWidth="180" Width="12%"
                                                        PropertiesTextEdit-DisplayFormatString="N2" />
                                                </Columns>
                                                <TotalSummary>
                                                    <dx:ASPxSummaryItem DisplayFormat="Vsota: {0:n2}" FieldName="Kolicina" SummaryType="Sum" />
                                                </TotalSummary>
                                            </dx:ASPxGridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxCallbackPanel>
            </div>
        </div>
    </div>

    <dx:ASPxButton ID="btnCancel" runat="server" Text="Prekliči" AutoPostBack="false" OnClick="btnCancelPopup_Click"
        Height="25" Width="110" UseSubmitBehavior="false" ClientVisible="false" ClientInstanceName="btnCancel">
        <Paddings PaddingLeft="10" PaddingRight="10" />
    </dx:ASPxButton>

    <dx:ASPxButton ID="btnConfirm" runat="server" Text="Potrdi" AutoPostBack="false" OnClick="btnConfirmPopup_Click"
        Height="25" Width="110" UseSubmitBehavior="false" ClientVisible="false" ClientInstanceName="btnConfirm">
        <Paddings PaddingLeft="10" PaddingRight="10" />
        <ClientSideEvents Click="HandleUserAction" />
    </dx:ASPxButton>


    <!-- Modal -->
    <div class="modal fade" id="ConfirmModal" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="staticBackdropLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">

                <div class="modal-body">
                    <div class="container-fluid">
                        <div class="row align-items-center justify-content-end">
                            <div class="col text-right">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                        </div>
                        <div class="row align-items-center justify-content-center">
                            <div class="col text-center">
                                <i class="fas fa-times" style="font-size: 55px; color: tomato;"></i>

                            </div>
                        </div>
                        <div class="row align-items-center justify-content-center">
                            <div class="col text-center">
                                <h1 id="ModalTitle" class="display-4 my-3" style="font-size: 30px;"></h1>
                            </div>
                        </div>
                        <div class="row align-items-center justify-content-center">
                            <div class="col text-center">
                                <p class="text-muted" id="ModalMessage"></p>
                            </div>
                        </div>
                        <div class="row align-items-center justify-content-center mt-2">
                            <div class="col text-center">
                                <button type="button" class="btn btn-secondary py-2 px-5" data-dismiss="modal">Razumem</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>


