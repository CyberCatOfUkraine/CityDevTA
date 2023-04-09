using AutoMapper;
using DatabaseWorker.Model;
using DatabaseWorker.Repository;
using Middleware.DBProvider;
using Middleware.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Middleware
{
    internal class SQLiteMappingProfile : Profile
    {
        public SQLiteMappingProfile()
        {
            CreateMap<App, AppDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<Comment, CommentDTO>().ReverseMap();
            CreateMap<Record, RecordDTO>().ReverseMap();
        }
    }
}