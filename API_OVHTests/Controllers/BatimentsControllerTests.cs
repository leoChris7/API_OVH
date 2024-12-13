using Moq;
using API_OVH.Controllers;
using API_OVH.Models.DTO;
using API_OVH.Models.EntityFramework;
using API_OVH.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

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
            List<BatimentDTO> batiments =
                [
                    new() { IdBatiment = 1, NomBatiment = "IUT" },
                    new() { IdBatiment = 2, NomBatiment = "Tetras" },
                    new() { IdBatiment = 3, NomBatiment = "McDonald's"},
                    new() { IdBatiment = 4, NomBatiment = "Salle de sport"},
                    new() { IdBatiment = 5, NomBatiment = "Cabane Jardin"}
                ];

            _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(batiments);

            // Act
            var actionResult = await _batimentController.GetBatiments();

            // Assert
            Assert.IsNotNull(actionResult.Value, "La liste des batiments est null.");
            Assert.IsInstanceOfType(actionResult.Value, typeof(IEnumerable<BatimentDTO>), "La liste retournée n'est pas une liste de types de batiments.");
            Assert.AreEqual(5, ((IEnumerable<BatimentDTO>)actionResult.Value).Count(), "Le nombre de batiments retourné est incorrect.");
            _mockRepository.Verify(repo => repo.GetAllAsync(), Times.Once, "La méthode n'a pas été appelé qu'une fois");
        }

        [TestMethod]
        public async Task GetBatiments_ReturnsEmptyList_WhenEmpty()
        {
            // Arrange: Liste vide
            List<BatimentDTO> batiments = [];
            _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(batiments);

            // Act
            var actionResult = await _batimentController.GetBatiments();

            // Assert
            Assert.IsNotNull(actionResult.Value, "La liste des batiments est null.");
            Assert.IsInstanceOfType(actionResult.Value, typeof(List<BatimentDTO>), "La liste retournée n'est pas une liste de types de batiments.");
            var batimentsList = actionResult.Value as List<BatimentDTO>;
            Assert.AreEqual(0, batimentsList.Count, "Le nombre de batiments retourné est incorrect.");
            Assert.IsTrue(!batimentsList.Any(), "La liste des batiments devrait être vide.");
            _mockRepository.Verify(repo => repo.GetAllAsync(), Times.Once, "La méthode n'a pas été appelé qu'une fois");
        }

        [TestMethod]
        public async Task GetBatimentById_Returns_Batiment()
        {
            // Arrange
            var expectedBatiment = new BatimentDetailDTO { 
                IdBatiment = 1, 
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

            _mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(expectedBatiment);

            // Act
            var actionResult = await _batimentController.GetBatimentById(1);

            // Assert
            Assert.IsNotNull(actionResult, "GetBatimentById: objet retourné null");
            Assert.IsNotNull(actionResult.Value, "GetBatimentById: valeur retournée null");
            Assert.AreEqual(expectedBatiment, actionResult.Value as BatimentDetailDTO, "GetBatimentById: types d'équipements non égaux, objet incohérent retourné");
            _mockRepository.Verify(repo => repo.GetByIdAsync(1), Times.Once, "La méthode n'a pas été appelé qu'une fois");
        }

        [TestMethod]
        public async Task GetBatimentById_Returns_NotFound_When_Batiment_NotFound()
        {
            // Arrange: batiment n'existe pas
            _mockRepository.Setup(repo => repo.GetByIdAsync(0));

            // Act
            var actionResult = await _batimentController.GetBatimentById(0);

            // Assert
            Assert.IsNull(actionResult.Value, "GetBatimentById: objet retourné non null");
            Assert.IsInstanceOfType<NotFoundObjectResult>(actionResult.Result, "GetBatimentById: pas Not Found");
            _mockRepository.Verify(repo => repo.GetByIdAsync(0), Times.Once, "La méthode n'a pas été appelé qu'une fois");
        }

        [TestMethod]
        public async Task GetBatimentByName_Returns_Batiment()
        {
            // Arrange
            BatimentDetailDTO expectedBatiment = new () { IdBatiment = 1, NomBatiment = "Tetras" };
            String? nomExactBatiment = "Tetras";

            _mockRepository.Setup(repo => repo.GetByStringAsync(nomExactBatiment)).ReturnsAsync(expectedBatiment);

            // Act
            var actionResult = await _batimentController.GetBatimentByName(nomExactBatiment);

            // Assert
            Assert.IsNotNull(actionResult, "GetBatimentByName: objet retourné null");
            Assert.IsNotNull(actionResult.Value, "GetBatimentByName: valeur retournée null");
            Assert.AreEqual(expectedBatiment, actionResult.Value as BatimentDetailDTO, "GetBatimentByName: batiments non égaux, objet incohérent retourné");
            _mockRepository.Verify(repo => repo.GetByStringAsync(nomExactBatiment), Times.Once, "La méthode n'a pas été appelé qu'une fois");
        }

        [TestMethod]
        public async Task GetBatimentByNameRandomUpperCase_Returns_Batiment()
        {
            // Arrange
            BatimentDetailDTO expectedBatiment = new () { IdBatiment = 1, NomBatiment = "Tetras" };
            String? nomBatimentDeforme = "TetRaS";

            _mockRepository.Setup(repo => repo.GetByStringAsync(nomBatimentDeforme)).ReturnsAsync(expectedBatiment);

            // Act
            var actionResult = await _batimentController.GetBatimentByName(nomBatimentDeforme);

            // Assert
            Assert.IsNotNull(actionResult, "GetBatimentByName: objet retourné null");
            Assert.IsNotNull(actionResult.Value, "GetBatimentByName: valeur retournée null");
            Assert.AreEqual(expectedBatiment, actionResult.Value as BatimentDetailDTO, "GetBatimentByName: batiments non égaux, objet incohérent retourné");
            _mockRepository.Verify(repo => repo.GetByStringAsync(nomBatimentDeforme), Times.Once, "La méthode n'a pas été appelé qu'une fois");
        }

        [TestMethod]
        public async Task GetBatimentByName_Returns_NotFound_When_Batiment_NotFound()
        {
            // Arrange
            String? nomBatimentInexistant = "Batiment inconnu";
            _mockRepository.Setup(repo => repo.GetByStringAsync(nomBatimentInexistant));

            // Act
            var actionResult = await _batimentController.GetBatimentByName(nomBatimentInexistant);

            // Assert
            Assert.IsNull(actionResult.Value, "GetBatimentByName: objet retourné non null");
            Assert.IsInstanceOfType<NotFoundObjectResult>(actionResult.Result, "GetBatimentByName: not found a échoué");
            _mockRepository.Verify(repo => repo.GetByStringAsync(nomBatimentInexistant), Times.Once, "La méthode n'a pas été appelé qu'une fois");
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

            _mockRepository.Setup(repo => repo.AddAsync(BatimentDTO));

            // Act
            var actionResult = await _batimentController.PostBatiment(BatimentDTO);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<BatimentSansNavigationDTO>), "PostBatiment: Pas un ActionResult<BatimentSansNavigationDTO>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "PostBatiment: Pas un CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;

            // Simuler la validation du modèle ajouté manuellement
            _batimentController.ModelState.Clear();
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(result.Value);
            bool isValid = Validator.TryValidateObject(result.Value, validationContext, validationResults, true);
            Assert.IsTrue(isValid, "La confirmation du modèle crée a échoué.");

            Assert.IsInstanceOfType(result.Value, typeof(BatimentSansNavigationDTO), "PostBatiment: Pas un batiment");
            Assert.AreEqual(BatimentDTO, (BatimentSansNavigationDTO)result.Value, "PostBatiment: Batiments non identiques");
            _mockRepository.Verify(repo => repo.AddAsync(It.IsAny<BatimentSansNavigationDTO>()), Times.Once, "La méthode n'a pas été appelé alors qu'elle aurait dû.");
        }

        [TestMethod]
        public async Task PostBatiment_ModelInvalid_ReturnsBadRequest()
        {
            // Arrange
            var invalidDto = new BatimentSansNavigationDTO {
                NomBatiment = "Le Très Grand Batiment Qui Depasse La Limite"
            };
            _mockRepository.Setup(repo => repo.AddAsync(invalidDto));

            // Simuler une validation réelle en appelant explicitement TryValidateModel si nécessaire
            var validationContext = new ValidationContext(invalidDto);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(invalidDto, validationContext, validationResults, true);

            foreach (var validationResult in validationResults)
            {
                _batimentController.ModelState.AddModelError(validationResult.MemberNames.FirstOrDefault() ?? string.Empty, validationResult.ErrorMessage);
            }

            // Act
            var result = await _batimentController.PostBatiment(invalidDto);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult), "Pas un bad request. ");

            // Vérification du nombre d'erreurs dans ModelState
            Assert.AreEqual(1, validationResults.Count, $"Le nombre d'erreurs de validation attendues est {1}, mais {validationResults.Count} ont été trouvées.");

            _mockRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Batiment>(), It.IsAny<BatimentSansNavigationDTO>()), Times.Never, "La méthode a été appelé alors qu'elle n'aurait pas dû.");
        }

        [TestMethod]
        public async Task PutBatiment_ModelValidated_UpdateOK()
        {
            // Arrange
            Batiment Batiment = new ()
            {
                IdBatiment = 1,
                NomBatiment = "IUT",
                Salles = []
            };

            BatimentSansNavigationDTO newBatiment = new ()
            {
                IdBatiment = 1,
                NomBatiment = "IUT - Batiment D"
            };

            _mockRepository.Setup(repo => repo.GetByIdWithoutDTOAsync(1)).ReturnsAsync(Batiment);
            _mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(new BatimentDetailDTO { IdBatiment= newBatiment.IdBatiment, NomBatiment= newBatiment.NomBatiment, Salles = [] });
            _mockRepository.Setup(repo => repo.UpdateAsync(Batiment, newBatiment));

            // Act
            var actionResult = await _batimentController.PutBatiment(newBatiment.IdBatiment, newBatiment);

            // Assert
            var changedObject = await _batimentController.GetBatimentById(1);
            Assert.AreEqual(changedObject.Value.NomBatiment, newBatiment.NomBatiment, "Nom du batiment incohérent après changement.");
            Assert.AreEqual(changedObject.Value.IdBatiment, newBatiment.IdBatiment, "Id du batiment incohérent après changement");
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult");
            _mockRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Batiment>(), It.IsAny<BatimentSansNavigationDTO>()), Times.Once, "La méthode n'a pas été appelé alors qu'elle aurait dû.");
        }

        [TestMethod]
        public async Task PutBatiment_ModelValidated_ReturnsBadRequest()
        {
            // Arrange
            BatimentSansNavigationDTO batimentBadRequest = new ()
            {
                IdBatiment = 1,
                NomBatiment = "Batiment échoué"
            };

            // Act
            var actionResult = await _batimentController.PutBatiment(3, batimentBadRequest);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestObjectResult), "Pas un Badrequest");
            _mockRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Batiment>(), It.IsAny<BatimentSansNavigationDTO>()), Times.Never, "La méthode a été appelé alors qu'elle n'aurait pas dû.");
        }

        [TestMethod]
        public async Task PutBatiment_ModelValidated_ReturnsNotFound()
        {
            // Arrange
            BatimentSansNavigationDTO batimentPasTrouve = new ()
            {
                IdBatiment = 3,
                NomBatiment = "Batiment non trouvé"
            };

            // Act
            var actionResult = await _batimentController.PutBatiment(3, batimentPasTrouve);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundObjectResult), "Pas un NotFound");
            _mockRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Batiment>(), It.IsAny<BatimentSansNavigationDTO>()), Times.Never, "La méthode a été appelé alors qu'elle n'aurait pas dû.");
        }

        [TestMethod]
        public async Task DeleteBatimentTest_OK()
        {
            // Arrange
            Batiment Batiment = new ()
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

            _mockRepository.Setup(repo => repo.GetByIdWithoutDTOAsync(1)).ReturnsAsync(Batiment);
            _mockRepository.Setup(repo => repo.DeleteAsync(Batiment));

            // Act
            var actionResult = await _batimentController.DeleteBatiment(1);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult");
            _mockRepository.Verify(repo => repo.DeleteAsync(It.IsAny<Batiment>()), Times.Once, "La méthode n'a pas été appelé alors qu'elle aurait dû.");
        }

        [TestMethod]
        public async Task DeleteBatimentTest_Returns_NotFound()
        {
            // Arrange: Le bâtiment n'existe pas

            // Act
            var actionResult = await _batimentController.DeleteBatiment(1);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundObjectResult), "Pas un NotFoundResult");
            _mockRepository.Verify(repo => repo.DeleteAsync(It.IsAny<Batiment>()), Times.Never, "La méthode a été appelé alors qu'elle n'aurait pas dû.");
        }
    }
}