using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DepartmentsRepository_WPF
{
    public class BaseClassConverter : JsonConverter<Employe>
    {
        private enum TypeDiscriminator
        {
            Employe = 0,
            Manager = 1,
            Worker = 2
        }

        public override bool CanConvert(Type type)
        {
            return typeof(Employe) == type;
        }

        public override Employe Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            if (!reader.Read()
                    || reader.TokenType != JsonTokenType.PropertyName
                    || reader.GetString() != "TypeDiscriminator")
            {
                throw new JsonException();
            }

            if (!reader.Read() || reader.TokenType != JsonTokenType.Number)
            {
                throw new JsonException();
            }

            Employe baseClass;
            TypeDiscriminator typeDiscriminator = (TypeDiscriminator)reader.GetInt32();
            switch (typeDiscriminator)
            {
                case TypeDiscriminator.Manager:
                    if (!reader.Read() || reader.GetString() != "TypeValue")
                    {
                        throw new JsonException();
                    }
                    if (!reader.Read() || reader.TokenType != JsonTokenType.StartObject)
                    {
                        throw new JsonException();
                    }
                    baseClass = (Manager)JsonSerializer.Deserialize(ref reader, typeof(Manager), options);
                    break;
                case TypeDiscriminator.Worker:
                    if (!reader.Read() || reader.GetString() != "TypeValue")
                    {
                        throw new JsonException();
                    }
                    if (!reader.Read() || reader.TokenType != JsonTokenType.StartObject)
                    {
                        throw new JsonException();
                    }
                    baseClass = (Worker)JsonSerializer.Deserialize(ref reader, typeof(Worker), options);
                    break;
                case TypeDiscriminator.Employe:
                    if (!reader.Read() || reader.GetString() != "TypeValue")
                    {
                        throw new JsonException();
                    }
                    if (!reader.Read() || reader.TokenType != JsonTokenType.StartObject)
                    {
                        throw new JsonException();
                    }
                    baseClass = (Employe)JsonSerializer.Deserialize(ref reader, typeof(Employe));
                    break;
                default:
                    throw new NotSupportedException();
            }

            if (!reader.Read() || reader.TokenType != JsonTokenType.EndObject)
            {
                throw new JsonException();
            }

            return baseClass;
        }

        public override void Write(
            Utf8JsonWriter writer,
            Employe value,
            JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            if (value is Manager manager)
            {
                writer.WriteNumber("TypeDiscriminator", (int)TypeDiscriminator.Manager);
                writer.WritePropertyName("TypeValue");
                JsonSerializer.Serialize(writer, manager, options);
            }
            else if (value is Worker worker)
            {
                writer.WriteNumber("TypeDiscriminator", (int)TypeDiscriminator.Worker);
                writer.WritePropertyName("TypeValue");
                JsonSerializer.Serialize(writer, worker, options);
            }
            else if (value is Employe baseClass)
            {
                writer.WriteNumber("TypeDiscriminator", (int)TypeDiscriminator.Employe);
                writer.WritePropertyName("TypeValue");
                JsonSerializer.Serialize(writer, baseClass);
            }
            else
            {
                throw new NotSupportedException();
            }

            writer.WriteEndObject();
        }
    }
}
