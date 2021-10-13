using System;
using System.Linq;
using System.Text.Json.Serialization;

namespace Domain.Api
{
    public class ApiValidation
    {
        public string Key { get; set; }
        public string Validation
        {
            get
            {
                return Common.GetEnumDescriptionAttribute<ApiValidationType>(ValidationType).Description;
            }
            set
            {
                ValidationType = Enum.GetValues(typeof(ApiValidationType)).Cast<ApiValidationType>().FirstOrDefault(v => v.GetEnumDescriptionAttribute<ApiValidationType>().Description == value);
            }
        }
        [JsonIgnore]
        public ApiValidationType ValidationType { get; set; }
        public ApiValidation() { }
        public ApiValidation(string key, ApiValidationType validationType)
        {
            this.Key = key;
            this.ValidationType = validationType;
        }
    }
}
