using MISA.Core.AttributeCustom;
using System;

namespace MISA.Core.Entities
{
    /// <summary>
    /// Class khách hàng.
    /// </summary>
    /// CreatedBy: dbhuan (19/04/2021)
    public class Customer
    {
        /// <summary>
        /// Id khách hàng.
        /// </summary>
        public Guid CustomerId { get; set; }

        /// <summary>
        /// Mã khách hàng.
        /// </summary>
        [PropertyRequired(name: "Mã khách hàng")]
        [PropertyMaxLength(20, name: "Mã khách hàng")]
        public string CustomerCode { get; set; }

        /// <summary>
        /// Họ tên khách hàng.
        /// </summary>
        [PropertyRequired(name: "Tên khách hàng")]
        public string FullName { get; set; }

        /// <summary>
        /// Giới tính khách hàng theo kiểu int.
        /// </summary>
        public int? Gender { get; set; }

        /// <summary>
        /// Mã thẻ thành viên.
        /// </summary>
        public string MemberCardCode { get; set; }

        /// <summary>
        /// Id nhóm khách hàng.
        /// </summary>
        public Guid? CustomerGroupId { get; set; }

        /// <summary>
        /// Tên nhóm khách hàng.
        /// </summary>
        public string CustomerGroupName { get; }

        /// <summary>
        /// Số điện thoại.
        /// </summary>
        [PropertyRequired(name: "Số điện thoại")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Ngày sinh.
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Tên công ty.
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// Mã số thuế công ty.
        /// </summary>
        public string CompanyTaxCode { get; set; }

        /// <summary>
        /// Địa chỉ Email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Địa chỉ thường trú.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Ghi chú.
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// Ngày thêm vào csdl.
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Người thêm vào.
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Ngày cập nhật dữ liệu gần đây nhất.
        /// </summary>
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Người cập nhật dữ liệu.
        /// </summary>
        public string ModifiedBy { get; set; }

        /// <summary>
        /// Giới tính của khách hàng.
        /// </summary>
        public string GenderName
        {
            get
            {
                if (Gender == 0)
                {
                    return "Nữ";
                }
                else if (Gender == 1)
                {
                    return "Nam";
                }
                else if (Gender == 2)
                {
                    return "Khác";
                }
                else
                {
                    return "Không xác định";
                }
            }
        }
    }
}
