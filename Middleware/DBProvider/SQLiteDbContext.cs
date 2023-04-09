﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DatabaseWorker.Model;
using DatabaseWorker.Repository;
using Middleware.Models;

namespace Middleware.DBProvider
{
    public class SQLiteDbContext : IDatabaseContext
    {
        private readonly Mapper mapper;

        public SQLiteDbContext(Mapper mapper)
        {
            this.mapper = mapper;
        }

        public IDatabaseRepository<AppDTO> AppRepository => new RepositoryHolder<AppDTO, App>(new AppRepository(), mapper);
        public IDatabaseRepository<UserDTO> UserRepository => new RepositoryHolder<UserDTO, User>(new UserRepository(), mapper);
        public IDatabaseRepository<CommentDTO> CommentRepository => new RepositoryHolder<CommentDTO, Comment>(new CommentRepository(), mapper);
        public IDatabaseRepository<RecordDTO> RecordRepository => new RepositoryHolder<RecordDTO, Record>(new RecordRepository(), mapper);

        private class RepositoryHolder<T, Y> : IDatabaseRepository<T> where T : class where Y : class
        {
            private IRepository<Y> _Repository;
            private readonly Mapper mapper;

            public RepositoryHolder(IRepository<Y> repository, Mapper mapper)
            {
                _Repository = repository;
                this.mapper = mapper;
            }

            public void Add(T entity)
            {
                _Repository.Add(mapper.Map<Y>(entity));
            }

            public int Count()
            {
                return _Repository.Count();
            }

            public void Delete(T entity)
            {
                _Repository.Delete(mapper.Map<Y>(entity));
            }

            public bool Exists(int key)
            {
                return _Repository.Exists(key);
            }

            public IEnumerable<T> GetAll()
            {
                return mapper.Map<IEnumerable<T>>(_Repository.GetAll());
            }

            public T GetById(int id)
            {
                return mapper.Map<T>(_Repository.GetById(id));
            }

            public void Update(T entity)
            {
                _Repository.Update(mapper.Map<Y>(entity));
            }
        }
    }
}