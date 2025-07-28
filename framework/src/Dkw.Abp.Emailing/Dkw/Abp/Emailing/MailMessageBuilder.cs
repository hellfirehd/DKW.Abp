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
using Dkw.Abp.Ui;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Net.Mime;
using System.Security.Claims;
using System.Text;
using Volo.Abp.DependencyInjection;

namespace Dkw.Abp.Emailing;

public class MailMessageBuilder(IOptions<EmailOptions> optionsAccessor, IMarkdownRenderer markdownRenderer)
    : IDisposable, IAsyncDisposable, ITransientDependency
{
    private readonly IMarkdownRenderer _markdownRenderer = markdownRenderer;
    private readonly EmailOptions _options = optionsAccessor.Value;
    private readonly StringBuilder _markdown = new();
    private MailMessage _message = new();
    private String? _text;
    private String? _html;
    private Boolean _built;
    private Boolean _disposed;

    public Boolean HasAttachments => _message.Attachments.Count > 0;
    public String Subject => _message.Subject ?? String.Empty;

    public MailMessageBuilder SendFrom(String name, Email email)
        => SendFrom(new EmailAddress(name, email));

    public MailMessageBuilder SendFrom(EmailAddress emailAddress)
    {
        EnsureNotDisposedOrBuilt();

        _message.From = emailAddress;

        return this;
    }

    public MailMessageBuilder ReplyTo(String name, Email email)
        => ReplyTo(new EmailAddress(name, email));

    public MailMessageBuilder ReplyTo(EmailAddress emailAddress)
    {
        EnsureNotDisposedOrBuilt();

        _message.ReplyToList.Add(emailAddress);

        return this;
    }

    public MailMessageBuilder To(ClaimsPrincipal principal)
        => To(new EmailAddress(principal.FullName(), principal.Email()));

    public MailMessageBuilder To(String name, Email email)
        => To(new EmailAddress(name, email));

    public MailMessageBuilder To(EmailAddress emailAddress)
    {
        EnsureNotDisposedOrBuilt();

        _message.To.Add(emailAddress);

        return this;
    }

    public MailMessageBuilder CC(String name, Email email)
        => CC(new EmailAddress(name, email));

    public MailMessageBuilder CC(EmailAddress emailAddress)
    {
        EnsureNotDisposedOrBuilt();

        _message.CC.Add(emailAddress);
        return this;
    }

    public MailMessageBuilder BCC(String name, Email email)
        => BCC(new EmailAddress(name, email));

    public MailMessageBuilder BCC(EmailAddress emailAddress)
    {
        EnsureNotDisposedOrBuilt();

        _message.Bcc.Add(emailAddress);
        return this;
    }

    public MailMessageBuilder WithSubject(String subject)
    {
        if (String.IsNullOrWhiteSpace(subject))
        {
            throw new ArgumentException($"'{nameof(subject)}' must not be null or whitespace.", nameof(subject));
        }

        EnsureNotDisposedOrBuilt();

        _message.Subject = subject;

        return this;
    }

    public MailMessageBuilder WithPriority(MailPriority priority)
    {
        EnsureNotDisposedOrBuilt();

        _message.Priority = priority;

        return this;
    }

    /// <summary>
    /// Appends the <paramref name="markdown"/> content to the email message body.
    /// </summary>
    /// <param name="markdown">The markdown content to be added. Cannot be null or whitespace.</param>
    /// <returns>The current instance of <see cref="MailMessageBuilder"/> to allow method chaining.</returns>
    /// <exception cref="ArgumentException">Thrown if <paramref name="markdown"/> is null or whitespace.</exception>
    /// <remarks>
    /// <para>If you use this, the markdown will be rendered to HTML and replace any HTML body that may have
    /// been provided by <see cref="WithHtmlBody"/>.</para>
    /// </remarks>
    public MailMessageBuilder Append(String markdown)
    {
        EnsureNotDisposedOrBuilt();

        if (String.IsNullOrWhiteSpace(markdown))
        {
            throw new ArgumentException($"'{nameof(markdown)}' cannot be null or whitespace.", nameof(markdown));
        }

        _markdown.Append(markdown);

        return this;
    }

    /// <summary>
    /// Appends the <paramref name="markdown"/> content to the email message body followed by the default line terminator.
    /// </summary>
    /// <param name="markdown">The markdown content to be added. Cannot be null or whitespace.</param>
    /// <returns>The current instance of <see cref="MailMessageBuilder"/> to allow method chaining.</returns>
    /// <exception cref="ArgumentException">Thrown if <paramref name="markdown"/> is null or whitespace.</exception>
    /// <remarks>
    /// <para>If you use this, the markdown will be rendered to HTML and replace any HTML body that may have
    /// been set by <see cref="WithHtmlBody"/>.</para>
    /// </remarks>
    public MailMessageBuilder AppendLine(String markdown)
    {
        EnsureNotDisposedOrBuilt();

        if (String.IsNullOrWhiteSpace(markdown))
        {
            throw new ArgumentException($"'{nameof(markdown)}' cannot be null or whitespace.", nameof(markdown));
        }

        _markdown.AppendLine(markdown);

        return this;
    }

    /// <summary>
    /// Appends a blank line to the markdown content of the email message body.
    /// </summary>
    /// <returns>The current instance of <see cref="MailMessageBuilder"/> to allow method chaining.</returns>
    /// <remarks>
    /// <para>If you use this, the markdown will be rendered to HTML and replace any HTML body that may have
    /// been set by <see cref="WithHtmlBody"/>.</para>
    /// </remarks>
    public MailMessageBuilder AppendLine()
    {
        EnsureNotDisposedOrBuilt();

        _markdown.AppendLine();

        return this;
    }

    /// <summary>
    /// Sets the plain text body of the email message.
    /// </summary>
    /// <param name="text">The plain text content to be used as the email body. Must not be null or whitespace.</param>
    /// <returns>The current instance of <see cref="MailMessageBuilder"/> to allow method chaining.</returns>
    /// <exception cref="ArgumentException">Thrown if <paramref name="text"/> is null or consists only of whitespace.</exception>
    public MailMessageBuilder WithTextBody(String text)
    {
        EnsureNotDisposedOrBuilt();

        if (String.IsNullOrWhiteSpace(text))
        {
            throw new ArgumentException($"'{nameof(text)}' must not be null or whitespace.", nameof(text));
        }

        _text = text;

        return this;
    }

    /// <summary>
    /// Sets the HTML content of the email body.
    /// </summary>
    /// <param name="html">The HTML string to be used as the email body. Must not be null or whitespace.</param>
    /// <returns>The current instance of <see cref="MailMessageBuilder"/> to allow method chaining.</returns>
    /// <exception cref="ArgumentException">Thrown if <paramref name="html"/> is null or consists only of whitespace.</exception>
    /// <remarks>
    /// Using <see cref="Append(String)"/> will overwrite body content from <see cref="WithHtmlBody"/>.
    /// </remarks>
    public MailMessageBuilder WithHtmlBody(String html)
    {
        EnsureNotDisposedOrBuilt();

        if (String.IsNullOrWhiteSpace(html))
        {
            throw new ArgumentException($"'{nameof(html)}' must not be null or whitespace.", nameof(html));
        }

        _html = html;

        return this;
    }

    public MailMessageBuilder AddAttachment(String name, Stream file, String mimeType)
    {
        EnsureNotDisposedOrBuilt();

        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentNullException.ThrowIfNull(file);
        ArgumentException.ThrowIfNullOrWhiteSpace(mimeType);

        if (!file.CanRead)
        {
            throw new ArgumentException($"The stream '{nameof(file)}' must be readable.", nameof(file));
        }

        if (file.Length == 0)
        {
            throw new ArgumentException($"The stream '{nameof(file)}' must not be empty.", nameof(file));
        }

        return AddAttachment(new Attachment(file, name, mimeType));
    }

    public MailMessageBuilder AddAttachment(Attachment attachment)
    {
        EnsureNotDisposedOrBuilt();

        _message.Attachments.Add(attachment ?? throw new ArgumentNullException(nameof(attachment)));

        return this;
    }

    /// <summary>
    /// Gets the current body of the email message based on the available content in the following
    /// order of preference: Markdown, HTML, and Plain Text.
    /// </summary>
    /// <returns>A string containing the current body content. May be empty.</returns>
    public String Body()
    {
        return _markdown.Length > 0
                ? _markdownRenderer.Render(_markdown.ToString())
                : !String.IsNullOrWhiteSpace(_html)
                    ? _html
                    : _text ?? String.Empty;
    }

    /// <summary>
    /// Constructs and returns a <see cref="MailMessage"/> object with the specified content.
    /// </summary>
    /// <remarks>
    /// <para>The body of the email message is based on the available content in the following
    /// order of preference: Markdown, HTML, and plain text. If the message body is set
    /// as HTML and a plain text version is available, it adds the plain text as an 
    /// alternate view.</para>
    /// </remarks>
    /// <returns>A <see cref="MailMessage"/> object containing the constructed email message.</returns>
    public MailMessage Build()
    {
        EnsureNotDisposedOrBuilt();

        Validate();

        if (_markdown.Length > 0)
        {
            _message.Body = _markdownRenderer.Render(_markdown.ToString());
            _message.IsBodyHtml = true;
        }
        else if (!String.IsNullOrWhiteSpace(_html))
        {
            _message.Body = _html;
            _message.IsBodyHtml = true;
        }
        else
        {
            _message.Body = _text ?? String.Empty;
        }

        // If the message is HTML and a text body has been set, we add a plain text version.
        if (_message.IsBodyHtml && !String.IsNullOrWhiteSpace(_text))
        {
            _message.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(_text, null, MediaTypeNames.Text.Plain));
        }

        _built = true;
        return _message;
    }

    private void EnsureNotDisposedOrBuilt()
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(nameof(MailMessageBuilder), "Cannot use a disposed MailMessageBuilder.");
        }

        if (_built)
        {
            throw new InvalidOperationException("The MailMessage has already been built. Create a new instance to build another message.");
        }
    }

    /// <summary>
    /// Throws <see cref="InvalidOperationException"/> .
    /// </summary>
    /// <exception cref="InvalidOperationException">if the current state is invalid</exception>
    /// <remarks>
    /// This is called automatically by the <see cref="Build"/> method.
    /// </remarks>
    public void Validate()
    {
        if (IsValid)
        {
            return;
        }

        if (_message.To.Count == 0)
        {
            throw new InvalidOperationException("The To: email address has not been set. At least one must be provided.");
        }

        if (_message.From is null && _message.ReplyToList.Count == 0)
        {
            throw new InvalidOperationException("The From: or ReplyTo: email address has not been set. One must be provided.");
        }

        if (String.IsNullOrWhiteSpace(Body()))
        {
            throw new InvalidOperationException($"No message body.");
        }
    }

    /// <summary>
    /// Returns <see langword="true" /> if the current <see cref="MailMessage"/> is valid.
    /// </summary>
    /// <remarks>
    /// To be considered valid the following must be true: <br />
    /// <list type="bullet">
    /// <item>From or ReplyTo have been specified</item>
    /// <item>There are one or more recipients</item>
    /// <item>The subject has been set to a non-empty string</item>
    /// <item>The body has been provided as Markdown, HTML, or Plain Text</item>
    /// </list>
    /// </remarks>
    public Boolean IsValid
        => _built == false
        && _message.To.Count > 0
        && (_message.From is not null || _message.ReplyToList.Count == 0)
        && !String.IsNullOrWhiteSpace(_message.Subject)
        && !String.IsNullOrWhiteSpace(Body());

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(Boolean disposing)
    {
        if (disposing && !_disposed)
        {
            // Dispose managed resources
            _built = true;
            _text = null;
            _html = null;
            _markdown.Clear();
            _message.Dispose();
            _message = null!;
            _disposed = true;
        }

        // Dispose unmanaged resources, if any.
    }

    [SuppressMessage("Usage", "CA1816:Dispose methods should call SuppressFinalize", Justification = "This calls Dispose() which takes care of the rest.")]
    public virtual ValueTask DisposeAsync()
    {
        try
        {
            Dispose();
            return ValueTask.CompletedTask;
        }
        catch (Exception ex)
        {
            return ValueTask.FromException(ex);
        }
    }
}
