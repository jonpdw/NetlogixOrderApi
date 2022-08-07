
using NetlogixOrderApi.Controllers;
using NetlogixOrderApi.DTO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;

namespace NetlogixOrderApi.Validation
{
    public class ControllerValidation
    {
        public static JSONValidation ValidateOrderRequestDTO(string orderDTO)
        {
            JSchemaGenerator generator = new JSchemaGenerator();
            JSchema schema = generator.Generate(typeof(OrderRequestDTO));
            JObject order = JObject.Parse(orderDTO);
            IList<ValidationError> errors;
            bool isValid = order.IsValid(schema, out errors);
            JSONValidation jsonValidation = new JSONValidation
            {
                isValid = isValid,
                validationErrors = errors.Select(x => x.Message).ToList()
            };
            return jsonValidation;
        }
    }

}
