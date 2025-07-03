using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Application.Repositories
{
    public interface IPasswordResetToken
    {

        Task SaveAsync( IPasswordResetToken token );
        Task<IPasswordResetToken?> GetTokenAsync(string token);


        Task DeleteAsync(int id);

    }
}
