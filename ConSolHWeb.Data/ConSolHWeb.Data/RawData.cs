namespace ConSolHWeb.Data
{
    public class RawData
    {
        public string IPAddress { get; set; }
        public string DatabaseName { get; set; }
        public string TableName { get; set; }
        public long TotalRecordCount { get; set; }
        public string DbId { get; set; }
        public string ColumnName { get; set; }
        public bool? NowSyching { get; set; }
        public long TotalUniqueCount { get; set; }
    }
}