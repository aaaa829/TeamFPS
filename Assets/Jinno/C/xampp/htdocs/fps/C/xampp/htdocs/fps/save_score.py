#!C:/Users/0609PM/AppData/Local/Programs/Python/Python312/python.exe
# -*- coding: utf-8 -*-
import sys, io, cgi, json
import dao

if __name__ == '__main__':
    # ファイル入出力・エンコーディング変換
    sys.stdout = io.TextIOWrapper(sys.stdout.buffer, encoding="utf-8")
    print("Content-Type:application/json;charset=utf-8;lang=ja;\n")

    # リクエストパラメータ取得
    param = cgi.FieldStorage()
    user_name = param.getvalue("user_name")
    score = param.getvalue("score")
    kill_cnt = param.getvalue("kill_cnt")
    kill_score = param.getvalue("kill_score")
    item_cnt = param.getvalue("item_cnt")
    item_score = param.getvalue("item_score")
    time_score = param.getvalue("time_score")
    # 全パラメータをディクショナリ型配列に変換
    game_score = {"user_name": user_name , "score": score, "kill_cnt": kill_cnt, "kill_score": kill_score, "item_cnt": item_cnt, "item_score": item_score, "time_score": time_score}

    # スコア情報登録
    result1 =  dao.insert_score(game_score)
    
    if (len(result1) == 0 and not any(result1)):
        result2 = dao.find_score(game_score)
        if (len(result2) != 0 and any(result2)):
            resultList = {"result": result2}
            response = json.dumps(resultList, default=dao.custom_default)
        else:
            response = "Score Confirmation Failed"
    else:
        resultList = {"result": result1}
        response = json.dumps(resultList, default=dao.custom_default)
        response = f"Save Error: {result1}"
    print(response)
