using DatabaseWebService.Models.Client;
using DatabaseWebService.ModelsNOZ.OptimalStockOrder;
using GrafolitNOZ.Common;
using GrafolitNOZ.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GrafolitNOZ.Helpers.DataProviders
{
    public class OptimalStockOrderDataProvider : ServerMasterPage
    {
        public void SetOptimalStockTreeHierarchy(List<OptimalStockTreeHierarchy> model)
        {
            AddValueToSession(Enums.OptimalStockOrderSession.OptimalStockTree, model);
        }

        public List<OptimalStockTreeHierarchy> GetOptimalStockTreeHierarchy()
        {
            if (SessionHasValue(Enums.OptimalStockOrderSession.OptimalStockTree))
                return (List<OptimalStockTreeHierarchy>)GetValueFromSession(Enums.OptimalStockOrderSession.OptimalStockTree);

            return null;
        }

        public void SetOptimalStockTreeHierarchyWithProducts(List<OptimalStockTreeHierarchy> model)
        {
            AddValueToSession(Enums.OptimalStockOrderSession.OptimalStockTreeWithProducts, model);
        }

        public void SetOptimalStockTreeHierarchyWithProductsNoSupplier(List<OptimalStockTreeHierarchy> model)
        {
            AddValueToSession(Enums.OptimalStockOrderSession.OptimalStockTreeWithProductsNoSupplier, model);
        }

        public List<OptimalStockTreeHierarchy> GetOptimalStockTreeHierarchyWithProducts()
        {
            if (SessionHasValue(Enums.OptimalStockOrderSession.OptimalStockTreeWithProducts))
                return (List<OptimalStockTreeHierarchy>)GetValueFromSession(Enums.OptimalStockOrderSession.OptimalStockTreeWithProducts);

            return null;
        }

        public List<OptimalStockTreeHierarchy> GetOptimalStockTreeHierarchyNoSupplier()
        {
            if (SessionHasValue(Enums.OptimalStockOrderSession.OptimalStockTreeWithProductsNoSupplier))
                return (List<OptimalStockTreeHierarchy>)GetValueFromSession(Enums.OptimalStockOrderSession.OptimalStockTreeWithProductsNoSupplier);

            return null;
        }




        public void SetSupplierList(List<ClientSimpleModel> model)
        {
            AddValueToSession(Enums.OptimalStockOrderSession.SupplierListModel, model);
        }

        public List<ClientSimpleModel> GetSupplierList()
        {
            if (SessionHasValue(Enums.OptimalStockOrderSession.SupplierListModel))
                return (List<ClientSimpleModel>)GetValueFromSession(Enums.OptimalStockOrderSession.SupplierListModel);

            return null;
        }

        public void SetOptimalStockTreeHierarchyWithProductsFilterBySupplier(List<OptimalStockTreeHierarchy> model)
        {
            AddValueToSession(Enums.OptimalStockOrderSession.OptimalStockTreeWithProductsFilterBySupplier, model);
        }

        public List<OptimalStockTreeHierarchy> GetOptimalStockTreeHierarchyWithProductsFilterBySupplier()
        {
            if (SessionHasValue(Enums.OptimalStockOrderSession.OptimalStockTreeWithProductsFilterBySupplier))
                return (List<OptimalStockTreeHierarchy>)GetValueFromSession(Enums.OptimalStockOrderSession.OptimalStockTreeWithProductsFilterBySupplier);

            return null;
        }

        public void SetOptimalStockOrderModel(OptimalStockOrderModel model)
        {
            AddValueToSession(Enums.OptimalStockOrderSession.OptimalStockOrderModel, model);
        }

        public OptimalStockOrderModel GetOptimalStockOrderModel()
        {
            if (SessionHasValue(Enums.OptimalStockOrderSession.OptimalStockOrderModel))
                return (OptimalStockOrderModel)GetValueFromSession(Enums.OptimalStockOrderSession.OptimalStockOrderModel);

            return null;
        }

        public void SetOptimalStockStatuses(List<OptimalStockOrderStatusModel> model)
        {
            AddValueToSession(Enums.OptimalStockOrderSession.OptimalStockStatuses, model);
        }

        public List<OptimalStockOrderStatusModel> GetOptimalStockStatuses()
        {
            if (SessionHasValue(Enums.OptimalStockOrderSession.OptimalStockStatuses))
                return (List<OptimalStockOrderStatusModel>)GetValueFromSession(Enums.OptimalStockOrderSession.OptimalStockStatuses);

            return null;
        }

        /// <summary>
        /// Shranimo seznam drevesnih vozlišč (nosilnih izdelkov) z izbranimi child izdelki. Ta seznam polnimo v popup-u
        /// </summary>
        /// <param name="model">Seznam drevesnih vozlišč (nosilnih izdelkov) z izbranimi child izdelki</param>
        public void SetSelectedChildProducts(List<OptimalStockTreeHierarchy> model)
        {
            AddValueToSession(Enums.OptimalStockOrderSession.SelectedChildProducts, model);
        }

        public List<OptimalStockTreeHierarchy> GetSelectedChildProducts()
        {
            if (SessionHasValue(Enums.OptimalStockOrderSession.SelectedChildProducts))
                return (List<OptimalStockTreeHierarchy>)GetValueFromSession(Enums.OptimalStockOrderSession.SelectedChildProducts);

            return null;
        }

        public void SetSearchedSupplierListModel(List<ClientSimpleModel> model)
        {
            AddValueToSession(Enums.OptimalStockOrderSession.SearchedSupplierList, model);
        }

        public List<ClientSimpleModel> GetSearchedSupplierListModel()
        {
            if (SessionHasValue(Enums.OptimalStockOrderSession.SupplierListModel))
                return (List<ClientSimpleModel>)GetValueFromSession(Enums.OptimalStockOrderSession.SearchedSupplierList);

            return null;
        }
    }
}