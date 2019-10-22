using StundenVerwaltung.Model;
using System.Collections.Generic;

namespace StundenVerwaltung.Services
{
    public interface IDataProvider
    {
        List<IPersonal> GetAllPersonal();
        IPersonal GetPersonalById(string id);
        bool Insert(IPersonal personal);
        bool Update(IPersonal personal);
        bool Delete(IPersonal personal);
    }
}
