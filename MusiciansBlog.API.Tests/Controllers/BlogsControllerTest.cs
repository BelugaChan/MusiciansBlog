using FakeItEasy;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MusiciansBlog.API.Controllers;
using MusiciansBlog.API.Infrastructure.Blogs.GetBlogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusiciansBlog.API.Tests.Controllers
{
    public class BlogsControllerTest
    {
        private readonly IMediator _mediator;
        public BlogsControllerTest()
        {
            _mediator = A.Fake<IMediator>();
        }

        [Fact]
        public async Task BlogsController_GetBlogs_ReturnsOk()
        {
            //Arrange
            var blogsQuery = A.Fake<GetBlogsQuery>();
       
            var blogsController = new BlogsController(_mediator);

            //Act
            var res = await blogsController.GetBlogs(blogsQuery, CancellationToken.None);

            //Assert
            res.Should().NotBeNull();

            var okRes = res.Should().BeOfType<OkObjectResult>().Subject;
            okRes.Value.Should().BeAssignableTo<GetBlogsResponse>();
        }
    }
}
