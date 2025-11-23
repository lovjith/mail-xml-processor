public class EmailRecord
{
    public int Id { get; set; }
    public int EmailIndex { get; set; }

    public string PayloadJson { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
