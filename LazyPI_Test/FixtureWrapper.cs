using AutoFixture;
using Dummies = LazyPI_Test.WebAPI.Dummies;
using LazyObjects = LazyPI.LazyObjects;

namespace LazyPI_Test
{
    public class FixtureWrapper
    {
        private Fixture fixture = new Fixture();
        private Dummies.ElementController elementController = new Dummies.ElementController();
        private Dummies.AFServerController serverController = new Dummies.AFServerController();
        private Dummies.AFDatabaseController dbController = new Dummies.AFDatabaseController();

        public FixtureWrapper()
        {
            fixture.Register(() =>
            {
                var server = new LazyObjects.AFServer(new TestConnection(), fixture.Create<string>(), fixture.Create<string>(), fixture.Create<string>(), fixture.Create<string>(), fixture.Create<bool>(), fixture.Create<string>(), fixture.Create<string>());
                server.ServerController = serverController;
                return server;
            });

            fixture.Register(() =>
            {
                var database = new LazyObjects.AFDatabase(new TestConnection(), fixture.Create<string>(), fixture.Create<string>(), fixture.Create<string>(), fixture.Create<string>(), fixture.Create<string>());
                database.DBController = dbController;
                return database;
            });

            fixture.Register(() =>
            {
                var ele = new LazyObjects.AFElement(new TestConnection(), fixture.Create<string>(), fixture.Create<string>(), fixture.Create<string>(), fixture.Create<string>(), fixture.Create<string>());
                ele.ElementController = elementController;
                return ele;
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
