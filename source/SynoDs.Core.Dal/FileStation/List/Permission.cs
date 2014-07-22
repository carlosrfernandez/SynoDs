using System.Runtime.Serialization;

namespace SynoDs.Core.Dal.FileStation.List
{
    [DataContract]
    public class Permission
    {
        [DataMember(Name = "share_right")]
        public string ShareRight { get; set; }

        [DataMember(Name = "posix")]
        public int PosixFilePermissions { get; set; }

        [DataMember(Name = "adv_right")]
        public AdvancedRights AdvancedRights { get; set; }

        [DataMember(Name = "acl_enable")]
        public bool AclPrivileges { get; set; }

        [DataMember(Name = "is_acl_enable")]
        public bool IsAclPrivileges { get; set; }

        [DataMember(Name = "acl")]
        public Acl Acl { get; set; }
    }
}
