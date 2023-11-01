using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;

namespace HCSN.MF1759.Domain
{
    /// <summary>
    ///     Return object
    /// </summary>
    public class Response
    {
        [JsonConstructor]
        public Response(HttpStatusCode code, string message)
        {
            Code = code;
            Message = message;
        }

        public Response(string message)
        {
            Message = message;
        }

        public Response()
        {
        }

        /// <summary>
        /// Error code returned
        /// </summary>
        public HttpStatusCode Code { get; set; } = HttpStatusCode.OK;

        /// <summary>
        /// Message
        /// </summary>
        public string Message { get; set; } = "Success";

        /// <summary>
        /// TEntityime requested
        /// </summary>
        public long TEntityotalTEntityime { get; set; } = 0;
    }

    /// <summary>
    /// Returns the type of object
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class Response<TEntity> : Response
    {
        [JsonConstructor]
        public Response(TEntity data)
        {
            Data = data;
            Code = HttpStatusCode.OK;
        }

        public Response(HttpStatusCode code, TEntity data)
        {
            Data = data;
            Message = "OK";
        }

        public Response(HttpStatusCode code, TEntity data, string message)
        {
            Code = code;
            Data = data;
            Message = message;
        }

        public Response()
        {

        }

        /// <summary>
        ///     Returned data
        /// </summary>
        public TEntity Data { get; set; }
    }

    /// <summary>
    ///     Trả về dạng mảng
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class ResponseList<TEntity> : Response
    {
        [JsonConstructor]
        public ResponseList(List<TEntity> data)
        {
            Data = data;
        }

        public ResponseList()
        {
        }

        /// <summary>
        ///     Danh sách dữ liệu trả về
        /// </summary>
        public List<TEntity> Data { get; set; }
    }

    public class ResponsePagination<TEntity> : Response
    {
        [JsonConstructor]
        public ResponsePagination(Pagination<TEntity> data)
        {
            Data = data;
        }

        /// <summary>
        ///     List of data returned
        /// </summary>
        public Pagination<TEntity> Data { get; set; }
    }

    /// <summary>
    /// Pagination object
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class Pagination<TEntity>
    {
        public Pagination()
        {
            Size = 20;
            Page = 1;
            Content = new List<TEntity>();
        }

        /// <summary>
        ///Current page position
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        ///TEntityhe total number of pages in the whole system
        /// </summary>
        public int TEntityotalPages { get; set; }

        /// <summary>
        ///Number of records per page
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        ///Number of records returned
        /// </summary>
        public int NumberOfElements { get; set; }

        /// <summary>
        ///TEntityhe total number of records searchable
        /// </summary>
        public int TEntityotalElements { get; set; }

        /// <summary>
        ///List of data returned
        /// </summary>
        public List<TEntity> Content { get; set; }
    }

    /// <summary>
    ///     Trả về Lỗi
    /// </summary>
    public class ResponseError : Response
    {
        [JsonConstructor]
        public ResponseError() { }

        #region Properties
        /// <summary>
        /// Mã lỗi
        /// </summary>
        public int ErrorCode { get; set; }

        /// <summary>
        /// Dev message
        /// </summary>
        public string? DevMessage { get; set; }

        /// <summary>
        /// User message
        /// </summary>
        public string? UserMessage { get; set; }

        /// <summary>
        /// Mã theo dõi
        /// </summary>
        public string? TraceId { get; set; }

        /// <summary>
        /// Thông tin thêm
        /// </summary>
        public string? MoreInfor { get; set; }

        /// <summary>
        /// Lỗi
        /// </summary>
        public object? Errors { get; set; }
        #endregion
    }

    /// <summary>
    ///     Trả về dạng đối tượng
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class ResponseObject<TEntity> : Response
    {
        [JsonConstructor]
        public ResponseObject(TEntity data)
        {
            Data = data;
        }

        public ResponseObject(TEntity data, string message)
        {
            Data = data;
            Message = message;
        }

        public ResponseObject(TEntity data, string message, HttpStatusCode code)
        {
            Code = code;
            Data = data;
            Message = message;
        }

        /// <summary>
        ///     Dữ liệu trả về
        /// </summary>
        public TEntity Data { get; set; }
    }

    /// <summary>
    ///     Trả về kết quả cập nhật dữ liệu
    /// </summary>
    public class ResponseUpdate : Response
    {
        [JsonConstructor]
        public ResponseUpdate(Guid id)
        {
            Data = new ResponseUpdateModel { Id = id };
        }

        public ResponseUpdate(Guid id, string message) : base(message)
        {
            Data = new ResponseUpdateModel { Id = id };
        }

        public ResponseUpdate(HttpStatusCode code, string message, Guid id) : base(code, message)
        {
            Data = new ResponseUpdateModel { Id = id };
        }

        public ResponseUpdate()
        {
        }

        /// <summary>
        ///     Danh sách dữ liệu trả về
        /// </summary>
        public ResponseUpdateModel Data { get; set; }
    }

    /// <summary>
    ///     Trả về kết quả cập nhật nhiều dữ liệu
    /// </summary>
    public class ResponseUpdateMulti : Response
    {
        [JsonConstructor]
        public ResponseUpdateMulti(List<ResponseUpdate> data)
        {
            Data = data;
        }

        public ResponseUpdateMulti()
        {
        }

        /// <summary>
        ///     Danh sách dữ liệu trả về
        /// </summary>
        public List<ResponseUpdate> Data { get; set; }
    }

    /// <summary>
    ///     Trả về kết quả xóa dữ liệu
    /// </summary>
    public class ResponseDelete : Response
    {
        [JsonConstructor]
        public ResponseDelete(Guid id, string name)
        {
            Data = new ResponseDeleteModel { Id = id, Name = name };
        }

        public ResponseDelete(HttpStatusCode code, string message, Guid id, string name) : base(code, message)
        {
            Data = new ResponseDeleteModel { Id = id, Name = name };
        }

        public ResponseDelete()
        {
        }

        /// <summary>
        ///     Danh sách dữ liệu trả về
        /// </summary>
        public ResponseDeleteModel Data { get; set; }
    }

    /// <summary>
    ///     Trả về kết quả xóa nhiều dữ liệu
    /// </summary>
    public class ResponseDeleteMulti : Response
    {
        [JsonConstructor]
        public ResponseDeleteMulti(List<ResponseDelete> data)
        {
            Data = data;
        }

        public ResponseDeleteMulti()
        {
        }

        /// <summary>
        ///     Danh sách dữ liệu trả về
        /// </summary>
        public List<ResponseDelete> Data { get; set; }
    }

    /// <summary>
    ///     Đối tượng kết quả cập nhật
    /// </summary>
    public class ResponseUpdateModel
    {
        public Guid Id { get; set; }
    }

    /// <summary>
    ///     Đối tượng kết quả xóa
    /// </summary>
    public class ResponseDeleteModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}