namespace Application.DTOs;

public class BookDto
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public double Price { get; set; }
    public int AuthorId { get; set; }
    public int BookTypeId { get; set; }
    public DateOnly ReleaseDate { get; set; }
}