namespace ARVTech.Shared.Enums
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Enum with values that represents "Unidade de Negócio".
    /// </summary>
    public enum UnidadeNegocio
    {
        [Display(Name = "TODAS")]
        Todas,

        [Display(Name = "MATRIZ")]
        Matriz = 1,

        [Display(Name = "MACROMIX")]
        Macromix = 2,

        [Display(Name = "RISSUL")]
        Rissul = 3,

        [Display(Name = "ATACAREJO")]
        Atacarejo = 4,

        [Display(Name = "INDÚSTRIA")]
        Industria = 5,

        [Display(Name = "LOGÍSTICA")]
        Logistica = 6,

        [Display(Name = "MANUTENÇÃO")]
        Manutencao = 7,

        [Display(Name = "TRANSPORTE")]
        Transporte = 8,
    }
}