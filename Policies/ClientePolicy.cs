using Polly;
using Polly.Retry;

namespace PeticionService.Policies
{
    public class ClientePolicy
    {
        public AsyncRetryPolicy<HttpResponseMessage> ImmediatoHttpRetry { get; }
        public AsyncRetryPolicy<HttpResponseMessage> LinearHttpRetry { get; }
        public AsyncRetryPolicy<HttpResponseMessage> ExponentialHttpRetry { get; }
        public ClientePolicy()
        {
            ImmediatoHttpRetry = Policy.HandleResult<HttpResponseMessage>(
                res => !res.IsSuccessStatusCode).RetryAsync(5);

            LinearHttpRetry = Policy.HandleResult<HttpResponseMessage>(
                res => !res.IsSuccessStatusCode)
                .WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromSeconds(3));

            ExponentialHttpRetry = Policy.HandleResult<HttpResponseMessage>(
           res => !res.IsSuccessStatusCode)
           .WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));


        }
    }
}