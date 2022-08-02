using EFCoreTest.Models;
using System.ComponentModel.DataAnnotations;

namespace EFCoreMySqlConcurrencyTest.Models;


public class TestRowVersion : EntityBase
{
    public int Id { get; set; }
    public string? Name { get; set; }

    [Timestamp]
    public byte[] RowVersion { get; set; }
}

public class TestConcurrency : EntityBase
{
    public int Id { get; set; }
    public string? Name { get; set; }

    [ConcurrencyCheck]
    public long RowVersion { get; set; }
}

