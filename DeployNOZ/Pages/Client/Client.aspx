<%@ Page Title="Dobavitelji" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Client.aspx.cs" Inherits="GrafolitNOZ.Pages.Client.Client" %>

<%@ MasterType VirtualPath="~/Main.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContentHolder" runat="server">
    <script>

        function DoubleClick(s, e) {
            gridClient.GetRowValues(gridClient.GetFocusedRowIndex(), 'idStranka', OnGetRowValues);
        }

        function OnGetRowValues(value) {
            gridClient.PerformCallback('DblClick;' + value);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <dx:ASPxGridView ID="ASPxGridViewClient" runat="server" EnableCallbackCompression="true" ClientInstanceName="gridClient"
        Width="100%" KeyboardSupport="true" AccessKey="G" OnDataBinding="ASPxGridViewClient_DataBinding"
        KeyFieldName="idStranka" CssClass="gridview-no-header-padding" OnCustomCallback="ASPxGridViewClient_CustomCallback">
        <ClientSideEvents RowDblClick="DoubleClick" />
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
            <dx:GridViewDataTextColumn Caption="ID" FieldName="idStranka" Width="80px" SortOrder="Descending" Visible="false">
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="Naziv" FieldName="NazivPrvi" Width="25%">
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="Naslov" FieldName="Naslov" Width="30%">
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="Email"
                FieldName="Email" Width="20%">
                <Settings AllowAutoFilter="True" AutoFilterCondition="Contains" />
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="Telefon"
                FieldName="Telefon" Width="15%">
                <Settings AllowAutoFilter="True" AutoFilterCondition="Contains" />
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="Tip"
                FieldName="TipStranka" Width="10%">
                <Settings AllowAutoFilter="True" AutoFilterCondition="Contains" />
            </dx:GridViewDataTextColumn>

             <dx:GridViewDataTextColumn Caption="Jezik"
                FieldName="Jezik" Width="10%">
                <Settings AllowAutoFilter="True" AutoFilterCondition="Contains" />
            </dx:GridViewDataTextColumn>


        </Columns>
    </dx:ASPxGridView>

    <div class="AddEditButtonsWrap medium-margin-l medium-margin-r">
        <div class="DeleteButtonElements">
            <span class="AddEditButtons">
                <dx:ASPxButton Theme="Moderno" ID="btnDelete" runat="server" Text="Izbriši" AutoPostBack="false"
                    Height="25" Width="50" ClientInstanceName="clientBtnDelete" OnClick="btnDelete_Click">
                    <Paddings PaddingLeft="10" PaddingRight="10" />
                    <Image Url="../../../Images/trash.png" UrlHottracked="../../Images/trashHover.png" />
                </dx:ASPxButton>
            </span>
        </div>
        <div class="AddEditButtonsElements">
            <span class="AddEditButtons">
                <dx:ASPxButton Theme="Moderno" ID="btnAdd" runat="server" Text="Dodaj" AutoPostBack="false"
                    Height="25" Width="90" ClientInstanceName="clientBtnAdd" OnClick="btnAdd_Click">
                    <Paddings PaddingLeft="10" PaddingRight="10" />
                    <Image Url="../../../Images/add.png" UrlHottracked="../../Images/addHover.png" />
                </dx:ASPxButton>
            </span>
            <span class="AddEditButtons">
                <dx:ASPxButton Theme="Moderno" ID="btnEdit" runat="server" Text="Spremeni" AutoPostBack="false"
                    Height="25" Width="90" ClientInstanceName="clientBtnEdit" OnClick="btnEdit_Click">
                    <Paddings PaddingLeft="10" PaddingRight="10" />
                    <Image Url="../../../Images/edit.png" UrlHottracked="../../Images/editHover.png" />
                </dx:ASPxButton>
            </span>
        </div>
    </div>
</asp:Content>
