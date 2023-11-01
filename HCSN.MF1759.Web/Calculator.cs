namespace HCSN.MF1759.Web
{
    public class Calculator
    {
        /// <summary>
        /// Hàm cộng 2 số nguyên
        /// </summary>
        /// <param name="x">Toán hạng 1</param>
        /// <param name="y">Toán hạng 2</param>
        /// <returns>Tổng 2 số nguyên</returns>
        /// Author: nxhinh (12/09/2023)
        public long Add(int x, int y)
        {
            return x + (long)y;
        }

        /// <summary>
        /// Hàm cộng các số nguyên từ chuỗi
        /// </summary>
        /// <param name="input">Chuỗi các số</param>
        /// <returns>Tổng các số nguyên</returns>
        /// Author: nxhinh (12/09/2023)
        public double Add(string input)
        {
            if (input == null)
            {
                throw new Exception("Chuỗi truyền vào không thể là null!");
            }

            // Chuỗi rỗng trả về không
            if (input == "") return 0;
            //
            string[] values = input.Split(',');
            double sum = 0;
            var negativeNumbers = new List<double>();
            bool isValid = true;

            foreach (string value in values)
            {
                double parseNum = 0;
                isValid = isValid && double.TryParse(value.Trim(), out parseNum);
                if (parseNum < 0)
                {
                    negativeNumbers.Add(parseNum);
                }
                else
                {
                    sum += parseNum;
                }
            }

            if (!isValid)
            {
                throw new Exception("Chuỗi truyền vào không hợp lệ!");
            }
            
            if (negativeNumbers.Count > 0)
            {
                var negativeString = string.Join(", ", negativeNumbers.ToArray());
                throw new Exception($"Không chấp nhận toán hạng âm: {negativeString}");
            }

            return sum;
        }

        /// <summary>
        /// Hàm trừ 2 số nguyên
        /// </summary>
        /// <param name="x">Toán hạng 1</param>
        /// <param name="y">Toán hạng 2</param>
        /// <returns>Hiệu 2 số nguyên</returns>
        /// Author: nxhinh (12/09/2023)
        /// 
        public long Sub(int x, int y) {  
            return x - (long)y; 
        }

        /// <summary>
        /// Hàm nhân 2 số nguyên
        /// </summary>
        /// <param name="x">Toán hạng 1</param>
        /// <param name="y">Toán hạng 2</param>
        /// <returns>Tích 2 số nguyên</returns>
        /// Author: nxhinh (12/09/2023)
        public long Mul(int x, int y)
        {
            return x * (long)y;
        }

        /// <summary>
        /// Hàm chia 2 số nguyên
        /// </summary>
        /// <param name="x">Toán hạng 1</param>
        /// <param name="y">Toán hạng 2</param>
        /// <returns>Thương 2 số nguyên</returns>
        /// Author: nxhinh (12/09/2023)
        public double Div(int x, int y)
        {
            if (y == 0)
            {
                throw new Exception("Không được chia cho 0!");
            }
            return (double)x / (double)y;
        }
    }
}
