using FluentValidation;
using GranTitan.DAL.Entities;

namespace GranTitan.BLL.Validators
{
    public class AuthorValidator : AbstractValidator<Author>
    {
        public AuthorValidator()
        {
            RuleFor(x => x.FirstName)
                .NotNull().WithMessage("El Primer Nombre es obligatorio")
                .NotEmpty().WithMessage("El Primer Nombre no puede estar vacio")
                .Matches(@"^[\p{L}\p{M}\s]+$")
                .WithMessage("El Primer Nombre no bebe tener números.");

            RuleFor(x => x.SecondName)                
                .Matches(@"^[\p{L}\p{M}\s]+$")
                .WithMessage("El Segundo Nombre no bebe tener números.");

            RuleFor(x => x.Surname)
                .NotNull().WithMessage("El Primer Nombre es obligatorio")
                .NotEmpty().WithMessage("El Primer Nombre es obligatorio")
                .Matches(@"^[\p{L}\p{M}\s]+$")
                .WithMessage("El Primer Nombre no bebe tener números.");

            RuleFor(x => x.SecondSurname)
                .Matches(@"^[\p{L}\p{M}\s]+$")
                .WithMessage("El Segundo Nombre no bebe tener números.");

            RuleFor(x => x.BirthDate)
                .NotNull().WithMessage("La fecha de nacimiento es obligatoria")
                .Must(IsValidDate).WithMessage("La fecha debe ser valida");
        }

        private bool IsValidDate(DateTime date)
        {
            // Utiliza una expresión regular para verificar que la fecha sea valida
            return !date.Equals(default(DateTime));
        }
    }
}
