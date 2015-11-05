using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RESTGateway.Core.Entities
{
    public class FeedEntity : TableEntity
    {
        [ Required]
        public Guid Id { get { return string.IsNullOrEmpty(RowKey) ? Guid.Empty : Guid.Parse(RowKey); } set { RowKey = value.ToString(); } }

        [Required]
        public Guid UserId { get { return string.IsNullOrEmpty(PartitionKey) ? Guid.Empty : Guid.Parse(PartitionKey); } set { PartitionKey = value.ToString(); } }

        [Required]
        public string Name { get; set; }

    }
}
