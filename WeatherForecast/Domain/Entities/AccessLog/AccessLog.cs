
using WeatherForecast.Domain.Entities.Shared;

namespace WeatherForecast.Domain.Entities.AccessLog
{
    public class AccessLog
    {
        public AccessID AccessID { get; private set; }
        public Shared.MemberID? MemberID { get; private set; }
        public AccessDateTime AccessDateTime { get; private set; }
        public AccessType AccessType { get; private set; }
        public AccessStatus AccessStatus { get; private set; }

        public AccessLog(Shared.MemberID? memberID, AccessDateTime accessDateTime, AccessType accessType, AccessStatus accessStatus)
        {
            MemberID = memberID;
            AccessDateTime = accessDateTime;
            AccessType = accessType;
            AccessStatus = accessStatus;
        }
        private AccessLog() { }

        public void UpdateAccessStatus(AccessStatus newStatus)
        {
            AccessStatus = newStatus;
        }

        public void UpdateAccessType(AccessType newType)
        {
            AccessType = newType;
        }

        public void UpdateMemberID(Shared.MemberID? memberID)
        {
            this.MemberID = memberID ?? new MemberID(0);
        }
    }
}
