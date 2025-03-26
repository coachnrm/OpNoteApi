using System;
using Microsoft.EntityFrameworkCore;
using OpNoteApi.Models;

namespace OpNoteApi.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
    {
        
    }
    public DbSet<OpNote> OpNotes {get; set;}
    public DbSet<OpTemplate> OpTemplates {get; set;}
    public DbSet<OpPicture> OpPictures {get; set;}
}
