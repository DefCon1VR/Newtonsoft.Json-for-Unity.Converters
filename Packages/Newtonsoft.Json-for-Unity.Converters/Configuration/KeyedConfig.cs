﻿using System;
using System.Collections.Generic;

namespace Newtonsoft.Json.UnityConverters.Configuration
{
#pragma warning disable CA2235 // Mark all non-serializable fields
    [Serializable]
    public struct KeyedConfig : IEquatable<KeyedConfig>
    {
        public string key { get; set; }

        public ConfigType type { get; set; }

        public bool boolean { get; set; }

        public int integer { get; set; }

        public float number { get; set; }

        public string text { get; set; }

        public override bool Equals(object obj)
        {
            return obj is KeyedConfig config && Equals(config);
        }

        public bool Equals(KeyedConfig other)
        {
            return key == other.key &&
                   type == other.type &&
                   boolean == other.boolean &&
                   integer == other.integer &&
                   number == other.number &&
                   text == other.text;
        }

#pragma warning disable S2328 // "GetHashCode" should not reference mutable fields
        public override int GetHashCode()
#pragma warning restore S2328 // "GetHashCode" should not reference mutable fields
        {
            int hashCode = 910641971;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(key);
            hashCode = hashCode * -1521134295 + type.GetHashCode();
            hashCode = hashCode * -1521134295 + boolean.GetHashCode();
            hashCode = hashCode * -1521134295 + integer.GetHashCode();
            hashCode = hashCode * -1521134295 + number.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(text);
            return hashCode;
        }

        public static bool operator ==(KeyedConfig left, KeyedConfig right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(KeyedConfig left, KeyedConfig right)
        {
            return !(left == right);
        }
    }
#pragma warning restore CA2235 // Mark all non-serializable fields
}
