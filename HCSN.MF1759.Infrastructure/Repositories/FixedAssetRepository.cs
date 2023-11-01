using Dapper;
using HCSN.MF1759.Domain;
using static Dapper.SqlMapper;

namespace HCSN.MF1759.Infrastructure
{
    /// <summary>
    /// Repository của tài sản
    /// </summary>
    /// Created by: nxhinh (11/09/2023) 
    public class FixedAssetRepository : BaseRepository<FixedAsset>, IFixedAssetRepository
    {
        public override string TableName { get; protected set; } = "fixed_asset";
        public override string TableCode { get; protected set; } = "fixed_asset_code";
        public override string TableKey { get; protected set; } = "fixed_asset_id";


        public FixedAssetRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

    }
}
