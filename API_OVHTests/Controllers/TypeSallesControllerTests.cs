using Moq;
using API_OVH.Controllers;
using API_OVH.Models.DTO;
using API_OVH.Models.EntityFramework;
using API_OVH.Models.Repository;
using Microsoft.AspNetCore.Mvc;

namespace API_OVH.Controllers.Tests
{
    [TestClass]
    public class TypeSalleControllerTest
    {
        private Mock<ITypeSalleRepository<TypeSalle, TypeSalleDTO, TypeSalleDetailDTO>> _mockRepository;
        private TypeSallesController _typeSalleController;

        [TestInitialize]
        public void Setup()
        {
            _mockRepository = new Mock<ITypeSalleRepository<TypeSalle, TypeSalleDTO, TypeSalleDetailDTO>>();

            _typeSalleController = new TypeSallesController(_mockRepository.Object);
        }

        [TestMethod]
        public async Task GetTypesSalle_ReturnsListOfTypesSalles()
        {
            // Arrange
            var typesSalle = new List<TypeSalleDTO>
                {
                    new () { IdTypeSalle = 1, NomTypeSalle = "Type Salle A" },
                    new () { IdTypeSalle = 2, NomTypeSalle = "Type Salle B" }
                };

            _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(typesSalle);

            // Act
            var actionResult = await _typeSalleController.GetTypesSalle();

            // Assert
            Assert.IsNotNull(actionResult.Value, "GetTypesSalle: La liste des types de salle est null.");
            Assert.IsInstanceOfType(actionResult.Value, typeof(IEnumerable<TypeSalleDTO>), "GetTypesSalle: La liste retournée  n'est pas une liste de types de salle.");
            Assert.AreEqual(2, ((IEnumerable<TypeSalleDTO>)actionResult.Value).Count(), "GetTypesSalle: Le nombre de types de salle retourné est incorrect.");
        }

        [TestMethod]
        public async Task GetTypeSalleById_Returns_TypeSalle()
        {
            // Arrange
            var expectedTypeSalle = new TypeSalleDetailDTO { IdTypeSalle = 1, NomTypeSalle = "TD" };

            _mockRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(expectedTypeSalle);

            // Act
            var actionResult = await _typeSalleController.GetTypeSalleById(1);

            // Assert
            Assert.IsNotNull(actionResult, "GetTypeSalleById objet retourné null");
            Assert.IsNotNull(actionResult.Value, "GetTypeSalleById valeur retournée null");
            Assert.AreEqual(expectedTypeSalle, actionResult.Value as TypeSalleDetailDTO, "GetTypeSalleById: types salles non égaux, objet incohérent retourné");
        }

        [TestMethod]
        public async Task GetTypeSalleById_Returns_NotFound_When_TypeSalle_NotFound()
        {
            // Act
            var actionResult = await _typeSalleController.GetTypeSalleById(0);

            // Assert
            Assert.IsNull(actionResult.Value, "GetTypeSalleById: objet retourné non null");
            Assert.IsInstanceOfType<NotFoundObjectResult>(actionResult.Result, "GetTypeSalleById not found a échoué");
        }

        [TestMethod]
        public async Task GetTypeSalleByName_Returns_TypeSalle()
        {
            // Arrange
            var expectedTypeSalle = new TypeSalleDetailDTO { IdTypeSalle = 1, NomTypeSalle = "TD" };

            _mockRepository.Setup(x => x.GetByStringAsync("TD")).ReturnsAsync(expectedTypeSalle);

            // Act
            var actionResult = await _typeSalleController.GetTypeSalleByName("TD");

            // Assert
            Assert.IsNotNull(actionResult, "GetTypeSalleByName objet retourné null");
            Assert.IsNotNull(actionResult.Value, "GetTypeSalleByName valeur retournée null");
            Assert.AreEqual(expectedTypeSalle, actionResult.Value as TypeSalleDetailDTO, "GetTypeSalleByName: types salles non égaux, objet incohérent retourné");
        }


        [TestMethod]
        public async Task GetTypeSalleByName_Returns_NotFound_When_TypeSalle_NotFound()
        {
            // Act
            var actionResult = await _typeSalleController.GetTypeSalleByName("Salle inconnue");

            // Assert
            Assert.IsNull(actionResult.Value, "GetTypeSalleByName: objet retourné non null");
            Assert.IsInstanceOfType<NotFoundObjectResult>(actionResult.Result, "GetTypeSalleByName not found a échoué");
        }

        [TestMethod]
        public async Task PostTypeSalle_ModelValidated_CreationOK()
        {
            // Arrange
            TypeSalleDTO typeSalleDTO = new TypeSalleDTO
            {
                IdTypeSalle = 1,
                NomTypeSalle = "TD"
            };

            // Act
            var actionResult = await _typeSalleController.PostTypeSalle(typeSalleDTO);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<TypeSalleDTO>), "Pas un ActionResult<TypeSalle>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(TypeSalleDTO), "Pas un type de salle");
            Assert.AreEqual(typeSalleDTO, (TypeSalleDTO)result.Value, "Types de salle non identiques");

        }

        [TestMethod]
        public async Task PostTypeSalle_ModelInvalid_ReturnsBadRequest()
        {
            // Arrange
            var invalidDto = new TypeSalleDTO();
            _typeSalleController.ModelState.AddModelError("Nom", "Nom is required.");

            // Act
            var result = await _typeSalleController.PostTypeSalle(invalidDto);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task PutTypeSalle_ModelValidated_UpdateOK()
        {
            // Arrange
            TypeSalle typeSalle = new TypeSalle
            {
                IdTypeSalle = 1,
                NomTypeSalle = "TD",
                Salles = [
                    new Salle{
                        IdBatiment = 1,
                        IdTypeSalle = 1,
                        IdSalle = 1,
                        NomSalle = "D101"
                    },
                    new Salle{
                        IdBatiment = 2,
                        IdTypeSalle = 1,
                        IdSalle = 2,
                        NomSalle = "D310"
                    }
                ]
            };

            TypeSalleDTO typeSalleUpdated = new ()
            {
                IdTypeSalle = 1,
                NomTypeSalle = "TP"
            };

            _mockRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(typeSalle);

            // Act
            var actionResult = await _typeSalleController.PutTypeSalle(typeSalleUpdated.IdTypeSalle, typeSalleUpdated);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult");
        }

        [TestMethod]
        public async Task PutTypeSalle_ModelValidated_ReturnsBadRequest()
        {
            // Act
            var actionResult = await _typeSalleController.PutTypeSalle(3, new TypeSalle
            {
                IdTypeSalle = 1,
                NomTypeSalle = "AAA"
            });

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestObjectResult), "Pas un Badrequest");
        }

        [TestMethod]
        public async Task PutTypeSalle_ModelValidated_ReturnsNotFound()
        {
            // Act
            var actionResult = await _typeSalleController.PutTypeSalle(3, new TypeSalle
            {
                IdTypeSalle = 3,
                NomTypeSalle = "AAA"
            });

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundObjectResult), "Pas un NotFound");
        }

        [TestMethod]
        public async Task DeleteTypeSalleTest()
        {
            // Arrange
            TypeSalle typeSalle = new TypeSalle
            {
                IdTypeSalle = 1,
                NomTypeSalle = "TD",
                Salles = [
                    new Salle{
                        IdBatiment = 1,
                        IdTypeSalle = 1,
                        IdSalle = 1,
                        NomSalle = "D101"
                    }, 
                    new Salle{
                        IdBatiment = 2,
                        IdTypeSalle = 1,
                        IdSalle = 2,
                        NomSalle = "D310"
                    }
                ]
            };

            _mockRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(typeSalle);

            // Act
            var actionResult = await _typeSalleController.DeleteTypeSalle(1);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult");
        }

        [TestMethod]
        public async Task DeleteTypeSalleTest_Returns_NotFound()
        {
            // Act
            var actionResult = await _typeSalleController.DeleteTypeSalle(1);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundObjectResult), "Pas un NotFoundResult");
        }
    }
}