using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;
using System.ComponentModel.DataAnnotations;

namespace MyRecipeBook.Application.UseCases.User.Register
{
    public class RegisterUserUseCase 
    { 
    
        public ResponseRegisteredUserJson Execute(RequestUserRegisterJson request)
        {
            Validate(request);
            //Validar a request



            //Mapear a request em uma entidade

            //Criptografar a senha

            //Salvar no banco de dados





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
                var errorMessages = result.Errors.Select(e => e.ErrorMessage);
            
                throw new Exception();
            }
        }

    }
}
