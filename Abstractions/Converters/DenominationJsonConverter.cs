﻿using Filuet.Infrastructure.Abstractions.Enums;
using Filuet.Infrastructure.Abstractions.Helpers;
using Filuet.Infrastructure.Abstractions.Models;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Filuet.Infrastructure.Abstractions.Converters
{
    public class DenominationJsonConverter : JsonConverter<Denomination>
    {
        public override Denomination Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string value = reader.GetString();
            string[] x = value.Trim().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (x.Length != 2)
                throw new ArgumentException($"Invalid denomination detected {value}");

            if (!uint.TryParse(x[0], out uint amount))
                throw new ArgumentException($"Invalid denomination amount {value}");

            return new Denomination(amount, EnumHelpers.GetValueFromCode<Currency>(x[1]));
        }

        public override void Write(Utf8JsonWriter writer, Denomination note, JsonSerializerOptions options)
            => writer.WriteStringValue(note.ToString());
    }
}