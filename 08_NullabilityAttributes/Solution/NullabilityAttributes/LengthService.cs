using System.Diagnostics.CodeAnalysis;

namespace NullabilityAttributes
{
    internal class LengthService
    {
        private readonly IFooService _fooService;
        private string? _foo;

        public LengthService(IFooService fooService)
        {
            _fooService = fooService;
        }

        public void Initialize()
        {
            _foo = _fooService.GetFooOrDefault();
        }

        public int GetLength()
        {
            EnsureInitialized();

            return _foo.Length;
        }

        [MemberNotNull(nameof(_foo))]
        private void EnsureInitialized()
        {
            if (_foo is null)
            {
                throw new ArgumentNullException(nameof(_foo));
            }            
        }
    }
}
