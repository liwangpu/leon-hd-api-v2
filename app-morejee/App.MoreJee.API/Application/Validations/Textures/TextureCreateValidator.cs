using App.MoreJee.API.Application.Commands.Materials;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.MoreJee.API.Application.Validations.Textures
{
    public class TextureCreateValidator : AbstractValidator<MaterialCreateCommand>
    {
        public TextureCreateValidator()
        {

        }
    }
}
