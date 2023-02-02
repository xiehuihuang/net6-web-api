using SqlSugar;

namespace Project.Framework.DatabaseConnectionOption
{
    public class CustomSlaveConnectionConfig : SlaveConnectionConfig
    {
        private int _CustomHitRate;
        public int CustomHitRate
        {
            get { return _CustomHitRate; }
            set
            {
                HitRate = value;
                _CustomHitRate = value;
            }
        }
    }
}
