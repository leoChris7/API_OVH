﻿using Moq;
using API_OVH.Controllers;
using API_OVH.Models.DTO;
using API_OVH.Models.EntityFramework;
using API_OVH.Models.Repository;
using Microsoft.AspNetCore.Mvc;

namespace TypeEquipementsControllerTest
{
    [TestClass]
    public class TypeEquipementsControllerTest
    {
        private Mock<ITypeEquipementRepository<TypeEquipement, TypeEquipementDTO>> _mockRepository;
        private TypeEquipementsController _typeEquipementController;

        [TestInitialize]
        public void Setup()
        {
            _mockRepository = new Mock<ITypeEquipementRepository<TypeEquipement, TypeEquipementDTO>>();

            _typeEquipementController = new TypeEquipementsController(_mockRepository.Object);
        }

        [TestMethod]
        public async Task GetTypesEquipement_ReturnsListOfTypesEquipements()
        {
            // Arrange
            var typesEquipement = new List<TypeEquipementDTO>
                {
                    new TypeEquipementDTO { IdTypeEquipement = 1, NomTypeEquipement = "Type Equipement A" },
                    new TypeEquipementDTO { IdTypeEquipement = 2, NomTypeEquipement = "Type Equipement B" }
                };

            _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(typesEquipement);

            // Act
            var actionResult = await _typeEquipementController.GetTypeEquipements();

            // Assert
            Assert.IsNotNull(actionResult.Value, "GetTypesEquipement: La liste des types d'équipement est null.");
            Assert.IsInstanceOfType(actionResult.Value, typeof(IEnumerable<TypeEquipementDTO>), "GetTypesEquipement: La liste retournée  n'est pas une liste de types d'équipement.");
            Assert.AreEqual(2, ((IEnumerable<TypeEquipementDTO>)actionResult.Value).Count(), "GetTypesEquipement: Le nombre de types d'équipements retourné est incorrect.");
        }

        [TestMethod]
        public async Task GetTypeEquipementById_Returns_TypeEquipement()
        {
            // Arrange
            var expectedTypeEquipement = new TypeEquipement { IdTypeEquipement = 1, NomTypeEquipement = "Fenetre" };

            _mockRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(expectedTypeEquipement);

            // Act
            var actionResult = _typeEquipementController.GetTypeEquipementById(1).Result;

            // Assert
            Assert.IsNotNull(actionResult, "GetTypeEquipementById: objet retourné null");
            Assert.IsNotNull(actionResult.Value, "GetTypeEquipementById: valeur retournée null");
            Assert.AreEqual(expectedTypeEquipement, actionResult.Value as TypeEquipement, "GetTypeEquipementById: types d'équipements non égaux, objet incohérent retourné");
        }


        [TestMethod]
        public async Task GetTypeEquipementById_Returns_NotFound_When_TypeEquipement_NotFound()
        {
            // Act
            var actionResult = await _typeEquipementController.GetTypeEquipementById(0);

            // Assert
            Assert.IsNull(actionResult.Value, "GetTypeEquipementById: objet retourné non null");
            Assert.IsInstanceOfType<NotFoundObjectResult>(actionResult.Result, "GetTypeEquipementById: pas Not Found");
        }

        [TestMethod]
        public async Task GetTypeEquipementByName_Returns_TypeEquipement()
        {
            // Arrange
            var expectedTypeEquipement = new TypeEquipement { IdTypeEquipement = 1, NomTypeEquipement = "Fenetre" };

            _mockRepository.Setup(x => x.GetByStringAsync("Fenetre")).ReturnsAsync(expectedTypeEquipement);

            // Act
            var actionResult = _typeEquipementController.GetTypeEquipementByName("Fenetre").Result;

            // Assert
            Assert.IsNotNull(actionResult, "GetTypeEquipementByName: objet retourné null");
            Assert.IsNotNull(actionResult.Value, "GetTypeEquipementByName: valeur retournée null");
            Assert.AreEqual(expectedTypeEquipement, actionResult.Value as TypeEquipement, "GetTypeEquipementByName: types d'équipements non égaux, objet incohérent retourné");
        }


        [TestMethod]
        public async Task GetTypeEquipementByName_Returns_NotFound_When_TypeEquipement_NotFound()
        {
            // Act
            var actionResult = await _typeEquipementController.GetTypeEquipementByName("Type d'équipement inconnue");

            // Assert
            Assert.IsNull(actionResult.Value, "GetTypeEquipementByName: objet retourné non null");
            Assert.IsInstanceOfType<NotFoundObjectResult>(actionResult.Result, "GetTypeEquipementByName: not found a échoué");
        }

        [TestMethod]
        public async Task PostTypeEquipementDTO_ModelValidated_CreationOK()
        {
            // Arrange
            TypeEquipementDTO typeEquipementDTO = new TypeEquipementDTO
            {
                IdTypeEquipement = 1,
                NomTypeEquipement = "Electronique"
            };

            // Act
            var actionResult = await _typeEquipementController.PostTypeEquipement(typeEquipementDTO);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<TypeEquipementDTO>), "PostTypeEquipement: Pas un ActionResult<TypeEquipement>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "PostTypeEquipement: Pas un CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(TypeEquipementDTO), "PostTypeEquipement: Pas un type d'équipement");
            Assert.AreEqual(typeEquipementDTO, (TypeEquipementDTO)result.Value, "PostTypeEquipement: Types d'équipement non identiques");

        }

        [TestMethod]
        public async Task PostTypeEquipement_ModelInvalid_ReturnsBadRequest()
        {
            // Arrange
            var invalidDto = new TypeEquipementDTO(); // Missing required fields
            _typeEquipementController.ModelState.AddModelError("NomTypeEquipement", "Nom est requis.");

            // Act
            var result = await _typeEquipementController.PostTypeEquipement(invalidDto);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task PutTypeEquipement_ModelValidated_UpdateOK()
        {
            // Arrange
            TypeEquipement typeEquipement = new TypeEquipement
            {
                IdTypeEquipement = 1,
                NomTypeEquipement = "A"
            };

            TypeEquipement newTypeEquipement = new TypeEquipement
            {
                IdTypeEquipement = 1,
                NomTypeEquipement = "B"
            };

            _mockRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(typeEquipement);

            // Act
            var actionResult = _typeEquipementController.PutTypeEquipement(newTypeEquipement.IdTypeEquipement, newTypeEquipement).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }

        [TestMethod]
        public async Task PutTypeEquipement_ModelValidated_ReturnsBadRequest()
        {
            // Act
            var actionResult = _typeEquipementController.PutTypeEquipement(3, new TypeEquipement
            {
                IdTypeEquipement = 1,
                NomTypeEquipement = "Type échoué"
            }).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestObjectResult), "Pas un Badrequest"); // Test du type de retour
        }

        [TestMethod]
        public async Task PutTypeEquipement_ModelValidated_ReturnsNotFound()
        {
            // Act
            var actionResult = _typeEquipementController.PutTypeEquipement(3, new TypeEquipement
            {
                IdTypeEquipement = 3,
                NomTypeEquipement = "Type non trouvé"
            }).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundObjectResult), "Pas un NotFound"); // Test du type de retour
        }

        [TestMethod]
        public async Task DeleteTypeEquipementTest_OK()
        {
            // Arrange
            TypeEquipement typeEquipement = new TypeEquipement
            {
                IdTypeEquipement = 1,
                NomTypeEquipement = "TD"
            };

            _mockRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(typeEquipement);

            // Act
            var actionResult = _typeEquipementController.DeleteTypeEquipement(1).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }

        [TestMethod]
        public async Task DeleteTypeEquipementTest_Returns_NotFound()
        {
            // Act
            var actionResult = _typeEquipementController.DeleteTypeEquipement(1);

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundObjectResult), "Pas un NotFoundResult"); // Test du type de retour
        }
    }
}