namespace SynoDs.Core.Dal.FileStation.CopyMove
{
    using System.Runtime.Serialization;
    using Attributes;

    [DataContract]
    [ApiMethod("start")]
    public class CopyMoveStart
    {
        [DataMember(Name = "taskid")]
        public string TaskId { get; set; }
    }
}
