using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{ 
    public record ErrorResponse
    {
        public string Response { get; init; }
        public string Error { get; init; }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"Response     : {Response}");
            stringBuilder.AppendLine($"Error        : {Error}");
            return stringBuilder.ToString();
        }
    }
}
