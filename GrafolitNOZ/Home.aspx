<%@ Page Title="Nadzorna plošča" Language="C#" MasterPageFile="~/Greetings.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="GrafolitNOZ.Home" %>

<%@ MasterType VirtualPath="~/Main.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContentHolder" runat="server">
    <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#MainContentPlaceHolder_ASPxFormLayoutLogin_0').keypress(function (event) {
                var key = event.which;
                if (key == 13) {
                    CauseValidation(this, event);
                    clientUsername.GetInputElement().blur();
                    clientPass.GetInputElement().blur();
                    return false;
                }
            });

            var unhandledException = GetUrlQueryStrings()['unhandledExp'];
            var messageType = GetUrlQueryStrings()['messageType'];

            if (unhandledException) {
                $("#unhandledExpModal").modal("show");

                //we delete successMessage query string so we show modal only once!
                var params = QueryStringsToObject();
                delete params.unhandledExp;
                var path = window.location.pathname + '?' + SerializeQueryStrings(params);
                history.pushState({}, document.title, path);
            }
            else if (messageType !== undefined) {
                var value = "";

               /* switch (messageType) {
                    case "1":
                        var resource = '';
                        value = resource;
                        break;
                    case "2":
                        value = '';
                        break;
                    default:
                        break;
                }

                $("#handledExpBodyText").append(value);
                $("#handledExpModal").modal("show");

                //we delete messageType query string so we show modal only once!
                var params = QueryStringsToObject();
                delete params.messageType;
                var path = window.location.pathname + '?' + SerializeQueryStrings(params);
                history.pushState({}, document.title, path);*/
            }

            clientChartsCallbackPanel.PerformCallback("RefreshCharts");
        });

        function CauseValidation(s, e) {
            var procees = false;
            var inputItems = [clientUsername, clientPass];

            procees = InputFieldsValidation(null, inputItems, null, null);

            if (procees) {
                clientLoadingPanel.Show();
                clientLoginCallback.PerformCallback("Test");
            }
        }

        function EndLoginCallback(s, e) {
            clientLoadingPanel.Hide();

            if (s.cpResult != null && s.cpResult !== undefined) {

                ShowErrorPopUp(s.cpResult);
                clientErrorLabel.SetText(s.cpResult);
                clientUsername.SetText("");
                clientPass.SetText("");
                delete (s.cpResult);
            }
            else
                window.location.assign('Home.aspx');//"../Default.aspx"
        }
        function ClearText(s, e) {
            clientErrorLabel.SetText("");
        }

        var data = null;
        var dataEmployees = null;
        var dataTransporters = null;
        var dataRoutes = null;
        var dataSupplier = null;

        function ChartsCallbackPanel_EndCallback(s, e) {
            if (s.cpChartData != null && s.cpChartData !== undefined) {
                data = s.cpChartData;

                // Load the Visualization API and the corechart package.
                google.charts.load('current', { 'packages': ['corechart'] });
                google.charts.setOnLoadCallback(drawChart);

                delete (s.cpChartData);
            }

            if (s.cpChartDataEmployees != null && s.cpChartDataEmployees !== undefined) {
                dataEmployees = s.cpChartDataEmployees;

                // Load the Visualization API and the corechart package.
                google.charts.load('current', { 'packages': ['corechart'] });
                google.charts.setOnLoadCallback(drawChartEmployees);

                delete (s.cpChartDataEmployees);
            }

            if (s.cpChartDataTransporters != null && s.cpChartDataTransporters !== undefined) {
                dataTransporters = s.cpChartDataTransporters;

                // Load the Visualization API and the corechart package.
                google.charts.load('current', { 'packages': ['corechart'] });
                google.charts.setOnLoadCallback(drawChartTransporters);

                delete (s.cpChartDataEmployees);
            }

            if (s.cpChartDataRoutes != null && s.cpChartDataRoutes !== undefined) {
                dataRoutes = s.cpChartDataRoutes;

                // Load the Visualization API and the corechart package.
                google.charts.load('current', { 'packages': ['corechart'] });
                google.charts.setOnLoadCallback(drawChartRoutes);

                delete (s.cpChartDataRoutes);
            }

            if (s.cpChartDataSupplier != null && s.cpChartDataSupplier !== undefined) {
                dataSupplier = s.cpChartDataSupplier;

                // Load the Visualization API and the corechart package.
                google.charts.load('current', { 'packages': ['corechart'] });
                google.charts.setOnLoadCallback(drawChartSupplier);

                delete (s.cpChartDataSupplier);
            }
        }
        function drawChart() {
            var dataChart = google.visualization.arrayToDataTable($.parseJSON(data));
            var options = {
                hAxis: { title: 'Naročila tekočega leta po mesecih', titleTextStyle: { color: '#333' } },
                vAxis: { minValue: 0 },
                legend: { position: 'top', maxLines: 3 },
                animation: {
                    duration: 3000,
                    easing: 'out',
                    startup: true
                }
            };

            var chart = new google.visualization.AreaChart(document.getElementById('chart_div'));
            chart.draw(dataChart, options);
        }

        function drawChartEmployees() {
            var dataChart = google.visualization.arrayToDataTable($.parseJSON(dataEmployees));
            var options = {
                hAxis: { title: 'Število naročil', titleTextStyle: { color: '#333' } },
                vAxis: { minValue: 0 },
                legend: { position: 'none' },
                animation: {
                    duration: 3000,
                    easing: 'out',
                    startup: true
                }
            };

            var chart = new google.visualization.BarChart(document.getElementById('chart_employees_div'));
            chart.draw(dataChart, options);
        }

        function drawChartTransporters() {
            var dataChart = google.visualization.arrayToDataTable($.parseJSON(dataTransporters));
            var options = {
                tooltip: 'none',
                bar: { groupWidth: "95%" },
                legend: { position: 'none' },
                animation: {
                    duration: 3000,
                    easing: 'out',
                    startup: true
                }
            };

            var chart = new google.visualization.ColumnChart(document.getElementById('chart_transporter_div'));
            chart.draw(dataChart, options);
        }

        function drawChartRoutes() {
            var dataChart = google.visualization.arrayToDataTable($.parseJSON(dataRoutes));
            var options = {
                tooltip: 'none',
                bar: { groupWidth: "65%" },
                legend: { position: 'none' },
                animation: {
                    duration: 3000,
                    easing: 'out',
                    startup: true
                }
            };

            var chart = new google.visualization.ColumnChart(document.getElementById('chart_route_div'));
            chart.draw(dataChart, options);
        }

        function drawChartSupplier() {
            var dataChart = google.visualization.arrayToDataTable($.parseJSON(dataSupplier));
            var options = {
                tooltip: 'none',
                pieHole: 0.4,
                animation: {
                    duration: 3000,
                    easing: 'out',
                    startup: true
                }
            };

            var chart = new google.visualization.PieChart(document.getElementById('chart_supplier_div'));
            chart.draw(dataChart, options);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <div id="FormLayoutWrap" runat="server" style="display: flex; width: 50%; margin: 0 auto; overflow: hidden; padding: 10px; border: 1px solid #e1e1e1; border-radius: 3px; box-shadow: 5px 10px 18px #e1e1e1; background-color: whitesmoke; margin-top: 30px;">
        <dx:ASPxFormLayout ID="ASPxFormLayoutLogin" runat="server">
            <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="500" />
            <Items>
                <dx:LayoutGroup Name="LOGIN" GroupBoxDecoration="HeadingLine" Caption="NOZ - Naročila optimalnih zalog" UseDefaultPaddings="false" GroupBoxStyle-Caption-BackColor="WhiteSmoke">
                    <Items>
                        <dx:LayoutItem Caption="Error label caption" Name="ErrorLabelCaption" ShowCaption="False"
                            CaptionSettings-VerticalAlign="Middle">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxLabel ID="ErrorLabel" runat="server" Text="" ForeColor="Red"
                                        ClientInstanceName="clientErrorLabel">
                                    </dx:ASPxLabel>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="Uporabniško ime" Name="Username" CaptionSettings-VerticalAlign="Middle" Paddings-PaddingBottom="20px">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxTextBox ID="txtUsername" runat="server" Theme="Moderno"
                                        CssClass="text-box-input" ClientInstanceName="clientUsername"
                                        AutoCompleteType="Disabled">
                                        <FocusedStyle CssClass="focus-text-box-input" />
                                        <ClientSideEvents Init="SetFocus" GotFocus="ClearText" />
                                    </dx:ASPxTextBox>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="Geslo" Name="Password" CaptionSettings-VerticalAlign="Middle" Paddings-PaddingBottom="10px">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxTextBox ID="txtPassword" runat="server" Theme="Moderno"
                                        CssClass="text-box-input" Password="true" ClientInstanceName="clientPass">
                                        <ClientSideEvents GotFocus="ClearText" />
                                        <FocusedStyle CssClass="focus-text-box-input" />
                                    </dx:ASPxTextBox>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="Zapomni si geslo" Name="RememberMe" Paddings-PaddingTop="10px">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxCheckBox ID="rememberMeCheckBox" runat="server" ToggleSwitchDisplayMode="Always" />
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Name="SignUp" HorizontalAlign="Right" ShowCaption="False" Paddings-PaddingTop="20px">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxButton ID="ASPxButton1" runat="server" Text="PRIJAVA" Width="100" Theme="Moderno"
                                        AutoPostBack="false" UseSubmitBehavior="false">
                                        <ClientSideEvents Click="CauseValidation" />
                                    </dx:ASPxButton>
                                    <dx:ASPxCallback ID="LoginCallback" runat="server" OnCallback="LoginCallback_Callback"
                                        ClientInstanceName="clientLoginCallback">
                                        <ClientSideEvents EndCallback="EndLoginCallback" />
                                    </dx:ASPxCallback>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                    </Items>
                </dx:LayoutGroup>
            </Items>
        </dx:ASPxFormLayout>
        <dx:ASPxLoadingPanel ID="LoadingPanel" ClientInstanceName="clientLoadingPanel" runat="server" Modal="true">
        </dx:ASPxLoadingPanel>
    </div>

    <div id="MainDashboard" runat="server" class="medium-paddings" style="display: none;">
        <div class="row">
            <div class="col-md-3 d-flex pr-0">
                <div class="row no-padding-imp card-column w-100" style="background-color: #FFF" onclick="location.href='/Pages/OptimalStockOrder/OptimalStockOrderTable.aspx';">

                    <div class="col-3 all-recalls card-column-image"></div>
                    <div class="col-9 card-column-content d-flex align-items-center justify-content-start" style="padding: 15px 10px;">
                        <div class="row p-0">
                            <div class="col">
                                <h4 class="card-column-content-title all-recalls-title">Vsa naročila optimalnih zalog</h4>
                                <dx:ASPxLabel ID="lblAllOrders" runat="server" CssClass="card-column-content-body" Font-Italic="true" />
                            </div>
                        </div>
                    </div>

                </div>
            </div>
            <div class="col-md-3 d-flex pr-0">
                <div class="row no-padding-imp card-column w-100" style="background-color: #FFF" onclick="location.href='/Pages/OptimalStockOrder/OptimalStockOrderTable.aspx?filter=1';">
                    <div class="col-3 approved-recalls card-column-image"></div>
                    <div class="col-9 card-column-content d-flex align-items-center justify-content-start">
                        <div class="row p-0">
                            <div class="col">
                                <h4 class="card-column-content-title approved-recalls-title">Oddana naročila</h4>
                                <dx:ASPxLabel ID="lblSubmitedOrders" runat="server" CssClass="card-column-content-body" Font-Italic="true" />
                            </div>
                        </div>
                    </div>
                </div>

            </div>

            <div class="col-md-3 pr-0 d-none">
                <div class="row no-padding-imp card-column w-100" style="background-color: #FFF" onclick="location.href='/Pages/OptimalStockOrder/OptimalStockOrderTable.aspx?filter=2';">
                    <div class="col-3 rejected-recalls card-column-image"></div>
                    <div class="col-9 card-column-content d-flex align-items-center justify-content-start">
                        <div class="row p-0">
                            <div class="col">
                                <h4 class="card-column-content-title rejected-recalls-title">Delovna naročila</h4>
                                <dx:ASPxLabel ID="lblInquiriesInProgress" runat="server" CssClass="card-column-content-body" Font-Italic="true" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-md-3 d-flex pr-0">
                <div class="row no-padding-imp card-column w-100" style="background-color: #FFF" onclick="location.href='/Pages/OptimalStockOrder/OptimalStockOrderTable.aspx?filter=2';">
                    <div class="col-3 need-approval-recalls card-column-image"></div>
                    <div class="col-9 card-column-content d-flex align-items-center justify-content-start">
                        <div class="row p-0">
                            <div class="col">
                                <h4 class="card-column-content-title need-approval-recalls-title">Kreirana naročila</h4>
                                <dx:ASPxLabel ID="lblCreatedOrder" runat="server" CssClass="card-column-content-body" Font-Italic="true" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <dx:ASPxCallbackPanel ID="ChartsCallbackPanel" runat="server" OnCallback="ChartsCallbackPanel_Callback" ClientInstanceName="clientChartsCallbackPanel">
            <ClientSideEvents EndCallback="ChartsCallbackPanel_EndCallback" />
            <PanelCollection>
                <dx:PanelContent>
                    <div class="row pt-5">
                        <div class="col-md-6">
                            <div class="panel panel-default">
                                <div class="panel-body no-padding-imp">
                                    <h4 class="small-margin-l" style="padding: 10px 20px;">Naročila tekočega leta</h4>
                                    <div id="chart_div" style="width: 100%; height: 300px;"></div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="panel panel-default">
                                <div class="panel-body no-padding-imp">
                                    <h4 class="small-margin-l" style="padding: 10px 20px;">Naročila po zaposlenem v  tekočem letu</h4>
                                    <div id="chart_employees_div" style="width: 100%; height: 300px;"></div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row d-none">
                        <div class="col-md-8">
                            <div class="panel panel-default">
                                <div class="panel-body no-padding-imp">
                                    <h4 class="small-margin-l" style="padding: 10px 20px;">Najpogosteje uporabljene relacije na odpoklicih</h4>
                                    <div id="chart_route_div" style="width: 100%; height: 300px;"></div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="panel panel-default">
                                <div class="panel-body no-padding-imp">
                                    <h4 class="small-margin-l" style="padding: 10px 20px;">Najpogostejši dobavitelji na odpoklicih</h4>
                                    <div id="chart_supplier_div" style="width: 100%; height: 300px;"></div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row d-none">
                        <div class="col-md-12">
                            <div class="panel panel-default">
                                <div class="panel-body no-padding-imp">
                                    <h4 class="small-margin-l" style="padding: 10px 20px;">Odpoklici za prevoznika v  tekočem letu</h4>
                                    <div id="chart_transporter_div" style="width: 100%; height: 300px;"></div>
                                </div>
                            </div>
                        </div>
                    </div>
                </dx:PanelContent>
            </PanelCollection>
        </dx:ASPxCallbackPanel>
    </div>

    <!-- Unhandled exception Modal -->
    <div id="unhandledExpModal" class="modal fade" role="dialog">
        <div class="modal-dialog modal-sm">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header text-center" style="background-color: red; border-top-left-radius: 6px; border-top-right-radius: 6px;">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <div><i class="material-icons" style="font-size: 48px; color: white">error_outline</i></div>
                </div>
                <div class="modal-body text-center">
                    <h3>Napaka!</h3>
                    <p>Sistem je naletel na napako. Naša ekipa razvijalcev je že dobila obvestilo o napaki in je v čakalni vrsti za odpravljanje. Za to se vam iskreno opravičujemo in vas lepo pozdravljamo.</p>
                </div>
            </div>

        </div>
    </div>

    <!-- Handled exception Modal -->
    <div id="handledExpModal" class="modal fade" role="dialog">
        <div class="modal-dialog modal-sm">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header text-center" style="background-color: #bce8f1; border-top-left-radius: 6px; border-top-right-radius: 6px;">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <div><i class="material-icons" style="font-size: 48px; color: white">error_outline</i></div>
                </div>
                <div class="modal-body text-center">
                    <h3>Opozorilo!</h3>
                    <p id="handledExpBodyText"></p>
                </div>
            </div>

        </div>
    </div>
</asp:Content>
