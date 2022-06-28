﻿using Filuet.Infrastructure.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Filuet.Infrastructure.Abstractions.Helpers
{
    public static class EnumHelpers
    {
        public static string GetDescription(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            if (attributes != null &&
                attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }

        public static string GetCode(this Enum value)
        {
            Type type = value.GetType();
            string name = Enum.GetName(type, value);

            if (name != null)
            {
                FieldInfo field = type.GetField(name);
                if (field != null)
                {
                    CodeAttribute codeAttr = Attribute.GetCustomAttribute(field, typeof(CodeAttribute)) as CodeAttribute;
                    if (codeAttr != null)
                        return codeAttr.DisplayCode;
                }
            }

            return value.ToString("G");
        }

        public static T GetValueFromCode<T>(string code) where T : Enum
        {
            var type = typeof(T);
            if (!type.IsEnum)
                throw new InvalidOperationException();

            foreach (var field in type.GetFields())
            {
                var attribute = Attribute.GetCustomAttribute(field,
                    typeof(CodeAttribute)) as CodeAttribute;
                if (attribute != null)
                {
                    if (string.Equals(attribute.DisplayCode, code, StringComparison.InvariantCultureIgnoreCase))
                        return (T)field.GetValue(null);
                }
                else if (string.Equals(field.Name, code, StringComparison.InvariantCultureIgnoreCase))
                        return (T)field.GetValue(null);
            }

            throw new ArgumentException($"Unable to cast {code} to {typeof(T)}", "code");
        }

        public static T TryGetValueFromCode<T>(string code, T defaultValue) where T : Enum
        {
            try
            {
                return GetValueFromCode<T>(code);
            }
            catch (ArgumentException)
            {
                return defaultValue;
            }
        }

        public static IEnumerable<T> GetValues<T>() => Enum.GetValues(typeof(T)).Cast<T>();
    }
}