namespace RpgBooks.Modules.Identity.Domain.Validation;

using RpgBooks.Modules.Identity.Domain.Exceptions;

using System.Runtime.CompilerServices;

internal static class ClaimValidation
{
    internal static class Values
    {
        public const int MaxTypeLenght = 256;

        public const int MaxValueLenght = 2048;

        public const int MaxValueTypeLenght = 256;
    }

    internal static class EnsureThat
    {
        internal static void HasValidType(string type, [CallerArgumentExpression(nameof(type))] string typeParamName = "")
        {
            Ensure.IsNotEmpty<InvalidClaimTypeException>(type, typeParamName);
            Ensure.HasMaxLength<InvalidClaimTypeException>(type, Values.MaxTypeLenght, typeParamName);
        }

        internal static void HasalidValue(string value, [CallerArgumentExpression(nameof(value))] string valueParamName = "")
        {
            Ensure.IsNotEmpty<InvalidClaimValueException>(value, valueParamName);
            Ensure.HasMaxLength<InvalidClaimValueException>(value, Values.MaxValueLenght, valueParamName);
        }

        internal static void HasValidValueType(string valueType, [CallerArgumentExpression(nameof(valueType))] string valueTypeParamName = "")
        {
            Ensure.IsNotEmpty<InvalidClaimValueTypeException>(valueType, valueTypeParamName);
            Ensure.HasMaxLength<InvalidClaimValueTypeException>(valueType, Values.MaxValueTypeLenght, valueTypeParamName);
        }
    }
}
