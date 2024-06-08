using HandyMan_.Shared.Entities;


namespace HandyMan_.Shered.DTOs
{
    public class SurveyResponseDTO
    {
        public int Id { get; set; }

        public string UserId { get; set; } = null!;

        public ICollection<AnswersDTO>? Responses { get; set; }
    }
}
