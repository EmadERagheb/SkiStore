﻿
namespace SkiStore.API.Errors
{
    public class APIResponse
    {
        public APIResponse(int stateCode, string message=null)
        {
            StateCode = stateCode;
            Message = message?? GetDefaultMessage(stateCode);
        }

        private string GetDefaultMessage(int stateCode)
        {
            return stateCode switch
            {
                400 => "The provided data does not meet the validation requirements. Please verify your inputs and try again.",
                401 => "Invalid credentials provided. Please log in with valid credentials to access this resource",
                404 => "The page or resource you requested does not exist",
                500 => "We're experiencing technical difficulties. Our team is working to resolve the issue as quickly as possible.",
                _ => "unhanded error message"
            };
        }

        public int StateCode { get; set; }
        public string Message { get; set; }
    }
}
