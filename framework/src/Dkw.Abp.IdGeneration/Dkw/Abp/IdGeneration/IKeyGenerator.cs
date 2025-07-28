namespace Dkw.Abp.IdGeneration;

public interface IKeyGenerator
{
    String Generate(Int32 keyLength);
}
