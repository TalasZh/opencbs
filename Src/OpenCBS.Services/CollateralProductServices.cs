// Octopus MFS is an integrated suite for managing a Micro Finance Institution: 
// clients, contracts, accounting, reporting and risk
// Copyright © 2006,2007 OCTO Technology & OXUS Development Network
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public License along
// with this program; if not, write to the Free Software Foundation, Inc.,
// 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
//
// Website: http://www.opencbs.com
// Contact: contact@opencbs.com

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
            if (exists) throw new OpenCbsCollateralSaveException(OpenCbsCollateralSaveExceptionEnum.AlreadyExist);
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
