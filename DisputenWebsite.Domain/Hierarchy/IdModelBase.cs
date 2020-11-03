using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

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
