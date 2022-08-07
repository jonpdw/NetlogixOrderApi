
using NetlogixOrderApi.DTO;
using NetlogixOrderApi.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;

namespace NetlogixOrderApi.Validation
{
    public class ControllerValidation
    {
        public static JsonValidationResponse ValidateOrderRequestDTO(string orderDTO)
        {
            JSchemaGenerator generator = new JSchemaGenerator();
            JSchema schema = generator.Generate(typeof(OrderRequestDTO));
            JObject order = JObject.Parse(orderDTO);
            IList<ValidationError> errors;
            bool isValid = order.IsValid(schema, out errors);
            JsonValidationResponse jsonValidation = new JsonValidationResponse
            {
                IsValid = isValid,
                ValidationErrors = errors.Select(x => x.Message).ToList()
            };
            return jsonValidation;
        }
    }

}
