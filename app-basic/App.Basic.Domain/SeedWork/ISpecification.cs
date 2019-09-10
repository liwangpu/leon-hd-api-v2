using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace App.Basic.Domain.SeedWork
{
    public interface ISpecification<T>
        where T : IAggregateRoot
    {
        Expression<Func<T, bool>> Criteria { get; }
        List<Expression<Func<T, object>>> Includes { get; }
    }
}
