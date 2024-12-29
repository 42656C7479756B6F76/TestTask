using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations;

public class AuthorService : IAuthorService
{
    private readonly ApplicationDbContext _context;
    
    public AuthorService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Author> GetAuthor()
    {
        return await _context.Authors
            .OrderByDescending(x => x.Books.Max(b => b.Title.Length))
            .FirstOrDefaultAsync();
    }

    public async Task<List<Author>> GetAuthors()
    {
        const int minYear = 2015;
        return await _context.Authors
            .Where(a =>
                a.Books.Count(b => b.PublishDate.Year > minYear) > 0 &&
                a.Books.Count % 2 == 0)
            .ToListAsync();
    }
}