using System;
using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using TableAttribute = System.ComponentModel.DataAnnotations.Schema.TableAttribute;
using Tree.Core.Domain.Entities;
using Tree.Core.Domain.UnitOfWork;
using Tree.Core.Extensions;

namespace Tree.Core.Domain.Repositories
{
    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>

    {
        private readonly IUnitOfWork _unitOfWork;
        string tablename = typeof(TEntity).GetAttributeValue((TableAttribute ta) => ta.Name);
        // ReSharper disable once MemberCanBeProtected.Global
        /// <summary>
        /// 
        /// </summary>
        /// <param name="unitOfWork"></param>
        public Repository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //Create
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<TKey> InsertAsync(TEntity entity)
        {
            return await _unitOfWork.Connection.InsertAsync<TKey, TEntity>(entity, _unitOfWork.Transaction);
        }

        //Retrieve
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<TEntity> GetAsync(TKey id)
        {
            return await _unitOfWork.Connection.GetAsync<TEntity>(id, _unitOfWork.Transaction);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<TEntity>> GetAsync()
        {
            return await _unitOfWork.Connection.GetListAsync<TEntity>(_unitOfWork.Transaction);
        }

        //Update
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task UpdateAsync(TEntity entity)
        {
            await _unitOfWork.Connection.UpdateAsync(entity, _unitOfWork.Transaction);
        }

        //Delete
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task DeleteAsync(TEntity entity)
        {
            await _unitOfWork.Connection.DeleteAsync(entity, _unitOfWork.Transaction);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> CheckRepeat(string name, string value, Guid? id = null)
        {
            string sql = $"select count(1) from {tablename}  where {name}='{value}' ";
            if (id != null)
            {
                sql += $" and id!='{id}     ";
            }

            return await ExecuteScalarAsync<int>(sql, null) > 0;
        }

        /// <summary>
        /// 获取数据库表中int类型最大的value+1
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public async Task<int> GetMaxCodeGal(string columnName)
        {
            var sql = $@"SELECT {columnName} FROM {tablename} ORDER BY {columnName} DESC LIMIT 1";
            var code = await ExecuteScalarAsync<int>(sql);
            if (code == 0)
            {
                code = 1;
            }
            else
            {
                code += 1;
            }

            return code;
        }

        /// <summary>
        /// 根据table中任意一组key-value获取对象
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<TEntity> GetTargetByKeyAndValue(string name, string value)
        {
            var sql = $@"SELECT * FROM {tablename} where {name}='{value}'";
            return await _unitOfWork.Connection.QueryFirstOrDefaultAsync<TEntity>(sql, null, _unitOfWork.Transaction);
        }

        //Query
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected async Task<IEnumerable<T>> QueryAsync<T>(string sql, object parameters = null)
        {
            try
            {
                var data = await _unitOfWork.Connection.QueryAsync<T>(sql, parameters, _unitOfWork.Transaction).ConfigureAwait(false);
                var List = await _unitOfWork.Connection.QueryAsync<T>(sql, parameters, _unitOfWork.Transaction).ConfigureAwait(false);
            }
            catch (Exception ex)
            {

                throw;
            }
            return await _unitOfWork.Connection.QueryAsync<T>(sql, parameters, _unitOfWork.Transaction).ConfigureAwait(false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected async Task<T> QueryFirstOrDefaultAsync<T>(string sql, object parameters = null)
        {
            return await _unitOfWork.Connection.QueryFirstOrDefaultAsync<T>(sql, parameters, _unitOfWork.Transaction).ConfigureAwait(false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="parameters"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected async Task<(int, IEnumerable<T>)> QueryByPagerAsync<T>(string sql, int skip, int take,
            object parameters = null)
        {
            var total = await ExecuteScalarAsync<int>($"select count(1) from ({sql}) t", parameters);
            var list = await QueryAsync<T>($"{sql} limit {skip},{take}", parameters);
            return (total, list);
        }

        //Execute
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        protected async Task<int> ExecuteAsync(string sql, object parameters = null)
        {
            return await _unitOfWork.Connection.ExecuteAsync(sql, parameters, _unitOfWork.Transaction);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected async Task<T> ExecuteScalarAsync<T>(string sql, object parameters = null)
        {
            return await _unitOfWork.Connection.ExecuteScalarAsync<T>(sql, parameters, _unitOfWork.Transaction);
        }
    }
}

