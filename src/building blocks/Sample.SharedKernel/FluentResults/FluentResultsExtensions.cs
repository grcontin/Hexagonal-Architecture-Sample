using FluentResults;
using System.Text;

namespace Sample.SharedKernel.FluentResults
{
    public static class FluentResultsExtensions
    {
        public static string DisplayErrors<T>(this Result<T> result) where T : class
        {
            StringBuilder stringBuilder = new StringBuilder();

            if (result.Errors?.Count > 0)
            {
                result.Errors.ForEach((errorMessage) =>
                {
                    stringBuilder.AppendLine(errorMessage.Message);
                });
            }

            return stringBuilder.ToString();
        }
    }
}
