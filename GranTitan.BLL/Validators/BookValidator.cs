using FluentValidation;
using GranTitan.DAL.Entities;

namespace GranTitan.BLL.Validators
{
    public class BookValidator : AbstractValidator<BookCreateDto>
    {
        public BookValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("El nombre es obligatorio")
                .NotNull().WithMessage("El nombre es obligatorio")
                .MaximumLength(100).MinimumLength(1);

            RuleFor(x => x.Library)
                .NotEmpty().WithMessage("El nombre es obligatorio")
                .MaximumLength(100).MinimumLength(1);
            //.Matches(@"^[\p{L}\p{M}\s]+$")
            //.WithMessage("El nombre editorial solo puede contener letras y espacios");

            RuleFor(x => x.Pages).NotNull().WithMessage("El número de paginas es obligatorio")
                .GreaterThan(0).WithMessage("El número de paginas debe ser mayor que cero")
                .Must(ContieneSoloNumeros).WithMessage("El precio debe contener solo números");

            RuleFor(x => x.Price).NotNull().WithMessage("El precio es obligatorio")
                .GreaterThan(0).WithMessage("El precio debe ser mayor que cero")
                .Must(ContieneSoloNumeros).WithMessage("El precio debe contener solo números");

            RuleFor(x => x.ReleaseDate).NotNull().WithMessage("La fecha de lanzamiento es obligatoria")
                .Must(IsValidDate).WithMessage("La fecha debe ser valida");
        }

        private bool ContieneSoloNumeros(int text)
        {
            // Utiliza una expresión regular para verificar que el texto contenga solo números
            return !string.IsNullOrEmpty(text.ToString()) && System.Text.RegularExpressions.Regex.IsMatch(text.ToString(), @"^\d+$");
        }

        private bool IsValidDate(DateTime date)
        {
            // Utiliza una expresión regular para verificar que la fecha sea valida
            return !date.Equals(default(DateTime));
        }
    }
}
