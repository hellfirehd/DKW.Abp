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

namespace Dkw.Abp.Emailing;

public class EmailComparerTests
{
    [Fact]
    public void When_both_Emails_are_Null_Compare_should_return_Zero()
    {
        // Arrange
        var comparer = new EmailComparer(EmailComparerMode.LocalDomain);

        // Act
        var result = comparer.Compare((Email)null!, (Email)null!);

        // Assert
        result.ShouldBe(0);
    }

    [Fact]
    public void When_both_Emails_are_Null_Nullable_Compare_should_return_Zero()
    {
        // Arrange
        var comparer = new EmailComparer(EmailComparerMode.LocalDomain);
        Email? email1 = null;
        Email? email2 = null;

        // Act
        var result = comparer.Compare(email1, email2);

        // Assert
        result.ShouldBe(0);
    }

    [Fact]
    public void When_first_Email_is_Null_Compare_should_return_NegativeValue()
    {
        // Arrange
        var comparer = new EmailComparer(EmailComparerMode.LocalDomain);
        Email? email = null;
        var bob = new Email("bob@example.org");

        // Act
        var result = comparer.Compare(email, bob);

        // Assert
        result.ShouldBeLessThan(0);
    }

    [Fact]
    public void When_second_Email_isNull_Compare_should_return_PositiveValue()
    {
        // Arrange
        var comparer = new EmailComparer(EmailComparerMode.LocalDomain);
        var alice = new Email("alice@example.org");

        // Act
        var result = comparer.Compare(alice, null);

        // Assert
        result.ShouldBeGreaterThan(0);
    }

    public class When_Mode_is_LocalDomain
    {
        [Fact]
        public void Compare_should_return_correct_comparison()
        {
            var comparer = new EmailComparer(EmailComparerMode.LocalDomain);
            var alice = new Email("alice@BOB.org");
            var bob = new Email("bob@alice.org");

            comparer.Compare(alice, bob).ShouldBeLessThan(0);
            comparer.Compare(bob, alice).ShouldBeGreaterThan(0);
        }

        [Fact]
        public void And_Emails_are_equal_Compare_should_return_Zero()
        {
            // Arrange
            var comparer = new EmailComparer(EmailComparerMode.LocalDomain);
            var alice1 = new Email("alice@BOB.org");
            var alice2 = new Email("alice@bob.org");

            // Act
            var result = comparer.Compare(alice1, alice2);

            // Assert
            result.ShouldBe(0);
        }
    }

    public class When_Mode_is_DomainLocal
    {
        [Fact]
        public void Compare_should_return_correct_comparison()
        {
            var comparer = new EmailComparer(EmailComparerMode.DomainLocal);
            var alice = new Email("alice@bob.org");
            var bob = new Email("bob@ALICE.ORG");

            comparer.Compare(alice, bob).ShouldBeGreaterThan(0);
            comparer.Compare(bob, alice).ShouldBeLessThan(0);
        }

        [Fact]
        public void And_Emails_are_equal_Compare_should_return_Zero()
        {
            // Arrange
            var comparer = new EmailComparer(EmailComparerMode.DomainLocal);
            var alice1 = new Email("alice@BOB.ORG");
            var alice2 = new Email("alice@bob.org");

            // Act
            var result = comparer.Compare(alice1, alice2);

            // Assert
            result.ShouldBe(0);
        }
    }
}
