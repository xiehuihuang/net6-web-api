using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Project.Framework.CustomEnum;

namespace Project.DTO
{
    ///<summary>
    ///
    ///</summary>
    public partial class UserInfoDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "请输入用户名")]
        [Display(Name = "用户名")]
        public string? Name { get; set; }

        [Display(Name = "密码")]
        [Required(ErrorMessage = "请输入密码")]
        [Compare("NewPassword", ErrorMessage = "两次密码输入不一致")]
        public string? Password { get; set; }

        [Display(Name = "确认密码")]
        [Required(ErrorMessage = "请输入新密码")]
        [Compare("Password", ErrorMessage = "两次密码输入不一致")]
        public string? NewPassword { get; set; }

        [Display(Name = "手机号")]
        [Required(ErrorMessage = "手机号不能为空")]
        [PhoneValiDataAttribute]
        public string? Mobile { get; set; }

        public DateTime Birthday { get; set; }
        public string? BirthdayRemark
        {
            get
            {
                return Birthday.ToString("yyyy-MM-dd");
            }
        }

        [Display(Name = "性别")]
        [Required(ErrorMessage = "请选择性别")]
        public int? Gender { get; set; }
        public string? GenderRemark
        {
            get
            {
                return Gender != null ? ((GenderEnum)Gender).GetRemark() : string.Empty;
            }
        }

        [Display(Name = "用户类型")]
        public int? UserType { get; set; }
        public string? UserTypeRemark
        {
            get
            {
                return UserType != null ? ((UserTypeEnum)UserType).GetRemark() : string.Empty;
            }
        }

        [Display(Name = "用户状态")]
        public int? Status { get; set; }
        public string? UserStatusRemark
        {
            get
            {
                return Status != null ? ((UserStatusEnum)Status).GetRemark() : string.Empty;
            }
        }

        public string? ImageUrl { get; set; }

        public DateTime CreateTime { get; set; }
        public string? CreateTimeRemark
        {
            get
            {
                return CreateTime.ToString("yyyy-MM-dd HH:mm:ss");
            }
        }
    }
}
