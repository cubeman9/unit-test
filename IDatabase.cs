using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    public interface IDatabase
    {
        void CreateEntityTables(IEnumerable<string> tables);
        IEntity SelectById(string table, Guid id);
        void Delete(string table, Guid id, bool inTransaction);
        void Save(IEntity obj, bool inTransaction);
        void CommitTransaction();
        void RollbackTransaction();
    }

    public interface IEntity
    {
        Guid Id { get; set; }
        string Tablename { get; }
    }

    public class Database : IDatabase
    {
        public void CreateEntityTables(IEnumerable<string> tables)
        {
            throw new NotImplementedException();
        }

        public IEntity SelectById(string table, Guid id)
        {
            throw new NotImplementedException();
        }

        public void Delete(string table, Guid id, bool inTransaction)
        {
            throw new NotImplementedException();
        }

        public void Save(IEntity obj, bool inTransaction)
        {
            throw new NotImplementedException();
        }

        public void CommitTransaction()
        {
            throw new NotImplementedException();
        }

        public void RollbackTransaction()
        {
            throw new NotImplementedException();
        }
    }

    public class Entity : IEntity
    {
        public Guid Id { get; set; }
        public string Tablename { get; }
    }

    [TestClass]
    public class TestClass
    {
        public void CreateTable()
        {
            IDatabase db = new Database();
            db.CreateEntityTables(null);
        }
        [TestMethod]
        public void SaveEntity()
        {
            IDatabase db = new Database();
            IEntity entity = new Entity();
            db.Save(entity, false);
            IEntity selectedEntity = db.SelectById(entity.Tablename, entity.Id);
            Assert.AreEqual(entity, selectedEntity);
        }
        [TestMethod]
        public void SaveEntityInTransaction()
        {
            IDatabase db = new Database();
            IEntity entity = new Entity();
            db.Save(entity, true);
            IEntity selectedEntity = db.SelectById(entity.Tablename, entity.Id);
            Assert.IsNull(selectedEntity);
            db.CommitTransaction();
            selectedEntity = db.SelectById(entity.Tablename, entity.Id);
            Assert.AreEqual(entity, selectedEntity);
        }
        [TestMethod]
        public void DeleteEntity()
        {
            IDatabase db = new Database();
            IEntity entity = new Entity();
            db.Save(entity, false);
            IEntity selectedEntity = db.SelectById(entity.Tablename, entity.Id);
            Assert.AreEqual(entity, selectedEntity);
            db.Delete(entity.Tablename, entity.Id, false);
            selectedEntity = db.SelectById(entity.Tablename, entity.Id);
            Assert.IsNull(selectedEntity);
        }
        [TestMethod]
        public void DeleteEntityInTransaction()
        {
            IDatabase db = new Database();
            IEntity entity = new Entity();
            db.Save(entity, false);
            db.Delete(entity.Tablename, entity.Id, true);
            IEntity selectedEntity = db.SelectById(entity.Tablename, entity.Id);
            Assert.AreEqual(entity, selectedEntity);
            db.CommitTransaction();
            selectedEntity = db.SelectById(entity.Tablename, entity.Id);
            Assert.IsNull(selectedEntity);
        }
        [TestMethod]
        public void SaveRollback()
        {
            IDatabase db = new Database();
            IEntity entity = new Entity();
            db.Save(entity, true);
            IEntity selectedEntity = db.SelectById(entity.Tablename, entity.Id);
            Assert.IsNull(selectedEntity);
            db.CommitTransaction();
            selectedEntity = db.SelectById(entity.Tablename, entity.Id);
            Assert.AreEqual(entity, selectedEntity);
            db.RollbackTransaction();
            selectedEntity = db.SelectById(entity.Tablename, entity.Id);
            Assert.IsNull(selectedEntity);
        }
        [TestMethod]
        public void DeleteRollback()
        {
            IDatabase db = new Database();
            IEntity entity = new Entity();
            db.Save(entity, false);
            db.Delete(entity.Tablename, entity.Id, true);
            db.CommitTransaction();
            IEntity selectedEntity = db.SelectById(entity.Tablename, entity.Id);
            Assert.IsNull(selectedEntity);
            db.RollbackTransaction();
            selectedEntity = db.SelectById(entity.Tablename, entity.Id);
            Assert.AreEqual(entity, selectedEntity);
        }
    }
}