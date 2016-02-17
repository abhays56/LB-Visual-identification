﻿using Newtonsoft.Json;

namespace VisualRecognition.Models
{
    public class ClassifiersResponse
    {
        [JsonProperty("classifiers")]
        public Classifier[] Classifiers { get; set; }

        public ClassifiersResponse()
        {
            Classifiers = new Classifier[0];
        }
    }
}
