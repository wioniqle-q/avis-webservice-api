using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Avis.Services.OrganizationModel;

public class OrganizationUserProperties
{
    [BsonId]
    public ObjectId Id { get; set; }
    public ObjectId OrganizationId { get; set; } = ObjectId.GenerateNewId();

    public string CreatedAt { get; set; } = DateTime.Now.ToString();
    public string HardwareId { get; set; } = Guid.NewGuid().ToString();
    public string Role { get; } = "None[Unauthorized]";
    
    public bool IsActive { get; set; } = true;
    public bool IsDeleted { get; set; } = false;
}
