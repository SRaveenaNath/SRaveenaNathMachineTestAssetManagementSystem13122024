using System.Threading.Tasks;
using SRaveenaNathMachineTestAssetManagementSystem.Model;

namespace SRaveenaNathMachineTestAssetManagementSystem.Repository
{
    public interface ILoginRepository
    {
        Task<string> AuthenticateAsync(string username, string password);
    }
}
