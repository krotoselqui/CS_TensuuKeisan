# CI とローカル検証

## 自動確認する内容

[GitHub Actions の定義](../.github/workflows/ci.yml) は push、pull request、手動実行で動く。Windows 2022 のランナー上で Debug / Release をそれぞれビルドし、失敗した処理があればジョブを失敗させる。権限はリポジトリの読み取りだけで、配備やコミットは行わない。

| 検査 | 対応する規則 |
| --- | --- |
| ソースと `Compile Include` の照合 | 新規ファイル登録漏れ、削除済みファイル参照、重複登録を検出 |
| 文書の相対ファイルリンク | README・規則・仕様から参照できることを確認 |
| Debug / Release の Rebuild | 型・参照・構文・プロジェクト設定の整合 |
| 採点 7 ケース | 親子、切り上げなし、満貫、数え役満、複数役満の具体的な支払額 |
| 牌変換 6 ケース | 物理牌 ID と牌種の違い、先頭・末尾・範囲外 |

外部のテストパッケージは追加していない。ビルド済みアプリを Windows PowerShell の .NET Framework で読み込み、既存の非公開型・メソッドをリフレクションで呼び出す。CI のためだけにアプリの公開範囲やソリューション構成を変更しないための暫定的な方式である。型・メソッドを抽出する段階で、この呼び出しも更新する。

既存の未使用変数や到達不能コードの警告は表示するが、一括でエラー扱いにはしない。命名や責務の良し悪し、ソース全体の整形は自動強制しない。既存コード全体への規則の遡及を避け、変更部分をレビューする。

## 実行手順

Visual Studio の開発者 PowerShell、または MSBuild が PATH にある Windows PowerShell で実行する。

```powershell
powershell -NoProfile -File scripts/Check-Repository.ps1
msbuild ConsoleApplication9.sln /t:Rebuild /p:Configuration=Debug /nologo /verbosity:minimal
powershell -NoProfile -File scripts/Test-Regression.ps1 -Configuration Debug
msbuild ConsoleApplication9.sln /t:Rebuild /p:Configuration=Release /nologo /verbosity:minimal
powershell -NoProfile -File scripts/Test-Regression.ps1 -Configuration Release
```

各コマンドの終了コードを確認し、ビルド失敗時に以前の実行ファイルのテスト結果を採用しない。構成ごとに別の PowerShell プロセスでアセンブリを読み込む。

## 検証の限界と拡張

- 点数ケースは符・飜から支払額への変換を確認する。手牌から役・符・飜を求める正しさを網羅しない。
- 現在の `Main` はカーソル操作・キー入力を使うため、CI では起動しない。対話 UI、生成全体、Shift_JIS ファイル出力、固定シードの出力比較は未自動化。
- `.editorconfig` はエディター設定であり、命名規則の検査器ではない。型検査の成功も仕様の正しさを保証しない。
- リンク検査は本文の Markdown インラインリンクの相対ファイルを対象とし、外部 URL や見出しアンカーの到達性を検査しない。
- 文書のみの変更でも現状は小規模な同じジョブを実行する。将来実行時間が増えたら、文書検査とアプリ検査の条件を分ける。

生成・採点の抽出に合わせて [リファクタリング方針](refactoring-plan.md) の回帰ケースを追加する。GitHub 上の実行結果とローカルで同じスクリプトを実行した結果は区別する。ブランチ保護による必須チェックの指定は、このワークフロー追加とは別のリポジトリ設定である。

環境・アクションの参考: [Windows 2022 イメージ](https://github.com/actions/runner-images/blob/main/images/windows/Windows2022-Readme.md)、[setup-msbuild](https://github.com/microsoft/setup-msbuild)、[checkout](https://github.com/actions/checkout)。
