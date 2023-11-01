using HCSN.MF1759.Domain;
using Dapper;

namespace HCSN.MF1759.Infrastructure
{
    /// <summary>
    /// Repository của loại tài sản
    /// </summary>
    /// Created by: nxhinh (22/09/2023)
    public class FixedAssetCategoryRepository : BaseRepository<FixedAssetCategory>, IFixedAssetCategoryRepository
    {
        public override string TableName { get; protected set; } = "fixed_asset_category";
        public override string TableCode { get; protected set; } = "fixed_asset_category_code";
        public override string TableKey { get; protected set; } = "fixed_asset_category_id";

        public FixedAssetCategoryRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

    }
}
