using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherLib.Model.Documents
{
    [BsonIgnoreExtraElements]
    public class Login
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id
        {
            get;
            set;
        }

        public bool State
        {
            get;
            set;
        }
        public DateTime UpdateTime
        {
            get;
            set;
        }
        public DateTime Time
        {
            get;
            set;
        }
    }
}
