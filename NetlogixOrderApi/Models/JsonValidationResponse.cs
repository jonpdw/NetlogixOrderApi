namespace NetlogixOrderApi.Models
{
    public class JsonValidationResponse
    {
        public bool isValid { get; set; }
        public IList<string> validationErrors { get; set; }
    }

}
