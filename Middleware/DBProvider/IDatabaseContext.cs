using AutoMapper;
using DatabaseWorker.Model;
using Middleware.Models;

namespace Middleware.DBProvider
{
    public interface IDatabaseContext
    {
        public IDatabaseRepository<AppDTO> AppRepository { get; }
        public IDatabaseRepository<UserDTO> UserRepository { get; }
        public IDatabaseRepository<CommentDTO> CommentRepository { get; }
        public IDatabaseRepository<RecordDTO> RecordRepository { get; }
    }
}