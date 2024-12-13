using Microsoft.VisualStudio.TestTools.UnitTesting;
using API_OVH.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API_OVH.Models.DTO;
using Moq;
using API_OVH.Models.EntityFramework;
using API_OVH.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace API_OVH.Controllers.Tests
{
    [TestClass()]
    public class DirectionControllerTests
    {

        private Mock<IDirectionRepository<DirectionDetailDTO, DirectionSansNavigationDTO>> _mockRepository;
        private DirectionController _directionsController;

        [TestInitialize]
        public void Setup()
        {
            _mockRepository = new Mock<IDirectionRepository<DirectionDetailDTO, DirectionSansNavigationDTO>> ();

            _directionsController = new DirectionController(_mockRepository.Object);
        }

        [TestMethod()]
        public async Task GetDirections_ReturnsListOfDirections()
        {
            // Arrange
            List<DirectionSansNavigationDTO> directionDTO = 
            [
                    new () { IdDirection = 1, LettresDirection = "N" },
                    new () { IdDirection = 2, LettresDirection = "S" },
                    new () { IdDirection = 3, LettresDirection = "E" },
                    new () { IdDirection = 4, LettresDirection = "O" }
            ];

            _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(directionDTO);

            // Act
            var actionResult = await _directionsController.GetDirections();

            // Assert
            Assert.IsNotNull(actionResult.Value, "La liste des directions est null.");
            Assert.IsInstanceOfType(actionResult.Value, typeof(IEnumerable<DirectionSansNavigationDTO>), "La liste retournée n'est pas une liste de directions.");
            Assert.AreEqual(4, ((IEnumerable<DirectionSansNavigationDTO>)actionResult.Value).Count(), "Le nombre de directions retournées est incorrect.");
            _mockRepository.Verify(repo => repo.GetAllAsync(), Times.Once, "La méthode n'a pas été appelé qu'une fois");
        }

        [TestMethod]
        public async Task GetDirections_ReturnsEmptyList_WhenEmpty()
        {
            // Arrange: Liste vide
            List<DirectionSansNavigationDTO> directions = [];
            _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(directions);

            // Act
            var actionResult = await _directionsController.GetDirections();

            // Assert
            Assert.IsNotNull(actionResult.Value, "La liste des directions est null.");
            Assert.IsInstanceOfType(actionResult.Value, typeof(List<DirectionSansNavigationDTO>), "La liste retournée n'est pas une liste de types de directions.");
            var directionsList = actionResult.Value as List<DirectionSansNavigationDTO>;
            Assert.AreEqual(0, directionsList.Count, "Le nombre de directions retourné est incorrect.");
            Assert.IsTrue(!directionsList.Any(), "La liste des directions devrait être vide.");
            _mockRepository.Verify(repo => repo.GetAllAsync(), Times.Once, "La méthode n'a pas été appelé qu'une fois");
        }

        [TestMethod()]
        public async Task GetDirectionByIdTest_ReturnsDirection_OK()
        {
            // Arrange
            DirectionDetailDTO directionExpected = new ()
            {
                IdDirection = 1,
                LettresDirection = "N",
                Murs = [
                    new() { IdMur = 1 },
                    new() { IdMur = 2 }
                ]
            };

            _mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(directionExpected);

            // Act
            var actionResult = await _directionsController.GetDirection(1);

            // Assert
            Assert.IsNotNull(actionResult, "Direction: objet retourné null");
            Assert.IsNotNull(actionResult.Value, "Direction: valeur retournée null");
            Assert.AreEqual(directionExpected, actionResult.Value as DirectionDetailDTO, "Direction: directions non égales, objet incohérent retourné");
            _mockRepository.Verify(repo => repo.GetByIdAsync(1), Times.Once, "La méthode n'a pas été appelé qu'une fois");
        }

        [TestMethod()]
        public async Task GetDirectionByIdTest_ReturnsNotFound_WhenNotFound()
        {
            // Act
            var actionResult = await _directionsController.GetDirection(0);

            // Assert
            Assert.IsNull(actionResult.Value, "Direction: objet retourné non null");
            Assert.IsInstanceOfType<NotFoundResult>(actionResult.Result, "Direction: pas Not Found");
            _mockRepository.Verify(repo => repo.GetByIdAsync(0), Times.Once, "La méthode n'a pas été appelé qu'une fois");
        }

        [TestMethod()]
        public async Task GetDirectionBy10Degres_ShouldReturnCorrectDirection()
        {
            // Arrange
            DirectionDetailDTO directionNord = new()
            { 
                IdDirection = 1, 
                LettresDirection = "N", 
                Murs = [] 
            };

            _mockRepository.Setup(repo => repo.GetByDegreAsync(10)).ReturnsAsync(directionNord);

            // Act
            var actualDirectionResult = await _directionsController.GetDirectionByDegre(10);

            // Assert
            Assert.IsNotNull(actualDirectionResult, "GetDirectionByDegres: direction obtenue null");
            Assert.IsInstanceOfType(((DirectionDetailDTO)actualDirectionResult.Value), typeof(DirectionDetailDTO), "L'objet retourné n'est pas un direction detail  dto");
            Assert.AreEqual("N", ((DirectionDetailDTO)actualDirectionResult.Value).LettresDirection,
                $"Erreur pour {10}° : attendu {"N"}, obtenu {actualDirectionResult}");
            _mockRepository.Verify(repo => repo.GetByDegreAsync(10), Times.Once, "La méthode n'a pas été appelé qu'une fois");
        }

        [TestMethod()]
        public async Task GetDirectionByMinus45Degres_ShouldReturnCorrectDirection()
        {
            // Arrange
            DirectionDetailDTO directionNordOuest = new()
            {
                IdDirection = 1,
                LettresDirection = "NO",
                Murs = []
            };
            _mockRepository.Setup(repo => repo.GetByDegreAsync(-45)).ReturnsAsync(directionNordOuest);

            // Act
            var actualDirectionResult = await _directionsController.GetDirectionByDegre(-45);

            // Assert
            Assert.IsNotNull(actualDirectionResult, "GetDirectionByDegres: direction obtenue null");
            Assert.IsInstanceOfType(((DirectionDetailDTO)actualDirectionResult.Value), typeof(DirectionDetailDTO), "L'objet retourné n'est pas un direction detail  dto");
            Assert.AreEqual("NO", ((DirectionDetailDTO)actualDirectionResult.Value).LettresDirection,
                $"Erreur pour {10}° : attendu {"NO"}, obtenu {actualDirectionResult}");
            _mockRepository.Verify(repo => repo.GetByDegreAsync(-45), Times.Once, "La méthode n'a pas été appelé qu'une fois");
        }

        [TestMethod()]
        public async Task GetDirectionBy1800Degres_ShouldReturnCorrectDirection()
        {
            // Arrange
            DirectionDetailDTO directionSud = new()
            {
                IdDirection = 1,
                LettresDirection = "S",
                Murs = []
            };
            _mockRepository.Setup(repo => repo.GetByDegreAsync(1800)).ReturnsAsync(directionSud);

            // Act
            var actualDirectionResult = await _directionsController.GetDirectionByDegre(1800);

            // Assert
            Assert.IsNotNull(actualDirectionResult, "GetDirectionByDegres: direction obtenue null");
            Assert.IsInstanceOfType(((DirectionDetailDTO)actualDirectionResult.Value), typeof(DirectionDetailDTO), "L'objet retourné n'est pas un direction detail  dto");
            Assert.AreEqual("S", ((DirectionDetailDTO)actualDirectionResult.Value).LettresDirection,
                $"Erreur pour {1800}° : attendu {"S"}, obtenu {actualDirectionResult}");
            _mockRepository.Verify(repo => repo.GetByDegreAsync(1800), Times.Once, "La méthode n'a pas été appelé qu'une fois");
        }
    }
}