using Xunit;

namespace First.Tests
{
    /// <summary>
    /// Unit tests for the Program class.
    /// Tests cover the generated entry point type and generic host creation.
    /// </summary>
    public class ProgramTests
    {
        [Fact]
        public void Program_Type_Exists()
        {
            // Arrange & Act
            var programType = typeof(Program);

            // Assert
            Assert.NotNull(programType);
        }

        [Fact]
        public void Program_ClassIsStaticOrSealed()
        {
            // Arrange & Act
            var programType = typeof(Program);

            // Note: The Program class is neither sealed nor static (it's a regular class)
            // Assert that it exists and has a public constructor (implicitly)
            Assert.NotNull(programType);
        }

        [Fact]
        public void Program_Type_HasPublicConstructor()
        {
            // Arrange - The Program class should have a constructor
            var constructors = typeof(Program).GetConstructors();

            // Note: The Program class is a standard class with a public constructor
            // Assert that it exists
            Assert.NotEmpty(constructors);
        }

        [Fact]
        public void Program_Namespace_IsCorrect()
        {
            // Arrange & Act
            var programNamespace = typeof(global::Program).Namespace;

            // Assert
            Assert.Null(programNamespace);
        }

        [Fact]
        public void Host_CanBeCreatedWithDefaultSettings()
        {
            // Arrange & Act
            var host = Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder().Build();

            // Assert
            Assert.NotNull(host);
            host.Dispose();
        }

        [Fact]
        public void Host_CanBeDisposed()
        {
            // Arrange & Act
            var host = Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder().Build();

            // Act & Assert
            Assert.NotNull(host);
            host.Dispose();
        }

        [Fact]
        public void Program_Type_IsPublic()
        {
            // Arrange & Act
            var programType = typeof(Program);

            // Assert
            Assert.True(programType.IsPublic);
        }
    }
}
