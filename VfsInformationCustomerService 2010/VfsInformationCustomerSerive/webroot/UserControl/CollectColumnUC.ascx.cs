using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Globalization;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Collections.Generic;
using VfsCustomerService.Business;
using VfsCustomerService.Data;
using VfsCustomerService.Entities;
using VfsCustomerService.Utility;
using Vfs.WebCrawler.Utility;
using System.Collections.Specialized;

public partial class UserControl_CollectColumnUC : System.Web.UI.UserControl
{
    protected Int32 templateId;
    protected string fileName;
    Hashtable hashtable = new Hashtable();
    string[] headers;
    ContentTemplate contentTemplate;
    protected Int32 SuccessCount = 0;
    protected Int32 FailedCount = 0;

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        GetParameters();
        CreateTextBoxObject();
        this.importStep1Button.Value = Resources.UIResource.CreateMessageFromExcel;
        previewMessageButton.Value = Resources.UIResource.PreviewMessage;
    }


    private void GetParameters()
    {
        templateId = AppConstants.GetInt32(AppConstants.QS_TEMPLATE_ID);
        fileName = AppConstants.GetString(AppConstants.QS_SESSION);
        contentTemplate = ContentTemplateService.GetContentTemplate(this.templateId);
        hashtable = VfsCustomerService.Business.ImportService.GetParameters(contentTemplate);
        ImportService.ReadCvsHeader(ApplicationHelper.AttachementUploadFolderPath + fileName + ".txt", out headers);
    }

    protected void CreateDropDownObject()
    {
        //StringBuilder stringBuilder = new StringBuilder();
        //stringBuilder.Append(""
    }

    protected void CreateTextBoxObject()   
    {
        
        StringBuilder stringBuilder = new StringBuilder();
        Int32 i = 0;
        
        foreach(DictionaryEntry x in hashtable)
        {
            TextBox textBox = new TextBox();
            textBox.ID = "SourceColumn" + "_TextBox" + i.ToString();
            textBox.Text = x.Key.ToString();
            TableRow htmlTableRow = new TableRow();

            DropDownList dropDownList = new DropDownList();
            dropDownList.ID = "MatchColumn" + "_DropDownList_" + i.ToString();
            dropDownList.DataSource = ConstructDropDownDataSource(headers);
            dropDownList.DataBind();         

            CollectColumnTable.Controls.Add(textBox);
            MatchColumnTable.Controls.Add(dropDownList);
            i++;
        }
    }
    
    protected List<ListItem> ConstructDropDownDataSource(string[] headers)
    {
        List<ListItem> returnlist = new List<ListItem>();
        foreach (string header in headers)
        {
            ListItem item = new ListItem();
            item.Value = header;
            item.Text = header;
            returnlist.Add(item);
        }
        return returnlist;
    }

    protected void ImportStep1Button_Click(object sender, EventArgs e)
    {
        NameValueCollection ParamNameAndValue = new NameValueCollection();
        List<string[]> failedData = new List<string[]>();
        for (int i = 0; i < hashtable.Count; i++)
        {
            Control controlParamName = this.CollectColumnTable.FindControl("SourceColumn" + "_TextBox" + i.ToString());
            Control controlParamValue = this.CollectColumnTable.FindControl("MatchColumn" + "_DropDownList_" + i.ToString());

            if (controlParamName != null && controlParamValue != null)
            {
                ParamNameAndValue.Add(((TextBox)controlParamName).Text, ((DropDownList)controlParamValue).SelectedIndex.ToString());                
            }
        }
        ImportService.CreateMessage(ApplicationHelper.AttachementUploadFolderPath, fileName + ".txt", this.contentTemplate, ParamNameAndValue, out SuccessCount, out failedData);
        StringBuilder stringBuilder = new StringBuilder();

        if (failedData.Count > 0)
        {
            FailedCount = failedData.Count;
            foreach (string[] items in failedData)
            {
                for(int i=0;i<items.Length -1;i++)
                {
                    stringBuilder.Append(items[i] + ";");
                }
                stringBuilder.Append("<br />");
            }
        }
        successMessage.Text = stringBuilder.ToString();
        importStep1Button.Disabled = true;
        previewMessageButton.Disabled = true;
    }

    protected void PreviewMessageButton_Click(object sender, EventArgs e)
    {
        NameValueCollection ParamNameAndValue = new NameValueCollection();
        for (int i = 0; i < hashtable.Count; i++)
        {
            Control controlParamName = this.CollectColumnTable.FindControl("SourceColumn" + "_TextBox" + i.ToString());
            Control controlParamValue = this.CollectColumnTable.FindControl("MatchColumn" + "_DropDownList_" + i.ToString());

            if (controlParamName != null && controlParamValue != null)
            {
                ParamNameAndValue.Add(((TextBox)controlParamName).Text, ((DropDownList)controlParamValue).SelectedIndex.ToString());                
            }
        }
        previewLiteral.Text = ImportService.PreviewMessage(ApplicationHelper.AttachementUploadFolderPath, fileName + ".txt", this.contentTemplate, ParamNameAndValue);
    }    
}
