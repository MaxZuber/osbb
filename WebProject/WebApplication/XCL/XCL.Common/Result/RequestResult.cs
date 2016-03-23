using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCL.Common.Result
{
    public class RequestResult<T>
    {
        public T Obj { get; set; }
        public string Message { get; set; }
        public RequestStatus Status { get; set; }
    }
}
