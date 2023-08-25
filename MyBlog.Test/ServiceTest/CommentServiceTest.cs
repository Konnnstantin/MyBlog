using FakeItEasy;
using FluentAssertions;
using MyBlog.Domains.Entity;
using MyBlog.Domains.ViewModels;
using MyBlog.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MyBlog.Test.Service
{
    public class CommentServiceTest
    {
        private readonly ICommentService _commentService;
        public CommentServiceTest()
        {
            _commentService = A.Fake<ICommentService>();
        }
        [Fact]
        public void CreateComment()
        {
            //Arrange
            Guid user = new Guid();
            var comment = A.Fake<CommentCreateViewModel>();
            A.CallTo(() => _commentService.CreateComment(comment,user)).Returns(user);

            //Act
            var result = _commentService.CreateComment(comment,user);
            //Assert
            result.Should().NotBeNull();
        }
    }
}
