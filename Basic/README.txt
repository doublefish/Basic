#生成实体语句
Scaffold-DbContext -Connection "server=127.0.0.1;port=3306;database=Basic;user=root;password=mysql@123456;" -Provider MySql.EntityFrameworkCore -OutputDir Models -f


Scaffold-DbContext -Connection "server=127.0.0.1,1433;database=Basic;user=sa;password=mssql@123456;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -f
