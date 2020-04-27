<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="GrafolitNOZ.Pages.Settings.Admin" %>

<%@ MasterType VirtualPath="~/Main.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContentHolder" runat="server">
    <script type="text/javascript">

</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <div class="card">
        <div class="card-header" style="background-color: #FAFCFE">
            <div class="d-flex justify-content-between align-items-center">
                <h6>NOZ - Naročilo optimalnih zalog</h6>
                <a data-toggle="collapse" href="#collapseBasicData" aria-expanded="true" aria-controls="collapseBasicData"><i style="font-size: 24px; color: #209FE8;" class='fas fa-angle-down'></i></a>
            </div>
        </div>
        <div class="card-body" style="background-color: #eef2f5d6;">

            <div class="row m-0 pb-3">
                <div class="col-sm-3 no-padding-left">
                    <div>
                        <h5 class="no-margin"><em>Log datoteke</em></h5>
                    </div>
                    <div class="panel panel-default" style="margin-top: 2px;">
                        <div class="panel-body">
                            <div style="display: inline-block;" title="Prodobi log file iz aplikacije in web servica" data-toggle="popover" data-trigger="hover" data-content="Izdelaj Naročilnico PDF in pošlji">
                                <dx:ASPxButton ID="btnGetLogs" runat="server" Text="Log datoteke" Width="100" OnClick="btnGetLogs_Click"
                                    AutoPostBack="false" UseSubmitBehavior="false">
                                </dx:ASPxButton>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterScriptHolder" runat="server">
</asp:Content>
