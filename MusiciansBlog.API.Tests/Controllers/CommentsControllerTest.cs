using FakeItEasy;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MusiciansBlog.API.Controllers;
using MusiciansBlog.API.Infrastructure.Comments.GetComments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusiciansBlog.API.Tests.Controllers
{
    public class CommentsControllerTest
    {
        private readonly IMediator _mediator;
        public CommentsControllerTest()
        {
            _mediator = A.Fake<IMediator>();
        }

        [Fact]
        public async Task CommentsController_GetBlogComments_ReturnsOk()
        {
            //Arrange
            var commentsQuery = A.Fake<GetCommentsQuery>();

            var commentsController = new CommentsController(_mediator);
            
            //Act
            var res = await commentsController.GetBlogComments(commentsQuery, CancellationToken.None);
        
            //Arrange
            res.Should().NotBeNull();

            var okRes = res.Should().BeOfType<OkObjectResult>().Subject;
            okRes.Value.Should().BeAssignableTo<GetCommentsResponse>();
        }
    }
}
