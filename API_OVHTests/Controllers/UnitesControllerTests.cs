using Moq;
using API_OVH.Controllers;
using API_OVH.Models.DTO;
using API_OVH.Models.EntityFramework;
using API_OVH.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using static API_OVH.Models.Repository.IUniteRepository;

namespace API_OVH.Controllers.Tests
{
    [TestClass]
    public class UnitesControllerTest
    {
        private Mock<IUniteRepository<Unite, UniteDTO, UniteDetailDTO>> _mockRepository;
        private UnitesController _UniteController;

        [TestInitialize]
        public void Setup()
        {
            _mockRepository = new Mock<IUniteRepository<Unite, UniteDTO, UniteDetailDTO>>();

            _UniteController = new UnitesController(_mockRepository.Object);
        }

        [TestMethod]
        public async Task GetUnites_ReturnsListOfUnites()
        {
            // Arrange
            var uniteDTO = new List<UniteDTO>
                {
                    new UniteDTO { IdUnite = 1, NomUnite = "Centimètre", SigleUnite="Cm" },
                    new UniteDTO { IdUnite = 2, NomUnite = "Kilomètre", SigleUnite="Km" }
                };

            _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(uniteDTO);

            // Act
            var actionResult = await _UniteController.GetUnites();

            // Assert
            Assert.IsNotNull(actionResult.Value, "La liste des unités est null.");
            Assert.IsInstanceOfType(actionResult.Value, typeof(IEnumerable<UniteDTO>), "La liste retournée n'est pas une liste d'unités.");
            Assert.AreEqual(2, ((IEnumerable<UniteDTO>)actionResult.Value).Count(), "Le nombre d'unités retourné est incorrect.");
        }

        [TestMethod]
        public async Task GetUniteById_Returns_Unite()
        {
            // Arrange
            var expectedUnite = new UniteDetailDTO { IdUnite = 1, NomUnite = "Ultraviolets", SigleUnite="UV", Capteurs = [] };

            _mockRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(expectedUnite);

            // Act
            var actionResult = _UniteController.GetUniteById(1).Result;

            // Assert
            Assert.IsNotNull(actionResult, "GetUniteById: objet retourné null");
            Assert.IsNotNull(actionResult.Value, "GetUniteById: valeur retournée null");
            Assert.AreEqual(expectedUnite, actionResult.Value as UniteDetailDTO, "GetUniteById: Unités non égaux, objet incohérent retourné");
        }


        [TestMethod]
        public async Task GetUniteById_Returns_NotFound_When_Unite_NotFound()
        {
            // Act
            var actionResult = await _UniteController.GetUniteById(0);

            // Assert
            Assert.IsNull(actionResult.Value, "GetUniteById: objet retourné non null");
            Assert.IsInstanceOfType<NotFoundObjectResult>(actionResult.Result, "GetUniteById: pas Not Found");
        }

        [TestMethod]
        public async Task GetUniteByName_Returns_Unite()
        {
            // Arrange
            var expectedUnite = new UniteDetailDTO { IdUnite = 1, NomUnite = "Ultraviolets", SigleUnite= "UV", Capteurs = [] };

            _mockRepository.Setup(x => x.GetByStringAsync("Ultraviolets")).ReturnsAsync(expectedUnite);

            // Act
            var actionResult = _UniteController.GetUniteByName("Ultraviolets").Result;

            // Assert
            Assert.IsNotNull(actionResult, "GetUniteByName: objet retourné null");
            Assert.IsNotNull(actionResult.Value, "GetUniteByName: valeur retournée null");
            Assert.AreEqual(expectedUnite, actionResult.Value as UniteDetailDTO, "GetUniteByName: unités non égaux, objet incohérent retourné");
        }


        [TestMethod]
        public async Task GetUniteByName_Returns_NotFound_When_Unite_NotFound()
        {
            // Act
            var actionResult = await _UniteController.GetUniteByName("Unité inconnue");

            // Assert
            Assert.IsNull(actionResult.Value, "GetUniteByName: objet retourné non null");
            Assert.IsInstanceOfType<NotFoundObjectResult>(actionResult.Result, "GetUniteByName: not found a échoué");
        }

        [TestMethod]
        public async Task GetUniteBySigle_Returns_Unite()
        {
            // Arrange
            var expectedUnite = new UniteDetailDTO { IdUnite = 1, NomUnite = "Ultraviolets", SigleUnite = "UV", Capteurs = [] };

            _mockRepository.Setup(x => x.GetByStringAsync("UV")).ReturnsAsync(expectedUnite);

            // Act
            var actionResult = _UniteController.GetUniteByName("UV").Result;

            // Assert
            Assert.IsNotNull(actionResult, "GetUniteBySigle: objet retourné null");
            Assert.IsNotNull(actionResult.Value, "GetUniteBySigle: valeur retournée null");
            Assert.AreEqual(expectedUnite, actionResult.Value as UniteDetailDTO, "GetUniteBySigle: unités non égaux, objet incohérent retourné");
        }


        [TestMethod]
        public async Task GetUniteBySigle_Returns_NotFound_When_Unite_NotFound()
        {
            // Act
            var actionResult = await _UniteController.GetUniteBySigle("Unité inconnue");

            // Assert
            Assert.IsInstanceOfType<NotFoundObjectResult>(actionResult.Result, "GetUniteBySigle: not found a échoué");
        }

        [TestMethod]
        public async Task PostUniteDTO_ModelValidated_CreationOK()
        {
            // Arrange
            UniteDTO UniteDTO = new UniteDTO
            {
                IdUnite = 1,
                NomUnite = "Kilogrammes",
                SigleUnite = "Kg"
            };

            // Act
            var actionResult = await _UniteController.PostUnite(UniteDTO);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<UniteDTO>), "PostUnite: Pas un ActionResult<UniteDTO>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "PostUnite: Pas un CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(UniteDTO), "PostUnite: Pas un uniteDTO");
            Assert.AreEqual(UniteDTO, (UniteDTO)result.Value, "PostUnite: Unités non identiques");

        }

        [TestMethod]
        public async Task PostUnite_ModelInvalid_ReturnsBadRequest()
        {
            // Arrange
            var invalidDto = new UniteDTO(); // Missing required fields
            _UniteController.ModelState.AddModelError("NomUnite", "NomUnite est requis.");

            // Act
            var result = await _UniteController.PostUnite(invalidDto);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task PutUnite_ModelValidated_UpdateOK()
        {
            // Arrange
            Unite Unite = new Unite
            {
                IdUnite = 1,
                NomUnite = "Ultraviolets",
                SigleUnite = "UV"
            };

            Unite newUnite = new Unite
            {
                IdUnite = 1,
                NomUnite = "Température",
                SigleUnite = "°C"
            };

            _mockRepository.Setup(x => x.GetByIdWithoutDTOAsync(1)).ReturnsAsync(Unite);

            // Act
            var actionResult = _UniteController.PutUnite(newUnite.IdUnite, newUnite).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult");
        }

        [TestMethod]
        public async Task PutUnite_ModelValidated_ReturnsBadRequest()
        {
            // Act
            var actionResult = _UniteController.PutUnite(3, new Unite
            {
                IdUnite = 1,
                NomUnite = "Type échoué"
            }).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestObjectResult), "Pas un Badrequest");
        }

        [TestMethod]
        public async Task PutUnite_ModelValidated_ReturnsNotFound()
        {
            // Act
            var actionResult = _UniteController.PutUnite(3, new Unite
            {
                IdUnite = 3,
                NomUnite = "Type non trouvé"
            }).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundObjectResult), "Pas un NotFound");
        }

        [TestMethod]
        public async Task DeleteUniteTest_OK()
        {
            // Arrange
            Unite Unite = new Unite
            {
                IdUnite = 1,
                NomUnite = "TD"
            };

            _mockRepository.Setup(x => x.GetByIdWithoutDTOAsync(1)).ReturnsAsync(Unite);

            // Act
            var actionResult = _UniteController.DeleteUnite(1).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult");
        }

        [TestMethod]
        public async Task DeleteUniteTest_Returns_NotFound()
        {
            // Act
            var actionResult = _UniteController.DeleteUnite(1);

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundObjectResult), "Pas un NotFoundResult");
        }
    }
}