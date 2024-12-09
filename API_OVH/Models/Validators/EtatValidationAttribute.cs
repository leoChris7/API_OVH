using System;
using System.ComponentModel.DataAnnotations;

public class EtatValidationAttribute : ValidationAttribute
{
    private readonly string[] _validValues = { "NSP", "OUI", "NON" };

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null)
        {
            return new ValidationResult("Etat ne peut pas être null.");
        }

        string stringValue = value.ToString() ?? string.Empty;

        if (Array.Exists(_validValues, v => v.Equals(stringValue, StringComparison.OrdinalIgnoreCase)))
        {
            return ValidationResult.Success; // Valid value
        }

        return new ValidationResult($"Etat doit être: {string.Join(", ", _validValues)}.");
    }
}
