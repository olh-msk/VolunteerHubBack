using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Application.Services
{
    public class ByteArrayConverter : JsonConverter<byte[]>
    {
        public override byte[] Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return Encoding.UTF8.GetBytes(reader.GetString());
        }

        public override void Write(Utf8JsonWriter writer, byte[] value, JsonSerializerOptions options)
        {
            writer.WriteStartArray();

            foreach (byte val in value)
            {
                writer.WriteNumberValue(val);
            }

            writer.WriteEndArray();
        }
    }
}
