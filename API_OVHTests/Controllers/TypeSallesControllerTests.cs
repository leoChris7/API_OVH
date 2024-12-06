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

            // Configure le mock
            _mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(expectedTypeSalle);

            // Act
            var actionResult = await _typeSalleController.GetTypeSalleById(1);

            // Assert - Vérifier que le résultat n'est pas null
            Assert.IsNotNull(actionResult, "GetTypeSalleById: La réponse ActionResult est null.");

            // Extraire le type d'ActionResult
            var result = actionResult.Result as OkObjectResult;

            // Assert
            Assert.IsNotNull(result, "GetProduitById: La réponse n'est pas de type OkObjectResult.");
            var returnedProduit = result.Value as TypeSalle;
            Assert.IsNotNull(returnedProduit, "GetProduitById: Le produit retourné est null.");
            Assert.AreEqual(returnedProduit.NomTypeSalle, expectedTypeSalle.NomTypeSalle, "GetProduitById: Les produits ne sont pas égaux.");
        }


    }
}