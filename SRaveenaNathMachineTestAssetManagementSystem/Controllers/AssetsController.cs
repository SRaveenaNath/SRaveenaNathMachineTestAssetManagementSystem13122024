using Microsoft.AspNetCore.Mvc;
using SRaveenaNathMachineTestAssetManagementSystem.Model;
using SRaveenaNathMachineTestAssetManagementSystem.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SRaveenaNathMachineTestAssetManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetsController : ControllerBase
    {
        private readonly IAssetRepository _assetRepository;

        public AssetsController(IAssetRepository assetRepository)
        {
            _assetRepository = assetRepository;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAsset([FromBody] Asset asset)
        {
            if (asset == null || asset.PurchaseOrderId <= 0)
                return BadRequest("Invalid asset data.");

            var result = await _assetRepository.CreateAssetAsync(asset);
            if (result == null)
                return BadRequest("Asset creation failed. Ensure the purchase order is valid and registered internally.");

            return CreatedAtAction(nameof(GetAssetById), new { id = result.AssetId }, result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAssetById(int id)
        {
            var asset = await _assetRepository.GetAssetByIdAsync(id);
            if (asset == null)
                return NotFound();

            return Ok(asset);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsset(int id, [FromBody] Asset updatedAsset)
        {
            if (id != updatedAsset.AssetId)
                return BadRequest("Asset ID mismatch.");

            var result = await _assetRepository.UpdateAssetAsync(updatedAsset);
            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsset(int id)
        {
            var result = await _assetRepository.DeleteAssetAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchAssets([FromQuery] string serialNumber)
        {
            var assets = await _assetRepository.SearchAssetsAsync(serialNumber);
            return Ok(assets);
        }
    }
}
