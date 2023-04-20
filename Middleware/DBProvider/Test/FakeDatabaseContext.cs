using Middleware.Models;
using System.Reflection;

namespace Middleware.DBProvider.Test
{
    public class FakeDatabaseContext : IDatabaseContext
    {
        public IDatabaseRepository<AppDTO> AppRepository => new FakeRepository<AppDTO>();

        public IDatabaseRepository<UserDTO> UserRepository => new FakeRepository<UserDTO>();

        public IDatabaseRepository<CommentDTO> CommentRepository => new FakeRepository<CommentDTO>();

        public IDatabaseRepository<RecordDTO> RecordRepository => new FakeRepository<RecordDTO>();

        private class FakeRepository<T> : IDatabaseRepository<T> where T : class
        {
            private static FakeRepository<T> _FakeRepository;
            private static List<T> list = new();

            private static FakeRepository<T> GetInstance()
            {
                _FakeRepository ??= new();
                return _FakeRepository;
            }

            public void Add(T entity)
            {
                dynamic y = entity;
                if (typeof(T)!.GetProperty("Id") != null)
                {
                    y.Id = list.Count;
                }
                else if (typeof(T)!.GetProperty("id") != null)
                {
                    y.id = list.Count;
                }
                list.Add(entity);
            }

            public int Count()
            {
                return list.Count;
            }

            public void Delete(T entity)
            {
                list.Remove(entity);
            }

            public bool Exists(int id)
            {
                return list.Exists(x =>
                {
                    dynamic y = x;
                    return (typeof(T)!.GetProperty("Id") != null && y.Id == id) || (typeof(T)!.GetProperty("id") != null && y.id == id);
                });
            }

            public IEnumerable<T> GetAll()
            {
                return list;
            }

            public T GetById(int id)
            {
                if (!Exists(id))
                {
                    return null!;
                }

                return list.First(x =>
                {
                    dynamic y = x;
                    return (typeof(T)!.GetProperty("Id") != null && y.Id == id) || (typeof(T)!.GetProperty("id") != null && y.id == id);
                });
            }
            public void Update(T entity)
            {
                if (entity.GetType().GetProperty("Id") != null)
                {
                    var members = entity.GetType().GetMembers()
                        .Where(member => member.MemberType == MemberTypes.Field || member.MemberType == MemberTypes.Property)
                        .Where(member => member.Name != "Id");

                    foreach (var member in members)
                    {
                        foreach (var item in list)
                        {
                            if (item.GetType().GetProperty("Id") != null && item.GetType().GetProperty("Id").GetValue(item).Equals(entity.GetType().GetProperty("Id").GetValue(entity)))
                            {
                                if (member.MemberType == MemberTypes.Field)
                                {
                                    var field = item.GetType().GetField(member.Name);
                                    var value = field.GetValue(entity);
                                    field.SetValue(item, value);
                                }
                                else if (member.MemberType == MemberTypes.Property)
                                {
                                    var prop = item.GetType().GetProperty(member.Name);
                                    var value = prop.GetValue(entity);
                                    prop.SetValue(item, value);
                                }
                            }
                        }
                    }
                }
                else if (entity.GetType().GetProperty("id") != null)
                {
                    var members = entity.GetType().GetMembers()
                        .Where(member => member.MemberType == MemberTypes.Field || member.MemberType == MemberTypes.Property)
                        .Where(member => member.Name != "id");

                    foreach (var member in members)
                    {
                        foreach (var item in list)
                        {
                            if (item.GetType().GetProperty("id") != null && item.GetType().GetProperty("id").GetValue(item).Equals(entity.GetType().GetProperty("id").GetValue(entity)))
                            {
                                if (member.MemberType == MemberTypes.Field)
                                {
                                    var field = item.GetType().GetField(member.Name);
                                    var value = field.GetValue(entity);
                                    field.SetValue(item, value);
                                }
                                else if (member.MemberType == MemberTypes.Property)
                                {
                                    var prop = item.GetType().GetProperty(member.Name);
                                    var value = prop.GetValue(entity);
                                    prop.SetValue(item, value);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}