using System;

namespace Licenta.EntityFramework
{
    public interface IMaintainableEntity
    {
        int Id { get; set; }
        int RowVersion { get; set; }
        DateTime? DateDeleted { get; set; }
    }
}