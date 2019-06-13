using System;

namespace AIT.UtilitiesComponents.Extensions
{
    public static class ObjectExtensions
    {
        public static TOut IfNotNull<T, TOut>(this T value, Func<T, TOut> func)
        {
            return value != null ? func(value) : default(TOut);
        }

        public static void IfNotNull<T>(this T value, Action action)
        {
            if (IsNotNull(value)) action();
        }

        public static void IfNotNull<T, TIn>(this T value, Action<TIn> action, TIn obj)
        {
            if (IsNotNull(value)) action(obj);
        }

        public static T IfNull<T>(this T value, Func<T> func)
        {
            return value == null ? func() : value;
        }

        public static bool IsNotNull<T>(this T value)
        {
            return !ReferenceEquals(value, null);
        }

        public static void IfTrueOrFalse(this bool value, Action onTrueAction, Action onFalseAction)
        {
            if (value)
                onTrueAction();
            else
                onFalseAction();
        }

        public static TResult IfTrueOrFalse<TResult>(this bool value, Func<TResult> onTrueFunc, Func<TResult> onFalseFunc)
        {
            if (value)
                return onTrueFunc();
            else
                return onFalseFunc();
        }
    }
}
