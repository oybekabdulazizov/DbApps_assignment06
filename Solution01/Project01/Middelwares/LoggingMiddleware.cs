﻿using Microsoft.AspNetCore.Http;
using Project01.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project01.Middelwares
{
    public class LoggingMiddleware
    {
        public readonly RequestDelegate _next;

        public LoggingMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task InvokeAsync(HttpContext context, IDbService service) 
        {

            if (context.Request != null) 
            {
                string path = context.Request.Path;
                string method = context.Request.Method;
                string queryString = context.Request.QueryString.ToString();
                string bodyString = context.Request.Body.ToString();

                using (StreamReader reader = new StreamReader(context.Request.Body, Encoding.UTF8, true, 1024, true))
                {
                    bodyString = await reader.ReadToEndAsync();
                }

                service.SaveLogData(path, method, queryString, bodyString);
            }

            await _next(context);
        }
    }
}
