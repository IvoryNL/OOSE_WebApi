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
            _repository = new OnderwijseenheidRepositoryMock();
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
        public async void Create()
        {
            var newOnderwijseenheid = new Onderwijseenheid { Id = 9, Naam = "Onderwijseenheid 1 test", Beschrijving = "Dit is een test beschrijving", Coordinator = "Jan Jansen", Studiepunten = 30 };
            await _controller.Post(newOnderwijseenheid);
            var onderwijseenheid = await _controller.Get(9);
            var result = onderwijseenheid.Result as OkObjectResult;
            Assert.NotNull(result.Value);
        }

        [Fact]
        public async void Update()
        {
            var newOnderwijseenheid = new Onderwijseenheid { Id = 1, Naam = "Onderwijseenheid 1 test", Beschrijving = "Dit is een test beschrijving", Coordinator = "Jan Jansen", Studiepunten = 20 };
            await _controller.Put(1, newOnderwijseenheid);
            var onderwijseenheid = await _controller.Get(1);
            var result = onderwijseenheid.Result as OkObjectResult;
            var updatedOnderwijseenheid = result.Value as Onderwijseenheid;

            Assert.Equal(updatedOnderwijseenheid.Studiepunten, 20);
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