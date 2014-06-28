using System.Collections;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Example.ErrorCode
{
    public class ErrorCode<T>
    {
        [Comment("성공")]
        public const int Success = 0;

        public static Dictionary<Type, Dictionary<int, string>> CommentTable = new Dictionary<Type, Dictionary<int, string>>();
        public static void LoadComment()
        {
            if (CommentTable.ContainsKey(typeof(T)) == true)
                return;

            Dictionary<int, string> errorCodeTable = new Dictionary<int, string>();
            foreach (var fi in typeof(T).GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy))
            {
                if (fi.IsLiteral == false)
                    continue;
                foreach (var attr in fi.GetCustomAttributes(typeof(CommentAttribute), true))
                {
                    CommentAttribute at = attr as CommentAttribute;
                    if (at == null)
                        continue;

                    errorCodeTable.Add((int)fi.GetValue(null), at.Comment);
                }
            }
            CommentTable.Add(typeof(T), errorCodeTable);
        }

        public static string ToString(int errorCode)
        {
            Type errorCodeClass = typeof(T);
            LoadComment();
            if (CommentTable.ContainsKey(errorCodeClass) == false)
                return string.Format("Unknown ErrorCodeClass:{0}", errorCodeClass);
            if (CommentTable[errorCodeClass].ContainsKey(errorCode) == false)
                return string.Format("Unknown ErrorCode:{0} ErrorCodeClass:{1}", errorCode, errorCodeClass);
            return CommentTable[errorCodeClass][errorCode];
        }
    }
}