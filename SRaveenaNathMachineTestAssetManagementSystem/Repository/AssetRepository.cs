using Microsoft.EntityFrameworkCore;
using SRaveenaNathMachineTestAssetManagementSystem.Model;

namespace SRaveenaNathMachineTestAssetManagementSystem.Repository
{
    public class AssetRepository : IAssetRepository
    {
        private readonly MachineTestDbContext _context;

        public AssetRepository(MachineTestDbContext context)
        {
            _context = context;
        }

        public async Task<Asset> CreateAssetAsync(Asset asset)
        {
            var purchaseOrder = await _context.PurchaseOrders
                .FirstOrDefaultAsync(po => po.PurchaseOrderId == asset.PurchaseOrderId &&
                                           po.Status == "Asset Details Registered Internally");

            if (purchaseOrder == null)
                return null;

            _context.Assets.Add(asset);
            await _context.SaveChangesAsync();
            return asset;
        }

        public async Task<Asset> GetAssetByIdAsync(int assetId)
        {
            return await _context.Assets.FindAsync(assetId);
        }

        public async Task<bool> UpdateAssetAsync(Asset asset)
        {
            var existingAsset = await _context.Assets.FindAsync(asset.AssetId);
            if (existingAsset == null)
                return false;

            existingAsset.SerialNumber = asset.SerialNumber;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAssetAsync(int assetId)
        {
            var asset = await _context.Assets.FindAsync(assetId);
            if (asset == null)
                return false;

            _context.Assets.Remove(asset);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Asset>> SearchAssetsAsync(string serialNumber)
        {
            return await _context.Assets
                .Where(a => a.SerialNumber.Contains(serialNumber))
                .ToListAsync();
        }
    }
}
