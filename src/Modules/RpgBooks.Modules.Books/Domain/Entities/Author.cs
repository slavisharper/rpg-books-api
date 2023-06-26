namespace RpgBooks.Modules.Books.Domain.Entities;

using RpgBooks.Libraries.Module.Domain.Entities;

/// <summary>
/// Represents an author of a book.
/// </summary>
public sealed class Author : BaseEntity<int>
{
    private readonly ICollection<Book> books;

    /// <summary>
    /// Initializes a new instance of the <see cref="Author"/> class.
    /// </summary>
    /// <param name="userId">External user Id.</param>
    /// <param name="name">User name.</param>
    /// <param name="alias">Author alias.</param>
    public Author(int userId, string name, string alias)
    {
        this.UserId = userId;
        this.Name = name;
        this.Alias = alias;

        this.books = new HashSet<Book>();
    }

    /// <summary>
    /// Gets the external user Id.
    /// </summary>
    public int UserId { get; private set; }

    /// <summary>
    /// Gets the author name.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Gets the author's alias.
    /// </summary>
    public string Alias { get; private set; }

    /// <summary>
    /// Gets a short description of the author.
    /// </summary>
    public string? Description { get; private set; }

    /// <summary>
    /// Gets the books written by the author.
    /// </summary>
    public IReadOnlyCollection<Book> Books => this.books.ToArray();
}
