using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.Data.Repositories.Interfaces
{
    /// <summary>
    /// Giving access to context if it is needed
    /// </summary>
    public interface IContextReturnable
    {
        /// <summary>
        /// Let you use context for call
        /// </summary>
        /// <returns>Reference to context</returns>
        BaseContext GetContext();
    }
}
