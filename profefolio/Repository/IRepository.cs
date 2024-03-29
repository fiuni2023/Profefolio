﻿namespace profefolio.Repository;

public interface IRepository<T> : IDisposable
{
    Task<T> FindById(int id);
    IEnumerable<T> GetAll(int page, int cantPorPag);
    T Edit(T t);
    Task<T>Add(T t);
    Task Save();
    int Count();
    bool Exist();

}