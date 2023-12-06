#!C:/Users/0609PM/AppData/Local/Programs/Python/Python312/python.exe
# -*- coding: utf-8 -*-
import sys, io, cgi, json
import dao
   
if __name__ == '__main__':
    # ファイル入出力・エンコーディング変換
    sys.stdout = io.TextIOWrapper(sys.stdout.buffer, encoding="utf-8")
    print("Content-Type:application/json;charset=utf-8;lang=ja;\n")

    # 全ランキング情報取得
    result = dao.find_all_scores()
    # ディクショナリ型配列 >> JSON形式に変換

    if (len(result) != 0 and any(result)) :
        resultList = {"result": result}
        response = json.dumps(resultList, default=dao.custom_default)
    else:
        response = "No Game Score"
    print(response)

