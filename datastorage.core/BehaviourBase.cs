using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

using Datastorage.Framework;

namespace Datastorage.Core
{
	abstract class BehaviourBase<TObject>
		where TObject : class, IIdentifiable<long>
	{
		#region Protected Methods

		protected IDbSet<TObject> GetDbSet(DbContext dbContext)
		{
			return dbContext.Set<TObject>();
		}

		protected IQueryable<TObject> ApplyIncludes(
			IQueryable<TObject> query,
			IReadOnlyList<Expression<Func<TObject, object>>> includes)
		{
			foreach(var include in includes)
			{
				query.Include(include);
			}

			return query;
		}

		protected IQueryable<TObject> ApplyFilter(
			IQueryable<TObject> query,
			Expression<Func<TObject, bool>> filter)
		{
			return filter == null
				? query
				: query.Where(filter);
		}

		#endregion
	}
}
