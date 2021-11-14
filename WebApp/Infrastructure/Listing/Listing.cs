using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Infrastructure
{
    public class Listing
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Category { get; set; }

        public string Title { get; set; }

        public string SubTitle { get; set; }

        public string Description { get; set; }

        public List<Image> Images { get; set; }

        public ListingType Type { get; set; }

        public string[] Tags { get; set; }

        public string Author { get; set; }

        public string ReplayBundleUrlJson { get; set; }

        public double Duration { get; set; }

        public bool IsDownloadable { get; set; }

        public bool IsStreamable { get; set; }

        public string Version { get; set; }
    }
}
