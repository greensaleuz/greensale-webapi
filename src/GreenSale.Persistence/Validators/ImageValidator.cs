using FluentValidation;
using GreenSale.Persistence.Dtos.BuyerPostImageUpdateDtos;
using GreenSale.Persistence.Validators.FileValidators;

namespace GreenSale.Persistence.Validators;

public class ImageValidator : AbstractValidator<BuyerPostImageDto>
{
    public ImageValidator()
    {
        RuleFor(x => x.ImagePath).SetValidator(new FileValidator());
    }
}
