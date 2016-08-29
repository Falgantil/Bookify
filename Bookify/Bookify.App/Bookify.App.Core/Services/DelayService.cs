using System.Threading.Tasks;
using Bookify.App.Core.Interfaces.Services;

namespace Bookify.App.Core.Services
{
    /// <summary>
    /// The Delay Service implementation that uses a <see cref="Task.Delay(int)"/>
    /// </summary>
    /// <seealso cref="IDelayService" />
    public class DelayService : IDelayService
    {
        /// <summary>
        /// Delays the execution of whatever awaits this, for <see cref="msDuration" /> milliseconds.
        /// </summary>
        /// <param name="msDuration">Duration of the ms.</param>
        /// <returns></returns>
        public async Task Delay(int msDuration)
        {
            await Task.Delay(msDuration);
        }
    }
}