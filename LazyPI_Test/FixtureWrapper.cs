using AutoFixture;
using LazyObjects = LazyPI.LazyObjects;

namespace LazyPI_Test
{
    public class FixtureWrapper
    {
        private Fixture fixture = new Fixture();

        public FixtureWrapper()
        {
            fixture.Register(() =>
            {
                return new LazyObjects.AFServer(new TestConnection(), fixture.Create<string>(), fixture.Create<string>(), fixture.Create<string>(), fixture.Create<string>(), fixture.Create<bool>(), fixture.Create<string>(), fixture.Create<string>());
            });

            fixture.Register(() =>
            {
                return new LazyObjects.AFDatabase(new TestConnection(), fixture.Create<string>(), fixture.Create<string>(), fixture.Create<string>(), fixture.Create<string>(), fixture.Create<string>());
            });

            fixture.Register(() =>
            {
                return new LazyObjects.AFElement(new TestConnection(), fixture.Create<string>(), fixture.Create<string>(), fixture.Create<string>(), fixture.Create<string>(), fixture.Create<string>());
            });

            fixture.Register(() =>
            {
                return new LazyObjects.AFAttribute(new TestConnection(), fixture.Create<string>(), fixture.Create<string>(), fixture.Create<string>(), fixture.Create<string>(), fixture.Create<string>());
            });

            fixture.Register(() => {
                return new LazyObjects.AFEventFrame(new TestConnection(), fixture.Create<string>(), fixture.Create<string>(), fixture.Create<string>(), fixture.Create<string>(), fixture.Create<string>());
            });

            fixture.Register(() => {
                return new LazyObjects.AFDatabase(new TestConnection(), fixture.Create<string>(), fixture.Create<string>(), fixture.Create<string>(), fixture.Create<string>(), fixture.Create<string>());
            });

            fixture.Register(() =>
            {
                return new LazyObjects.AFUnit(new TestConnection(), fixture.Create<string>(), fixture.Create<string>(), fixture.Create<string>(), fixture.Create<string>(), fixture.Create<string>());
            });

            fixture.Register(() =>
            {
                return new LazyObjects.PIPoint(new TestConnection(), fixture.Create<string>(), fixture.Create<string>(), fixture.Create<string>(), fixture.Create<string>(), fixture.Create<string>());
            });
        }

        public T Create<T>()
        {
            return fixture.Create<T>();
        }
    }
}
