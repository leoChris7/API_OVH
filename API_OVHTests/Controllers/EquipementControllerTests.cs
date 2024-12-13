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
            List<EquipementDTO> equipementsDTO = [
                    new () { IdEquipement = 1, NomEquipement = "Fenêtre" },
                    new () { IdEquipement = 2, NomEquipement = "Bureau" }
                ];

            _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(equipementsDTO);

            // Act
            var actionResult = await _equipementController.GetEquipements();

            // Assert
            Assert.IsNotNull(actionResult.Value, "La liste des equipements est null.");
            Assert.IsInstanceOfType(actionResult.Value, typeof(IEnumerable<EquipementDTO>), "La liste retournée n'est pas une liste d'equipement.");
            Assert.AreEqual(2, ((IEnumerable<EquipementDTO>)actionResult.Value).Count(), "Le nombre d'equipements retourné est incorrect.");
            _mockRepository.Verify(repo => repo.GetAllAsync(), Times.Once, "La méthode n'a pas été appelé qu'une fois");
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
            Assert.IsNotNull(actionResult.Value, "La liste des équipements est null.");
            Assert.IsInstanceOfType(actionResult.Value, typeof(List<EquipementDTO>), "La liste retournée n'est pas une liste d'équipements.");
            var EquipementsList = actionResult.Value as List<EquipementDTO>;
            Assert.AreEqual(0, EquipementsList.Count, "Le nombre de équipements retourné est incorrect.");
            Assert.IsTrue(!EquipementsList.Any(), "La liste des équipements devrait être vide.");
            _mockRepository.Verify(repo => repo.GetAllAsync(), Times.Once, "La méthode n'a pas été appelé qu'une fois");
        }

        [TestMethod]
        public async Task GetEquipementById_Returns_Equipement()
        {
            // Arrange
            EquipementDetailDTO expectedEquipement = new ()
            {
                IdEquipement = 1,
                NomEquipement = "Bureau",
                EstActif = "NSP",
                PositionX = 0,
                PositionY = 100,
                PositionZ = 20,
                Salle = new SalleSansNavigationDTO
                {
                    IdBatiment=2,
                    IdSalle=2,
                    IdTypeSalle=3,
                    NomSalle="Lal"
                },
                TypeEquipement= new TypeEquipementDTO
                {
                    IdTypeEquipement=5,
                    NomTypeEquipement="Immobilier bois"
                }
            };

            _mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(expectedEquipement);

            // Act
            var actionResult = await _equipementController.GetEquipementById(1);

            // Assert
            Assert.IsNotNull(actionResult, "GetEquipementById: objet retourné null");
            Assert.IsNotNull(actionResult.Value, "GetEquipementById: valeur retournée null");
            Assert.AreEqual(expectedEquipement, actionResult.Value as EquipementDetailDTO, "GetEquipementById: Equipements non égaux, objet incohérent retourné");
            _mockRepository.Verify(repo => repo.GetByIdAsync(1), Times.Once, "La méthode n'a pas été appelé qu'une fois");
        }

        [TestMethod]
        public async Task GetEquipementById_Returns_NotFound_When_Equipement_NotFound()
        {
            // Arrange: équipement n'existe pas
            _mockRepository.Setup(repo => repo.GetByIdAsync(0));

            // Act
            var actionResult = await _equipementController.GetEquipementById(0);

            // Assert
            Assert.IsNull(actionResult.Value, "GetEquipementById: objet retourné non null");
            Assert.IsInstanceOfType<NotFoundObjectResult>(actionResult.Result, "GetEquipementById: pas Not Found");
            _mockRepository.Verify(repo => repo.GetByIdAsync(0), Times.Once, "La méthode n'a pas été appelé qu'une fois");
        }

        [TestMethod]
        public async Task GetEquipementByName_Returns_Equipement()
        {
            // Arrange
            EquipementDetailDTO expectedEquipement = new()
            {
                IdEquipement = 1,
                NomEquipement = "Radiateur",
                EstActif = "OUI",
                TypeEquipement = new TypeEquipementDTO
                {
                    IdTypeEquipement = 10,
                    NomTypeEquipement = "Chauffant"
                }
            };

            _mockRepository.Setup(repo => repo.GetByStringAsync("Radiateur")).ReturnsAsync(expectedEquipement);

            // Act
            var actionResult = await _equipementController.GetEquipementByString("Radiateur");

            // Assert
            Assert.IsNotNull(actionResult, "Objet retourné null");
            Assert.IsNotNull(actionResult.Value, "Valeur retournée null");
            Assert.AreEqual(expectedEquipement, actionResult.Value as EquipementDetailDTO, "Equipements non égaux, objet incohérent retourné");
            _mockRepository.Verify(repo => repo.GetByStringAsync("Radiateur"), Times.Once, "La méthode n'a pas été appelé qu'une fois");
        }

        [TestMethod]
        public async Task GetEquipementByNameRandomUppercase_Returns_Equipement()
        {
            // Arrange
            EquipementDetailDTO expectedEquipement = new () { 
                IdEquipement = 1, 
                NomEquipement = "Radiateur",
                EstActif = "OUI",
                PositionX = 1000,
                PositionY  = 0,
                PositionZ = 100,
                Salle = new SalleSansNavigationDTO
                {
                    IdSalle=5,
                    NomSalle="AEIOU",
                    IdBatiment=100,
                    IdTypeSalle=3
                }
            };

            _mockRepository.Setup(repo => repo.GetByStringAsync("rADIATEUR")).ReturnsAsync(expectedEquipement);

            // Act
            var actionResult = await _equipementController.GetEquipementByString("rADIATEUR");

            // Assert
            Assert.IsNotNull(actionResult, "Objet retourné null");
            Assert.IsNotNull(actionResult.Value, "Valeur retournée null");
            Assert.AreEqual(expectedEquipement, actionResult.Value as EquipementDetailDTO, "Equipements non égaux, objet incohérent retourné");
            _mockRepository.Verify(repo => repo.GetByStringAsync("rADIATEUR"), Times.Once, "La méthode n'a pas été appelé qu'une fois");
        }


        [TestMethod]
        public async Task GetEquipementByString_Returns_NotFound_When_Equipement_NotFound()
        {
            // Arrange: Equipement Non Existant
            String? nomEquipementInexistant = "Equipement inconnu";
            _mockRepository.Setup(repo => repo.GetByStringAsync(nomEquipementInexistant));

            // Act
            var actionResult = await _equipementController.GetEquipementByString(nomEquipementInexistant);

            // Assert
            Assert.IsNull(actionResult.Value, "GetEquipementByString: objet retourné non null");
            Assert.IsInstanceOfType<NotFoundObjectResult>(actionResult.Result, "GetEquipementByString: not found a échoué");
            _mockRepository.Verify(repo => repo.GetByStringAsync(nomEquipementInexistant), Times.Once, "La méthode n'a pas été appelé qu'une fois");
        }

        [TestMethod]
        public async Task PostEquipementDTO_ModelValidated_CreationOK()
        {
            // Arrange
            EquipementSansNavigationDTO EquipementAjoute = new()
            {
                IdEquipement = 1,
                NomEquipement = "Bureau",
                Hauteur = 800,
                Longueur = 100,
                EstActif = "NSP"
            };

            _mockRepository.Setup(repo => repo.AddAsync(EquipementAjoute));

            // Act
            var actionResult = await _equipementController.PostEquipement(EquipementAjoute);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<EquipementSansNavigationDTO>), "PostEquipement: Pas un ActionResult<EquipementSansNavigation>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "PostEquipement: Pas un CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;

            // Simuler la validation du modèle ajouté manuellement
            _equipementController.ModelState.Clear();
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(result.Value);
            bool isValid = Validator.TryValidateObject(result.Value, validationContext, validationResults, true);
            Assert.IsTrue(isValid, "La confirmation du modèle crée a échoué.");

            Assert.IsInstanceOfType(result.Value, typeof(EquipementSansNavigationDTO), "PostEquipement: Pas un EquipementSansNavigation");
            Assert.AreEqual(EquipementAjoute, (EquipementSansNavigationDTO)result.Value, "PostEquipement: Equipements non identiques");

            _mockRepository.Verify(repo => repo.AddAsync(It.IsAny<EquipementSansNavigationDTO>()), Times.Once, "La méthode n'a pas été appelé alors qu'elle aurait dû.");
        }

        [TestMethod]
        public async Task PostEquipement_ModelInvalid_PositionsNegatives_ReturnsBadRequest()
        {
            // Arrange
            EquipementSansNavigationDTO invalidDto = new ()
            {
                IdEquipement = 1,
                NomEquipement = "Nom",
                EstActif = "NSP",
                Hauteur = 1000,
                Largeur = 100,
                Longueur = 1020,
                XEquipement = -500,
                YEquipement = -8000,
                ZEquipement = -70
            };
            _mockRepository.Setup(repo => repo.AddAsync(invalidDto));

            // Simuler une validation réelle en appelant explicitement TryValidateModel si nécessaire
            var validationContext = new ValidationContext(invalidDto);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(invalidDto, validationContext, validationResults, true);

            foreach (var validationResult in validationResults)
            {
                _equipementController.ModelState.AddModelError(validationResult.MemberNames.FirstOrDefault() ?? string.Empty, validationResult.ErrorMessage);
            }

            // Act
            var result = await _equipementController.PostEquipement(invalidDto);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult), "Pas un bad request");

            // Vérification du nombre d'erreurs dans ModelState
            Assert.AreEqual(3, validationResults.Count, $"Le nombre d'erreurs de validation attendues est 3, mais {validationResults.Count} ont été trouvées.");

            _mockRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Equipement>(), It.IsAny<EquipementSansNavigationDTO>()), Times.Never, "La méthode a été appelé alors qu'elle n'aurait pas dû.");
        }

        [TestMethod]
        public async Task PostEquipement_ModelInvalid_PositionsTropGrande_ReturnsBadRequest()
        {
            // Arrange
            EquipementSansNavigationDTO invalidDto = new()
            {
                IdEquipement = 1,
                NomEquipement = "Nom",
                EstActif = "NSP",
                Hauteur = 1000,
                Largeur = 100,
                Longueur = 1020,
                XEquipement = 9999999999,
                YEquipement = 9999999999,
                ZEquipement = 9999999999
            };
            _mockRepository.Setup(repo => repo.AddAsync(invalidDto));

            // Simuler une validation réelle en appelant explicitement TryValidateModel si nécessaire
            var validationContext = new ValidationContext(invalidDto);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(invalidDto, validationContext, validationResults, true);

            foreach (var validationResult in validationResults)
            {
                _equipementController.ModelState.AddModelError(validationResult.MemberNames.FirstOrDefault() ?? string.Empty, validationResult.ErrorMessage);
            }

            // Act
            var result = await _equipementController.PostEquipement(invalidDto);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult), "Pas un bad request");

            // Vérification du nombre d'erreurs dans ModelState
            Assert.AreEqual(3, validationResults.Count, $"Le nombre d'erreurs de validation attendues est 3, mais {validationResults.Count} ont été trouvées.");

            _mockRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Equipement>(), It.IsAny<EquipementSansNavigationDTO>()), Times.Never, "La méthode a été appelé alors qu'elle n'aurait pas dû.");
        }

        [TestMethod]
        public async Task PostEquipement_ModelInvalid_TailleNegative_ReturnsBadRequest()
        {
            // Arrange
            EquipementSansNavigationDTO invalidDto = new()
            {
                IdEquipement = 1,
                NomEquipement = "Nom",
                EstActif = "NSP",
                Hauteur = -800,
                Largeur = -50,
                Longueur = -500,
                XEquipement = 50,
                YEquipement = 90,
                ZEquipement = 40
            };
            _mockRepository.Setup(repo => repo.AddAsync(invalidDto));

            // Simuler une validation réelle en appelant explicitement TryValidateModel si nécessaire
            var validationContext = new ValidationContext(invalidDto);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(invalidDto, validationContext, validationResults, true);

            foreach (var validationResult in validationResults)
            {
                _equipementController.ModelState.AddModelError(validationResult.MemberNames.FirstOrDefault() ?? string.Empty, validationResult.ErrorMessage);
            }

            // Act
            var result = await _equipementController.PostEquipement(invalidDto);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult), "Pas un bad request");

            // Vérification du nombre d'erreurs dans ModelState
            Assert.AreEqual(3, validationResults.Count, $"Le nombre d'erreurs de validation attendues est 3, mais {validationResults.Count} ont été trouvées.");

            _mockRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Equipement>(), It.IsAny<EquipementSansNavigationDTO>()), Times.Never, "La méthode a été appelé alors qu'elle n'aurait pas dû.");
        }

        [TestMethod]
        public async Task PostEquipement_ModelInvalid_TailleTropGrande_ReturnsBadRequest()
        {
            // Arrange
            EquipementSansNavigationDTO invalidDto = new()
            {
                IdEquipement = 1,
                NomEquipement = "Nom",
                EstActif = "NSP",
                Hauteur = 9999999999,
                Largeur = 9999999999,
                Longueur = 9999999999,
                XEquipement = 50,
                YEquipement = 90,
                ZEquipement = 40
            };
            _mockRepository.Setup(repo => repo.AddAsync(invalidDto));

            // Simuler une validation réelle en appelant explicitement TryValidateModel si nécessaire
            var validationContext = new ValidationContext(invalidDto);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(invalidDto, validationContext, validationResults, true);

            foreach (var validationResult in validationResults)
            {
                _equipementController.ModelState.AddModelError(validationResult.MemberNames.FirstOrDefault() ?? string.Empty, validationResult.ErrorMessage);
            }

            // Act
            var result = await _equipementController.PostEquipement(invalidDto);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult), "Pas un bad request");

            // Vérification du nombre d'erreurs dans ModelState
            Assert.AreEqual(3, validationResults.Count, $"Le nombre d'erreurs de validation attendues est 3, mais {validationResults.Count} ont été trouvées.");

            _mockRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Equipement>(), It.IsAny<EquipementSansNavigationDTO>()), Times.Never, "La méthode a été appelé alors qu'elle n'aurait pas dû.");
        }

        [TestMethod]
        public async Task PostEquipement_ModelInvalid_EtatTropCourt_ReturnsBadRequest()
        {
            // Arrange
            EquipementSansNavigationDTO invalidDto = new()
            {
                IdEquipement = 1,
                NomEquipement = "Nom",
                EstActif = "NA",
                Hauteur = 150,
                Largeur = 150,
                Longueur = 150,
                XEquipement = 50,
                YEquipement = 90,
                ZEquipement = 40
            };
            _mockRepository.Setup(repo => repo.AddAsync(invalidDto));

            // Simuler une validation réelle en appelant explicitement TryValidateModel si nécessaire
            var validationContext = new ValidationContext(invalidDto);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(invalidDto, validationContext, validationResults, true);

            foreach (var validationResult in validationResults)
            {
                _equipementController.ModelState.AddModelError(validationResult.MemberNames.FirstOrDefault() ?? string.Empty, validationResult.ErrorMessage);
            }

            // Act
            var result = await _equipementController.PostEquipement(invalidDto);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult), "Pas un bad request");

            // Vérification du nombre d'erreurs dans ModelState
            Assert.AreEqual(2, validationResults.Count, $"Le nombre d'erreurs de validation attendues est 2, mais {validationResults.Count} ont été trouvées.");

            _mockRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Equipement>(), It.IsAny<EquipementSansNavigationDTO>()), Times.Never, "La méthode a été appelé alors qu'elle n'aurait pas dû.");
        }

        [TestMethod]
        public async Task PostEquipement_ModelInvalid_EtatCheckInvalide_ReturnsBadRequest()
        {
            // Arrange
            EquipementSansNavigationDTO invalidDto = new()
            {
                IdEquipement = 1,
                NomEquipement = "Nom",
                EstActif = "NAN",
                Hauteur = 150,
                Largeur = 150,
                Longueur = 150,
                XEquipement = 50,
                YEquipement = 90,
                ZEquipement = 40
            };
            _mockRepository.Setup(repo => repo.AddAsync(invalidDto));

            // Simuler une validation réelle en appelant explicitement TryValidateModel si nécessaire
            var validationContext = new ValidationContext(invalidDto);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(invalidDto, validationContext, validationResults, true);

            foreach (var validationResult in validationResults)
            {
                _equipementController.ModelState.AddModelError(validationResult.MemberNames.FirstOrDefault() ?? string.Empty, validationResult.ErrorMessage);
            }

            // Act
            var result = await _equipementController.PostEquipement(invalidDto);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult), "Pas un bad request");

            // Vérification du nombre d'erreurs dans ModelState
            Assert.AreEqual(1, validationResults.Count, $"Le nombre d'erreurs de validation attendues est 1, mais {validationResults.Count} ont été trouvées.");

            _mockRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Equipement>(), It.IsAny<EquipementSansNavigationDTO>()), Times.Never, "La méthode a été appelé alors qu'elle n'aurait pas dû.");
        }

        [TestMethod]
        public async Task PostEquipement_ModelInvalid_NomEquipementTropLong_ReturnsBadRequest()
        {
            // Arrange
            EquipementSansNavigationDTO invalidDto = new()
            {
                IdEquipement = 1,
                NomEquipement = "Le Nom Qui Exagere En Terme De Longueur",
                EstActif = "NSP",
                Hauteur = 150,
                Largeur = 150,
                Longueur = 150,
                XEquipement = 50,
                YEquipement = 90,
                ZEquipement = 40
            };
            _mockRepository.Setup(repo => repo.AddAsync(invalidDto));

            // Simuler une validation réelle en appelant explicitement TryValidateModel si nécessaire
            var validationContext = new ValidationContext(invalidDto);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(invalidDto, validationContext, validationResults, true);

            foreach (var validationResult in validationResults)
            {
                _equipementController.ModelState.AddModelError(validationResult.MemberNames.FirstOrDefault() ?? string.Empty, validationResult.ErrorMessage);
            }

            // Act
            var result = await _equipementController.PostEquipement(invalidDto);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult), "Pas un bad request");

            // Vérification du nombre d'erreurs dans ModelState
            Assert.AreEqual(1, validationResults.Count, $"Le nombre d'erreurs de validation attendues est 1, mais {validationResults.Count} ont été trouvées.");

            _mockRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Equipement>(), It.IsAny<EquipementSansNavigationDTO>()), Times.Never, "La méthode a été appelé alors qu'elle n'aurait pas dû.");
        }

        [TestMethod]
        public async Task PutEquipement_ModelValidated_UpdateOK()
        {
            // Arrange
            Equipement Equipement = new ()
            {
                IdEquipement = 1,
                Hauteur = 50,
                Longueur = 1,
                Largeur = 1000,
                EstActif = "OUI",
                XEquipement = 100
            };

            EquipementSansNavigationDTO newEquipement = new ()
            {
                IdEquipement = 1,
                Hauteur = 1020,
                Longueur = 3000,
                Largeur = 1000,
                EstActif = "OUI",
                XEquipement = 100
            };

            _mockRepository.Setup(repo => repo.GetByIdWithoutDTOAsync(1)).ReturnsAsync(Equipement);
            _mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(new EquipementDetailDTO
            {
                IdEquipement = newEquipement.IdEquipement,
                NomEquipement = newEquipement.NomEquipement,
                EstActif = newEquipement.EstActif,
                PositionX = newEquipement.XEquipement,
                PositionY = newEquipement.YEquipement,
                PositionZ = newEquipement.ZEquipement,
                Dimensions = newEquipement.Longueur + "x" + newEquipement.Hauteur + "x" + newEquipement.Largeur
            });
            _mockRepository.Setup(repo => repo.UpdateAsync(Equipement, newEquipement));


            // Act
            var actionResult = await _equipementController.PutEquipement(newEquipement.IdEquipement, newEquipement);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult");
            _mockRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Equipement>(), It.IsAny<EquipementSansNavigationDTO>()), Times.Once, "La méthode n'a pas été appelé alors qu'elle aurait dû.");
        }

        [TestMethod]
        public async Task PutEquipement_ModelValidated_ReturnsBadRequest()
        {
            // Arrange
            EquipementSansNavigationDTO equipementBadRequest = new()
            {
                IdEquipement = 1,
                NomEquipement = "Test Equipement",
                EstActif = "NSP",
                Hauteur = 1000,
                Largeur = 30.5m,
                Longueur = 900.10m,
                XEquipement = 390m,
                YEquipement = 100.25m,
                ZEquipement = 0,
                IdMur = 10,
                IdTypeEquipement = 2
            };

            // Act
            var actionResult = await _equipementController.PutEquipement(3, equipementBadRequest);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestObjectResult), "Pas un Badrequest");
            _mockRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Equipement>(), It.IsAny<EquipementSansNavigationDTO>()), Times.Never, "La méthode a été appelé alors qu'elle n'aurait pas dû.");
        }

        [TestMethod]
        public async Task PutEquipement_ModelValidated_ReturnsNotFound()
        {
            // Arrange: on ne mock pas, pour que ça ne trouve rien
            EquipementSansNavigationDTO equipementNotFound = new()
            {
                IdEquipement = 3,
                NomEquipement = "Test Equipement",
                EstActif = "NSP",
                Hauteur = 1000,
                Largeur = 30.5m,
                Longueur = 900.10m,
                XEquipement = 390m,
                YEquipement = 100.25m,
                ZEquipement = 0,
                IdMur = 10,
                IdTypeEquipement = 2
            };

            // Act
            var actionResult = await _equipementController.PutEquipement(3, equipementNotFound);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundObjectResult), "Pas un NotFound");
            _mockRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Equipement>(), It.IsAny<EquipementSansNavigationDTO>()), Times.Never, "La méthode a été appelé alors qu'elle n'aurait pas dû.");
        }

        [TestMethod]
        public async Task DeleteEquipementTest_OK()
        {
            // Arrange
            Equipement Equipement = new ()
            {
                IdEquipement = 1,
                NomEquipement = "Lampe",
                EstActif = "OUI",
                Hauteur = 1000.23m,
                Largeur = 20,
                IdMur = 1,
                Longueur = 100,
                IdTypeEquipement = 5,
                XEquipement = 30,
                YEquipement = 10,
                ZEquipement = 30000,
                MurNavigation =  new () { IdMur = 1, Hauteur = 20000, Longueur = 30000 },
                TypeEquipementNavigation = new () { IdTypeEquipement = 2, NomTypeEquipement = "Lumières" }
            };

            _mockRepository.Setup(repo => repo.GetByIdWithoutDTOAsync(1)).ReturnsAsync(Equipement);
            _mockRepository.Setup(repo => repo.DeleteAsync(Equipement));

            // Act
            var actionResult = await _equipementController.DeleteEquipement(1);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult");
            _mockRepository.Verify(repo => repo.DeleteAsync(It.IsAny<Equipement>()), Times.Once, "La méthode n'a pas été appelé alors qu'elle aurait dû.");

        }

        [TestMethod]
        public async Task DeleteEquipementTest_Returns_NotFound()
        {
            // Arrange: Le batiment n'existe pas

            // Act
            var actionResult = await _equipementController.DeleteEquipement(1);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundObjectResult), "Pas un NotFoundResult");
            _mockRepository.Verify(repo => repo.DeleteAsync(It.IsAny<Equipement>()), Times.Never, "La méthode a été appelé alors qu'elle n'aurait pas dû.");
        }
    }
}