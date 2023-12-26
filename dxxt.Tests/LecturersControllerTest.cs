namespace dxxt.Tests;

public class LecturersControllerTest
{
    [Theory]
    [InlineData(1, 2)]
    [InlineData(3, 4)]
    [InlineData(5, 6)]

    public void Post_Create_WithValidLecturer(int x, int y)
    {
        // Arrange


        // Act
        int result = x + y;

        // Assert
        Assert.Equal(3, result);
        Assert.
    }
}