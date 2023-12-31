# RunTimeRecords_CSDNF
Application run time records.(Csharp and DotNetFramework)

アプリケーションの実行時間を記録するツールです。  
本命はPCゲームのプレイ時間を計測したかったから。

以下の環境で作成（これを選んだ理由は単に今、業務で利用しているからです。）
* 言語：C#
* フレームワーク：.NetFramework4.8
* OS：Windows10 Home
  * 各種設定は日本
* IDE：VisualStudio2022 Community

大枠の機能を以下にメモ書きする。
* コンピュータ上で動いているプロセスを一覧を取得
* プロセスの実行ファイルが格納されているフォルダ名でフィルタリングを可能とする。（ホワイトリスト）
  * 複数個指定可能とし、それらは OR 条件とする。
  * ホワイトリストに存在するが、対象外とするフォルダまたはファイル名またはプロセス名を別途指定可能とする。（ブラックリスト）
    * これらは OR 条件とする。
  * これらの設定は外部ファイルとして保存可能とする。
* プロセスが終了するまで分単位で監視する。（それ以上は細かく計測してもあまり意味が無い。）
  * 本アプリケーションが起動時、既に実行しているプロセスの実行時間は取得可能ならその値をそのまま使用
  * プロセスの終了が確認されるとプロセス名と開始時刻・終了時刻・実行時間をファイルに保存する。
  * プロセスが終了する前に本アプリケーションが終了する場合、記録は残らない。
    * 対象プロセスが終了していなければ本アプリケーションを再実行することで計測可能
* 記録方法
  * CSVファイルとする。
