using System;
namespace BootstrapCore
{
    public class DataInitializer
    {
        private AppDbContext _db;

        public DataInitializer(AppDbContext ctx)
        {
            _db = ctx;
        }

        public void SeedData(){
            _db.Inscriptions.Add(new Domain.Inscription(){Id=1,UserId="BEBIS",CreationDate=DateTime.Now.AddDays(-5),LastUpdated=DateTime.Now,Status="PENDING"});
            _db.SaveChanges();
        }
    }
}
