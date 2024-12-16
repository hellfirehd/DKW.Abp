namespace TestApp.EntityFrameworkCore;

public class TestAppEntityFrameworkCoreFixture : IDisposable
{
    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}
