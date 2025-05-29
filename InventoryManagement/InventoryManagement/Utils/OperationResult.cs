using InventoryManagement.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Utils
{
    public class OperationResult<T>
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public ErrorCode ErrorCode { get; set; }

        public T Data { get; set; }
        public static OperationResult<T> Success (T data) => new() { IsSuccess = true, Data = data };
        public static OperationResult<T> Fail(string errorMessage, ErrorCode errorCode = ErrorCode.UnknownError) => 
                                        new() { IsSuccess = false, ErrorMessage = errorMessage, ErrorCode = errorCode };

    }
    public class OperationResult : OperationResult<object>
    {
        public static OperationResult Success() => new() { IsSuccess = true };
        public static OperationResult Fail(string errorMessage, ErrorCode errorCode = ErrorCode.UnknownError) => new() { IsSuccess = false, ErrorMessage = errorMessage, ErrorCode = errorCode };
    }
}
