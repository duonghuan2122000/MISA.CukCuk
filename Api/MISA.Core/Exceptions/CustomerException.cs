using System;

namespace MISA.Core.Exceptions
{
    public class CustomerException : Exception
    {
        public CustomerException(string msg) : base(msg)
        {

        }

        public static void CheckCustomerCodeEmpty(string customerCode)
        {
            if (string.IsNullOrEmpty(customerCode))
            {
                /*var response = new
                {
                    devMsg = "Mã khách hàng không được để trống",
                    MISACode = "001"
                };*/
                throw new CustomerException("Mã khách hàng không được để trống.");
            }
        }

        public static void CheckCustomerIdEmpty(Guid customerId)
        {
            if (string.IsNullOrEmpty(customerId.ToString()))
            {
                throw new CustomerException("Id khách hàng không được để trống.");
            }
        }
    }
}
