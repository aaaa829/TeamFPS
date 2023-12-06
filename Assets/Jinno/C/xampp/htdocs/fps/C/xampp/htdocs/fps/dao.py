import pymysql.cursors
from datetime import datetime, date, time

def connect():
    try:
        connection = pymysql.connect(
            host="localhost",
            user="root",
            password="root",
            database="fps_db",
            cursorclass=pymysql.cursors.DictCursor,
        )
    except Exception or pymysql.Error() as err:
        print(f"Error: '{err}'")
    return connection

def insert_user(user_info):
    result = list()
    with connect() as con:
        try:
            with con.cursor() as cursor:
                sql = "INSERT IGNORE INTO user_list(user_name, user_pass) VALUES(%s,%s)"
                cursor.execute(sql, (user_info["user_name"], user_info["user_pass"]))
                result = cursor.fetchall()
            con.commit()
        except Exception or pymysql.Error() as err:
            result.append("Error: " + err)
            # raise Exception
    return result

def check_user_name(user_info):
    result = list()
    with connect() as con:
        try:
            with con.cursor() as cursor:
                sql = "SELECT user_name FROM user_list WHERE user_name = %s"
                cursor.execute(sql, (user_info["user_name"]))
                result = cursor.fetchall()
        except Exception or pymysql.Error() as err:
            result.append("Error: " + err)
    return result

def login_user(user_info):
    result = list()
    with connect() as con:
        try:
            with con.cursor() as cursor:
                sql = "SELECT user_name FROM user_list WHERE user_name = %s AND user_pass = %s"
                cursor.execute(sql, (user_info["user_name"], user_info["user_pass"] ))
                result = cursor.fetchall()
        except Exception or pymysql.Error() as err:
            result.append("Error: " + err)
    return result

def check_user_record(user_info):
    result = list()
    with connect() as con:
        try:
            with con.cursor() as cursor:
                sql = "SELECT ss.score_date, ss.user_name, ss.score, ss.kill_cnt, ss.item_cnt, ss.time_score FROM score_list AS ss JOIN user_list AS uu ON ss.user_name=uu.user_name WHERE uu.user_name = %s AND uu.user_pass = %s ORDER BY ss.score DESC, ss.kill_cnt DESC LIMIT 3"
                cursor.execute(sql, (user_info["user_name"], user_info["user_pass"] ))
                result = cursor.fetchall()
            con.commit()
        except Exception or pymysql.Error() as err:
            result.append("Error: " + err)
    return result

def check_user_summary(user_info):
    result = list()
    with connect() as con:
        try:
            with con.cursor() as cursor:
                sql = "SELECT s.user_name, SUM(s.score) AS total_score, SUM(s.kill_cnt) AS total_kill_cnt, SUM(s.item_cnt) AS total_item_cnt, SUM(s.time_score) AS total_time_score FROM score_list AS s JOIN user_list AS u ON s.user_name=u.user_name WHERE u.user_name = %s AND u.user_pass = %s GROUP BY u.user_name"
                cursor.execute(sql, (user_info["user_name"], user_info["user_pass"] ))
                result = cursor.fetchall()
            con.commit()
        except Exception or pymysql.Error() as err:
            result.append("Error: " + err)
    return result

def login_status(user_info):
    result = dict()
    with connect() as con:
        try:
            with con.cursor() as cursor:
                sql1 = "SELECT s.user_name, SUM(s.score) AS total_score, SUM(s.kill_cnt) AS total_kill_cnt, SUM(s.item_cnt) AS total_item_cnt, SUM(s.time_score) AS total_time_score FROM score_list AS s JOIN user_list AS u ON s.user_name=u.user_name WHERE u.user_name = %s AND u.user_pass = %s GROUP BY u.user_name"
                sql2 = "SELECT ss.score_date, ss.score, ss.kill_cnt, ss.item_cnt, ss.time_score FROM score_list AS ss JOIN user_list AS uu ON ss.user_name=uu.user_name WHERE uu.user_name = %s AND uu.user_pass = %s ORDER BY ss.score DESC, ss.kill_cnt DESC LIMIT 3"
                # sql = "(SELECT u.user_name, SUM(s.score) AS total_score, SUM(s.kill_cnt) AS total_kill_cnt, SUM(s.item_cnt) AS total_item_cnt, SUM(s.time_score) AS total_time_score FROM score_list AS s JOIN user_list AS u ON s.user_name=u.user_name WHERE u.user_name = %s AND u.user_pass = %s GROUP BY u.user_name) UNION ALL (SELECT ss.score_date AS score_date, ss.score AS score, ss.kill_cnt AS kill_cnt, ss.item_cnt AS item_cnt, ss.time_score AS time_score FROM score_list AS ss JOIN user_list AS uu ON ss.user_name=uu.user_name ORDER BY ss.score DESC LIMIT 3)"
                # sql = "SELECT s.score as score FROM score_list AS s JOIN player_list AS p ON s.player_name=p.player_name WHERE p.player_name='test_player' AND p.player_pass='1234' ORDER BY score DESC LIMIT 3"
                # sql = "SELECT p.user_name FROM user_list as p WHERE `user_name`=%s AND `user_pass`=%s"
                cursor.execute(sql1, (user_info["user_name"], user_info["user_pass"] ))
                result["user_summary"] = cursor.fetchall()
                cursor.execute(sql2, (user_info["user_name"], user_info["user_pass"] ))
                result["user_record"] = cursor.fetchall()
            con.commit()
        except Exception or pymysql.Error() as err:
            result.append("Error: " + err)
    return result

def insert_score(game_score):
    result = list()
    with connect() as con:
        try:
            with con.cursor() as cursor:
                sql = "INSERT INTO score_list(user_name, score, kill_cnt, kill_score, item_cnt, item_score, time_score) VALUES(%s,%s,%s,%s,%s,%s,%s)"
                cursor.execute(sql, (game_score["user_name"], game_score["score"], game_score["kill_cnt"], game_score["kill_score"], game_score["item_cnt"], game_score["item_score"], game_score["time_score"]))
                result = cursor.fetchall()
            con.commit()
        except Exception or pymysql.Error() as err:
            result.append("Error: " + err)
    return result

def find_score(game_score):
    result = list()
    with connect() as con:
        try:
            with con.cursor() as cursor:
                sql = "SELECT * FROM score_list WHERE user_name=%s AND score=%s AND kill_cnt=%s AND kill_score=%s AND item_cnt=%s AND item_score=%s AND time_score=%s ORDER BY score_date DESC"
                cursor.execute(sql, (game_score["user_name"], game_score["score"], game_score["kill_cnt"], game_score["kill_score"], game_score["item_cnt"], game_score["item_score"], game_score["time_score"]))
                result = cursor.fetchall()
        except Exception or pymysql.Error() as err:
            result.append("Error: " + err)
    return result

def find_all_scores():
    result = list()
    with connect() as con:
        try:
            with con.cursor() as cursor:
                sql = "SELECT * FROM score_list ORDER BY score DESC, kill_cnt DESC"
                cursor.execute(sql)
                result = cursor.fetchall()
        except Exception or pymysql.Error() as err:
            result.append("Error: " + err)
    return result

# データ型変換
def custom_default(o):
    if hasattr(o, '__iter__'):
        # イテラブル >> リスト型に変換
        return list(o)
    elif isinstance(o, (datetime, date, time)):
        # 日時型 >> isoformat型に変換
        return o.isoformat()
    else:
        # それ以外のデータ型 >> 文字列型に変換
        return str(o)
