AspNetIdentityCustomStoreSample
===============================

ASP.NET Identityのカスタムのストアを利用するサンプル

### これは何？
ASP.NET Identity の 簡易カスタマイズで、独自のユーザーストアを利用するサンプルです。
『既に運用されている既存の独自のユーザー管理データベースを利用する』というケースを想定しています。

### 環境
作成した環境は以下です

- Visual Studio 2013 Update 2
- .NET Framework 4.5.1
- ASP.NET MVC 5.1

### データベースについて
データベースは特に特定のものを想定しませんが、サンプルとしての容易さからSQL Server LocalDBを利用しています。
（ただし、DBアクセスにはEntity Frameworkを敢えて利用せず、Dapperを利用しています。）

### セットアップ方法
そのまま実行するにはデータベースファイルが必要です。LocalDBを利用する場合は下記の手順でデータべースファイルおよび必要なオフジェクトを設定してください。
既存のDBインスタンスに設定する場合は`db_script`配下のスクリプトを利用するなどし、適当に設定してください。

#### LocalDBを利用する場合のDBのセットアップ
1. src配下のソリューションファイル（CustomIdentitySample.sln）をVisual Studioで開く
2. App_Dataフォルダを追加
	- ソリューションエクスプローラーより、プロジェクト「CustomIdentitySample」直下に`App_Data`フォルダを追加
3. App_DataフォルダにDBファイルを追加
    - [追加]-[新しい項目]-[SQL Server データベース]
    - ファイル名は任意だが、Web.Configの`configuration/connectionStrings`セクションで指定している`AttachDbFilename`と合わせること
	    - サンプルでは`UserInfo.mdf`としている
4. 追加したmdfのDBに対して、`db_script`配下のスクリプトを順番に実行
  
#### サンプルDBのユーザー情報について
サンプルDBに登録されているユーザーでログインする場合、

- ID : narami
- パスワード : hogepassword

です。変更したい場合は`db_dcript/04.insert_user_data.sql`あたりを見ながら適当に。


### その他
本サンプルは下記の記事などを参考しています

- [ASP.NET Identity | The ASP.NET Site](http://www.asp.net/identity)
	- [Implementing a Custom MySQL ASP.NET Identity Storage Provider](http://www.asp.net/identity/overview/extensibility/implementing-a-custom-mysql-aspnet-identity-storage-provider)
- [ASP.NET Identityカスタマイズに挑戦](http://okazuki.hatenablog.com/entry/2013/11/10/190811)
- [ASP.NET Identity入門](http://codezine.jp/article/corner/511)

