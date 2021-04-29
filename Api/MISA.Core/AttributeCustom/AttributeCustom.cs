using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.AttributeCustom
{
    /// <summary>
    /// Attribute check required.
    /// </summary>
    /// CreatedBy: dbhuan (29/04/2021)
    [AttributeUsage(AttributeTargets.Property)]
    public class PropertyRequired: Attribute
    {
        /// <summary>
        /// Thông báo lỗi tùy chỉnh.
        /// </summary>
        public string MsgError = string.Empty;

        /// <summary>
        /// Tên sẽ được thay thế vào thông báo lỗi mặc định.
        /// </summary>
        public string Name = String.Empty;
        public PropertyRequired(string msgError = "", string name = "")
        {
            Name = name;
            MsgError = msgError;
        }
    }

    /// <summary>
    /// Attribute check max length
    /// </summary>
    /// CreatedBy: dbhuan (29/04/2021)
    [AttributeUsage(AttributeTargets.Property)]
    public class PropertyMaxLength: Attribute
    {
        /// <summary>
        /// Độ dài tối đa cho phép.
        /// </summary>
        public int MaxLength;

        /// <summary>
        /// Thông báo lỗi tùy chỉnh.
        /// </summary>
        public string MsgError = string.Empty;
        /// <summary>
        /// Tên sẽ được thay thế vào thông báo lỗi mặc định.
        /// </summary>
        public string Name = string.Empty;

        public PropertyMaxLength(int maxLength, string msgError = "", string name = "")
        {
            MaxLength = maxLength;
            MsgError = msgError;
            Name = name;
        }
    }
}
