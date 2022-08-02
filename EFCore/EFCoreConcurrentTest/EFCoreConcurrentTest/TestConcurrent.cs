using System.ComponentModel.DataAnnotations;

namespace EFCoreConcurrentTest;

public class TestConcurrent
{
    public int Id { get; set; }
    public string? Name { get; set; }

    [ConcurrencyCheck]
    public string ConcurrentStamp { get; set; } = default!;
}

public class TestRowVersion
{
    public int Id { get; set; }
    public string? Name { get; set; }

    [Timestamp]
    public byte[] RowVersion { get; set; }
}
