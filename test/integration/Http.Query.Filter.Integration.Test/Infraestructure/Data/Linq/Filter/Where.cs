namespace Http.Query.Filter.Integration.Test.Infraestructure.Data.Linq.Filter
{
    using System;
    using System.Linq;
    using System.Reflection;

    using Http.Query.Filter;
    using Http.Query.Filter.Integration.Test.Infraestructure.Filter;
    using Http.Query.Filter.Integration.Test.Infraestructure.Filter.Extensions;

    internal class Where<TEntity> : IWhere<bool, Filter, TEntity>
    {
        private Filter filter;

        public Func<TEntity, bool> Apply(Filter filter)
        {
            this.filter = filter;
            return this.Apply;
        }

        public bool Apply(TEntity entity)
        {
            if (!this.filter.HasCondition)
            {
                return true;
            }

            var type = entity.GetType();
            const BindingFlags BindingAttr = BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance;

            return this.filter
                .Where
                .All(condition => type
                    .GetProperty(condition.Field, BindingAttr)
                    .GetValue(entity)
                    .ToString()
                    .Verify(condition.Value.ToString(), condition.Comparison));
        }
    }
}