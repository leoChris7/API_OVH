using Moq;
using API_OVH.Controllers;
using API_OVH.Models.DTO;
using API_OVH.Models.EntityFramework;
using API_OVH.Models.Repository;
using Microsoft.AspNetCore.Mvc;

namespace TypeSallesControllerTests
{
    [TestClass]
    public class MursControllerTest
    {
        private Mock<IMurRepository<Mur, MurDTO, MurSansNavigationDTO>> _mockRepository;
        private MursController _MurController;

        [TestInitialize]
        public void Setup()
        {
            _mockRepository = new Mock<IMurRepository<Mur, MurDTO, MurSansNavigationDTO>>();

            _MurController = new MursController(_mockRepository.Object);
        }

        [TestMethod]
        public async Task GetMurs_ReturnsListOfMurs()
        {
            // Arrange
            var MursDTO = new List<MurDTO>
                {
                    new MurDTO { IdMur = 1, NomSalle = "D101", Direction="N", Orientation=60 },
                    new MurDTO { IdMur = 2, NomSalle = "D351", Direction="S", Orientation=180 }
                };

            _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(MursDTO);

            // Act
            var actionResult = await _MurController.GetMurs();

            // Assert
            Assert.IsNotNull(actionResult.Value, "La liste des murs est null.");
            Assert.IsInstanceOfType(actionResult.Value, typeof(IEnumerable<MurDTO>), "La liste retournée n'est pas une liste de mur.");
            Assert.AreEqual(2, ((IEnumerable<MurDTO>)actionResult.Value).Count(), "Le nombre de murs retourné est incorrect.");
        }

        [TestMethod]
        public async Task GetMurById_Returns_Mur()
        {
            // Arrange
            var expectedMur = new Mur { IdMur = 1, 
                                        Hauteur = 100, 
                                        Longueur = 200, 
                                        Equipements = [], 
                                        Capteurs = [], 
                                        IdDirection = 1, 
                                        IdSalle = 1, 
                                        Orientation = 300 };

            _mockRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(expectedMur);

            // Act
            var actionResult = _MurController.GetMurById(1).Result;

            // Assert
            Assert.IsNotNull(actionResult, "GetMurById: objet retourné null");
            Assert.IsNotNull(actionResult.Value, "GetMurById: valeur retournée null");
            Assert.AreEqual(expectedMur, actionResult.Value as Mur, "GetMurById: Murs non égaux, objet incohérent retourné");
        }


        [TestMethod]
        public async Task GetMurById_Returns_NotFound_When_Mur_NotFound()
        {
            // Act
            var actionResult = await _MurController.GetMurById(0);

            // Assert
            Assert.IsInstanceOfType<NotFoundObjectResult>(actionResult.Result, "GetMurById: pas Not Found");
        }

        [TestMethod]
        public async Task PostMurDTO_ModelValidated_CreationOK()
        {
            // Arrange
            MurSansNavigationDTO murAjoute = new MurSansNavigationDTO
            {
                IdMur = 1, Hauteur = 100, Longueur = 100, Orientation = 3, IdSalle = 1, IdDirection = 1
            };

            // Act
            var actionResult = await _MurController.PostMur(murAjoute);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<MurSansNavigationDTO>), "PostMur: Pas un ActionResult<MurSansNavigation>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "PostMur: Pas un CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(MurSansNavigationDTO), "PostMur: Pas un MurSansNavigation");
            Assert.AreEqual(murAjoute, (MurSansNavigationDTO)result.Value, "PostMur: Murs non identiques");

        }

        [TestMethod]
        public async Task PostMur_ModelInvalid_ReturnsBadRequest()
        {
            // Arrange
            var invalidDto = new MurSansNavigationDTO();
            _MurController.ModelState.AddModelError("IdDirection", "IdDirection est requis.");

            // Act
            var result = await _MurController.PostMur(invalidDto);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task PutMur_ModelValidated_UpdateOK()
        {
            // Arrange
            Mur Mur = new Mur
            {
                IdMur = 1,
                IdDirection = 1,
                Hauteur = 50,
                Longueur = 1,
                Orientation = 0,
                Capteurs = [],
                Equipements = [],
                IdSalle = 1
            };

            Mur newMur = new Mur
            {
                IdMur = 1,
                IdDirection = 1,
                Hauteur = 1020,
                Longueur = 3000,
                Orientation = 120,
                Capteurs = [],
                Equipements = [],
                IdSalle = 1
            };

            _mockRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(Mur);

            // Act
            var actionResult = _MurController.PutMur(newMur.IdMur, newMur).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult");
        }

        [TestMethod]
        public async Task PutMur_ModelValidated_ReturnsBadRequest()
        {
            // Act
            var actionResult = _MurController.PutMur(3, new Mur
            {
                IdMur = 1
            }).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestObjectResult), "Pas un Badrequest");
        }

        [TestMethod]
        public async Task PutMur_ModelValidated_ReturnsNotFound()
        {
            // Act
            var actionResult = _MurController.PutMur(3, new Mur
            {
                IdMur = 3
            }).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundObjectResult), "Pas un NotFound");
        }

        [TestMethod]
        public async Task DeleteMurTest_OK()
        {
            // Arrange
            Mur Mur = new Mur
            {
                IdMur = 1
            };

            _mockRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(Mur);

            // Act
            var actionResult = _MurController.DeleteMur(1).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult");
        }

        [TestMethod]
        public async Task DeleteMurTest_Returns_NotFound()
        {
            // Act
            var actionResult = _MurController.DeleteMur(1);

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundObjectResult), "Pas un NotFoundResult");
        }
    }
}