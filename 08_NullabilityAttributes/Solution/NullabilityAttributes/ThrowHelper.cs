using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace NullabilityAttributes
{
    internal static class ThrowHelper
    {
        [DoesNotReturn]
        public static void ArgumentNull(object? argument, [CallerArgumentExpression(nameof(argument))] string? argStr = null) =>
            throw new ArgumentNullException(argStr);
    }
}
