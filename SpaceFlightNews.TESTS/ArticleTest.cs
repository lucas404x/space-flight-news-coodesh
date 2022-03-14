using Moq;
using SpaceFlightNews.Services;
using SpaceFlightNews.Data.Entities;
using Xunit;
using SpaceFlightNews.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using SpaceFlightNews.Data.Models;

namespace SpaceFlightNews.TESTS;

public class ArticleServiceTest
{
    private readonly IArticleServices _articleServices;
    private readonly Mock<IArticleRepository> _articleRepositorySub = new();

    public ArticleServiceTest()
    {
        _articleServices = new ArticleServices(_articleRepositorySub.Object);
    }

    [Fact]
    public async void GetArticles_ValidParameters_ReturnsList()
    {
        // Arrange
        List<Article> expected = new();
        _articleRepositorySub.Setup(
            m => m.GetArticlesByOffset(It.IsAny<int>()))
            .ReturnsAsync(expected);

        int limit = 100;
        int offset = 20;

        // Act
        var actual = await _articleServices.GetArticles(limit, offset);

        // Assert
        Assert.Equal<List<Article>>(expected, actual);
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(-1, 0)]
    public async void GetArticles_InvalidLimit_ThrowsException(int limit, int offset)
    {
        await Assert.ThrowsAsync<ArgumentException>(() => _articleServices.GetArticles(limit, offset));
    }

    [Theory]
    [InlineData(1, -1)]
    [InlineData(1, -1000)]

    public async void GetArticles_InvalidOffset_ThrowsException(int limit, int offset)
    {
        await Assert.ThrowsAsync<ArgumentException>(() => _articleServices.GetArticles(limit, offset));
    }

    [Fact]
    public async void GetArticle_ValidId_ReturnsArticle()
    {
        // Arrange
        string id = "abcde";
        var expected = new Article();
        _articleRepositorySub.Setup(
            m => m.GetArticle(It.IsAny<string>()))
            .ReturnsAsync(expected);

        // Act
        var actual = await _articleServices.GetArticle(id);

        // Assert
        Assert.Equal<Article>(expected, actual);
    }

    [Fact]
    public async void GetArticle_WithInexistentId_ThrowsException()
    {
        // Arrange
        string id = "this_id_not_exist";
        _articleRepositorySub.Setup(
            m => m.GetArticle(It.IsAny<string>()))
            .ReturnsAsync(() => null);

        // Act & Assert
        await Assert.ThrowsAsync<NullReferenceException>(() => _articleServices.GetArticle(id));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public async void GetArticle_InvalidId_ThrowsException(string id)
    {
        await Assert.ThrowsAsync<ArgumentException>(() => _articleServices.GetArticle(id));
    }

    [Fact]
    public async void AddArticle_WithValidArticle_ReturnsTrue()
    {
        // Arrange
        UserArticle article = new();
        bool expected = true;
        _articleRepositorySub.Setup(m => m.AddArticle(It.IsAny<Article>()))
        .ReturnsAsync(true);

        // Act
        var actual = await _articleServices.AddArticle(article);

        // Assert
        Assert.Equal<bool>(expected, actual);
    }

    [Fact]
    public async void AddArticle_WithNullArticle_ThrowsException()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(() => _articleServices.AddArticle(null));
    }

    [Fact]
    public async void UpdateArticle_WithValidParameters_ReturnsTrue()
    {
        // Arrange
        string id = "abcde";
        UserArticle article = new();
        bool expected = true;
        _articleRepositorySub.Setup(m => m.GetArticle(It.IsAny<string>()))
        .ReturnsAsync(new Article());
        _articleRepositorySub.Setup(m => m.UpdateArticle(It.IsAny<Article>()))
        .ReturnsAsync(expected);

        // Act
        var actual = await _articleServices.UpdateArticle(id, article);

        // Assert
        Assert.Equal<bool>(expected, actual);
    }

    [Fact]
    public async void UpdateArticle_WithInexistentId_ThrowsException()
    {
        // Arrange
        string id = "this_id_not_exist";
        UserArticle article = new();
        _articleRepositorySub.Setup(m => m.GetArticle(It.IsAny<string>()))
        .ReturnsAsync(() => null);

        // Act & Assert
        await Assert.ThrowsAsync<NullReferenceException>(() => _articleServices.UpdateArticle(id, article));
    }

    [Fact]
    public async void UpdateArticle_WithNullArticle_ThrowsException()
    {
        // Arrange
        string id = "abcde";

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => _articleServices.UpdateArticle(id, null));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public async void UpdateArticle_WithInvalidId_ThrowsException(string id)
    {
        // Arrange
        UserArticle article = new();

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => _articleServices.UpdateArticle(id, article));
    }

    [Fact]
    public async void DeleteArticle_WithValidId_ReturnsTrue() 
    {
        // Arrange
        string id = "abcde";
        bool expected = true;
        _articleRepositorySub.Setup(m => m.DeleteArticle(It.IsAny<string>()))
        .ReturnsAsync(expected);

        // Act
        var actual = await _articleServices.DeleteArticle(id);

        // Assert
        Assert.Equal<bool>(expected, actual);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public async void DeleteArticle_WithInvalidId_ThrowsException(string id)
    {
        // Assert
        await Assert.ThrowsAsync<ArgumentException>(() => _articleServices.DeleteArticle(id));
    }
}