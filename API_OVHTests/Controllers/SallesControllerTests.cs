using Moq;
using API_OVH.Controllers;
using API_OVH.Models.DTO;
using API_OVH.Models.EntityFramework;
using API_OVH.Models.Repository;
using Microsoft.AspNetCore.Mvc;

namespace API_OVH.Controllers.Tests
{
    [TestClass]
    public class SallesControllerTest
    {
        private Mock<ISalleRepository<Salle, SalleSansNavigationDTO, SalleDTO, SalleDetailDTO>> _mockRepository;
        private SallesController _SalleController;

        [TestInitialize]
        public void Setup()
        {
            _mockRepository = new Mock<ISalleRepository<Salle, SalleSansNavigationDTO, SalleDTO, SalleDetailDTO>>();

            _SalleController = new SallesController(_mockRepository.Object);
        }

        [TestMethod]
        public async Task GetSalles_ReturnsListOfSalle()
        {
            // Arrange
            var typesEquipement = new List<SalleDTO>
                {
                    new SalleDTO { IdSalle = 1, NomSalle = "D101" },
                    new SalleDTO { IdSalle = 2, NomSalle = "D351" }
                };

            _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(typesEquipement);

            // Act
            var actionResult = await _SalleController.GetSalles();

            // Assert
            Assert.IsNotNull(actionResult.Value, "GetSalles: La liste des salles est null.");
            Assert.IsInstanceOfType(actionResult.Value, typeof(IEnumerable<SalleDTO>), "GetSalles: La liste retournée n'est pas une liste de salles.");
            Assert.AreEqual(2, ((IEnumerable<SalleDTO>)actionResult.Value).Count(), "GetSalles: Le nombre de salles retournées est incorrect.");
        }

        [TestMethod]
        public async Task GetSalleById_Returns_Salle()
        {
            // Arrange
            var expectedSalle = new SalleDetailDTO { IdSalle = 1, NomSalle = "Fenetre" };

            _mockRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(expectedSalle);

            // Act
            var actionResult = _SalleController.GetSalleById(1).Result;

            // Assert
            Assert.IsNotNull(actionResult, "GetSalleById: objet retourné null");
            Assert.IsNotNull(actionResult.Value, "GetSalleById: valeur retournée null");
            Assert.AreEqual(expectedSalle, actionResult.Value as SalleDetailDTO, "GetSalleById:Salles non égales, objet incohérent retourné");
        }


        [TestMethod]
        public async Task GetSalleById_Returns_NotFound_When_Salle_NotFound()
        {
            // Act
            var actionResult = await _SalleController.GetSalleById(0);

            // Assert
            Assert.IsNull(actionResult.Value, "Salles: objet retourné non null");
            Assert.IsInstanceOfType<NotFoundObjectResult>(actionResult.Result, "GetSalleById: pas Not Found");
        }

        [TestMethod]
        public async Task GetSalleByName_Returns_Salle()
        {
            // Arrange
            var expectedSalle = new SalleDetailDTO { IdSalle = 1, NomSalle = "D101" };

            _mockRepository.Setup(x => x.GetByStringAsync("D101")).ReturnsAsync(expectedSalle);

            // Act
            var actionResult = _SalleController.GetSalleByName("D101").Result;

            // Assert
            Assert.IsNotNull(actionResult, "GetSalleByName: objet retourné null");
            Assert.IsNotNull(actionResult.Value, "GetSalleByName: valeur retournée null");
            Assert.AreEqual(expectedSalle, actionResult.Value as SalleDetailDTO, "GetSalleByName: salles non égales, objet incohérent retourné");
        }


        [TestMethod]
        public async Task GetSalleByName_Returns_NotFound_When_Salle_NotFound()
        {
            // Act
            var actionResult = await _SalleController.GetSalleByName("Salle inconnue");

            // Assert
            Assert.IsNull(actionResult.Value, "GetSalleByName: objet retourné non null");
            Assert.IsInstanceOfType<NotFoundObjectResult>(actionResult.Result, "GetSalleByName: not found a échoué");
        }

        [TestMethod]
        public async Task PostSalleDTO_ModelValidated_CreationOK()
        {
            // Arrange
            SalleSansNavigationDTO SalleDTO = new SalleSansNavigationDTO
            {
                IdSalle = 1,
                NomSalle = "D101"
            };

            // Act
            var actionResult = await _SalleController.PostSalle(SalleDTO);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<SalleSansNavigationDTO>), "PostSalle: Pas un ActionResult<SalleSansNavigationDTO>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "PostSalle: Pas un CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(SalleSansNavigationDTO), "PostSalle: Pas une salle");
            Assert.AreEqual(SalleDTO, (SalleSansNavigationDTO)result.Value, "PostSalle: Salles non identiques");

        }

        [TestMethod]
        public async Task PostSalle_ModelInvalid_ReturnsBadRequest()
        {
            // Arrange
            var invalidDto = new SalleSansNavigationDTO(); // Missing required fields
            _SalleController.ModelState.AddModelError("NomSalle", "Nom est requis.");

            // Act
            var result = await _SalleController.PostSalle(invalidDto);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task PutSalle_ModelValidated_UpdateOK()
        {
            // Arrange
            Salle Salle = new Salle
            {
                IdSalle = 1,
                NomSalle = "A"
            };

            Salle newSalle = new Salle
            {
                IdSalle = 1,
                NomSalle = "B"
            };

            _mockRepository.Setup(x => x.GetByIdWithoutDTOAsync(1)).ReturnsAsync(Salle);

            // Act
            var actionResult = _SalleController.PutSalle(newSalle.IdSalle, newSalle).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult");
        }

        [TestMethod]
        public async Task PutSalle_ModelValidated_ReturnsBadRequest()
        {
            // Act
            var actionResult = _SalleController.PutSalle(3, new Salle
            {
                IdSalle = 1,
                NomSalle = "Type échoué"
            }).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestObjectResult), "Pas un Badrequest");
        }

        [TestMethod]
        public async Task PutSalle_ModelValidated_ReturnsNotFound()
        {
            // Act
            var actionResult = _SalleController.PutSalle(3, new Salle
            {
                IdSalle = 3,
                NomSalle = "Type non trouvé"
            }).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundObjectResult), "Pas un NotFound"); // Test du type de retour
        }

        [TestMethod]
        public async Task DeleteSalleTest_OK()
        {
            // Arrange
            Salle Salle = new Salle
            {
                IdSalle = 1,
                NomSalle = "D101",
                Murs = [],
                IdBatiment = 1,
                IdTypeSalle = 1
            };

            _mockRepository.Setup(x => x.GetByIdWithoutDTOAsync(1)).ReturnsAsync(Salle);

            // Act
            var actionResult = _SalleController.DeleteSalle(1).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult");
        }

        [TestMethod]
        public async Task DeleteSalleTest_Returns_NotFound()
        {
            // Act
            var actionResult = _SalleController.DeleteSalle(1);

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundObjectResult), "Pas un NotFoundResult");
        }
    }
}