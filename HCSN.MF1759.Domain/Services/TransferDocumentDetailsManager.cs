using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCSN.MF1759.Domain
{
    public class TransferDocumentDetailsManager: ITransferDocumentDetailsManager
    {
        private readonly ITransferDocumentDetailsRepository _transferDocumentDetailsRepository;
        private readonly ITransferDocumentRepository _transferDocumentRepository;
        public TransferDocumentDetailsManager(ITransferDocumentDetailsRepository transferDocumentDetailsRepository, ITransferDocumentRepository transferDocumentRepository)
        {
            _transferDocumentDetailsRepository = transferDocumentDetailsRepository;
            _transferDocumentRepository = transferDocumentRepository;
        }

        /// <summary>
        /// Validate dữ liệu nhập vào
        /// </summary>
        /// <param name="transferDocument">Chứng từ</param>
        /// <param name="transferDocumentDetails">Chi tiết chứng từ</param>
        /// <returns></returns>
        /// <exception cref="InvalidDataException">Exception lỗi dữ liệu</exception>
        /// Author: nxhinh (30/10/2023)  
        public async Task ValidateData(TransferDocument transferDocument, TransferDocumentDetails transferDocumentDetails)
        {
            var lastTransferDate = await GetLastTransferDateOfAsset(transferDocumentDetails.fixed_asset_id);

            if (DateTime.Compare(lastTransferDate, transferDocument.transfer_date) > 0)
            {
                throw new InvalidDataException(ResourceVN.Error_TransferDateError);
            }
        }

        /// <summary>
        /// Validate dữ liệu khi xóa
        /// </summary>
        /// <param name="transferDocument">Chứng từ</param>
        /// <param name="transferDocumentDetails">Chi tiết chứng từ</param>
        /// <returns></returns>
        /// <exception cref="InvalidDataException">Exception lỗi dữ liệu</exception>
        /// Author: nxhinh (30/10/2023)  
        public async Task ValidateDelete(TransferDocument transferDocument, TransferDocumentDetails transferDocumentDetails)
        {
            var entities = await _transferDocumentRepository.GetListByFixedAssetId(transferDocumentDetails.fixed_asset_id);

            if (entities != null)
            {
                foreach (var entity in entities)
                {
                    if (DateTime.Compare(entity.transfer_date, transferDocument.transfer_date) > 0)
                    {
                        throw new InvalidDataException(ResourceVN.ErrorDelete_TransferDateError.Replace("{code}", entity.document_code).Replace("{date}", entity.transfer_date.ToString("dd/MM/yyyy")));
                    } 
                }
            }
        }

        /// <summary>
        /// Lấy ngày điều chuyển cuối cùng của tài sản
        /// </summary>
        /// <param name="fixed_asset_id">Id của tài sản</param>
        /// <returns></returns>
        /// Author: nxhinh (30/10/2023)  
        private async Task<DateTime> GetLastTransferDateOfAsset(Guid fixed_asset_id)
        {
            var entities = await _transferDocumentRepository.GetListByFixedAssetId(fixed_asset_id);

            var transferDate = DateTime.MinValue;
            if (entities != null)
            {
                foreach (var entity in entities)
                {
                    transferDate = DateTime.Compare(entity.transfer_date, transferDate) > 0 ? entity.transfer_date : transferDate;
                }
            }
            return transferDate;
        }
    }
}
