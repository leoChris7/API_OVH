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

        private Mock<IDirectionRepository<Direction>> _mockRepository;
        private DirectionController _directionsController;

        [TestInitialize]
        public void Setup()
        {
            _mockRepository = new Mock<IDirectionRepository<Direction>>();

            _directionsController = new DirectionController(_mockRepository.Object);
        }

        [TestMethod()]
        public async Task GetDirectionsTest()
        {
            // Arrange
            var directionDTO = new List<Direction>
                {
                    new Direction { IdDirection = 1, LettresDirection = "N" },
                    new Direction { IdDirection = 2, LettresDirection = "S" },
                    new Direction { IdDirection = 3, LettresDirection = "E" },
                    new Direction { IdDirection = 4, LettresDirection = "O" }
            };

            _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(directionDTO);

            // Act
            var actionResult = await _directionsController.GetDirections();

            // Assert
            Assert.IsNotNull(actionResult.Value, "La liste des directions est null.");
            Assert.IsInstanceOfType(actionResult.Value, typeof(IEnumerable<Direction>), "La liste retournée n'est pas une liste de directions.");
            Assert.AreEqual(4, ((IEnumerable<Direction>)actionResult.Value).Count(), "Le nombre de directions retournées est incorrect.");
        }

        [TestMethod()]
        public async Task GetDirectionByIdTest_ReturnsDirection_OK()
        {
            // Arrange
            var directionExpected = new Direction
            {
                IdDirection = 1,
                LettresDirection = "N",
                Murs = [
                    new() { IdMur = 1 },
                ]
            };

            _mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(directionExpected);

            // Act
            var actionResult = await _directionsController.GetDirection(1);

            // Assert
            Assert.IsNotNull(actionResult, "Direction: objet retourné null");
            Assert.IsNotNull(actionResult.Value, "Direction: valeur retournée null");
            Assert.AreEqual(directionExpected, actionResult.Value as Direction, "Direction: directions non égales, objet incohérent retourné");
        }

        [TestMethod()]
        public async Task GetDirectionByIdTest_ReturnsNotFound_WhenNotFound()
        {
            // Act
            var actionResult = await _directionsController.GetDirection(0);

            // Assert
            Assert.IsNull(actionResult.Value, "Direction: objet retourné non null");
            Assert.IsInstanceOfType<NotFoundResult>(actionResult.Result, "Direction: pas Not Found");
        }

        [TestMethod()]
        public async Task GetDirectionByDegres_ShouldReturnCorrectDirection()
        {
            // Arrange
            var degreesToTest = new List<(decimal degrees, string expectedDirection)>
                {
                    (-360, "N"),
                    (0, "N"),
                    (90, "E"),
                    (180, "S"),
                    (270, "O"),
                    (45, "NE"),
                    (135, "SE"),
                    (225, "SO"),
                    (315, "NO"),
                    (3600, "N"),
                    (360, "N"),
                    (22.5m, "N")
                };

            foreach (var (degrees, expectedDirection) in degreesToTest)
            {
                // Arrange
                _mockRepository.Setup(x => x.GetByDegreAsync(degrees)).ReturnsAsync(new Direction { LettresDirection = expectedDirection});

                // Act
                var actualDirectionResult = await _directionsController.GetDirectionByDegre(degrees);

                // Assert
                Assert.IsNotNull(actualDirectionResult, "GetDirectionByDegres: direction obtenue null");
                Assert.IsInstanceOfType(((Direction)actualDirectionResult.Value), typeof(Direction));
                Assert.AreEqual(expectedDirection, ((Direction)actualDirectionResult.Value).LettresDirection,
                    $"Erreur pour {degrees}° : attendu {expectedDirection}, obtenu {actualDirectionResult}");
            }
        }



    }
}