using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using profefolio.Models;
using profefolio.Models.Entities;
using profefolio.Repository;

namespace profefolio.Services
{
    public class ContactoEmergenciaService : IContactoEmergencia
    {

        public readonly ApplicationDbContext _context;
        public ContactoEmergenciaService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ContactoEmergencia> Add(ContactoEmergencia t)
        {
            var result = await _context.ContactosEmergencias.AddAsync(t);
            return result.Entity;
        }

        public int Count()
        {
            throw new NotImplementedException();
        }

        public async void Dispose()
        {
            await _context.DisposeAsync();
        }

        public ContactoEmergencia Edit(ContactoEmergencia t)
        {
            _context.Entry(t).State = EntityState.Modified;
            return t;
        }

        public bool Exist()
        {
            throw new NotImplementedException();
        }

        public Task<ContactoEmergencia> FindById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ContactoEmergencia> GetAll(int page, int cantPorPag)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ContactoEmergencia>> GetAllByAlumno(string idAlumno)
        {
            var alumno = await _context.Users.Where(a => !a.Deleted).FirstOrDefaultAsync();
            if(alumno == null){
                throw new FileNotFoundException("Alumno no encontrado");
            }
            return await _context.ContactosEmergencias.Where(a => !a.Deleted && idAlumno.Equals(a.AlumnoId)).ToListAsync();
        }

        public Task Save()
        {
            return _context.SaveChangesAsync();
        }
    }
}