using FluentMigrator;

namespace MyRecipeBook.Infrastructure.Migrations.Versions;

[Migration(DatabaseVersions.USER_TABLE, "Create a table to save the user's information")]
public class Version0000001 : ForwardOnlyMigration
{
    public override void Up()
    {
        
    }
}