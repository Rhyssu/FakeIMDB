using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{ 
    public class ErrorResponse
    {
        public string? Response { get; set; }
        public string? Error { get; set; }
    }

}
