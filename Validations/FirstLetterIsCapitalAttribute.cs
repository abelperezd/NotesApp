using System.ComponentModel.DataAnnotations;

namespace Notes.Validations
{
	public class FirstLetterIsCapitalAttribute : ValidationAttribute
	{
		protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
		{
			string? valAsString = value?.ToString();

			//return true becau
			if (string.IsNullOrEmpty(valAsString))
				return new ValidationResult("Text cannot be empty.");

			string firstLetter = valAsString[0].ToString();

			if (firstLetter != firstLetter.ToUpper())
				return new ValidationResult("First letter must be Uppercase.");

			return ValidationResult.Success;
		}
	}
}
