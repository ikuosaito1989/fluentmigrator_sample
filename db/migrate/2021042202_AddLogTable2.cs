using FluentMigrator;

namespace fluentmigrator_sample
{
    [Migration(2021042202)]
    public class AddLogTable1 : Migration
    {
        public override void Up()
        {
            Create.Table("Log2")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Text").AsString();
        }

        public override void Down()
        {
            Delete.Table("Log2");
        }
    }
}