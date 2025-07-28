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

using Dkw.Abp.Security;
using System.Net.Mail;
using System.Security.Claims;

namespace Dkw.Abp.Emailing;

public static class EmailTemplateBuilderExtensions
{
    public static T To<T>(this T t, ClaimsPrincipal principal)
        where T : IEmailTemplateBuilder
    {
        t.Builder.To(principal.EmailAddress());
        return t;
    }

    public static T To<T>(this T t, String name, Email email)
        where T : IEmailTemplateBuilder
    {
        t.Builder.To(name, email);
        return t;
    }

    public static T To<T>(this T t, EmailAddress emailAddress)
        where T : IEmailTemplateBuilder
    {
        t.Builder.To(emailAddress);
        return t;
    }

    public static T AddAttachment<T>(this T t, Attachment attachment)
        where T : IEmailTemplateBuilder
    {
        t.Builder.AddAttachment(attachment);
        return t;
    }

    public static T AddAttachment<T>(this T t, String name, Stream file, String mimeType)
        where T : IEmailTemplateBuilder
    {
        t.Builder.AddAttachment(name, file, mimeType);
        return t;
    }

    public static T BCC<T>(this T t, EmailAddress emailAddress)
        where T : IEmailTemplateBuilder
    {
        t.Builder.BCC(emailAddress);
        return t;
    }

    public static T BCC<T>(this T t, String name, Email email)
        where T : IEmailTemplateBuilder
    {
        t.Builder.BCC(name, email);
        return t;
    }

    public static T CC<T>(this T t, EmailAddress emailAddress)
        where T : IEmailTemplateBuilder
    {
        t.Builder.CC(emailAddress);
        return t;
    }

    public static T CC<T>(this T t, String name, Email email)
        where T : IEmailTemplateBuilder
    {
        t.Builder.CC(name, email);
        return t;
    }

    public static T SendFrom<T>(this T t, EmailAddress emailAddress)
            where T : IEmailTemplateBuilder
    {
        t.Builder.SendFrom(emailAddress);
        return t;
    }

    public static T SendFrom<T>(this T t, String name, Email email)
                where T : IEmailTemplateBuilder
    {
        t.Builder.SendFrom(name, email);
        return t;
    }

    public static T WithHtmlBody<T>(this T t, String html)
                where T : IEmailTemplateBuilder
    {
        t.Builder.WithHtmlBody(html);
        return t;
    }

    public static T WithSubject<T>(this T t, String subject)
                where T : IEmailTemplateBuilder
    {
        t.Builder.WithSubject(subject);
        return t;
    }

    public static T WithTextBody<T>(this T t, String text)
                where T : IEmailTemplateBuilder
    {
        t.Builder.WithTextBody(text);
        return t;
    }

    public static T Append<T>(this T t, String markdown)
                where T : IEmailTemplateBuilder
    {
        t.Builder.Append(markdown);
        return t;
    }

    public static T AppendLine<T>(this T t, String markdown)
                where T : IEmailTemplateBuilder
    {
        t.Builder.AppendLine(markdown);
        return t;
    }

    public static T AppendLine<T>(this T t)
                where T : IEmailTemplateBuilder
    {
        t.Builder.AppendLine();
        return t;
    }
}
