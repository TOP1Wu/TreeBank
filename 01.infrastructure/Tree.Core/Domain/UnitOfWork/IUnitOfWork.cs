using System;
using System.Data;
using Treebank.Core.Autofac;

namespace Tree.Core.Domain.UnitOfWork
{
    /// <summary>
    /// 
    /// </summary>
    public interface IUnitOfWork : IDisposable, IScopeDependency
    {
        /// <summary>
        /// 
        /// </summary>
        IDbConnection Connection { get; }

        /// <summary>
        /// 
        /// </summary>
        IDbTransaction Transaction { get; }

        /// <summary>
        /// 
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// 
        /// </summary>
        void Commit();

        /// <summary>
        /// 
        /// </summary>
        void Rollback();
    }
}
