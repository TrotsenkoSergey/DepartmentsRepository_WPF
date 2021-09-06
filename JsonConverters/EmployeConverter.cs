using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DepartmentsRepository_WPF
{
    public class EmployeConverter : JsonConverter<Employe>
    {

        enum TypeDiscriminator
        {
            Manager = 1,
            Worker = 2
        }

        public override bool CanConvert(Type typeToConvert) =>
            typeof(Employe).IsAssignableFrom(typeToConvert);

        public override Employe Read(
            ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            reader.Read();
            if (reader.TokenType != JsonTokenType.PropertyName)
            {
                throw new JsonException();
            }

            string propertyName = reader.GetString();
            if (propertyName != "TypeDiscriminator")
            {
                throw new JsonException();
            }

            reader.Read();
            if (reader.TokenType != JsonTokenType.Number)
            {
                throw new JsonException();
            }

            TypeDiscriminator typeDiscriminator = (TypeDiscriminator)reader.GetInt32();
            Employe employe;
            switch (typeDiscriminator)
            {
                case TypeDiscriminator.Manager:
                    employe = new Manager();
                    break;
                case TypeDiscriminator.Worker:
                    employe = new Worker();
                    break;
                default:
                    throw new JsonException();
            };

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    return employe;
                }

                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    propertyName = reader.GetString();
                    reader.Read();
                    switch (propertyName)
                    {
                        case "CreationTime":
                            DateTime creationTime = reader.GetDateTime();
                            employe.CreationTime = creationTime;
                            break;
                        case "FirstName":
                            string firstName = reader.GetString();
                            employe.FirstName = firstName;
                            break;
                        case "LastName":
                            string lastName = reader.GetString();
                            employe.LastName = lastName;
                            break;
                        case "DateOfBirth":
                            DateTime dateOfBirth = reader.GetDateTime();
                            employe.DateOfBirth = dateOfBirth;
                            break;
                        case "Department":
                            //var department = reader.Get
                            //department = typeof(Department);
                            //employe.Department = department;
                            //break;
                        case "Salary":
                            double salary = reader.GetDouble();
                            employe.Salary = salary;
                            break;
                        case "Attribute":
                            EmployeAttribute attribute = (EmployeAttribute)reader.GetInt32();
                            employe.Attribute = attribute;
                            break;
                    }
                }
            }

            throw new JsonException();
        }

        public override void Write(
            Utf8JsonWriter writer, Employe employe, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            if (employe is Manager manager)
            {
                writer.WriteNumber("TypeDiscriminator", (int)TypeDiscriminator.Manager);
                //writer.WriteNumber("CreditLimit", manager.CreditLimit);
            }
            else if (employe is Worker worker)
            {
                writer.WriteNumber("TypeDiscriminator", (int)TypeDiscriminator.Worker);
                //writer.WriteString("OfficeNumber", worker.OfficeNumber);
            }

            //writer.WriteString("Name", employe.Name);

            writer.WriteEndObject();
        }

    }
}
