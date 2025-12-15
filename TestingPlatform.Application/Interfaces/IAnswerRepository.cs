using TestingPlatform.Application.DTOS;

namespace TestingPlatform.Application.Interfaces;

public interface IAnswerRepository
{
    /// <summary>
    /// Создать новый ответ.
    /// </summary>
    /// <param name="answerDto">Модель создания нового ответа.</param>
    /// <returns>Идентификатор нового ответа.</returns>
    Task<int> CreateAsync(AnswerDTO answerDto);

    /// <summary>
    /// Обновить информацию об ответе.
    /// </summary>
    /// <param name="answerDto">Модель обновления ответа.</param>
    Task UpdateAsync(AnswerDTO answerDto);
    Task DeleteAsync(int answerId);
}