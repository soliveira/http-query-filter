namespace Http.Query.Filter.Integration.Test.Infraestructure.Filter
{
    using System;

    using Http.Query.Filter;

    internal interface IWhere<out TReturn, in TFilter, in TParam>
        where TFilter : Filter
    {
        Func<TParam, TReturn> Apply(TFilter filter);

        TReturn Apply(TParam param);
    }
}
