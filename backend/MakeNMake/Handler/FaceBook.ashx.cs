﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MakeNMake.Handler
{
    /// <summary>
    /// Summary description for FaceBook
    /// </summary>
    public class FaceBook : IHttpHandler
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