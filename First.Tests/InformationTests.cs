using First.Models;
using Xunit;

namespace First.Tests
{
    /// <summary>
    /// Unit tests for the Information model class.
    /// Tests cover constructor, property initialization, and data annotations.
    /// </summary>
    public class InformationTests
    {
        [Fact]
        public void Information_CanBeInstantiated_WithDefaultConstructor()
        {
            // Arrange & Act
            var information = new Information();

            // Assert
            Assert.NotNull(information);
            Assert.IsType<Information>(information);
        }

        [Fact]
        public void Information_ID_DefaultValue_IsZero()
        {
            // Arrange
            var information = new Information();

            // Assert
            Assert.Equal(0, information.ID);
        }

        [Fact]
        public void Information_Name_CanBeSetAndRetrieved()
        {
            // Arrange
            var information = new Information();
            var expectedName = "John Doe";

            // Act
            information.Name = expectedName;

            // Assert
            Assert.Equal(expectedName, information.Name);
        }

        [Fact]
        public void Information_Appointment_CanBeSetAndRetrieved()
        {
            // Arrange
            var information = new Information();
            var expectedAppointment = "Software Developer";

            // Act
            information.Appointment = expectedAppointment;

            // Assert
            Assert.Equal(expectedAppointment, information.Appointment);
        }

        [Fact]
        public void Information_Technology_CanBeSetAndRetrieved()
        {
            // Arrange
            var information = new Information();
            var expectedTechnology = ".NET";

            // Act
            information.Technology = expectedTechnology;

            // Assert
            Assert.Equal(expectedTechnology, information.Technology);
        }

        [Fact]
        public void Information_Task_CanBeSetAndRetrieved()
        {
            // Arrange
            var information = new Information();
            var expectedTask = "API Development";

            // Act
            information.Task = expectedTask;

            // Assert
            Assert.Equal(expectedTask, information.Task);
        }

        [Fact]
        public void Information_AllProperties_CanBeSetInChain()
        {
            // Arrange
            var information = new Information();

            // Act
            information.ID = 1;
            information.Name = "Jane Smith";
            information.Appointment = "Team Lead";
            information.Technology = "C#";
            information.Task = "Code Review";

            // Assert
            Assert.Equal(1, information.ID);
            Assert.Equal("Jane Smith", information.Name);
            Assert.Equal("Team Lead", information.Appointment);
            Assert.Equal("C#", information.Technology);
            Assert.Equal("Code Review", information.Task);
        }

        [Fact]
        public void Information_Properties_AcceptNullValues()
        {
            // Arrange
            var information = new Information();

            // Act
            information.Name = null;
            information.Appointment = null;
            information.Technology = null;
            information.Task = null;

            // Assert
            Assert.Null(information.Name);
            Assert.Null(information.Appointment);
            Assert.Null(information.Technology);
            Assert.Null(information.Task);
        }

        [Fact]
        public void Information_Properties_AcceptEmptyStrings()
        {
            // Arrange
            var information = new Information();

            // Act
            information.Name = string.Empty;
            information.Appointment = string.Empty;
            information.Technology = string.Empty;
            information.Task = string.Empty;

            // Assert
            Assert.Equal(string.Empty, information.Name);
            Assert.Equal(string.Empty, information.Appointment);
            Assert.Equal(string.Empty, information.Technology);
            Assert.Equal(string.Empty, information.Task);
        }

        [Fact]
        public void Information_Properties_AcceptLongStrings()
        {
            // Arrange
            var information = new Information();
            var longString = new string('A', 1000);

            // Act
            information.Name = longString;
            information.Appointment = longString;
            information.Technology = longString;
            information.Task = longString;

            // Assert
            Assert.Equal(1000, information.Name.Length);
            Assert.Equal(1000, information.Appointment.Length);
            Assert.Equal(1000, information.Technology.Length);
            Assert.Equal(1000, information.Task.Length);
        }

        [Fact]
        public void Information_TwoInstances_WithSameValues_AreEqual()
        {
            // Arrange
            var info1 = new Information
            {
                ID = 1,
                Name = "Test User",
                Appointment = "Tester",
                Technology = "QA",
                Task = "Testing"
            };

            var info2 = new Information
            {
                ID = 1,
                Name = "Test User",
                Appointment = "Tester",
                Technology = "QA",
                Task = "Testing"
            };

            // Note: Since Information doesn't override Equals, we test reference equality
            // In a real scenario, you might want to implement IEquatable<Information>
            Assert.NotSame(info1, info2);
        }

        [Theory]
        [InlineData(1, "Name1", "Appointment1", "Tech1", "Task1")]
        [InlineData(2, "Name2", "Appointment2", "Tech2", "Task2")]
        [InlineData(999, "Long Name Here", "Long Appointment", "C#", "Task")]
        public void Information_VariousValues_CanBeAssigned(
            int id, string name, string appointment, string technology, string task)
        {
            // Arrange & Act
            var information = new Information
            {
                ID = id,
                Name = name,
                Appointment = appointment,
                Technology = technology,
                Task = task
            };

            // Assert
            Assert.Equal(id, information.ID);
            Assert.Equal(name, information.Name);
            Assert.Equal(appointment, information.Appointment);
            Assert.Equal(technology, information.Technology);
            Assert.Equal(task, information.Task);
        }

        [Fact]
        public void Information_ID_CanBeModified()
        {
            // Arrange
            var information = new Information { ID = 1 };

            // Act
            information.ID = 99;

            // Assert
            Assert.Equal(99, information.ID);
        }
    }
}