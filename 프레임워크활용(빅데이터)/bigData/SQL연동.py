import pymysql
import pandas as pd

conn = pymysql.connect(
    host = 'localhost',
    user = 'taesub',
    password = '0204',
    database = 'library',
    charset = 'utf8mb4'
)

query = """
SELECT *
FROM students;
"""

df = pd.read_sql(query, conn)

print(df)
conn.close()
