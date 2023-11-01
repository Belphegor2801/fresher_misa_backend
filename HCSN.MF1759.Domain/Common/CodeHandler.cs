using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCSN.MF1759.Domain
{
    /// <summary>
    /// Xử lý mã 
    /// </summary>
    /// Created by: nxhinh (30/09/2023)
    public class CodeHandler
    {
        /// <summary>
        /// Tạo mã từ các phần
        /// </summary>
        /// <param name="itemCode">Các thành phần của mã</param>
        /// <returns>Mã</returns>
        /// Created by: nxhinh (30/09/2023)
        public static string NewCode(IEnumerable<string> codes)
        {
            var code = "";

            // Tiền tố
            var prefix = "TS";
            // Hậu tố
            var suffix = "";
            // Giá trị số
            var baseValue = 0;
            // Độ dài giá trị số 
            var length = 5;

            // Lấy tiền tố, hậu tố
            if (codes.Count() > 0)
            {
                var firstTtemcode = SplitCode(codes.FirstOrDefault());
                prefix = firstTtemcode.prefix;
                suffix = firstTtemcode.suffix;
                length = firstTtemcode.value_length;
            }

            var baseValueList = new List<int>();
            
            foreach (var c in codes)
            {
                var itemCode = SplitCode(c);
                baseValueList.Add(itemCode.base_value);
            }


            var newBaseValue = baseValueList.Max() + 1;
            var newItemCode = new ItemCode
            {
                prefix = prefix,
                base_value = newBaseValue,
                suffix = suffix,
                value_length = length
            };

            return GetCode(newItemCode);
        }

        /// <summary>
        /// Tạo mã từ các phần
        /// </summary>
        /// <param name="itemCode">Các thành phần của mã</param>
        /// <returns>Mã</returns>
        /// Created by: nxhinh (30/09/2023)
        public static string GetCode(ItemCode itemCode)
        {
            var code = "";
            if (itemCode != null)
            {
                // Lấy giá trị số
                var baseValue = itemCode.base_value;
                // Lấy độ dài giá trị số
                var length = itemCode.value_length;
                // Lấy tiền tố
                var prefix = itemCode.prefix ?? "";
                // Lấy hậu tố
                var suffix = itemCode.suffix ?? "";

                // Lấy giá trị số dạng chuỗi
                var baseValueString = baseValue.ToString();

                // Nếu độ dài chuỗi nhỏ hơn độ dài quy định thì thêm các ký tự 0 vào đầu
                if (baseValueString.Length < length)
                {
                    baseValueString = baseValueString.PadLeft(length, '0');
                }

                // Tạo mã
                code = prefix + baseValueString + suffix;
            }

            return code;
        }

        /// <summary>
        /// Tách mã thành các phần
        /// </summary>
        /// <param name="code">Mã</param>
        /// <returns>Các thành phần của mã</returns>
        /// Created by: nxhinh (30/09/2023)
        public static ItemCode SplitCode(string code)
        {
            // Tiền tố
            var prefix = "";
            // Hậu tố
            var suffix = "";
            // Giá trị số
            var baseValue = 0;
            // Độ dài giá trị số 
            var length = 0;

            if (!string.IsNullOrEmpty(code))
            {

                // Vị trí của số đầu tiên và cuối cùng trong mã 
                var firstNumberIndex = code.IndexOfAny("0123456789".ToCharArray());
                var lastNumberIndex = code.LastIndexOfAny("0123456789".ToCharArray());

                // prefix là từ đầu đến ký tự số đầu tiên
                if (firstNumberIndex >= 0)
                {
                    prefix = code.Substring(0, firstNumberIndex);
                }
                // Nếu không có số thì lấy toàn bộ
                else
                {
                    prefix = code;
                }

                // suffix là từ ký tự số cuối cùng đến hết
                if (lastNumberIndex >= 0)
                {
                    suffix = code.Substring(lastNumberIndex + 1);
                }

                // numberPart là phần số ở giữa
                var numberPart = "";
                if (firstNumberIndex >= 0 && lastNumberIndex >= 0)
                {
                    numberPart = code.Substring(firstNumberIndex, lastNumberIndex - firstNumberIndex + 1);
                }

                // Chuyển thành số và lấy độ dài 
                if (int.TryParse(numberPart, out int result))
                {
                    // Chuyển được thì lấy giá trị baseValue và length
                    baseValue = result;
                    length = numberPart.Length;
                }
                else
                {
                    // Không chuyển được trả về giá trị mặc định
                    baseValue = 0;
                    length = 5;
                }
            }
            // Nếu không có bản ghi nào thì trả về mã mặc định TS00000
            else
            {
                prefix = "TS";
                baseValue = 0;
                suffix = "";
                length = 5;
            }

            var itemCode = new ItemCode
            {
                prefix = prefix,
                base_value = baseValue,
                suffix = suffix,
                value_length = length
            };

            return itemCode;
        }
    }
}
