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
    public class FixedAssetCategoryManagerTests
    {
        private IFixedAssetCategoryRepository _fixedAssetCategoryRepository;

        private IFixedAssetCategoryManager _fixedAssetCategoryManager;

        [SetUp]
        public void SetUp()
        {
            _fixedAssetCategoryRepository = Substitute.For<IFixedAssetCategoryRepository>();
            _fixedAssetCategoryManager = new FixedAssetCategoryManager(_fixedAssetCategoryRepository);
        }

        /// <summary>
        /// Test với đầu vào là mã phòng ban đã tồn tại
        /// </summary>
        /// <returns></returns>
        /// Author: nxhinh (25/09/2023)
        [Test]
        public async Task CheckExistByCodeAsync_ExistFixedAssetCategory_ConflicException()
        {
            // Arrange
            var code = "FixedAssetCategory_Code";

            _fixedAssetCategoryRepository.FindByCodeAsync(code).Returns(new FixedAssetCategory());

            // Act && Assert
            Assert.ThrowsAsync<ConflictException>(async () => await _fixedAssetCategoryManager.CheckExistByCodeAsync(code));

            await _fixedAssetCategoryRepository.Received(1).FindByCodeAsync(code);
        }

        /// <summary>
        /// Test với đầu vào là mã phòng ban chưa tồn tại
        /// </summary>
        /// <returns></returns>
        /// Author: nxhinh (25/09/2023)
        [Test]
        public async Task CheckExistByCodeAsync_NotExistFixedAssetCategory_Success()
        {
            // Arrange
            var code = "FixedAssetCategory_Code";

            _fixedAssetCategoryRepository.FindByCodeAsync(code).ReturnsNull();

            // Act
            await _fixedAssetCategoryManager.CheckExistByCodeAsync(code);

            // Assert 
            await _fixedAssetCategoryRepository.Received(1).FindByCodeAsync(code);
        }
    }
}
