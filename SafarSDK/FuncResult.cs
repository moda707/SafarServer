using System;
using System.Threading.Tasks;

namespace SafarSDK
{
    public class FuncResult
    {
        public ResultEnum Result { get; set; }
        public string Message { get; set; }

        public FuncResult(ResultEnum result, string message = "")
        {
            Result = result;
            Message = message;
        }

        public static explicit operator Task<object>(FuncResult v)
        {
            throw new NotImplementedException();
        }
    }

    public enum ResultEnum
    {
        Successfull = 1,
        Unsuccessfull = 0
    }

}