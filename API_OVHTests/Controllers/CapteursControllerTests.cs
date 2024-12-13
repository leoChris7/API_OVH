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
            List<CapteurDTO> lesCapteurs =
                [
                    new() { IdCapteur = 1, NomCapteur = "Termomètre 3000C", NomSalle = "D101" },
                    new() { IdCapteur = 2, NomCapteur = "Humidiman", NomSalle = "D21002-A" },
                    new() { IdCapteur = 3, NomCapteur = "Takors", NomSalle = "A1020"},
                    new() { IdCapteur = 4, NomCapteur = "Polite", NomSalle = "b1002"},
                    new() { IdCapteur = 5, NomCapteur = "Le Capteur Avec rien", NomSalle="C-300"}
                ];

            _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(lesCapteurs);

            // Act
            var actionResult = await _capteurController.GetCapteurs();

            // Assert
            Assert.IsNotNull(actionResult.Value, "La liste des capteurs est null.");
            Assert.IsInstanceOfType(actionResult.Value, typeof(IEnumerable<CapteurDTO>), "La liste retournée n'est pas une liste de types de capteurs.");
            Assert.AreEqual(5, ((IEnumerable<CapteurDTO>)actionResult.Value).Count(), "Le nombre de capteurs retourné est incorrect.");
            _mockRepository.Verify(repo => repo.GetAllAsync(), Times.Once, "La méthode n'a pas été appelé qu'une fois");
        }

        [TestMethod]
        public async Task GetCapteurs_ReturnsEmptyList_WhenEmpty()
        {
            // Arrange: Liste vide
            List<CapteurDTO> capteurs = [];
            _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(capteurs);

            // Act
            var actionResult = await _capteurController.GetCapteurs();

            // Assert
            Assert.IsNotNull(actionResult.Value, "La liste des capteurs est null.");
            Assert.IsInstanceOfType(actionResult.Value, typeof(List<CapteurDTO>), "La liste retournée n'est pas une liste de types de capteurs.");
            var capteursList = actionResult.Value as List<CapteurDTO>;
            Assert.AreEqual(0, capteursList.Count, "Le nombre de capteurs retourné est incorrect.");
            Assert.IsTrue(!capteursList.Any(), "La liste des capteurs devrait être vide.");
            _mockRepository.Verify(repo => repo.GetAllAsync(), Times.Once, "La méthode n'a pas été appelé qu'une fois");
        }

        [TestMethod]
        public async Task GetCapteurById_Returns_Capteur()
        {
            // Arrange
            CapteurDetailDTO expectedCapteur = new () 
            { 
                IdCapteur = 1, 
                NomCapteur = "CO2 Maximum Detection",
                EstActif = "OUI",
                Mur = new MurSansNavigationDTO
                {
                    IdMur=1,
                    IdDirection=1,
                    Hauteur=1000,
                    IdSalle=1,
                    Longueur=1000,
                    Orientation=300
                },
                Salle = new SalleSansNavigationDTO
                {
                    IdSalle=1,
                    IdBatiment=1,
                    IdTypeSalle=1,
                    NomSalle="D101"
                },
                Unites = new List<UniteDTO>
                {
                    new () {IdUnite=1, NomUnite="cme", SigleUnite="Ce"}
                },
                XCapteur = 100,
                YCapteur = 0,
                ZCapteur = 0
            };

            _mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(expectedCapteur);

            // Act
            var actionResult = await _capteurController.GetCapteurById(1);

            // Assert
            Assert.IsNotNull(actionResult, "GetCapteurById: objet retourné null");
            Assert.IsNotNull(actionResult.Value, "GetCapteurById: valeur retournée null");
            Assert.AreEqual(expectedCapteur, actionResult.Value as CapteurDetailDTO, "GetCapteurById: capteurs non égaux, objet incohérent retourné");
            _mockRepository.Verify(repo => repo.GetByIdAsync(1), Times.Once, "La méthode n'a pas été appelé qu'une fois");
        }

        [TestMethod]
        public async Task GetCapteurById_Returns_NotFound_When_Capteur_NotFound()
        {
            // Arrange: capteur inconnu
            _mockRepository.Setup(repo => repo.GetByIdAsync(0));

            // Act
            var actionResult = await _capteurController.GetCapteurById(0);

            // Assert
            Assert.IsNull(actionResult.Value, "GetCapteurById: objet retourné non null");
            Assert.IsInstanceOfType<NotFoundObjectResult>(actionResult.Result, "GetCapteurById: pas Not Found");
            _mockRepository.Verify(repo => repo.GetByIdAsync(0), Times.Once, "La méthode n'a pas été appelé qu'une fois");
        }

        [TestMethod]
        public async Task GetCapteurByName_Returns_Capteur()
        {
            // Arrange
            CapteurDetailDTO expectedCapteur = new ()
            {
                IdCapteur = 1,
                NomCapteur = "Max Humidico 3000",
                EstActif = "OUI",
                Mur = new MurSansNavigationDTO
                {
                    IdMur = 1,
                    IdDirection = 1,
                    Hauteur = 100,
                    IdSalle = 1,
                    Longueur = 1,
                    Orientation = 100
                },
                Salle = new SalleSansNavigationDTO
                {
                    IdSalle = 1,
                    IdBatiment = 1,
                    IdTypeSalle = 1,
                    NomSalle = "D10101"
                },
                Unites = [
                    new () { IdUnite=1, NomUnite="Ultraviolets", SigleUnite="UV"},
                    new () { IdUnite=2, NomUnite="Temperature", SigleUnite="°C"}
                ],
                XCapteur = 0,
                YCapteur = 0,
                ZCapteur = 0
            };

            _mockRepository.Setup(repo => repo.GetByStringAsync("Max Humidico 3000")).ReturnsAsync(expectedCapteur);

            // Act
            var actionResult = await _capteurController.GetCapteurByName("Max Humidico 3000");

            // Assert
            Assert.IsNotNull(actionResult, "GetCapteurByName: objet retourné null");
            Assert.IsNotNull(actionResult.Value, "GetCapteurByName: valeur retournée null");
            Assert.AreEqual(expectedCapteur, actionResult.Value as CapteurDetailDTO, "GetCapteurByName: capteurs non égaux, objet incohérent retourné");
            _mockRepository.Verify(repo => repo.GetByStringAsync("Max Humidico 3000"), Times.Once, "La méthode n'a pas été appelé qu'une fois");

        }

        [TestMethod]
        public async Task GetCapteurByNameRandomUppercase_Returns_Capteur()
        {
            // Arrange
            CapteurDetailDTO expectedCapteur = new ()
            {
                IdCapteur = 1,
                NomCapteur = "Max Humidico 3000",
                EstActif = "OUI",
                Mur = new MurSansNavigationDTO
                {
                    IdMur = 1,
                    IdDirection = 1,
                    Hauteur = 100,
                    IdSalle = 1,
                    Longueur = 1,
                    Orientation = 100
                },
                Salle = new SalleSansNavigationDTO
                {
                    IdSalle = 1,
                    IdBatiment = 1,
                    IdTypeSalle = 1,
                    NomSalle = "D10101"
                },
                Unites = [
                    new () { IdUnite=1, NomUnite="Ultraviolets", SigleUnite="UV"},
                    new () { IdUnite=2, NomUnite="Temperature", SigleUnite="°C"}
                ],
                XCapteur = 0,
                YCapteur = 0,
                ZCapteur = 0
            };

            _mockRepository.Setup(repo => repo.GetByStringAsync("MAX HumIDico 3000")).ReturnsAsync(expectedCapteur);

            // Act
            var actionResult = await _capteurController.GetCapteurByName("MAX HumIDico 3000");

            // Assert
            Assert.IsNotNull(actionResult, "GetCapteurByName: objet retourné null");
            Assert.IsNotNull(actionResult.Value, "GetCapteurByName: valeur retournée null");
            Assert.AreEqual(expectedCapteur, actionResult.Value as CapteurDetailDTO, "GetCapteurByName: capteurs non égaux, objet incohérent retourné");
            _mockRepository.Verify(repo => repo.GetByStringAsync("MAX HumIDico 3000"), Times.Once, "La méthode n'a pas été appelé qu'une fois");
        }

        [TestMethod]
        public async Task GetCapteurByName_Returns_NotFound_When_Capteur_NotFound()
        {
            // Arrange: capteur avec non inconnu retourne NotFound
            _mockRepository.Setup(repo => repo.GetByStringAsync("Capteur inconnu"));

            // Act
            var actionResult = await _capteurController.GetCapteurByName("Capteur inconnu");

            // Assert
            Assert.IsNull(actionResult.Value, "GetCapteurByName: objet retourné non null");
            Assert.IsInstanceOfType<NotFoundObjectResult>(actionResult.Result, "GetCapteurByName: not found a échoué");
            _mockRepository.Verify(repo => repo.GetByStringAsync("Capteur inconnu"), Times.Once, "La méthode n'a pas été appelé qu'une fois");
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
            Assert.IsTrue(isValid, "La confirmation du modèle crée a échoué.");

            Assert.IsInstanceOfType(result.Value, typeof(CapteurSansNavigationDTO), "PostCapteur: Pas un Capteur");
            Assert.AreEqual(CapteurDTO, (CapteurSansNavigationDTO)result.Value, "PostCapteur: Capteurs non identiques");
            _mockRepository.Verify(repo => repo.AddAsync(CapteurDTO), Times.Once, "La méthode n'a pas été appelé qu'une fois");
        }

        [TestMethod]
        public async Task PostCapteur_ModelInvalid_CapteursPositionsNegative_ReturnsBadRequest()
        {
            // Arrange
            CapteurSansNavigationDTO invalidDto = new ()
            {
                IdCapteur = 1,
                EstActif = "NSP",
                NomCapteur = "NomActif",
                IdMur = 1,
                XCapteur = -9000,
                YCapteur = -8000,
                ZCapteur = -900
            };

            // Simuler une validation réelle en appelant explicitement TryValidateModel si nécessaire
            var validationContext = new ValidationContext(invalidDto);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(invalidDto, validationContext, validationResults, true);

            foreach (var validationResult in validationResults)
            {
                _capteurController.ModelState.AddModelError(validationResult.MemberNames.FirstOrDefault() ?? string.Empty, validationResult.ErrorMessage);
            }

            // Act
            var result = await _capteurController.PostCapteur(invalidDto);

            // Assert : Vérification de la réponse BadRequest
            Assert.IsNull(result.Value);
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));

            // Vérification du nombre d'erreurs dans ModelState
            int expectedErrorCount = 3;
            Assert.AreEqual(expectedErrorCount, validationResults.Count, $"Le nombre d'erreurs de validation attendues est {expectedErrorCount}, mais {validationResults.Count} ont été trouvées.");

            // Vérification du nombre d'exécutions
            _mockRepository.Verify(repo => repo.AddAsync(invalidDto), Times.Never, "La méthode a été appelé alors qu'elle n'aurait pas dû.");
        }

        [TestMethod]
        public async Task PostCapteur_ModelInvalid_CapteursPositionsTooBig_ReturnsBadRequest()
        {
            // Arrange
            CapteurSansNavigationDTO invalidDto = new ()
            {
                IdCapteur = 1,
                EstActif = "NSP",
                NomCapteur = "NomActif",
                IdMur = 1,
                XCapteur = 9999999999,
                YCapteur = 9999999999,
                ZCapteur = 9999999999
            };

            _mockRepository.Setup(repo => repo.AddAsync(invalidDto));

            // Simuler une validation réelle en appelant explicitement TryValidateModel si nécessaire
            var validationContext = new ValidationContext(invalidDto);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(invalidDto, validationContext, validationResults, true);

            foreach (var validationResult in validationResults)
            {
                _capteurController.ModelState.AddModelError(validationResult.MemberNames.FirstOrDefault() ?? string.Empty, validationResult.ErrorMessage);
            }

            // Act
            var result = await _capteurController.PostCapteur(invalidDto);

            // Assert : Vérification de la réponse BadRequest
            Assert.IsNull(result.Value);
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));


            // Vérification du nombre d'erreurs dans ModelState
            int expectedErrorCount = 3; // Ajustez ce nombre en fonction du nombre d'erreurs attendues
            Assert.AreEqual(expectedErrorCount, validationResults.Count, $"Le nombre d'erreurs de validation attendues est {expectedErrorCount}, mais {validationResults.Count} ont été trouvées.");

            _mockRepository.Verify(repo => repo.AddAsync(invalidDto), Times.Never, "La méthode a été appelé alors qu'elle n'aurait pas dû.");
        }

        [TestMethod]
        public async Task PostCapteur_ModelInvalid_EtatInvalideNbLettres_ReturnsBadRequest()
        {
            // Arrange
            CapteurSansNavigationDTO invalidDto = new ()
            {
                IdCapteur = 1,
                EstActif = "AB",
                NomCapteur = "NomActif",
                IdMur = 1,
                XCapteur = 120,
                YCapteur = 500,
                ZCapteur = 0
            };

            _mockRepository.Setup(repo => repo.AddAsync(invalidDto));

            // Simuler une validation réelle en appelant explicitement TryValidateModel si nécessaire
            var validationContext = new ValidationContext(invalidDto);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(invalidDto, validationContext, validationResults, true);

            foreach (var validationResult in validationResults)
            {
                _capteurController.ModelState.AddModelError(validationResult.MemberNames.FirstOrDefault() ?? string.Empty, validationResult.ErrorMessage);
            }

            // Act
            var result = await _capteurController.PostCapteur(invalidDto);

            // Assert : Vérification de la réponse BadRequest
            Assert.IsNull(result.Value);
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));


            // Vérification du nombre d'erreurs dans ModelState
            int expectedErrorCount = 2;
            Assert.AreEqual(expectedErrorCount, validationResults.Count, $"Le nombre d'erreurs de validation attendues est {expectedErrorCount}, mais {validationResults.Count} ont été trouvées.");

            _mockRepository.Verify(repo => repo.AddAsync(invalidDto), Times.Never, "La méthode a été appelé alors qu'elle n'aurait pas dû.");
        }

        [TestMethod]
        public async Task PostCapteur_ModelInvalid_EtatInvalideCheck_ReturnsBadRequest()
        {
            // Arrange
            CapteurSansNavigationDTO invalidDto = new ()
            {
                IdCapteur = 1,
                EstActif = "NOO",
                NomCapteur = "NomActif",
                IdMur = 1,
                XCapteur = 120,
                YCapteur = 500,
                ZCapteur = 0
            };

            _mockRepository.Setup(repo => repo.AddAsync(invalidDto));

            // Simuler une validation réelle en appelant explicitement TryValidateModel si nécessaire
            var validationContext = new ValidationContext(invalidDto);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(invalidDto, validationContext, validationResults, true);

            foreach (var validationResult in validationResults)
            {
                _capteurController.ModelState.AddModelError(validationResult.MemberNames.FirstOrDefault() ?? string.Empty, validationResult.ErrorMessage);
            }

            // Act
            var result = await _capteurController.PostCapteur(invalidDto);

            // Assert : Vérification de la réponse BadRequest
            Assert.IsNull(result.Value);
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));


            // Vérification du nombre d'erreurs dans ModelState
            int expectedErrorCount = 1;
            Assert.AreEqual(expectedErrorCount, validationResults.Count, $"Le nombre d'erreurs de validation attendues est {expectedErrorCount}, mais {validationResults.Count} ont été trouvées.");

            _mockRepository.Verify(repo => repo.AddAsync(invalidDto), Times.Never, "La méthode a été appelé alors qu'elle n'aurait pas dû.");
        }

        [TestMethod]
        public async Task PostCapteur_ModelInvalid_NomCapteurTropLong_ReturnsBadRequest()
        {
            // Arrange
            CapteurSansNavigationDTO invalidDto = new ()
            {
                IdCapteur = 1,
                EstActif = "OUI",
                NomCapteur = "Nom Capteur Beaucoup Trop Long, Invalide",
                IdMur = 1,
                XCapteur = 120,
                YCapteur = 500,
                ZCapteur = 0
            };

            _mockRepository.Setup(repo => repo.AddAsync(invalidDto));

            // Simuler une validation réelle en appelant explicitement TryValidateModel si nécessaire
            var validationContext = new ValidationContext(invalidDto);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(invalidDto, validationContext, validationResults, true);

            foreach (var validationResult in validationResults)
            {
                _capteurController.ModelState.AddModelError(validationResult.MemberNames.FirstOrDefault() ?? string.Empty, validationResult.ErrorMessage);
            }

            // Act
            var result = await _capteurController.PostCapteur(invalidDto);

            // Assert : Vérification de la réponse BadRequest
            Assert.IsNull(result.Value);
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));


            // Vérification du nombre d'erreurs dans ModelState
            int expectedErrorCount = 1;
            Assert.AreEqual(expectedErrorCount, validationResults.Count, $"Le nombre d'erreurs de validation attendues est {expectedErrorCount}, mais {validationResults.Count} ont été trouvées.");

            _mockRepository.Verify(repo => repo.AddAsync(invalidDto), Times.Never, "La méthode a été appelé alors qu'elle n'aurait pas dû.");
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

            _mockRepository.Setup(repo => repo.GetByIdWithoutDTOAsync(1)).ReturnsAsync(Capteur);
            _mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(new CapteurDetailDTO { IdCapteur = Capteur.IdCapteur , EstActif = newCapteur.EstActif, XCapteur = Capteur.XCapteur, YCapteur = Capteur.YCapteur, ZCapteur = Capteur.ZCapteur, NomCapteur = Capteur.NomCapteur, Mur=new MurSansNavigationDTO{IdMur=1}, Salle = new SalleSansNavigationDTO(), Unites = new List<UniteDTO>() });
            _mockRepository.Setup(repo => repo.UpdateAsync(Capteur, newCapteur));

            // Act
            var actionResult = await _capteurController.PutCapteur(newCapteur.IdCapteur, newCapteur);

            // Assert
            var changedObject = await _capteurController.GetCapteurById(1);
            Assert.IsNotNull(actionResult);
            Assert.AreEqual(changedObject.Value.IdCapteur, newCapteur.IdCapteur, "Id de l'objet incohérent après changement");
            Assert.AreEqual(changedObject.Value.NomCapteur, newCapteur.NomCapteur, "Nom de l'objet incohérent après changement");
            Assert.AreEqual(changedObject.Value.XCapteur, newCapteur.XCapteur, "Position X de l'objet incohérent après changement");
            Assert.AreEqual(changedObject.Value.YCapteur, newCapteur.YCapteur, "Position Y de l'objet incohérent après changement");
            Assert.AreEqual(changedObject.Value.ZCapteur, newCapteur.ZCapteur, "Position Z de l'objet incohérent après changement");
            Assert.AreEqual(changedObject.Value.Mur.IdMur, newCapteur.IdMur, "Id du mur de l'objet incohérent après changement");
            Assert.AreEqual(changedObject.Value.EstActif, newCapteur.EstActif, "Etat de l'objet incohérent après changement");
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult");
            _mockRepository.Verify(repo => repo.UpdateAsync(Capteur, newCapteur), Times.Once, "La méthode n'a pas été appelé alors qu'elle aurait dû.");
        }

        [TestMethod]
        public async Task PutCapteur_ModelValidated_ReturnsBadRequest()
        {
            // Arrange
            CapteurSansNavigationDTO capteurBadRequest = new ()
            {
                IdCapteur = 1,
                NomCapteur = "Type échoué",
                EstActif = "NSP",
                IdMur = 2,
                XCapteur = 10,
                YCapteur = 10
            };

            // Act
            var actionResult = await _capteurController.PutCapteur(3, capteurBadRequest);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestObjectResult), "Pas un Badrequest");
            _mockRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Capteur>(), It.IsAny<CapteurSansNavigationDTO>()), Times.Never, "La méthode a été appelé alors qu'elle n'aurait pas dû.");
        }

        [TestMethod]
        public async Task PutCapteur_ModelValidated_ReturnsNotFound()
        {
            // Arrange
            CapteurSansNavigationDTO capteurNonTrouve = new()
            {
                IdCapteur = 3,
                NomCapteur = "Type non trouvé",
                EstActif = "NSP",
                IdMur = 2,
                XCapteur = 10,
                YCapteur = 10
            };

            // Act
            var actionResult = await _capteurController.PutCapteur(3, capteurNonTrouve);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundObjectResult), "Pas un NotFound");
            _mockRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Capteur>(), It.IsAny<CapteurSansNavigationDTO>()), Times.Never, "La méthode a été appelé alors qu'elle n'aurait pas dû.");
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

            _mockRepository.Setup(repo => repo.GetByIdWithoutDTOAsync(1)).ReturnsAsync(Capteur);
            _mockRepository.Setup(repo => repo.DeleteAsync(Capteur));

            // Act
            var actionResult = await _capteurController.DeleteCapteur(1);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult");
            _mockRepository.Verify(repo => repo.DeleteAsync(It.IsAny<Capteur>()), Times.Once, "La méthode n'a pas été appelé alors qu'elle aurait dû.");
        }

        [TestMethod]
        public async Task DeleteCapteurTest_Returns_NotFound()
        {
            // Arrange: Le capteur n'existe pas

            // Act
            var actionResult = await _capteurController.DeleteCapteur(1);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundObjectResult), "Pas un NotFoundResult");
            _mockRepository.Verify(repo => repo.DeleteAsync(It.IsAny<Capteur>()), Times.Never, "La méthode a été appelé alors qu'elle n'aurait pas dû.");
        }
    }
}