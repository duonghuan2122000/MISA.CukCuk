using System;

namespace MISA.Core.Exceptions
{
    /// <summary>
    /// Class ngoại lệ của khách hàng.
    /// </summary>
    /// CreatedBy: dbhuan (28/04/2021)
    public class ClientException : Exception
    {
        public ClientException(string msg) : base(msg)
        {

        }
    }
}
