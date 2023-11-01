using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCSN.MF1759.Domain
{
    public class TransferDocumentManager: ITransferDocumentManager
    {
        private readonly ITransferDocumentRepository _transferDocumentRepository;
        public TransferDocumentManager(ITransferDocumentRepository transferDocumentRepository)
        {
            _transferDocumentRepository = transferDocumentRepository;

        }

        /// <summary>
        /// Kiểm tra dữ liệu chứng từ đã hợp lệ chưa
        /// </summary>
        /// <param name="transferDocument">Chi tiết chứng từ</param>
        /// <returns></returns>
        /// Author: nxhinh (30/10/2023)  
        public Task ValidateData(TransferDocument transferDocument)
        {
            if (transferDocument.document_date.Date > transferDocument.transfer_date.Date)
            {
                throw new InvalidDataException(ResourceVN.Error_DocumentDateMustLessThenTransferDate);
            }

            return Task.CompletedTask;
        }

        /// <summary>
        /// Kiểm tra mã chứng từ mới đã tồn tại chưa
        /// </summary>
        /// <param name="code">Mã chứng từ</param>
        /// <param name="transferDocumentId">Id chứng từ</param>
        /// <returns></returns>
        /// Author: nxhinh (25/10/2023)  
        public async Task CheckExistAsync(string code, Guid? transferDocumentId)
        {
            // Đếm số bản ghi có id và code trùng với dữ liệu truyền vào
            var count = await _transferDocumentRepository.CountByCodeOrId(code, transferDocumentId != null ? transferDocumentId : Guid.Empty);

            // Trả về 1 bản ghi có id tương ứng, trả nếu code đã có trong db và khác code cũ sẽ trả về thêm bản ghi, khi đó thông báo lỗi
            if ((transferDocumentId == null & count > 0) || (transferDocumentId != null & count > 1))
            {
                throw new ConflictException(ResourceVN.Error_DuplicateTransferDocumentCode.Replace("{code}", code));
            }
        }
    }
}
