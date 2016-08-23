using System.Threading.Tasks;
using Bookify.App.Core.Interfaces.Services;

namespace Bookify.App.Core.Services
{
    public class DelayService : IDelayService
    {
        public async Task Delay(int msDuration)
        {
            await Task.Delay(msDuration);
        }
    }
}