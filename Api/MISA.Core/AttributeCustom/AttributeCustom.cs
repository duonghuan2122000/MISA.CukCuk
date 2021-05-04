using System;

namespace MISA.Core.AttributeCustom
{
    /// <summary>
    /// Attribute check required.
    /// </summary>
    /// CreatedBy: dbhuan (29/04/2021)
    [AttributeUsage(AttributeTargets.Property)]
    public class PropertyRequired : Attribute
    {
        /// <summary>
        /// Thông báo lỗi tùy chỉnh.
        /// </summary>
        public string MsgError = string.Empty;

        /// <summary>
        /// Nguồn resource.
        /// </summary>
        public Type? ErrorResourceType { get; set; }

        /// <summary>
        /// Tên key trong resource.
        /// </summary>
        public string ErrorResourceName { get; set; }
    }

    /// <summary>
    /// Attribute check max length
    /// </summary>
    /// CreatedBy: dbhuan (29/04/2021)
    [AttributeUsage(AttributeTargets.Property)]
    public class PropertyMaxLength : Attribute
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
        /// Nguồn resource.
        /// </summary>
        public Type? ErrorResourceType { get; set; }

        /// <summary>
        /// Tên key trong resource.
        /// </summary>
        public string ErrorResourceName { get; set; }
    }
}
