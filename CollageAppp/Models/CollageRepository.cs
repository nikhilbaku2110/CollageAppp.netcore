namespace CollageAppp.Models
{
    public static class CollageRepository
    {
        public static List<Student> Students { get; set; } = new List<Student>()
        {
            new Student
            {
                Id = 1, 
                StudentName = "nikhil",   
                Email = "nikhil@gmail.com",
                Address = "hyd,india"
            },
            new Student
            {
                Id = 2,
                StudentName = "raj",
                Email = "raj@gmail.cpm",
                Address = "mumbai"
            }
        };
    }
}
