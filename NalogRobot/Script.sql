SELECT * FROM tax;

--drop table tax;
CREATE TABLE tax (
Id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
RegNum TEXT NOT NULL,
TempFile TEXT,
DestFile TEXT,
ImportState INTEGER NOT NULL DEFAULT 0,
Updated TEXT, 
Created TEXT NOT NULL
);

insert into tax (RegNum, TempFile, DestFile, ImportState, Updated, Created) 
values ('RegNum', 'TempFile', 'DestFile', 1, datetime('now'), datetime('now'))