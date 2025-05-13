namespace MyRecipeBook.Domain.Entities;
public class EntityBase 
{
    public long Id { get; set; } 
    public bool Active { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow; //UtcNow, quero a data base independente da regiao

}
   

