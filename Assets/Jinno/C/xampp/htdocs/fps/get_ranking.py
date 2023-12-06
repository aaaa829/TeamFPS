#!C:/Users/0609PM/AppData/Local/Programs/Python/Python312/python.exe
# -*- coding: utf-8 -*-
import json
import dao
import sys, io, cgi
from datetime import datetime, date, time

# サポート外のデータ型変換
def custom_default(o):
    if hasattr(o, '__iter__'):
        # イテラブルなものはリスト型に変換
        return list(o)
    elif isinstance(o, (datetime, date, time)):
        # 日時型はisoformat型に変換
        return o.isoformat()
    else:
        # それ以外は文字列型に変換
        return str(o)
    
if __name__ == '__main__':
    sys.stdout = io.TextIOWrapper(sys.stdout.buffer, encoding="utf-8")
    print("Content-Type:application/json;charset=utf-8;lang=ja;\n")
    # 全ランキング履歴情報取得
    result = dao.find_all()
    resultList = {"result": result}
    response = json.dumps(resultList, default=custom_default)
    print(response)
