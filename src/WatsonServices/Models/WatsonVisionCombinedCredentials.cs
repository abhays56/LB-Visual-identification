﻿namespace WatsonServices.Models
{
    public class WatsonVisionCombinedCredentials : IValidated
    {
        public string ApiEndPoint { get; set; }
        public string ApiKey { get; set; }

        public bool IsValid
        {
            get
            {
                return !(string.IsNullOrEmpty(ApiEndPoint) || string.IsNullOrEmpty(ApiKey));
            }
        }
    }
}
