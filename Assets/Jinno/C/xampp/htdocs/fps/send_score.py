#!C:/Users/0609PM/AppData/Local/Programs/Python/Python312/python.exe
# -*- coding: utf-8 -*-
import json
import dao
import sys, io, cgi

if __name__ == '__main__':
    sys.stdout = io.TextIOWrapper(sys.stdout.buffer, encoding="utf-8")
    print("Content-Type:text/html;charset=utf-8;lang=ja;\n")

    # リクエストパラメータ取得
    param = cgi.FieldStorage()
    player_name = param.getvalue("player_name")
    score = param.getvalue("score")
    time_score = param.getvalue("time_score")
    score_date = param.getvalue("score_date")

    game_score = {"player_name": player_name , "score": score, "time_score": time_score, "score_date": score_date}
    dao.insert_score(game_score)