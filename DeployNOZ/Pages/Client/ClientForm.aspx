<%@ Page Title="Dobavitelji, kupci" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ClientForm.aspx.cs" Inherits="GrafolitNOZ.Pages.Client.ClientForm" %>

<%@ MasterType VirtualPath="~/Main.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContentHolder" runat="server">
    <script>
        function CheckFieldValidation(s, e) {
            var process = false;

            var inputItems = [clientTxtNaziv];
            var comboBoxItems = [clientComboBoxZaposleni, clientComboBoxTip/*, clientComboBoxJezik*/]
            process = InputFieldsValidation(null, inputItems, null, null, comboBoxItems, null);

            if (clientBtnConfirm.GetText() == 'Izbriši')
                process = true;

            if (process)
                e.processOnServer = true;
            else
                e.processOnServer = false;

            return process;
        }

        /*function ComboBoxTip_ValueChanged(s, e)
        {
            var isvisible = false;
            if (s.GetText() == "Prevoznik")
            {
                isvisible = true;
            }

            clientLblTransportType.SetVisible(isvisible);
            clientComboBoxTipPrevoza.SetVisible(isvisible);
        }*/

        function CallbackPanelContactPerson_EndCallback(s, e) {
            LoadingPanel.Hide();
        }

        function HandleUserAction(s, e) {

            var process = false;

            var inputItems = [clientTxtNaziv];
            var comboBoxItems = [clientComboBoxZaposleni, clientComboBoxTip/*, clientComboBoxJezik*/]
            process = InputFieldsValidation(null, inputItems, null, null, comboBoxItems, null);
            if (process) {
                var result = HandleUserActionsOnTabs(gridContactPerson, clientBtnAdd, clientBtnEdit, clientBtnDelete, s);
                CallbackPanelContactPerson.PerformCallback(result);
                LoadingPanel.Show();
            }
            else
                $('#tab-basic a[href="#basic"]').tab('show');
        }

        function OnClosePopUpHandler(command, sender) {
            switch (command) {
                case 'Potrdi':
                    switch (sender) {
                        case 'ContactPerson':
                            PopupControlContactPerson.Hide();
                            gridContactPerson.Refresh();
                            break;
                    }
                    break;
                case 'Preklici':
                    switch (sender) {
                        case 'ContactPerson':
                            PopupControlContactPerson.Hide();
                            break;
                    }
                    break;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <div class="small-padding-top">
        <ul class="nav nav-tabs">
            <li class="nav-item" id="tab-basic"><a data-toggle="tab" class="nav-link active" href="#basic"><span class="glyphicon glyphicon-import"></span>Osnovni podatki</a></li>
            <li class="nav-item d-none" id="tab-contact"><a data-toggle="tab" class="nav-link" href="#contact"><span class="badge" runat="server" id="contactBadge">0</span> Kontaktne osebe</a></li>
        </ul>
        <div class="tab-content border mb-3">
            <div id="basic" class="container-fluid p-0 tab-pane active">
                <div class="card" style="border-top: none; background-color: transparent;">
                    <div class="card-body">

                        <div class="row m-0 pb-3">
                            <div class="col-lg-12 mb-2 mb-lg-0">
                                <div class="row m-0 align-items-center">
                                    <div class="col-0 p-0" style="margin-right: 28px">
                                        <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="NAZIV PRVI : "></dx:ASPxLabel>
                                    </div>
                                    <div class="col p-0">
                                        <dx:ASPxTextBox runat="server" ID="txtNazivPrvi" ClientInstanceName="clientTxtNaziv"
                                            CssClass="text-box-input" Font-Size="13px" Width="100%" AutoCompleteType="Disabled">
                                            <FocusedStyle CssClass="focus-text-box-input"></FocusedStyle>
                                            <ClientSideEvents Init="SetFocus" />
                                        </dx:ASPxTextBox>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row m-0 pb-3">
                            <div class="col-lg-12">
                                <div class="row m-0 align-items-center">
                                    <div class="col-0 p-0 mr-3">
                                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="NAZIV DRUGI : "></dx:ASPxLabel>
                                    </div>
                                    <div class="col p-0">
                                        <dx:ASPxTextBox runat="server" ID="txtNazivDrugi"
                                            CssClass="text-box-input" Font-Size="13px" Width="100%" AutoCompleteType="Disabled">
                                            <FocusedStyle CssClass="focus-text-box-input"></FocusedStyle>
                                        </dx:ASPxTextBox>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row m-0 pb-3">
                            <div class="col-lg-4">
                                <div class="row m-0 align-items-center">
                                    <div class="col-0 p-0" style="margin-right: 48px;">
                                        <dx:ASPxLabel ID="ASPxLabel8" runat="server" Text="NASLOV : "></dx:ASPxLabel>
                                    </div>
                                    <div class="col p-0">
                                        <dx:ASPxTextBox runat="server" ID="txtNaslov"
                                            CssClass="text-box-input" Font-Size="13px" Width="100%" AutoCompleteType="Disabled">
                                            <FocusedStyle CssClass="focus-text-box-input"></FocusedStyle>
                                        </dx:ASPxTextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-4">
                                <div class="row m-0 align-items-center">
                                    <div class="col-0 p-0 mr-3">
                                        <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="ŠTEV. POŠTE : "></dx:ASPxLabel>
                                    </div>
                                    <div class="col p-0">
                                        <dx:ASPxTextBox runat="server" ID="txtStevPoste"
                                            CssClass="text-box-input" Font-Size="13px" Width="100%" AutoCompleteType="Disabled">
                                            <FocusedStyle CssClass="focus-text-box-input"></FocusedStyle>
                                        </dx:ASPxTextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-4">
                                <div class="row m-0 align-items-center">
                                    <div class="col-0 p-0 mr-3">
                                        <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="NAZIV POŠTE : "></dx:ASPxLabel>
                                    </div>
                                    <div class="col p-0">
                                        <dx:ASPxTextBox runat="server" ID="txtNazivPoste"
                                            CssClass="text-box-input" Font-Size="13px" Width="100%" AutoCompleteType="Disabled">
                                            <FocusedStyle CssClass="focus-text-box-input"></FocusedStyle>
                                        </dx:ASPxTextBox>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row m-0 pb-3">
                            <div class="col-lg-4">
                                <div class="row m-0 align-items-center">
                                    <div class="col-0 p-0" style="margin-right: 44px;">
                                        <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="TELEFON : "></dx:ASPxLabel>
                                    </div>
                                    <div class="col p-0">
                                        <dx:ASPxTextBox runat="server" ID="txtTelefon"
                                            CssClass="text-box-input" Font-Size="13px" Width="100%" AutoCompleteType="Disabled">
                                            <FocusedStyle CssClass="focus-text-box-input"></FocusedStyle>
                                        </dx:ASPxTextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-4">
                                <div class="row m-0 align-items-center">
                                    <div class="col-0 p-0" style="margin-right: 70px;">
                                        <dx:ASPxLabel ID="ASPxLabel7" runat="server" Text="FAX : "></dx:ASPxLabel>
                                    </div>
                                    <div class="col p-0">
                                        <dx:ASPxTextBox runat="server" ID="txtFAX"
                                            CssClass="text-box-input" Font-Size="13px" Width="100%" AutoCompleteType="Disabled">
                                            <FocusedStyle CssClass="focus-text-box-input"></FocusedStyle>
                                        </dx:ASPxTextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-4">
                                <div class="row m-0 align-items-center">
                                    <div class="col-0 p-0" style="margin-right: 60px;">
                                        <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="EMAIL : "></dx:ASPxLabel>
                                    </div>
                                    <div class="col p-0">
                                        <dx:ASPxTextBox runat="server" ID="txtEmail"
                                            CssClass="text-box-input" Font-Size="13px" Width="100%" AutoCompleteType="Disabled">
                                            <FocusedStyle CssClass="focus-text-box-input"></FocusedStyle>
                                        </dx:ASPxTextBox>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row m-0 pb-3">
                            <div class="col-lg-4">
                                <div class="row m-0 pb-3 align-items-center">
                                    <div class="col-0 p-0" style="margin-right: 80px;">
                                        <dx:ASPxLabel ID="ASPxLabel9" runat="server" Text="TIP : "></dx:ASPxLabel>
                                    </div>
                                    <div class="col p-0">
                                        <dx:ASPxComboBox ID="ComboBoxTip" runat="server" ValueType="System.String" DropDownStyle="DropDownList"
                                            IncrementalFilteringMode="StartsWith" TextField="Naziv" ValueField="TipStrankaID"
                                            EnableSynchronization="False" Width="100%" OnDataBinding="ComboBoxTip_DataBinding"
                                            Font-Size="14px" Font-Names="Segoe UI" CssClass="text-box-input" ClientInstanceName="clientComboBoxTip">
                                            <ClearButton DisplayMode="OnHover" />
                                            <FocusedStyle CssClass="focus-text-box-input"></FocusedStyle>
                                            <%--<ClientSideEvents ValueChanged="ComboBoxTip_ValueChanged" />--%>
                                        </dx:ASPxComboBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-4">
                                <div class="row m-0 align-items-center" style="align-items: center">
                                    <div class="col-0 p-0" style="margin-right: 40px;">
                                        <dx:ASPxLabel ID="ASPxLabel10" runat="server" Text="SKRBNIK : "></dx:ASPxLabel>
                                    </div>
                                    <div class="col p-0">
                                        <dx:ASPxComboBox ID="ComboBoxSkrbnik" runat="server" ValueType="System.String" DropDownStyle="DropDownList"
                                            IncrementalFilteringMode="StartsWith" TextField="CelotnoIme" ValueField="idOsebe"
                                            EnableSynchronization="False" Width="100%" OnDataBinding="ComboBoxSkrbnik_DataBinding"
                                            Font-Size="14px" Font-Names="Segoe UI" CssClass="text-box-input" ClientInstanceName="clientComboBoxZaposleni">
                                            <ClearButton DisplayMode="OnHover" />
                                            <FocusedStyle CssClass="focus-text-box-input"></FocusedStyle>
                                        </dx:ASPxComboBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-4 d-none">
                                <div class="row m-0 align-items-center" style="align-items: center">
                                    <div class="col-0 p-0" style="margin-right: 40px;">
                                        <dx:ASPxLabel ID="ASPxLabel11" runat="server" Text="JEZIK : "></dx:ASPxLabel>
                                    </div>
                                    <div class="col p-0">
                                        <dx:ASPxComboBox ID="ComboBoxJezik" runat="server" ValueType="System.String" DropDownStyle="DropDownList"
                                            IncrementalFilteringMode="StartsWith" TextField="Naziv" ValueField="JezikID"
                                            EnableSynchronization="False" Width="100%" OnDataBinding="ComboBoxJezik_DataBinding"
                                            Font-Size="14px" Font-Names="Segoe UI" CssClass="text-box-input" ClientInstanceName="clientComboBoxJezik">
                                            <ClearButton DisplayMode="OnHover" />
                                            <FocusedStyle CssClass="focus-text-box-input"></FocusedStyle>
                                        </dx:ASPxComboBox>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="AddEditButtonsWrap medium-margin-l medium-margin-r">
                            <span class="AddEditButtons">
                                <dx:ASPxButton Theme="Moderno" ID="btnConfirm" runat="server" Text="Shrani" AutoPostBack="false"
                                    Height="25" Width="110" ClientInstanceName="clientBtnConfirm" UseSubmitBehavior="false"
                                    OnClick="btnConfirm_Click">
                                    <Paddings PaddingLeft="10" PaddingRight="10" />
                                    <Image Url="../../../Images/add.png" UrlHottracked="../../Images/addHover.png" />
                                    <ClientSideEvents Click="CheckFieldValidation" />
                                </dx:ASPxButton>
                            </span>
                            <span class="AddEditButtons">
                                <dx:ASPxButton Theme="Moderno" ID="btnCancel" runat="server" Text="Prekliči" AutoPostBack="false"
                                    Height="25" Width="110" UseSubmitBehavior="false" OnClick="btnCancel_Click">
                                    <Paddings PaddingLeft="10" PaddingRight="10" />
                                    <Image Url="../../../Images/add.png" UrlHottracked="../../Images/addHover.png" />
                                </dx:ASPxButton>
                            </span>
                        </div>

                    </div>
                </div>
            </div>

            <div id="contact" class="tab-pane fade">
                <div class="card" style="border-top: none; background-color: transparent;">
                    <div class="card-body">
                        <dx:ASPxCallbackPanel ID="CallbackPanelContactPerson" runat="server" OnCallback="CallbackPanelContactPerson_Callback" ClientInstanceName="CallbackPanelContactPerson">
                            <SettingsLoadingPanel Enabled="false" />
                            <ClientSideEvents EndCallback="CallbackPanelContactPerson_EndCallback" />
                            <PanelCollection>
                                <dx:PanelContent>
                                    <dx:ASPxGridView ID="ASPxGridViewContactPerson" runat="server" EnableCallbackCompression="true" ClientInstanceName="gridContactPerson"
                                        Width="100%" KeyboardSupport="true" AccessKey="G" OnDataBinding="ASPxGridViewContactPerson_DataBinding"
                                        KeyFieldName="idKontaktneOsebe" CssClass="gridview-no-header-padding" OnDataBound="ASPxGridViewContactPerson_DataBound">
                                        <ClientSideEvents RowDblClick="HandleUserAction" />
                                        <Paddings Padding="0" />
                                        <Settings ShowVerticalScrollBar="True"
                                            ShowFilterBar="Auto" ShowFilterRow="True" VerticalScrollableHeight="400"
                                            ShowFilterRowMenu="True" VerticalScrollBarStyle="Standard" VerticalScrollBarMode="Auto" />
                                        <SettingsPager PageSize="100" ShowNumericButtons="false" AlwaysShowPager="false">
                                            <PageSizeItemSettings Visible="true" Items="200,300" Caption="Zapisi na stran : " AllItemText="Vsi">
                                            </PageSizeItemSettings>
                                            <Summary Visible="true" Text="Vseh zapisov : {2}" EmptyText="Ni zapisov" />
                                        </SettingsPager>
                                        <SettingsBehavior AllowFocusedRow="true" />
                                        <Styles Header-Wrap="True">
                                            <Header Paddings-PaddingTop="5" HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="true"></Header>
                                            <FocusedRow BackColor="#d1e6fe" Font-Bold="true" ForeColor="#606060"></FocusedRow>
                                        </Styles>
                                        <SettingsText EmptyDataRow="Trenutno ni podatka o strankah. Dodaj novega." />
                                        <Columns>
                                            <dx:GridViewDataTextColumn Caption="ID" FieldName="idKontaktneOsebe" Width="80px" SortOrder="Descending" Visible="false">
                                            </dx:GridViewDataTextColumn>

                                            <dx:GridViewDataTextColumn Caption="Naziv" FieldName="NazivKontaktneOsebe" Width="40%">
                                            </dx:GridViewDataTextColumn>

                                            <dx:GridViewDataTextColumn Caption="Telefon" FieldName="Telefon" Width="20%">
                                            </dx:GridViewDataTextColumn>

                                            <dx:GridViewDataTextColumn Caption="GSM" FieldName="GSM" Width="20%">
                                            </dx:GridViewDataTextColumn>

                                            <dx:GridViewDataTextColumn Caption="Email"
                                                FieldName="Email" Width="20%">
                                                <Settings AllowAutoFilter="True" AutoFilterCondition="Contains" />
                                            </dx:GridViewDataTextColumn>

                                        </Columns>
                                    </dx:ASPxGridView>

                                    <dx:ASPxPopupControl ID="PopupControlContactPerson" runat="server" ContentUrl="ContactPerson_popup.aspx"
                                        ClientInstanceName="PopupControlContactPerson" Modal="True" HeaderText="KONTAKTNA OSEBA"
                                        CloseAction="CloseButton" Width="1100px" Height="500px" PopupHorizontalAlign="WindowCenter"
                                        PopupVerticalAlign="WindowCenter" PopupAnimationType="Fade" AllowDragging="true" ShowSizeGrip="true"
                                        AllowResize="true" ShowShadow="true"
                                        OnWindowCallback="PopupControlContactPerson_WindowCallback">
                                        <ClientSideEvents CloseButtonClick="OnPopupCloseButtonClick" />
                                        <ContentStyle BackColor="#F7F7F7">
                                            <Paddings PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px" PaddingTop="0px"></Paddings>
                                        </ContentStyle>
                                    </dx:ASPxPopupControl>

                                    <div class="AddEditButtonsWrap medium-margin-l medium-margin-r">
                                        <div class="DeleteButtonElements">
                                            <span class="AddEditButtons">
                                                <dx:ASPxButton Theme="Moderno" ID="btnDelete" runat="server" Text="Izbriši" AutoPostBack="false"
                                                    Height="25" Width="50" ClientInstanceName="clientBtnDelete">
                                                    <Paddings PaddingLeft="10" PaddingRight="10" />
                                                    <Image Url="../../../Images/trash.png" UrlHottracked="../../Images/trashHover.png" />
                                                    <ClientSideEvents Click="HandleUserAction" />
                                                </dx:ASPxButton>
                                            </span>
                                        </div>
                                        <div class="AddEditButtonsElements">
                                            <span class="AddEditButtons">
                                                <dx:ASPxButton Theme="Moderno" ID="btnAdd" runat="server" Text="Dodaj" AutoPostBack="false"
                                                    Height="25" Width="90" ClientInstanceName="clientBtnAdd">
                                                    <Paddings PaddingLeft="10" PaddingRight="10" />
                                                    <Image Url="../../../Images/add.png" UrlHottracked="../../Images/addHover.png" />
                                                    <ClientSideEvents Click="HandleUserAction" />
                                                </dx:ASPxButton>
                                            </span>
                                            <span class="AddEditButtons">
                                                <dx:ASPxButton Theme="Moderno" ID="btnEdit" runat="server" Text="Spremeni" AutoPostBack="false"
                                                    Height="25" Width="90" ClientInstanceName="clientBtnEdit">
                                                    <Paddings PaddingLeft="10" PaddingRight="10" />
                                                    <Image Url="../../../Images/edit.png" UrlHottracked="../../Images/editHover.png" />
                                                    <ClientSideEvents Click="HandleUserAction" />
                                                </dx:ASPxButton>
                                            </span>
                                        </div>
                                    </div>
                                </dx:PanelContent>
                            </PanelCollection>
                        </dx:ASPxCallbackPanel>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
