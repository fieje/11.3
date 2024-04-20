using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestStudentSearch()
        {
            List<Student> testStudents = new List<Student>
            {
                new Student { ID = 1, LastName = "Smith", Course = 3, Specialization = "Computer Science" },
                new Student { ID = 2, LastName = "Johnson", Course = 2, Specialization = "Engineering" },
                new Student { ID = 3, LastName = "Brown", Course = 3, Specialization = "Computer Science" },
            };

            bool foundExisting = Program.BinarySearch(testStudents, "Smith", 3, "Computer Science");
            bool foundNonExisting = Program.BinarySearch(testStudents, "Taylor", 2, "Physics");

            Assert.IsTrue(foundExisting, "Existing student not found.");
            Assert.IsFalse(foundNonExisting, "Non-existing student found.");
        }
    }
}
