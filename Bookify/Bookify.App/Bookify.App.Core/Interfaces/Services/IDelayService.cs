using System.Threading.Tasks;

namespace Bookify.App.Core.Interfaces.Services
{
    public interface IDelayService
    {
        Task Delay(int msDuration);
    }
}