using System;
using System.Collections.Generic;
using System.Text;

namespace Tree.Core.Domain.Entities
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public interface IKey<TKey>
    {
        /// <summary>
        /// Key
        /// </summary>
     //   [ExplicitKey]
        TKey Id { get; set; }
    }
}
