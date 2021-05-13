using System;
using System.Linq;
using EFCore_DatabaseFirst.Db;

namespace EFCore_DatabaseFirst
{
    class Program
    {
        static void Main(string[] args)
        {
            DeleteRecord(1);
            Console.WriteLine($"Record count: {GetRecordCount()}");
        }

        static readonly DbFirstDemoContext _dbContext = new DbFirstDemoContext();

        static int GetRecordCount()
        {
            return _dbContext.People.ToList().Count;
        }

        static void AddRecord()
        //CREATE
        {
            var person = new Person 
            { 
                FirstName = "Vjor",
                LastName = "Durano",
                DateOfBirth = Convert.ToDateTime("19/06/2020")
            };
            _dbContext.Add(person);
            _dbContext.SaveChanges();
        }

        static Person GetRecord(int id)
        // READ
        {
            return _dbContext.People.SingleOrDefault(p => p.Id.Equals(id));
        }
        static void UpdateRecord(int id)
        // UPDATE
        {
            var person = _dbContext.People.Find(id);
            person.FirstName = "Vynn Markus";
            person.DateOfBirth = Convert.ToDateTime("22/11/2016");

            _dbContext.Update(person);
            _dbContext.SaveChanges();
        }

        static void DeleteRecord(int id)
        // DELETE
        {
            var person = _dbContext.People.Find(id);
            if (person != null)
            {
                _dbContext.Remove(person);
                _dbContext.SaveChanges();
            }
        }
    }
}
