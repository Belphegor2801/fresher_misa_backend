using AutoMapper;
using HCSN.MF1759.Application;
using HCSN.MF1759.Domain;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NSubstitute.ReturnsExtensions;
using NUnit.Framework;

namespace HCSN.MF1759.Infrastructure
{
    [TestFixture]
    public class FixedAssetServiceTests
    {
        private IFixedAssetRepository _fixedAssetRepository { get; set; }
        private IMapper _mapper { get; set; }
        private IFixedAssetManager _fixedAssetManager { get; set; }
        private IUnitOfWork _unitOfWork { get; set; }
        private IFixedAssetExcelHandler _fixedAssetExcelHandler { get; set; }
        private FixedAssetService _fixedAssetService { get; set; }



        [SetUp]
        public void SetUp()
        {
            _fixedAssetRepository = Substitute.For<IFixedAssetRepository>();
            var config = new MapperConfiguration(cfg => cfg.AddProfile<FixedAssetProfile>());
            _mapper = config.CreateMapper();
            _fixedAssetManager = Substitute.For<IFixedAssetManager>();
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _fixedAssetExcelHandler = Substitute.For<IFixedAssetExcelHandler>();
            _fixedAssetService = new FixedAssetService(_fixedAssetRepository, _mapper, _fixedAssetManager, _unitOfWork, _fixedAssetExcelHandler);
        }

        /// <summary>
        /// Test MapCreateDtoToEntity trả về EmptyId
        /// </summary>
        /// <returns></returns>
        /// Author: nxhinh (25/09/2023)
        [Test]
        public async Task MapCreateDtoToEntity_EmptyId_ReturnIdNotEmpty()
        {
            // Arrange
            var fixedAsset = new FixedAsset()
            {
                fixed_asset_id = Guid.Empty
            };
            var fixedAssetCreateDto = new FixedAssetCreateDto();

            _fixedAssetService.MapCreateDtoToEntity(fixedAssetCreateDto).Returns(fixedAsset);

            // Act && Assert
            var fixedAssetOutput = await _fixedAssetService.MapCreateDtoToEntity(fixedAssetCreateDto);

            Assert.That(fixedAssetOutput.fixed_asset_id, Is.EqualTo(Guid.Empty));
        }

        /// <summary>
        /// Test Insert thêm thành công
        /// </summary>
        /// <returns></returns>
        /// Author: nxhinh (25/09/2023)
        [Test]
        public async Task InsertAsync_Valid_Success()
        {
            // Arrange
            var fixedAssetCreateDto = new FixedAssetCreateDto();

            // Act && Assert
            await _fixedAssetService.InsertAsync(fixedAssetCreateDto);

            await _unitOfWork.Received(1).BeginTransactionAsync();
        }

        /// <summary>
        /// Test InsertMulti thêm mã trùng lặp
        /// </summary>
        /// <returns></returns>
        /// Author: nxhinh (25/09/2023)
        [Test]
        public async Task InsertMultiAsync_DuplicateCode_ThrowException()
        {
            // Arrange
            var fixedAssetCreateDtos = new List<FixedAssetCreateDto>();
            // Tạo 10 id tương ứng 10 tài sản
            for (int i = 0; i < 10; i++)
            {
                var fixedAssetCode = "code";
                var newFixedAssetCreateDto = new FixedAssetCreateDto()
                {
                    fixed_asset_code = fixedAssetCode
                };
                fixedAssetCreateDtos.Add(newFixedAssetCreateDto);
            }

            var expectedMessage = "Mã tài sản không được trùng lặp.";

            // Act && Assert
            var exception = Assert.ThrowsAsync<Domain.InvalidDataException>(async () => await _fixedAssetService.InsertMultiAsync(fixedAssetCreateDtos));

            Assert.That(exception.Message, Is.EqualTo(expectedMessage));

            await _unitOfWork.Received(1).BeginTransactionAsync();
            await _unitOfWork.Received(1).RollBackAsync();
        }

        /// <summary>
        /// Test InsertMulti thêm mã không trùng lặp
        /// </summary>
        /// <returns></returns>
        /// Author: nxhinh (25/09/2023)
        [Test]
        public async Task InsertMultiAsync_NotDuplicateCode_Success()
        {
            // Arrange
            var fixedAssetCreateDtos = new List<FixedAssetCreateDto>();
            // Tạo 10 id tương ứng 10 tài sản
            for (int i = 0; i < 10; i++)
            {
                var fixedAssetCode = "code_" + i.ToString();
                var newFixedAssetCreateDto = new FixedAssetCreateDto()
                {
                    fixed_asset_code = fixedAssetCode
                };
                fixedAssetCreateDtos.Add(newFixedAssetCreateDto);
            }


            // Act && Assert
            await _fixedAssetService.InsertMultiAsync(fixedAssetCreateDtos);

            await _unitOfWork.Received(1).BeginTransactionAsync();
        }


        /// <summary>
        /// Test Delete với đầu vào là id không hợp lệ
        /// </summary>
        /// <returns></returns>
        /// Author: nxhinh (25/09/2023)
        [Test]
        public async Task DeleteAsync_InvalidId_ThrowException()
        {
            // Arrange
            var id = Guid.NewGuid();
            var fixedAsset = new FixedAsset();

            _fixedAssetRepository.GetAsync(id).Throws(new NotFoundException());

            // Act && Assert
            Assert.ThrowsAsync<NotFoundException>(async () => await _fixedAssetService.DeleteAsync(id));

            await _fixedAssetRepository.Received(1).GetAsync(id);
        }

        /// <summary>
        /// Test Delete với đầu vào là id hợp lệ
        /// </summary>
        /// <returns></returns>
        /// Author: nxhinh (25/09/2023)
        [Test]
        public async Task DeleteAsync_ValidId_Success()
        {
            // Arrange
            var id = Guid.NewGuid();
            var fixedAsset = new FixedAsset();

            _fixedAssetRepository.GetAsync(id).Returns(fixedAsset);

            // Act && Assert
            await _fixedAssetService.DeleteAsync(id);

            await _fixedAssetRepository.Received(1).GetAsync(id);
            await _fixedAssetRepository.Received(1).DeleteAsync(fixedAsset);
        }


        /// <summary>
        /// Test DeleteMulti với đầu vào là chuỗi rỗng
        /// </summary>
        /// <returns></returns>
        /// Author: nxhinh (25/09/2023)
        [Test]
        public async Task DeleteMultiAsync_EmptyList_ThrowException()
        {
            // Arrange
            var ids = new List<Guid>();

            var expectedMessage = "Không được truyền danh sách rỗng!";

            // Act && Assert
            var exception = Assert.ThrowsAsync<Exception>(async () => await _fixedAssetService.DeleteMultiAsync(ids));

            Assert.That(exception.Message, Is.EqualTo(expectedMessage));

            await _unitOfWork.Received(1).BeginTransactionAsync();
            await _unitOfWork.Received(1).RollBackAsync();
        }

        /// <summary>
        /// Test DeleteMulti với đầu vào có một hoặc nhiều id không hợp lệ
        /// </summary>
        /// <returns></returns>
        /// Author: nxhinh (25/09/2023)
        [Test]
        public async Task DeleteMultiAsync_List10Items_ThrowException()
        {
            // Arrange
            var ids = new List<Guid>();
            var fixedAssets = new List<FixedAsset>();
            // Tạo 10 id tương ứng 10 tài sản
            for (int i = 0; i < 10; i++)
            {
                var newGuid = Guid.NewGuid();
                ids.Add(newGuid);
                var newFixedAsset = new FixedAsset();
                fixedAssets.Add(newFixedAsset);
            }
            // Tạo thêm 1 id thừa
            ids.Add(Guid.NewGuid());

            var expectedMessage = "Không thể xoá!";

            _fixedAssetRepository.GetListByIdsAsync(ids).Returns(fixedAssets);

            // Act && Assert
            var exception = Assert.ThrowsAsync<Exception>(async () => await _fixedAssetService.DeleteMultiAsync(ids));

            Assert.That(exception.Message, Is.EqualTo(expectedMessage));

            await _fixedAssetRepository.Received(1).GetListByIdsAsync(ids);

            await _unitOfWork.Received(1).BeginTransactionAsync();
            await _unitOfWork.Received(1).RollBackAsync();
        }

        /// <summary>
        /// Test DeleteMulti với đầu vào là chuỗi hợp lệ
        /// </summary>
        /// <returns></returns>
        /// Author: nxhinh (25/09/2023)
        [Test]
        public async Task DeleteMultiAsync_List10Items_Success()
        {
            // Arrange
            var ids = new List<Guid>();
            var fixedAssets = new List<FixedAsset>();
            // Tạo 10 id tương ứng 10 tài sản
            for (int i = 0; i < 10; i++)
            {
                var newGuid = Guid.NewGuid();
                ids.Add(newGuid);
                var newFixedAsset = new FixedAsset();
                fixedAssets.Add(newFixedAsset);
            }

            _fixedAssetRepository.GetListByIdsAsync(ids).Returns(fixedAssets);

            // Act && Assert
            await _fixedAssetService.DeleteMultiAsync(ids);

            await _fixedAssetRepository.Received(1).GetListByIdsAsync(ids);

            await _fixedAssetRepository.Received(1).DeleteMultiAsync(fixedAssets);

            await _unitOfWork.Received(1).BeginTransactionAsync();
            await _unitOfWork.Received(1).CommitAsync();
        }
    }
}
