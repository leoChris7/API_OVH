using Moq;
using API_OVH.Controllers;
using API_OVH.Models.DTO;
using API_OVH.Models.EntityFramework;
using API_OVH.Models.Repository;
using Microsoft.AspNetCore.Mvc;

namespace API_OVH.Controllers.Tests
{
    [TestClass]
    public class CapteursControllerTest
    {
        private Mock<ICapteurRepository<Capteur, CapteurDTO, CapteurDetailDTO, CapteurSansNavigationDTO>> _mockRepository;
        private CapteursController _capteurController;

        [TestInitialize]
        public void Setup()
        {
            _mockRepository = new Mock<ICapteurRepository<Capteur, CapteurDTO, CapteurDetailDTO, CapteurSansNavigationDTO>>();

            _capteurController = new CapteursController(_mockRepository.Object);
        }

        [TestMethod]
        public async Task GetCapteurs_ReturnsListOfCapteurs()
        {
            // Arrange
            var lesCapteurs = new List<CapteurDTO>
                {
                    new() { IdCapteur = 1, NomCapteur = "Termomètre 3000C", NomSalle = "D101" },
                    new() { IdCapteur = 2, NomCapteur = "Humidiman", NomSalle = "D21002-A" }
                };

            _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(lesCapteurs);

            // Act
            var actionResult = await _capteurController.GetCapteurs();

            // Assert
            Assert.IsNotNull(actionResult.Value, "La liste des capteurs est null.");
            Assert.IsInstanceOfType(actionResult.Value, typeof(IEnumerable<CapteurDTO>), "La liste retournée n'est pas une liste de types de capteurs.");
            Assert.AreEqual(2, ((IEnumerable<CapteurDTO>)actionResult.Value).Count(), "Le nombre de capteurs retourné est incorrect.");
        }

        [TestMethod]
        public async Task GetCapteurById_Returns_Capteur()
        {
            // Arrange
            var expectedCapteur = new CapteurDetailDTO { IdCapteur = 1, NomCapteur = "CO2 Maximum Detection" };

            _mockRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(expectedCapteur);

            // Act
            var actionResult = await _capteurController.GetCapteurById(1);

            // Assert
            Assert.IsNotNull(actionResult, "GetCapteurById: objet retourné null");
            Assert.IsNotNull(actionResult.Value, "GetCapteurById: valeur retournée null");
            Assert.AreEqual(expectedCapteur, actionResult.Value as CapteurDetailDTO, "GetCapteurById: capteurs non égaux, objet incohérent retourné");
        }


        [TestMethod]
        public async Task GetCapteurById_Returns_NotFound_When_Capteur_NotFound()
        {
            // Act
            var actionResult = await _capteurController.GetCapteurById(0);

            // Assert
            Assert.IsNull(actionResult.Value, "GetCapteurById: objet retourné non null");
            Assert.IsInstanceOfType<NotFoundObjectResult>(actionResult.Result, "GetCapteurById: pas Not Found");
        }

        [TestMethod]
        public async Task GetCapteurByName_Returns_Capteur()
        {
            // Arrange
            var expectedCapteur = new CapteurDetailDTO
            {
                IdCapteur = 1,
                NomCapteur = "Max Humidico 3000",
                EstActif = "NSP",
                Salle = new Salle()
            };

            _mockRepository.Setup(x => x.GetByStringAsync("Max Humidico 3000")).ReturnsAsync(expectedCapteur);

            // Act
            var actionResult = await _capteurController.GetCapteurByName("Max Humidico 3000");

            // Assert
            Assert.IsNotNull(actionResult, "GetCapteurByName: objet retourné null");
            Assert.IsNotNull(actionResult.Value, "GetCapteurByName: valeur retournée null");
            Assert.AreEqual(expectedCapteur, actionResult.Value as CapteurDetailDTO, "GetCapteurByName: capteurs non égaux, objet incohérent retourné");
        }


        [TestMethod]
        public async Task GetCapteurByName_Returns_NotFound_When_Capteur_NotFound()
        {
            // Act
            var actionResult = await _capteurController.GetCapteurByName("Capteur inconnu");

            // Assert
            Assert.IsNull(actionResult.Value, "GetCapteurByName: objet retourné non null");
            Assert.IsInstanceOfType<NotFoundObjectResult>(actionResult.Result, "GetCapteurByName: not found a échoué");
        }

        [TestMethod]
        public async Task PostCapteurDTO_ModelValidated_CreationOK()
        {
            // Arrange
            CapteurSansNavigationDTO CapteurDTO = new CapteurSansNavigationDTO
            {
                IdCapteur = 1,
                NomCapteur = "Max Humidico 3000",
                EstActif = "NSP",
                IdMur = 1,
                XCapteur = 0,
                YCapteur = 100,
                ZCapteur = 2000
            };

            // Act
            var actionResult = await _capteurController.PostCapteur(CapteurDTO);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<CapteurSansNavigationDTO>), "PostCapteur: Pas un ActionResult<CapteurSansNavigationDTO>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "PostCapteur: Pas un CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(CapteurSansNavigationDTO), "PostCapteur: Pas un Capteur");
            Assert.AreEqual(CapteurDTO, (CapteurSansNavigationDTO)result.Value, "PostCapteur: Capteurs non identiques");

        }

        [TestMethod]
        public async Task PostCapteur_ModelInvalid_ReturnsBadRequest()
        {
            // Arrange
            var invalidDto = new CapteurSansNavigationDTO();
            _capteurController.ModelState.AddModelError("NomCapteur", "NomCapteur est requis.");

            // Act
            var result = await _capteurController.PostCapteur(invalidDto);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task PutCapteur_ModelValidated_UpdateOK()
        {
            // Arrange
            Capteur Capteur = new Capteur
            {
                IdCapteur = 1,
                NomCapteur = "A"
            };

            Capteur newCapteur = new Capteur
            {
                IdCapteur = 1,
                NomCapteur = "B"
            };

            _mockRepository.Setup(x => x.GetByIdWithoutDTOAsync(1)).ReturnsAsync(Capteur);

            // Act
            var actionResult = await _capteurController.PutCapteur(newCapteur.IdCapteur, newCapteur);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult");
        }

        [TestMethod]
        public async Task PutCapteur_ModelValidated_ReturnsBadRequest()
        {
            // Act
            var actionResult = await _capteurController.PutCapteur(3, new Capteur
            {
                IdCapteur = 1,
                NomCapteur = "Type échoué"
            });

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestObjectResult), "Pas un Badrequest");
        }

        [TestMethod]
        public async Task PutCapteur_ModelValidated_ReturnsNotFound()
        {
            // Act
            var actionResult = await _capteurController.PutCapteur(3, new Capteur
            {
                IdCapteur = 3,
                NomCapteur = "Type non trouvé"
            });

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundObjectResult), "Pas un NotFound");
        }

        [TestMethod]
        public async Task DeleteCapteurTest_OK()
        {
            // Arrange
            Capteur Capteur = new()
            {
                IdCapteur = 1,
                NomCapteur = "CO2 Max"
            };

            _mockRepository.Setup(x => x.GetByIdWithoutDTOAsync(1)).ReturnsAsync(Capteur);

            // Act
            var actionResult = await _capteurController.DeleteCapteur(1);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); 
        }

        [TestMethod]
        public async Task DeleteCapteurTest_Returns_NotFound()
        {
            // Act
            var actionResult = await _capteurController.DeleteCapteur(1);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundObjectResult), "Pas un NotFoundResult");
        }
    }
}