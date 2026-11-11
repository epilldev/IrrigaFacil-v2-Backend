
namespace IrrigaFacil.Domain.Entities;

/// <summary>
/// Representa uma planta cadastrada no sistema de irrigação.
/// Armazena suas condições ideais de umidade e temperatura.
/// </summary>
public class Planta
{
    public Guid Id { get; private set; }

    public string Nome { get; private set; }

    public string Especie { get; private set; }

    public decimal UmidadeMinima { get; private set; }

    public decimal UmidadeMaxima { get; private set; }

    public decimal TemperaturaMinima { get; private set; }

    public decimal TemperaturaMaxima { get; private set; }

    public bool Ativa { get; private set; }

    public DateTimeOffset DataCadastro { get; private set; }

    /// <summary>
    /// Cria uma nova planta validando suas informações.
    /// </summary>
    public Planta(
        string nome,
        string especie,
        decimal umidadeMinima,
        decimal umidadeMaxima,
        decimal temperaturaMinima,
        decimal temperaturaMaxima)
    {
        ValidarNome(nome);
        ValidarEspecie(especie);
        ValidarUmidade(umidadeMinima, umidadeMaxima);
        ValidarTemperatura(temperaturaMinima, temperaturaMaxima);

        Id = Guid.NewGuid();
        Nome = nome.Trim();
        Especie = especie.Trim();
        UmidadeMinima = umidadeMinima;
        UmidadeMaxima = umidadeMaxima;
        TemperaturaMinima = temperaturaMinima;
        TemperaturaMaxima = temperaturaMaxima;
        Ativa = true;
        DataCadastro = DateTimeOffset.UtcNow;
    }

    /// <summary>
    /// Atualiza as condições ideais da planta.
    /// </summary>
    public void AtualizarParametrosIdeais(
        decimal umidadeMinima,
        decimal umidadeMaxima,
        decimal temperaturaMinima,
        decimal temperaturaMaxima)
    {
        ValidarUmidade(umidadeMinima, umidadeMaxima);
        ValidarTemperatura(temperaturaMinima, temperaturaMaxima);

        UmidadeMinima = umidadeMinima;
        UmidadeMaxima = umidadeMaxima;
        TemperaturaMinima = temperaturaMinima;
        TemperaturaMaxima = temperaturaMaxima;
    }

    /// <summary>
    /// Ativa a planta no sistema.
    /// </summary>
    public void Ativar()
    {
        Ativa = true;
    }

    /// <summary>
    /// Desativa a planta no sistema.
    /// </summary>
    public void Desativar()
    {
        Ativa = false;
    }

    private static void ValidarNome(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException(
                "O nome da planta é obrigatório.",
                nameof(nome));
    }

    private static void ValidarEspecie(string especie)
    {
        if (string.IsNullOrWhiteSpace(especie))
            throw new ArgumentException(
                "A espécie da planta é obrigatória.",
                nameof(especie));
    }

    private static void ValidarUmidade(
        decimal minima,
        decimal maxima)
    {
        if (minima < 0 || maxima > 100 || minima >= maxima)
            throw new ArgumentException(
                "Os limites de umidade devem estar entre 0 e 100%, com o mínimo menor que o máximo.");
    }

    private static void ValidarTemperatura(
        decimal minima,
        decimal maxima)
    {
        if (minima >= maxima)
            throw new ArgumentException(
                "A temperatura mínima deve ser menor que a máxima.");
    }
}
