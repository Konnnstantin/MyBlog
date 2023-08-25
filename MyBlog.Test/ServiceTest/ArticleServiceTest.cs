using FakeItEasy;
using FluentAssertions;
using MyBlog.Domains.Entity;
using MyBlog.Service;
using MyBlog.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MyBlog.Test.Service
{
    public class ArticleServiceTest
    {
        private readonly IArticleService _articleService;
        public ArticleServiceTest()
        {
            _articleService = A.Fake<IArticleService>();
        }

        [Fact]
        public void GetAllArticle() 
        {
            //Arrange -
            var article = A.Fake<Article>();
            A.CallTo(() => _articleService.GetAll());
            //Act
            var result = _articleService.GetAll();
            //Assert 
            result.Should().NotBeNull();
        }
        [Fact]
        public void GetArticle()
        {
            //Arrange
            Guid id = new Guid();
            var club = A.Fake<Article>();
            A.CallTo(() => _articleService.Get(id));
       
            //Act
            var result = _articleService.Get(id);
            //Assert
            result.Should().NotBeNull();
        }

    }
}
