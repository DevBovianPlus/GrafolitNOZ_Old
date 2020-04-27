using DatabaseWebService.Models.Client;
using DatabaseWebService.ModelsNOZ.OptimalStockOrder;
using DevExpress.Web;
using DevExpress.Web.ASPxTreeList;
using DevExpress.Web.Data;
using GrafolitNOZ.Common;
using GrafolitNOZ.Helpers;
using GrafolitNOZ.Infrastructure;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GrafolitNOZ.Pages.OptimalStockOrder
{
    public partial class OptimalStockOrderAdd : ServerMasterPage
    {
        OptimalStockOrderModel model;
        string category;
        bool colorChanged;
        bool categoryChanged;
        bool filterProductsBySupplier;

        protected void Page_Init(object sender, EventArgs e)
        {
            this.Master.PageHeadlineTitle = this.Title;
            this.Master.DisableNavBar = true;

            GridLookupCategory.GridView.Settings.GridLines = GridLines.Both;
            GridLookupColor.GridView.Settings.GridLines = GridLines.Both;
            TreeListOptimalStock.Settings.GridLines = GridLines.Both;
            TreeListOptimalStockWithProducts.Settings.GridLines = GridLines.Both;
            GridLookupSupplier.GridView.Settings.GridLines = GridLines.Both;
            ASPxGridViewProducts.Settings.GridLines = GridLines.Both;
            ASPxGridViewProducts.SettingsEditing.BatchEditSettings.KeepChangesOnCallbacks = DevExpress.Utils.DefaultBoolean.False;//Preview button batch update

            ASPxWebControl item = (ASPxWebControl)TreeListOptimalStockWithProducts;
            item.EncodeHtml = false;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Initialize();
            }

            TreeListOptimalStock.DataBind();
            TreeListOptimalStockWithProducts.DataBind();
            GridLookupSupplier.GridView.HtmlRowPrepared += new ASPxGridViewTableRowEventHandler(GridLookupSupplier_HtmlRowPrepared);            
        }

       

        private void Initialize()
        {
            GridLookupCategory.DataBind();
            GridLookupSupplier.DataBind();
            GetOptimalStockOrderDataProvider().SetOptimalStockStatuses(CheckModelValidation(GetDatabaseConnectionInstance().GetOptimalStockStatuses()));
            GridLookupCategory.Value = 1;//"KARTON";
            category = "KARTON";
            GridLookupColor.DataBind();
        }

        private bool AddOrEditEntityObject(bool add = false)
        {
            if (add)
            {
                model = GetOptimalStockOrderDataProvider().GetOptimalStockOrderModel();
                model.NarociloOptimalnihZalogID = 0;
                model.NarociloOptimalnihZalogPozicija.ToList().ForEach(a => a.NarociloOptimalnihZalogPozicijaID = 0);
            }
            else if (!add && model == null)
            {
                model = GetOptimalStockOrderDataProvider().GetOptimalStockOrderModel();
            }

            model.DatumOddaje = DateEditSubmitOrder.Date.Equals(DateTime.MinValue) ? DateTime.Now : DateEditSubmitOrder.Date;
            model.Kolicina = GetOptimalStockOrderDataProvider().GetOptimalStockOrderModel().NarociloOptimalnihZalogPozicija.Sum(l => l.Kolicina);
            model.NarociloOddal = PrincipalHelper.GetUserPrincipal().ID;
            model.Naziv = String.IsNullOrEmpty(txtName.Text) ? "Naročilo optimalne zaloge za: " + GridLookupSupplier.Text.Trim() + ", dne: " + model.DatumOddaje.ToString("dd. MMMM yyyy") : txtName.Text;
            model.Opombe = MemoNotes.Text;

            string statusCode = DatabaseWebService.Common.Enums.Enums.StatusOfOptimalStock.ODDANO.ToString();
            model.StatusID = GetOptimalStockOrderDataProvider()
                .GetOptimalStockStatuses()
                .Where(stat => stat.Koda == statusCode).FirstOrDefault()
                .StatusNarocilaOptimalnihZalogID;


            model.tsIDOsebe = PrincipalHelper.GetUserPrincipal().ID;
            model.tsUpdateUserID = PrincipalHelper.GetUserPrincipal().ID;

            var returnModel = CheckModelValidation(GetDatabaseConnectionInstance().SaveOptimalStockOrder(model, false));

            if (returnModel != null)
            {
                model = returnModel;

                return true;
            }
            else
                return false;
        }

        private void ProcessUserAction()
        {
            bool isValid = true;
            bool confirm = false;

            isValid = AddOrEditEntityObject(true);
            confirm = true;

            if (isValid)
                ClearSessionsAndRedirect();
        }


        protected void btnCancelPopup_Click(object sender, EventArgs e)
        {
            ClearSessionsAndRedirect();
        }

        protected void btnConfirmPopup_Click(object sender, EventArgs e)
        {
            ProcessUserAction();
        }

        private void ClearSessionsAndRedirect(bool isIDDeleted = false, bool stayOnForm = false, bool submitOrder = false)
        {
            string redirectString = "";
            List<QueryStrings> queryStrings = new List<QueryStrings> {
                new QueryStrings() { Attribute = Enums.QueryStringName.recordId.ToString(), Value = model != null ?model.NarociloOptimalnihZalogID.ToString():"0" }
            };


            if (isIDDeleted)
                redirectString = "OptimalStockOrderTable.aspx";
            else
                redirectString = GenerateURI("OptimalStockOrderTable.aspx", queryStrings);

            /* if (stayOnForm)
             {
                 queryStrings.Add(new QueryStrings { Attribute = Enums.QueryStringName.action.ToString(), Value = ((int)Enums.UserAction.Edit).ToString() });
                 redirectString = GenerateURI("OptimalStockOrderForm.aspx", queryStrings);
             }*/

            List<Enums.OptimalStockOrderSession> list = Enum.GetValues(typeof(Enums.OptimalStockOrderSession)).Cast<Enums.OptimalStockOrderSession>().ToList();

            ClearAllSessions(list, redirectString, IsCallback);
        }


        #region Step 1

        #region Callback's
        protected void CallbackPanelTreeHierarchy_Callback(object sender, CallbackEventArgsBase e)
        {
            if (e.Parameter == "CategoryChanged")
            {
                category = GridLookupCategory.Text;
                GridLookupColor.DataBind();
                categoryChanged = true;
                TreeListOptimalStock.UnselectAll();
                TreeListOptimalStock.DataBind();
            }
            else if (e.Parameter == "ColorChanged")
            {
                colorChanged = true;
                TreeListOptimalStock.UnselectAll();
                TreeListOptimalStock.DataBind();
            }
            else if (e.Parameter == "PositiveValues")
            {
                FilterPositiveValues(TreeListOptimalStock);
            }
            else if (e.Parameter == "NegativeValues")
            {
                FilterNegativeValues(TreeListOptimalStock);
            }
            else if (e.Parameter == "AllValues")
            {
                NoFilter(TreeListOptimalStock);
            }
        }
        #endregion

        #region DataBindings
        protected void GridLookupColor_DataBinding(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(category)) return;
            category = category.Trim().Replace(" ", "%20");
            var list = CheckModelValidation(GetDatabaseConnectionInstance().GetColorListByCategory(category));
            if (list != null)
            {
                (sender as ASPxGridLookup).DataSource = list;

                if (list.Count == 1)
                {
                    GridLookupColor.Value = list[0].TempID;
                }
            }
        }

        protected void GridLookupCategory_DataBinding(object sender, EventArgs e)
        {
            var list = CheckModelValidation(GetDatabaseConnectionInstance().GetCategoryList());
            if (list != null)
                (sender as ASPxGridLookup).DataSource = list;
        }

        protected void TreeListOptimalStock_DataBinding(object sender, EventArgs e)
        {
            var list = GetOptimalStockOrderDataProvider().GetOptimalStockTreeHierarchy();

            if (list == null || categoryChanged || colorChanged)
                list = CheckModelValidation(GetDatabaseConnectionInstance().GetOptimalStockTree(GridLookupCategory.Text.Trim().Replace(" ", "%20"), GridLookupColor.Text));

            (sender as ASPxTreeList).DataSource = list;

            GetOptimalStockOrderDataProvider().SetOptimalStockTreeHierarchy(list);
        }

        protected void TreeListOptimalStock_DataBound(object sender, EventArgs e)
        {
            TreeListOptimalStock.ExpandAll();

            ASPxTreeList list = sender as ASPxTreeList;
            if (list.GetSelectedNodes().Count <= 0 && list.Nodes.Count > 0)
            {
                for (int i = 0; i < list.Nodes.Count; i++)
                {
                    ProcessNodes(list.Nodes[i], "VsotaZalNarRazlikaOpt");
                }
            }
        }
        #endregion

        #region OtherEvents (HtmlDataCellPrepared, HtmlRowPrepared)
        protected void TreeListOptimalStock_HtmlDataCellPrepared(object sender, TreeListHtmlDataCellEventArgs e)
        {
            if (e.Column.FieldName != "VsotaZalNarRazlikaOpt") return;

            if (Convert.ToDecimal(e.CellValue) < 0)
                e.Cell.BackColor = Color.Tomato;
        }

        protected void TreeListOptimalStock_HtmlRowPrepared(object sender, TreeListHtmlRowEventArgs e)
        {
            if (Convert.ToDecimal(e.GetValue("VsotaZalNarRazlikaOpt")) < 0)
                e.Row.BackColor = Color.Tomato;
        }

        void GridLookupSupplier_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {
            if ((e.RowType != GridViewRowType.Data))
                return;

            if (e.GetValue("HasStock").ToString() == "1")
                e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#dff0d8");
        }
        #endregion

        #endregion


        #region Step 2

        #region Callback's
        protected void CallbackPanelTreeHierarchyWithProducts_Callback(object sender, CallbackEventArgsBase e)
        {
            if (e.Parameter == "GetProductForSelectedOptimalStock")
            {
                var selectedNodes = TreeListOptimalStock.GetSelectedNodes().Where(n => !n.HasChildren).ToList();//da bomo lažje pridobili del drevesa kjer je bil izbran list, iz tega seznama pridobimo node ki imajo lastnost HasChildren = false

                //pridobi celotno hierarhično strukturo za izbrane liste
                var filteredTree = FilterTreeHierarchy(selectedNodes, GetOptimalStockOrderDataProvider().GetOptimalStockTreeHierarchy());

                // 
                //var treeWithProducts = CheckModelValidation(GetDatabaseConnectionInstance().GetProductsForSelectedOptimalStock(GridLookupColor.Text, filteredTree));
                hlpOptimalStockOrderModel hlp = new hlpOptimalStockOrderModel();
                hlp.SubCategoryWithProducts = filteredTree;

                var hlpReturn = CheckModelValidation(GetDatabaseConnectionInstance().GetProductsForSelectedOptimalStock(GridLookupColor.Text, hlp));
                if (hlpReturn != null)
                {
                    GetOptimalStockOrderDataProvider().SetSupplierList(hlpReturn.Suppliers);
                    GetOptimalStockOrderDataProvider().SetOptimalStockTreeHierarchyWithProducts(hlpReturn.SubCategoryWithProducts);
                }
                TreeListOptimalStockWithProducts.DataBind();
                GridLookupSupplier.DataBind();
            }
            else if (e.Parameter.Contains("FilterProductsBySupplier"))
            {
                //TreeListOptimalStockWithProducts.UnselectAll();
                filterProductsBySupplier = true;
                string searchString = GridLookupSupplier.Text;
                var treeList = GetOptimalStockOrderDataProvider().GetOptimalStockTreeHierarchyWithProducts();

                var selectedNodes = TreeListOptimalStockWithProducts.GetSelectedNodes();

                // da obdržimo stare podatje za prikaz brez dobavitelja
                List<OptimalStockTreeHierarchy> filteredTreeList = new List<OptimalStockTreeHierarchy>();
                filteredTreeList = CommonMethods.DeepCopy(treeList);

                if (e.Parameter.Contains("_RefreshTreeList"))//če stusne gumb Osveži za preverjanje izbrani podskupin če imajo iskanega dobavitelja za artikel.
                {
                    hlpOptimalStockOrderModel hlp = new hlpOptimalStockOrderModel();
                    hlp.SubCategoryWithProducts = FilterTreeHierarchy(selectedNodes, filteredTreeList);
                    var hlpReturn = CheckModelValidation(GetDatabaseConnectionInstance().GetProductsForSelectedOptimalStock(GridLookupColor.Text, hlp));
                    if (hlpReturn != null)
                    {
                        filteredTreeList = hlpReturn.SubCategoryWithProducts;
                    }
                }

                List<OptimalStockTreeHierarchy> products = new List<OptimalStockTreeHierarchy>();

                foreach (var leaf in selectedNodes)
                {
                    int leafID = CommonMethods.ParseInt(leaf.Key);
                    var node = filteredTreeList.Where(ost => ost.ID == leafID).FirstOrDefault();//iz drevesa pridobimo list vozlišča ki ga je uporabnik izbral.
                    if (node != null)
                    {
                        node.IsProduct = true;
                        products.Add(node);
                    }
                }

                //preverimo kateri produkti imajo dobavitlejja, ki ga filtriramo. Če izbrana podskupina nima dobavitelja za ta produkt potem moramo javiti uporabniku da more odpreti šifro v Pantehonu.
                products = GetFilteredProductsBySupplier(searchString, products);

                bool createEntityModel = false;

                List<OptimalStockTreeHierarchy> filteredList = null;

                if (products != null && !String.IsNullOrEmpty(searchString))
                {
                    filteredList = new List<OptimalStockTreeHierarchy>();

                    foreach (var product in products)
                    {
                        var ParentProduct = treeList.Where(ost => ost.ID == product.ParentID).FirstOrDefault();//se pomaknemo en nivo višje po drevesu
                        decimal ZaokrozenoNarocilo = (ParentProduct != null ? ParentProduct.VsotaZalNarRazlikaOpt : 0);
                        ZaokrozenoNarocilo = ParentProduct.VsotaZalNarRazlikaOpt * (-1);
                        ZaokrozenoNarocilo = (ZaokrozenoNarocilo < 500 ? 1000 : ZaokrozenoNarocilo);
                        ZaokrozenoNarocilo = ZaokrozenoNarocilo % 1000 >= 500 ? ZaokrozenoNarocilo + 1000 - ZaokrozenoNarocilo % 1000 : ZaokrozenoNarocilo - ZaokrozenoNarocilo % 1000;
                        product.KolicinaNarocilo = ZaokrozenoNarocilo;

                        if (product.Product.ChildProducts.Count > 0)
                        {
                            var productsForSubGroupBySupplier = (product.Product.ChildProducts.Where(p => p.DOBAVITELJ.Contains(searchString)).ToList());
                            productsForSubGroupBySupplier = productsForSubGroupBySupplier.OrderByDescending(s => s.TrenutnaZaloga).ToList();
                            if (productsForSubGroupBySupplier.Count > 0)
                            {
                                if (productsForSubGroupBySupplier.Count == 1)
                                    product.Name = productsForSubGroupBySupplier[0].NAZIV;
                                else if (productsForSubGroupBySupplier.Count > 1)
                                {
                                    int cnt = productsForSubGroupBySupplier.Count - 1;
                                    product.Name = productsForSubGroupBySupplier[0].NAZIV + " (" + cnt.ToString() + " artikel/ov)";
                                }
                                product.Product.NAZIV = productsForSubGroupBySupplier[0].NAZIV;
                                product.Product.IDENT = productsForSubGroupBySupplier[0].IDENT;
                                product.KolicinaZaloga = Convert.ToDecimal(productsForSubGroupBySupplier[0].TrenutnaZaloga);
                                product.KolicinaOptimalna = 0;
                            }
                            filteredList.Add(product);//dodamo list drevesa v novi seznam
                        }

                        var node = product;
                        do
                        {
                            node = filteredTreeList.Where(ost => ost.ID == node.ParentID).FirstOrDefault();//se pomaknemo en nivo višje po drevesu

                            var nodeInReturnList = filteredList.Where(ost => ost.ID == node.ID).FirstOrDefault();//preverimo če ta node že obstaja v seznamu ki ga bo metoda vrnila. Če ne ga dodamo.

                            if (nodeInReturnList == null)
                                filteredList.Add(node);
                        }
                        while (node.ParentID > 0);
                    }

                    string valid = ValidateSelectedSubGroups(filteredList, searchString.Trim());
                    if (String.IsNullOrEmpty(valid))
                    {
                        CallbackPanelTreeHierarchyWithProducts.JSProperties["cpShowSubmitOrderButton"] = true;//Uporabnik ima možnost oddati naročilo že v drugem koraku
                        createEntityModel = true;
                    }
                    else
                        CallbackPanelTreeHierarchyWithProducts.JSProperties["cpErrorOpenNewCodeForProduct"] = valid;

                    GetOptimalStockOrderDataProvider().SetOptimalStockTreeHierarchyWithProductsFilterBySupplier(filteredList);
                }
                else
                    GetOptimalStockOrderDataProvider().SetOptimalStockTreeHierarchyWithProductsFilterBySupplier(null);

                TreeListOptimalStockWithProducts.DataBind();

                if (createEntityModel)
                    BindProductsAndCreateOrderEntity();//Kreiramo in napolnimo OptimalStockOrderModel model z pozicijami in podatki. Kajti na tem koraku se že lahko izdela naročilnica
            }
            else if (e.Parameter.Contains("PopupChildProducts;"))
            {
                string[] split = e.Parameter.Split(';');
                int id = CommonMethods.ParseInt(split[1]);
                string supplier = GridLookupSupplier.Text;
                if (!String.IsNullOrEmpty(supplier))
                {
                    //var treeList = GetOptimalStockOrderDataProvider().GetOptimalStockTreeHierarchyWithProducts();
                    var treeList = GetOptimalStockOrderDataProvider().GetOptimalStockTreeHierarchyWithProductsFilterBySupplier();
                    var clickedNode = treeList.Where(tl => tl.ID == id).FirstOrDefault();
                    if (clickedNode != null && clickedNode.IsProduct && !String.IsNullOrEmpty(GridLookupSupplier.Text))
                    {
                        //OpenPopup and show products. Set session from main product
                        clickedNode.Product.DOBAVITELJ = supplier;

                        AddValueToSession(Enums.OptimalStockOrderSession.MainProductPopup, clickedNode);
                        PopupControlMainProductProductsClick.ShowOnPageLoad = true;
                    }
                    else
                        return;
                }
                else
                {
                    var treelistWithProducts = GetOptimalStockOrderDataProvider().GetOptimalStockTreeHierarchyWithProducts();
                    var clickedNode = treelistWithProducts.Where(twp => twp.ID == id).FirstOrDefault();

                    if (clickedNode != null && clickedNode.IsProduct && (clickedNode.Product.AllSubCategories != null && clickedNode.Product.AllSubCategories.Count > 0))
                    {
                        // preverimo nosilca in v seznamu ne prikažemo nosilca
                        int iIndex1 = clickedNode.Name.IndexOf(" (");
                        if (iIndex1 > -1)
                        {
                            string sNameMainProduct = clickedNode.Name.Substring(0, iIndex1);
                            clickedNode.Name = sNameMainProduct;
                        }

                            foreach (var item in clickedNode.Product.AllSubCategories)
                        {
                            if (!treelistWithProducts.Exists(c => c.Name == item.NazivPodKategorije))
                            {
                                int newId = treelistWithProducts.Max(m => m.ID) + 1;
                                //Dodamo produkt pod starša
                                treelistWithProducts.Add(new OptimalStockTreeHierarchy
                                {
                                    ID = newId,
                                    Name = item.NazivPodKategorije,
                                    ParentID = clickedNode.ParentID,
                                    KolicinaZaloga = clickedNode.TrenutnaZalogaProdukt,
                                    KolicinaNarocilo = clickedNode.KolicinaNarocilo,
                                    Product = new GetProductsByOptimalStockValuesModel { ChildProducts = new List<GetProductsByOptimalStockValuesModel>(item.ChildProducts), NAZIV = item.NazivPodKategorije }
                                });
                            }
                        }

                        GetOptimalStockOrderDataProvider().SetOptimalStockTreeHierarchyWithProducts(treelistWithProducts);
                        TreeListOptimalStockWithProducts.DataBind();
                    }
                    else
                        return;
                }
            }
            else if (e.Parameter == "AddChildProductToList")
            {
                var treeList = GetOptimalStockOrderDataProvider().GetOptimalStockTreeHierarchyWithProductsFilterBySupplier();

                var products = treeList
                    .Where(ost => ost.IsProduct)
                    .ToList();
                foreach (var item in products)
                {
                    if (item.Product.SelectedChildProducts != null)
                    {
                        foreach (var childProd in item.Product.SelectedChildProducts)
                        {

                            if (!treeList.Exists(c => c.Name == childProd.NAZIV))
                            {
                                int id = treeList.Max(m => m.ID) + 1;
                                //Dodamo produkt pod starša
                                treeList.Add(new OptimalStockTreeHierarchy
                                {
                                    ID = id,
                                    Name = childProd.NAZIV,
                                    ParentID = item.ID,
                                    KolicinaZaloga = childProd.TrenutnaZaloga,
                                    KolicinaNarocilo = item.KolicinaNarocilo,
                                });
                            }
                        }
                    }
                }
                GetOptimalStockOrderDataProvider().SetOptimalStockTreeHierarchyWithProductsFilterBySupplier(treeList);
                TreeListOptimalStockWithProducts.DataBind();

                BindProductsAndCreateOrderEntity();
            }
            else if (e.Parameter == "StartSearchPopup")
            {
                PopupControlSearchSupplier.ShowOnPageLoad = true;
                //List<ClientSimpleModel> model;
                //model = CheckModelValidation(GetDatabaseConnectionInstance().GetSupplierByName(txtSupplierSearch.Text.Trim()));

                //if (model.Count > 1)
                //{
                //    PopupControlSearchSupplier.ShowOnPageLoad = true;
                //}
                //else if (model.Count == 1)
                //{
                //    AddSupplierToForm(model[0]);
                //    GridLookupKontaktnaOSeba.DataBind();
                //}
                //else if (model.Count == 0)
                //{
                //    PopupControlSearchSupplier.ShowOnPageLoad = true;
                //}
            }
        }


        protected void PopupControlSearchSupplier_WindowCallback(object source, PopupWindowCallbackArgs e)
        {
            //RemoveSession(Enums.CommonSession.SearchString);
            //RemoveSession(Enums.InquirySession.SelectedSupplierPopup);
            //RemoveSession(Enums.InquirySession.SupplierListModel);
        }
        #endregion

        #region DataBindings
        protected void TreeListOptimalStockWithProducts_DataBinding(object sender, EventArgs e)
        {
            var list = GetOptimalStockOrderDataProvider().GetOptimalStockTreeHierarchyWithProducts();
            var filteredListBySupplier = GetOptimalStockOrderDataProvider().GetOptimalStockTreeHierarchyWithProductsFilterBySupplier();

            if (filteredListBySupplier != null)
            {
                (sender as ASPxTreeList).DataSource = filteredListBySupplier;
            }
            else
            {
                (sender as ASPxTreeList).DataSource = list;
            }
        }

        protected void TreeListOptimalStockWithProducts_DataBound(object sender, EventArgs e)
        {
            SetNodeSelectionSettings();
            TreeListOptimalStockWithProducts.ExpandAll();

            //if (filterProductsBySupplier)
            //{
                ASPxTreeList list = sender as ASPxTreeList;
                if (list.GetSelectedNodes().Count <= 0 && list.Nodes.Count > 0)
                {
                    for (int i = 0; i < list.Nodes.Count; i++)
                    {
                        ProcessNodes(list.Nodes[i], "VsotaZalNarRazlikaOpt");
                    }
                }
                filterProductsBySupplier = false;
            //}
        }

        protected void GridLookupSupplier_DataBinding(object sender, EventArgs e)
        {
            var list = GetOptimalStockOrderDataProvider().GetSupplierList();

            (sender as ASPxGridLookup).DataSource = list;
        }
        #endregion

        #region OtherEvents (PopupControl_WindowCallback, HtmlrowPrepared)
        protected void PopupControlMainProductProductsClick_WindowCallback(object source, PopupWindowCallbackArgs e)
        {
            RemoveSession(Enums.OptimalStockOrderSession.MainProductPopup);
        }

        protected void TreeListOptimalStockWithProducts_HtmlRowPrepared(object sender, TreeListHtmlRowEventArgs e)
        {
            if (e.RowKind != TreeListRowKind.Data) return;

            var filteredTreelist = GetOptimalStockOrderDataProvider().GetOptimalStockTreeHierarchyWithProductsFilterBySupplier();
            if (filteredTreelist != null)
            {
                int id = CommonMethods.ParseInt(e.NodeKey);

                var node = filteredTreelist.Where(tl => tl.ID == id).FirstOrDefault();
                if (node != null && node.OpenNewCodeForProductInPantheon)
                    e.Row.ForeColor = Color.Tomato;
            }
        }
        #endregion
        #endregion


        #region Step 3

        #region Callback's
        protected void CallbackPanelProducts_Callback(object sender, CallbackEventArgsBase e)
        {
            if (e.Parameter == "BindSelectedProducts")
            {
                FillBasicDataForOrder();
                BindProductsAndCreateOrderEntity();
            }
        }
        #endregion

        #region DataBindings
        protected void ASPxGridViewProducts_DataBinding(object sender, EventArgs e)
        {
            var optimalStockOrder = GetOptimalStockOrderDataProvider().GetOptimalStockOrderModel();

            if (optimalStockOrder != null)
            {
                (sender as ASPxGridView).DataSource = optimalStockOrder.NarociloOptimalnihZalogPozicija;
            }
        }

        protected void ASPxGridViewProducts_DataBound(object sender, EventArgs e)
        {
            ASPxGridView gridView = sender as ASPxGridView;
            gridView.JSProperties["cpQuantitySum"] = gridView.GetTotalSummaryValue(gridView.TotalSummary["Kolicina"]);
        }
        #endregion

        #region OtherEvents (BatchUpdate, HtmlRowPrepared)
        protected void ASPxGridViewProducts_BatchUpdate(object sender, ASPxDataBatchUpdateEventArgs e)
        {
            OptimalStockOrderPositionModel position = null;
            Type myType = typeof(OptimalStockOrderPositionModel);
            List<PropertyInfo> myPropInfo = myType.GetProperties().ToList();
            var productsPositions = GetOptimalStockOrderDataProvider().GetOptimalStockOrderModel().NarociloOptimalnihZalogPozicija.ToList();

            foreach (ASPxDataUpdateValues item in e.UpdateValues)
            {
                position = new OptimalStockOrderPositionModel();

                foreach (DictionaryEntry obj in item.Keys)//we set table ID
                {
                    PropertyInfo info = myPropInfo.Where(prop => prop.Name.Equals(obj.Key.ToString())).FirstOrDefault();

                    if (info != null)
                    {
                        position = productsPositions.Where(p => p.NarociloOptimalnihZalogPozicijaID == (int)obj.Value).FirstOrDefault();
                        break;
                    }
                }

                foreach (DictionaryEntry obj in item.NewValues)
                {
                    PropertyInfo info = myPropInfo.Where(prop => prop.Name.Equals(obj.Key.ToString())).FirstOrDefault();

                    if (info != null)
                        info.SetValue(position, obj.Value);
                }
            }

            //Brisanje pozicija naročila optimalnih zalog
            foreach (ASPxDataDeleteValues item in e.DeleteValues)
            {
                position = new OptimalStockOrderPositionModel();

                foreach (DictionaryEntry obj in item.Keys)//we set table ID
                {
                    PropertyInfo info = myPropInfo.Where(prop => prop.Name.Equals(obj.Key.ToString())).FirstOrDefault();

                    if (info != null)
                    {
                        position = productsPositions.Where(p => p.NarociloOptimalnihZalogPozicijaID == (int)obj.Value).FirstOrDefault();
                        break;
                    }
                }

                productsPositions.Remove(position);
            }

            var oso = GetOptimalStockOrderDataProvider().GetOptimalStockOrderModel();
            oso.NarociloOptimalnihZalogPozicija = productsPositions;

            GetOptimalStockOrderDataProvider().SetOptimalStockOrderModel(oso);

            e.Handled = true;
        }

        protected void ASPxGridViewProducts_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {
            var optimalStockOrder = GetOptimalStockOrderDataProvider().GetOptimalStockOrderModel();
            if (optimalStockOrder != null && e.KeyValue != null)
            {
                int id = CommonMethods.ParseInt(e.KeyValue);
                string barva = optimalStockOrder.NarociloOptimalnihZalogPozicija.Where(pos => pos.NarociloOptimalnihZalogPozicijaID == id).FirstOrDefault().Barva;
                if (!String.IsNullOrEmpty(barva))
                    e.Row.BackColor = ColorTranslator.FromHtml(barva);
            }
        }
        #endregion

        #endregion

        #region HelperMethods

        #region Tree data structure related
        private List<OptimalStockTreeHierarchy> FilterTreeHierarchy(List<TreeListNode> listOfLeafs, List<OptimalStockTreeHierarchy> sessionTreeList)
        {
            var optimalStockTree = sessionTreeList;
            List<OptimalStockTreeHierarchy> returnList = new List<OptimalStockTreeHierarchy>();

            foreach (var leaf in listOfLeafs)
            {
                int leafID = CommonMethods.ParseInt(leaf.Key);
                var node = optimalStockTree.Where(ost => ost.ID == leafID).FirstOrDefault();//iz drevesa pridobimo list vozlišča ki ga je uporabnik izbral.
                if (node != null)
                {
                    returnList.Add(node);//dodamo list drevesa v novi seznam

                    do
                    {
                        node = optimalStockTree.Where(ost => ost.ID == node.ParentID).FirstOrDefault();//se pomaknemo en nivo višje po drevesu

                        var nodeInReturnList = returnList.Where(ost => ost.ID == node.ID).FirstOrDefault();//preverimo če ta node že obstaja v seznamu ki ga bo metoda vrnila. Če ne ga dodamo.

                        if (nodeInReturnList == null)
                            returnList.Add(node);
                    }
                    while (node.ParentID > 0);

                }
            }

            return returnList;
        }

        private string GetCategoriyNameFromProduct(OptimalStockTreeHierarchy productLeaf)
        {
            var list = GetOptimalStockOrderDataProvider().GetOptimalStockTreeHierarchyWithProducts();
            while (productLeaf.ParentID > 0)
            {
                productLeaf = list.Where(l => l.ID == productLeaf.ParentID).FirstOrDefault();
            }

            return productLeaf.Name;
        }

        private List<OptimalStockTreeHierarchy> GetFilteredProductsBySupplier(string sSupplierName, List<OptimalStockTreeHierarchy> lProductList)
        {
            for (int i = lProductList.Count - 1; i >= 0; i--)
            {
                OptimalStockTreeHierarchy item = lProductList[i];
                if (item != null)
                {
                    if (item.Product.ChildProducts.Where(sp => sp.DOBAVITELJ.Contains(sSupplierName)).ToList().Count == 0)
                    {
                        //lProductList.RemoveAt(i);
                        item.Name += "  Potrebno je odpreti šifro!";
                        item.OpenNewCodeForProductInPantheon = true;
                    }
                }
            }

            return lProductList;
        }

        private string ValidateSelectedSubGroups(List<OptimalStockTreeHierarchy> filteredList, string supplier)
        {
            var selectedNodes = filteredList.Where(ftl => ftl.OpenNewCodeForProductInPantheon).ToList();
            string error = "";
            if (selectedNodes != null && selectedNodes.Count > 0)
            {
                error = "Potrebno je odpreti šifre za naslednje izdelke za dobavitelja <strong>" + supplier + "</strong><br /><ul class=\"d-flex justify-content-center\">";
                foreach (var item in selectedNodes)
                {
                    error += "<li>" + item.Name + "</li>";
                }
                error += "</ul>";
            }

            return error;
        }
        #endregion

        #region ASPxTreeList control related
        void SetNodeSelectionSettings()
        {
            TreeListNodeIterator iterator = TreeListOptimalStockWithProducts.CreateNodeIterator();
            TreeListNode node;
            while (true)
            {
                node = iterator.GetNext();
                if (node == null) break;

                node.AllowSelect = (node.Level == 6);
            }
        }

        private void ProcessNodes(TreeListNode startNode, string ColumnName)
        {
            if (startNode == null) return;
            TreeListNodeIterator iterator = new TreeListNodeIterator(startNode);

            while (iterator.Current != null)
            {
                iterator.Current.Selected = (filterProductsBySupplier ? true : ((Convert.ToDecimal(iterator.Current.GetValue(ColumnName)) < 0) ? true : false));
                iterator.GetNext();
            }
        }

        private void FilterNegativeValues(ASPxTreeList treeList)
        {
            treeList.FilterExpression = "[VsotaZalNarRazlikaOpt] < 0";
            treeList.DataBind();
        }

        private void FilterPositiveValues(ASPxTreeList treeList)
        {
            treeList.FilterExpression = "[VsotaZalNarRazlikaOpt] > 0";
            treeList.DataBind();
        }

        private void NoFilter(ASPxTreeList treeList)
        {
            treeList.FilterExpression = "";
            treeList.DataBind();
        }
        #endregion

        #region Data information related
        private void BindProductsAndCreateOrderEntity()
        {
            var list = GetOptimalStockOrderDataProvider().GetOptimalStockTreeHierarchyWithProductsFilterBySupplier();
            var selectedNodes = TreeListOptimalStockWithProducts.GetSelectedNodes().Where(n => !n.HasChildren || n.Level == 6).ToList();

            var selectedProductsFromMainProduct = GetOptimalStockOrderDataProvider().GetSelectedChildProducts();//seznam vseh izdelkov, ki smo jih dodali iz nosilnih (izbirali smo jih preko popup-a).

            OptimalStockOrderModel optimalStockOrder = new OptimalStockOrderModel();
            optimalStockOrder.NarociloOptimalnihZalogID = 0;

            optimalStockOrder.StrankaID = CheckModelValidation(GetDatabaseConnectionInstance().GetClientByNameOrInsert(GridLookupSupplier.Text.Trim()));
            optimalStockOrder.NarociloOddal = PrincipalHelper.GetUserPrincipal().ID;

            List<OptimalStockOrderPositionModel> positions = new List<OptimalStockOrderPositionModel>();
            foreach (var item in selectedNodes)
            {
                int itemID = CommonMethods.ParseInt(item.Key);
                var prod = list.Where(l => l.ID == itemID).FirstOrDefault();
                if (prod != null)
                {
                    OptimalStockOrderPositionModel osop = new OptimalStockOrderPositionModel();
                    osop.KategorijaNaziv = (prod != null) ? GetCategoriyNameFromProduct(prod) : "";
                    osop.Kolicina = prod.KolicinaNarocilo;
                    osop.NarociloOptimalnihZalogPozicijaID = positions.Count > 0 ? positions.Max(m => m.NarociloOptimalnihZalogPozicijaID) + 1 : 1;//Nastavimo začasne Id-je da bomo lahko prikazovali vrednosti v grid-u
                                                                                                                                                   //ob shranjevanje je potrebno te id-je nastavit na 0!!!
                    osop.NazivArtikla = prod.Product.NAZIV;
                    osop.IdentArtikla_P = prod.Product.IDENT;
                    osop.NazivPodKategorija = prod.NazivPodkategorije;
                    osop.VsotaZalNarRazlikaOpt = prod.VsotaZalNarRazlikaOpt;

                    positions.Add(osop);
                }
            }

            if (selectedProductsFromMainProduct != null && selectedProductsFromMainProduct.Count > 0)
            {
                foreach (var item in selectedProductsFromMainProduct)
                {
                    foreach (var prod in item.Product.SelectedChildProducts)
                    {
                        OptimalStockOrderPositionModel osop = new OptimalStockOrderPositionModel();
                        osop.KategorijaNaziv = GetCategoriyNameFromProduct(item);
                        osop.Kolicina = item.KolicinaNarocilo;
                        osop.NarociloOptimalnihZalogPozicijaID = positions.Count > 0 ? positions.Max(m => m.NarociloOptimalnihZalogPozicijaID) + 1 : 1;//Nastavimo začasne Id-je da bomo lahko prikazovali vrednosti v grid-u
                                                                                                                                                       //ob shranjevanje je potrebno te id-je nastavit na 0!!!
                        osop.NazivArtikla = prod.NAZIV;
                        osop.IdentArtikla_P = prod.IDENT;
                        osop.NazivPodKategorija = prod.NazivPodkategorije;
                        osop.VsotaZalNarRazlikaOpt = item.VsotaZalNarRazlikaOpt;

                        positions.Add(osop);
                    }
                }
            }

            // grupiraj po nazivu podkategorija
            var groupedBySubCategorie = positions.Where(w => !String.IsNullOrEmpty(w.NazivPodKategorija)).GroupBy(g => g.NazivPodKategorija).ToList();
            RandomColorHelper colorHelper = new RandomColorHelper();
            foreach (var item in groupedBySubCategorie)
            {
                var listOfGroupedProducts = item.ToList();
                if (listOfGroupedProducts != null && listOfGroupedProducts.Count > 1)
                {
                    string color = colorHelper.GetNextUnselectedColor();
                    foreach (var obj in listOfGroupedProducts)
                    {
                        obj.Barva = color;
                    }
                }
            }

            optimalStockOrder.NarociloOptimalnihZalogPozicija = new List<OptimalStockOrderPositionModel>(positions);

            GetOptimalStockOrderDataProvider().SetOptimalStockOrderModel(optimalStockOrder);
            ASPxGridViewProducts.DataBind();

            FillBasicDataForOrder();
        }

        private void FillBasicDataForOrder()
        {
            txtSupplier.Text = GridLookupSupplier.Text;
            DateEditSubmitOrder.Date = DateTime.Now;
            txtSumQuantity.Text = GetOptimalStockOrderDataProvider().GetOptimalStockOrderModel().NarociloOptimalnihZalogPozicija.Sum(l => l.Kolicina).ToString("N2");
            txtName.Text = "Naročilo optimalne zaloge za: " + txtSupplier.Text.Trim() + ", dne: " + DateEditSubmitOrder.Date.ToString("dd. MMMM yyyy");
        }
        #endregion

        #endregion

        

        
    }
}