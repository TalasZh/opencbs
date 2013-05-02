// LICENSE PLACEHOLDER

using System.Collections.Generic;
using OpenCBS.CoreDomain.Accounting;
using OpenCBS.Manager;
using OpenCBS.CoreDomain;

namespace OpenCBS.Services
{
    /// <summary>
    /// Service layer for Payment Methods
    /// </summary>
    public class PaymentMethodServices : Services
    {
        private PaymentMethodManager _paymentMethodManager;
        private readonly User _user = new User();

        public PaymentMethodServices(PaymentMethodManager paymentMethodManager)
        {
            _paymentMethodManager = paymentMethodManager;
        }

        public PaymentMethodServices(User user)
        {
            _user = user;
            _paymentMethodManager = new PaymentMethodManager(_user);
        }

        /// <summary>
        /// The method retrieves all payment methods from database
        /// </summary>
        /// <returns>List of PaymentMethod</returns>
        public List<PaymentMethod> GetAllPaymentMethods()
        {
            return _paymentMethodManager.SelectPaymentMethods();
        }

        public List<PaymentMethod> GetAllPaymentMethodsForClosure()
        {
            return _paymentMethodManager.SelectPaymentMethodsForClosure();
        }

        public List<PaymentMethod> GetAllPaymentMethodsOfBranch(int branchId)
        {
            return _paymentMethodManager.SelectPaymentMethodOfBranch(branchId);
        }

        /// <summary>
        /// The method retrieves payment method by id
        /// </summary>
        /// <param name="paymentMethodId">Id of a payment method</param>
        /// <returns>Returns an instant of the PaymentMethod</returns>
        public PaymentMethod GetPaymentMethodById(int paymentMethodId)
        {
            return _paymentMethodManager.SelectPaymentMethodById(paymentMethodId);
        }

        public PaymentMethod GetPaymentMethodByName(string paymentMethodName)
        {
            return _paymentMethodManager.SelectPaymentMethodByName(paymentMethodName);
        }

        public void AddPaymentMethodToBranch(PaymentMethod paymentMethod)
        {
            _paymentMethodManager.AddPaymentMethodToBranch(paymentMethod);
        }

        public void Delete(PaymentMethod paymentMethod)
        {
            _paymentMethodManager.DeletePaymentMethodFromBranach(paymentMethod);
        }

        public void Update(PaymentMethod paymentMethod)
        {
            _paymentMethodManager.UpdatePaymentMethodFromBranch(paymentMethod);
        }
    }
}
    
