namespace ConSolHWeb.Data
{
    public class EmployeeAttendance
    {
        public string ACNo { get; set; }
        public string No { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public string Onduty { get; set; }
        public string Offduty { get; set; }
        public string ClockIn { get; set; }
        public string ClockOut { get; set; }
        public string WorkTime { get; set; }
        public string BeforeOT { get; set; }
        public string AfterOT { get; set; }
        public string NDays_OT { get; set; }
        public string WeekEnd_OT { get; set; }
        public string Holiday_OT { get; set; }
        public string TotalOT { get; set; }
        public string Memo { get; set; }
    }

    internal class EmpConstants
    {
        private const string DOMAIN_NAME = "xyz.com";
    }
}