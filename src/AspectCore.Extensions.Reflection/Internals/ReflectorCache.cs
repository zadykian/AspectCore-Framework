using System;
using System.Reflection;

namespace AspectCore.Extensions.Reflection
{
    public static class ReflectorCache
    {
        internal static volatile Options CurrentOptions = Options.Enabled;

        public static Options GetOptions() => CurrentOptions;

        public static void SetOptions(Options options)
            => CurrentOptions = options ?? throw new ArgumentNullException(nameof(options));

        public static void Clear()
        {
            ReflectorCacheUtils<ConstructorInfo, ConstructorReflector>.Clear();
            ReflectorCacheUtils<CustomAttributeData, CustomAttributeReflector>.Clear();
            ReflectorCacheUtils<FieldInfo, FieldReflector>.Clear();
            ReflectorCacheUtils<ParameterInfo, ParameterReflector>.Clear();
            ReflectorCacheUtils<Pair<PropertyInfo, CallOptions>, PropertyReflector>.Clear();
            ReflectorCacheUtils<Pair<MethodInfo, CallOptions>, MethodReflector>.Clear();
            ReflectorCacheUtils<TypeInfo, TypeReflector>.Clear();
            MethodExtensions.ClearCache();
        }

        public sealed class Options
        {
            public static readonly Options Enabled = new Options(isEnabled: true);

            public static readonly Options Disabled = new Options(isEnabled: false);

            public readonly bool IsEnabled;

            private Options(bool isEnabled) => IsEnabled = isEnabled;
        }
    }
}