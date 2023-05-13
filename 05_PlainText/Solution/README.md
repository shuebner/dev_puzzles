# Solution

## 1. Not all UTF-8 encodings are created equal

Unicode features a so-called [byte order mark (BOM)](https://en.wikipedia.org/wiki/Byte_order_mark).
A BOM is unnecessary for UTF-8, because it is a single-byte encoding.

However, Microsoft tools for a long time used to write a BOM at the beginning of UTF-8 streams and files.
Readers took the presence of the BOM as a hint that the file contains UTF-encoded text.
Pretty much no-one outside of the Microsoft universe does this.
Having a BOM at the beginning of a UTF-8 text can break tools and APIs.

Microsoft has largely abandoned this approach.
However, for backwards compatibility reasons, to this day, [`System.Text.Encoding.UTF8`](https://learn.microsoft.com/en-us/dotnet/api/system.text.encoding.utf8) has the BOM enabled.
This may not manifest when calling `GetBytes()`, but will when writing to a stream.

In contrast, [`System.Text.Encoding.UTF8Encoding`](https://learn.microsoft.com/en-us/dotnet/api/system.text.utf8encoding) by default writes no BOM.

This is inconsistent and confusing.
As a rule-of-thumb, you should always use `System.Text.Encoding.UTF8Encoding` and be explicit about which encoding flavor you want to use.

### The BOM in network APIs

The [UTF-8 standard recommends](https://datatracker.ietf.org/doc/html/rfc3629#section-6) that protocols *forbid* use of the BOM when the protocol itself either provides other mechanisms of identifying the encoding or even mandates the encoding to always be UTF-8.

#### HTTP
HTTP provides the [`Content-Type` header](https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Content-Type) to identify the text encoding.
Thus, the BOM should *NOT* be used with UTF-8.

#### WebSocket
[WebSocket mandates UTF-8 be always used](https://www.rfc-editor.org/rfc/rfc6455.html#section-5.6) for messages of type `Text`.
Thus, the BOM should *NOT* be used.

## 2. Strings are not independent from Context

Turkish has two "i"s, both with an upper-case and lower-case version.
The "i" with dot has an upper-case "İ".
The "I" without dot has a lower-case "ı".

Thus, `ToUpper` and `ToLower` will not stay within the latin alphabet when the thread happens to run under turkish culture settings, even when the original string was latin-only.

Do *NOT* use `ToUpper` and `ToLower` on system strings, such as configuration keys.
In general, always be specific about what your intention is by using the overloads that accept a `CultureInfo`, `StringComparison` etc..

Some SDK analyzers from the [globalization rules](https://learn.microsoft.com/en-us/dotnet/fundamentals/code-analysis/quality-rules/globalization-warnings) like [CA1304](https://learn.microsoft.com/en-us/dotnet/fundamentals/code-analysis/quality-rules/ca1304) help to enforce that.

## 3. Not all UTF-8 strings are created equal

The Unicode form of a character is not unique.
In particular, characters containing diacritics can use both a single code point representing the entire character, or multiple code points for the base character and the diacritics separately, see [here](https://en.wikipedia.org/wiki/Unicode_equivalence#Combining_and_precomposed_characters).

The are two normal forms defined by Unicode.
When comparing Unicode strings you need to make sure that the comparison is based on one of the two normal forms.
The default `string.Equals` does *NOT* do that.
You need to either call [`string.Normalize`](https://learn.microsoft.com/de-de/dotnet/api/system.string.normalize) yourself, or use a `string.Equals` overload that takes a `StringComparison` and use a culture-aware comparison mode.