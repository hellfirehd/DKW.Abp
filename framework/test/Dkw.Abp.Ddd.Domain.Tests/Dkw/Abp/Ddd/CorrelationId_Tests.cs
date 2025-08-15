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

public class CorrelationId_Tests
{
    [Fact]
    public void ImplicitConversionToString_ShouldReturnUnderlyingString()
    {
        var correlationId = new CorrelationId("test-id");
        String result = correlationId;
        Assert.Equal("test-id", result);
    }

    [Fact]
    public void ImplicitConversionFromString_ShouldCreateCorrelationId()
    {
        CorrelationId correlationId = "test-id";
        Assert.Equal("test-id", correlationId.ToString(CultureInfo.InvariantCulture));
    }

    [Fact]
    public void ImplicitConversionFromGuid_ShouldCreateCorrelationId()
    {
        var guid = Guid.NewGuid();
        CorrelationId correlationId = guid;
        Assert.Equal(guid.ToString(), correlationId.ToString(CultureInfo.InvariantCulture));
    }

    [Fact]
    public void Equals_ShouldReturnTrueForSameValue()
    {
        var id1 = new CorrelationId("test-id");
        var id2 = new CorrelationId("test-id");
        Assert.True(id1.Equals(id2));
        Assert.True(id1 == id2);
    }

    [Fact]
    public void Equals_ShouldReturnFalseForDifferentValues()
    {
        var id1 = new CorrelationId("test-id-1");
        var id2 = new CorrelationId("test-id-2");
        Assert.False(id1.Equals(id2));
        Assert.True(id1 != id2);
    }

    [Fact]
    public void CompareTo_ShouldReturnZeroForSameValue()
    {
        var id1 = new CorrelationId("test-id");
        var id2 = new CorrelationId("test-id");
        Assert.Equal(0, id1.CompareTo(id2));
    }

    [Fact]
    public void CompareTo_ShouldReturnNegativeForSmallerValue()
    {
        var id1 = new CorrelationId("a");
        var id2 = new CorrelationId("b");
        Assert.True(id1 < id2);
    }

    [Fact]
    public void CompareTo_ShouldReturnPositiveForLargerValue()
    {
        var id1 = new CorrelationId("b");
        var id2 = new CorrelationId("a");
        Assert.True(id1 > id2);
    }

    [Fact]
    public void Empty_ShouldBeEmptyString()
    {
        Assert.Equal(String.Empty, CorrelationId.Empty.ToString(CultureInfo.InvariantCulture));
    }

    [Fact]
    public void GetHashCode_ShouldBeConsistentWithEquals()
    {
        var id1 = new CorrelationId("test-id");
        var id2 = new CorrelationId("test-id");
        Assert.Equal(id1.GetHashCode(), id2.GetHashCode());
    }

    [Fact]
    public void ToString_ShouldReturnUnderlyingString()
    {
        var correlationId = new CorrelationId("test-id");
        Assert.Equal("test-id", correlationId.ToString(CultureInfo.InvariantCulture));
    }

    [Fact]
    public void ToBoolean_ShouldReturnTrueForNonEmptyString()
    {
        var correlationId = new CorrelationId("test-id");
        Assert.True(((IConvertible)correlationId).ToBoolean(null));
    }

    [Fact]
    public void ToBoolean_ShouldReturnFalseForEmptyString()
    {
        var correlationId = CorrelationId.Empty;
        Assert.False(((IConvertible)correlationId).ToBoolean(null));
    }

    [Fact]
    public void ToType_ShouldConvertToString()
    {
        var correlationId = new CorrelationId("test-id");
        Assert.Equal("test-id", ((IConvertible)correlationId).ToType(typeof(String), null));
    }

    [Fact]
    public void ToType_ShouldConvertToGuid()
    {
        var guid = Guid.NewGuid();
        var correlationId = new CorrelationId(guid.ToString());
        Assert.Equal(guid, ((IConvertible)correlationId).ToType(typeof(Guid), null));
    }

    [Fact]
    public void ToType_ShouldThrowForUnsupportedType()
    {
        var correlationId = new CorrelationId("test-id");
        Assert.Throws<InvalidCastException>(() => ((IConvertible)correlationId).ToType(typeof(Int32), null));
    }
}
