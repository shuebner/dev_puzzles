using System.Globalization;
using System.Text;

namespace PlainText
{
    public class Test
    {
        [Fact]
        public void Not_all_utf8_encodings_are_created_equal()
        {
            // the ctor parameter is not strictly necessary, but
            // makes abundantly clear that we deliberately do not want a BOM
            Encoding encoding = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false);
            Assert.Equal("Unicode (UTF-8)", encoding.EncodingName);

            string str = "foo";
            MemoryStream ms = new();
            using (var writer = new StreamWriter(ms, encoding))
            {
                writer.Write(str);
            }

            var bytes = ms.ToArray();
            Assert.Equal(expected: str.Length, bytes.Length);
        }

        [Fact]
        public void Strings_are_not_independent_from_context()
        {
            // Turkish has two "i"s, both with an upper-case and lower-case version
            // The "i" with dot has an upper-case "İ".
            // The "I" without dot has a lower-case "ı"
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("tr-TR");
            var upper = "Hi there".ToUpper();

            Assert.NotEqual(expected: "HI THERE", actual: upper);
        }

        [Fact]
        public void Not_all_utf8_strings_are_created_equal()
        {
            Encoding encoding = new UTF8Encoding();
            string originalText1 = File.ReadAllText("Text1.txt", encoding);
            string originalText2 = File.ReadAllText("Text2.txt", encoding);

            // normalize UTF strings before comparing them
            string finalText1 = originalText1.Normalize();
            string finalText2 = originalText2.Normalize();

            Assert.Equal(finalText1, finalText2);

            // alternatively, use a string.Equals overload with culture-awareness
            Assert.True(originalText1.Equals(originalText2, StringComparison.InvariantCulture));
        }
    }
}