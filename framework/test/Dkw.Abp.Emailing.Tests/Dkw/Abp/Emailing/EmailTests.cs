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

using System.Text.Json;
using System.Xml;
using System.Xml.Serialization;

namespace Dkw.Abp.Emailing;

public class EmailTests
{
    [Fact]
    public void Email_can_be_created()
    {
        var a = new Email();
        a.ShouldBe(Email.Empty);

        var b = new Email("a@x.yz");
        b.ShouldNotBe(Email.Empty);

        var c = new Email(String.Empty);
        c.ShouldBe(Email.Empty);
    }

    [Fact]
    public void Email_can_be_assigned_from_string()
    {
        Email a = "a@x.yz";
        a.ToString().ShouldBe("a@x.yz");
    }

    [Fact]
    public void Email_can_be_assigned_to_string()
    {
        String b = new Email("a@x.yz");
        b.ShouldBe("a@x.yz");
    }

    [Fact]
    public void Email_can_be_created_from_null_string()
    {
        new Email(null!).ShouldBe(Email.Empty);
    }

    [Fact]
    public void Email_cannot_be_created_from_malformed_email()
    {
        Should.Throw<ArgumentException>(() => new Email("Bob's Burger"));
    }

    [Fact]
    public void Email_is_Equatable()
    {
        new Email("a@x.yz").ShouldBe(new Email("a@x.yz"));
        new Email("a@x.yz").ShouldNotBe(new Email("b@x.yz"));

        // Local is case sensitive, Domain is not case sensitive
        new Email("a@x.yz").ShouldNotBe(new Email("A@X.YZ")); // Local upper, domain upper
        new Email("a@x.yz").ShouldNotBe(new Email("A@x.yz")); // Local upper, domain lower
        new Email("a@x.yz").ShouldBe(new Email("a@X.YZ")); // Local lower, domain upper
    }

    [Fact]
    public void Email_is_Comparable()
    {
        var a = new Email("a@x.yz");
        var b = new Email("a@x.yz");
        var c = new Email("b@x.yz");

        (a == b).ShouldBeTrue();
        (a < b).ShouldBeFalse();
        (a < c).ShouldBeTrue();

        (c == a).ShouldBeFalse();
        (c < a).ShouldBeFalse();
        (c > a).ShouldBeTrue();

        a.CompareTo(b).ShouldBe(0);
        b.CompareTo(c).ShouldBe(-1);
        c.CompareTo(a).ShouldBe(1);
    }

    [Fact]
    public void Email_is_Parsable()
    {
        var a = Email.Parse("a@x.yz");
        a.ToString().ShouldBe("a@x.yz");

        Should.Throw<ArgumentException>(() => Email.Parse("z at x dot yz"));
    }

    [Fact]
    public void Email_can_be_serialized_to_and_from_JSON()
    {
        var before = new Email("a@x.yz");

        var json = JsonSerializer.Serialize(before);

        var after = JsonSerializer.Deserialize<Email>(json);

        after.ShouldBe(before);
    }

    [Fact]
    public void Email_can_be_serialized_to_and_from_XML()
    {
        var email = new Email("a@x.yz");
        var settings = new XmlReaderSettings
        {
            DtdProcessing = DtdProcessing.Prohibit,
            XmlResolver = null
        };

        // Serialization
        var serializer = new XmlSerializer(typeof(Email));
        using (var writer = new StringWriter())
        {
            serializer.Serialize(writer, email);
            var xmlString = writer.ToString();

            // Deserialization using XmlReader for safer processing
            using (var reader = XmlReader.Create(new StringReader(xmlString), settings))
            {
                var deserializedEmail = (Email)serializer.Deserialize(reader)!;

                // Assert
                deserializedEmail.ShouldBe(email);
            }
        }
    }
}
