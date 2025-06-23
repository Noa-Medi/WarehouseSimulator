using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WarehouseSimulator.Utils
{

    public class TupleConverter : JsonConverter<(int, int)>
    {
        public override (int, int) Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
            {
                var array = doc.RootElement.EnumerateArray().ToArray();
                return (array[0].GetInt32(), array[1].GetInt32());
            }
        }

        public override void Write(Utf8JsonWriter writer, (int, int) value, JsonSerializerOptions options)
        {
            writer.WriteStartArray();
            writer.WriteNumberValue(value.Item1);
            writer.WriteNumberValue(value.Item2);
            writer.WriteEndArray();
        }
    }
}
