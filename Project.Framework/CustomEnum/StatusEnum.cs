using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Framework.CustomEnum
{
    /// <summary>
    /// 通用状态
    /// </summary>
    public enum StatusEnum
    {
        /// <summary>
        /// 禁用
        /// </summary>
        [Remark("禁用")]
        Disable = 0,

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
