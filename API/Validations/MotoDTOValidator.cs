using FluentValidation;
using Mottu.Application.DTOs;

namespace Mottu.API.Validations
{
    public class MotoDTOValidator : AbstractValidator<MotoDto>
    {
        public MotoDTOValidator()
        {
            RuleFor(x => x.Modelo).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Placa).NotEmpty().Length(7);
            RuleFor(x => x.PatioId).GreaterThan(0);
            RuleFor(x => x.Status).IsInEnum();
        }
    }
}