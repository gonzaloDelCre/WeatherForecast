namespace WeatherForecast.Domain.Entities.Member
{
    public class Member
    {
        public Shared.MemberID MemberID { get; private set; }
        public MemberFullName FullName { get; private set; }
        public MemberEmail Email { get; private set; }
        public MemberPhone Phone { get; private set; }
        public DateTime JoinDate { get; private set; }

        public Member(Shared.MemberID memberID, MemberFullName fullName, MemberEmail email, MemberPhone phone, DateTime joinDate)
        {
            MemberID = memberID;
            FullName = fullName;
            Email = email;
            Phone = phone;
            JoinDate = joinDate;
        }

        public Member() { }
        public void UpdatePhone(MemberPhone newPhone)
        {
            Phone = newPhone;
        }
    }

}
