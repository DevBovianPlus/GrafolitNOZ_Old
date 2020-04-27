<%@ Page Title="" Language="C#" MasterPageFile="~/Popup.Master" AutoEventWireup="true" CodeBehind="ContactPerson_popup.aspx.cs" Inherits="GrafolitNOZ.Pages.Client.ContactPerson_popup" %>

<%@ Register Assembly="DevExpress.Web.v19.2, Version=19.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>


<%@ MasterType VirtualPath="~/Popup.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var hasStartEditing = false;
        var postbackInitiated = false;

        function CheckFieldValidation() {
            var process = false;
            var lookUpItems = [];

            var inputItems = [txtName, txtEmail];
            var dateItems = null;

            process = InputFieldsValidation(lookUpItems, inputItems, dateItems, /*memoItems*/ null, /*comboBoxItems*/null, null);

            if (btnConfirm.GetText() == 'Izbriši')
                process = true;

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

    <div class="row m-0 pb-3 pt-3">
        <div class="col-lg-12 mb-2 mb-lg-0">
            <div class="row m-0 align-items-center">
                <div class="col-0 p-0" style="margin-right: 25px;">
                    <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="NAZIV : *" Font-Size="12px"></dx:ASPxLabel>
                </div>
                <div class="col p-0">
                    <dx:ASPxTextBox runat="server" ID="txtName" ClientInstanceName="txtName" MaxLength="300"
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
                    <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="TELEFON : " Font-Size="12px"></dx:ASPxLabel>
                </div>
                <div class="col-7 p-0">
                    <dx:ASPxTextBox runat="server" ID="txtPhone" ClientInstanceName="txtPhone" MaxLength="50"
                        CssClass="text-box-input" Font-Size="13px" Width="100%" AutoCompleteType="Disabled">
                        <FocusedStyle CssClass="focus-text-box-input"></FocusedStyle>
                    </dx:ASPxTextBox>
                </div>
            </div>
        </div>

        <div class="col-lg-6 mb-2 mb-lg-0">
            <div class="row m-0 align-items-center justify-content-end">
                <div class="col-0 p-0 mr-2">
                    <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="GSM : " Font-Size="12px"></dx:ASPxLabel>
                </div>
                <div class="col-7 p-0">
                    <dx:ASPxTextBox runat="server" ID="txtPhoneGSM" ClientInstanceName="txtPhoneGSM" MaxLength="50"
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
                <div class="col-0 p-0" style="margin-right: 47px;">
                    <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="FAX : " Font-Size="12px"></dx:ASPxLabel>
                </div>
                <div class="col-7 p-0">
                    <dx:ASPxTextBox runat="server" ID="txtFax" ClientInstanceName="txtFax" MaxLength="30"
                        CssClass="text-box-input" Font-Size="13px" Width="100%" AutoCompleteType="Disabled">
                        <FocusedStyle CssClass="focus-text-box-input"></FocusedStyle>
                    </dx:ASPxTextBox>
                </div>
            </div>
        </div>

        <div class="col-lg-6 mb-2 mb-lg-0">
            <div class="row m-0 align-items-center justify-content-end">
                <div class="col-0 p-0" style="margin-right: 17px">
                    <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="EMAIL : *" Font-Size="12px"></dx:ASPxLabel>
                </div>
                <div class="col-7 p-0">
                    <dx:ASPxTextBox runat="server" ID="txtEmail" ClientInstanceName="txtEmail" MaxLength="50"
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
                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="PODPIS : " Font-Size="12px"></dx:ASPxLabel>
                </div>
                <div class="col p-0">
                    <dx:ASPxTextBox runat="server" ID="txtSignature" ClientInstanceName="txtSignature" MaxLength="30"
                        CssClass="text-box-input" Font-Size="13px" Width="100%" AutoCompleteType="Disabled">
                        <FocusedStyle CssClass="focus-text-box-input"></FocusedStyle>
                    </dx:ASPxTextBox>
                </div>
            </div>
        </div>
    </div>

    <div class="row m-0 pb-3">
        <div class="col-lg-12 mb-2 mb-lg-0">
            <div class="row m-0 align-items-center">
                <div class="col-0 p-0" style="margin-right: 20px;">
                    <dx:ASPxLabel ID="ASPxLabel8" runat="server" Text="OPOMBE : " Font-Size="12px"></dx:ASPxLabel>
                </div>
                <div class="col p-0">
                    <dx:ASPxMemo ID="MemoNotes" runat="server" Width="100%" Rows="8" MaxLength="1000" CssClass="text-box-input" AutoCompleteType="Disabled">
                        <FocusedStyle CssClass="focus-text-box-input"></FocusedStyle>
                    </dx:ASPxMemo>
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
</asp:Content>
