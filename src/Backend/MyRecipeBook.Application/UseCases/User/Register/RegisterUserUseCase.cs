using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;
using MyRecipeBook.Exceptions.ExceptionsBase;
using System.ComponentModel.DataAnnotations;

namespace MyRecipeBook.Application.UseCases.User.Register
{
    public class RegisterUserUseCase
    {
        public ResponseRegisteredUserJson Execute(RequestUserRegisterJson request)
        {
            Validate(request);

            var user = new Domain.Entities.User();
            {
                
            }

            // Criptografia da Senha

            // Salvar no banco de Dados



            return new ResponseRegisteredUserJson
            {
                Name = request.Name,
                
            };
        }

        private void Validate(RequestUserRegisterJson request)
        {
            var validator = new RegisterUserValidator();

            var result = validator.Validate(request);

            if (result.IsValid == false)
            {
                var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errorMessages);

            }
        }
    } 
}
