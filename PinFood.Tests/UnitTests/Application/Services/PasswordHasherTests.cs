using PinFood.Application.Services;

namespace PinFood.Tests.UnitTests.Application.Services;

public class PasswordHasherTests
    {
        private readonly PasswordHasher _passwordHasher;

        public PasswordHasherTests()
        {
            _passwordHasher = new PasswordHasher();
        }

        [Fact]
        public void Hash_ShouldGenerateHashWithSalt_WhenPasswordProvided()
        {
            // Arrange
            var password = "SecurePassword123!";

            // Act
            var hashedPassword = _passwordHasher.Hash(password);

            // Assert
            hashedPassword.ShouldNotBeNull();
            hashedPassword.ShouldContain("-");
            var parts = hashedPassword.Split('-');
            parts.Length.ShouldBe(2);
            parts[0].Length.ShouldBeGreaterThan(0); // Hash
            parts[1].Length.ShouldBeGreaterThan(0); // Salt
        }

        [Fact]
        public void Verify_ShouldReturnTrue_WhenValidPasswordAndHashProvided()
        {
            // Arrange
            var password = "SecurePassword123!";
            var hashedPassword = _passwordHasher.Hash(password);

            // Act
            var isValid = _passwordHasher.Verify(password, hashedPassword);

            // Assert
            isValid.ShouldBeTrue(); // Password matches the hash
        }

        [Fact]
        public void Verify_ShouldReturnFalse_WhenInvalidPasswordProvided()
        {
            // Arrange
            var password = "SecurePassword123!";
            var hashedPassword = _passwordHasher.Hash(password);
            var incorrectPassword = "WrongPassword456!";

            // Act
            var isValid = _passwordHasher.Verify(incorrectPassword, hashedPassword);

            // Assert
            isValid.ShouldBeFalse(); // Password does not match the hash
        }

        [Fact]
        public void Hash_ShouldGenerateUniqueHashes_WhenCalledMultipleTimesForSamePassword()
        {
            // Arrange
            var password = "SecurePassword123!";

            // Act
            var hashedPassword1 = _passwordHasher.Hash(password);
            var hashedPassword2 = _passwordHasher.Hash(password);

            // Assert
            hashedPassword1.ShouldNotBe(hashedPassword2); // Hashes should differ due to unique salts
        }
    }
