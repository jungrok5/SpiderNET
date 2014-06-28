using System.Collections;

namespace Example.MiniJSON
{
    public class ErrorCode_LOGIN : ErrorCode<ErrorCode_LOGIN>
    {
        [Comment("새로운 계정이 생성되었습니다")]
        public const int NewUDID = 1;
        [Comment("잘못된 UDID입니다")]
        public const int WrongUDID = 2;
    }
}