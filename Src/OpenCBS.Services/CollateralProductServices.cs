using System;
using System.Collections.Generic;
using OpenCBS.CoreDomain;
using OpenCBS.CoreDomain.Products.Collaterals;
using OpenCBS.ExceptionsHandler;
using OpenCBS.Manager;

namespace OpenCBS.Services
{
    public class CollateralProductServices : MarshalByRefObject
	{
		private CollateralProductManager collateralProductManager;
        //private User user;

		public CollateralProductServices(CollateralProductManager collateralProductManager)
		{
			this.collateralProductManager = collateralProductManager;
		}

		public CollateralProductServices(User pUser)
		{
            //user = pUser;
			collateralProductManager = new CollateralProductManager(pUser);
		}

		public CollateralProductServices(string pTestDB)
		{
			collateralProductManager = new CollateralProductManager(pTestDB);
		}

        public void AddCollateralProduct(CollateralProduct collateralProduct)
        {
            var productName = collateralProduct.Name;
            CheckCollateralProductNameExistance(productName);
            collateralProductManager.AddCollateralProduct(collateralProduct);
        }

        private void CheckCollateralProductNameExistance(string productName)
        {
            var exists = collateralProductManager.PorductExists(productName);
            if (exists) throw new OctopusCollateralSaveException(OctopusCollateralSaveExceptionEnum.AlreadyExist);
        }

        public void AddCollateralProperty(int collateralProductId, CollateralProperty collateralProperty)
        {
            collateralProductManager.AddCollateralProperty(collateralProductId, collateralProperty);
        }

        /*public void AddCollateralProductToContract(ContractCollateral contractProduct)
        {
            collateralProductManager.AddCollateralProductToContract(contractProduct);
        }*/

        public List<CollateralProduct> SelectAllCollateralProducts(bool pShowAlsoDeleted)
        {
            return collateralProductManager.SelectAllCollateralProducts(pShowAlsoDeleted);
        }

        public CollateralProduct SelectCollateralProduct(int collateralProductId)
        {
            return collateralProductManager.SelectCollateralProduct(collateralProductId);
        }

        public CollateralProduct SelectCollateralProductByPropertyId(int propertyId)
        {
            return collateralProductManager.SelectCollateralProductByPropertyId(propertyId);
        }

        public void UpdateCollateralProduct(int productId, string oldName, string newName, string description)
        {
            if (!oldName.Equals(newName)) CheckCollateralProductNameExistance(newName);
            collateralProductManager.UpdateCollateralProduct(productId, newName, description);
        }

        public void DeleteCollateralProduct(int id)
        {
            collateralProductManager.DeleteCollateralProduct(id);
        }
	}
}
