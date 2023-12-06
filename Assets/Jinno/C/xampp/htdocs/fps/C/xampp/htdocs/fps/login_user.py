#!C:/Users/0609PM/AppData/Local/Programs/Python/Python312/python.exe
# -*- coding: utf-8 -*-
import sys, io, cgi, json
import dao

if __name__ == '__main__':
    # ファイル入出力・エンコーディング変換
    sys.stdout = io.TextIOWrapper(sys.stdout.buffer, encoding="utf-8")
    print("Content-Type:application/json;charset=utf-8;lang=ja;\n")

    # リクエストパラメータの取得
    param = cgi.FieldStorage()
    user_name = param.getvalue("user_name")
    user_pass = param.getvalue("user_pass")
    user_info = {"user_name": user_name , "user_pass": user_pass}

    # ログイン確認
    result1 = dao.login_user(user_info)

    if (len(result1) != 0 and any(result1)):
        # スコア情報取得
        result2 = dao.check_user_summary(user_info)
        if (len(result2) != 0 and any(result2)):
            # ディクショナリ型配列 >> JSON形式に変換
            resultList = {"result": result2}
            response = json.dumps(resultList, default=dao.custom_default)
        else:
            response = f"Error:  No Game Scare:  [user_name]  {user_name}"
    else:
        response = f"Login Failed:  [user_name] {user_name}  [user_pass] {user_pass}"
    print(response)