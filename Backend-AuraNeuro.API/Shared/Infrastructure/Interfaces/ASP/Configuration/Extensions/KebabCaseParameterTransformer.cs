using System.Text.RegularExpressions;

namespace Backend_AuraNeuro.API.Shared.Infrastructure.Interfaces.ASP.Configuration.Extensions;

public sealed partial class KebabCaseParameterTransformer : IOutboundParameterTransformer
{
    //  Compilación en tiempo de compilación (sin overhead en runtime)
    [GeneratedRegex("([a-z])([A-Z])", RegexOptions.Compiled)]
    private static partial Regex KebabCaseRegex();
    
    public string? TransformOutbound(object? value)
    {
        if (value is null)
            return null;

        // 🔹 Convierte PascalCase o camelCase → kebab-case
        return KebabCaseRegex()
            .Replace(value.ToString()!, "$1-$2")
            .ToLowerInvariant();
    }
}