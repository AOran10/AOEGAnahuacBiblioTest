﻿namespace SL
{
    public class ApiKey
    {
        private readonly RequestDelegate _next;
        private const string APIKEY = "Apikey";
        public ApiKey(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            if(!context.Request.Headers.TryGetValue(APIKEY, out var extractedApiKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Api key was not provider"); 
                return;
            }

            var AppSettings = context.RequestServices.GetRequiredService<IConfiguration>();

            var apikey = AppSettings.GetValue<string>(APIKEY);

            if(!apikey.Equals(extractedApiKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Unauthorized client");
                return;
            }

            await _next(context);
        }
    }
}
