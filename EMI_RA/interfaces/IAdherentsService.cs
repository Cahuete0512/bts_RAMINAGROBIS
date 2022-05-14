using System.Collections.Generic;

namespace EMI_RA
{
    public interface IAdherentsService
    {
        public List<Adherents> GetAllAdherents();
        public Adherents GetByID(int idAdherents);
        public Adherents Insert(Adherents a);
        public Adherents Update(Adherents a);
        public void Delete(Adherents a);
    }
}
