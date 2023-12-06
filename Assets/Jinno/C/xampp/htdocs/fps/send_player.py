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
    player_pass = param.getvalue("player_pass")
    created_date = param.getvalue("created_date")

    player_info = {"player_name": player_name , "player_pass": player_pass, "created_date": created_date}
    dao.insert_player(player_info)