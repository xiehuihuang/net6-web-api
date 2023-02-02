using System;
using System.Linq;
using System.Text;

namespace Project.DbModels
{
    ///<summary>
    ///
    ///</summary>
    public partial class inventory
    {
        public inventory()
        {


        }
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        public int Id { get; set; }

        /// <summary>
        /// Desc:商品Id
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? Goods { get; set; }

        /// <summary>
        /// Desc:库存数量
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? Stocks { get; set; }

        /// <summary>
        /// Desc:版本号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? Version { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public byte? IsDeleted { get; set; }

    }
}
