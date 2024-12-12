using Moq;
using API_OVH.Controllers;
using API_OVH.Models.DTO;
using API_OVH.Models.EntityFramework;
using API_OVH.Models.Repository;
using Microsoft.AspNetCore.Mvc;

namespace API_OVH.Controllers.Tests
{
    [TestClass]
    public class MursControllerTest
    {
        private Mock<IMurRepository<Mur, MurDTO, MurDetailDTO, MurSansNavigationDTO>> _mockRepository;
        private MursController _murController;

        [TestInitialize]
        public void Setup()
        {
            _mockRepository = new Mock<IMurRepository<Mur, MurDTO, MurDetailDTO, MurSansNavigationDTO>>();

            _murController = new MursController(_mockRepository.Object);
        }

        [TestMethod]
        public async Task GetMurs_ReturnsListOfMurs()
        {
            // Arrange
            var MursDTO = new List<MurDTO>
                {
                    new () { IdMur = 1, NomSalle = "D101", Direction="N", Orientation=60 },
                    new () { IdMur = 2, NomSalle = "D351", Direction="S", Orientation=180 }
                };

            _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(MursDTO);

            // Act
            var actionResult = await _murController.GetMurs();

            // Assert
            Assert.IsNotNull(actionResult.Value, "La liste des murs est null.");
            Assert.IsInstanceOfType(actionResult.Value, typeof(IEnumerable<MurDTO>), "La liste retournée n'est pas une liste de mur.");
            Assert.AreEqual(2, ((IEnumerable<MurDTO>)actionResult.Value).Count(), "Le nombre de murs retourné est incorrect.");
        }

        [TestMethod]
        public async Task GetMurById_Returns_Mur()
        {
            // Arrange
            var expectedMur = new MurDetailDTO { IdMur = 1, 
                                        Hauteur = 100, 
                                        Longueur = 200, 
                                        Equipements = [], 
                                        Capteurs = [], 
                                        IdDirection = 1, 
                                        IdSalle = 1, 
                                        Orientation = 300 };

            _mockRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(expectedMur);

            // Act
            var actionResult = await _murController.GetMurById(1);

            // Assert
            Assert.IsNotNull(actionResult, "GetMurById: objet retourné null");
            Assert.IsNotNull(actionResult.Value, "GetMurById: valeur retournée null");
            Assert.AreEqual(expectedMur, actionResult.Value as MurDetailDTO, "GetMurById: Murs non égaux, objet incohérent retourné");
        }


        [TestMethod]
        public async Task GetMurById_Returns_NotFound_When_Mur_NotFound()
        {
            // Act
            var actionResult = await _murController.GetMurById(0);

            // Assert
            Assert.IsNull(actionResult.Value, "GetMurById: objet retourné non null");
            Assert.IsInstanceOfType<NotFoundObjectResult>(actionResult.Result, "GetMurById: pas Not Found");
        }

        [TestMethod]
        public async Task PostMurDTO_ModelValidated_CreationOK()
        {
            // Arrange
            MurSansNavigationDTO murAjoute = new ()
            {
                IdMur = 1, Hauteur = 100, Longueur = 100, Orientation = 3, IdSalle = 1, IdDirection = 1
            };

            // Act
            var actionResult = await _murController.PostMur(murAjoute);

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
            _murController.ModelState.AddModelError("IdDirection", "IdDirection est requis.");

            // Act
            var result = await _murController.PostMur(invalidDto);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task PutMur_ModelValidated_UpdateOK()
        {
            // Arrange
            Mur Mur = new ()
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

            MurSansNavigationDTO newMur = new ()
            {
                IdMur = 1,
                IdDirection = 1,
                Hauteur = 1020,
                Longueur = 3000,
                Orientation = 120,
                IdSalle = 1
            };

            _mockRepository.Setup(x => x.GetByIdWithoutDTOAsync(1)).ReturnsAsync(Mur);

            // Act
            var actionResult = await _murController.PutMur(newMur.IdMur, newMur);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult");
        }

        [TestMethod]
        public async Task PutMur_ModelValidated_ReturnsBadRequest()
        {
            // Act
            var actionResult = await _murController.PutMur(3, new MurSansNavigationDTO
            {
                IdMur = 1
            });

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestObjectResult), "Pas un Badrequest");
        }

        [TestMethod]
        public async Task PutMur_ModelValidated_ReturnsNotFound()
        {
            // Act
            var actionResult = await _murController.PutMur(3, new MurSansNavigationDTO
            {
                IdMur = 3
            });

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

            _mockRepository.Setup(x => x.GetByIdWithoutDTOAsync(1)).ReturnsAsync(Mur);

            // Act
            var actionResult = await _murController.DeleteMur(1);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult");
        }

        [TestMethod]
        public async Task DeleteMurTest_Returns_NotFound()
        {
            // Act
            var actionResult = await _murController.DeleteMur(1);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundObjectResult), "Pas un NotFoundResult");
        }
    }
}