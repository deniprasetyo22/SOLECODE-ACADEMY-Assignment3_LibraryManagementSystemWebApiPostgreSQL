using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Assignment3_LibraryManagementSystemWebApiPostgreSQL.Models;

[Table("books")]
[Index("Isbn", Name = "books_isbn_key", IsUnique = true)]
public partial class Book
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("title")]
    [StringLength(255)]
    public string Title { get; set; } = null!;

    [Column("author")]
    [StringLength(255)]
    public string Author { get; set; } = null!;

    [Column("publicationyear")]
    public int Publicationyear { get; set; }

    [Column("isbn")]
    [StringLength(255)]
    public string Isbn { get; set; } = null!;
}
