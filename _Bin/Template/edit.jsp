<%@page contentType="text/html; charset=UTF-8"%>
<%@taglib uri="http://java.sun.com/jsp/jstl/core" prefix="c"%>
<%@taglib uri="http://www.o.cn/jsp/tags/form" prefix="form"%>
<%@taglib uri="http://www.o.cn/jsp/tags/util" prefix="util"%>
<%@taglib prefix="spring" uri="http://www.springframework.org/tags"%>
<html>
<head>
<title>edit</title>
<meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
<link href="../css/stype.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="${static_domain}/js/select.js"></script>
<script type="text/javascript" src="${static_domain}/js/validation.js"></script>
<style type="text/css">
<!--
body {
	margin-left: 0px;
	margin-top: 0px;
	margin-right: 0px;
	margin-bottom: 0px;
}
-->
</style>
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

	<table width="100%" border="0" align="center" cellpadding="5"
		cellspacing="0">
		<tr class="ttbg">
			<td><c:if test="${empty {||ClassNameLower||}.id}">添加</c:if> <c:if
					test="${!empty {||ClassNameLower||}.id}">修改</c:if></td>
			<td align="right"><a href="javascript:history.back();" class="txt_1">&lt;&lt;返回</a>
			</td>
		</tr>
	</table>
	<spring:bind path="{||ClassNameLower||}.*">
		<c:forEach var="error" items="${status.errorMessages}">
			<B> <FONT color=RED> <BR> <c:out value="${error}" />
			</FONT>
			</B>
		</c:forEach>
	</spring:bind>

	<form name="{||ClassNameFirstLower||}Form" action="{||ClassNameLower||}.shtml?act=edit" method="post"
		onSubmit="return validate{||ClassName||}Form(this);">
		<input type="hidden" name="_flowExecutionKey"
			value="${flowExecutionKey}"> <input type="hidden"
			name="_eventId" value="submit">
		<c:if test="${!empty {||ClassNameLower||}.id}">
			<input type="hidden" name="id" value="${{||ClassNameLower||}.id}">
		</c:if>
		<table cellspacing="0" cellpadding="0" width="100%" align="center"
			border="0">
			{||EditField||}					
		</table>
		<table width="100%" border="0" align="center" cellpadding="5"
			cellspacing="0">
			<tr>
				<td align="center" class="bg_f1f1f1"><input name="Submit3322"
					type="submit" class="in_ao" value="提交" /></td>
			</tr>
		</table>
	</form>
	<form:focus fieldName="name" />
	<form:validate formClass="cn.o.map.entity.pu.{||ClassName||}"
		formName="{||ClassNameFirstLower||}Form" />
</body>
</html>
