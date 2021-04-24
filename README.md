# fluentmigrator sample

## Set the connection string

```c$
// Program.cs
public const string connectionString = "{connectionString}"
```

## Create the migration file

```c#
// db/migrate/2021042201_AddLogTable.cs
[Migration(2021042201)]
public class AddLogTable : Migration
{
    public override void Up()
    {
        Create.Table("Log")
            .WithColumn("Id").AsInt64().PrimaryKey().Identity()
            .WithColumn("Text").AsString();
    }

    public override void Down()
    {
        Delete.Table("Log");
    }
}
```

## Up migration

```bash
$ dotnet run

-------------------------------------------------------------------------------
2021042201: AddLogTable migrating
-------------------------------------------------------------------------------
Beginning Transaction
CreateTable Log
Committing Transaction
2021042201: AddLogTable migrated
```

## Down migration

```bash
$ dotnet run --down 0

-------------------------------------------------------------------------------
2021042201: AddLogTable reverting
-------------------------------------------------------------------------------
Beginning Transaction
DeleteTable Log
Committing Transaction
2021042201: AddLogTable reverted
```
