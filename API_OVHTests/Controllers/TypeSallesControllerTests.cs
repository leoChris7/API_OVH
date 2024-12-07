using Moq;
using API_OVH.Controllers;
using API_OVH.Models.DTO;
using API_OVH.Models.EntityFramework;
using API_OVH.Models.Repository;
using Microsoft.AspNetCore.Mvc;

namespace TypeSallesControllerTests{
    [TestClass]
    public class TypeSalleControllerTest
    {
        private Mock<ITypeSalleRepository<TypeSalle, TypeSalleDTO>> _mockRepository;
        private TypeSallesController _typeSalleController;

        [TestInitialize]
        public void Setup()
        {
            _mockRepository = new Mock<ITypeSalleRepository<TypeSalle, TypeSalleDTO>>();

            _typeSalleController = new TypeSallesController(_mockRepository.Object);
        }

        [TestMethod]
        public async Task GetTypesSalle_ReturnsListOfTypesSalles()
        {
            // Arrange
            var typesSalle = new List<TypeSalleDTO>
                {
                    new TypeSalleDTO { IdTypeSalle = 1, NomTypeSalle = "Type Salle A" },
                    new TypeSalleDTO { IdTypeSalle = 2, NomTypeSalle = "Type Salle B" }
                };

            _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(typesSalle);

            // Act
            var actionResult = await _typeSalleController.GetTypeSalle();

            // Assert
            Assert.IsNotNull(actionResult.Value, "GetTypesSalle: La liste des types de salle est null.");
            Assert.IsInstanceOfType(actionResult.Value, typeof(IEnumerable<TypeSalleDTO>), "GetTypesSalle: La liste retournée  n'est pas une liste de types de salle.");
            Assert.AreEqual(2, ((IEnumerable<TypeSalleDTO>)actionResult.Value).Count(), "GetTypesSalle: Le nombre de types de salle retourné est incorrect.");
        }

        [TestMethod]
        public async Task GetTypeSalleById_Returns_TypeSalle()
        {
            // Arrange
            var expectedTypeSalle = new TypeSalle { IdTypeSalle = 1, NomTypeSalle = "TD" };

            _mockRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(expectedTypeSalle);

            // Act
            var actionResult = _typeSalleController.GetTypeSalleById(1).Result;

            // Assert
            Assert.IsNotNull(actionResult, "Gettypesallebyid objet retourné null");
            Assert.IsNotNull(actionResult.Value, "GetTypeSalleById valeur retournée null");
            Assert.AreEqual(expectedTypeSalle, actionResult.Value as TypeSalle, "GetTypeSalle: types salles non égaux, objet incohérent retourné");
        }


        [TestMethod]
        public async Task GetTypeSalleById_Returns_NotFound_When_TypeSalle_NotFound()
        {
            // Act
            var actionResult = await _typeSalleController.GetTypeSalleById(0);

            // Assert
            Assert.IsInstanceOfType<NotFoundObjectResult>(actionResult.Result, "Test GetById not found a échoué");
        }

        [TestMethod]
        public async Task GetTypeSalleByName_Returns_TypeSalle()
        {
            // Arrange
            var expectedTypeSalle = new TypeSalle { IdTypeSalle = 1, NomTypeSalle = "TD" };

            _mockRepository.Setup(x => x.GetByStringAsync("TD")).ReturnsAsync(expectedTypeSalle);

            // Act
            var actionResult = _typeSalleController.GetTypeSalleByName("TD").Result;

            // Assert
            Assert.IsNotNull(actionResult, "GetTypeSalleByName objet retourné null");
            Assert.IsNotNull(actionResult.Value, "GetTypeSalleByName valeur retournée null");
            Assert.AreEqual(expectedTypeSalle, actionResult.Value as TypeSalle, "GetTypeSalleByName: types salles non égaux, objet incohérent retourné");
        }


        [TestMethod]
        public async Task GetTypeSalleByName_Returns_NotFound_When_TypeSalle_NotFound()
        {
            // Act
            var actionResult = await _typeSalleController.GetTypeSalleByName("Salle inconnue");

            // Assert
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
            var invalidDto = new TypeSalleDTO(); // Missing required fields
            _typeSalleController.ModelState.AddModelError("Nom", "Nom is required.");

            // Act
            var result = await _typeSalleController.PostTypeSalle(invalidDto);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task Pututilisateur_ModelValidated_UpdateOK()
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

            TypeSalle typeSalleUpdated = new TypeSalle
            {
                IdTypeSalle = 1,
                NomTypeSalle = "TP",
                Salles = []
            };

            _mockRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(typeSalle);

            // Act
            var actionResult = _typeSalleController.PutTypeSalle(typeSalleUpdated.IdTypeSalle, typeSalleUpdated).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }

        [TestMethod]
        public async Task Pututilisateur_ModelValidated_ReturnsBadRequest()
        {
            // Act
            var actionResult = _typeSalleController.PutTypeSalle(3, new TypeSalle
            {
                IdTypeSalle = 1,
                NomTypeSalle = "AAA"
            }).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestObjectResult), "Pas un Badrequest"); // Test du type de retour
        }

        [TestMethod]
        public async Task Pututilisateur_ModelValidated_ReturnsNotFound()
        {
            // Act
            var actionResult = _typeSalleController.PutTypeSalle(3, new TypeSalle
            {
                IdTypeSalle = 3,
                NomTypeSalle = "AAA"
            }).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundObjectResult), "Pas un NotFound"); // Test du type de retour
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
            var actionResult = _typeSalleController.DeleteTypeSalle(1).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }

        [TestMethod]
        public async Task DeleteTypeSalleTest_Returns_NotFound()
        {
            // Act
            var actionResult = _typeSalleController.DeleteTypeSalle(1);

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundObjectResult), "Pas un NotFoundResult"); // Test du type de retour
        }
    }
}