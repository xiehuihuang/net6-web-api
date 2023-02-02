using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Framework.CustomEnum
{
    /// <summary>
    /// 用户类别
    /// </summary>
    public enum UserTypeEnum
    {
        /// <summary>
        /// 管理员
        /// </summary>
        [Remark("管理员")]
        Administrators = 1,

        /// <summary>
        /// 普通用户
        /// </summary>
        [Remark("普通用户")]
        Member = 2,

        /// <summary>
        /// VIP用户
        /// </summary>
        [Remark("VIP用户")]
        Anchor = 3
    }
}
