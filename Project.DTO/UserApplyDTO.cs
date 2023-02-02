using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.DTO
{
    /// <summary>
    /// 记录当前用户的审批情况
    /// </summary>
    public class UserApplyDTO
    {
        public int UserId { get; set; }
        public string? ApprovalMsg { get; set; }
        public string? Remark { get; set; }
        public string? Name { get; set; }
        public string? Mobile { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public long? QQ { get; set; }
        public string? WeChat { get; set; }
        public int? Sex { get; set; }

        public int ApplyId { get; set; }

        /// <summary>
        /// 身份证图片
        /// </summary>
        public string? IdImageUrl { get; set; }
        public string? UserNo { get; set; }
        public int ApprovalStatus { get; set; }
    }
}
