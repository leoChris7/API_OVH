using Moq;
using API_OVH.Controllers;
using API_OVH.Models.DTO;
using API_OVH.Models.EntityFramework;
using API_OVH.Models.Repository;
using Microsoft.AspNetCore.Mvc;

namespace API_OVH.Controllers.Tests
{
    [TestClass]
    public class EquipementsControllerTest
    {
        private Mock<IEquipementRepository<Equipement, EquipementDTO, EquipementDetailDTO , EquipementSansNavigationDTO>> _mockRepository;
        private EquipementController _equipementController;

        [TestInitialize]
        public void Setup()
        {
            _mockRepository = new Mock<IEquipementRepository<Equipement, EquipementDTO, EquipementDetailDTO, EquipementSansNavigationDTO>>();

            _equipementController = new EquipementController(_mockRepository.Object);
        }

        [TestMethod]
        public async Task GetEquipements_ReturnsListOfEquipements()
        {
            // Arrange
            var equipementsDTO = new List<EquipementDTO>
                {
                    new () { IdEquipement = 1, NomEquipement = "Fenêtre" },
                    new () { IdEquipement = 2, NomEquipement = "Bureau" }
                };

            _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(equipementsDTO);

            // Act
            var actionResult = await _equipementController.GetEquipements();

            // Assert
            Assert.IsNotNull(actionResult.Value, "La liste des equipements est null.");
            Assert.IsInstanceOfType(actionResult.Value, typeof(IEnumerable<EquipementDTO>), "La liste retournée n'est pas une liste d'equipement.");
            Assert.AreEqual(2, ((IEnumerable<EquipementDTO>)actionResult.Value).Count(), "Le nombre d'equipements retourné est incorrect.");
        }

        [TestMethod]
        public async Task GetEquipements_ReturnsEmptyList_WhenEmpty()
        {
            // Arrange
            List<EquipementDTO> Equipements = new List<EquipementDTO>();
            _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(Equipements);

            // Act
            var actionResult = await _equipementController.GetEquipements();

            // Assert
            Assert.IsNotNull(actionResult.Value, "La liste des Equipements est null.");
            Assert.IsInstanceOfType(actionResult.Value, typeof(List<EquipementDTO>), "La liste retournée n'est pas une liste de types de Equipements.");
            var EquipementsList = actionResult.Value as List<EquipementDTO>;
            Assert.AreEqual(0, EquipementsList.Count, "Le nombre de Equipements retourné est incorrect.");
            Assert.IsTrue(!EquipementsList.Any(), "La liste des Equipements devrait être vide.");
        }

        [TestMethod]
        public async Task GetEquipementById_Returns_Equipement()
        {
            // Arrange
            var expectedEquipement = new EquipementDetailDTO
            {
                IdEquipement = 1,
                NomEquipement = "Bureau"
            };

            _mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(expectedEquipement);

            // Act
            var actionResult = await _equipementController.GetEquipementById(1);

            // Assert
            Assert.IsNotNull(actionResult, "GetEquipementById: objet retourné null");
            Assert.IsNotNull(actionResult.Value, "GetEquipementById: valeur retournée null");
            Assert.AreEqual(expectedEquipement, actionResult.Value as EquipementDetailDTO, "GetEquipementById: Equipements non égaux, objet incohérent retourné");
        }


        [TestMethod]
        public async Task GetEquipementById_Returns_NotFound_When_Equipement_NotFound()
        {
            // Act
            var actionResult = await _equipementController.GetEquipementById(0);

            // Assert
            Assert.IsNull(actionResult.Value, "GetEquipementById: objet retourné non null");
            Assert.IsInstanceOfType<NotFoundObjectResult>(actionResult.Result, "GetEquipementById: pas Not Found");
        }

        [TestMethod]
        public async Task GetEquipementByName_Returns_Equipement()
        {
            // Arrange
            var expectedEquipement = new EquipementDetailDTO { IdEquipement = 1, NomEquipement = "Radiateur" };

            _mockRepository.Setup(x => x.GetByStringAsync("Radiateur")).ReturnsAsync(expectedEquipement);

            // Act
            var actionResult = await _equipementController.GetEquipementByString("Radiateur");

            // Assert
            Assert.IsNotNull(actionResult, "GetSalleByName: objet retourné null");
            Assert.IsNotNull(actionResult.Value, "GetSalleByName: valeur retournée null");
            Assert.AreEqual(expectedEquipement, actionResult.Value as EquipementDetailDTO, "GetSalleByName: salles non égales, objet incohérent retourné");
        }

        [TestMethod]
        public async Task GetEquipementByNameRandomUppercase_Returns_Equipement()
        {
            // Arrange
            var expectedEquipement = new EquipementDetailDTO { IdEquipement = 1, NomEquipement = "Radiateur" };

            _mockRepository.Setup(x => x.GetByStringAsync("rADIATEUR")).ReturnsAsync(expectedEquipement);

            // Act
            var actionResult = await _equipementController.GetEquipementByString("rADIATEUR");

            // Assert
            Assert.IsNotNull(actionResult, "GetSalleByName: objet retourné null");
            Assert.IsNotNull(actionResult.Value, "GetSalleByName: valeur retournée null");
            Assert.AreEqual(expectedEquipement, actionResult.Value as EquipementDetailDTO, "GetSalleByName: salles non égales, objet incohérent retourné");
        }


        [TestMethod]
        public async Task GetSalleByString_Returns_NotFound_When_Salle_NotFound()
        {
            // Act
            var actionResult = await _equipementController.GetEquipementByString("Equipement inconnu");

            // Assert
            Assert.IsNull(actionResult.Value, "GetEquipementByString: objet retourné non null");
            Assert.IsInstanceOfType<NotFoundObjectResult>(actionResult.Result, "GetEquipementByString: not found a échoué");
        }

        [TestMethod]
        public async Task PostEquipementDTO_ModelValidated_CreationOK()
        {
            // Arrange
            EquipementSansNavigationDTO EquipementAjoute = new ()
            {
                IdEquipement = 1,
                NomEquipement = "Bureau",
                Hauteur = 99999999,
                Longueur = 100
            };

            // Act
            var actionResult = await _equipementController.PostEquipement(EquipementAjoute);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<EquipementSansNavigationDTO>), "PostEquipement: Pas un ActionResult<EquipementSansNavigation>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "PostEquipement: Pas un CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(EquipementSansNavigationDTO), "PostEquipement: Pas un EquipementSansNavigation");
            Assert.AreEqual(EquipementAjoute, (EquipementSansNavigationDTO)result.Value, "PostEquipement: Equipements non identiques");

        }

        [TestMethod]
        public async Task PostEquipement_ModelInvalid_ReturnsBadRequest()
        {
            // Arrange
            var invalidDto = new EquipementSansNavigationDTO();
            _equipementController.ModelState.AddModelError("IdDirection", "IdDirection est requis.");

            // Act
            var result = await _equipementController.PostEquipement(invalidDto);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task PutEquipement_ModelValidated_UpdateOK()
        {
            // Arrange
            Equipement Equipement = new Equipement
            {
                IdEquipement = 1,
                Hauteur = 50,
                Longueur = 1
            };

            EquipementSansNavigationDTO newEquipement = new ()
            {
                IdEquipement = 1,
                Hauteur = 1020,
                Longueur = 3000
            };

            _mockRepository.Setup(repo => repo.GetByIdWithoutDTOAsync(1)).ReturnsAsync(Equipement);

            // Act
            var actionResult = await _equipementController.PutEquipement(newEquipement.IdEquipement, newEquipement);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult");
        }

        [TestMethod]
        public async Task PutEquipement_ModelValidated_ReturnsBadRequest()
        {
            // Act
            var actionResult = await _equipementController.PutEquipement(3, new EquipementSansNavigationDTO
            {
                IdEquipement = 1
            });

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestObjectResult), "Pas un Badrequest");
        }

        [TestMethod]
        public async Task PutEquipement_ModelValidated_ReturnsNotFound()
        {
            // Act
            var actionResult = await _equipementController.PutEquipement(3, new EquipementSansNavigationDTO
            {
                IdEquipement = 3
            });

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundObjectResult), "Pas un NotFound");
        }

        [TestMethod]
        public async Task DeleteEquipementTest_OK()
        {
            // Arrange
            Equipement Equipement = new Equipement
            {
                IdEquipement = 1
            };

            _mockRepository.Setup(x => x.GetByIdWithoutDTOAsync(1)).ReturnsAsync(Equipement);

            // Act
            var actionResult = await _equipementController.DeleteEquipement(1);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult");
        }

        [TestMethod]
        public async Task DeleteEquipementTest_Returns_NotFound()
        {
            // Act
            var actionResult = await _equipementController.DeleteEquipement(1);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundObjectResult), "Pas un NotFoundResult");
        }
    }
}