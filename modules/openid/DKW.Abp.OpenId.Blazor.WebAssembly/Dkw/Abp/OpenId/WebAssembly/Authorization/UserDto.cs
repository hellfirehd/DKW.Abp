namespace DKW.Abp.OpenId.WebAssembly.Authorization;

public class UserDto
{
    public static readonly UserDto Anonymous = new();

    public Boolean IsAuthenticated { get; set; }

    public String NameClaimType { get; set; } = "name";

    public String RoleClaimType { get; set; } = "role";

    public ICollection<ClaimDto> Claims { get; set; } = [];
}
