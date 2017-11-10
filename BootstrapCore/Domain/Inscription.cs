using System;
namespace BootstrapCore.Domain
{
    public class Inscription
    {
        public long Id { get; set; }
        public String UserId {get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastUpdated { get; set; }
        public String Name { get; set; }
        public String Status { get; set; }

        public Inscription()
        {
        }
    }
}
