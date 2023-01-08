using MongoDB.Bson.Serialization.Attributes;

namespace Avis.Services.Models.UsersProperty;

public sealed class UserDerivedProperty : UserVirtualProperty
{
    public override Guid Id
    {
        get { return base.Id; }
        set { base.Id = value; }
    }

    public override DateTime CreationTime
    {
        get { return base.CreationTime; }
        set { base.CreationTime = value; }
    }

    public override string HWID
    {
        get { return base.HWID; }
        set { base.HWID = value; }
    }

    public override bool IsActive
    {
        get { return base.IsActive; }
        set { base.IsActive = value; }
    }

    public override bool IsBlocked
    {
        get { return base.IsBlocked; }
        set { base.IsBlocked = value; }
    }

    public override List<string> Roles
    {
        get { return base.Roles; }
        set { base.Roles = value; }
    }
}

public class UserVirtualProperty
{
    [BsonId]
    public virtual Guid Id { get; set; } = Guid.NewGuid();
    public virtual DateTime CreationTime { get; set; } = DateTime.UtcNow;
    public virtual string HWID { get; set; } = string.Empty;
    public virtual bool IsActive { get; set; } = true;
    public virtual bool IsBlocked { get; set; } = false;
    public virtual List<string> Roles { get; set; } = new();

    public UserVirtualProperty()
    {

    }

    public UserVirtualProperty(Guid id, DateTime creationTime, string hwid, bool isActive, bool isBlocked, List<string> roles)
    {
        this.Id = id;
        this.CreationTime = creationTime;
        this.HWID = hwid;
        this.IsActive = isActive;
        this.IsBlocked = isBlocked;
        this.Roles = roles;
    }
}
