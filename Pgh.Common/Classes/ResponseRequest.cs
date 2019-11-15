using System.Collections.Generic;
using Newtonsoft.Json;

namespace Pgh.Common.Classes
{
    public class ResponseRequest<TEntity> where TEntity : class
    {

        [JsonProperty("Request Response")]
        public List<TEntity> Result { get; set; } =  new List<TEntity>();

        [JsonProperty("Request Status")]
        public string Status { get; set; }

        [JsonProperty("Request Message")]
        public string MessageRemarque { get; set; }

        [JsonProperty("Request Error")]
        public string MessageError { get; set; }

    }
}