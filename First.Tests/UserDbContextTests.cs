using First.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace First.Tests
{
    /// <summary>
    /// Unit tests for the UserDbContext class.
    /// Tests cover constructor, DbSet configuration, and database operations.
    /// </summary>
    public class UserDbContextTests
    {
        [Fact]
        public void UserDbContext_CanBeInstantiated_WithOptions()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<UserDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            // Act
            var context = new UserDbContext(options);

            // Assert
            Assert.NotNull(context);
            Assert.IsType<UserDbContext>(context);
        }

        [Fact]
        public void UserDbContext_HasInfoDbSet()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<UserDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            // Act
            using var context = new UserDbContext(options);

            // Assert
            Assert.NotNull(context.info);
            Assert.IsAssignableFrom<DbSet<Information>>(context.info);
        }

        [Fact]
        public void UserDbContext_CanAddInformation()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<UserDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new UserDbContext(options);
            var information = new Information
            {
                ID = 1,
                Name = "Test User",
                Appointment = "Developer",
                Technology = "C#",
                Task = "Testing"
            };

            // Act
            context.info.Add(information);
            context.SaveChanges();

            // Assert
            var savedInfo = context.info.FirstOrDefault(i => i.ID == 1);
            Assert.NotNull(savedInfo);
            Assert.Equal("Test User", savedInfo.Name);
        }

        [Fact]
        public void UserDbContext_CanRetrieveInformation()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<UserDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new UserDbContext(options);
            var information = new Information
            {
                ID = 2,
                Name = "Retrieve User",
                Appointment = "QA Engineer",
                Technology = "Python",
                Task = "Automation"
            };

            context.info.Add(information);
            context.SaveChanges();

            // Act
            var result = context.info.FirstOrDefault(i => i.ID == 2);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Retrieve User", result.Name);
            Assert.Equal("QA Engineer", result.Appointment);
        }

        [Fact]
        public void UserDbContext_CanUpdateInformation()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<UserDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new UserDbContext(options);
            var information = new Information
            {
                ID = 3,
                Name = "Original Name",
                Appointment = "Original Role",
                Technology = "Original Tech",
                Task = "Original Task"
            };

            context.info.Add(information);
            context.SaveChanges();

            // Act
            var toUpdate = context.info.FirstOrDefault(i => i.ID == 3);
            Assert.NotNull(toUpdate);
            toUpdate.Name = "Updated Name";
            toUpdate.Appointment = "Updated Role";
            context.SaveChanges();

            // Assert
            var updated = context.info.FirstOrDefault(i => i.ID == 3);
            Assert.NotNull(updated);
            Assert.Equal("Updated Name", updated.Name);
            Assert.Equal("Updated Role", updated.Appointment);
        }

        [Fact]
        public void UserDbContext_CanDeleteInformation()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<UserDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new UserDbContext(options);
            var information = new Information
            {
                ID = 4,
                Name = "Delete Me",
                Appointment = "Temporary",
                Technology = "Temp",
                Task = "To Be Deleted"
            };

            context.info.Add(information);
            context.SaveChanges();

            // Act
            var toDelete = context.info.FirstOrDefault(i => i.ID == 4);
            Assert.NotNull(toDelete);
            context.info.Remove(toDelete);
            context.SaveChanges();

            // Assert
            var deleted = context.info.FirstOrDefault(i => i.ID == 4);
            Assert.Null(deleted);
        }

        [Fact]
        public void UserDbContext_CanRetrieveAllInformation()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<UserDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new UserDbContext(options);

            context.info.Add(new Information { ID = 1, Name = "User1" });
            context.info.Add(new Information { ID = 2, Name = "User2" });
            context.info.Add(new Information { ID = 3, Name = "User3" });
            context.SaveChanges();

            // Act
            var allInfo = context.info.ToList();

            // Assert
            Assert.Equal(3, allInfo.Count);
        }

        [Fact]
        public void UserDbContext_CanQueryWithWhereClause()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<UserDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new UserDbContext(options);

            context.info.Add(new Information { ID = 1, Name = "Alpha", Technology = "C#" });
            context.info.Add(new Information { ID = 2, Name = "Beta", Technology = "Java" });
            context.info.Add(new Information { ID = 3, Name = "Gamma", Technology = "C#" });
            context.SaveChanges();

            // Act
            var csharpUsers = context.info.Where(i => i.Technology == "C#").ToList();

            // Assert
            Assert.Equal(2, csharpUsers.Count);
            Assert.All(csharpUsers, user => Assert.Equal("C#", user.Technology));
        }

        [Fact]
        public void UserDbContext_CanOrderResults()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<UserDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new UserDbContext(options);

            context.info.Add(new Information { ID = 3, Name = "Charlie" });
            context.info.Add(new Information { ID = 1, Name = "Alice" });
            context.info.Add(new Information { ID = 2, Name = "Bob" });
            context.SaveChanges();

            // Act
            var ordered = context.info.OrderBy(i => i.Name).ToList();

            // Assert
            Assert.Equal(3, ordered.Count);
            Assert.Equal("Alice", ordered[0].Name);
            Assert.Equal("Bob", ordered[1].Name);
            Assert.Equal("Charlie", ordered[2].Name);
        }

        [Fact]
        public void UserDbContext_DatabaseName_IsUnique()
        {
            // Arrange
            var options1 = new DbContextOptionsBuilder<UserDbContext>()
                .UseInMemoryDatabase(databaseName: "DB1")
                .Options;

            var options2 = new DbContextOptionsBuilder<UserDbContext>()
                .UseInMemoryDatabase(databaseName: "DB2")
                .Options;

            // Act
            using var context1 = new UserDbContext(options1);
            using var context2 = new UserDbContext(options2);

            context1.info.Add(new Information { ID = 1, Name = "Context1" });
            context1.SaveChanges();

            context2.info.Add(new Information { ID = 1, Name = "Context2" });
            context2.SaveChanges();

            // Assert
            Assert.Single(context1.info.ToList());
            Assert.Single(context2.info.ToList());
            Assert.Equal("Context1", context1.info.First().Name);
            Assert.Equal("Context2", context2.info.First().Name);
        }

        [Fact]
        public void UserDbContext_Count_ReturnsCorrectValue()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<UserDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new UserDbContext(options);

            context.info.Add(new Information { ID = 1, Name = "User1" });
            context.info.Add(new Information { ID = 2, Name = "User2" });
            context.info.Add(new Information { ID = 3, Name = "User3" });
            context.SaveChanges();

            // Act
            var count = context.info.Count();

            // Assert
            Assert.Equal(3, count);
        }

        [Fact]
        public void UserDbContext_FirstOrDefault_ReturnsNull_WhenNotFound()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<UserDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new UserDbContext(options);

            // Act
            var result = context.info.FirstOrDefault(i => i.ID == 999);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void UserDbContext_SingleOrDefault_ReturnsNull_WhenMultipleMatches()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<UserDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new UserDbContext(options);

            context.info.Add(new Information { ID = 1, Technology = "C#" });
            context.info.Add(new Information { ID = 2, Technology = "C#" });
            context.SaveChanges();

            // Act & Assert
            // SingleOrDefault throws InvalidOperationException when multiple matches
            Assert.Throws<InvalidOperationException>(() =>
                context.info.Where(i => i.Technology == "C#").SingleOrDefault());
        }

        [Fact]
        public void UserDbContext_CanUseFindMethod()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<UserDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new UserDbContext(options);

            context.info.Add(new Information { ID = 42, Name = "The Answer" });
            context.SaveChanges();

            // Act
            var result = context.info.Find(42);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(42, result.ID);
            Assert.Equal("The Answer", result.Name);
        }

        [Fact]
        public void UserDbContext_Find_ReturnsNull_ForNonExistentKey()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<UserDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new UserDbContext(options);

            // Act
            var result = context.info.Find(999);

            // Assert
            Assert.Null(result);
        }
    }
}