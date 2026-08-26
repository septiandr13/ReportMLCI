# Database Schema Documentation

Complete database schema documentation for the ReportMLCI project.

---

## Database Overview

**Database Name:** `ReportMLCI`
**Database Type:** SQL Server (LocalDB)
**Connection String:** `Data Source=(localdb)\mssqllocaldb;Initial Catalog=ReportMLCI;Integrated Security=true;`

---

## Tables

### 1. MasterClauses

Main table storing all clause master data.

#### Schema
```sql
CREATE TABLE MasterClauses (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    ClauseCode NVARCHAR(50),
    ClauseTitle NVARCHAR(200),
    ClauseContent NVARCHAR(MAX),
    IsActive BIT DEFAULT 1,
    CreatedAt DATETIME2 DEFAULT GETDATE(),
    UpdatedAt DATETIME2 DEFAULT GETDATE()
);
```

#### Column Details

| Column | Type | Null | Default | Description |
|--------|------|------|---------|-------------|
| Id | UNIQUEIDENTIFIER | NO | NEWID() | Primary key, auto-generated GUID |
| ClauseCode | NVARCHAR(50) | YES | NULL | Code identifier for the clause (e.g., CLAUSE001) |
| ClauseTitle | NVARCHAR(200) | YES | NULL | Human-readable title for the clause |
| ClauseContent | NVARCHAR(MAX) | YES | NULL | Full text content of the clause |
| IsActive | BIT | NO | 1 | Flag to mark clause as active (1) or inactive (0) |
| CreatedAt | DATETIME2 | NO | GETDATE() | Timestamp when record was created |
| UpdatedAt | DATETIME2 | NO | GETDATE() | Timestamp when record was last updated |

#### Indexes
- Primary Key: `Id`

#### Relationships
- One-to-Many with `MasterClausesDetails` (via foreign key `MasterClauseId`)

#### Sample Data
```sql
INSERT INTO MasterClauses (ClauseCode, ClauseTitle, ClauseContent, IsActive)
VALUES 
('CL001', 'Warranty', 'The seller warrants that...', 1),
('CL002', 'Liability', 'In no event shall either party be liable for...', 1);
```

---

### 2. MasterClausesDetails

Sub-clauses or details under a main clause (one-to-many with MasterClauses).

#### Schema
```sql
CREATE TABLE MasterClausesDetails (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    MasterClauseId UNIQUEIDENTIFIER NOT NULL,
    ClauseSubCode NVARCHAR(50),
    ClauseSubTitle NVARCHAR(200),
    ClauseSubContent NVARCHAR(MAX),
    IsActive BIT DEFAULT 1,
    CreatedAt DATETIME2 DEFAULT GETDATE(),
    UpdatedAt DATETIME2 DEFAULT GETDATE(),
    FOREIGN KEY (MasterClauseId) REFERENCES MasterClauses(Id) ON DELETE CASCADE
);
```

#### Column Details

| Column | Type | Null | Default | Description |
|--------|------|------|---------|-------------|
| Id | UNIQUEIDENTIFIER | NO | NEWID() | Primary key, auto-generated GUID |
| MasterClauseId | UNIQUEIDENTIFIER | NO | - | Foreign key to MasterClauses |
| ClauseSubCode | NVARCHAR(50) | YES | NULL | Sub-code identifier |
| ClauseSubTitle | NVARCHAR(200) | YES | NULL | Title for the sub-clause |
| ClauseSubContent | NVARCHAR(MAX) | YES | NULL | Content of the sub-clause |
| IsActive | BIT | NO | 1 | Flag to mark as active/inactive |
| CreatedAt | DATETIME2 | NO | GETDATE() | Timestamp when created |
| UpdatedAt | DATETIME2 | NO | GETDATE() | Timestamp when updated |

#### Indexes
- Primary Key: `Id`
- Foreign Key: `MasterClauseId` (with CASCADE delete)

#### Relationships
- Many-to-One with `MasterClauses`
- One-to-Many with `MasterClausesSubDetails`

#### Sample Data
```sql
INSERT INTO MasterClausesDetails (MasterClauseId, ClauseSubCode, ClauseSubTitle, ClauseSubContent, IsActive)
VALUES 
('550e8400-e29b-41d4-a716-446655440000', 'SUB001', 'Product Warranty', 'Products are warranted for 12 months...', 1);
```

---

### 3. MasterClausesSubDetails

Granular details under clause details (one-to-many with MasterClausesDetails).

#### Schema
```sql
CREATE TABLE MasterClausesSubDetails (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    MasterClauseDetailId UNIQUEIDENTIFIER NOT NULL,
    SubDetailCode NVARCHAR(10),
    SubDetailTitle NVARCHAR(200),
    SubDetailContent NVARCHAR(MAX),
    IsActive BIT DEFAULT 1,
    FOREIGN KEY (MasterClauseDetailId) REFERENCES MasterClausesDetails(Id) ON DELETE CASCADE
);
```

#### Column Details

| Column | Type | Null | Default | Description |
|--------|------|------|---------|-------------|
| Id | UNIQUEIDENTIFIER | NO | NEWID() | Primary key, auto-generated GUID |
| MasterClauseDetailId | UNIQUEIDENTIFIER | NO | - | Foreign key to MasterClausesDetails |
| SubDetailCode | NVARCHAR(10) | YES | NULL | Short code for the sub-detail |
| SubDetailTitle | NVARCHAR(200) | YES | NULL | Title for the sub-detail |
| SubDetailContent | NVARCHAR(MAX) | YES | NULL | Content of the sub-detail |
| IsActive | BIT | NO | 1 | Flag to mark as active/inactive |

#### Indexes
- Primary Key: `Id`
- Foreign Key: `MasterClauseDetailId` (with CASCADE delete)

#### Relationships
- Many-to-One with `MasterClausesDetails`

#### Sample Data
```sql
INSERT INTO MasterClausesSubDetails (MasterClauseDetailId, SubDetailCode, SubDetailTitle, SubDetailContent, IsActive)
VALUES 
('550e8400-e29b-41d4-a716-446655440100', 'SD1', 'Manufacturing defects', 'Covers defects in materials and workmanship...', 1);
```

---

### 4. MasterHeaderClauses

Header/category information for organizing clauses (planned/optional).

#### Schema
```sql
CREATE TABLE MasterHeaderClauses (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    ClauseHeaderCode NVARCHAR(50),
    ClauseHeaderTitle NVARCHAR(200),
    ClauseHeaderDescription NVARCHAR(MAX),
    IsActive BIT DEFAULT 1,
    CreatedAt DATETIME2 DEFAULT GETDATE(),
    UpdatedAt DATETIME2 DEFAULT GETDATE()
);
```

#### Column Details

| Column | Type | Null | Default | Description |
|--------|------|------|---------|-------------|
| Id | UNIQUEIDENTIFIER | NO | NEWID() | Primary key, auto-generated GUID |
| ClauseHeaderCode | NVARCHAR(50) | YES | NULL | Code for the header category |
| ClauseHeaderTitle | NVARCHAR(200) | YES | NULL | Title for the header category |
| ClauseHeaderDescription | NVARCHAR(MAX) | YES | NULL | Description of the header category |
| IsActive | BIT | NO | 1 | Flag to mark as active/inactive |
| CreatedAt | DATETIME2 | NO | GETDATE() | Timestamp when created |
| UpdatedAt | DATETIME2 | NO | GETDATE() | Timestamp when updated |

#### Indexes
- Primary Key: `Id`

---

## Entity Relationship Diagram (ERD)

```
???????????????????????????????????
?   MasterHeaderClauses           ?
???????????????????????????????????
? Id (PK) GUID                    ?
? ClauseHeaderCode NVARCHAR(50)   ?
? ClauseHeaderTitle NVARCHAR(200) ?
? ClauseHeaderDescription NVARCHAR?
? IsActive BIT                    ?
? CreatedAt DATETIME2             ?
? UpdatedAt DATETIME2             ?
???????????????????????????????????
             ?
             ? (1:N)
             ?
             ?
???????????????????????????????????
?   MasterClauses                 ?
???????????????????????????????????
? Id (PK) GUID                    ?
? ClauseCode NVARCHAR(50)         ?
? ClauseTitle NVARCHAR(200)       ?
? ClauseContent NVARCHAR(MAX)     ?
? IsActive BIT                    ?
? CreatedAt DATETIME2             ?
? UpdatedAt DATETIME2             ?
???????????????????????????????????
             ?
             ? (1:N) FK: MasterClauseId
             ? OnDelete: CASCADE
             ?
             ?
???????????????????????????????????
?  MasterClausesDetails           ?
???????????????????????????????????
? Id (PK) GUID                    ?
? MasterClauseId (FK) GUID        ?
? ClauseSubCode NVARCHAR(50)      ?
? ClauseSubTitle NVARCHAR(200)    ?
? ClauseSubContent NVARCHAR(MAX)  ?
? IsActive BIT                    ?
? CreatedAt DATETIME2             ?
? UpdatedAt DATETIME2             ?
???????????????????????????????????
             ?
             ? (1:N) FK: MasterClauseDetailId
             ? OnDelete: CASCADE
             ?
             ?
???????????????????????????????????
?MasterClausesSubDetails          ?
???????????????????????????????????
? Id (PK) GUID                    ?
? MasterClauseDetailId (FK) GUID  ?
? SubDetailCode NVARCHAR(10)      ?
? SubDetailTitle NVARCHAR(200)    ?
? SubDetailContent NVARCHAR(MAX)  ?
? IsActive BIT                    ?
???????????????????????????????????
```

---

## Cascading Behavior

### DELETE CASCADE Rules

When deleting records, the following cascading behavior occurs:

1. **Delete MasterClause**
   - Automatically deletes all related `MasterClausesDetails`
   - Those deletions cascade to delete all related `MasterClausesSubDetails`

2. **Delete MasterClausesDetails**
   - Automatically deletes all related `MasterClausesSubDetails`

3. **Delete MasterClausesSubDetails**
   - No cascade (no child records)

### Example
```sql
-- Deleting a clause
DELETE FROM MasterClauses WHERE Id = '550e8400-e29b-41d4-a716-446655440000'
-- This will also delete:
-- - All MasterClausesDetails records where MasterClauseId matches
-- - All MasterClausesSubDetails records for those details
```

---

## Data Types

### GUID (UNIQUEIDENTIFIER)
- Universally unique 128-bit identifier
- Generated using `NEWID()` function
- Format: `550e8400-e29b-41d4-a716-446655440000`
- Advantages: Globally unique, can generate on client or server

### NVARCHAR
- Unicode variable-length character string
- NVARCHAR(50): Max 50 characters
- NVARCHAR(200): Max 200 characters
- NVARCHAR(MAX): Unlimited length (up to 2 GB)

### BIT
- Boolean type (0 = false, 1 = true)
- Default values: 0 or 1
- Used for IsActive flags

### DATETIME2
- Date and time with precision to 100 nanoseconds
- Format: `YYYY-MM-DD HH:MM:SS.fffffff`
- Good for audit trails and timestamps

---

## Queries

### Find All Clauses with Their Details
```sql
SELECT 
    mc.Id,
    mc.ClauseCode,
    mc.ClauseTitle,
    mcd.Id AS DetailId,
    mcd.ClauseSubCode,
    mcd.ClauseSubTitle,
    mcsd.Id AS SubDetailId,
    mcsd.SubDetailCode,
    mcsd.SubDetailTitle
FROM MasterClauses mc
LEFT JOIN MasterClausesDetails mcd ON mc.Id = mcd.MasterClauseId
LEFT JOIN MasterClausesSubDetails mcsd ON mcd.Id = mcsd.MasterClauseDetailId
WHERE mc.IsActive = 1
ORDER BY mc.ClauseCode, mcd.ClauseSubCode, mcsd.SubDetailCode;
```

### Find Active Clauses Only
```sql
SELECT * FROM MasterClauses WHERE IsActive = 1;
```

### Find Details for Specific Clause
```sql
SELECT * FROM MasterClausesDetails 
WHERE MasterClauseId = '550e8400-e29b-41d4-a716-446655440000'
  AND IsActive = 1;
```

### Find Sub-Details for Specific Clause Detail
```sql
SELECT * FROM MasterClausesSubDetails 
WHERE MasterClauseDetailId = '550e8400-e29b-41d4-a716-446655440100'
  AND IsActive = 1;
```

### Count Records by Clause
```sql
SELECT 
    mc.ClauseCode,
    COUNT(mcd.Id) AS DetailCount,
    SUM(CASE WHEN mcsd.Id IS NOT NULL THEN 1 ELSE 0 END) AS SubDetailCount
FROM MasterClauses mc
LEFT JOIN MasterClausesDetails mcd ON mc.Id = mcd.MasterClauseId
LEFT JOIN MasterClausesSubDetails mcsd ON mcd.Id = mcsd.MasterClauseDetailId
GROUP BY mc.Id, mc.ClauseCode;
```

---

## Performance Considerations

### Indexes
Current indexes are limited to primary keys and foreign keys. Consider adding these for better query performance:

```sql
-- For frequently filtered columns
CREATE INDEX IX_MasterClauses_IsActive ON MasterClauses(IsActive);
CREATE INDEX IX_MasterClauses_ClauseCode ON MasterClauses(ClauseCode);
CREATE INDEX IX_MasterClausesDetails_MasterClauseId_IsActive ON MasterClausesDetails(MasterClauseId, IsActive);
CREATE INDEX IX_MasterClausesSubDetails_MasterClauseDetailId_IsActive ON MasterClausesSubDetails(MasterClauseDetailId, IsActive);
```

### Query Optimization
- Use indexes on foreign keys for JOIN operations
- Filter on IsActive early in queries
- Consider pagination for large result sets
- Use appropriate data types to minimize storage

---

## Backup and Recovery

### Backup Database
```sql
-- Full backup
BACKUP DATABASE ReportMLCI 
TO DISK = 'C:\Backup\ReportMLCI.bak'
WITH INIT, COMP;

-- Differential backup
BACKUP DATABASE ReportMLCI 
TO DISK = 'C:\Backup\ReportMLCI_Diff.bak'
WITH INIT, COMP, DIFFERENTIAL;
```

### Restore Database
```sql
-- From full backup
RESTORE DATABASE ReportMLCI 
FROM DISK = 'C:\Backup\ReportMLCI.bak'
WITH REPLACE;
```

---

## Migration History

### Migrations Executed

Your project uses Entity Framework Core migrations. Check the `Migrations` folder in your project for all executed migrations.

Common migration commands:

```bash
# Add new migration
dotnet ef migrations add MigrationName

# Update database
dotnet ef database update

# Revert last migration
dotnet ef database update PreviousMigration

# List all migrations
dotnet ef migrations list
```

---

## Maintenance

### Regular Maintenance Tasks

1. **Index Maintenance**
   ```sql
   -- Rebuild fragmented indexes
   ALTER INDEX ALL ON MasterClauses REBUILD;
   ALTER INDEX ALL ON MasterClausesDetails REBUILD;
   ALTER INDEX ALL ON MasterClausesSubDetails REBUILD;
   ```

2. **Update Statistics**
   ```sql
   UPDATE STATISTICS MasterClauses;
   UPDATE STATISTICS MasterClausesDetails;
   UPDATE STATISTICS MasterClausesSubDetails;
   ```

3. **Shrink Database (if needed)**
   ```sql
   DBCC SHRINKDATABASE (ReportMLCI, 10);
   ```

---

## Data Constraints

### Business Rules

1. **Active Flag**
   - Default: `IsActive = 1`
   - Soft delete pattern: Set `IsActive = 0` instead of deleting

2. **Timestamps**
   - `CreatedAt` is immutable (set at creation only)
   - `UpdatedAt` should be updated on every modification

3. **Cascade Deletes**
   - Deleting parent always deletes children
   - No orphaned records can exist

4. **Code Fields**
   - Should be unique per level (ClauseCode, ClauseSubCode, SubDetailCode)
   - Consider adding unique constraints if needed:
   ```sql
   ALTER TABLE MasterClauses ADD CONSTRAINT UQ_ClauseCode UNIQUE(ClauseCode);
   ALTER TABLE MasterClausesDetails ADD CONSTRAINT UQ_SubCode UNIQUE(ClauseSubCode, MasterClauseId);
   ```

---

## Security Considerations

1. **Row-Level Security**: Not currently implemented. Consider for multi-tenant scenarios.
2. **Encryption**: Consider encrypting sensitive fields at rest.
3. **Auditing**: Currently only CreatedAt/UpdatedAt. Consider adding user tracking.
4. **Data Validation**: Enforce constraints at both database and application level.

---

**Last Updated:** January 2024
**Database Version:** SQL Server LocalDB
**ORM:** Entity Framework Core
