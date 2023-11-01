using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace HCSN.MF1759.Web.UnitTests
{
    [TestFixture]
    public class CalculatorTests
    {
        private Calculator _cal;

        [SetUp]
        public void Setup()
        {
            _cal = new Calculator();
        }

        /// <summary>
        /// Hàm unit test tổng 2 số nguyên
        /// <param name="x">Toán hạng 1</param>
        /// <param name="y">Toán hạng 2</param>
        /// <param name="expectedResult">Kết quả mong muốn</param>
        /// </summary>
        /// Author: nxhinh (12/09/2023)
        [Test]
        [TestCase(1, 2, 3)]
        [TestCase(2, -3, -1)]
        [TestCase(1, int.MaxValue, int.MaxValue + (long)1)]
        public void Add_ValidInput_Sum2Digit(int x, int y, long expectedResult)
        {
            // Act
            var actualResult = _cal.Add(x, y);

            // Assert
            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        /// <summary>
        /// Hàm unit test tổng các số nguyên từ chuỗi
        /// Test khi đầu vào rỗng
        /// <param name="input">Chuỗi đầu vào</param>
        /// </summary>
        /// Author: nxhinh (12/09/2023)
        [Test]
        [TestCase(null)]
        public void Add_NullInput_ThrowException(string input)
        {
            // Arrange
            var expectedMessage = "Chuỗi truyền vào không thể là null!";

            try
            {
                var actualResult = _cal.Add(input);
            }
            catch (Exception ex)
            {
                Assert.That(ex.Message, Is.EqualTo(expectedMessage));
            }
        }

        /// <summary>
        /// Hàm unit test tổng các số nguyên từ chuỗi
        /// Test khi đầu vào không hợp lệ
        /// <param name="input">Chuỗi đầu vào</param>
        /// </summary>
        /// Author: nxhinh (12/09/2023)
        [Test]
        [TestCase("2, a, 3, t")]
        [TestCase("1,a, 3, t,4")]
        [TestCase("1, 2, three")]
        [TestCase("1, 2, int.MaxValue")]
        [TestCase("-3  ,a, -3, t,4")]
        public void Add_InvalidInput_ThrowException(string input)
        {
            // Arrange
            var expectedMessage = "Chuỗi truyền vào không hợp lệ!";

            try
            {
                var actualResult = _cal.Add(input);
            }
            catch (Exception ex)
            {
                Assert.That(ex.Message, Is.EqualTo(expectedMessage));
            }  
        }

        /// <summary>
        /// Hàm unit test tổng các số nguyên từ chuỗi
        /// Test khi đầu vào là chuỗi hợp lệ, bao gồm chuỗi rỗng
        /// <param name="input">Chuỗi đầu vào</param>
        /// <param name="expectedResult">Kết mong muốn</param>
        /// </summary>
        /// Author: nxhinh (12/09/2023)
        [Test]
        [TestCase("", 0)]
        [TestCase("1", 1)]
        [TestCase("1,2,3", 6)]
        [TestCase("1, 2, 3", 6)]
        [TestCase("1, 2, 3,  5,3", 14)]
        [TestCase("1.0, 2.4, 3,  5,3", 14.4)]
        public void Add_ValidInput_SumDigits(string input, double expectedResult)
        {
            // Act
            var actualResult = _cal.Add(input);

            // Assert
            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        /// <summary>
        /// Hàm unit test tổng các số nguyên từ chuỗi
        /// Test khi đầu vào có chứa số âm
        /// <param name="input">Chuỗi đầu vào</param>
        /// <param name="expectedNegative">Chuỗi số âm mong muốn</param>
        /// </summary>
        /// Author: nxhinh (12/09/2023)
        [Test]
        [TestCase("1, -2, -3", "-2, -3")]
        [TestCase("-1,-2,   3, -4", "-1, -2, -4")]
        public void Add_ValidInput_InvalidResult(string input, string expectedNegative)
        {
            // Arrange
            var expectedMessage = $"Không chấp nhận toán hạng âm: {expectedNegative}";

            try
            {
                var actualResult = _cal.Add(input);
            }
            catch (Exception ex)
            {
                Assert.That(ex.Message, Is.EqualTo(expectedMessage));
            }
        }


        /// <summary>
        /// Hàm unit test hiệu 2 số nguyên
        /// <param name="x">Toán hạng 1</param>
        /// <param name="y">Toán hạng 2</param>
        /// <param name="expectedResult">Kết quả mong muốn</param>
        /// </summary>
        /// Author: nxhinh (12/09/2023)
        [Test]
        [TestCase(5, 2, 3)]
        [TestCase(-4, -3, -1)]
        [TestCase(1, int.MaxValue, (long)1 - int.MaxValue)]
        [TestCase(int.MaxValue, int.MinValue, (long)2 * int.MaxValue + 1)]
        public void Sub_ValidInput_Sub2Digit(int x, int y, long expectedResult)
        {
            // Act
            var actualResult = _cal.Sub(x, y);

            // Assert
            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        /// <summary>
        /// Hàm unit test tích 2 số nguyên
        /// <param name="x">Toán hạng 1</param>
        /// <param name="y">Toán hạng 2</param>
        /// <param name="expectedResult">Kết quả mong muốn</param>
        /// </summary>
        /// Author: nxhinh (12/09/2023)
        [Test]
        [TestCase(1, 2, 2)]
        [TestCase(-4, -3, 12)]
        [TestCase(int.MaxValue, int.MinValue, (long)int.MaxValue * int.MinValue)]
        public void Mul_ValidInput_Mul2Digit(int x, int y, long expectedResult)
        {
            // Arrange
            var _cal = new Calculator();
            // Act
            var actualResult = _cal.Mul(x, y);

            // Assert
            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        /// <summary>
        /// Hàm unit test thương 2 số nguyên khi chia cho 0
        /// <param name="x">Toán hạng 1</param>
        /// </summary>
        /// Author: nxhinh (12/09/2023)
        [Test]
        [TestCase(1)]
        [TestCase(4)]
        public void Div_DevidedByZero_ThrowException(int x)
        {
            // Arrange
            var y = 0;
            var expectedResult = "Không được chia cho 0!";
            var _cal = new Calculator();
            // Act
            try
            {
                var actualResult = _cal.Div(x, y);
            }
            catch (Exception ex)
            {
                Assert.That(ex.Message, Is.EqualTo(expectedResult));

            }
        }

        /// <summary>
        /// Hàm unit test thương 2 số nguyên
        /// <param name="x">Toán hạng 1</param>
        /// <param name="y">Toán hạng 2</param>
        /// <param name="expectedResult">Kết quả mong muốn</param>
        /// </summary>
        /// Author: nxhinh (12/09/2023)
        [Test]
        [TestCase(1, 2, 0.5)]
        [TestCase(3, 8, (double)3 / 8)]
        [TestCase(2, 3, 0.6666666666666666666)]
        public void Div_ValidInput_Div2Digit(int x, int y, double expectedResult)
        {
            // Arrange
            var _cal = new Calculator();
            // Act
            var actualResult = _cal.Div(x, y);

            // Assert
            Assert.That(Math.Abs(actualResult - expectedResult), Is.LessThan(10e-6));
        }
    }
}
