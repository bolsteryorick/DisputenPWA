using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace DisputenPWA.Domain.Hierarchy
{
    public class IdModelBase : IIdModelBase
    {
        [Key]
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }
        public IdModelBase()
        {
            Id = Guid.NewGuid();
        }
    }
}
