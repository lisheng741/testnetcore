using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelationshipTest.Model;

public class DeleteTest : IDeleteEntity
{
    public int Id { get; set; }
    public bool Deleted { get; set; }
}
