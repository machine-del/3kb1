using TestingPlatform.Domain.Enums;

namespace practice.Responses.Test;

public class TestForStudentResponse
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; set; }
    
    /// Название теста
    /// </summary>
    public string Title { get; set; }
    
    /// <summary>
    /// Описание теста
    /// </summary>
    public string Description { get; set; }
    
    /// <summary>
    /// Можно ли тест пройти повторно или есть только одна попытка
    /// </summary>
    public bool IsRepeatable { get; set; }
    
    /// <summary>
    /// Тип теста - образовательный, дополнительные активности, другое
    /// </summary>
    public TestType Type { get; set; }
    
    /// <summary>
    /// Дата создания
    /// </summary>
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// Когда опубликован (стал доступен студентам)
    /// </summary>
    public DateTime PublishedAt { get; set; }
    
    /// <summary>
    /// Срок выполнения теста
    /// </summary>
    public DateTime Deadline { get; set; }
    
    /// <summary>
    /// Время на выполнение теста
    /// </summary>
    public int? DurationMinutes { get; set; }
    
    /// <summary>
    /// Проходной балл (достигнув которого тест больше нельзя пройти)
    /// </summary>
    public int? PassingScore { get; set; }
    
    /// <summary>
    /// Максимальное количество попыток прохождения теста
    /// </summary>
    public int? MaxAttempts { get; set; }
}