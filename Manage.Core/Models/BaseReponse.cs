using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Manage.Core.Models
{
    /// <summary>
    /// Base reponse class containing informations about response being sucessful or not
    /// </summary>
    public class BaseReponse
    {
        [JsonIgnore]
        public bool IsSuccessful { get; set; }

        [JsonIgnore]
        public string ErrorMessage { get; set; }
    }
}
