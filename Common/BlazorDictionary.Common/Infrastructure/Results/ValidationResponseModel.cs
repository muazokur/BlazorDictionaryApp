using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlazorDictionary.Common.Infrastructure.Results
{
    public class ValidationResponseModel
    {
        public IEnumerable<string> errors { get; set; }

        public ValidationResponseModel()
        {

        }

        public ValidationResponseModel(IEnumerable<string> errors)
        {
            this.errors = errors;
        }

        public ValidationResponseModel(string message) : this(new List<string>() { message})
        {

        }

        [JsonIgnore]
        public string FlattenErrors => this.errors != null ? string.Join(Environment.NewLine, this.errors) : string.Empty;    
    }
}
