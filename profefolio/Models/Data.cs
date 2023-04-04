namespace profefolio.Models;

public class Data
{
    public int Id
    {
        get;
        set;
    }

    public bool Deleted
    {
        get;
        set;
    }

    public DateTime Created
    {
        get;
        set;
    }

    public string? CreatedBy
    {
        get;
        set;
    }
    public DateTime Modified
    {
        get;
        set;
    }

    public string? ModifiedBy
    {
        get;
        set;
    }
    
}