using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers;
using WebAPI.Entities;
using WebAPI.Repositories.Interfaces;

namespace WebApi.Tests.OnderwijseenheidControllerTest
{
    public class TestOnderwijseenheiController
    {
        private readonly IRepository<Onderwijseenheid> _repository;
        private readonly OnderwijseenheidController _controller;

        public TestOnderwijseenheiController()
        {
            _repository = new OnderwijseenheidRepositoryTest();
            _controller = new OnderwijseenheidController(_repository);
        }

        [Fact]
        public async void GetAll()
        {
            var onderwijseenhedenResult = await _controller.Get();
            var result = onderwijseenhedenResult.Result as OkObjectResult;
            var onderwijseenheden = result.Value as List<Onderwijseenheid>;
            Assert.Equal(onderwijseenheden.Count, 4);
        }

        [Fact]
        public async void GetById_ShouldFind()
        {
            var onderwijseenheid = await _controller.Get(3);
            var result = onderwijseenheid.Result as OkObjectResult;
            Assert.NotNull(result);
        }

        [Fact]
        public async void GetById_ShouldNotFind()
        {
            var onderwijseenheid = await _controller.Get(9);
            var result = onderwijseenheid.Result as OkObjectResult;
            Assert.Null(result.Value);
        }

        [Fact]
        public async void Delete_ShouldNotFind()
        {
            await _controller.Delete(3);
            var onderwijseenheid = await _controller.Get(3);
            var result = onderwijseenheid.Result as OkObjectResult;
            Assert.Null(result.Value);
        }
    }
}