using System.Collections;

namespace Example.JsonNET
{
    public class Protocol_GET_KEY_REQ
    {
        [Comment("버전")]
        public string szVersion;
    }

    public class Protocol_GET_KEY_ACK
    {
        [Comment("에러코드")]
        public int nErrorCode;
        [Comment("암호화키")]
        public string szKey;
    }
}