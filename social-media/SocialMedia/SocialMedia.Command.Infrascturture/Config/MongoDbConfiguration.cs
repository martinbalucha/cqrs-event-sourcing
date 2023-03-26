namespace SocialMedia.Command.Infrascturture.Config;

public record MongoDbConfiguration
{
    public required string ConnectionString { get; init; }
    public required string Dataabase { get; init; }
    public required string Collection { get; init; }
}
