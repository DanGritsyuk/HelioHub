namespace HelioHub.UserService.Common.Entities
{
    public enum ChangeDbEventType
    {
        Created,  // Создание новой записи
        Updated,  // Обновление существующей записи
        Deleted,  // Удаление записи
        Recovered   // Восстановлен
    }
}
