using Moq;
using API_OVH.Controllers;
using API_OVH.Models.DTO;
using API_OVH.Models.EntityFramework;
using API_OVH.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

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
                    new() { IdCapteur = 2, NomCapteur = "Humidiman", NomSalle = "D21002-A" },
                    new() { IdCapteur = 3, NomCapteur = "Takors", NomSalle = "A1020"},
                    new() { IdCapteur = 4, NomCapteur = "Polite", NomSalle = "b1002"},
                    new() { IdCapteur = 5, NomCapteur = "Le Capteur Avec rien", NomSalle="C-300"}
                };

            _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(lesCapteurs);

            // Act
            var actionResult = await _capteurController.GetCapteurs();

            // Assert
            Assert.IsNotNull(actionResult.Value, "La liste des capteurs est null.");
            Assert.IsInstanceOfType(actionResult.Value, typeof(IEnumerable<CapteurDTO>), "La liste retournée n'est pas une liste de types de capteurs.");
            Assert.AreEqual(5, ((IEnumerable<CapteurDTO>)actionResult.Value).Count(), "Le nombre de capteurs retourné est incorrect.");
        }

        [TestMethod]
        public async Task GetCapteurs_ReturnsEmptyList_WhenEmpty()
        {
            // Arrange
            List<CapteurDTO> capteurs = new List<CapteurDTO>();
            _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(capteurs);

            // Act
            var actionResult = await _capteurController.GetCapteurs();

            // Assert
            Assert.IsNotNull(actionResult.Value, "La liste des capteurs est null.");
            Assert.IsInstanceOfType(actionResult.Value, typeof(List<CapteurDTO>), "La liste retournée n'est pas une liste de types de capteurs.");
            var capteursList = actionResult.Value as List<CapteurDTO>;
            Assert.AreEqual(0, capteursList.Count, "Le nombre de capteurs retourné est incorrect.");
            Assert.IsTrue(!capteursList.Any(), "La liste des capteurs devrait être vide.");
        }

        [TestMethod]
        public async Task GetCapteurById_Returns_Capteur()
        {
            // Arrange
            var expectedCapteur = new CapteurDetailDTO 
            { 
                IdCapteur = 1, 
                NomCapteur = "CO2 Maximum Detection",
                EstActif = "OUI",
                XCapteur = 0,
                YCapteur = 0,
                ZCapteur = 0
            };

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
                EstActif = "OUI",
                XCapteur = 0,
                YCapteur = 0,
                ZCapteur = 0
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
        public async Task GetCapteurByNameRandomUppercase_Returns_Capteur()
        {
            // Arrange
            var expectedCapteur = new CapteurDetailDTO
            {
                IdCapteur = 1,
                NomCapteur = "Max Humidico 3000",
                EstActif = "OUI",
                XCapteur = 0,
                YCapteur = 0,
                ZCapteur = 0
            };

            _mockRepository.Setup(x => x.GetByStringAsync("MAX HumIDico 3000")).ReturnsAsync(expectedCapteur);

            // Act
            var actionResult = await _capteurController.GetCapteurByName("MAX HumIDico 3000");

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
            CapteurSansNavigationDTO CapteurDTO = new ()
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

            // Simuler la validation du modèle ajouté manuellement
            _capteurController.ModelState.Clear();
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(result.Value);
            bool isValid = Validator.TryValidateObject(result.Value, validationContext, validationResults, true);
            Assert.IsTrue(isValid, validationContext + " + " + validationResults + " => " + isValid);

            Assert.IsInstanceOfType(result.Value, typeof(CapteurSansNavigationDTO), "PostCapteur: Pas un Capteur");
            Assert.AreEqual(CapteurDTO, (CapteurSansNavigationDTO)result.Value, "PostCapteur: Capteurs non identiques");
        }

        [TestMethod]
        public async Task PostCapteur_ModelInvalid_CapteursPositionsNegative_ReturnsBadRequest()
        {
            // Arrange
            var invalidDto = new CapteurSansNavigationDTO
            {
                IdCapteur = 1,
                EstActif = "NSP",
                NomCapteur = "NomActif",
                IdMur = 1,
                XCapteur = -9000,
                YCapteur = -8000,
                ZCapteur = -900
            };

            // Ajout manuel d'une erreur de validation pour le champ "NomCapteur"
            _capteurController.ModelState.AddModelError("XCapteur", "XCapteur ne peut pas être négatif");

            // Act
            var result = await _capteurController.PostCapteur(invalidDto);

            // Assert : Vérification de la réponse BadRequest
            Assert.IsNull(result.Value);
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));

            // Vérification des erreurs de validation du modèle
            _capteurController.ModelState.Clear();
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(invalidDto);
            bool isValid = Validator.TryValidateObject(invalidDto, validationContext, validationResults, true);

            // Assert : Le modèle n'est pas valide
            Assert.IsFalse(isValid, $"Le modèle doit être invalide. Validation Results: {string.Join(", ", validationResults.Select(v => v.ErrorMessage))}");

            // Vérification du nombre d'erreurs dans ModelState
            int expectedErrorCount = 3;
            Assert.AreEqual(expectedErrorCount, validationResults.Count, $"Le nombre d'erreurs de validation attendues est {expectedErrorCount}, mais {validationResults.Count} ont été trouvées.");

            // Vérification du message d'erreur spécifique si nécessaire
            // Assert.IsTrue(validationResults.Any(v => v.ErrorMessage.Contains("NomCapteur est requis.")), "L'erreur 'NomCapteur est requis.' n'a pas été trouvée.");
        }

        [TestMethod]
        public async Task PostCapteur_ModelInvalid_CapteursPositionsTooBig_ReturnsBadRequest()
        {
            // Arrange
            var invalidDto = new CapteurSansNavigationDTO
            {
                IdCapteur = 1,
                EstActif = "NSP",
                NomCapteur = "NomActif",
                IdMur = 1,
                XCapteur = 9999999999,
                YCapteur = 9999999999,
                ZCapteur = 9999999999
            };

            // Ajout manuel d'une erreur de validation pour le champ "NomCapteur"
            _capteurController.ModelState.AddModelError("NomCapteur", "NomCapteur est requis.");

            // Act
            var result = await _capteurController.PostCapteur(invalidDto);

            // Assert : Vérification de la réponse BadRequest
            Assert.IsNull(result.Value);
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));

            // Vérification des erreurs de validation du modèle
            _capteurController.ModelState.Clear();
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(invalidDto);
            bool isValid = Validator.TryValidateObject(invalidDto, validationContext, validationResults, true);

            // Assert : Le modèle n'est pas valide
            Assert.IsFalse(isValid, $"Le modèle doit être invalide. Validation Results: {string.Join(", ", validationResults.Select(v => v.ErrorMessage))}");

            // Vérification du nombre d'erreurs dans ModelState
            int expectedErrorCount = 3; // Ajustez ce nombre en fonction du nombre d'erreurs attendues
            Assert.AreEqual(expectedErrorCount, validationResults.Count, $"Le nombre d'erreurs de validation attendues est {expectedErrorCount}, mais {validationResults.Count} ont été trouvées.");

            // Vérification du message d'erreur spécifique si nécessaire
            // Assert.IsTrue(validationResults.Any(v => v.ErrorMessage.Contains("NomCapteur est requis.")), "L'erreur 'NomCapteur est requis.' n'a pas été trouvée.");
        }


        [TestMethod]
        public async Task PutCapteur_ModelValidated_UpdateOK()
        {
            // Arrange
            Capteur Capteur = new ()
            {
                IdCapteur = 1,
                NomCapteur = "Aotec 6-1",
                EstActif = "OUI",
                IdMur = 1,
                XCapteur = 10,
                YCapteur = 10,
                ZCapteur = 0
            };

            CapteurSansNavigationDTO newCapteur = new ()
            {
                IdCapteur = 1,
                NomCapteur = "Aotec 6-1",
                EstActif = "NON",
                IdMur = 1,
                XCapteur = 10,
                YCapteur = 10,
                ZCapteur = 0
            };

            _mockRepository.Setup(x => x.GetByIdWithoutDTOAsync(1)).ReturnsAsync(Capteur);

            // Act
            var actionResult = await _capteurController.PutCapteur(newCapteur.IdCapteur, newCapteur);

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult");
        }

        [TestMethod]
        public async Task PutCapteur_ModelValidated_ReturnsBadRequest()
        {
            // Act
            var actionResult = await _capteurController.PutCapteur(3, new CapteurSansNavigationDTO
            {
                IdCapteur = 1,
                NomCapteur = "Type échoué",
                EstActif = "NSP",
                IdMur = 2,
                XCapteur = 10,
                YCapteur = 10
            });

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestObjectResult), "Pas un Badrequest");
        }

        [TestMethod]
        public async Task PutCapteur_ModelValidated_ReturnsNotFound()
        {
            // Act
            var actionResult = await _capteurController.PutCapteur(3, new CapteurSansNavigationDTO
            {
                IdCapteur = 3,
                NomCapteur = "Type non trouvé",
                EstActif = "NSP",
                IdMur = 2,
                XCapteur = 10,
                YCapteur = 10
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
                NomCapteur = "CO2 Max",
                EstActif = "NSP",
                XCapteur = 100,
                YCapteur = 90000,
                ZCapteur = 0,
                IdMur = 10
            };

            // Simuler la validation du modèle manuellement
            _capteurController.ModelState.Clear();
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(Capteur);
            bool isValid = Validator.TryValidateObject(Capteur, validationContext, validationResults, true);
            Assert.IsTrue(isValid, validationContext + " + " + validationResults + " => " + isValid);

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