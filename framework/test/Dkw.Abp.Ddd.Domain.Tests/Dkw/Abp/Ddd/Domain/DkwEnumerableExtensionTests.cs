using Volo.Abp.Domain.Entities;

namespace System;

public class DkwEnumerableExtensionTests
{
    private sealed class TestEntity : IEntity<Int32>
    {
        public Int32 Id { get; init; }
        public String Name { get; init; } = String.Empty;
        public Byte[] ConcurrencyToken { get; init; } = [];

        public Object?[] GetKeys() => [Id];
    }

    [Fact]
    public void Replace_should_swap_supplied_IEntity_with_matching_IEntity_in_TSource_if_it_exists()
    {
        var enumerable = new[] {
            new TestEntity { Id = 0, Name ="Zero" },
            new TestEntity { Id = 1, Name = "One" },
            new TestEntity { Id = 2, Name = "Two" },
            new TestEntity { Id = 3, Name = "Three" },
            new TestEntity { Id = 4, Name = "Four" }
        };
        var replacement = new TestEntity { Id = 2, Name = "Updated" };

        var updated = enumerable.Replace<TestEntity, Int32>(replacement).ToArray();

        // Original should be unchanged:
        enumerable.Length.ShouldBe(5);
        enumerable[2].Name.ShouldBe("Two");
        enumerable.Single(e => e.Name == "Two").ShouldNotBeNull();

        // Updated should be updated. Duh!
        updated.Length.ShouldBe(5);
        updated[2].Name.ShouldBe("Updated");
        updated.Any(e => e.Name == "Two").ShouldBeFalse();
    }

    [Fact]
    public void Replace_must_accept_null_value()
    {
        // Arrange
        var enumerable = new[] {
            new TestEntity { Id = 0, Name ="Zero" },
            new TestEntity { Id = 1, Name = "One" },
            new TestEntity { Id = 2, Name = "Two" },
            new TestEntity { Id = 3, Name = "Three" },
            new TestEntity { Id = 4, Name = "Four" }
        };
        var replacement = new TestEntity { Id = 2, Name = "Updated" };

        // Act
        var updated = enumerable.Replace<TestEntity, Int32>(null!).ToArray();

        // Assert: Original should be unchanged
        enumerable.Length.ShouldBe(5);
        enumerable[2].Name.ShouldBe("Two");
        enumerable.Single(e => e.Name == "Two").ShouldNotBeNull();
    }
}
