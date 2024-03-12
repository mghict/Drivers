using System.Text.Json.Serialization;

namespace Driver.Domain.Entities;

public class MapReverseDto 
{
    public long Id { get; set; }
    public string? Address { get; set; }

    [JsonPropertyName("postal_address")]
    public string? PostalAddress { get; set; }

    [JsonPropertyName("address_compact")]
    public string? AddressCompact { get; set; }
    public string? Primary { get; set; }
    public string? Name { get; set; }
    public string? Poi { get; set; }
    public string? Penult { get; set; }
    public string? Country { get; set; }
    public string? Province { get; set; }
    public string? County { get; set; }
    public string? District { get; set; }

    [JsonPropertyName("rural_district")]
    public string? RuralDistrict { get; set; }
    public string? City { get; set; }
    public string? Village { get; set; }
    public string? Region { get; set; }
    public string? Neighbourhood { get; set; }
    public string? Last { get; set; }
    public string? Plaque { get; set; }

    [JsonPropertyName("postal_code")]
    public string? PostalCode { get; set; }
    public Geoms Geom { get; set; }

}
