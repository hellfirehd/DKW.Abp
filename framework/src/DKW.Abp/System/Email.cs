// Canadian Professional Counsellors Association Application Suite
// Copyright (C) 2024 Doug Wilson
//
// This program is free software: you can redistribute it and/or modify it under the terms of
// the GNU Affero General Public License as published by the Free Software Foundation, either
// version 3 of the License, or (at your option) any later version.
//
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY
// without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
// See the GNU Affero General Public License for more details.
//
// You should have received a copy of the GNU Affero General Public License along with this
// program. If not, see <https://www.gnu.org/licenses/>.

using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System;

[JsonConverter(typeof(EmailAddressJsonConverter))]
[TypeConverter(typeof(EmailAddressTypeConverter))]
public struct Email : IXmlSerializable, IComparable<Email>, IEquatable<Email>
{
    public static readonly Email Empty;
    private String _email = String.Empty;

    public Email(String email)
    {
        if (!String.IsNullOrEmpty(email))
        {
            var parts = email.Split('@', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            if (parts.Length != 2)
            {
                throw new ArgumentException($"The string '{email}' is not a valid email.", nameof(email));
            }

            _email = email;
        }
    }

    public override readonly String ToString() => _email;

    public static Email Parse(String email) => new(email);

    public static Boolean TryParse(String? value, out Email email)
    {
        email = Empty;
        try
        {
            email = new Email(value!);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public static implicit operator String(Email email) => email.ToString();

    public static implicit operator Email(String email) => Parse(email);

    public static Boolean operator <(Email left, Email right) => left.CompareTo(right) < 0;

    public static Boolean operator <=(Email left, Email right) => left.CompareTo(right) <= 0;

    public static Boolean operator >(Email left, Email right) => left.CompareTo(right) > 0;

    public static Boolean operator >=(Email left, Email right) => left.CompareTo(right) >= 0;

    public readonly Int32 CompareTo(Email? other) => String.Compare(ToString(), other?.ToString(), StringComparison.OrdinalIgnoreCase);
    public readonly Int32 CompareTo(Email other) => String.Compare(ToString(), other.ToString(), StringComparison.OrdinalIgnoreCase);

    public readonly Boolean Equals(Email other)
    {
        if (String.IsNullOrWhiteSpace(_email))
        {
            return String.IsNullOrWhiteSpace(other._email);
        }

        var myParts = _email.Split('@', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        var otherParts = other._email.Split('@', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        if (otherParts.Length != 2 || myParts.Length != otherParts.Length)
        {
            return false;
        }

        return String.Equals(myParts[0], otherParts[0], StringComparison.Ordinal)
            && String.Equals(myParts[1], otherParts[1], StringComparison.OrdinalIgnoreCase);
    }

    readonly XmlSchema? IXmlSerializable.GetSchema() => null;

    void IXmlSerializable.ReadXml(XmlReader reader)
    {
        if (reader.ReadToDescendant("Email"))
        {
            _email = reader.ReadElementContentAsString();
        }
    }

    readonly void IXmlSerializable.WriteXml(XmlWriter writer)
        => writer.WriteElementString("Email", _email);

    [SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "See the JsonConverter attribute.")]
    private sealed class EmailAddressJsonConverter : JsonConverter<Email>
    {
        public override Boolean CanConvert(Type objectType)
            => objectType == typeof(Email);

        public override Email Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            => new(reader.GetString() ?? String.Empty);

        public override void Write(Utf8JsonWriter writer, Email email, JsonSerializerOptions options)
            => writer.WriteStringValue(email);
    }

    [SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "See the TypeConverter attribute.")]
    private sealed class EmailAddressTypeConverter : TypeConverter
    {
        public override Boolean CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
            => sourceType == typeof(String) || base.CanConvertFrom(context, sourceType);

        public override Object ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, Object value)
        {
            var email = value as String;

            return String.IsNullOrEmpty(email)
                ? base.ConvertFrom(context, culture, value)!
                : new Email(email);
        }
    }

    public override readonly Boolean Equals(Object? obj) => obj switch
    {
        Email email => Equals(email),
        _ => false
    };

    public override readonly Int32 GetHashCode() => _email.GetHashCode();

    public static Boolean operator ==(Email left, Email right) => left.Equals(right);

    public static Boolean operator !=(Email left, Email right) => !(left == right);
}
