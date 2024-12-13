using SRaveenaNathMachineTestAssetManagementSystem.Model;

namespace SRaveenaNathMachineTestAssetManagementSystem.Repository
{
    public interface IAssetRepository
    {
        Task<Asset> CreateAssetAsync(Asset asset);
        Task<Asset> GetAssetByIdAsync(int assetId);
        Task<bool> UpdateAssetAsync(Asset asset);
        Task<bool> DeleteAssetAsync(int assetId);
        Task<IEnumerable<Asset>> SearchAssetsAsync(string serialNumber);
    }
}
