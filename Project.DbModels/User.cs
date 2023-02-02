using SqlSugar;
using System;
using System.Linq;
using System.Security.Principal;
using System.Text;

namespace Project.DbModels
{
    ///<summary>
    ///
    ///</summary>
    public partial class User
    {
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        /// <summary>
        /// Desc:用户名
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string? Name { get; set; }

        /// <summary>
        /// Desc:用户密码
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string? Password { get; set; }

        /// <summary>
        /// Desc:手机号
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string? Mobile { get; set; }

        /// <summary>
        /// Desc:出生日期
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// Desc:性别：1为男,2为女,3为未知
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? Gender { get; set; }

        /// <summary>
        /// Desc:用户类别：1为管理员, 2为普通用户,3为VIP用户
        /// Default:1
        /// Nullable:True
        /// </summary>           
        public int? UserType { get; set; }

        /// <summary>
        /// Desc:用户状态：0为冻结、1为正常、2为已删除
        /// Default:1
        /// Nullable:True
        /// </summary>           
        public int? Status { get; set; }

        /// <summary>
        /// Desc:用户头像
        /// Default:
        /// Nullable:True
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// Desc:创建时间
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// Desc:修改时间
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? UpdateTime { get; set; }

    }
}
