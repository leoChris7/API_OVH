using Moq;
using API_OVH.Controllers;
using API_OVH.Models.DTO;
using API_OVH.Models.EntityFramework;
using API_OVH.Models.Repository;
using Microsoft.AspNetCore.Mvc;

namespace API_OVH.Controllers.Tests
{
    [TestClass]
    public class TypeEquipementsControllerTest
    {
        private Mock<ITypeEquipementRepository<TypeEquipement, TypeEquipementDTO, TypeEquipementDetailDTO>> _mockRepository;
        private TypeEquipementsController _typeEquipementController;

        [TestInitialize]
        public void Setup()
        {
            _mockRepository = new Mock<ITypeEquipementRepository<TypeEquipement, TypeEquipementDTO, TypeEquipementDetailDTO>>();

            _typeEquipementController = new TypeEquipementsController(_mockRepository.Object);
        }

        [TestMethod]
        public async Task GetTypesEquipement_ReturnsListOfTypesEquipements()
        {
            // Arrange
            var typesEquipement = new List<TypeEquipementDTO>
                {
                    new () { IdTypeEquipement = 1, NomTypeEquipement = "Type Equipement A" },
                    new () { IdTypeEquipement = 2, NomTypeEquipement = "Type Equipement B" }
                };

            _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(typesEquipement);

            // Act
            var actionResult = await _typeEquipementController.GetTypesEquipement();

            // Assert
            Assert.IsNotNull(actionResult.Value, "GetTypesEquipement: La liste des types d'équipement est null.");
            Assert.IsInstanceOfType(actionResult.Value, typeof(IEnumerable<TypeEquipementDTO>), "GetTypesEquipement: La liste retournée  n'est pas une liste de types d'équipement.");
            Assert.AreEqual(2, ((IEnumerable<TypeEquipementDTO>)actionResult.Value).Count(), "GetTypesEquipement: Le nombre de types d'équipements retourné est incorrect.");
        }

        [TestMethod]
        public async Task GetTypesEquipement_ReturnsEmptyList_WhenEmpty()
        {
            // Arrange
            List<TypeEquipementDTO> TypeEquipements = new List<TypeEquipementDTO>();
            _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(TypeEquipements);

            // Act
            var actionResult = await _typeEquipementController.GetTypesEquipement();

            // Assert
            Assert.IsNotNull(actionResult.Value, "La liste des TypeEquipements est null.");
            Assert.IsInstanceOfType(actionResult.Value, typeof(List<TypeEquipementDTO>), "La liste retournée n'est pas une liste de types de TypeEquipements.");
            var TypeEquipementsList = actionResult.Value as List<TypeEquipementDTO>;
            Assert.AreEqual(0, TypeEquipementsList.Count, "Le nombre de TypeEquipements retourné est incorrect.");
            Assert.IsTrue(!TypeEquipementsList.Any(), "La liste des TypeEquipements devrait être vide.");
        }

        [TestMethod]
        public async Task GetTypeEquipementById_Returns_TypeEquipement()
        {
            // Arrange
            var expectedTypeEquipement = new TypeEquipementDetailDTO { IdTypeEquipement = 1, NomTypeEquipement = "Fenetre" };

            _mockRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(expectedTypeEquipement);

            // Act
            var actionResult = await _typeEquipementController.GetTypeEquipementById(1);

            // Assert
            Assert.IsNotNull(actionResult, "GetTypeEquipementById: objet retourné null");
            Assert.IsNotNull(actionResult.Value, "GetTypeEquipementById: valeur retournée null");
            Assert.AreEqual(expectedTypeEquipement, actionResult.Value as TypeEquipementDetailDTO, "GetTypeEquipementById: types d'équipements non égaux, objet incohérent retourné");
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
            var expectedTypeEquipement = new TypeEquipementDetailDTO { IdTypeEquipement = 1, NomTypeEquipement = "Fenetre" };

            _mockRepository.Setup(x => x.GetByStringAsync("Fenetre")).ReturnsAsync(expectedTypeEquipement);

            // Act
            var actionResult = await _typeEquipementController.GetTypeEquipementByName("Fenetre");

            // Assert
            Assert.IsNotNull(actionResult, "GetTypeEquipementByName: objet retourné null");
            Assert.IsNotNull(actionResult.Value, "GetTypeEquipementByName: valeur retournée null");
            Assert.AreEqual(expectedTypeEquipement, actionResult.Value as TypeEquipementDetailDTO, "GetTypeEquipementByName: types d'équipements non égaux, objet incohérent retourné");
        }

        [TestMethod]
        public async Task GetTypeEquipementByNameRandomUppercase_Returns_TypeEquipement()
        {
            // Arrange
            var expectedTypeEquipement = new TypeEquipementDetailDTO { IdTypeEquipement = 1, NomTypeEquipement = "Fenetre" };

            _mockRepository.Setup(x => x.GetByStringAsync("FeNETRE")).ReturnsAsync(expectedTypeEquipement);

            // Act
            var actionResult = await _typeEquipementController.GetTypeEquipementByName("FeNETRE");

            // Assert
            Assert.IsNotNull(actionResult, "GetTypeEquipementByName: objet retourné null");
            Assert.IsNotNull(actionResult.Value, "GetTypeEquipementByName: valeur retournée null");
            Assert.AreEqual(expectedTypeEquipement, actionResult.Value as TypeEquipementDetailDTO, "GetTypeEquipementByName: types d'équipements non égaux, objet incohérent retourné");
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
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<TypeEquipementDTO>), "PostTypeEquipement: Pas un ActionResult<TypeEquipementDTO>");
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

            TypeEquipementDTO newTypeEquipement = new TypeEquipementDTO
            {
                IdTypeEquipement = 1,
                NomTypeEquipement = "B"
            };

            _mockRepository.Setup(x => x.GetByIdWithoutDTOAsync(1)).ReturnsAsync(typeEquipement);

            // Act
            var actionResult = await _typeEquipementController.PutTypeEquipement(newTypeEquipement.IdTypeEquipement, newTypeEquipement);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult");
        }

        [TestMethod]
        public async Task PutTypeEquipement_ModelValidated_ReturnsBadRequest()
        {
            // Act
            var actionResult = await _typeEquipementController.PutTypeEquipement(3, new TypeEquipementDTO
            {
                IdTypeEquipement = 1,
                NomTypeEquipement = "Type échoué"
            });

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestObjectResult), "Pas un Badrequest");
        }

        [TestMethod]
        public async Task PutTypeEquipement_ModelValidated_ReturnsNotFound()
        {
            // Act
            var actionResult = await _typeEquipementController.PutTypeEquipement(3, new TypeEquipementDTO
            {
                IdTypeEquipement = 3,
                NomTypeEquipement = "Type non trouvé"
            });

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundObjectResult), "Pas un NotFound");
        }

        [TestMethod]
        public async Task DeleteTypeEquipementTest_OK()
        {
            // Arrange
            TypeEquipement typeEquipement = new ()
            {
                IdTypeEquipement = 1,
                NomTypeEquipement = "TD"
            };

            _mockRepository.Setup(x => x.GetByIdWithoutDTOAsync(1)).ReturnsAsync(typeEquipement);

            // Act
            var actionResult = await _typeEquipementController.DeleteTypeEquipement(1);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult");
        }

        [TestMethod]
        public async Task DeleteTypeEquipementTest_Returns_NotFound()
        {
            // Act
            var actionResult = await _typeEquipementController.DeleteTypeEquipement(1);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundObjectResult), "Pas un NotFoundResult");
        }
    }
}