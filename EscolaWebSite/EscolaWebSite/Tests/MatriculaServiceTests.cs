using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using EscolaWebSite.DTO;
using EscolaWebSite.Repositories.Interfaces;
using EscolaWebSite.Services;
using EscolaWebSite.Services.Interfaces;

namespace EscolaWebSite.Tests
{
    [TestClass]
    public class MatriculaServiceTests
    {
        private Mock<IMatriculaRepository> _matriculaRepositoryMock;
        private Mock<IAlunoService> _alunoServiceMock;
        private Mock<ITurmaService> _turmaServiceMock;
        private MatriculaService _matriculaService;

        [TestInitialize]
        public void Setup()
        {            
            _alunoServiceMock = new Mock<IAlunoService>();
            _matriculaRepositoryMock = new Mock<IMatriculaRepository>();
            _turmaServiceMock = new Mock<ITurmaService>();
            _matriculaService = new MatriculaService(
                _matriculaRepositoryMock.Object,
                _alunoServiceMock.Object,
                _turmaServiceMock.Object);
        }

        [TestMethod]
        
        public void RealizarMatricula_AlunoInativo_RetornaConflito()
        {
            // Arrange
            var matriculaDTO = new MatriculaDTO { AlunoId = 1, TurmaId = 1 };
            _alunoServiceMock.Setup(x => x.Exists(1)).Returns(true);
            _alunoServiceMock.Setup(x => x.IsActive(1)).Returns(false);
            _turmaServiceMock.Setup(x => x.Exists(1)).Returns(true);
            _turmaServiceMock.Setup(x => x.HasAvailableVagas(1)).Returns(true);

            // Act
            _matriculaService.RealizarMatricula(matriculaDTO);

            Assert.Throws<InvalidOperationException>(() => _matriculaService.RealizarMatricula(matriculaDTO), "Aluno inativo");
        }

        [TestMethod]
        
        public void RealizarMatricula_TurmaSemVagas_RetornaConflito()
        {
            // Arrange
            var matriculaDTO = new MatriculaDTO { AlunoId = 1, TurmaId = 1 };
            _alunoServiceMock.Setup(x => x.Exists(1)).Returns(true);
            _alunoServiceMock.Setup(x => x.IsActive(1)).Returns(true);
            _turmaServiceMock.Setup(x => x.Exists(1)).Returns(true);
            _turmaServiceMock.Setup(x => x.HasAvailableVagas(1)).Returns(false);

            // Act
            _matriculaService.RealizarMatricula(matriculaDTO);

            Assert.Throws<InvalidOperationException>(() => _matriculaService.RealizarMatricula(matriculaDTO), "Turma sem vagas disponíveis");
        }

        [TestMethod]
        
        public void RealizarMatricula_AlunoJaMatriculado_RetornaConflito()
        {
            // Arrange
            var matriculaDTO = new MatriculaDTO { AlunoId = 1, TurmaId = 1 };
            _alunoServiceMock.Setup(x => x.Exists(1)).Returns(true);
            _alunoServiceMock.Setup(x => x.IsActive(1)).Returns(true);
            _turmaServiceMock.Setup(x => x.Exists(1)).Returns(true);
            _turmaServiceMock.Setup(x => x.HasAvailableVagas(1)).Returns(true);
            _matriculaRepositoryMock.Setup(x => x.Exists(1, 1)).Returns(true);

            // Act
            _matriculaService.RealizarMatricula(matriculaDTO);
        }

        [TestMethod]
        public void RealizarMatricula_MatriculaValida_RetornaSucesso()
        {
            // Arrange
            var matriculaDTO = new MatriculaDTO { AlunoId = 1, TurmaId = 1 };
            _alunoServiceMock.Setup(x => x.Exists(1)).Returns(true);
            _alunoServiceMock.Setup(x => x.IsActive(1)).Returns(true);
            _turmaServiceMock.Setup(x => x.Exists(1)).Returns(true);
            _turmaServiceMock.Setup(x => x.HasAvailableVagas(1)).Returns(true);
            _matriculaRepositoryMock.Setup(x => x.Exists(1, 1)).Returns(false);
            _matriculaRepositoryMock.Setup(x => x.Insert(It.IsAny<EscolaWebSite.Models.Matricula>())).Returns(1);
            _turmaServiceMock.Setup(x => x.GetVagasDisponiveis(1)).Returns(5);
            _turmaServiceMock.Setup(x => x.UpdateVagas(1, 4)).Returns(true);

            // Act
            var result = _matriculaService.RealizarMatricula(matriculaDTO);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.AlunoId);
            Assert.AreEqual(1, result.TurmaId);
        }
    }
}