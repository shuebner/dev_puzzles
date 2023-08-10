namespace GenericMapping
{
    public class MapperTest
    {
        readonly Mapper _mapper;

        public MapperTest()
        {
            _mapper = new Mapper();
        }

        [Fact]
        public void Map_should_map_from_generic_bool_to_generic_bool()
        {
            Source source = new Source<bool>(true);

            Target target = _mapper.Map(source);

            var typedTarget = Assert.IsType<Target<bool>>(target);
            Assert.True(typedTarget.Value);
        }

        [Fact]
        public void Map_should_map_from_generic_int_to_generic_int()
        {
            Source source = new Source<int>(42);

            Target target = _mapper.Map(source);

            var typedTarget = Assert.IsType<Target<int>>(target);
            Assert.Equal(42, typedTarget.Value);
        }

        [Fact]
        public void Map_should_map_from_generic_string_to_generic_string()
        {
            Source source = new Source<string>("foo");

            Target target = _mapper.Map(source);

            var typedTarget = Assert.IsType<Target<string>>(target);
            Assert.Equal("foo", typedTarget.Value);
        }

        [Fact]
        public void Map_should_map_from_generic_DateTime_to_generic_DateTime()
        {
            DateTime someTime = new DateTime(2023, 1, 2, 3, 4, 5);
            Source source = new Source<DateTime>(someTime);

            Target target = _mapper.Map(source);

            var typedTarget = Assert.IsType<Target<DateTime>>(target);
            Assert.Equal(target, typedTarget);
        }
    }
}