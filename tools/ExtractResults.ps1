$ErrorActionPreference='Stop'
$dom=if($env:C360_SCRIPTDOM_DLL){$env:C360_SCRIPTDOM_DLL}else{'C:\Program Files (x86)\Microsoft SQL Server Management Studio 20\Common7\IDE\Extensions\Application\Microsoft.SqlServer.TransactSql.ScriptDom.dll'}
Add-Type -Path $dom
Add-Type -ReferencedAssemblies $dom -TypeDefinition @'
using System;using System.Collections.Generic;using Microsoft.SqlServer.TransactSql.ScriptDom;
public class ResultProbe:TSqlFragmentVisitor {
 public List<SelectStatement> Statements=new List<SelectStatement>();
 public override void ExplicitVisit(SelectStatement n){var q=n.QueryExpression as QuerySpecification; if(q!=null && q.SelectElements.Count>0 && q.SelectElements[0] is SelectScalarExpression)Statements.Add(n);}
}
public class NamedTables:TSqlFragmentVisitor {public List<NamedTableReference> Tables=new List<NamedTableReference>(); public override void ExplicitVisit(NamedTableReference n){Tables.Add(n);}}
'@
$parser=New-Object Microsoft.SqlServer.TransactSql.ScriptDom.TSql150Parser($true)
$all=@()
Get-ChildItem (Join-Path $PSScriptRoot '../../Database/05_StoredProcedures') -Filter *.sql -Recurse | ForEach-Object {
 $sql=[IO.File]::ReadAllText($_.FullName);$errs=$null;$tree=$parser.Parse([IO.StringReader]::new($sql),[ref]$errs)
 if($errs.Count){Write-Host $_.Name; $errs | Select-Object -First 1 | Format-List; return}
 $v=New-Object ResultProbe;$tree.Accept($v);$sets=@()
 foreach($st in $v.Statements){
  $query=$st.QueryExpression;$refs=New-Object NamedTables;$query.Accept($refs)
  $tables=@($refs.Tables | ForEach-Object { @{alias=if($_.Alias){$_.Alias.Value}else{$_.SchemaObject.BaseIdentifier.Value};table=$_.SchemaObject.BaseIdentifier.Value} })
  $cols=@($query.SelectElements | ForEach-Object { $e=$_.Expression; $name=if($_.ColumnName){$_.ColumnName.Value}elseif($e -is [Microsoft.SqlServer.TransactSql.ScriptDom.ColumnReferenceExpression]){$e.MultiPartIdentifier.Identifiers[-1].Value}else{''}; @{name=$name;expr=$sql.Substring($e.StartOffset,$e.FragmentLength)} })
  $sets+=@{columns=$cols;tables=$tables;sql=$sql.Substring($st.StartOffset,$st.FragmentLength)}
 }
 $all+=@{procedure=$_.BaseName;sets=$sets}
}
$all | ConvertTo-Json -Depth 15 | Set-Content (Join-Path $PSScriptRoot 'ProcedureResultProbes.json')

