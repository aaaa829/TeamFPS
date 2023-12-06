import pymysql.cursors


def connect():
    connection = pymysql.connect(
        host="localhost",
        user="root",
        password="root",
        database="fps_db",
        cursorclass=pymysql.cursors.DictCursor,
    )
    return connection


def find_all():
    result = None
    with connect() as con:
        with con.cursor() as cursor:
            sql = "SELECT * FROM score_list ORDER BY `score` DESC"
            cursor.execute(sql)
            result = cursor.fetchall()
            # print(result)
    return result

def insert_score(game_score):
    with connect() as con:
        with con.cursor() as cursor:
            sql = "INSERT INTO score_list(`player_name`, `score`, `time_score`, `score_date`) VALUES(%s,%s,%s,%s)"
            cursor.execute(sql, (game_score["player_name"], game_score["score"], game_score["time_score"], game_score["score_date"]))
        con.commit()


def insert_player(player_info):
    with connect() as con:
        with con.cursor() as cursor:
            sql = "INSERT INTO player_list(`player_name`, `player_pass`, `created_date`) VALUES(%s,%s,%s)"
            cursor.execute(sql, (player_info["player_name"], player_info["player_pass"], player_info["created_date"]))
        con.commit()

def login_player(player_info):
    result = None
    with connect() as con:
        with con.cursor() as cursor:
            sql = "SELECT s.score as score FROM score_list AS s JOIN player_list AS p ON s.player_name=p.player_name WHERE p.player_name=%s AND p.player_pass=%s ORDER BY score DESC LIMIT 3"
            # sql = "SELECT p.player_name FROM player_list as p WHERE `player_name`=%s AND `player_pass`=%s"
            # sql = "SELECT * FROM player_list WHERE `player_name`=%s AND `player_pass`=%s"
            cursor.execute(sql, (player_info["player_name"], player_info["player_pass"] ))
            result = cursor.fetchall()
    return result