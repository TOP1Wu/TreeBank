using Dapper;
using System;

namespace Tree.Core.Domain.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class Entity : Entity<Guid>
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public class Entity<TKey> : IEntity<TKey>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key] public TKey Id { get; set; }
    }
}
