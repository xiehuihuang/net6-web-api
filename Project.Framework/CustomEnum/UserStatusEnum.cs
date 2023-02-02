using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Framework.CustomEnum
{
    /// <summary>
    /// 用户状态
    /// </summary>
    public enum UserStatusEnum
    {
        /// <summary>
        /// 已冻结
        /// </summary>
        [Remark("已冻结")]
        Frozen = 0,

        /// <summary>
        /// 正常
        /// </summary>
        [Remark("正常")]
        Normal = 1,

        /// <summary>
        /// 已删除
        /// </summary>
        [Remark("已删除")]
        Delete = 2
    }
}
