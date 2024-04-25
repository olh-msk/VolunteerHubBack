using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Domain.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true)]
    public sealed class StringToEnumAttribute : ValidationAttribute
    {
        public Type EnumType { get; }
        public StringToEnumAttribute(Type enumType)
        {
            EnumType = enumType;
        }
        public override string FormatErrorMessage(string name)
        {
            return string.Format(CultureInfo.CurrentCulture,
              ErrorMessageString, name, EnumType);
        }
        public override bool IsValid(object? value)
        {
            if (value != null && Enum.TryParse(EnumType, value.ToString(), out object? _))
            {
                return true;
            }
            return false;
        }
    }
}
