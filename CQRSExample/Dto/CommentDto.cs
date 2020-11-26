using Newtonsoft.Json;

namespace CQRSExample.Dto
{
    public partial class CommentDto
    {
        [JsonProperty("postId")]
        public long PostId { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }
    }
}
