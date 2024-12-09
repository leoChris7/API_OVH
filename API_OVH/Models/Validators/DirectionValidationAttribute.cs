using System;
using System.ComponentModel.DataAnnotations;

public class DirectionValidationAttribute : ValidationAttribute
{
    private readonly string[] _validValues = { "N", "S", "E", "O", "NO", "NE", "SO", "SE" };

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null)
        {
            return new ValidationResult("La cardinalité ne peut pas être null.");
        }

        string stringValue = value.ToString() ?? string.Empty;

        if (Array.Exists(_validValues, v => v.Equals(stringValue, StringComparison.OrdinalIgnoreCase)))
        {
            return ValidationResult.Success;
        }

        return new ValidationResult($"La cardinalité doit être: {string.Join(", ", _validValues)}.");
    }
}
