using Azure.Data.Tables;
using Azure;

namespace WebApp.Database
{
    public class SeedExampleDatabase
    {
        public static List<ExampleClass> GetExamples()
        {
            return new List<ExampleClass>()
            {
                new ExampleClass(){ PartitionKey=string.Empty, Id=1, Name="John Doe", IsConfirmed=true, Email="john.doe@example.com", BirthDate = new DateTime(1985,5,15,0,0,0,DateTimeKind.Utc)},
                new ExampleClass(){ PartitionKey=string.Empty, Id=2, Name="Jane Smith", IsConfirmed=false, Email="jane.smith@example.com", BirthDate = new DateTime(1990,7,20,0,0,0,DateTimeKind.Utc)},
                new ExampleClass(){ PartitionKey=string.Empty, Id=3, Name="Alice Johnson", IsConfirmed=true, Email="alice.johnson@example.com", BirthDate = new DateTime(1978,3,10,0,0,0,DateTimeKind.Utc)},
                new ExampleClass(){ PartitionKey=string.Empty, Id=4, Name="Bob Williams", IsConfirmed=false, Email="bob.williams@example.com", BirthDate = new DateTime(1980,12,5,0,0,0,DateTimeKind.Utc)},
                new ExampleClass(){ PartitionKey=string.Empty, Id=5, Name="Charlie Brown", IsConfirmed=true, Email="charlie.brown@example.com", BirthDate = new DateTime(1995,11,11,0,0,0,DateTimeKind.Utc)},
                new ExampleClass(){ PartitionKey=string.Empty, Id=6, Name="Emily Davis", IsConfirmed=false, Email="emily.davis@example.com", BirthDate = new DateTime(1988,10,25,0,0,0,DateTimeKind.Utc)},
                new ExampleClass(){ PartitionKey=string.Empty, Id=7, Name="Frank Miller", IsConfirmed=true, Email="frank.miller@example.com", BirthDate = new DateTime(1975,6,18,0,0,0,DateTimeKind.Utc)},
                new ExampleClass(){ PartitionKey=string.Empty, Id=8, Name="Grace Lee", IsConfirmed=false, Email="grace.lee@example.com", BirthDate = new DateTime(1992,9,30,0,0,0,DateTimeKind.Utc)},
                new ExampleClass(){ PartitionKey=string.Empty, Id=9, Name="Harry Garcia", IsConfirmed=true, Email="harry.garcia@example.com", BirthDate = new DateTime(1997,8,12,0,0,0,DateTimeKind.Utc)},
                new ExampleClass(){ PartitionKey=string.Empty, Id=10, Name="Ivy Wilson", IsConfirmed=false, Email="ivy.wilson@example.com", BirthDate = new DateTime(1993,4,23,0,0,0,DateTimeKind.Utc)},
                new ExampleClass(){ PartitionKey=string.Empty, Id=11, Name="Jack Thomas", IsConfirmed=true, Email="jack.thomas@example.com", BirthDate = new DateTime(1989,2,15,0,0,0,DateTimeKind.Utc)},
                new ExampleClass(){ PartitionKey=string.Empty, Id=12, Name="Katie Rodriguez", IsConfirmed=false, Email="katie.rodriguez@example.com", BirthDate = new DateTime(1995,4,9,0,0,0,DateTimeKind.Utc)},
                new ExampleClass(){ PartitionKey=string.Empty, Id=13, Name="Louis Martinez", IsConfirmed=true, Email="louis.martinez@example.com", BirthDate = new DateTime(1977,6,22,0,0,0,DateTimeKind.Utc)},
                new ExampleClass(){ PartitionKey=string.Empty, Id=14, Name="Molly Perez", IsConfirmed=false, Email="molly.perez@example.com", BirthDate = new DateTime(1982,11,30,0,0,0,DateTimeKind.Utc)},
                new ExampleClass(){ PartitionKey=string.Empty, Id=15, Name="Noah Young", IsConfirmed=true, Email="noah.young@example.com", BirthDate = new DateTime(1990,1,18,0,0,0,DateTimeKind.Utc)},
                new ExampleClass(){ PartitionKey=string.Empty, Id=16, Name="Olivia Hernandez", IsConfirmed=false, Email="olivia.hernandez@example.com", BirthDate = new DateTime(1986,9,5,0,0,0,DateTimeKind.Utc)},
                new ExampleClass(){ PartitionKey=string.Empty, Id=17, Name="Peter Gonzalez", IsConfirmed=true, Email="peter.gonzalez@example.com", BirthDate = new DateTime(1979,3,14,0,0,0,DateTimeKind.Utc)},
                new ExampleClass(){ PartitionKey=string.Empty, Id=18, Name="Queenie Scott", IsConfirmed=false, Email="queenie.scott@example.com", BirthDate = new DateTime(1988,12,20,0,0,0,DateTimeKind.Utc)},
                new ExampleClass(){ PartitionKey=string.Empty, Id=19, Name="Richard King", IsConfirmed=true, Email="richard.king@example.com", BirthDate = new DateTime(1975,8,10,0,0,0,DateTimeKind.Utc)},
                new ExampleClass(){ PartitionKey=string.Empty, Id=20, Name="Samantha Wright", IsConfirmed=false, Email="samantha.wright@example.com", BirthDate = new DateTime(1984,7,28,0,0,0,DateTimeKind.Utc)}
            };
        }

        public static void Seed(AzureDataTablesDatabaseContext context)
        {
            var tableClient = context.ExampleClassesTableClient();
            List<ExampleClass> testClasses = new List<ExampleClass>();
            var pagableResult = tableClient.Query<ExampleClass>(maxPerPage: 1000);
            foreach (Page<ExampleClass> page in pagableResult.AsPages())
                testClasses.AddRange(page.Values);
            if (testClasses.Count == 0)
            {
                var testlist = GetExamples();
                foreach (var item in testlist)
                    tableClient.AddEntity(item);
            }
        }
    }
}