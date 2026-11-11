
using IrrigaFacil.Domain.Entities;

namespace IrrigaFacil.TesteUnitario;

/// <summary>
/// Testes unitários das regras de negócio da entidade Planta.
/// </summary>
public class PlantaTests
{
    /// <summary>
    /// Testa se uma planta é criada corretamente quando todos os dados são válidos.
    /// </summary>
    [Fact]
    public void CriarPlanta_ComDadosValidos_DeveCriarCorretamente()
    {
        // Arrange
        var nome = "Tomate";
        var especie = "Solanum lycopersicum";

        // Act
        var planta = new Planta(
            nome,
            especie,
            40m,
            70m,
            18m,
            30m);

        // Assert
        Assert.NotEqual(Guid.Empty, planta.Id);
        Assert.Equal(nome, planta.Nome);
        Assert.Equal(especie, planta.Especie);
        Assert.Equal(40m, planta.UmidadeMinima);
        Assert.Equal(70m, planta.UmidadeMaxima);
        Assert.True(planta.Ativa);
    }

    /// <summary>
    /// Testa se a criação de uma planta com nome vazio gera uma exceção.
    /// </summary>
    [Fact]
    public void CriarPlanta_ComNomeVazio_DeveLancarExcecao()
    {
        // Act
        var excecao = Assert.Throws<ArgumentException>(() =>
            new Planta("", "Tomate", 40m, 70m, 18m, 30m));

        // Assert
        Assert.Equal("nome", excecao.ParamName);
    }

    /// <summary>
    /// Testa se a criação de uma planta com limites de umidade inválidos gera uma exceção.
    /// </summary>
    [Fact]
    public void CriarPlanta_ComUmidadeInvalida_DeveLancarExcecao()
    {
        // Act
        var excecao = Assert.Throws<ArgumentException>(() =>
            new Planta("Tomate", "Solanum lycopersicum", 80m, 70m, 18m, 30m));

        // Assert
        Assert.Contains("umidade", excecao.Message, StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// Testa se a planta tem seu status alterado para inativo após a chamada do método Desativar.
    /// </summary>
    [Fact]
    public void DesativarPlanta_DeveAlterarStatus()
    {
        // Arrange
        var planta = CriarPlantaValida();

        // Act
        planta.Desativar();

        // Assert
        Assert.False(planta.Ativa);
    }

    /// <summary>
    /// Testa se uma planta desativada pode voltar ao estado ativo.
    /// </summary>
    [Fact]
    public void AtivarPlanta_DeveAlterarStatus()
    {
        // Arrange
        var planta = CriarPlantaValida();
        planta.Desativar();

        // Act
        planta.Ativar();

        // Assert
        Assert.True(planta.Ativa);
    }

    /// <summary>
    /// Cria uma planta com dados válidos para reutilização nos testes.
    /// </summary>
    private static Planta CriarPlantaValida()
    {
        return new Planta(
            "Tomate",
            "Solanum lycopersicum",
            40m,
            70m,
            18m,
            30m);
    }
}
