// DKW ABP Framework Extensions
// Copyright (C) 2025 Doug Wilson
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

namespace Dkw.Abp.Ddd;

public readonly struct CorrelationId(String correlationId) : IEquatable<CorrelationId>, IComparable<CorrelationId>, IComparable<String>, IConvertible
{
    public static readonly CorrelationId Empty = new(String.Empty);

    private readonly String _id = correlationId ?? throw new ArgumentNullException(nameof(correlationId));

    public static implicit operator String(CorrelationId correlationId) => correlationId._id;

    public static implicit operator CorrelationId(String correlationId) => new(correlationId);
    public static implicit operator CorrelationId(Guid correlationId) => new(correlationId.ToString("d"));

    public override String ToString() => _id;

    public override Boolean Equals(Object? obj) => obj is CorrelationId other && Equals(other);

    public Boolean Equals(CorrelationId other) => String.Equals(_id, other._id, StringComparison.Ordinal);

    public override Int32 GetHashCode() => _id.GetHashCode(StringComparison.Ordinal);

    public Int32 CompareTo(CorrelationId other) => String.Compare(_id, other._id, StringComparison.Ordinal);

    public Int32 CompareTo(String? other) => String.Compare(_id, other, StringComparison.Ordinal);

    public static Boolean operator ==(CorrelationId left, CorrelationId right) => left.Equals(right);

    public static Boolean operator !=(CorrelationId left, CorrelationId right) => !left.Equals(right);

    public static Boolean operator <(CorrelationId left, CorrelationId right) => left.CompareTo(right) < 0;

    public static Boolean operator >(CorrelationId left, CorrelationId right) => left.CompareTo(right) > 0;

    public static Boolean operator <=(CorrelationId left, CorrelationId right) => left.CompareTo(right) <= 0;

    public static Boolean operator >=(CorrelationId left, CorrelationId right) => left.CompareTo(right) >= 0;

    // IConvertible Implementation
    public TypeCode GetTypeCode() => TypeCode.String;

    public String ToString(IFormatProvider? provider) => _id;

    public Guid ToGuid(IFormatProvider? provider) => Guid.Parse(_id);

    public Boolean ToBoolean(IFormatProvider? provider) => !String.IsNullOrEmpty(_id);

    public Byte ToByte(IFormatProvider? provider) => throw new InvalidCastException();

    public Char ToChar(IFormatProvider? provider) => throw new InvalidCastException();

    public DateTime ToDateTime(IFormatProvider? provider) => throw new InvalidCastException();

    public Decimal ToDecimal(IFormatProvider? provider) => throw new InvalidCastException();

    public Double ToDouble(IFormatProvider? provider) => throw new InvalidCastException();

    public Int16 ToInt16(IFormatProvider? provider) => throw new InvalidCastException();

    public Int32 ToInt32(IFormatProvider? provider) => throw new InvalidCastException();

    public Int64 ToInt64(IFormatProvider? provider) => throw new InvalidCastException();

    public SByte ToSByte(IFormatProvider? provider) => throw new InvalidCastException();

    public Single ToSingle(IFormatProvider? provider) => throw new InvalidCastException();

    public UInt16 ToUInt16(IFormatProvider? provider) => throw new InvalidCastException();

    public UInt32 ToUInt32(IFormatProvider? provider) => throw new InvalidCastException();

    public UInt64 ToUInt64(IFormatProvider? provider) => throw new InvalidCastException();

    public Object ToType(Type conversionType, IFormatProvider? provider)
    {
        if (conversionType == typeof(String))
        {
            return _id;
        }

        if (conversionType == typeof(Guid))
        {
            return ToGuid(provider);
        }

        throw new InvalidCastException($"Cannot convert CorrelationId to {conversionType.Name}");
    }
}
