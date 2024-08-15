using SonarTrack.Application.Dtos;

namespace SonarTrack.Infrastructure.Abstractions
{
    public abstract class RestHttpClient
    {
        protected readonly HttpClient _httpClient;

        protected RestHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private async Task<OperationResultDto<TResult>> GetResultAsync<TResult>(HttpResponseMessage response)
            where TResult : class
        {
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsAsync<TResult>();
                return OperationResultDto<TResult>.Ok(result);
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                return OperationResultDto<TResult>.Fail(message);
            }
        }

        protected virtual async Task<OperationResultDto<TResult>> GetAsync<TResult>(string route)
          where TResult : class
        {
            using var response = await _httpClient.GetAsync(route);
            return await GetResultAsync<TResult>(response);
        }

        protected void SetAuthorization(string token)
        {
            _httpClient.DefaultRequestHeaders.Add("Authorization", token);
        }
    }
}
