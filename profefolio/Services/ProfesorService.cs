using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using profefolio.Models.Entities;
using profefolio.Repository;

namespace profefolio.Services
{
    public class ProfesorService : IProfesor
    {
        public Task<Persona> Add(Persona t)
        {
            throw new NotImplementedException();
        }

        public int Count()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Persona Edit(Persona t)
        {
            throw new NotImplementedException();
        }

        public bool Exist()
        {
            throw new NotImplementedException();
        }

        public Task<Persona> FindById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Persona> GetAll(int page, int cantPorPag)
        {
            throw new NotImplementedException();
        }

        public Task Save()
        {
            throw new NotImplementedException();
        }

        public Task<Persona> Save(Persona profesor, int idColegio)
        {
            throw new NotImplementedException();
        }
    }
}