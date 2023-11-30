namespace GenericDeserialization
{
    public class DeserializtionTests
    {
        [Fact]
        public void Test1()
        {
            Pipeline pipeline = new PipelineDeserializer().Deserialize("string", "int", "SomeType");

            Assert.IsType<DefaultPipeline<string, int, SomeComplexType>>(pipeline);
        }

        [Fact]
        public void Test2()
        {
            Pipeline pipeline = new PipelineDeserializer().Deserialize("int", "string", "SomeType");

            Assert.IsType<DefaultPipeline<int, string, SomeComplexType>>(pipeline);
        }
    }
}