namespace NetlogixOrderApi.Models
{
    public class JsonValidationResponse
    {
        public bool IsValid { get; set; }
        public IList<string> ValidationErrors { get; set; }
    }

}
