using OneOf;

namespace DocsSave.OneOf.Extensions;

public static class OneOf3Extensions
{
    public static async Task<TResult> Match<TResult, T0, T1, T2>(this OneOf<T0, T1, T2> oneOf, Func<T0, TResult> f0, Func<T1, Task<TResult>> f1, Func<T2, TResult> f2)
    {
        if (oneOf.Index == 0)
            return f0(oneOf.AsT0);
        
        if (oneOf.Index== 1)
            return await f1(oneOf.AsT1);
        
        if (oneOf.Index== 2)
            return f2(oneOf.AsT2);
        
        throw new InvalidOperationException();
    }
}