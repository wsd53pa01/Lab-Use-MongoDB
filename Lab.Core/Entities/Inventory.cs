using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Lab.Core.Entities
{
    public class Inventory : BaseEntity
    {

        [BsonElement("item")]
        public string Item { get; set; }

        [BsonElement("qty")]
        public int Quantity { get; set; }

        [BsonElement("size")]
        public Size Size { get; set; }

        [BsonElement("status")]
        public string Status { get; set; }
    }

    public class Size 
    {
        [BsonElement("h")]
        [BsonRepresentation(BsonType.Int32, AllowTruncation=true)]
        public int Height { get; set; }

        [BsonElement("w")]
        [BsonRepresentation(BsonType.Int32, AllowTruncation=true)]
        public int Width  { get; set; }

        [BsonElement("uom")]
        [BsonRepresentation(BsonType.String, AllowTruncation=true)]
        public string Uom { get; set;}

    }
}