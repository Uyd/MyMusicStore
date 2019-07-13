<meta http-equiv="content-type" content="text/html" ;charset="UTF-8">
<%
db="database/saveText.mdb"
Set conn=Server.CreateObject("ADODB.Connection")
connstr="Provider=Microsoft.Jet.OLEDB.4.0;Data Source="& Server.MapPath(""&db&"")
conn.Open connstr

dim comm,tim
comm=request.form("textarea")
tim=time(now())

set rs=server.CreateObject("adodb.recordset")

rs.open "select * from comment",conn,1,3
rs.addnew
rs("comment_on")=comm
rs("time")=tim

rs.update
response.Redirect("index.html")
rs.close

set rs=nothing
%>