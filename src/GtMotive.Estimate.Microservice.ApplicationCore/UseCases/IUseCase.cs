using System.Threading.Tasks;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases
{
    /// <summary>
    /// Interface for the handler of a use case.
    /// </summary>
    /// <typeparam name="TInput">Type of the input message.</typeparam>
    /// <typeparam name="TResult">Type of the result returned by the use case.</typeparam>
    public interface IUseCase<in TInput, TResult>
    {
        /// <summary>
        /// Executes the use case.
        /// </summary>
        /// <param name="input">Input message.</param>
        /// <returns>The result of the operation.</returns>
        Task<TResult> Execute(TInput input);
    }
}
