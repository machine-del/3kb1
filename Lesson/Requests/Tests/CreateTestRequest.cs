using System.ComponentModel.DataAnnotations;
using TestingPlatform.Domain.Enums;

namespace practice.Requests.Test;

/// <summary>
/// Модель создания теста
/// </summary>
public class CreateTestRequest
{
    /// <summary>
    /// Название теста
    /// </summary>
    [Required]
    public string Title { get; set; }
    
    /// <summary>
    /// Описание теста
    /// </summary>
    [Required]
    public string Description { get; set; }
    
    /// <summary>
    /// Можно ли пройти тест больше одного раза
    /// </summary>
    public bool IsRepeatable { get; set; }
    
    /// <summary>
    /// Тип теста
    /// </summary>
    [Required]
    public TestType Type { get; set; }
    
    /// <summary>
    /// Дата публикации теста
    /// </summary>
    [Required]
    public DateTime PublishedAt { get; set; }
    
    /// <summary>
    /// Дата, до которой можно пройти тест
    /// </summary>
    [Required]
    public DateTime Deadline { get; set; }
    
    /// <summary>
    /// Время на прохождения теста (если нужно)
    /// </summary>
    public int? DurationMinutes { get; set; }
    
    /// <summary>
    /// Количество очков для прохождения тест (если нужно)
    /// </summary>
    public int? PassingScore { get; set; }
    
    /// <summary>
    /// Максимально количество попыток прохождения (если нужно)
    /// </summary>
    public int? MaxAttempts { get; set; }
    
    /// <summary>
    /// Идентификаторы студентов (для кого предназначен тест)
    /// </summary>
    public List<int>? Students { get; set; }
    
    /// <summary>
    /// Идентификаторы проектов (для которых предназначен тест)
    /// </summary>
    public List<int>? Projects { get; set; }
    
    /// <summary>
    /// Идентификаторы курсов (для которых предназначен тест)
    /// </summary>
    public List<int>? Courses { get; set; }
    
    /// <summary>
    /// Идентификаторы групп (для которых предназначен тест)
    /// </summary>
    public List<int>? Groups { get; set; }
    
    /// <summary>
    /// Идентификаторы направлений (для которых предназначен тест)
    /// </summary>
    public List<int>? Directions { get; set; }
}