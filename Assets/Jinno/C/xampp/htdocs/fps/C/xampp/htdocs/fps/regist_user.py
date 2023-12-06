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
    user_pass = param.getvalue("user_pass")
    # 全パラメータをディクショナリ型配列に変換
    user_info = {"user_name": user_name, "user_pass": user_pass}

    result1 = dao.check_user_name(user_info)
    if (len(result1) == 0 and not any(result1)):
        # ユーザー情報登録
        result2 = dao.insert_user(user_info)
        if (len(result2) == 0 and not any(result2)):
            # 初回ログイン確認
            result3 = dao.login_user(user_info)
            if (len(result3) != 0 and any(result3)):
                # ディクショナリ型配列 >> JSON形式に変換
                resultList = {"result": result3}
                response = json.dumps(resultList, default=dao.custom_default)
            else:
                response = f"Login Confirmation Failed: [user_name] {user_name}"
        else:
            resultList = {"result": result2}
            response = json.dumps(resultList, default=dao.custom_default)
            response = f"Error: [user_name] {user_name} :: {result2}"
    else:
        response = f"Error: Already User Exists: [user_name] {user_name}"
    print(response)
