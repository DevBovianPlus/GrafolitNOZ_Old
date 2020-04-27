<%@ Page Title="" Language="C#" MasterPageFile="~/Popup.Master" AutoEventWireup="true" CodeBehind="Employee_popup.aspx.cs" Inherits="GrafolitNOZ.Pages.Employee.Employee_popup" %>

<%@ Register Assembly="DevExpress.Web.v19.2, Version=19.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>


<%@ MasterType VirtualPath="~/Popup.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var hasStartEditing = false;
        var postbackInitiated = false;

        function CheckFieldValidation() {
            var process = false;
            var lookUpItems = [lookUpSupervisor, lookUpRole, lookUpPantheonUsers];

            var inputItems = [txtFirstname, txtLastname, txtEmail];
            var dateItems = null;

            if (btnConfirm.GetText() == 'Izbriši')
                process = true;
            else
                process = InputFieldsValidation(lookUpItems, inputItems, dateItems, /*memoItems*/ null, /*comboBoxItems*/null, null);

            return process;
        }

        function ActionButton_Click(s, e) {
            var process = CheckFieldValidation();

            if (process) {
                e.processOnServer = !postbackInitiated;
                postbackInitiated = true;
            }
            else
                e.processOnServer = false;
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <ul class="nav nav-tabs">
        <li class="nav-item">
            <a class="nav-link active" data-toggle="tab" href="#Basic">Osnovno</a>
        </li>
        <li class="nav-item" runat="server" id="employeeEmailSettings">
            <a class="nav-link" data-toggle="tab" href="#EmailSettings">Email</a>
        </li>
        <li class="nav-item" runat="server" id="employeeCredentials">
            <a class="nav-link" data-toggle="tab" href="#Credentials">NOZ - dostop</a>
        </li>
    </ul>

    <div class="tab-content border mb-3">
        <div id="Basic" class="container-fluid p-0 tab-pane active">

            <div class="row m-0 pb-3 pt-3">
                <div class="col-lg-6 mb-2 mb-lg-0">
                    <div class="row m-0 align-items-center">
                        <div class="col-0 p-0" style="margin-right: 50px;">
                            <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="IME : *" Font-Size="12px"></dx:ASPxLabel>
                        </div>
                        <div class="col p-0">
                            <dx:ASPxTextBox runat="server" ID="txtFirstname" ClientInstanceName="txtFirstname" MaxLength="200"
                                CssClass="text-box-input" Font-Size="13px" Width="100%" AutoCompleteType="Disabled">
                                <FocusedStyle CssClass="focus-text-box-input"></FocusedStyle>
                            </dx:ASPxTextBox>
                        </div>
                    </div>
                </div>

                <div class="col-lg-6 mb-2 mb-lg-0">
                    <div class="row m-0 align-items-center justify-content-end">
                        <div class="col-0 p-0 mr-3">
                            <dx:ASPxLabel ID="ASPxLabel7" runat="server" Text="PRIIMEK : *" Font-Size="12px"></dx:ASPxLabel>
                        </div>
                        <div class="col p-0">
                            <dx:ASPxTextBox runat="server" ID="txtLastname" ClientInstanceName="txtLastname" MaxLength="200"
                                CssClass="text-box-input" Font-Size="13px" Width="100%" AutoCompleteType="Disabled">
                                <FocusedStyle CssClass="focus-text-box-input"></FocusedStyle>
                            </dx:ASPxTextBox>
                        </div>
                    </div>
                </div>
            </div>

             <div class="row m-0 pb-3 pt-3">
                <div class="col-lg-12 mb-2 mb-lg-0">
                    <div class="row m-0 align-items-center">
                        <div class="col-0 p-0" style="margin-right: 37px;">
                            <dx:ASPxLabel ID="ASPxLabel18" runat="server" Text="Pantheon uporabnik :*" Font-Size="12px"></dx:ASPxLabel>
                        </div>
                        <div class="col p-0">
                           <dx:ASPxGridLookup ID="GridLookupPantheonUsers" runat="server" ClientInstanceName="lookUpPantheonUsers"
                                 KeyFieldName="TempID" TextFormatString="{0}" CssClass="text-box-input"
                                    Paddings-PaddingTop="0" Paddings-PaddingBottom="0" Width="100%" Font-Size="13px"
                                    OnLoad="ASPxGridLookupLoad_WidthLarge" OnDataBinding="GridLookupPantheonUsers_DataBinding"
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
                                        <dx:GridViewDataTextColumn Caption="User ID " FieldName="acUserId" Width="30%"></dx:GridViewDataTextColumn>     
                                        <dx:GridViewDataTextColumn Caption="Uporabnik" FieldName="acSubject" Width="70%"></dx:GridViewDataTextColumn>                                                                           
                                    </Columns>
                            </dx:ASPxGridLookup>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row m-0 pb-3 pt-3">
                <div class="col-lg-12 mb-2 mb-lg-0">
                    <div class="row m-0 align-items-center">
                        <div class="col-0 p-0" style="margin-right: 37px;">
                            <dx:ASPxLabel ID="ASPxLabel9" runat="server" Text="NASLOV : " Font-Size="12px"></dx:ASPxLabel>
                        </div>
                        <div class="col p-0">
                            <dx:ASPxTextBox runat="server" ID="txtAddress" ClientInstanceName="txtAddress" MaxLength="200"
                                CssClass="text-box-input" Font-Size="13px" Width="100%" AutoCompleteType="Disabled">
                                <FocusedStyle CssClass="focus-text-box-input"></FocusedStyle>
                            </dx:ASPxTextBox>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row m-0 pb-3">
                <div class="col-lg-6 mb-2 mb-lg-0 ">
                    <div class="row m-0 align-items-center">
                        <div class="col-0 p-0" style="margin-right: 15px;">
                            <dx:ASPxLabel ID="ASPxLabel10" runat="server" Font-Size="12px" Text="DATUM ROJ. : "></dx:ASPxLabel>
                        </div>
                        <div class="col-4 p-0">
                            <dx:ASPxDateEdit ID="DateEditBirthDate" runat="server" EditFormat="Date" Width="100%"
                                CssClass="text-box-input date-edit-padding" Font-Size="13px"
                                ClientInstanceName="DateEditBirthDate">
                                <FocusedStyle CssClass="focus-text-box-input" />
                                <CalendarProperties TodayButtonText="Danes" ClearButtonText="Izbriši" />
                                <DropDownButton Visible="true"></DropDownButton>
                            </dx:ASPxDateEdit>
                        </div>
                    </div>
                </div>

                <div class="col-lg-6 mb-2 mb-lg-0">
                    <div class="row m-0 align-items-center justify-content-end">
                        <div class="col-0 p-0" style="margin-right: 33px;">
                            <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="TELEFON : " Font-Size="12px"></dx:ASPxLabel>
                        </div>
                        <div class="col-6 p-0">
                            <dx:ASPxTextBox runat="server" ID="txtPhone" ClientInstanceName="txtPhone" MaxLength="40"
                                CssClass="text-box-input" Font-Size="13px" Width="100%" AutoCompleteType="Disabled">
                                <FocusedStyle CssClass="focus-text-box-input"></FocusedStyle>
                                <ClientSideEvents KeyPress="isNumberKey_int" />
                            </dx:ASPxTextBox>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row m-0 pb-3">
                <div class="col-lg-6 mb-2 mb-lg-0">
                    <div class="row m-0 align-items-center">
                        <div class="col-0 p-0" style="margin-right: 37px;">
                            <dx:ASPxLabel ID="ASPxLabel16" runat="server" Text="PODPIS : " Font-Size="12px"></dx:ASPxLabel>
                        </div>
                        <div class="col p-0">
                            <dx:ASPxTextBox runat="server" ID="txtSignature" ClientInstanceName="txtSignature" MaxLength="200"
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
                        <div class="col-0 p-0" style="margin-right: 15px;">
                            <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="NADREJENI : *" Font-Size="12px"></dx:ASPxLabel>
                        </div>
                        <div class="col-7 p-0">
                            <dx:ASPxGridLookup ID="GridLookupSupervisor" runat="server" ClientInstanceName="lookUpSupervisor"
                                KeyFieldName="idOsebe" TextFormatString="{0} {1}" CssClass="text-box-input"
                                Paddings-PaddingTop="0" Paddings-PaddingBottom="0" Width="100%" Font-Size="13px" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                                OnLoad="ASPxGridLookupLoad_WidthLarge" OnDataBinding="GridLookupSupervisor_DataBinding"
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

                                    <dx:GridViewDataTextColumn Caption="Ime" FieldName="Ime" Width="30%"></dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Priimek" FieldName="Priimek" Width="30%"></dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Email" FieldName="Email" Width="20%"></dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Telefon" FieldName="TelefonGSM" Width="20%"></dx:GridViewDataTextColumn>

                                </Columns>
                            </dx:ASPxGridLookup>
                        </div>
                    </div>
                </div>

                <div class="col-lg-6 mb-2 mb-lg-0">
                    <div class="row m-0 mb-2 align-items-center justify-content-end">
                        <div class="col-0 p-0" style="margin-right: 22px;">
                            <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="VLOGA : *" Font-Size="12px"></dx:ASPxLabel>
                        </div>
                        <div class="col-5 p-0">
                            <dx:ASPxGridLookup ID="GridLookupRole" runat="server" ClientInstanceName="lookUpRole"
                                KeyFieldName="idVloga" TextFormatString="{0}" CssClass="text-box-input"
                                Paddings-PaddingTop="0" Paddings-PaddingBottom="0" Width="100%" Font-Size="13px" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                                OnLoad="ASPxGridLookupLoad_WidthSmall" OnDataBinding="GridLookupRole_DataBinding"
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
                                    <Settings ShowFilterRow="false" ShowFilterRowMenu="false" ShowVerticalScrollBar="True"
                                        ShowHorizontalScrollBar="false" VerticalScrollableHeight="170" AutoFilterCondition="Contains"></Settings>
                                </GridViewProperties>
                                <Columns>

                                    <dx:GridViewDataTextColumn Caption="Naziv" FieldName="Naziv" Width="100%"></dx:GridViewDataTextColumn>

                                </Columns>
                            </dx:ASPxGridLookup>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row m-0 pb-3 ">
                <div class="col-lg-12 mb-2 mb-lg-0">
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
        </div>

        <div id="EmailSettings" class="container-fluid tab-pane fade">
            <div class="row m-0 pb-3 pt-3">
                <div class="col-lg-6 mb-2 mb-lg-0">
                    <div class="row m-0 align-items-center justify-content-end">
                        <div class="col-0 p-0" style="margin-right: 65px;">
                            <dx:ASPxLabel ID="ASPxLabel11" runat="server" Text="EMAIL : *" Font-Size="12px"></dx:ASPxLabel>
                        </div>
                        <div class="col p-0">
                            <dx:ASPxTextBox runat="server" ID="txtEmail" ClientInstanceName="txtEmail" MaxLength="200"
                                CssClass="text-box-input" Font-Size="13px" Width="100%" AutoCompleteType="Disabled">
                                <FocusedStyle CssClass="focus-text-box-input"></FocusedStyle>
                            </dx:ASPxTextBox>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row m-0 pb-3">
                <div class="col-lg-6 mb-2 mb-lg-0">
                    <div class="row m-0 align-items-center justify-content-end">
                        <div class="col-0 p-0" style="margin-right: 75px;">
                            <dx:ASPxLabel ID="ASPxLabel12" runat="server" Text="GESLO : " Font-Size="12px"></dx:ASPxLabel>
                        </div>
                        <div class="col p-0">
                            <dx:ASPxTextBox runat="server" ID="txtEmailPassword" ClientInstanceName="txtEmailPassword" MaxLength="200"
                                CssClass="text-box-input" Font-Size="13px" Width="100%" AutoCompleteType="Disabled">
                                <FocusedStyle CssClass="focus-text-box-input"></FocusedStyle>
                            </dx:ASPxTextBox>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row m-0 pb-3">
                <div class="col-lg-6 mb-2 mb-lg-0">
                    <div class="row m-0 align-items-center justify-content-end">
                        <div class="col-0 p-0" style="margin-right: 25px;">
                            <dx:ASPxLabel ID="ASPxLabel13" runat="server" Text="STREŽNIK SMTP : " Font-Size="12px"></dx:ASPxLabel>
                        </div>
                        <div class="col p-0">
                            <dx:ASPxTextBox runat="server" ID="txtSmtpServer" ClientInstanceName="txtSmtpServer" MaxLength="200"
                                CssClass="text-box-input" Font-Size="13px" Width="100%" AutoCompleteType="Disabled">
                                <FocusedStyle CssClass="focus-text-box-input"></FocusedStyle>
                            </dx:ASPxTextBox>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row m-0 pb-3">
                <div class="col-lg-6 mb-2 mb-lg-0">
                    <div class="row m-0 align-items-center justify-content-end">
                        <div class="col-0 p-0" style="margin-right: 25px;">
                            <dx:ASPxLabel ID="ASPxLabel14" runat="server" Text="SSL ŠIFRIRANJE : " Font-Size="12px"></dx:ASPxLabel>
                        </div>
                        <div class="col p-0">
                            <dx:ASPxCheckBox ID="CheckBoxSSLEncrypting" runat="server" ToggleSwitchDisplayMode="Always"></dx:ASPxCheckBox>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row m-0 pb-3">
                <div class="col-lg-6 mb-2 mb-lg-0">
                    <div class="row m-0 align-items-center">
                        <div class="col-0 p-0" style="margin-right: 75px;">
                            <dx:ASPxLabel ID="ASPxLabel15" runat="server" Text="VRATA : " Font-Size="12px"></dx:ASPxLabel>
                        </div>
                        <div class="col p-0">
                            <dx:ASPxTextBox runat="server" ID="txtPort" ClientInstanceName="txtPort"
                                CssClass="text-box-input" Font-Size="13px" Width="100%" AutoCompleteType="Disabled" Font-Bold="true">
                                <FocusedStyle CssClass="focus-text-box-input"></FocusedStyle>
                                <ClientSideEvents KeyPress="isNumberKey_int" />
                            </dx:ASPxTextBox>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div id="Credentials" class="container-fluid tab-pane fade">
            <div class="row m-0 pb-3 pt-3">
                <div class="col-lg-6 mb-2 mb-lg-0">
                    <div class="row m-0 align-items-center">
                        <div class="col-0 p-0 mr-3">
                            <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="UPORABNIŠKO IME : " Font-Size="12px"></dx:ASPxLabel>
                        </div>
                        <div class="col p-0">
                            <dx:ASPxTextBox runat="server" ID="txtUsername" ClientInstanceName="txtUsername" MaxLength="200"
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
                        <div class="col-0 p-0" style="margin-right: 82px;">
                            <dx:ASPxLabel ID="ASPxLabel8" runat="server" Text="GESLO : " Font-Size="12px"></dx:ASPxLabel>
                        </div>
                        <div class="col p-0">
                            <dx:ASPxTextBox runat="server" ID="txtPassword" ClientInstanceName="txtPassword" MaxLength="200"
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
                        <div class="col-0 p-0" style="margin-right: 38px;">
                            <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="NOZ DOSTOP : " Font-Size="12px"></dx:ASPxLabel>
                        </div>
                        <div class="col p-0">
                            <dx:ASPxCheckBox ID="CheckBoxAllowSignIn" runat="server"></dx:ASPxCheckBox>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>
</asp:Content>
