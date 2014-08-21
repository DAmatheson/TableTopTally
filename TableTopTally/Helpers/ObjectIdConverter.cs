/* ObjectIdConverter.cs
 * 
 * Purpose: Json.NET deserializing converter for ObjectIds
 * 
 * Revision History:
 *      Drew Matheson, 2014.08.20: Created
 */ 

using MongoDB.Bson;
using Newtonsoft.Json;
using System;

namespace TableTopTally.Helpers
{
    /// <summary>
    /// Json.NET converter for deserialization of json into ObjectIds
    /// </summary>
    public class ObjectIdConverter : JsonConverter
    {
        public override bool CanWrite
        {
            get
            {
                return false;
            }
        }

        public override bool CanRead
        {
            get
            {
                return true;
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            // The default serialization works fine so it doesn't need to be handled here
            throw new NotImplementedException();
        }

        public override object ReadJson(
            JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            ObjectId convertedValue;

            ObjectId.TryParse(reader.Value.ToString(), out convertedValue);

            return convertedValue;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof (ObjectId);
        }
    }
}