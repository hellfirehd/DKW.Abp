namespace TestApp.Data;

public interface ITestAppDbSchemaMigrator
{
    Task MigrateAsync();
}
