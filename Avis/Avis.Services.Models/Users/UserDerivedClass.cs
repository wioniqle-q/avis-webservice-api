using Avis.Services.Models.UsersProperty;
using MongoDB.Bson.Serialization.Attributes;

namespace Avis.Services.Models;

public sealed class DerivedUserVirtualClass : UserVirtualClass
{
    public override string UserName
    {
        get { return base.UserName; }
        set { base.UserName = value; }
    }

    public override string Password
    {
        get { return base.Password; }
        set { base.Password = value; }
    }

    public override string Salt
    {
        get { return base.Salt; }
        set { base.Salt = value; }
    }
}

public class UserVirtualClass : UserVirtualProperty
{
    [BsonElement("UserName")]
    public virtual string UserName { get; set; }
    
    [BsonElement("Password")]
    public virtual string Password { get; set; } 
    
    /// <summary>
    /// The salt key will soon be kept in a separate db. A 2nd db will be created in future updates.
    /// </summary>
    [BsonElement("Salt")]
    public virtual string Salt { get; set; } 

    public UserVirtualClass()
    {

    }
    
    public UserVirtualClass(string userName, string password, string salt)
    {
        this.UserName = userName;
        this.Password = password;
        this.Salt = salt;
    }
}