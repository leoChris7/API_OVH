using Microsoft.VisualStudio.TestTools.UnitTesting;
using API_OVH.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using API_OVH.Models.EntityFramework;
using API_OVH.Models.DTO;
using API_OVH.Models.Repository;

namespace API_OVH.Controllers.Tests
{
    [TestClass()]
    public class UniteCapteurControllerTests
    {
        private Mock<IUniteCapteurRepository<UniteCapteur, UniteCapteurSansNavigationDTO>> _mockRepository;
        private UniteCapteurController _uniteCapteurController;

        [TestInitialize]
        public void Setup()
        {
            _mockRepository = new Mock<IUniteCapteurRepository<UniteCapteur, UniteCapteurSansNavigationDTO>>();

            _uniteCapteurController = new UniteCapteurController(_mockRepository.Object);
        }

        [TestMethod]
        public async Task PostUniteCapteurSansNavigationDTO_ModelValidated_CreationOK()
        {
            // Arrange
            UniteCapteurSansNavigationDTO UniteDTO = new UniteCapteurSansNavigationDTO
            {
                IdUnite = 1,
                IdCapteur = 1
            };

            // Act
            var actionResult = await _uniteCapteurController.PostUniteCapteur(UniteDTO);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<UniteCapteurSansNavigationDTO>), "PostUniteCapteur: Pas un ActionResult<UniteCapteurSansNavigationDTO>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "PostUniteCapteur: Pas un CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(UniteCapteurSansNavigationDTO), "PostUniteCapteur: Pas un UniteCapteurSansNavigationDTO");
            Assert.AreEqual(UniteDTO, (UniteCapteurSansNavigationDTO)result.Value, "PostUniteCapteur: UnitésCapteurs non identiques");

        }

        [TestMethod]
        public async Task PostUniteCapteurSansNavigationDTO_ModelInvalid_ReturnsBadRequest()
        {
            // Arrange
            var invalidDto = new UniteCapteurSansNavigationDTO();
            _uniteCapteurController.ModelState.AddModelError("IdUnite", "IdUnite est requis.");

            // Act
            var result = await _uniteCapteurController.PostUniteCapteur(invalidDto);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task DeleteUniteCapteurTest_OK()
        {
            // Arrange
            UniteCapteur Unite = new UniteCapteur
            {
                IdUnite = 1,
                IdCapteur = 2
            };

            _mockRepository.Setup(repo => repo.GetByIdAsync(2, 1)).ReturnsAsync(Unite);

            // Act
            var actionResult = await _uniteCapteurController.DeleteUnite(2, 1);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult");
        }

        [TestMethod]
        public async Task DeleteUniteCapteurTest_Returns_NotFound()
        {
            // Act
            var actionResult = await _uniteCapteurController.DeleteUnite(1,2);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundObjectResult), "Pas un NotFoundResult");
        }
    }
}