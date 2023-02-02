﻿using Microsoft.Extensions.Options;
using Project.Framework.RedisHelper.Init;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Framework.RedisHelper.Interface
{
    /// <summary>
    /// RedisBase类，是redis操作的基类，继承自IDisposable接口，主要用于释放内存
    /// </summary>
    public abstract class RedisBase : IDisposable
    {
        public IRedisClient IClient { get; private set; }
        /// <summary>
        /// 构造时完成链接的打开
        /// </summary>
        public RedisBase(IOptionsMonitor<RedisConfigInfo> options)
        {
            IClient = RedisManager.GetClient(options.CurrentValue);
        }

        //public static IRedisClient iClient { get; private set; }
        //static RedisBase()
        //{
        //    iClient = RedisManager.GetClient();
        //}


        private bool _disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    IClient.Dispose();
                    IClient = null;
                }
            }
            _disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Transcation()
        {
            using IRedisTransaction irt = IClient.CreateTransaction();
            try
            {
                irt.QueueCommand(r => r.Set("key", 20));
                irt.QueueCommand(r => r.Increment("key", 1));
                irt.Commit(); // 提交事务
            }
            catch (Exception)
            {
                irt.Rollback();
                throw;
            }
        }


        /// <summary>
        /// 清除全部数据 请小心
        /// </summary>
        public virtual void FlushAll()
        {
            IClient.FlushAll();
        }

        /// <summary>
        /// 保存数据DB文件到硬盘
        /// </summary>
        public void Save()
        {
            IClient.Save();//阻塞式save
        }

        /// <summary>
        /// 异步保存数据DB文件到硬盘
        /// </summary>
        public void SaveAsync()
        {
            IClient.SaveAsync();//异步save
        }
    }
}
