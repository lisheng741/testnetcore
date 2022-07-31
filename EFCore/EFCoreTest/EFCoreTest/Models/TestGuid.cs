using Microsoft.EntityFrameworkCore;

namespace EFCoreTest.Models;

public class TestGuid : EntityBase<Guid>
{
    public new Guid Id { get; set; }
    public int CreateSequential { get; set; }
}

public class TestGuid1 : EntityBase<Guid>
{
    public new Guid Id { get; set; }
    public int CreateSequential { get; set; }
}

public class TestGuid2 : EntityBase<Guid>
{
    public new Guid Id { get; set; }
    public int CreateSequential { get; set; }
}
