﻿using System;
using System.Collections.Generic;

namespace Example.LitJSON
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class CommentAttribute : Attribute
    {
        public string Comment { get; set; }

        public CommentAttribute(string comment)
        {
            Comment = comment;
        }
    }
}