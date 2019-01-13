using System.Runtime.Serialization;

namespace API.MyContacts.Models
{
    /// <summary>
    /// API operation Status
    /// </summary>
    [DataContract]
    public class StatusModel
    {
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public dynamic Id { get; set; }

        [DataMember(Name = "resultCode", EmitDefaultValue = false)]
        public string ResultCode { get; set; }

        [DataMember(Name = "reason", EmitDefaultValue = false)]
        public string Reason { get; set; }
    }
}