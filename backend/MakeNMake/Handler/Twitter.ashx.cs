﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MakeNMake.Handler
{
    /// <summary>
    /// Summary description for Twitter
    /// </summary>
    public class Twitter : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}