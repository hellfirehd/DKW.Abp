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

namespace Dkw.Abp.Ddd.Domain.Values;

/// <summary>
/// A base class for value objects, providing structural equality and immutability.
/// </summary>
/// <remarks>
/// <para>Value objects are immutable and are compared based on their structural equality rather
/// than reference equality. This base class enforces these principles and provides a foundation
/// for implementing value objects in a consistent manner.</para>
/// <para>Derived classes should take care to ensure that their properties are immutable.</para>
/// </remarks>
/// <typeparam name="T">The type of the derived value object. This ensures that equality comparisons are performed between objects of the
/// same type.</typeparam>
public abstract record ValueObject<T>
    where T : ValueObject<T>
{
}
