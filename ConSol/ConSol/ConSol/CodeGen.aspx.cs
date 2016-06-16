using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.UI.WebControls;

public partial class CodeGen : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string ns = ConfigurationManager.AppSettings["CodeGenDataNS"];
        txtDataNamespace.Text = ns;
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(ns))
            {
                var bet = Assembly.LoadFrom(Server.MapPath("Bin\\" + ns + ".dll"));
                Type[] types = bet.GetTypes();
                var typeList = (from c in types
                                where c.FullName.Contains(ns + ".Models") && !c.FullName.Contains(ns + ".Models.Mapping")
                                select c.FullName).ToList();
                ddlType.DataSource = typeList;
                ddlType.DataBind();
            }
        }
    }

    protected void BtnCommand_Click(object sender, EventArgs e)
    {
        string ObjectName = txtObjectName.Text;
        string DataNamespace = txtDataNamespace.Text.Trim();
        string uriVal = CodeUtil.ConvertNameToUri(ObjectName);
        string ObjectRealName = CodeUtil.UriToRealName(uriVal);
        string GridViewName = txtGridViewName.Text.Trim();
        txtOutput.Text = "";

        StringBuilder sb = new StringBuilder();

        switch (chkControlType.SelectedValue)
        {
            case "View":
                View(ObjectName, DataNamespace, uriVal, ObjectRealName, GridViewName, sb);
                break;

            case "Add": Add(ObjectName, DataNamespace, uriVal, ObjectRealName, GridViewName, sb); break;
            case "Edit": Edit(ObjectName, DataNamespace, uriVal, ObjectRealName, GridViewName, sb); break;
            default: break;
        }
    }

    private void Edit(string ObjectName, string DataNamespace, string uriVal, string ObjectRealName, string GridViewName, StringBuilder sb)
    {
        string model = txtDataNamespace.Text.Trim() + ".Models." + txtObjectName.Text.Trim();
        //var bet = Assembly.LoadFrom("C:\\Projects\\TMS\\Bin\\ConSol.Data.dll");
        var bet = Assembly.LoadFrom(Server.MapPath("Bin\\" + txtDataNamespace.Text + ".dll"));
        Type[] types = bet.GetTypes();//("\"" + model + "\"",false);
        Type type = null;
        if (types != null)
        {
            foreach (var item in types)
            {
                if (item.FullName == model)
                {
                    type = item;
                }
            }
            PropertyInfo[] mi = type.GetProperties();
            sb.AppendLine(" <div id='message' class='message' runat='server' visible='false' enableviewstate='false'></div>");
            sb.AppendLine("<table style='width: 100%;'>");
            sb.AppendLine("<tbody>");
            foreach (PropertyInfo item in mi)
            {
                sb.AppendLine("<tr>");
                sb.AppendLine("<td style='width: 211px; font-weight: 700;'>");
                sb.AppendLine(item.Name.ToUpper());
                sb.AppendLine("</td>");
                sb.AppendLine("<td>");
                sb.AppendLine("<asp:TextBox  ID='" + "txt" + item.Name.Trim() + "' runat='server'></asp:TextBox>");
                sb.AppendLine("</td>");
                sb.AppendLine("<td style='width: 57px'>");
                sb.AppendLine("&nbsp;");
                sb.AppendLine("</td>");
                sb.AppendLine("</tr>");
            }
            sb.AppendLine("<tr>");
            sb.AppendLine("<td style='height: 21px; width: 211px;'>");
            sb.AppendLine("&nbsp;</td>");
            sb.AppendLine("<td style='height: 21px'>");
            sb.AppendLine("<asp:LinkButton ID='BtnCommand' runat='server' Text='Update " + uriVal + "' OnClick='BtnCommand_Click' />");
            sb.AppendLine("</td>");
            sb.AppendLine("<td style='width: 57px; height: 21px'>");
            sb.AppendLine("&nbsp;</td>");
            sb.AppendLine("</tr>");
            sb.AppendLine("</tbody>");
            sb.AppendLine("</table>");

            sb.AppendLine(""); sb.AppendLine("\r"); sb.AppendLine("\r"); sb.AppendLine("\r");

            sb.AppendLine("string bid = \"\"; string action = \"\";");
            sb.AppendLine("protected void Page_Load(object sender, EventArgs e)");
            sb.AppendLine("{");
            sb.AppendLine("if (Request.Params[\"action\"] != null)");
            sb.AppendLine(" {");
            sb.AppendLine("action = Request.Params[\"action\"];");
            sb.AppendLine("if (int.Parse(action) == 1)");
            sb.AppendLine("{");
            sb.AppendLine("    bid = Request.Params[\"" + txtParams.Text + "\"];");
            sb.AppendLine("}");
            sb.AppendLine("}");
            sb.AppendLine("if (Page.IsPostBack == false)");
            sb.AppendLine("{");
            sb.AppendLine("    if (bid != \"\")");
            sb.AppendLine("    {");
            sb.AppendLine("        Bank dnd = DataService.Provider.GetBankByID(int.Parse(bid));");
            sb.AppendLine("       lblConfigID.Text = dnd.ID.ToString();");
            sb.AppendLine("       txtConfigValue.Text = dnd.BankName;");
            sb.AppendLine("       BtnCommand.Text = \" Update " + uriVal + "\";");
            sb.AppendLine("   }");
            sb.AppendLine("}");
            sb.AppendLine(" }");

            sb.AppendLine("protected void BtnCommand_Click(object sender, EventArgs e)");
            sb.AppendLine("{");
            sb.AppendLine("    int result = -1;");
            sb.AppendLine("    try");
            sb.AppendLine("    {");
            sb.AppendLine("        if (Request.Params[\"action\"] != null)");
            sb.AppendLine("        {");
            sb.AppendLine("            action = Request.Params[\"action\"];");
            sb.AppendLine("            if (int.Parse(action) == 1)");
            sb.AppendLine("            {");
            sb.AppendLine("                bid =  Request.Params[\"" + txtParams.Text + "\"];");
            sb.AppendLine(string.Format("{0} aston = new {0}();", model));
            foreach (PropertyInfo item in mi)
            {
                sb.AppendLine("aston." + item.Name + " = txt" + item.Name + ".Text;");
            }
            //sb.AppendLine("                aston.BankName = (txtConfigValue.Text == string.Empty) ? \"\" : txtConfigValue.Text;");
            sb.AppendLine("                result = DataService.Provider.Update" + uriVal + "(aston);");
            sb.AppendLine("            }");
            sb.AppendLine("            else");
            sb.AppendLine("            {");
            sb.AppendLine("");
            sb.AppendLine("            }");
            sb.AppendLine("        }");
            sb.AppendLine("    }");
            sb.AppendLine("    catch (Exception ex)");
            sb.AppendLine("    {");
            sb.AppendLine("        ShowError(ex.Message);");
            sb.AppendLine("    }");
            sb.AppendLine("    if (result > 0)");
            sb.AppendLine("    {");
            sb.AppendLine("        Response.Redirect(\"~/p.aspx?p=" + txtListUrl.Text + "\", true);");
            sb.AppendLine("    }");
            sb.AppendLine("}");

            txtOutput.Text = sb.ToString();
        }
    }

    private void View(string ObjectName, string DataNamespace, string uriVal, string ObjectRealName, string GridViewName, StringBuilder sb)
    {
        sb.AppendLine("<asp:GridView ID=\"" + GridViewName + "\" runat=\"server\" AutoGenerateColumns=\"False\" AllowPaging=\"True\"");
        sb.AppendLine("EmptyDataText=\"No record yet in this list\" CellPadding=\"5\" CellSpacing=\"5\" CaptionAlign=\"Left\"");
        sb.AppendLine("HorizontalAlign=\"Left\" OnPageIndexChanging=\"" + GridViewName + "_PageIndexChanging\" ForeColor=\"#333333\"");
        sb.AppendLine("GridLines=\"None\" BorderWidth=\"5\" Width=\"100%\">");
        sb.AppendLine("<FooterStyle BackColor=\"#990000\" Font-Bold=\"True\" ForeColor=\"White\" />");
        sb.AppendLine("<RowStyle BackColor=\"#FFFBD6\" ForeColor=\"#333333\" />");
        sb.AppendLine("<PagerStyle BackColor=\"#FFCC66\" ForeColor=\"#333333\" HorizontalAlign=\"Center\" />");
        sb.AppendLine("<SelectedRowStyle BackColor=\"#FFCC66\" Font-Bold=\"True\" ForeColor=\"Navy\" />");
        sb.AppendLine("<HeaderStyle BackColor=\"#990000\" Font-Bold=\"True\" ForeColor=\"White\" />");
        sb.AppendLine("<AlternatingRowStyle BackColor=\"White\" />");
        sb.AppendLine("<Columns>");
        sb.AppendLine("   <asp:BoundField DataField=\"ID\" HeaderText=\"Id\" SortExpression=\"ID\" />");
        sb.AppendLine("    <asp:BoundField DataField=\"Name\" HeaderText=\"Department Name\" ReadOnly=\"True\"");
        sb.AppendLine("       SortExpression=\"Name\" />");
        sb.AppendLine("  <asp:TemplateField>");
        sb.AppendLine("      <ItemTemplate>");
        sb.AppendLine("        <asp:HyperLink ID=\"HyperLink1\" runat=\"server\" Text=\"Edit " + uriVal + "\" NavigateUrl='<%# \"~/p.aspx?p=edit-" + uriVal.ToLower() + "&action=1&ccid=\" + Eval(\"ID\") %>' />");
        sb.AppendLine("     </ItemTemplate>");
        sb.AppendLine(" </asp:TemplateField>");
        sb.AppendLine(" </Columns>");
        sb.AppendLine("</asp:GridView>");
        sb.AppendLine("<br />");
        sb.AppendLine("<br />");
        sb.AppendLine("<div class=\"clear\" style=\"clear: both;\">");
        sb.AppendLine("</div>");
        sb.AppendLine("<asp:HyperLink ID=\"HyperLink2\" runat=\"server\" Text=\"Add New " + uriVal + "\" NavigateUrl=\"~/p.aspx?p=add-" + uriVal.ToLower() + "&action=0\" />");
        sb.AppendLine("");
        sb.AppendLine(""); sb.AppendLine("\r"); sb.AppendLine("\r"); sb.AppendLine("\r");
        sb.AppendLine("using " + DataNamespace + ";");
        sb.AppendLine("public partial class Controls_" + GridViewName + " : BaseControl");
        sb.AppendLine("       {");
        sb.AppendLine("protected void Page_Load(object sender, EventArgs e)");
        sb.AppendLine("       {");
        sb.AppendLine("              Page.Title = \":: " + ObjectRealName + " Maintenance ::\";");
        sb.AppendLine("               BindList();");
        sb.AppendLine("       }");

        sb.AppendLine("            protected void " + GridViewName + "_PageIndexChanging(object sender, GridViewPageEventArgs e)");
        sb.AppendLine("            {");
        sb.AppendLine("                " + GridViewName + ".PageIndex = e.NewPageIndex;");
        sb.AppendLine("               BindList();");
        sb.AppendLine("           }");

        sb.AppendLine("            private void BindList()");
        sb.AppendLine("            {");
        sb.AppendLine("               " + GridViewName + ".DataSource = DataService.Provider.GetAll" + ObjectName + "s();");
        sb.AppendLine("               " + GridViewName + ".DataBind();");
        sb.AppendLine("          }");
        sb.AppendLine("        }");
        txtOutput.Text = sb.ToString();
    }

    private void Add(string ObjectName, string DataNamespace, string uriVal, string ObjectRealName, string GridViewName, StringBuilder sb)
    {
        string model = txtDataNamespace.Text.Trim() + ".Models." + txtObjectName.Text.Trim();
        //var bet = Assembly.LoadFrom("C:\\Projects\\TMS\\Bin\\ConSol.Data.dll");
        var bet = Assembly.LoadFrom(Server.MapPath("Bin\\" + txtDataNamespace.Text + ".dll"));
        Type[] types = bet.GetTypes();//("\"" + model + "\"",false);
        Type type = null;
        if (types != null)
        {
            foreach (var item in types)
            {
                if (item.FullName == model)
                {
                    type = item;
                }
            }
            PropertyInfo[] mi = type.GetProperties();
            sb.AppendLine(" <div id='message' class='message' runat='server' visible='false' enableviewstate='false'></div>");
            sb.AppendLine("<table style='width: 100%;'>");
            sb.AppendLine("<tbody>");
            foreach (PropertyInfo item in mi)
            {
                sb.AppendLine("<tr>");
                sb.AppendLine("<td style='width: 211px; font-weight: 700;'>");
                sb.AppendLine(item.Name.ToUpper());
                sb.AppendLine("</td>");
                sb.AppendLine("<td>");
                sb.AppendLine("<asp:TextBox  ID='" + "txt" + item.Name.Trim() + "' runat='server'></asp:TextBox>");
                sb.AppendLine("</td>");
                sb.AppendLine("<td style='width: 57px'>");
                sb.AppendLine("&nbsp;");
                sb.AppendLine("</td>");
                sb.AppendLine("</tr>");
            }
            sb.AppendLine("<tr>");
            sb.AppendLine("<td style='height: 21px; width: 211px;'>");
            sb.AppendLine("&nbsp;</td>");
            sb.AppendLine("<td style='height: 21px'>");
            sb.AppendLine("<asp:LinkButton ID='BtnCommand' runat='server' Text='Add  " + uriVal + "'' OnClick='BtnCommand_Click' />");
            sb.AppendLine("</td>");
            sb.AppendLine("<td style='width: 57px; height: 21px'>");
            sb.AppendLine("&nbsp;</td>");
            sb.AppendLine("</tr>");
            sb.AppendLine("</tbody>");
            sb.AppendLine("</table>");
            sb.AppendLine(""); sb.AppendLine("\r"); sb.AppendLine("\r"); sb.AppendLine("\r");
            sb.AppendLine("protected void Page_Load(object sender, EventArgs e)");
            sb.AppendLine("{");

            sb.AppendLine("}");
            sb.AppendLine("protected void BtnCommand_Click(object sender, EventArgs e)");
            sb.AppendLine("{");
            sb.AppendLine(string.Format("{0} curr = new {0}();", model));
            foreach (PropertyInfo item in mi)
            {
                sb.AppendLine("curr." + item.Name + " = txt" + item.Name + ".Text;");
            }
            sb.AppendLine("int result = DataService.Provider.Add" + txtObjectName.Text + "(curr);");
            sb.AppendLine("if (result > 0) { Response.Redirect(Util.BaseSiteUrl + 'p.aspx?p=departments', true); }");
            sb.AppendLine("}");

            sb.AppendLine(""); sb.AppendLine("\r"); sb.AppendLine("\r"); sb.AppendLine("\r");
            sb.AppendLine("public abstract int Add" + txtObjectName.Text + "(" + txtObjectName.Text + " curr);");
            sb.AppendLine(""); sb.AppendLine("\r"); sb.AppendLine("\r"); sb.AppendLine("\r");

            sb.AppendLine("public override int Add" + txtObjectName.Text + "(" + txtObjectName.Text + " curr)");
            sb.AppendLine("{");
            sb.AppendLine(" if (curr != null)");
            sb.AppendLine("{");
            sb.AppendLine("    db." + txtObjectName.Text + ".Add(curr);");
            sb.AppendLine("    db.SaveChanges();");
            sb.AppendLine("   return 1;");
            sb.AppendLine("}");
            sb.AppendLine("return 0;");
            sb.AppendLine("}");
            txtOutput.Text = sb.ToString();
        }
    }

    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        string url = "", newmet = "", met = ddlType.SelectedValue;
        if (met != "-Select-")
        {
            newmet = met.Remove(0, met.LastIndexOf('.') + 1);
            txtObjectName.Text = newmet;
            if (txtObjectName.Text.Contains('_'))
            {
                url = txtObjectName.Text.Split('_')[1];
            }
            else { url = txtObjectName.Text; }
            txtGridViewName.Text = CodeUtil.UCFirst(CodeUtil.ConvertNameToUri(url)) + "ListGrid";
            txtEditUrl.Text = "edit-" + CodeUtil.ConvertNameToUri(url).ToLower();
            txtAddUrl.Text = "add-" + CodeUtil.ConvertNameToUri(url).ToLower();
            txtListUrl.Text = CodeUtil.ConvertNameToUri(url).ToLower() + "-list";
            txtParams.Text = CodeUtil.GetParamId(CodeUtil.UCFirst(CodeUtil.ConvertNameToUri(url)));
        }
    }
}

#region Utility

public static class CodeUtil
{
    public static string ConvertNameToUri(string ObjectName)
    {
        List<char> listchar = new List<char>();
        if (!string.IsNullOrEmpty(ObjectName))
        {
            Char[] charArray = ObjectName.ToCharArray();
            foreach (char item in charArray)
            {
                if (Char.IsUpper(item))
                {
                    if (listchar.Count > 0)
                    {
                        listchar.Add('-');
                        listchar.Add(item);
                    }
                    else
                    {
                        /* charArray2 is null - no item in it  */
                        listchar.Add(item);
                    }
                }
                else
                {
                    listchar.Add(item);
                }
            }
            string result = new string(listchar.ToArray());
            return result;
        }
        return string.Empty;
    }

    public static string UriToRealName(string uriVal)
    {
        if (!string.IsNullOrEmpty(uriVal))
        {
            return uriVal.Replace('-', ' ');
        }
        return string.Empty;
    }

    internal static string UCFirst(string p)
    {
        List<char> chars2 = new List<char>();
        char[] chars = p.ToCharArray();
        foreach (var item in chars)
        {
            if (chars2.Count > 0) { chars2.Add(item); }
            else
            {
                chars2.Add(char.ToUpper(item));
            }
        }
        string me = new string(chars2.ToArray());
        chars2 = null; chars = null;
        return me.Replace("-", "");
    }

    internal static string GetParamId(string ObjectName)
    {
        List<char> listchar = new List<char>();
        if (!string.IsNullOrEmpty(ObjectName))
        {
            Char[] charArray = ObjectName.ToCharArray();
            foreach (char item in charArray)
            {
                if (Char.IsUpper(item))
                {
                    listchar.Add(item);
                }
            }
            string result = new string(listchar.ToArray());
            return (result + "id").ToLower();
        }
        return string.Empty;
    }
}

#endregion Utility