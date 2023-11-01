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
    public class FixedAssetManagerTests
    {
        private IFixedAssetRepository _fixedAssetRepository;

        private IFixedAssetManager _fixedAssetManager;

        [SetUp]
        public void SetUp()
        {
            _fixedAssetRepository = Substitute.For<IFixedAssetRepository>();
            _fixedAssetManager = new FixedAssetManager(_fixedAssetRepository);
        }

        /// <summary>
        /// Test với đầu vào là mã phòng ban đã tồn tại khi thêm mới
        /// </summary>
        /// <returns></returns>
        /// Author: nxhinh (25/09/2023)
        [Test]
        public async Task CheckExistByCodeAsync_ExistFixedAsset_ConflicException()
        {
            // Arrange
            var code = "FixedAsset_Code";

            var id = Guid.Empty;

            _fixedAssetRepository.CountByCodeOrId(code, id).Returns(1);

            // Act && Assert
            Assert.ThrowsAsync<ConflictException>(async () => await _fixedAssetManager.CheckExistAsync(code, null));

            await _fixedAssetRepository.Received(1).CountByCodeOrId(code, id);
        }

        /// <summary>
        /// Test với đầu vào là mã phòng ban chưa tồn tại khi thêm mới
        /// </summary>
        /// <returns></returns>
        /// Author: nxhinh (25/09/2023)
        [Test]
        public async Task CheckExistByCodeAsync_NotExistFixedAsset_Success()
        {
            // Arrange
            var code = "FixedAsset_Code";

            var id = Guid.Empty;

            _fixedAssetRepository.CountByCodeOrId(code, id).Returns(0);

            // Act && Assert
            await _fixedAssetManager.CheckExistAsync(code, null);

            await _fixedAssetRepository.Received(1).CountByCodeOrId(code, id);
        }

        /// <summary>
        /// Test với đầu vào là mã phòng ban đã tồn tại khi chỉnh sửa
        /// </summary>
        /// <returns></returns>
        /// Author: nxhinh (25/09/2023)
        [Test]
        public async Task CheckExistByCodeAsync_ExistFixedAssetWhenUpdate_ConflicException()
        {
            // Arrange
            var code = "FixedAsset_Code";

            var id = Guid.NewGuid();

            _fixedAssetRepository.CountByCodeOrId(code, id).Returns(2);

            // Act && Assert
            Assert.ThrowsAsync<ConflictException>(async () => await _fixedAssetManager.CheckExistAsync(code, id));

            await _fixedAssetRepository.Received(1).CountByCodeOrId(code, id);
        }

        /// <summary>
        /// Test với đầu vào là mã phòng ban chưa tồn tại khi chỉnh sửa
        /// </summary>
        /// <returns></returns>
        /// Author: nxhinh (25/09/2023)
        [Test]
        public async Task CheckExistByCodeAsync_NotExistFixedAssetWhenUpdate_Success()
        {
            // Arrange
            var code = "FixedAsset_Code";

            var id = Guid.NewGuid();

            _fixedAssetRepository.CountByCodeOrId(code, id).Returns(1);

            // Act && Assert
            await _fixedAssetManager.CheckExistAsync(code, id);

            await _fixedAssetRepository.Received(1).CountByCodeOrId(code, id);
        }
    }
}
