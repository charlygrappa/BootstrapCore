using System;
using System.Collections.Generic;
using BootstrapCore.Domain;
using System.Linq;
namespace BootstrapCore.Services
{
    public class InscriptionService
    {
        AppDbContext _db;
        public InscriptionService(AppDbContext ctx)
        {
            _db = ctx;
        }

        public List<Inscription> GetInscriptionsByUserId(String UserId, int Offset=0, int Limit=1){

            return _db.Inscriptions.Where(i=> i.UserId == UserId).Skip(Offset).Take(Limit).ToList();
        }
    }
}
