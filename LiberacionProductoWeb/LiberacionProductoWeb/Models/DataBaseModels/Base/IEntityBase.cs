using System;
namespace LiberacionProductoWeb.Models.DataBaseModels.Base
{
    public interface IEntityBase<TId>
    {
        TId Id { get; }
    }
}
