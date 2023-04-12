using DatabaseWorker.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Data.Entity.Infrastructure.Design.Executor;

namespace DatabaseWorker
{
    internal static class SQLiteTemplate
    {
        /// <summary>
        /// Цей запит поверне назву таблиці, якщо вона існує в базі даних, або нічого не поверне, якщо таблиці не існує
        /// </summary>
        /// <param name="tableName">Назва таблиці</param>
        /// <returns></returns>
        public static string CheckTableExistQuery(string tableName) => $"SELECT name FROM sqlite_master WHERE type='table' AND name='{tableName}';";

        #region Створення таблиць якщо вони не були створені

        public static string CreateAppTableTemplate = $"CREATE TABLE IF NOT EXISTS {nameof(App)} ({nameof(App.Name)} TEXT NOT NULL)";
        public static string CreateCommentTableQuery = $"CREATE TABLE IF NOT EXISTS {nameof(Comment)} ({nameof(Comment.Text)} TEXT NOT NULL, {nameof(Comment.TimeStamp)} TEXT NOT NULL)";
        public static string CreateUserTableQuery = $"CREATE TABLE IF NOT EXISTS {nameof(User)} ({nameof(User.Name)} TEXT NOT NULL)";
        public static string CreateRecordTableQuery = $"CREATE TABLE IF NOT EXISTS {nameof(Record)} ( {nameof(Record.App) + "Id"} INTEGER NOT NULL, {nameof(Record.User) + "Id"} INTEGER NOT NULL, {nameof(Record.Comment) + "Id"} INTEGER NOT NULL )";

        #endregion Створення таблиць якщо вони не були створені

        public static string SelectAllFromQuery(string tableName) => $"SELECT ROWID,* FROM {tableName}";

        public static string GetByIdQuery(string tableName, int id) => $"SELECT ROWID,* FROM {tableName} WHERE ROWID={id}";

        public static string RemoveByID(string tableName, int id) => $"DELETE FROM {tableName} WHERE ROWID = {id}";

        #region Оновлення даних

        public static string UpdateAppQuery(App app) => $"UPDATE {nameof(App)} SET {nameof(App.Name)} = '{app.Name}' WHERE ROWID = {app.Id}";

        public static string UpdateCommentQuery(Comment comment) => $"UPDATE {nameof(Comment)} SET {nameof(Comment.Text)} = '{comment.Text}', {nameof(Comment.TimeStamp)} = '{comment.TimeStamp.ToString(CultureInfo.InvariantCulture)}' WHERE ROWID = {comment.Id}";

        public static string UpdateUserQuery(User user) => $"UPDATE {nameof(User)} SET {nameof(User.Name)} = '{user.Name}' WHERE ROWID = {user.Id}";

        public static string UpdateRecordQuery(Record record) => $"UPDATE {nameof(Record)} SET {nameof(Record.App)}Id = {record.App.Id}, {nameof(Record.User)}Id = {record.User.Id}, {nameof(Record.Comment)}Id = {record.Comment.Id} WHERE ROWID = {record.Id}";

        #endregion Оновлення даних

        #region Додавання сутностей

        public static string InsertAppQuery(App app) => $"INSERT INTO {nameof(App)} ({nameof(App.Name)}) VALUES ('{app.Name}')";

        public static string InsertCommentQuery(Comment comment) => $"INSERT INTO {nameof(Comment)} ({nameof(Comment.Text)}, {nameof(Comment.TimeStamp)}) VALUES ('{comment.Text}', '{comment.TimeStamp.ToString(CultureInfo.InvariantCulture)}')";

        public static string InsertUserQuery(User user) => $"INSERT INTO {nameof(User)} ({nameof(User.Name)}) VALUES ('{user.Name}')";

        public static string InsertRecordQuery(Record record) => $"INSERT INTO {nameof(Record)} ({nameof(Record.App)}Id, {nameof(Record.User)}Id, {nameof(Record.Comment)}Id) VALUES ('{record.App.Id}', '{record.User.Id}', '{record.Comment.Id}')";

        #endregion Додавання сутностей

        public static string GetRowsCountQuery(string tableName) => $"SELECT COUNT(*) FROM {tableName}; ";
    }
}