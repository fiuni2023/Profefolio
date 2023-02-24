namespace profefolio.Models.DTOs;


public class DataListDTO <T>
{
    public List<T> DataList
    {
        get;
        set;
    } = new List<T>();

    public int CurrentPage
    {
        get;
        set;
    }

    public int TotalPage
    {
        get;
        set;
    }

    public bool Next
    {
        get;
        set;
    }
}