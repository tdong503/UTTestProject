namespace Common
{
    public class AppConfiguration
    {
        public TestDatabase TestDatabase { get; set; }
    }

    public class TestDatabase
    {
        public string ConnectionString { get; set; }
    }
}