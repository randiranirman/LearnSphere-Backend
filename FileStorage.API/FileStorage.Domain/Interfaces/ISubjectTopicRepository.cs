using FileStorage.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileStorage.Domain.Interfaces
{
    public interface ISubjectTopicRepository
    {
        public Task<IEnumerable<SubjectTopicEntity>> GetTopicsBySubjectIdAsync(int subjectId);
    }
}
