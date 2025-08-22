#!/bin/bash

echo "Running database migrations..."


dotnet ef migrations add Init --output-dir Persistence/Migrations/Core --project ./src/Monolith.Core.Infrastructure/Monolith.Core.Infrastructure.csproj --startup-project ./src/Monolith.Core.WebAPI/Monolith.Core.WebAPI.csproj --context CoreDbContext

echo "Running database update migrations..."
# với postgres trước khi chạy lệnh này, cần đảm bảo rằng database đã được tạo
# sử dụng lệnh sau để tạo database
# docker exec -it postgres bash
# createdb -U postgres CoreDb
# \l check database
# \c CoreDb connect to database
# \dt list tables
# \d table_name check table
# \q để thoát khỏi psql

dotnet ef database update --project ./src/Monolith.Core.Infrastructure/Monolith.Core.Infrastructure.csproj --startup-project ./src/Monolith.Core.WebAPI/Monolith.Core.WebAPI.csproj   --context CoreDbContext
 
  



echo "Migration completed."