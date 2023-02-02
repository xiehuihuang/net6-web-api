using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Framework.CustomEnum
{
    /// <summary>
    /// 性别
    /// </summary>
    public enum GenderEnum
    {
        [Remark("男")]
        Male = 1,
        [Remark("女")]
        Female = 2,
        [Remark("人妖")]
        Simon = 3
    }
}
