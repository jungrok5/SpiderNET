using Newtonsoft.Json;
using System.Collections;

namespace Example.JsonNET
{
    [JsonObject]
    public class Protocol_LOGIN_REQ
    {
        [Comment("디바이스 고유값")]
        [JsonProperty("udid")]
        public string szUdid;
        [Comment("플랫폼")]
        [JsonProperty("p")]
        public byte bPlatform;
    }

    [JsonObject]
    public class Protocol_LOGIN_ACK
    {
        [Comment("에러코드")]
        [JsonProperty("ec")]
        public int nErrorCode;
        [Comment("유저 고유값")]
        [JsonProperty("id")]
        public long biUserID;
        [Comment("유저 명")]
        [JsonProperty("name")]
        public string szUserName;
    }
}