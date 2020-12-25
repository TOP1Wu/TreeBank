using System;
using System.Collections.Generic;
using System.Text;

namespace Tree.Core.Domain.Entities
{
    public interface IEntity<TKey> : IKey<TKey>
    {
    }
}
