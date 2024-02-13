using Azure;
using Azure.Data.Tables;

namespace WebApp.Database
{
    public class ExampleClass : ITableEntity
    {
        public int Id
        {
            get { return string.IsNullOrEmpty(RowKey) ? 0 : int.Parse(RowKey); }
            set { RowKey = value.ToString(); }
        }

        public string Name { get; set; }

        public bool IsConfirmed { get; set; }

        public string Email { get; set; }

        public DateTime BirthDate { get; set; }
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
    }
}