using AutoMapper;
using HCSN.MF1759.Application;
using HCSN.MF1759.Domain;
using HCSN.MF1759.Infrastructure;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using NSubstitute.ReturnsExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCSN.MF1759.Domain
{
    [TestFixture]
    public class DepartmentManagerTests
    {
        private IDepartmentRepository _departmentRepository;

        private IDepartmentManager _departmentManager;

        [SetUp]
        public void SetUp()
        {
            _departmentRepository = Substitute.For<IDepartmentRepository>();
            _departmentManager = new DepartmentManager(_departmentRepository);
        }

        /// <summary>
        /// Test với đầu vào là mã phòng ban đã tồn tại
        /// </summary>
        /// <returns></returns>
        /// Author: nxhinh (25/09/2023)
        [Test]
        public async Task CheckExistByCodeAsync_ExistDepartment_ConflicException()
        {
            // Arrange
            var code = "Department_Code";

            _departmentRepository.FindByCodeAsync(code).Returns(new Department());

            // Act && Assert
            Assert.ThrowsAsync<ConflictException>(async () => await _departmentManager.CheckExistByCodeAsync(code));

            await _departmentRepository.Received(1).FindByCodeAsync(code);
        }

        /// <summary>
        /// Test với đầu vào là mã phòng ban chưa tồn tại
        /// </summary>
        /// <returns></returns>
        /// Author: nxhinh (25/09/2023)
        [Test]
        public async Task CheckExistByCodeAsync_NotExistDepartment_Success()
        {
            // Arrange
            var code = "Department_Code";

            _departmentRepository.FindByCodeAsync(code).ReturnsNull();

            // Act
            await _departmentManager.CheckExistByCodeAsync(code);

            // Assert 
            await _departmentRepository.Received(1).FindByCodeAsync(code);
        }
    }
}
