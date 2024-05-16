using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using LikhodedDynamics.Sber.GigaChatSDK.Models;

namespace Infrastructure.Services.Options
{
    public class MessageOptions
    {
        [JsonPropertyName("model")]
        public string model { get; set; }

        [JsonPropertyName("messages")]
        public List<MessageContent> messages { get; set; }

        [JsonPropertyName("temperature")]
        public float temperature { get; set; }

        [JsonPropertyName("top_p")]
        public float top_p { get; set; }

        [JsonPropertyName("n")]
        public long n { get; set; }

        [JsonPropertyName("stream")]
        public bool stream { get; set; }

        [JsonPropertyName("max_tokens")]
        public long max_tokens { get; set; }

        [JsonPropertyName("function_call")]
        public string function_call { get; set; }

        public MessageOptions(List<MessageContent>? messages = null, string model = "GigaChat:latest", float temperature = 0.87f, float top_p = 0.47f, long n = 1L, bool stream = false, long max_tokens = 512L, string function_call = "auto")
        {
            List<MessageContent> list = new List<MessageContent>();
            this.model = model;
            this.messages = messages ?? list;
            this.temperature = temperature;
            this.top_p = top_p;
            this.n = n;
            this.stream = stream;
            this.max_tokens = max_tokens;
            this.function_call = function_call;
        }
    }
}

