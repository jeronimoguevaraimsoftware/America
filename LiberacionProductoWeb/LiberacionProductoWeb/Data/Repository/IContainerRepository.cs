using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiberacionProductoWeb.Data.Repository
{
    public interface IContainerRepository: IDisposable
    {
        IExampleRepository exampleRepository { get; }

        void Save();
    }
}
