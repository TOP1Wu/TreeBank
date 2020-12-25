using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tree.Core.Domain.Entities;
using Treebank.Core.Autofac;

namespace Tree.Core.Domain.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface IRepository<TEntity, TKey> : IScopeDependency where TEntity : IEntity<TKey>
    {
        //Create
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<TKey> InsertAsync(TEntity entity);

        //Retrieve
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntity> GetAsync(TKey id);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetAsync();

        //Update
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task UpdateAsync(TEntity entity);

        //Delete
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task DeleteAsync(TEntity entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> CheckRepeat(string name, string value, Guid? id = null);
    }
}
