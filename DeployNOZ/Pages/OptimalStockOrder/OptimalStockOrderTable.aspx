<%@ Page Title="Naročilo optimalnih zalog" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="OptimalStockOrderTable.aspx.cs" Inherits="GrafolitNOZ.Pages.OptimalStockOrder.OptimalStockOrderTable" %>

<%@ MasterType VirtualPath="~/Main.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContentHolder" runat="server">
    <script type="text/javascript">
        function HandleUserAction(s, e) {
            var action = HandleUserActionsOnTabs(gridOptimalStockOrder, btnAdd, btnEdit, btnDelete, s);
            CallbackPanelOptimalStockOrder.PerformCallback(action);
        }

        function OnClosePopUpHandler(command, sender) {
            switch (command) {
                case 'Potrdi':
                    switch (sender) {
                        case 'OptimalStockOrder':
                            PopupControlOptimalStockOrderAdd.Hide();
                            gridOptimalStockOrder.Refresh();
                            break;
                    }
                    break;
                case 'Preklici':
                    switch (sender) {
                        case 'OptimalStockOrder':
                            PopupControlOptimalStockOrderAdd.Hide();
                    }
                    break;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <dx:ASPxCallbackPanel ID="CallbackPanelOptimalStockOrder" runat="server" OnCallback="CallbackPanelOptimalStockOrder_Callback" ClientInstanceName="CallbackPanelOptimalStockOrder">
        <SettingsLoadingPanel Enabled="false" />
        <PanelCollection>
            <dx:PanelContent>
                <dx:ASPxGridView ID="ASPxGridViewOptimalStockOrder" runat="server" Width="100%" KeyFieldName="NarociloOptimalnihZalogID" OnDataBinding="ASPxGridViewOptimalStockOrder_DataBinding"
                    CssClass="gridview-no-header-padding mw-100" EnableRowsCache="false" AutoGenerateColumns="False" ClientInstanceName="gridOptimalStockOrder"
                    OnDataBound="ASPxGridViewOptimalStockOrder_DataBound">
                    <ClientSideEvents RowDblClick="HandleUserAction" />
                    <SettingsAdaptivity AdaptivityMode="HideDataCells" AllowOnlyOneAdaptiveDetailExpanded="true"
                        AllowHideDataCellsByColumnMinWidth="true">
                    </SettingsAdaptivity>
                    <SettingsBehavior AllowEllipsisInText="true" />
                    <Paddings Padding="0" />
                    <Settings ShowVerticalScrollBar="True"
                        ShowFilterBar="Auto" ShowFilterRow="True" VerticalScrollableHeight="550"
                        ShowFilterRowMenu="True" VerticalScrollBarStyle="Standard" VerticalScrollBarMode="Auto" />
                    <SettingsPager PageSize="50" ShowNumericButtons="true" NumericButtonCount="6">
                        <PageSizeItemSettings Visible="true" Items="50,80,100" Caption="Zapisi na stran : " AllItemText="Vsi">
                        </PageSizeItemSettings>
                        <Summary Visible="true" Text="Vseh zapisov : {2}" EmptyText="Ni zapisov" />
                    </SettingsPager>
                    <SettingsBehavior AllowFocusedRow="true" AllowEllipsisInText="true" />
                    <Styles>
                        <Header Paddings-PaddingTop="5" HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="true"></Header>
                        <FocusedRow BackColor="#d1e6fe" Font-Bold="true" ForeColor="#606060"></FocusedRow>
                        <Cell Wrap="False" />
                    </Styles>
                    <SettingsText EmptyDataRow="Trenutno ni podatka o naročilih optimalnih zalog. Dodaj novo." />
                    <EditFormLayoutProperties>
                        <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="600" />
                    </EditFormLayoutProperties>

                    <Columns>
                        <dx:GridViewDataTextColumn Caption="Zap. številka" FieldName="NarociloOptimalnihZalogStevilka" AdaptivePriority="2" MinWidth="80" MaxWidth="150" Width="9%" />
                        <dx:GridViewDataTextColumn Caption="Naročilo pantheon" FieldName="NarociloID_P" AdaptivePriority="1" MinWidth="100" MaxWidth="170" Width="10%" />
                        <dx:GridViewDataTextColumn Caption="Dobavitelj" FieldName="Stranka.NazivPrvi" AdaptivePriority="1" MinWidth="100" MaxWidth="200" Width="25%" />
                        <dx:GridViewDataTextColumn Caption="Naziv" FieldName="Naziv" AdaptivePriority="2" MinWidth="200" MaxWidth="250" Width="30%" />
                        <dx:GridViewDataTextColumn Caption="Status" FieldName="StatusNarocilaOptimalnihZalog.Naziv" AdaptivePriority="2" MinWidth="150" MaxWidth="200" Width="16%" />
                        <dx:GridViewDataTextColumn Caption="Količina" FieldName="Kolicina" AdaptivePriority="1" MinWidth="100" MaxWidth="150" Width="10%" />
                    </Columns>
                    <SettingsResizing ColumnResizeMode="NextColumn" Visualization="Live" />
                </dx:ASPxGridView>

                <dx:ASPxPopupControl ID="PopupControlOptimalStockOrderAdd" runat="server" ContentUrl="OptimalStockOrderAdd_popup.aspx"
                    ClientInstanceName="PopupControlOptimalStockOrderAdd" Modal="True" HeaderText="NAROČILO OPTIMALNIH ZALOG"
                    CloseAction="CloseButton" Width="1400px" Height="900px" PopupHorizontalAlign="WindowCenter"
                    PopupVerticalAlign="WindowCenter" PopupAnimationType="Fade" AllowDragging="true" ShowSizeGrip="true"
                    AllowResize="true" ShowShadow="true"
                    OnWindowCallback="PopupControlOptimalStockOrderAdd_WindowCallback">
                    <SettingsAdaptivity Mode="OnWindowInnerWidth" SwitchAtWindowInnerWidth="800" VerticalAlign="WindowCenter" MinHeight="400px" MaxWidth="680px" MaxHeight="400px" />
                    <ContentStyle BackColor="#F7F7F7">
                        <Paddings Padding="0px"></Paddings>
                    </ContentStyle>
                </dx:ASPxPopupControl>

                <div class="row m-0 mt-2">
                    <div class="col-sm-9">
                        <dx:ASPxButton ID="btnDelete" runat="server" Text="Izbriši" AutoPostBack="false"
                            Height="25" Width="50" ClientInstanceName="btnDelete">
                            <Paddings PaddingLeft="10" PaddingRight="10" />
                            <Image Url="../../Images/trash.png" UrlHottracked="../../Images/trashHover.png" />
                            <ClientSideEvents Click="HandleUserAction" />
                        </dx:ASPxButton>

                        <dx:ASPxButton ID="btnCopyOrder" runat="server" Text="Kopiraj" AutoPostBack="false"
                            Height="25" Width="110" ClientInstanceName="btnCopyOrder" UseSubmitBehavior="false" OnClick="btnCopyOrder_Click">
                            <Paddings PaddingLeft="10" PaddingRight="10" />
                            <Image Url="../../Images/copy.png" UrlHottracked="../../Images/copyHover.png" />
                        </dx:ASPxButton>
                    </div>
                    <div class="col-sm-3 text-right">
                        <dx:ASPxButton ID="btnAdd" runat="server" Text="Dodaj" AutoPostBack="false"
                            Height="25" Width="90" ClientInstanceName="btnAdd">
                            <Paddings PaddingLeft="10" PaddingRight="10" />
                            <Image Url="../../Images/add.png" UrlHottracked="../../Images/addHover.png" />
                            <ClientSideEvents Click="HandleUserAction" />
                        </dx:ASPxButton>

                        <dx:ASPxButton ID="btnEdit" runat="server" Text="Spremeni" AutoPostBack="false"
                            Height="25" Width="90" ClientInstanceName="btnEdit">
                            <Paddings PaddingLeft="10" PaddingRight="10" />
                            <Image Url="../../Images/edit.png" UrlHottracked="../../Images/editHover.png" />
                            <ClientSideEvents Click="HandleUserAction" />
                        </dx:ASPxButton>
                    </div>
                </div>
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxCallbackPanel>
</asp:Content>
