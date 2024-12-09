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
        private UnitesController _uniteController;

        [TestInitialize]
        public void Setup()
        {
            _mockRepository = new Mock<IUniteRepository<Unite, UniteDTO, UniteDetailDTO>>();

            _uniteController = new UnitesController(_mockRepository.Object);
        }

        [TestMethod]
        public async Task GetUnites_ReturnsListOfUnites()
        {
            // Arrange
            var uniteDTO = new List<UniteDTO>
                {
                    new () { IdUnite = 1, NomUnite = "Centimètre", SigleUnite="Cm" },
                    new () { IdUnite = 2, NomUnite = "Kilomètre", SigleUnite="Km" }
                };

            _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(uniteDTO);

            // Act
            var actionResult = await _uniteController.GetUnites();

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
            var actionResult = await _uniteController.GetUniteById(1);

            // Assert
            Assert.IsNotNull(actionResult, "GetUniteById: objet retourné null");
            Assert.IsNotNull(actionResult.Value, "GetUniteById: valeur retournée null");
            Assert.AreEqual(expectedUnite, actionResult.Value as UniteDetailDTO, "GetUniteById: Unités non égaux, objet incohérent retourné");
        }


        [TestMethod]
        public async Task GetUniteById_Returns_NotFound_When_Unite_NotFound()
        {
            // Act
            var actionResult = await _uniteController.GetUniteById(0);

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
            var actionResult = await _uniteController.GetUniteByName("Ultraviolets");

            // Assert
            Assert.IsNotNull(actionResult, "GetUniteByName: objet retourné null");
            Assert.IsNotNull(actionResult.Value, "GetUniteByName: valeur retournée null");
            Assert.AreEqual(expectedUnite, actionResult.Value as UniteDetailDTO, "GetUniteByName: unités non égaux, objet incohérent retourné");
        }


        [TestMethod]
        public async Task GetUniteByName_Returns_NotFound_When_Unite_NotFound()
        {
            // Act
            var actionResult = await _uniteController.GetUniteByName("Unité inconnue");

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
            var actionResult = await _uniteController.GetUniteByName("UV");

            // Assert
            Assert.IsNotNull(actionResult, "GetUniteBySigle: objet retourné null");
            Assert.IsNotNull(actionResult.Value, "GetUniteBySigle: valeur retournée null");
            Assert.AreEqual(expectedUnite, actionResult.Value as UniteDetailDTO, "GetUniteBySigle: unités non égaux, objet incohérent retourné");
        }


        [TestMethod]
        public async Task GetUniteBySigle_Returns_NotFound_When_Unite_NotFound()
        {
            // Act
            var actionResult = await _uniteController.GetUniteBySigle("Unité inconnue");

            // Assert
            Assert.IsInstanceOfType<NotFoundObjectResult>(actionResult.Result, "GetUniteBySigle: not found a échoué");
        }

        [TestMethod]
        public async Task PostUniteDTO_ModelValidated_CreationOK()
        {
            // Arrange
            var uniteDTO = new UniteDTO
            {
                IdUnite = 1,
                NomUnite = "Kilogrammes",
                SigleUnite = "Kg"
            };

            // Act
            var actionResult = await _uniteController.PostUnite(uniteDTO);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<UniteDTO>), "PostUnite: Pas un ActionResult<UniteDTO>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "PostUnite: Pas un CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(UniteDTO), "PostUnite: Pas un uniteDTO");
            Assert.AreEqual(uniteDTO, (UniteDTO)result.Value, "PostUnite: Unités non identiques");

        }

        [TestMethod]
        public async Task PostUnite_ModelInvalid_ReturnsBadRequest()
        {
            // Arrange
            var invalidDto = new UniteDTO();
            _uniteController.ModelState.AddModelError("NomUnite", "NomUnite est requis.");

            // Act
            var result = await _uniteController.PostUnite(invalidDto);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task PutUnite_ModelValidated_UpdateOK()
        {
            // Arrange
            Unite Unite = new ()
            {
                IdUnite = 1,
                NomUnite = "Ultraviolets",
                SigleUnite = "UV"
            };

            Unite newUnite = new ()
            {
                IdUnite = 1,
                NomUnite = "Température",
                SigleUnite = "°C"
            };

            _mockRepository.Setup(x => x.GetByIdWithoutDTOAsync(1)).ReturnsAsync(Unite);

            // Act
            var actionResult = await _uniteController.PutUnite(newUnite.IdUnite, newUnite);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult");
        }

        [TestMethod]
        public async Task PutUnite_ModelValidated_ReturnsBadRequest()
        {
            // Act
            var actionResult = await _uniteController.PutUnite(3, new Unite
            {
                IdUnite = 1,
                NomUnite = "Type échoué"
            });

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestObjectResult), "Pas un Badrequest");
        }

        [TestMethod]
        public async Task PutUnite_ModelValidated_ReturnsNotFound()
        {
            // Act
            var actionResult = await _uniteController.PutUnite(3, new Unite
            {
                IdUnite = 3,
                NomUnite = "Type non trouvé"
            });

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundObjectResult), "Pas un NotFound");
        }

        [TestMethod]
        public async Task DeleteUniteTest_OK()
        {
            // Arrange
            Unite Unite = new ()
            {
                IdUnite = 1,
                NomUnite = "TD"
            };

            _mockRepository.Setup(x => x.GetByIdWithoutDTOAsync(1)).ReturnsAsync(Unite);

            // Act
            var actionResult = await _uniteController.DeleteUnite(1);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult");
        }

        [TestMethod]
        public async Task DeleteUniteTest_Returns_NotFound()
        {
            // Act
            var actionResult = await _uniteController.DeleteUnite(1);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundObjectResult), "Pas un NotFoundResult");
        }
    }
}