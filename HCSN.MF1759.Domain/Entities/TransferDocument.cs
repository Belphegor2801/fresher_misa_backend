namespace HCSN.MF1759.Domain
{
    public class TransferDocument: BaseAuditableEntity, IHaskey, IHasCode
    {
        /// <summary>
        /// Id chứng từ
        /// </summary>
        public Guid document_id { get; set; }

        /// <summary>
        /// Mã chứng từ
        /// </summary>
        public string document_code { get; set; }

        /// <summary>
        /// Ngày điều chuyển
        /// </summary>
        public DateTime transfer_date { get; set; }

        /// <summary>
        /// Ngày chứng từ
        /// </summary>
        public DateTime document_date { get; set; }

        /// <summary>
        /// Nguyên giá
        /// </summary>
        public decimal? cost { get; set; }

        /// <summary>
        /// Giá trị còn lại
        /// </summary>
        public decimal? remaining_value { get; set; }

        /// <summary>
        /// Năm bắt đầu theo dõi
        /// </summary>
        public int? tracked_year { get; set; }

        /// <summary>
        /// Ghi chú
        /// </summary>
        public string? note { get; set; }

        /// <summary>
        /// Lấy id chứng từ
        /// </summary>
        /// <returns>id của chứng từ</returns>
        /// Author: nxhinh (25/10/2023)  

        public Guid GetKey()
        {
            return document_id;
        }

        /// <summary>
        /// Trả về mã chứng từ
        /// </summary>
        /// <returns>Mã</returns>
        /// Author: nxhinh (25/10/2023)  
        public string GetCode()
        {
            return document_code ?? "";
        }

    }
}
