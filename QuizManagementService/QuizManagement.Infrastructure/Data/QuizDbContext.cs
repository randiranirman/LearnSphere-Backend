using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace QuizManagement.Infrastructure.Data
{
    public  class QuizDbContext(DbContextOptions<QuizDbContext> options): DbContext(options)
    {




    }
}
