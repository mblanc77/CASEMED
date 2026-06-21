# Copilot Instructions

## Project Guidelines
- User does not want a real-time parser for Access queries; prefers pre-generated SQL scripts (stored procedures/functions) that are called from code with parameters to mimic VB6 Recordset experience.
- User requires SQL Server-valid generated scripts without blind conversion errors; specifically avoid Access-style double-quoted string literals and invalid DELETE field-list syntax.
- User wants an intelligent (not blind) Access-to-SQL conversion, prioritizing semantic correctness over mechanical translation.
- User asked to not modify query names for now; if queries error due to missing objects, create missing tables from specs.txt definitions instead.
- User prefers receiving an estimated count of remaining rounds during ongoing SQL-fix iterations.