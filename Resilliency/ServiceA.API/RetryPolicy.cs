using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Http;


namespace ServiceA.API
{
    public  class RetryPolicy
    {
       public static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
            {
                return HttpPolicyExtensions.HandleTransientHttpError().OrResult(msg => msg.StatusCode == HttpStatusCode.NotFound).WaitAndRetryAsync(5, retryAttempt =>
                {
                    Debug.WriteLine($"Retry Count: {retryAttempt}");
                    return TimeSpan.FromSeconds(10);
                }, onRetryAsync: onRetryAsync);
            }

      private static Task onRetryAsync(DelegateResult<HttpResponseMessage> arg1, TimeSpan arg2)
        {
            Debug.WriteLine($"Request is made again: {arg2.TotalMilliSeconds}");
            return Task.CompletedTask;
        }
    }
}