<%@ Page Title="Zaposleni" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="EmployeeTable.aspx.cs" Inherits="GrafolitNOZ.Pages.Employee.EmployeeTable" %>

<%@ MasterType VirtualPath="~/Main.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContentHolder" runat="server">
    <script type="text/javascript">


        function EmployeeCallbackPanel_EndCallback(s, e) {
            LoadingPanel.Hide();
        }

        function PopupHandeling_Click(s, e) {
            var parameter = HandleUserActionsOnTabs(gridEmployee, btnAdd, btnEdit, btnDelete, s);

            LoadingPanel.Show();
            EmployeeCallbackPanel.PerformCallback(parameter);
        }

        function OnClosePopUpHandler(command, sender) {
            switch (command) {
                case 'Potrdi':
                    switch (sender) {
                        case 'Employee':
                            PopupControlEmployee.Hide();
                            gridEmployee.Refresh();
                            break;
                    }
                    break;
                case 'Preklici':
                    switch (sender) {
                        case 'Employee':
                            PopupControlEmployee.Hide();
                            break;
                    }
                    break;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <dx:ASPxCallbackPanel ID="EmployeeCallbackPanel" OnCallback="EmployeeCallbackPanel_Callback" ClientInstanceName="EmployeeCallbackPanel" runat="server">
        <ClientSideEvents EndCallback="EmployeeCallbackPanel_EndCallback" />
        <SettingsLoadingPanel Enabled="false" />
        <PanelCollection>
            <dx:PanelContent>
                <dx:ASPxGridView ID="ASPxGridViewEmployee" runat="server" EnableCallbackCompression="true" ClientInstanceName="gridEmployee"
                    OnDataBinding="ASPxGridViewEmployee_DataBinding" Width="100%"
                    KeyFieldName="idOsebe" CssClass="gridview-no-header-padding">
                    <ClientSideEvents RowDblClick="PopupHandeling_Click" />
                    <Paddings Padding="0" />
                    <Settings ShowVerticalScrollBar="True"
                        ShowFilterBar="Auto" ShowFilterRow="True" VerticalScrollableHeight="400" AutoFilterCondition="Contains"
                        ShowFilterRowMenu="True" VerticalScrollBarStyle="Standard" VerticalScrollBarMode="Auto" />
                    <SettingsPager PageSize="50" ShowNumericButtons="true">
                        <PageSizeItemSettings Visible="true" Items="50,80,100" Caption="Zapisi na stran : " AllItemText="Vsi">
                        </PageSizeItemSettings>
                        <Summary Visible="true" Text="Vseh zapisov : {2}" EmptyText="Ni zapisov" />
                    </SettingsPager>
                    <SettingsBehavior AllowFocusedRow="true" AllowEllipsisInText="true" />
                    <Styles Header-Wrap="True">
                        <Header Paddings-PaddingTop="5" HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="true"></Header>
                        <FocusedRow BackColor="#d1e6fe" Font-Bold="true" ForeColor="#606060"></FocusedRow>
                    </Styles>
                    <SettingsText EmptyDataRow="Trenutno ni podatka o ¸zaposlenih. Dodaj novega." />
                    <Columns>

                        <dx:GridViewDataTextColumn Caption="ID" FieldName="idOsebe" Visible="false" SortOrder="Descending" />

                        <dx:GridViewDataTextColumn Caption="Ime" FieldName="Ime" Width="25%" />

                        <dx:GridViewDataTextColumn Caption="Priimek" FieldName="Priimek" Width="25%" />

                        <dx:GridViewDataTextColumn Caption="Naslov" FieldName="Naslov" Width="30%" />

                        <dx:GridViewDataTextColumn Caption="Email" FieldName="Email" Width="15%" />

                        <dx:GridViewDataTextColumn Caption="Telefon" FieldName="TelefonGSM" Width="12%" />

                    </Columns>
                </dx:ASPxGridView>

                <dx:ASPxPopupControl ID="PopupControlEmployee" runat="server" ContentUrl="Employee_popup.aspx"
                    ClientInstanceName="PopupControlEmployee" Modal="True" HeaderText="ZAPOSLEN"
                    CloseAction="CloseButton" Width="1000px" Height="530px" PopupHorizontalAlign="WindowCenter"
                    PopupVerticalAlign="WindowCenter" PopupAnimationType="Fade" AllowDragging="true" ShowSizeGrip="true"
                    AllowResize="true" ShowShadow="true"
                    OnWindowCallback="PopupControlEmployee_WindowCallback">
                    <ClientSideEvents CloseButtonClick="OnPopupCloseButtonClick" />
                    <ContentStyle BackColor="#F7F7F7">
                        <Paddings PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px" PaddingTop="0px"></Paddings>
                    </ContentStyle>
                </dx:ASPxPopupControl>


                <div class="AddEditButtonsWrap medium-margin-l medium-margin-r">
                    <div class="DeleteButtonElements">
                        <span class="AddEditButtons">
                            <dx:ASPxButton ID="btnDelete" runat="server" Text="Izbriši" AutoPostBack="false"
                                Height="25" Width="50" ClientInstanceName="btnDelete">
                                <Paddings PaddingLeft="10" PaddingRight="10" />
                                <Image Url="../../../Images/trash.png" UrlHottracked="../../Images/trashHover.png" />
                                <ClientSideEvents Click="PopupHandeling_Click" />
                            </dx:ASPxButton>
                        </span>

                    </div>
                    <div class="AddEditButtonsElements">
                        <span class="AddEditButtons">
                            <dx:ASPxButton ID="btnAdd" runat="server" Text="Dodaj" AutoPostBack="false"
                                Height="25" Width="90" ClientInstanceName="btnAdd">
                                <Paddings PaddingLeft="10" PaddingRight="10" />
                                <Image Url="../../../Images/add.png" UrlHottracked="../../Images/addHover.png" />
                                <ClientSideEvents Click="PopupHandeling_Click" />
                            </dx:ASPxButton>
                        </span>
                        <span class="AddEditButtons">
                            <dx:ASPxButton ID="btnEdit" runat="server" Text="Spremeni" AutoPostBack="false"
                                Height="25" Width="90" ClientInstanceName="btnEdit">
                                <Paddings PaddingLeft="10" PaddingRight="10" />
                                <Image Url="../../../Images/edit.png" UrlHottracked="../../Images/editHover.png" />
                                <ClientSideEvents Click="PopupHandeling_Click" />
                            </dx:ASPxButton>
                        </span>
                    </div>
                </div>
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxCallbackPanel>
</asp:Content>
