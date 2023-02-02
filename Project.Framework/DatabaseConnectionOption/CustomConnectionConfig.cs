using SqlSugar;

namespace Project.Framework.DatabaseConnectionOption
{
    /// <summary>
    /// 
    /// </summary>
    public class CustomConnectionConfig
    {

        /// <summary>
        /// 
        /// </summary>
        public CustomConnectionConfig()
        {
            SlaveConnectionConfigs = new List<CustomSlaveConnectionConfig>();
        }

        /// <summary>
        /// 
        /// </summary>
        public string? ConnectionString
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public List<CustomSlaveConnectionConfig> SlaveConnectionConfigs { get; set; }
    }
}
