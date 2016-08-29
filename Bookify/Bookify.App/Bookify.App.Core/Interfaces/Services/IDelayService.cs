using System.Threading.Tasks;

namespace Bookify.App.Core.Interfaces.Services
{
    /// <summary>
    /// The Delay Service. Implements a simple <see cref="Task.Delay(int)"/> in the real service.
    /// This Interface allows for mocking, making it possible to unit test.
    /// </summary>
    public interface IDelayService
    {
        /// <summary>
        /// Delays the execution of whatever awaits this, for <see cref="msDuration"/> milliseconds.
        /// </summary>
        /// <param name="msDuration">Duration of the ms.</param>
        /// <returns></returns>
        Task Delay(int msDuration);
    }
}