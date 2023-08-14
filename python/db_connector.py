import sqlite3
import pandas as pd


def main():
    # pd.set_option('display.max_columns', None)
    # pd.set_option('display.width', 1000)
    connector = DBConnector()
    connector.insert_user()
    # connector.select_pandas()
    # connector.drop_table("TB_STUDENT")


class DBConnector:
    def __init__(self):
        self.conn = None

    def start_conn(self):
        self.conn = sqlite3.connect("new_db.sqlite")
        return self.conn.cursor()

    def end_conn(self):
        if self.conn is not None:
            self.conn.close()

    def commit_db(self):
        self.conn.commit()

    def insert_user(self):
        self.start_conn()
        new_dict = dict()
        new_dict.update({"first_name": "박"})
        new_dict.update({"last_name": "광현"})
        df = pd.DataFrame(new_dict, index=[0])
        df.to_sql("TB_STUDENT", self.conn, if_exists="append", index=False)
        self.end_conn()

    def select_pandas(self):
        pstmt = "select * from TB_STUDENT"
        df = pd.read_sql(pstmt, self.conn)
        print(df["AGE"].to_string(index=False))

    def drop_table(self, table_name):
        pstmt = f"drop table {table_name}"
        cursor = self.start_conn()
        cursor.execute(pstmt)
        self.commit_db()
        self.end_conn()


if __name__ == '__main__':
    main()
