using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelationshipTest.Model;

public interface IDeleteEntity
{
    bool Deleted { get; set; }
}
