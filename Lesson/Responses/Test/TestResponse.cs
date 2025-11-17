using TestingPlatform.Domain.Enums;

namespace practice.Responses.Test;

/// <summary>
/// Краткая модель теста
/// </summary>
public class TestResponse
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Название
    /// </summary>
    public string Title { get; set; }
    
    /// <summary>
    /// Описание
    /// </summary>
    public string Description { get; set; }
    
    /// <summary>
    /// Можно ли пройти тест больше одного раза
    /// </summary>
    public bool IsRepeatable { get; set; }
    
    /// <summary>
    /// Тип теста
    /// </summary>
    public TestType Type { get; set; }
    
    /// <summary>
    /// Опубликован ли для студентов
    /// </summary>
    public bool IsPublic { get; set; }
    
    /// <summary>
    /// Дата создания
    /// </summary>
    public DateTime CreatedAt { get; set; }
}