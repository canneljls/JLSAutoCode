using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace JLSAutoCode
{
    /// <summary>
    /// Hibernate自动生成实体具体生成的类
    /// </summary>
    public class HHelperJava
    {
        public static void BuildCode(List<HField> m_Datas, HTable table, string rootFolder)
        {
            //实体
            BuildEntity(m_Datas, table, rootFolder);
            //Dao接口
            BuildDaoInterface(m_Datas, table, rootFolder);
            //service接口
            BuildServiceInterface(m_Datas, table, rootFolder);
            //service实现类
            BuildServiceImpl(m_Datas, table, rootFolder);
            //Dao Xml配置
            BuildDaoXml(m_Datas, table, rootFolder);
            //Service Xml配置
            BuildServiceXml(m_Datas, table, rootFolder);
            //Ctrl
            BuildCtrl(m_Datas, table, rootFolder);
            //jsp  list.jsp
            BuildPageList(m_Datas, table, rootFolder);
            //jsp   edit.jsp
            BuildPageEdit(m_Datas, table, rootFolder);
            //字段验证配置
            BuildValidConfig(m_Datas, table, rootFolder);

            MessageBox.Show("导出成功");
        }

        private static void BuildEntity(List<HField> m_Datas, HTable table, string rootFolder)
        {
            string templateString = HOther.ReadTemplate(HConst.TemplatePath, "entity.java");

            StringBuilder sb = new StringBuilder();

            sb.Append("@Entity" + Environment.NewLine);
            sb.Append("@Table(name = \"" + table.ENName + "\")" + Environment.NewLine);
            sb.Append("public class " + table.ClassName + " implements DeletedFlag, Serializable {" + Environment.NewLine);

            sb.Append("private static final long serialVersionUID = -" + GetRandomNumber(19, true) + "L;" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            foreach (HField field in m_Datas)
            {
                ///**
                // * 城市名称
                // */
                //@Column(nullable = false, length = 20)
                //private String name;

                ///**
                // * 性别
                // */
                //@Enumerated(EnumType.STRING)
                //private Gender gender;

                sb.Append("/**" + Environment.NewLine);
                sb.Append(" * " + field.CHName + Environment.NewLine);
                sb.Append(" */" + Environment.NewLine);
                if (field.ENName.Equals("Id", StringComparison.OrdinalIgnoreCase) == true)
                {
                    sb.AppendLine("@Id");
                    sb.AppendLine("@TableGenerator(name = \"" + table.ENName + "\", table = \"wgh_sequence\", pkColumnName = \"sequenceName\", valueColumnName = \"value\", allocationSize = 1)");
                    sb.AppendLine("@GeneratedValue(strategy = GenerationType.TABLE, generator = \"" + table.ENName + "\")");
                    sb.AppendLine("private Integer id;");
                }
                else
                {
                    if (field.Type == HFieldType.CLOB)
                    {
                        sb.Append("@Column(columnDefinition=\"CLOB\")");
                    }
                    else
                    {
                        sb.Append("@Column");
                        if ((field.Type == HFieldType.NVARCHAR2 || field.Type == HFieldType.NUMBER) && field.Length > 0)
                        {
                            sb.Append("(length = " + field.Length + ")");
                        }
                    }
                    sb.Append(Environment.NewLine);
                    if (field.Type == HFieldType.DATE)
                    {
                        sb.Append("@Temporal(TemporalType.TIMESTAMP)" + Environment.NewLine);
                    }
                    string f1 = GetCSType(field);
                    sb.Append("private " + f1 + HOther.FirstWordLower(field.ENName) + ";" + Environment.NewLine);
                    sb.Append(Environment.NewLine);
                }
                //sb.Append("" + Environment.NewLine);

                //sb.Append("");               
            }

            //@Column
            //private boolean deleted;
            sb.Append("@Column" + Environment.NewLine);
            sb.Append("private boolean deleted;" + Environment.NewLine);
            sb.Append("" + Environment.NewLine);

            foreach (HField field in m_Datas)
            {
                //public String getName() {
                //    return name;
                //}

                //public void setName(String name) {
                //    this.name = name;
                //}

                string f1 = GetCSType(field);
                sb.Append("public " + f1 + " get" + field.ENName + "() { " + Environment.NewLine);
                sb.Append("    return " + HOther.FirstWordLower(field.ENName) + ";" + Environment.NewLine);
                sb.Append("}" + Environment.NewLine);
                sb.Append(Environment.NewLine);

                sb.Append("public void set" + field.ENName + "(" + f1 + " " + HOther.FirstWordLower(field.ENName) + ") {" + Environment.NewLine);
                sb.Append("    this." + HOther.FirstWordLower(field.ENName) + " = " + HOther.FirstWordLower(field.ENName) + ";" + Environment.NewLine);
                sb.Append("}" + Environment.NewLine);
                sb.Append(Environment.NewLine);
            }

            //public boolean isDeleted() {
            //    return deleted;
            //}

            //public void setDeleted(boolean deleted) {
            //    this.deleted = deleted;
            //}
            sb.Append("public boolean isDeleted() {" + Environment.NewLine);
            sb.Append("    return deleted;" + Environment.NewLine);
            sb.Append("}" + Environment.NewLine);
            sb.Append("" + Environment.NewLine);
            sb.Append("public void setDeleted(boolean deleted) {" + Environment.NewLine);
            sb.Append("    this.deleted = deleted;" + Environment.NewLine);
            sb.Append("}" + Environment.NewLine);
            sb.Append("" + Environment.NewLine);

            //实体类注释
            templateString = templateString.Replace("{||ClassAnno||}", table.CHName);
            //主要内容
            templateString = templateString.Replace("{||FieldList||}", sb.ToString());

            HOther.SaveStringToFile(templateString, rootFolder + "\\" + table.ClassName + ".java");
        }

        private static void BuildDaoInterface(List<HField> m_Datas, HTable table, string rootFolder)
        {
            string templateString = HOther.ReadTemplate(HConst.TemplatePath, "entityDao.java");

            StringBuilder sb = new StringBuilder();

            sb.Append("import cn.o.map.entity.pu." + table.ClassName + ";" + Environment.NewLine);

            sb.Append("" + Environment.NewLine);

            sb.Append("public interface " + table.ClassName + "Dao extends BaseDao<" + table.ClassName + ", Integer>, BaseHibernateDao<" + table.ClassName + ", String>  {" + Environment.NewLine);

            //主要内容
            templateString = templateString.Replace("{||MainContent||}", sb.ToString());

            HOther.SaveStringToFile(templateString, rootFolder + "\\" + table.ClassName + "Dao.java");
        }

        private static void BuildServiceInterface(List<HField> m_Datas, HTable table, string rootFolder)
        {
            string templateString = HOther.ReadTemplate(HConst.TemplatePath, "entityService.java");

            StringBuilder sb = new StringBuilder();

            sb.Append("import cn.o.map.entity.pu." + table.ClassName + ";" + Environment.NewLine);

            sb.Append("" + Environment.NewLine);

            sb.Append("public interface " + table.ClassName + "Service  extends BasicService{" + Environment.NewLine);

            //主要内容
            templateString = templateString.Replace("{||MainContent||}", sb.ToString());

            HOther.SaveStringToFile(templateString, rootFolder + "\\" + table.ClassName + "Service.java");
        }

        private static void BuildServiceImpl(List<HField> m_Datas, HTable table, string rootFolder)
        {
            string templateString = HOther.ReadTemplate(HConst.TemplatePath, "entityServiceImpl.java");

            StringBuilder sb = new StringBuilder();

            sb.Append("import cn.o.map.dao.infs.pu." + table.ClassName + "Dao;" + Environment.NewLine);
            sb.Append("import cn.o.map.entity.pu." + table.ClassName + ";" + Environment.NewLine);
            sb.Append("import cn.o.console.service.pu." + table.ClassName + "Service;" + Environment.NewLine);

            sb.Append("" + Environment.NewLine);

            sb.Append("@LogDescription(name = \"" + table.CHName + "BLL\")" + Environment.NewLine);
            sb.Append("@Transactional(readOnly = true)" + Environment.NewLine);
            sb.Append("public class " + table.ClassName + "ServiceImpl  extends BasicServiceImpl  implements " + table.ClassName + "Service {" + Environment.NewLine);

            sb.Append("" + Environment.NewLine);

            sb.Append("@Autowired" + Environment.NewLine);
            sb.Append("private " + table.ClassName + "Dao " + HOther.FirstWordLower(table.ClassName) + "Dao;" + Environment.NewLine);

            sb.Append("" + Environment.NewLine);

         
            //主要内容
            templateString = templateString.Replace("{||MainContent||}", sb.ToString());

            HOther.SaveStringToFile(templateString, rootFolder + "\\" + table.ClassName + "ServiceImpl.java");
        }

        private static void BuildCtrl(List<HField> m_Datas, HTable table, string rootFolder)
        {
            string templateString = HOther.ReadTemplate(HConst.TemplatePath, "entityCtrl.java");

            StringBuilder sb = new StringBuilder();

            sb.Append("import cn.o.console.service.pu." + table.ClassName + "Service;" + Environment.NewLine);
            sb.Append("import cn.o.map.entity.pu." + table.ClassName + ";" + Environment.NewLine);

            sb.Append("" + Environment.NewLine);

            sb.Append("@Controller" + Environment.NewLine);
            sb.Append("@RequestMapping(\"/pu/" + table.ClassName.ToLower() + ".shtml\")" + Environment.NewLine);
            sb.Append("@SessionAttributes(\"" + table.ClassName.ToLower() + "\")" + Environment.NewLine);
            sb.Append("public class " + table.ClassName + "Ctrl {" + Environment.NewLine);

            sb.Append("" + Environment.NewLine);

            sb.Append("@Autowired" + Environment.NewLine);
            sb.Append("private " + table.ClassName + "Service " + HOther.FirstWordLower(table.ClassName) + "Service;" + Environment.NewLine);

            sb.Append("" + Environment.NewLine);

            sb.Append("@InitBinder" + Environment.NewLine);
            sb.Append("public void initBinder(WebDataBinder binder) {" + Environment.NewLine);
            sb.Append("SimpleEditor simpleEditor = new SimpleEditor();" + Environment.NewLine);
            sb.Append("simpleEditor.setEntityType(" + table.ClassName + ".class);" + Environment.NewLine);
            sb.Append("binder.registerCustomEditor(" + table.ClassName + ".class, simpleEditor);" + Environment.NewLine);
            sb.Append("}" + Environment.NewLine);

            sb.Append("" + Environment.NewLine);

            sb.Append("@RequestMapping(params = \"act=list\")" + Environment.NewLine);
            sb.Append("public String list(Integer currentPage, Integer pageSize, ModelMap model,HttpServletRequest request, HttpServletResponse response)" + Environment.NewLine);
            sb.Append("throws Exception {" + Environment.NewLine);
            sb.Append("Pagination pageArg = new Pagination(currentPage == null ? 1: currentPage, pageSize == null ? 10 : pageSize);" + Environment.NewLine);
            sb.Append("Page<" + table.ClassName + "> page = " + HOther.FirstWordLower(table.ClassName) + "Service.page" + table.ClassName + "(pageArg);" + Environment.NewLine);
            sb.Append("if (page.getResultSize() == 0) {" + Environment.NewLine);
            sb.Append("PageUtils.saveModeldefault(model);" + Environment.NewLine);
            sb.Append("} else {" + Environment.NewLine);
            sb.Append("PageUtils.saveModel(model, page.getPageSize(),page.getCurrentPageNo(), page.getTotalCount(),page.getTotalPageCount());" + Environment.NewLine);
            sb.Append("}" + Environment.NewLine);
            sb.Append("model.addAttribute(\"" + table.ClassName.ToLower() + "s\", page.getResult());" + Environment.NewLine);
            sb.Append("return \"pu/" + table.ClassName.ToLower() + "/list\";" + Environment.NewLine);
            sb.Append("}" + Environment.NewLine);

            sb.Append("" + Environment.NewLine);

            sb.Append("@RequestMapping(params = \"act=del\")" + Environment.NewLine);
            sb.Append("public String del(Integer id, ModelMap model,HttpServletRequest request, HttpServletResponse response)" + Environment.NewLine);
            sb.Append("throws Exception {" + Environment.NewLine);
            sb.Append("try {" + Environment.NewLine);
            sb.Append(HOther.FirstWordLower(table.ClassName) + "Service.remove(" + table.ClassName + ".class, id);" + Environment.NewLine);
            sb.Append("} catch (AccessDeniedException ex) {" + Environment.NewLine);
            sb.Append("return \"error/accessDenied\";" + Environment.NewLine);
            sb.Append("}" + Environment.NewLine);
            sb.Append("return \"redirect:/pu/" + table.ClassName.ToLower() + ".shtml?act=list\";" + Environment.NewLine);
            sb.Append("}" + Environment.NewLine);

            sb.Append("" + Environment.NewLine);

            sb.Append("@RequestMapping(params = \"act=edit\", method = RequestMethod.GET)" + Environment.NewLine);
            sb.Append("public String edit(Integer id, ModelMap model, HttpServletRequest request,HttpServletResponse response) throws Exception {" + Environment.NewLine);
            sb.Append(table.ClassName + " " + HOther.FirstWordLower(table.ClassName) + " = new " + table.ClassName + "();" + Environment.NewLine);
            sb.Append("if (id != null) {" + Environment.NewLine);
            sb.Append(HOther.FirstWordLower(table.ClassName) + " = " + HOther.FirstWordLower(table.ClassName) + "Service.find(" + table.ClassName + ".class, id);" + Environment.NewLine);
            sb.Append("}" + Environment.NewLine);
            sb.Append("model.addAttribute(\"" + table.ClassName.ToLower() + "\", " + HOther.FirstWordLower(table.ClassName) + ");" + Environment.NewLine);
            sb.Append("return \"pu/" + table.ClassName.ToLower() + "/edit\";" + Environment.NewLine);
            sb.Append("}" + Environment.NewLine);

            sb.Append("" + Environment.NewLine);

            sb.Append("@RequestMapping(params = \"act=edit\", method = RequestMethod.POST)" + Environment.NewLine);
            sb.Append("public String editSubmit(@ModelAttribute(\"" + table.ClassName.ToLower() + "\") " + table.ClassName + " " + HOther.FirstWordLower(table.ClassName) + ",BindingResult result, SessionStatus status, ModelMap model,HttpServletRequest request, HttpServletResponse response)" + Environment.NewLine);
            sb.Append("throws Exception {" + Environment.NewLine);
            sb.Append("if (FormValidatorUtils.hasErrors(" + HOther.FirstWordLower(table.ClassName) + ", result)) {	" + Environment.NewLine);
            sb.Append("return \"pu/" + table.ClassName.ToLower() + "/edit\";" + Environment.NewLine);
            sb.Append("}" + Environment.NewLine);
            sb.Append("try {" + Environment.NewLine);
            sb.Append("if (" + HOther.FirstWordLower(table.ClassName) + ".getID() == null)" + Environment.NewLine);
            sb.Append(HOther.FirstWordLower(table.ClassName) + "Service.save(" + HOther.FirstWordLower(table.ClassName) + ");" + Environment.NewLine);
            sb.Append("else" + Environment.NewLine);
            sb.Append(HOther.FirstWordLower(table.ClassName) + "Service.update(" + HOther.FirstWordLower(table.ClassName) + ");" + Environment.NewLine);
            sb.Append("} catch (AccessDeniedException ex) {" + Environment.NewLine);
            sb.Append("return \"error/accessDenied\";" + Environment.NewLine);
            sb.Append("}" + Environment.NewLine);
            sb.Append("model.addAttribute(\"id\", " + HOther.FirstWordLower(table.ClassName) + ".getID());" + Environment.NewLine);
            sb.Append("status.setComplete();" + Environment.NewLine);
            sb.Append("return \"redirect:/pu/" + table.ClassName.ToLower() + ".shtml?act=list\";" + Environment.NewLine);
            sb.Append("}" + Environment.NewLine);

            //主要内容
            templateString = templateString.Replace("{||MainContent||}", sb.ToString());

            HOther.SaveStringToFile(templateString, rootFolder + "\\" + table.ClassName + "Ctrl.java");
        }

        private static void BuildPageList(List<HField> m_Datas, HTable table, string rootFolder)
        {
            string templateString = HOther.ReadTemplate(HConst.TemplatePath, "list.jsp");           
            
            templateString = templateString.Replace("{||ClassName||}", table.ClassName);
            templateString = templateString.Replace("{||ClassNameLower||}", table.ClassName.ToLower());
            templateString = templateString.Replace("{||ClassNameFirstLower||}", HOther.FirstWordLower(table.ClassName));

            templateString = templateString.Replace("{||ENName||}", table.ENName);
            templateString = templateString.Replace("{||CHName||}", table.CHName);

            StringBuilder sb = new StringBuilder();
            //表格列头
            foreach (HField field in m_Datas)
            {
                sb.Append("<td align=\"center\" class=\"bg_f1f1f1\"><span class=\"txt_bold\">" + field.CHName + "</span>" + Environment.NewLine);
                sb.Append("</td>" + Environment.NewLine);
            }

            templateString = templateString.Replace("{||TableTitle||}", sb.ToString());

            sb = new StringBuilder();
            //表格行内容
            foreach (HField field in m_Datas)
            {
                sb.Append("<td align=\"center\" class=\"bg_f1f1f1\">${entity." + HOther.FirstWordLower(field.ENName) + "}</td>" + Environment.NewLine);
            }

            templateString = templateString.Replace("{||TableRow||}", sb.ToString());

            HOther.SaveStringToFile(templateString, rootFolder + "\\list.jsp");
        }

        private static void BuildPageEdit(List<HField> m_Datas, HTable table, string rootFolder)
        {
            string templateString = HOther.ReadTemplate(HConst.TemplatePath, "edit.jsp");

            templateString = templateString.Replace("{||ClassName||}", table.ClassName);
            templateString = templateString.Replace("{||ClassNameLower||}", table.ClassName.ToLower());
            templateString = templateString.Replace("{||ClassNameFirstLower||}", HOther.FirstWordLower(table.ClassName));

            templateString = templateString.Replace("{||ENName||}", table.ENName);
            templateString = templateString.Replace("{||CHName||}", table.CHName);

            StringBuilder sb = new StringBuilder();
            //编辑字段
            foreach (HField field in m_Datas)
            {
                sb.Append("<tr>" + Environment.NewLine);
                sb.Append("<td height=\"31\" class=\"bg_f1f1f1\">" + field.CHName + "：</td>" + Environment.NewLine);
                sb.Append("<td class=\"bg_f1f1f1\"><input name=\"" + HOther.FirstWordLower(field.ENName) + "\" value=\"${" + table.ClassName.ToLower() + "." + HOther.FirstWordLower(field.ENName) + "}\"  type=\"text\"></td>" + Environment.NewLine);
                sb.Append("</tr>" + Environment.NewLine);
            }

            templateString = templateString.Replace("{||EditField||}", sb.ToString());
           
            HOther.SaveStringToFile(templateString, rootFolder + "\\edit.jsp");
        }

        private static void BuildDaoXml(List<HField> m_Datas, HTable table, string rootFolder)
        {
            string templateString = HOther.ReadTemplate(HConst.TemplatePath, "applicationContext-dao.xml");
        
            //主要内容
            templateString = templateString.Replace("{||Content1||}", "<bean id=\"" + HOther.FirstWordLower(table.ClassName) + "Dao\" parent=\"abstractDao\">");

            templateString = templateString.Replace("{||Content2||}", "<value>cn.o.map.dao.infs.pu." + table.ClassName + "Dao</value>");

            templateString = templateString.Replace("{||Content3||}", "<constructor-arg value=\"cn.o.map.entity.pu." + table.ClassName + "\" />");

            HOther.SaveStringToFile(templateString, rootFolder + "\\" + table.ClassName + "applicationContext-dao.xml");
        }

        private static void BuildServiceXml(List<HField> m_Datas, HTable table, string rootFolder)
        {
            string templateString = HOther.ReadTemplate(HConst.TemplatePath, "applicationContext-service.xml");

            templateString = templateString.Replace("{||Content1||}", "<bean id=\"" + HOther.FirstWordLower(table.ClassName) + "Service\" class=\"cn.o.console.service.pu.impl." + table.ClassName + "ServiceImpl\">");

            HOther.SaveStringToFile(templateString, rootFolder + "\\" + table.ClassName + "applicationContext-service.xml");
        }

        /// <summary>
        /// 字段验证配置
        /// </summary>
        /// <param name="m_Datas"></param>
        /// <param name="table"></param>
        /// <param name="rootFolder"></param>
        private static void BuildValidConfig(List<HField> m_Datas, HTable table, string rootFolder)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("第一部分，添加项，但提示内容为空" + Environment.NewLine);
            sb.Append("" + Environment.NewLine);

            //长度
            foreach (HField field in m_Datas)
            {
                sb.Append(table.ClassName + "." + HOther.FirstWordLower(field.ENName) + ".length =" + Environment.NewLine);
            }
            sb.Append("" + Environment.NewLine);

            //必填
            foreach (HField field in m_Datas)
            {
                sb.Append(table.ClassName + "." + HOther.FirstWordLower(field.ENName) + ".required =" + Environment.NewLine);
            }

            sb.Append("" + Environment.NewLine);
            sb.Append("第二部分，内容值" + Environment.NewLine);
            sb.Append("" + Environment.NewLine);

            //格式内容
            foreach (HField field in m_Datas)
            {
                sb.Append(field.CHName + "长度不能超过{1}." + Environment.NewLine);
            }
            sb.Append("" + Environment.NewLine);

            //必填内容
            foreach (HField field in m_Datas)
            {
                sb.Append("请录入" + field.CHName + "." + Environment.NewLine);
            }

            sb.Append("" + Environment.NewLine);
            sb.Append("所有实体属性：" + Environment.NewLine);
            foreach (HField field in m_Datas)
            {
                sb.Append(HOther.FirstWordLower(field.ENName) + ",");
            }
            sb.Append("" + Environment.NewLine);
            sb.Append("所有实体属性（大写）：" + Environment.NewLine);
            foreach (HField field in m_Datas)
            {
                sb.Append(field.ENName.ToUpper() + ",");
            }
            sb.Append("" + Environment.NewLine);
            sb.Append("所有属性中文名：" + Environment.NewLine);
            foreach (HField field in m_Datas)
            {
                sb.Append(field.CHName + ",");
            }
            sb.Append("" + Environment.NewLine);

            HOther.SaveStringToFile(sb.ToString(), rootFolder + "\\" + table.ClassName + "字段验证配置.txt");
        }

        private static string GetCSType(HField field)
        {
            string f1 = "";
            if (field.Type == HFieldType.NVARCHAR2 || field.Type == HFieldType.CLOB)
            {
                f1 = "String ";
            }
            else if (field.Type == HFieldType.NUMBER)
            {
                //通过长度是否大于0，判断是整数还是小数
                if (field.Length > 0)
                {
                    f1 = "Integer ";
                }
                else
                {
                    f1 = "Double ";
                }
            }
            else if (field.Type == HFieldType.DATE)
            {
                f1 = "Date ";
            }

            return f1;
        }

        /// <summary>
        /// 生成随机数字
        /// </summary>
        /// <param name="Length">生成长度</param>
        /// <param name="Sleep">是否要在生成前将当前线程阻止以避免重复</param>
        /// <returns></returns>
        private static string GetRandomNumber(int Length, bool Sleep)
        {
            if (Sleep)
                System.Threading.Thread.Sleep(3);
            string result = "";
            System.Random random = new Random();
            for (int i = 0; i < Length; i++)
            {
                result += random.Next(10).ToString();
            }
            return result;
        }
    }
}
