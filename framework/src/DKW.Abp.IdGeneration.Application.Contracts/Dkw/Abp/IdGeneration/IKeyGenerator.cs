namespace DKW.Abp.IdGeneration;

public interface IKeyGenerator
{
    String Generate(Int32 keyLength);
}
