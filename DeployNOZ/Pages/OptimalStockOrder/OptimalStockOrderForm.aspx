<%@ Page Title="Naročilo optimalnih zalog" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="OptimalStockOrderForm.aspx.cs" Inherits="GrafolitNOZ.Pages.OptimalStockOrder.OptimalStockOrderForm" %>

<%@ MasterType VirtualPath="~/Main.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContentHolder" runat="server">
    <script type="text/javascript">
        var postbackInitiated = false;
        var confirmSubmitCopiedOrder = false;

        $(document).ready(function () {
            $("#btnModalConfirm").on("click", function () {
                confirmSubmitCopiedOrder = true;
                btnSubmitOrder.DoClick();
            });
        });

        function ActionButton_Click(s, e) {
            var process = true;

            if (process) {
                LoadingPanel.Show();
                e.processOnServer = !postbackInitiated;
                postbackInitiated = true;
            }
            else
                e.processOnServer = false;
        }

        function SubmitOrderButton_Click(s, e) {
            var process = true;

            if (process && confirmSubmitCopiedOrder) {
                LoadingPanel.Show();
                e.processOnServer = !postbackInitiated;
                postbackInitiated = true;
                confirmSubmitCopiedOrder = false;
            }
            else if (!confirmSubmitCopiedOrder) {
                ShowModal("Ali ste prepričani?", "Z gumbom potrditev boste kreirali naročilnico. Ste prepričani?");
                e.processOnServer = false;
            }
            else
                e.processOnServer = false;
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

        function gridProducts_StartEditing(s, e) {
            clientBtnSaveChangesGrid.SetEnabled(true);
            clientBtnCancelChangesGrid.SetEnabled(true);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <div class="card">
        <div class="card-body">
            <div class="row m-0 pb-3">
                <div class="col-lg-4 mb-2 mb-lg-0">
                    <div class="row m-0 align-items-center">
                        <div class="col-0 p-0" style="margin-right: 25px">
                            <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="ZAP. ŠTEV. : " Font-Size="12px"></dx:ASPxLabel>
                        </div>
                        <div class="col p-0 mr-3">
                            <dx:ASPxTextBox runat="server" ID="txtNumber" ClientInstanceName="txtNumber"
                                CssClass="text-box-input" Font-Size="13px" Width="100%" MaxLength="50" AutoCompleteType="Disabled"
                                Font-Bold="true" ClientEnabled="false" BackColor="LightGray">
                                <FocusedStyle CssClass="focus-text-box-input"></FocusedStyle>
                            </dx:ASPxTextBox>
                        </div>
                    </div>
                </div>

                <div class="col-lg-4 mb-2 mb-lg-0">
                    <div class="row m-0 align-items-center justify-content-center">
                        <div class="col-0 p-0 mr-3">
                            <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="STATUS : " Font-Size="12px"></dx:ASPxLabel>
                        </div>
                        <div class="col p-0 mr-3">
                            <dx:ASPxTextBox runat="server" ID="txtStatusName" ClientInstanceName="txtStatusName"
                                CssClass="text-box-input" Font-Size="13px" Width="100%" MaxLength="250" AutoCompleteType="Disabled"
                                Font-Bold="true" ClientEnabled="false" BackColor="LightGray">
                                <FocusedStyle CssClass="focus-text-box-input"></FocusedStyle>
                            </dx:ASPxTextBox>
                        </div>
                    </div>
                </div>

                <div class="col-lg-4 mb-2 mb-lg-0">
                    <div class="row m-0 pr-0 align-items-center justify-content-end">
                        <div class="col-0 p-0 mr-3">
                            <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="PANTHEON NAROČILNICA : " Font-Size="12px"></dx:ASPxLabel>
                        </div>
                        <div class="col p-0">
                            <dx:ASPxTextBox runat="server" ID="txtPantheonOrderNumber" ClientInstanceName="txtPantheonOrderNumber"
                                CssClass="text-box-input" Font-Size="13px" Width="100%" MaxLength="150" AutoCompleteType="Disabled"
                                Font-Bold="true" ClientEnabled="false" BackColor="LightGray">
                                <FocusedStyle CssClass="focus-text-box-input"></FocusedStyle>
                            </dx:ASPxTextBox>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row m-0 pb-3">
                <div class="col-lg-4 mb-2 mb-lg-0">
                    <div class="row m-0 align-items-center">
                        <div class="col-0 p-0 mr-3">
                            <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="DOBAVITELJ : " Font-Size="12px"></dx:ASPxLabel>
                        </div>
                        <div class="col p-0">
                            <dx:ASPxTextBox runat="server" ID="txtSupplier" ClientInstanceName="txtSupplier"
                                CssClass="text-box-input" Font-Size="13px" Width="100%" MaxLength="350" AutoCompleteType="Disabled"
                                Font-Bold="true" ClientEnabled="false" BackColor="LightGray">
                                <FocusedStyle CssClass="focus-text-box-input"></FocusedStyle>
                            </dx:ASPxTextBox>
                        </div>
                    </div>
                </div>

                <div class="col-lg-8 mb-2 mb-lg-0">
                    <div class="row m-0 align-items-center justify-content-end">
                        <div class="col-0 p-0" style="margin-right: 20px">
                            <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="NAZIV : " Font-Size="12px"></dx:ASPxLabel>
                        </div>
                        <div class="col p-0">
                            <dx:ASPxTextBox runat="server" ID="txtName" ClientInstanceName="txtName"
                                CssClass="text-box-input" Font-Size="13px" Width="100%" MaxLength="350" AutoCompleteType="Disabled">
                                <FocusedStyle CssClass="focus-text-box-input"></FocusedStyle>
                            </dx:ASPxTextBox>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row m-0 pb-3">
                <div class="col-lg-4 mb-2 mb-lg-0">
                    <div class="row m-0 align-items-center">
                        <div class="col-0 p-0" style="margin-right: 30px">
                            <dx:ASPxLabel ID="ASPxLabel7" runat="server" Text="KOLIČINA : " Font-Size="12px"></dx:ASPxLabel>
                        </div>
                        <div class="col p-0">
                            <dx:ASPxTextBox runat="server" ID="txtQuantity" ClientInstanceName="txtQuantity"
                                CssClass="text-box-input" Font-Size="13px" Width="100%" MaxLength="350" AutoCompleteType="Disabled"
                                Font-Bold="true" ClientEnabled="false" BackColor="LightGray">
                                <FocusedStyle CssClass="focus-text-box-input"></FocusedStyle>
                            </dx:ASPxTextBox>
                        </div>
                    </div>
                </div>

                <div class="col-lg-4 mb-2 mb-lg-0">
                    <div class="row m-0 align-items-center justify-content-center">
                        <div class="col-0 p-0 mr-3">
                            <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="DATUM ODDAJE : " Font-Size="12px"></dx:ASPxLabel>
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

                <div class="col-lg-4 mb-2 mb-lg-0">
                    <div class="row m-0 align-items-center justify-content-end">
                        <div class="col-0 p-0 mr-3">
                            <dx:ASPxLabel ID="ASPxLabel8" runat="server" Text="NAROČILO ODDAL : " Font-Size="12px"></dx:ASPxLabel>
                        </div>
                        <div class="col p-0">
                            <dx:ASPxTextBox runat="server" ID="txtEmployee" ClientInstanceName="txtEmployee"
                                CssClass="text-box-input" Font-Size="13px" Width="100%" MaxLength="350" AutoCompleteType="Disabled"
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
                        <div class="col-0 p-0" style="margin-right: 33px;">
                            <dx:ASPxLabel ID="ASPxLabel9" runat="server" Text="OPOMBE : " Font-Size="12px"></dx:ASPxLabel>
                        </div>
                        <div class="col p-0">
                            <dx:ASPxMemo ID="MemoNotes" runat="server" Width="100%" Rows="10" MaxLength="8000" CssClass="text-box-input" AutoCompleteType="Disabled">
                                <FocusedStyle CssClass="focus-text-box-input"></FocusedStyle>
                            </dx:ASPxMemo>
                        </div>
                    </div>
                </div>
            </div>
            <h5 class="font-italic border-bottom pl-2 pb-1 mt-2 mb-3">Pozicije naročila</h5>
            <div class="row m-0 pb-3">
                <div class="col mb-2 mb-lg-0">
                    <dx:ASPxGridView ID="ASPxGridViewProducts" runat="server" Width="100%" KeyFieldName="NarociloOptimalnihZalogPozicijaID" OnDataBinding="ASPxGridViewProducts_DataBinding"
                        CssClass="gridview-no-header-padding mw-100" EnableRowsCache="false" AutoGenerateColumns="False" ClientInstanceName="gridProducts"
                        OnBatchUpdate="ASPxGridViewProducts_BatchUpdate">
                        <ClientSideEvents BatchEditStartEditing="gridProducts_StartEditing" />
                        <SettingsAdaptivity AdaptivityMode="HideDataCells" AllowOnlyOneAdaptiveDetailExpanded="true"
                            AllowHideDataCellsByColumnMinWidth="true">
                        </SettingsAdaptivity>
                        <SettingsBehavior AllowEllipsisInText="true" />
                        <Paddings Padding="0" />
                        <Settings ShowVerticalScrollBar="True"
                            ShowFilterBar="Auto" ShowFilterRow="True" VerticalScrollableHeight="250"
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
                            <StatusBar CssClass="d-none" /> 
                        </Styles>
                        <SettingsText EmptyDataRow="Trenutno ni podatka o artiklih." CommandBatchEditUpdate="Spremeni" CommandBatchEditCancel="Prekliči"
                            CommandBatchEditPreviewChanges="Predogled" />
                        <SettingsEditing Mode="Batch" BatchEditSettings-StartEditAction="DblClick" />

                        <EditFormLayoutProperties>
                            <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="600" />
                        </EditFormLayoutProperties>

                        <Columns>
                            <dx:GridViewDataTextColumn Caption="Kategorija" FieldName="KategorijaNaziv" AdaptivePriority="2" MinWidth="100" MaxWidth="180" Width="12%" EditFormSettings-Visible="False" />
                            <dx:GridViewDataTextColumn Caption="Ident Pantheon" FieldName="IdentArtikla_P" AdaptivePriority="2" MinWidth="130" MaxWidth="200" Width="20%" EditFormSettings-Visible="False" />
                            <dx:GridViewDataTextColumn Caption="Naziv" FieldName="NazivArtikla" AdaptivePriority="1" MinWidth="150" MaxWidth="350" Width="30%" EditFormSettings-Visible="False" />
                            <dx:GridViewDataTextColumn Caption="Količina" FieldName="Kolicina" AdaptivePriority="1" MinWidth="100" MaxWidth="180" Width="15%">
                                <PropertiesTextEdit>
                                    <ValidationSettings>
                                        <%--<RegularExpression ErrorText="Vnesi število" ValidationExpression="/^\d*\.?\d+$/" />--%>
                                    </ValidationSettings>
                                </PropertiesTextEdit>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Opomba" FieldName="Opombe" AdaptivePriority="1" MinWidth="150" MaxWidth="400" Width="20%" />
                        </Columns>
                        
                        <TotalSummary>
                            <dx:ASPxSummaryItem DisplayFormat="Vsota: {0:n2}" FieldName="Kolicina" SummaryType="Sum" />
                        </TotalSummary>
                        
                        <Templates>
                            <StatusBar>
                                <div class="row m-2">
                                    <div class="col-6">
                                        <span class="AddEditButtons no-margin-top">
                                            <dx:ASPxButton ID="btnAddOrderPosition" runat="server" Text="Dodaj pozicijo" AutoPostBack="false" CssClass="statusBarButton"
                                                Height="40" Width="110" UseSubmitBehavior="false" ClientInstanceName="clientBtnAddOrderPosition">
                                                <ClientSideEvents Click="btnAddOrderPosition_Click" />
                                                <DisabledStyle CssClass="statusBarButtonsDisabled"></DisabledStyle>
                                            </dx:ASPxButton>
                                        </span>
                                        <span class="AddEditButtons no-margin-top">
                                            <dx:ASPxButton ID="btnDeleteSelectedPosition" runat="server" Text="Izbriši izbrano pozicijo" AutoPostBack="false" CssClass="statusBarButton red"
                                                Height="40" Width="110" UseSubmitBehavior="false" ClientEnabled="false" ClientInstanceName="clientBtnDeleteSelectedPosition">
                                                <ClientSideEvents Click="btnDeleteSelectedPosition_Click" Init="btnDeleteSelectedPosition_Init" />
                                                <DisabledStyle CssClass="statusBarButtonsDisabled"></DisabledStyle>
                                            </dx:ASPxButton>
                                        </span>
                                    </div>
                                    <div class="col-6 text-right">
                                        <span class="AddEditButtons no-margin-top">
                                            <dx:ASPxButton ID="btnSaveChanges" runat="server" Text="Spremeni" AutoPostBack="false" CssClass="statusBarButton"
                                                Height="40" Width="110" UseSubmitBehavior="false" ClientEnabled="false" ClientInstanceName="clientBtnSaveChangesGrid">
                                                <DisabledStyle CssClass="statusBarButtonsDisabled" />
                                                <ClientSideEvents Click="function(s,e) { gridProducts.UpdateEdit(); }" />
                                            </dx:ASPxButton>
                                        </span>
                                        <span class="AddEditButtons no-margin-top">
                                            <dx:ASPxButton ID="btnCancelChanges" runat="server" Text="Prekliči" AutoPostBack="false" CssClass="statusBarButton"
                                                Height="40" Width="110" UseSubmitBehavior="false" ClientEnabled="false" ClientInstanceName="clientBtnCancelChangesGrid">
                                                <DisabledStyle CssClass="statusBarButtonsDisabled" />
                                                <ClientSideEvents Click="function(s,e) { gridProducts.CancelEdit(); }" />
                                            </dx:ASPxButton>
                                        </span>
                                    </div>
                                </div>
                            </StatusBar>
                        </Templates>
                    </dx:ASPxGridView>
                </div>
            </div>

            <div class="row m-0">
                <div class="col-lg-6">
                    <div class="row m-0 align-items-center">
                        <div class="col-0 p-0 pr-2">
                            <dx:ASPxButton ID="btnSubmitOrder" runat="server" Text="Oddaj naročilo" AutoPostBack="false" OnClick="btnSubmitOrder_Click"
                                Height="25" Width="110" UseSubmitBehavior="false" ClientVisible="false" ClientInstanceName="btnSubmitOrder">
                                <Paddings PaddingLeft="10" PaddingRight="10" />
                                <Image Url="../../Images/newOrder.png" UrlHottracked="../../Images/newOrderHover.png" />
                                <ClientSideEvents Click="SubmitOrderButton_Click" />
                            </dx:ASPxButton>
                        </div>
                    </div>
                </div>
                <div class="col-lg-6">
                    <%-- Gumbi za Naročilo --%>
                    <div class="row m-0 align-items-center justify-content-end">
                        <div class="col-0 p-0 pr-2">
                            <dx:ASPxButton ID="btnCancel" runat="server" Text="Prekliči" AutoPostBack="false" OnClick="btnCancel_Click"
                                Height="25" Width="110" UseSubmitBehavior="false">
                                <Paddings PaddingLeft="10" PaddingRight="10" />
                                <Image Url="../../Images/cancel.png" UrlHottracked="../../Images/cancelHover.png" />
                            </dx:ASPxButton>
                        </div>
                        <div class="col-0 p-0">
                            <dx:ASPxButton ID="btnSaveChanges" runat="server" Text="Shrani" AutoPostBack="false" OnClick="btnSaveChanges_Click"
                                Height="25" Width="110" ClientInstanceName="clientBtnSaveChanges" UseSubmitBehavior="false">
                                <Paddings PaddingLeft="10" PaddingRight="10" />
                                <Image Url="../../Images/add.png" UrlHottracked="../../Images/addHover.png" />
                                <ClientSideEvents Click="ActionButton_Click" />
                            </dx:ASPxButton>
                        </div>

                    </div>
                </div>
            </div>
        </div>

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
                                    <i class="fas fa-question" style="font-size: 55px; color: tomato;"></i>

                                </div>
                            </div>
                            <div class="row align-items-center justify-content-center">
                                <div class="col text-center">
                                    <h1 id="ModalTitle" class="display-4 my-3" style="font-size: 30px;">Ali ste prepričani?</h1>
                                </div>
                            </div>
                            <div class="row align-items-center justify-content-center">
                                <div class="col text-center">
                                    <p class="text-muted" id="ModalMessage">Z gumbom potrditev boste dokončno izbrisali zapis v aplikaciji. Ste prepričani?</p>
                                </div>
                            </div>
                            <div class="row align-items-center justify-content-center mt-2">
                                <div class="col text-center">
                                    <button type="button" class="btn btn-secondary py-2 px-5" data-dismiss="modal">Prekliči</button>
                                    <button type="button" id="btnModalConfirm" class="btn btn-success py-2 px-5 ml-3">Potrdi</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
</asp:Content>
