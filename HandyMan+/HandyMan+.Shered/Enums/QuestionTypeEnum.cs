using Orders.Shared.Enums;
using System.ComponentModel;

namespace Orders.Shared.Enums
{
    public enum QuestionTypeEnum
    {
        [Description("Selección Múltiple")]
        MultipleChoice,

        [Description("Única Respuesta")]
        SingleResponse,

        [Description("Verdadero/Falso")]
        TrueFalse,

        [Description("Rango de Estrellas")]
        StarRange,

        [Description("Comentario")]
        Comment
    }
}

public class QuestionTypeEnumToList
{
    public static List<string> GetList()
    {
        return Enum.GetNames(typeof(QuestionTypeEnum)).ToList();
    }
}