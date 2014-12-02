using Ploeh.AutoFixture;

namespace ALPACA.Test
{
    public static class Builder
    {
        private static Fixture _fixture = new Fixture();

        public static T Build<T>()
        {
            return _fixture.CreateAnonymous<T>();
        }
    }
}
