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
        private Mock<IBatimentRepository<Batiment, BatimentDTO, BatimentDetailDTO, BatimentSansNavigationDTO>> _mockRepository;
        private BatimentsController _batimentController;

        [TestInitialize]
        public void Setup()
        {
            _mockRepository = new Mock<IBatimentRepository<Batiment, BatimentDTO, BatimentDetailDTO, BatimentSansNavigationDTO>>();

            _batimentController = new BatimentsController(_mockRepository.Object);
        }

        [TestMethod]
        public async Task GetBatiments_ReturnsListOfBatiments()
        {
            // Arrange
            var batiments = new List<BatimentDTO>
                {
                    new() { IdBatiment = 1, NomBatiment = "IUT" },
                    new() { IdBatiment = 2, NomBatiment = "Tetras" },
                    new() { IdBatiment = 3, NomBatiment = "McDonald's"},
                    new() { IdBatiment = 4, NomBatiment = "Salle de sport"},
                    new() { IdBatiment = 5, NomBatiment = "Cabane Jardin"}
                };

            _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(batiments);

            // Act
            var actionResult = await _batimentController.GetBatiments();

            // Assert
            Assert.IsNotNull(actionResult.Value, "La liste des batiments est null.");
            Assert.IsInstanceOfType(actionResult.Value, typeof(IEnumerable<BatimentDTO>), "La liste retournée n'est pas une liste de types de batiments.");
            Assert.AreEqual(5, ((IEnumerable<BatimentDTO>)actionResult.Value).Count(), "Le nombre de batiments retourné est incorrect.");
        }

        [TestMethod]
        public async Task GetBatiments_ReturnsEmptyList_WhenEmpty()
        {
            // Arrange
            List<BatimentDTO> batiments = new List<BatimentDTO>();
            _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(batiments);

            // Act
            var actionResult = await _batimentController.GetBatiments();

            // Assert
            Assert.IsNotNull(actionResult.Value, "La liste des batiments est null.");
            Assert.IsInstanceOfType(actionResult.Value, typeof(List<BatimentDTO>), "La liste retournée n'est pas une liste de types de batiments.");
            var batimentsList = actionResult.Value as List<BatimentDTO>;
            Assert.AreEqual(0, batimentsList.Count, "Le nombre de batiments retourné est incorrect.");
            Assert.IsTrue(!batimentsList.Any(), "La liste des batiments devrait être vide.");
        }


        [TestMethod]
        public async Task GetBatimentById_Returns_Batiment()
        {
            // Arrange
            var expectedBatiment = new BatimentDetailDTO { IdBatiment = 1, 
                                                            NomBatiment = "Fenetre",
                                                            Salles = [
                                                                new SalleSansNavigationDTO {
                                                                IdSalle = 1,
                                                                IdBatiment = 1,
                                                                IdTypeSalle = 1,
                                                                NomSalle = "D101"
                                                            }
                                                            ]
            };

            _mockRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(expectedBatiment);

            // Act
            var actionResult = await _batimentController.GetBatimentById(1);

            // Assert
            Assert.IsNotNull(actionResult, "GetBatimentById: objet retourné null");
            Assert.IsNotNull(actionResult.Value, "GetBatimentById: valeur retournée null");
            Assert.AreEqual(expectedBatiment, actionResult.Value as BatimentDetailDTO, "GetBatimentById: types d'équipements non égaux, objet incohérent retourné");
        }


        [TestMethod]
        public async Task GetBatimentById_Returns_NotFound_When_Batiment_NotFound()
        {
            // Act
            var actionResult = await _batimentController.GetBatimentById(0);

            // Assert
            Assert.IsNull(actionResult.Value, "GetBatimentById: objet retourné non null");
            Assert.IsInstanceOfType<NotFoundObjectResult>(actionResult.Result, "GetBatimentById: pas Not Found");
        }

        [TestMethod]
        public async Task GetBatimentByName_Returns_Batiment()
        {
            // Arrange
            var expectedBatiment = new BatimentDetailDTO { IdBatiment = 1, NomBatiment = "Tetras" };

            _mockRepository.Setup(x => x.GetByStringAsync("Tetras")).ReturnsAsync(expectedBatiment);

            // Act
            var actionResult = await _batimentController.GetBatimentByName("Tetras");

            // Assert
            Assert.IsNotNull(actionResult, "GetBatimentByName: objet retourné null");
            Assert.IsNotNull(actionResult.Value, "GetBatimentByName: valeur retournée null");
            Assert.AreEqual(expectedBatiment, actionResult.Value as BatimentDetailDTO, "GetBatimentByName: batiments non égaux, objet incohérent retourné");
        }

        [TestMethod]
        public async Task GetBatimentByNameRandomUpperCase_Returns_Batiment()
        {
            // Arrange
            var expectedBatiment = new BatimentDetailDTO { IdBatiment = 1, NomBatiment = "Tetras" };

            _mockRepository.Setup(x => x.GetByStringAsync("TetRaS")).ReturnsAsync(expectedBatiment);

            // Act
            var actionResult = await _batimentController.GetBatimentByName("TetRaS");

            // Assert
            Assert.IsNotNull(actionResult, "GetBatimentByName: objet retourné null");
            Assert.IsNotNull(actionResult.Value, "GetBatimentByName: valeur retournée null");
            Assert.AreEqual(expectedBatiment, actionResult.Value as BatimentDetailDTO, "GetBatimentByName: batiments non égaux, objet incohérent retourné");
        }

        [TestMethod]
        public async Task GetBatimentByName_Returns_NotFound_When_Batiment_NotFound()
        {
            // Act
            var actionResult = await _batimentController.GetBatimentByName("Batiment inconnu");

            // Assert
            Assert.IsNull(actionResult.Value, "GetByName: objet retourné non null");
            Assert.IsInstanceOfType<NotFoundObjectResult>(actionResult.Result, "GetBatimentByName: not found a échoué");
        }

        [TestMethod]
        public async Task PostBatimentDTO_ModelValidated_CreationOK()
        {
            // Arrange
            BatimentSansNavigationDTO BatimentDTO = new ()
            {
                IdBatiment = 1,
                NomBatiment = "Tetras"
            };

            // Act
            var actionResult = await _batimentController.PostBatiment(BatimentDTO);

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
            var invalidDto = new BatimentSansNavigationDTO();
            _batimentController.ModelState.AddModelError("NomBatiment", "NomBatiment est requis.");

            // Act
            var result = await _batimentController.PostBatiment(invalidDto);

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
                NomBatiment = "IUT",
                Salles = []
            };

            BatimentSansNavigationDTO newBatiment = new BatimentSansNavigationDTO
            {
                IdBatiment = 1,
                NomBatiment = "IUT - Batiment D"
            };

            _mockRepository.Setup(x => x.GetByIdWithoutDTOAsync(1)).ReturnsAsync(Batiment);

            // Act
            var actionResult = await _batimentController.PutBatiment(newBatiment.IdBatiment, newBatiment);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult");
        }

        [TestMethod]
        public async Task PutBatiment_ModelValidated_ReturnsBadRequest()
        {
            // Act
            var actionResult = await _batimentController.PutBatiment(3, new BatimentSansNavigationDTO
            {
                IdBatiment = 1,
                NomBatiment = "Batiment échoué"
            });

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestObjectResult), "Pas un Badrequest");
        }

        [TestMethod]
        public async Task PutBatiment_ModelValidated_ReturnsNotFound()
        {
            // Act
            var actionResult = await _batimentController.PutBatiment(3, new BatimentSansNavigationDTO
            {
                IdBatiment = 3,
                NomBatiment = "Batiment non trouvé"
            });

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundObjectResult), "Pas un NotFound");
        }

        [TestMethod]
        public async Task DeleteBatimentTest_OK()
        {
            // Arrange
            Batiment Batiment = new Batiment
            {
                IdBatiment = 1,
                NomBatiment = "Tetras",
                Salles = [
                    new Salle{
                        IdBatiment=1,
                        IdSalle=1,
                        IdTypeSalle=1,
                        NomSalle="D10001",
                        Murs=[
                            new Mur{
                                IdMur=1,
                                Orientation=360,
                                Hauteur=100,
                                Longueur=100,
                                IdDirection=1,
                                IdSalle=1
                            }
                        ],
                    }
                ]
            };

            _mockRepository.Setup(x => x.GetByIdWithoutDTOAsync(1)).ReturnsAsync(Batiment);

            // Act
            var actionResult = await _batimentController.DeleteBatiment(1);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult");
        }

        [TestMethod]
        public async Task DeleteBatimentTest_Returns_NotFound()
        {
            // Act
            var actionResult = await _batimentController.DeleteBatiment(1);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundObjectResult), "Pas un NotFoundResult");
        }
    }
}