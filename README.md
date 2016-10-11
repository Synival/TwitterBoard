# TwitterBoard
ASP.NET 4.5 Web Forms-based single-board messager with SQL Backend

# SQL Setup:
Please set the appropriate SQL Connection String and table for your Azure database in **App_Code/MessageHandler/cs**:

```
public class MessageHandler : IHttpHandler {
  ...
  private const string SQL_CONNECT_STRING = "{your connect string}";
  private const string SQL_TABLE          = "{your table}";
```

The following fields are required in the SQL Table:

```
ID:       type=bigint,       non-null, primary key
UserFrom: type=nvarchar(MAX) non-null
Message:  type=nvarchar(140) non-null
```
