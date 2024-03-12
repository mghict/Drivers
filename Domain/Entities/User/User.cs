using Common.Extensions;
using DocumentFormat.OpenXml.Wordprocessing;
using Driver.Common.Models;
using Driver.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

[Table("Users")]
[Index(nameof(UserName), Name = "IX_User_UserName")]
[Index(nameof(UserName),nameof(Password), Name = "IX_User_UserName_Password")]
public class User:Moneyon.Common.Data.Entity<long>
{

    [MaxLength(50)]
    public string UserName { get; set; }

    [MaxLength(150)]
    public string Password { get; set; }
    
    public bool IsActive { get; set; }

    public long PersonId { get; set; }

    [ForeignKey("PersonId")]
    public virtual Person Person { get; set; }

    [Column(TypeName ="datetime")]
    public DateTime LastChanged { get; set; }

    public int RoleId { get; set; } = 4;
    public virtual Role Role { get; set; }
    public ICollection<Province>? Provinces { get; set; }
    public ICollection<Mine>? Mines { get; set; }

    public User()
    {
        Provinces = new HashSet<Province>();
        Mines = new HashSet<Mine>();
    }

    public User(string password):this()
    {
        Password = password.Hash();
    }
    public User(string userName, string password, bool? active = false)
        :this(password)
    {
        UserName = userName;
        IsActive = active.Value;
    }


    public List<string> GetPermission()
    {
        List<string> permissions = new List<string>();
        if (Role is null || Role!.Permissions is null )
            return permissions;


        foreach (Permission permission in Role.Permissions!)
        {
            if (permissions.Contains(permission.PermisionId.ToString()))
                continue;

            permissions.Add(permission.PermisionId.ToString());
        }

        return permissions;
    }
    public List<string> GetRoles()
    {
        List<string> roles = new List<string>();
        if (Role is null)
            return roles;

        roles.Add(Role.RoleName);

        return roles;
    }
}
