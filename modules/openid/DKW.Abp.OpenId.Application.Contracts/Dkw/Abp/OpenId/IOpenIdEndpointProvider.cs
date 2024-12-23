﻿namespace DKW.Abp.OpenId;

public interface IOpenIdEndpointProvider : IEnumerable<OpenIdEndpoint>
{
    OpenIdEndpoint this[String key] { get; }
}
