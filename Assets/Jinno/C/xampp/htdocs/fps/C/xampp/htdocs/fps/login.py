#!C:/Users/0609PM/AppData/Local/Programs/Python/Python312/python.exe
# -*- coding: utf-8 -*-
import sys, io, cgi, json
import dao

def login_check(user_info):
    # ログイン確認
    result = dao.login_user(user_info)
    if (len(result) != 0 and any(result)):
        result_list = dict()
    else:
        result = {"Login Status": "Fail", "user_name": user_info["user_name"], "user_pass": user_info["user_pass"]}
        result_list = {"result": result}
    return result_list  # 参照渡し並行利用

def status_check(user_info):
    result = dao.login_status(user_info)
    result_list = {"result": result}
    if (len(result) != 0 and any(result)):
        result_list = {"result": result}
    else:
        result = {"Login Status": "Success", "user_name": user_info.user_name, "user_record": "No Game Scare Record"}
        result_list = {"result": result}
    return result_list  # 参照渡し並行利用

if __name__ == '__main__':
    # ファイル入出力・エンコーディング変換
    sys.stdout = io.TextIOWrapper(sys.stdout.buffer, encoding="utf-8")
    print("Content-Type:application/json;charset=utf-8;lang=ja;\n")

    # リクエストパラメータの取得
    param = cgi.FieldStorage()
    user_name = param.getvalue("user_name")
    user_pass = param.getvalue("user_pass")
    user_info = {"user_name": user_name , "user_pass": user_pass}
  
    result = login_check(user_info)
    if (len(result) != 0 and any(result)):
        result_list = {"result": result}
    else:
        result = status_check(user_info)
        result_list = {"result": result}
    response = json.dumps(result_list, default=dao.custom_default)
    print(response)

