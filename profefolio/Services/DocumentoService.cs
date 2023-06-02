using Microsoft.EntityFrameworkCore;
using profefolio.Models;
using profefolio.Models.Entities;
using profefolio.Repository;

namespace profefolio.Services;

public class DocumentoService : IDocumento
{
    private ApplicationDbContext _dbContext;
    private bool _disposed;

    public DocumentoService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Documento> FindById(int id)
    {
        return await _dbContext.Documentos
            .Where(p => !p.Deleted && p.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task<Documento> FindByNameDocumento(string n, int idML)
    {
        return await _dbContext.Documentos
            .Where(p => !p.Deleted)
            .Where(p => p.Nombre == n)
            .Where(p => p.MateriaListaId == idML)
            .FirstOrDefaultAsync();
    }

    public Documento Edit(Documento t)
    {
        _dbContext.Entry(t).State = EntityState.Modified;
        return t;
    }

    public async Task<Documento> Add(Documento t)
    {
        var result = await _dbContext.Documentos.AddAsync(t);
        return result.Entity;
    }

    public async Task Save()
    {
        await _dbContext.SaveChangesAsync();
    }

    public int Count()
    {
        return _dbContext.Documentos
            .Count(p => !p.Deleted);
    }

    public bool Exist()
    {
        return Count() > 0;
    }
    protected virtual void Dispose(bool disposing)
    {
        if (!this._disposed)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
        }
        this._disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public IEnumerable<Documento> GetAll(int page, int cantPorPag)
    {
        return _dbContext.Documentos
         .Where(p => !p.Deleted)
         .Skip(page * cantPorPag)
         .Take(cantPorPag);
    }

    /*
    La lista de Documento resultante contiene los documentos que pertenecen
    a los registros de MateriaLista con el mismo idMateria que se pasó como parámetro y que  
     ProfesorId  == idPrf
    */
    public async Task<List<Documento>> GetAll(int idMateria, string idPrf)
    {
        var materiasListaIds = await _dbContext.MateriaListas
            .Where(ml => ml.MateriaId == idMateria && ml.ProfesorId == idPrf)
            .Select(ml => ml.Id)
            .ToListAsync();

        return await _dbContext.Documentos
            .Where(p => !p.Deleted)
            .Where(p => materiasListaIds.Contains(p.MateriaListaId))
            .ToListAsync();
    }


    /**
    * Retorna true si el idPrf pasado es igual al ProfesorId de MateriaLista
    **/
    public async Task<bool> FindProfesorIdByDocumento(int idMateriaLista, string idPrf)
    {
        var materiaLista = await _dbContext.MateriaListas
            .FirstOrDefaultAsync(ml => ml.Id == idMateriaLista && ml.ProfesorId == idPrf);

        return materiaLista != null;
    }

    public async Task<bool> FindProfesorOfDocumento(int idDocumento, string mailProfesor){
         var isDoc = await _dbContext.Documentos
            .FirstOrDefaultAsync(ml => ml.Id == idDocumento && ml.CreatedBy == mailProfesor);
        return isDoc != null;
    }


}