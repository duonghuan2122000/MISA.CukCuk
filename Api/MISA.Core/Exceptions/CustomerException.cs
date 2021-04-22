using System;

namespace MISA.Core.Exceptions
{
    /// <summary>
    /// Class ngoại lệ của khách hàng.
    /// </summary>
    public class CustomerException : Exception
    {
        public CustomerException(string msg) : base(msg)
        {

        }

        /// <summary>
        /// Hàm kiểm tra sự tồn tại của mã khách hàng.
        /// </summary>
        /// <param name="customerCode">mã khách hàng cần kiểm tra.</param>
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

        /// <summary>
        /// Hàm kiểm tra sự tồn tại của id khách hàng.
        /// </summary>
        /// <param name="customerId">id của khách hàng.</param>
        public static void CheckCustomerIdEmpty(Guid customerId)
        {
            if (string.IsNullOrEmpty(customerId.ToString()))
            {
                throw new CustomerException("Id khách hàng không được để trống.");
            }
        }
    }
}
