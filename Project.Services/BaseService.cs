using SqlSugar;
using System.Linq.Expressions;
using Project.Framework;
using Project.Interfaces;

namespace Project.Services
{
    public class BaseService : IBaseService
    {
        protected ISqlSugarClient _Client { get; set; }


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="context"></param>
        public BaseService(ISqlSugarClient client)
        {
            _Client = client;
        }

        #region Query
        public T Find<T>(int id) where T : class
        {
            return _Client.Queryable<T>().InSingle(id);
        }

        /// <summary>
        /// 不应该暴露给上端使用者，尽量少用
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        [Obsolete("尽量避免使用，using 带表达式目录树的代替")]
        public ISugarQueryable<T> Set<T>() where T : class
        {
            return _Client.Queryable<T>();
        }

        /// <summary>
        /// 这才是合理的做法，上端给条件，这里查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="funcWhere"></param>
        /// <returns></returns>
        public ISugarQueryable<T> Query<T>(Expression<Func<T, bool>> funcWhere) where T : class
        {
            return _Client.Queryable<T>().Where(funcWhere);
        }

        public PagingData<T> QueryPage<T>(Expression<Func<T, bool>> funcWhere, int pageSize, int pageIndex, Expression<Func<T, object>> funcOrderby, bool isAsc = true) where T : class
        {
            var list = _Client.Queryable<T>();
            if (funcWhere != null)
            {
                list = list.Where(funcWhere);
            }
            list = list.OrderByIF(true, funcOrderby, isAsc ? OrderByType.Asc : OrderByType.Desc);
            PagingData<T> result = new PagingData<T>()
            {
                DataList = list.ToPageList(pageIndex, pageSize),
                PageIndex = pageIndex,
                PageSize = pageSize,
                RecordCount = list.Count(),
            };
            return result;
        }
        #endregion

        #region Insert
        /// <summary>
        /// 即使保存  不需要再Commit
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public T Insert<T>(T t) where T : class, new()
        {
            _Client.Insertable(t).ExecuteCommand();
            return t;
        }

        public IEnumerable<T> Insert<T>(List<T> tList) where T : class, new()
        {
            _Client.Insertable(tList.ToList()).ExecuteCommand();
            return tList;
        }
        #endregion

        #region Update
        /// <summary>
        /// 是没有实现查询，直接更新的,需要Attach和State
        /// 
        /// 如果是已经在context，只能再封装一个(在具体的service)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        public void Update<T>(T t) where T : class, new()
        {
            if (t == null) throw new Exception("t is null");

            _Client.Updateable(t).ExecuteCommand();
        }

        public void Update<T>(List<T> tList) where T : class, new()
        {
            _Client.Updateable(tList).ExecuteCommand();
        }

        #endregion

        #region Delete
        /// <summary>
        /// 先附加 再删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        public void Delete<T>(T t) where T : class, new()
        {
            _Client.Deleteable(t).ExecuteCommand();
        }

        /// <summary>
        /// 还可以增加非即时commit版本的，
        /// 做成protected
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Id"></param>
        public void Delete<T>(int Id) where T : class, new()
        {
            T t = _Client.Queryable<T>().InSingle(Id);
            _Client.Deleteable(t).ExecuteCommand();
        }

        public void Delete<T>(List<T> tList) where T : class
        {
            _Client.Deleteable(tList).ExecuteCommand();
        }
        #endregion


        #region Other

        ISugarQueryable<T> IBaseService.ExcuteQuery<T>(string sql) where T : class
        {
            return _Client.SqlQueryable<T>(sql);
        }
        public void Dispose()
        {
            if (_Client != null)
            {
                _Client.Dispose();
            }
        }

        #endregion
    }
}