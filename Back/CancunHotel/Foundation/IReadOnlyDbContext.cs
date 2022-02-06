using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;

namespace Foundation.Api.Database.Contract
{
    public interface IReadOnlyDbContext
    {
        bool IsAlreadyAttached<T>(Func<T, bool> match, out T entityFound)
            where T : class;

        int CountFromSql(string sql, params KeyValuePair<string, object>[] parameters);

        ChangeTracker ChangeTracker { get; }
    }
}
