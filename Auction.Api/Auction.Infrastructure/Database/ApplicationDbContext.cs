using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace Auction.Infrastructure.Database;

public class ApplicationDbContext(DbContextOptions options) : DbContext(options)
{
    
}