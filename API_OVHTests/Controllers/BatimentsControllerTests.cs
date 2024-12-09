using Moq;
using API_OVH.Controllers;
using API_OVH.Models.DTO;
using API_OVH.Models.EntityFramework;
using API_OVH.Models.Repository;
using Microsoft.AspNetCore.Mvc;

namespace API_OVH.Controllers.Tests
{
    [TestClass]
    public class BatimentsControllerTest
    {
        private Mock<IBatimentRepository<Batiment, BatimentDTO, BatimentSansNavigationDTO>> _mockRepository;
        private BatimentsController _BatimentController;

        [TestInitialize]
        public void Setup()
        {
            _mockRepository = new Mock<IBatimentRepository<Batiment, BatimentDTO, BatimentSansNavigationDTO>>();

            _BatimentController = new BatimentsController(_mockRepository.Object);
        }

        [TestMethod]
        public async Task GetBatiments_ReturnsListOfBatiments()
        {
            // Arrange
            var typesEquipement = new List<BatimentDTO>
                {
                    new() { IdBatiment = 1, NomBatiment = "IUT" },
                    new() { IdBatiment = 2, NomBatiment = "Tetras" }
                };

            _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(typesEquipement);

            // Act
            var actionResult = await _BatimentController.GetBatiments();

            // Assert
            Assert.IsNotNull(actionResult.Value, "La liste des batiments est null.");
            Assert.IsInstanceOfType(actionResult.Value, typeof(IEnumerable<BatimentDTO>), "La liste retournée n'est pas une liste de types de batiments.");
            Assert.AreEqual(2, ((IEnumerable<BatimentDTO>)actionResult.Value).Count(), "Le nombre de batiments retourné est incorrect.");
        }

        [TestMethod]
        public async Task GetBatimentById_Returns_Batiment()
        {
            // Arrange
            var expectedBatiment = new Batiment { IdBatiment = 1, NomBatiment = "Fenetre" };

            _mockRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(expectedBatiment);

            // Act
            var actionResult = _BatimentController.GetBatimentById(1).Result;

            // Assert
            Assert.IsNotNull(actionResult, "GetBatimentById: objet retourné null");
            Assert.IsNotNull(actionResult.Value, "GetBatimentById: valeur retournée null");
            Assert.AreEqual(expectedBatiment, actionResult.Value as Batiment, "GetBatimentById: types d'équipements non égaux, objet incohérent retourné");
        }


        [TestMethod]
        public async Task GetBatimentById_Returns_NotFound_When_Batiment_NotFound()
        {
            // Act
            var actionResult = await _BatimentController.GetBatimentById(0);

            // Assert
            Assert.IsNull(actionResult.Value, "GetBatimentById: objet retourné non null");
            Assert.IsInstanceOfType<NotFoundObjectResult>(actionResult.Result, "GetBatimentById: pas Not Found");
        }

        [TestMethod]
        public async Task GetBatimentByName_Returns_Batiment()
        {
            // Arrange
            var expectedBatiment = new Batiment { IdBatiment = 1, NomBatiment = "Tetras" };

            _mockRepository.Setup(x => x.GetByStringAsync("Tetras")).ReturnsAsync(expectedBatiment);

            // Act
            var actionResult = _BatimentController.GetBatimentByName("Tetras").Result;

            // Assert
            Assert.IsNotNull(actionResult, "GetBatimentByName: objet retourné null");
            Assert.IsNotNull(actionResult.Value, "GetBatimentByName: valeur retournée null");
            Assert.AreEqual(expectedBatiment, actionResult.Value as Batiment, "GetBatimentByName: types d'équipements non égaux, objet incohérent retourné");
        }


        [TestMethod]
        public async Task GetBatimentByName_Returns_NotFound_When_Batiment_NotFound()
        {
            // Act
            var actionResult = await _BatimentController.GetBatimentByName("Batiment inconnu");

            // Assert
            Assert.IsNull(actionResult.Value, "GetByName: objet retourné non null");
            Assert.IsInstanceOfType<NotFoundObjectResult>(actionResult.Result, "GetBatimentByName: not found a échoué");
        }

        [TestMethod]
        public async Task PostBatimentDTO_ModelValidated_CreationOK()
        {
            // Arrange
            BatimentSansNavigationDTO BatimentDTO = new BatimentSansNavigationDTO
            {
                IdBatiment = 1,
                NomBatiment = "Tetras"
            };

            // Act
            var actionResult = await _BatimentController.PostBatiment(BatimentDTO);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<BatimentSansNavigationDTO>), "PostBatiment: Pas un ActionResult<BatimentSansNavigationDTO>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "PostBatiment: Pas un CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(BatimentSansNavigationDTO), "PostBatiment: Pas un batiment");
            Assert.AreEqual(BatimentDTO, (BatimentSansNavigationDTO)result.Value, "PostBatiment: Batiments non identiques");

        }

        [TestMethod]
        public async Task PostBatiment_ModelInvalid_ReturnsBadRequest()
        {
            // Arrange
            var invalidDto = new BatimentSansNavigationDTO(); // Missing required fields
            _BatimentController.ModelState.AddModelError("NomBatiment", "NomBatiment est requis.");

            // Act
            var result = await _BatimentController.PostBatiment(invalidDto);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task PutBatiment_ModelValidated_UpdateOK()
        {
            // Arrange
            Batiment Batiment = new Batiment
            {
                IdBatiment = 1,
                NomBatiment = "A"
            };

            Batiment newBatiment = new Batiment
            {
                IdBatiment = 1,
                NomBatiment = "B"
            };

            _mockRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(Batiment);

            // Act
            var actionResult = _BatimentController.PutBatiment(newBatiment.IdBatiment, newBatiment).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }

        [TestMethod]
        public async Task PutBatiment_ModelValidated_ReturnsBadRequest()
        {
            // Act
            var actionResult = _BatimentController.PutBatiment(3, new Batiment
            {
                IdBatiment = 1,
                NomBatiment = "Type échoué"
            }).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestObjectResult), "Pas un Badrequest"); // Test du type de retour
        }

        [TestMethod]
        public async Task PutBatiment_ModelValidated_ReturnsNotFound()
        {
            // Act
            var actionResult = _BatimentController.PutBatiment(3, new Batiment
            {
                IdBatiment = 3,
                NomBatiment = "Type non trouvé"
            }).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundObjectResult), "Pas un NotFound"); // Test du type de retour
        }

        [TestMethod]
        public async Task DeleteBatimentTest_OK()
        {
            // Arrange
            Batiment Batiment = new Batiment
            {
                IdBatiment = 1,
                NomBatiment = "Tetras"
            };

            _mockRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(Batiment);

            // Act
            var actionResult = _BatimentController.DeleteBatiment(1).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }

        [TestMethod]
        public async Task DeleteBatimentTest_Returns_NotFound()
        {
            // Act
            var actionResult = _BatimentController.DeleteBatiment(1);

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundObjectResult), "Pas un NotFoundResult"); // Test du type de retour
        }
    }
}