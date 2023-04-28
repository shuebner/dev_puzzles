using System.Text;

namespace PlainText
{
    public class Test
    {
        [Fact]
        public void Not_all_utf8_encodings_are_created_equal()
        {
            // change the line below to make the test pass
            Encoding encoding = Encoding.UTF8;
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
            // insert a single line below this comment to make the test pass
            var upper = "Hi there".ToUpper();

            Assert.NotEqual(expected: "HI THERE", actual: upper);
        }

        [Fact]
        public void Not_all_utf8_strings_are_created_equal()
        {
            // look at the originalText strings in the debugger or
            // look at the files themselves with any text editor
            Encoding encoding = new UTF8Encoding();
            string originalText1 = File.ReadAllText("Text1.txt", encoding);
            string originalText2 = File.ReadAllText("Text2.txt", encoding);

            // change how the finalText*** variables are obtained to make the test pass
            string finalText1 = originalText1;
            string finalText2 = originalText2;

            Assert.Equal(finalText1, finalText2);
        }
    }
}