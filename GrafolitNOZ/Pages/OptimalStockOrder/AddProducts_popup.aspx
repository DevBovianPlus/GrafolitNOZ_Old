<%@ Page Title="" Language="C#" MasterPageFile="~/Popup.Master" AutoEventWireup="true" CodeBehind="AddProducts_popup.aspx.cs" Inherits="GrafolitNOZ.Pages.OptimalStockOrder.AddProducts_popup" %>

<%@ Register Assembly="DevExpress.Web.v19.2, Version=19.2.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ MasterType VirtualPath="~/Popup.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var hasStartEditing = false;
        var postbackInitiated = false;

        function CheckFieldValidation() {
            var process = false;
            var lookUpItems = [lookUpCategory];

            var inputItems = [txtName];
            var dateItems = null;

            if (btnConfirm.GetText() == 'Izbriši')
                process = true;
            else {
                if ((txtQuantity1.GetText().trim() == "" || txtUnitOfMeasure1.GetText().trim() == "") && (txtQuantity2.GetText().trim() == "" || txtUnitOfMeasure2.GetText().trim() == "")) {
                    inputItems.push(txtQuantity1);
                    inputItems.push(txtUnitOfMeasure1);
                }
                else {
                    $(txtQuantity1.GetInputElement()).parent().parent().parent().removeClass("focus-text-box-input-error");
                    $(txtUnitOfMeasure1.GetInputElement()).parent().parent().parent().removeClass("focus-text-box-input-error");
                }

                //če ni dodanega nobenega dobavitelja ne smemo dovoliti shranjevanja pozicija, zato obarvamo okvirček za iskanje dobaviteljev
                //if (gridSelectedSupplier.GetVisibleRowsOnPage() <= 0)
                //    inputItems.push(txtSupplierSearch);
                //else
                //    $(txtSupplierSearch.GetInputElement()).parent().parent().parent().removeClass("focus-text-box-input-error");

                process = InputFieldsValidation(lookUpItems, inputItems, dateItems, /*memoItems*/ null, /*comboBoxItems*/null, null);


            }

            return process;
        }

        function ActionButton_Click(s, e) {
            process = true;
            if (gridSelectedArtikel.GetVisibleRowsOnPage() <= 0) {
                process = false;
            }


            if (process) {
                e.processOnServer = !postbackInitiated;
                postbackInitiated = true;
            }
            else
                e.processOnServer = false;
        }

        function CallbackPanel_EndCallback(s, e) {
            //if (s.cpNoSelectedValidationError != "" && s.cpNoSelectedValidationError != undefined) {
            //    ShowModal('Pozicija naročila!', 'Vsaj ena pozija mora biti izbrana!')
            //    delete (s.cpNoSelectedValidationError);
            //}
        }

        function btnSearch_Click(s, e) {
            //var process = CheckFieldValidation();
            var process = true;

            if (process) {
                CallbackPanel.PerformCallback("StartSearchPopup");
            }
            else
                e.processOnServer = false;
        }

        function btnAddArtikel_Click(s, e) {
            var process = CheckFieldValidation();


            if (process) {
                CallbackPanel.PerformCallback("AddArtikel");
            }
            else
                e.processOnServer = false;
        }

        function btnAddContactPerson_Click(s, e) {
            var inputItems = [txtSupplierName];
            var process = InputFieldsValidation(null, inputItems, null, null, null, null);


            if (process) {
                CallbackPanel.PerformCallback("AddContactPersons");
            }
            else
                e.processOnServer = false;
        }

        function txtSupplierSearch_KeyUp(s, e) {
            if (s.GetText().length >= 3)
                btnSearch.SetEnabled(true);
            else
                btnSearch.SetEnabled(false);
        }

        function StartSearchByKeyboardKey(s, e) {
            if (e.htmlEvent.keyCode == 13 && s.GetText().length >= 3)//Enter
            {
                //var process = CheckFieldValidation();
                var process = true;

                if (process) {
                    CallbackPanel.PerformCallback("StartSearchPopup");
                }
                else
                    e.processOnServer = false;
            }
        }

       
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <dx:ASPxCallbackPanel ID="CallbackPanel" ClientInstanceName="CallbackPanel" OnCallback="CallbackPanel_Callback" runat="server">
        <ClientSideEvents EndCallback="CallbackPanel_EndCallback" />
        <PanelCollection>
            <dx:PanelContent>

                <div class="row m-0 pb-3 pt-3">
                    <div class="col-lg-6 mb-2 mb-lg-0">
                        <div class="row m-0 align-items-center">
                            <div class="col-0 p-0" style="margin-right: 15px;">
                                <dx:ASPxLabel ID="ASPxLabel9" runat="server" Text="DOBAVITELJ : *" Font-Size="12px"></dx:ASPxLabel>
                            </div>
                            <div class="col-6 p-0 mr-3">
                                <dx:ASPxTextBox runat="server" ID="txtSupplierSearch" ClientInstanceName="txtSupplierSearch" MaxLength="30"
                                    CssClass="text-box-input" Font-Size="13px" Width="100%" AutoCompleteType="Disabled" NullText="Poišči dobavitelja...">
                                    <FocusedStyle CssClass="focus-text-box-input"></FocusedStyle>
                                    <ClientSideEvents KeyUp="txtSupplierSearch_KeyUp" KeyPress="StartSearchByKeyboardKey" />
                                </dx:ASPxTextBox>
                            </div>
                            <div class="col-3 p-0">
                                <dx:ASPxButton ID="ASPxButton1" runat="server" AutoPostBack="false" ClientInstanceName="btnSearch"
                                    Height="25" Width="50" UseSubmitBehavior="false" ClientEnabled="false">
                                    <Paddings Padding="0" />
                                    <Image Url="../../Images/search.png" UrlHottracked="../../Images/searchHover.png" UrlDisabled="../../Images/searchDisabled.png" />
                                    <ClientSideEvents Click="btnSearch_Click" />
                                </dx:ASPxButton>
                            </div>
                        </div>
                    </div>

                    <div class="col-lg-6 mb-2 mb-lg-0">
                        <div class="row m-0 align-items-center">
                            <div class="col-0 p-0 mr-2">
                                <dx:ASPxLabel ID="ASPxLabel12" runat="server" Text="NAZIV DOB. : " Font-Size="12px"></dx:ASPxLabel>
                            </div>
                            <div class="col p-0">
                                <dx:ASPxTextBox runat="server" ID="txtSupplierName" ClientInstanceName="txtSupplierName" MaxLength="200"
                                    CssClass="text-box-input" Font-Size="13px" Width="100%" AutoCompleteType="Disabled" BackColor="White" ClientEnabled="false">
                                    <FocusedStyle CssClass="focus-text-box-input"></FocusedStyle>
                                </dx:ASPxTextBox>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row m-0 pb-3">
                    <div class="col-lg-6 mb-2 mb-lg-0">
                        <div class="row m-0 align-items-center">
                            <div class="col-0 p-0" style="margin-right: 15px;">
                                <dx:ASPxLabel ID="ASPxLabel10" runat="server" Text="KONT. OSEBA : *" Font-Size="12px"></dx:ASPxLabel>
                            </div>
                            <div class="col-6 mr-3 p-0">
                                <dx:ASPxGridLookup ID="GridLookupKontaktnaOSeba" runat="server" ClientInstanceName="lookUpKontOseba"
                                    KeyFieldName="idKontaktneOsebe" TextFormatString="{0}" CssClass="text-box-input" SelectionMode="Multiple"
                                    Paddings-PaddingTop="0" Paddings-PaddingBottom="0" Width="100%" Font-Size="13px"
                                    OnLoad="ASPxGridLookupLoad_WidthLarge" OnDataBinding="GridLookupKontOseba_DataBinding"
                                    IncrementalFilteringMode="Contains">
                                    <ClientSideEvents Init="SetFocus" />
                                    <ClearButton DisplayMode="OnHover" />
                                    <FocusedStyle CssClass="focus-text-box-input"></FocusedStyle>
                                    <GridViewStyles>
                                        <Header CssClass="gridview-no-header-padding" ForeColor="Black"></Header>
                                    </GridViewStyles>
                                    <GridViewProperties>
                                        <SettingsBehavior EnableRowHotTrack="True" AllowEllipsisInText="true" AllowDragDrop="false" />
                                        <SettingsPager ShowSeparators="True" NumericButtonCount="3" EnableAdaptivity="true" />
                                        <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowVerticalScrollBar="True"
                                            ShowHorizontalScrollBar="true" VerticalScrollableHeight="200" AutoFilterCondition="Contains"></Settings>
                                    </GridViewProperties>
                                    <Columns>
                                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" />
                                        <dx:GridViewDataTextColumn Caption="Naziv" FieldName="NazivKontaktneOsebe" Width="70%"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Email" FieldName="Email" Width="30%"></dx:GridViewDataTextColumn>

                                    </Columns>
                                </dx:ASPxGridLookup>
                            </div>
                            <div class="col-3 p-0">
                                <dx:ASPxButton ID="ASPxButton2" runat="server" AutoPostBack="false" ClientInstanceName="btnAddContact"
                                    Height="25" Width="50" UseSubmitBehavior="false" ClientEnabled="true" ToolTip="Dodaj kontaktno osebo">
                                    <Paddings Padding="0" />
                                    <Image Url="../../Images/addContact.png" UrlHottracked="../../Images/addContact.png" UrlDisabled="../../Images/addContact.png" />
                                    <ClientSideEvents Click="btnAddContactPerson_Click" />
                                </dx:ASPxButton>
                            </div>
                        </div>
                    </div>

                    <div class="col-lg-6 mb-2 mb-lg-0">
                        <div class="row m-0 align-items-center justify-content-end">
                            <div class="col-0 p-0 mr-2">
                                <dx:ASPxLabel ID="ASPxLabel11" runat="server" Text="KUPEC VIDEN : *" Font-Size="12px"></dx:ASPxLabel>
                            </div>
                            <div class="col-0 p-0">
                                <dx:ASPxCheckBox ID="CheckBoxKupecViden" runat="server"></dx:ASPxCheckBox>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row m-0 pb-3">
                    <div class="col-lg-6 mb-2 mb-lg-0">
                        <div class="row m-0 align-items-center">
                            <div class="col-0 p-0" style="margin-right: 15px;">
                                <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="KATEGORIJA : *" Font-Size="12px"></dx:ASPxLabel>
                            </div>
                            <div class="col p-0">
                                <dx:ASPxGridLookup ID="GridLookupCategory" runat="server" ClientInstanceName="lookUpCategory"
                                    KeyFieldName="TempID" TextFormatString="{0}" CssClass="text-box-input"
                                    Paddings-PaddingTop="0" Paddings-PaddingBottom="0" Width="100%" Font-Size="13px"
                                    OnLoad="ASPxGridLookupLoad_WidthLarge" OnDataBinding="GridLookupCategory_DataBinding"
                                    IncrementalFilteringMode="Contains">
                                    <ClientSideEvents Init="SetFocus" />
                                    <ClearButton DisplayMode="OnHover" />
                                    <FocusedStyle CssClass="focus-text-box-input"></FocusedStyle>
                                    <GridViewStyles>
                                        <Header CssClass="gridview-no-header-padding" ForeColor="Black"></Header>
                                    </GridViewStyles>
                                    <GridViewProperties>
                                        <SettingsBehavior EnableRowHotTrack="True" AllowEllipsisInText="true" AllowDragDrop="false" />
                                        <SettingsPager ShowSeparators="True" NumericButtonCount="3" EnableAdaptivity="true" />
                                        <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowVerticalScrollBar="True"
                                            ShowHorizontalScrollBar="true" VerticalScrollableHeight="200" AutoFilterCondition="Contains"></Settings>
                                    </GridViewProperties>
                                    <Columns>

                                        <dx:GridViewDataTextColumn Caption="Naziv" FieldName="Naziv" Width="100%"></dx:GridViewDataTextColumn>

                                    </Columns>
                                </dx:ASPxGridLookup>
                            </div>
                        </div>
                    </div>

                    <div class="col-lg-6 mb-2 mb-lg-0">
                        <div class="row m-0 align-items-center">
                            <div class="col-0 p-0 mr-2">
                                <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="NAZIV : *" Font-Size="12px"></dx:ASPxLabel>
                            </div>
                            <div class="col p-0">
                                <dx:ASPxTextBox runat="server" ID="txtName" ClientInstanceName="txtName" MaxLength="200"
                                    CssClass="text-box-input" Font-Size="13px" Width="100%" AutoCompleteType="Disabled">
                                    <FocusedStyle CssClass="focus-text-box-input"></FocusedStyle>
                                </dx:ASPxTextBox>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row m-0 pb-3">
                    <div class="col-lg-6 mb-2 mb-lg-0">
                        <div class="row m-0 align-items-center">
                            <div class="col-0 p-0" style="margin-right: 20px;">
                                <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="KOLIČINA 1 : *" Font-Size="12px"></dx:ASPxLabel>
                            </div>
                            <div class="col-4 p-0">
                                <dx:ASPxTextBox runat="server" ID="txtQuantity1" ClientInstanceName="txtQuantity1" MaxLength="23"
                                    CssClass="text-box-input" Font-Size="13px" Width="100%" AutoCompleteType="Disabled">
                                    <FocusedStyle CssClass="focus-text-box-input"></FocusedStyle>
                                    <ClientSideEvents KeyPress="isNumberKey_decimal" />
                                </dx:ASPxTextBox>
                            </div>
                        </div>
                    </div>

                    <div class="col-lg-6 mb-2 mb-lg-0">
                        <div class="row m-0 align-items-center justify-content-end">
                            <div class="col-0 p-0 mr-2">
                                <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="ENOTA MERE 1 : *" Font-Size="12px"></dx:ASPxLabel>
                            </div>
                            <div class="col-3 p-0">
                                <dx:ASPxTextBox runat="server" ID="txtUnitOfMeasure1" ClientInstanceName="txtUnitOfMeasure1" MaxLength="30"
                                    CssClass="text-box-input" Font-Size="13px" Width="100%" AutoCompleteType="Disabled">
                                    <FocusedStyle CssClass="focus-text-box-input"></FocusedStyle>
                                </dx:ASPxTextBox>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row m-0 pb-3">
                    <div class="col-lg-6 mb-2 mb-lg-0">
                        <div class="row m-0 align-items-center">
                            <div class="col-0 p-0" style="margin-right: 28px;">
                                <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="KOLIČINA 2 : " Font-Size="12px"></dx:ASPxLabel>
                            </div>
                            <div class="col-4 p-0">
                                <dx:ASPxTextBox runat="server" ID="txtQuantity2" ClientInstanceName="txtQuantity2" MaxLength="23"
                                    CssClass="text-box-input" Font-Size="13px" Width="100%" AutoCompleteType="Disabled">
                                    <FocusedStyle CssClass="focus-text-box-input"></FocusedStyle>
                                    <ClientSideEvents KeyPress="isNumberKey_decimal" />
                                </dx:ASPxTextBox>
                            </div>
                        </div>
                    </div>

                    <div class="col-lg-6 mb-2 mb-lg-0">
                        <div class="row m-0 align-items-center justify-content-end">
                            <div class="col-0 p-0" style="margin-right: 17px">
                                <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="ENOTA MERE 2 : " Font-Size="12px"></dx:ASPxLabel>
                            </div>
                            <div class="col-3 p-0">
                                <dx:ASPxTextBox runat="server" ID="txtUnitOfMeasure2" ClientInstanceName="txtUnitOfMeasure2" MaxLength="30"
                                    CssClass="text-box-input" Font-Size="13px" Width="100%" AutoCompleteType="Disabled">
                                    <FocusedStyle CssClass="focus-text-box-input"></FocusedStyle>
                                </dx:ASPxTextBox>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row m-0 pb-3 align-items-center">
                    <div class="col-lg-8 mb-2 mb-lg-0">
                        <div class="row m-0 align-items-center">
                            <div class="col-0 p-0" style="margin-right: 43px;">
                                <dx:ASPxLabel ID="ASPxLabel8" runat="server" Text="OPOMBE : " Font-Size="12px"></dx:ASPxLabel>
                            </div>
                            <div class="col p-0">
                                <dx:ASPxMemo ID="MemoNotes" runat="server" Width="100%" Rows="2" MaxLength="3000" CssClass="text-box-input" AutoCompleteType="Disabled">
                                    <FocusedStyle CssClass="focus-text-box-input"></FocusedStyle>
                                </dx:ASPxMemo>
                            </div>
                        </div>
                    </div>

                    <div class="col-lg-4 mb-2 mb-lg-0">
                        <div class="row m-0 align-items-center">
                            <div class="col p-0 text-right">
                                <dx:ASPxButton ID="btnAddProduct" runat="server" AutoPostBack="false" ClientInstanceName="btnAddProduct"
                                    Height="40" Width="110" UseSubmitBehavior="false" Text="V izbor">
                                    <Paddings PaddingLeft="10" PaddingRight="10" />
                                    <Paddings Padding="0" />
                                    <ClientSideEvents Click="btnAddArtikel_Click" />
                                </dx:ASPxButton>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row m-0 pb-3">
                    <div class="col-lg-4 mb-lg-0 ">
                        <div class="row m-0 align-items-center">
                            <div class="col-0 p-0" style="margin-right: 15px;">
                                <dx:ASPxLabel ID="ASPxLabel7" runat="server" Text="SPOROČILO : " Font-Size="12px"></dx:ASPxLabel>
                            </div>
                            <div class="col-12 p-0">
                                
                            </div>
                        </div>
                    </div>

                    <div class="col-lg-8 p-0">
                        <div class="row m-0 align-items-center">
                            <div class="row m-0 align-items-center">
                                <div class="col-0 p-0" style="margin-right: 15px;">
                                    <dx:ASPxLabel ID="ASPxLabel13" runat="server" Text="ARTIKLI : " Font-Size="12px"></dx:ASPxLabel>
                                </div>
                                <div class="col-12 p-0">
                                    <dx:ASPxGridView ID="ASPxGridViewSelectedArtikel" runat="server" EnableCallbackCompression="true" ClientInstanceName="gridSelectedArtikel"
                                        OnDataBinding="ASPxGridViewSelectedArtikel_DataBinding" Width="100%" OnBatchUpdate="ASPxGridViewSelectedArtikel_BatchUpdate"
                                        KeyFieldName="PovprasevanjePozicijaArtikelID" CssClass="gridview-no-header-padding">
                                        <Paddings Padding="0" />
                                        <Settings ShowVerticalScrollBar="True"
                                            ShowFilterBar="Auto" ShowFilterRow="false" VerticalScrollableHeight="300"
                                            ShowFilterRowMenu="false" VerticalScrollBarStyle="Standard" VerticalScrollBarMode="Auto" />
                                        <SettingsPager PageSize="50" ShowNumericButtons="false">
                                            <PageSizeItemSettings Visible="false" Items="50,80,100" Caption="Zapisi na stran : " AllItemText="Vsi">
                                            </PageSizeItemSettings>
                                            <Summary Visible="true" Text="Vseh zapisov : {2}" EmptyText="Ni zapisov" />
                                        </SettingsPager>
                                        <SettingsBehavior AllowFocusedRow="true" />
                                        <Styles Header-Wrap="True">
                                            <Header Paddings-PaddingTop="5" HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="true"></Header>
                                            <FocusedRow BackColor="#d1e6fe" Font-Bold="true" ForeColor="#606060"></FocusedRow>
                                        </Styles>
                                        <SettingsText EmptyDataRow="Trenutno ni podatka o Artikel. Dodaj novega."
                                            CommandBatchEditUpdate="Spremeni" CommandBatchEditCancel="Prekliči" />
                                        <SettingsEditing Mode="Batch" BatchEditSettings-StartEditAction="DblClick" />
                                        <SettingsBehavior AllowEllipsisInText="true" />
                                        <SettingsCommandButton>
                                            <DeleteButton Text="Izbriši"></DeleteButton>
                                            <RecoverButton Text="Povrni"></RecoverButton>
                                        </SettingsCommandButton>
                                        <Columns>

                                            <%--<dx:GridViewCommandColumn ShowSelectCheckbox="true" Width="90px" SelectAllCheckboxMode="None" Caption="Izberi" ShowClearFilterButton="true" />--%>

                                            <dx:GridViewCommandColumn Caption="Briši"
                                                ShowDeleteButton="true" Width="5%" VisibleIndex="0" />

                                            <dx:GridViewDataTextColumn Caption="ID" FieldName="PovprasevanjePozicijaArtikelID" Visible="false" SortOrder="Descending" EditFormSettings-Visible="False" />

                                            <dx:GridViewDataTextColumn Caption="Kategorija" FieldName="KategorijaNaziv" Width="7%" EditFormSettings-Visible="False" />

                                            <dx:GridViewDataTextColumn Caption="Artikel" FieldName="Naziv" Width="30%" />

                                            <dx:GridViewDataTextColumn Caption="Kol. 1" FieldName="Kolicina1" Width="7%" EditFormSettings-Visible="true" />

                                            <dx:GridViewDataTextColumn Caption="EM 1" FieldName="EnotaMere1" Width="5%" EditFormSettings-Visible="true" />

                                            <dx:GridViewDataTextColumn Caption="Kol. 2" FieldName="Kolicina2" Width="7%" EditFormSettings-Visible="true" />

                                            <dx:GridViewDataTextColumn Caption="EM 2" FieldName="EnotaMere2" Width="5%" EditFormSettings-Visible="true" />

                                            <dx:GridViewDataTextColumn Caption="Opomba poz." FieldName="OpombaNarocilnica" Width="18%" EditFormSettings-Visible="true" />

                                        </Columns>
                                    </dx:ASPxGridView>
                                </div>

                            </div>
                        </div>
                        <dx:ASPxPopupControl ID="PopupControlSearchSupplier" runat="server" ContentUrl="SearchSupplier_popup.aspx"
                            ClientInstanceName="PopupControlSearchSupplier" Modal="True" HeaderText="DOBAVITELJ"
                            CloseAction="CloseButton" Width="800px" Height="635px" PopupHorizontalAlign="WindowCenter"
                            PopupVerticalAlign="WindowCenter" PopupAnimationType="Fade" AllowDragging="true" ShowSizeGrip="true"
                            AllowResize="true" ShowShadow="true"
                            OnWindowCallback="PopupControlSearchSupplier_WindowCallback">
                            <ClientSideEvents CloseButtonClick="OnPopupCloseButtonClick" />
                            <ContentStyle BackColor="#F7F7F7">
                                <Paddings PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px" PaddingTop="0px"></Paddings>
                            </ContentStyle>
                        </dx:ASPxPopupControl>

                        <dx:ASPxPopupControl ID="PopupControlAddContactPerson" runat="server" ContentUrl="../Client/ContactPerson_popup.aspx"
                            ClientInstanceName="PopupControlAddContactPerson" Modal="True" HeaderText="Kontaktna oseba"
                            CloseAction="CloseButton" Width="1100px" Height="535px" PopupHorizontalAlign="WindowCenter"
                            PopupVerticalAlign="WindowCenter" PopupAnimationType="Fade" AllowDragging="true" ShowSizeGrip="true"
                            AllowResize="true" ShowShadow="true"
                            OnWindowCallback="PopupControlAddContactPerson_WindowCallback">
                            <ClientSideEvents CloseButtonClick="OnPopupCloseButtonClick" />
                            <ContentStyle BackColor="#F7F7F7">
                                <Paddings PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px" PaddingTop="0px"></Paddings>
                            </ContentStyle>
                        </dx:ASPxPopupControl>
                    </div>
                </div>

                <div class="row m-0 pb-3 ">
                     <div class="col-lg-6 mb-2 mb-lg-0">
                        <div class="row m-0 align-items-center justify-content-end">
                            <div class="col-0 p-0 pr-2">
                                <dx:ASPxButton ID="btnClear" runat="server" Text="Odstrani vse" AutoPostBack="false"
                                    Height="25" Width="110" UseSubmitBehavior="false" OnClick="btnClear_Click">
                                    <Paddings PaddingLeft="10" PaddingRight="10" />
                                    <Image Url="../../Images/trashPopUp.png" />
                                </dx:ASPxButton>
                            </div>
                           
                        </div>
                    </div>
                    <div class="col-lg-6 mb-2 mb-lg-0">
                        <div class="row m-0 align-items-center justify-content-end">
                            <div class="col-0 p-0 pr-2">
                                <dx:ASPxButton ID="btnCancel" runat="server" Text="Prekliči" AutoPostBack="false"
                                    Height="25" Width="110" UseSubmitBehavior="false" OnClick="btnCancel_Click">
                                    <Paddings PaddingLeft="10" PaddingRight="10" />
                                    <Image Url="../../Images/cancelPopup.png" />
                                </dx:ASPxButton>
                            </div>
                            <div class="col-0 p-0">
                                <dx:ASPxButton ID="btnConfirm" runat="server" Text="Potrdi" AutoPostBack="false"
                                    Height="25" Width="110" UseSubmitBehavior="false" OnClick="btnConfirm_Click" ClientInstanceName="btnConfirm">
                                    <Paddings PaddingLeft="10" PaddingRight="10" />
                                    <Image Url="../../Images/addPopup.png" />
                                    <ClientSideEvents Click="ActionButton_Click" />
                                </dx:ASPxButton>
                            </div>
                        </div>
                    </div>
                </div>

            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxCallbackPanel>
</asp:Content>

