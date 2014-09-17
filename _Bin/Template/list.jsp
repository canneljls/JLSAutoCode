<%@ page contentType="text/html; charset=UTF-8"%>
<%@taglib uri="http://java.sun.com/jsp/jstl/core" prefix="c"%>
<%@taglib uri="http://www.o.cn/jsp/tags/form" prefix="form"%>
<%@taglib uri="http://www.o.cn/jsp/tags/util" prefix="util"%>
<%@taglib prefix="spring" uri="http://www.springframework.org/tags"%>

<html>
<head>
<title>list</title>
<meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
<link href="../css/stype.css" rel="stylesheet" type="text/css" />
<style type="text/css">
body {
	margin-left: 0px;
	margin-top: 0px;
	margin-right: 0px;
	margin-bottom: 0px;
}

.STYLE2 {
	font-size: 36px;
	font-weight: bold;
}
</style>
<script>

function add(){
	location='{||ClassNameLower||}.shtml?act=edit';
}

function edit(id){
    location='{||ClassNameLower||}.shtml?act=edit&id='+id;
}

function del(id) {
	if(confirm('确定删除吗？')){
		location='{||ClassNameLower||}.shtml?act=del&id='+id+"&currentPage="+document.queryForm.currentPage.value;
	}
}

</script>

</head>
<body>
	<table cellspacing="0" cellpadding="0" width="100%" align="center"
		border="0">
		<tbody>
			<tr>
				<td class="main_tt"><img src="../images/edit_f2.png" width="32"
					height="32">{||CHName||}</td>
			</tr>
		</tbody>
	</table>
	<form name="queryForm" method="get" action="{||ClassNameLower||}.shtml">
	<input type="hidden" name="act" value="list">
	<table width="100%" border="0" align="center" cellpadding="0"
		cellspacing="0" class="bg_dddddd">
		<tbody>
			<tr class="ttbg">
				<td></td>
				<td align="right">&nbsp;&nbsp; <a
					href="javascript:void" onclick="add()">添加 </a> &nbsp;&nbsp;</td>
			</tr>
		</tbody>
	</table>
	<table cellspacing="0" cellpadding="5" width="100%" align="center"
		border="0">
		<tbody>
			<tr>
				{||TableTitle||}
				<td align="center" class="bg_f1f1f1"><span class="txt_bold">修改</span>
				</td>
				<td align="center" class="bg_f1f1f1"><span class="txt_bold">删除</span>
				</td>
			</tr>
			<c:forEach var="entity" items="${{||ClassNameLower||}s}">
				<tr>
					{||TableRow||}

					<td align="center" class="bg_f1f1f1"><a
						href="javascript:void()" onclick="edit(${entity.id})"><img
							src="../images/68design.net_(edit)_16x16.gif" alt="修改" width="16"
							height="16" border="0">
					</a>
					</td>
					<td align="center" class="bg_f1f1f1"><a href="#"
						onClick="del(${entity.id});"><img
							src="../images/publish_x.png" alt="删除" width="12" height="12"
							border="0">
					</a>
					</td>
				</tr>
			</c:forEach>
				<tr>
					<td  height="29" colspan="8" align="center" class="bg_dddddd"> <jsp:include
							page="/include/paging.jsp?formName=queryForm" flush="true" /></td>
				</tr>
		</tbody>
	</table>
	</form>
</body>
</html>
